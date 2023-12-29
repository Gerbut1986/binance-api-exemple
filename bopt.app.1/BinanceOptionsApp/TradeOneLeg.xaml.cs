using MultiTerminal.Connections;
using MultiTerminal.Connections.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace BinanceOptionsApp
{
    public partial class TradeOneLeg : UserControl, IConnectorLogger, ITradeTabInterface
    {
        Models.TradeModel model;
        ManualResetEvent threadStop;
        ManualResetEvent threadStopped;
        readonly object loglock = new object();
        public TradeOneLeg()
        {
            InitializeComponent();
        }
        public void InitializeTab()
        {
            model = DataContext as Models.TradeModel;
            fast.InitializeProviderControl(model.Fast,true);
            slow.InitializeProviderControl(model.Slow,false);

            model.LogError = LogError;
            model.LogInfo = LogInfo;
            model.LogWarning = LogWarning;
            model.LogClear = LogClear;
            HiddenLogs.LogHeader(model);
            tbFreezeTime.IsEnabled = Model.UseFreezeTime;
        }

        public void RestoreNullCombo(ConnectionModel cm)
        {
            fast.RestoreNullCombo(cm);
            slow.RestoreNullCombo(cm);
        }

        #region Log's methods:
        private void LogInfo(string message)
        {
            Log(message, Colors.White, Color.FromRgb(0x00, 0x23, 0x44));
        }
        private void LogError(string message)
        {
            Log(message, Color.FromRgb(0xf3, 0x56, 0x51), Color.FromRgb(0xf3, 0x56, 0x51));
        }
        private void LogWarning(string message)
        {
            Log(message, Colors.LightBlue, Colors.Blue);
        }
        private void LogClear()
        {
            logBlock.Text = "";
        }
        private void Log(string _message, Color color, Color dashboardColor)
        {
            string message = DateTime.Now.ToString("HH:mm:ss.ffffff") + "> " + _message + "\r\n";
            lock (loglock)
            {
                if (swLogPath != null)
                {
                    System.IO.File.AppendAllText(swLogPath, message);
                    Model.CommonLogSave(message);
                }
            }
            SafeInvoke(() =>
            {
                model.LastLog = _message;
                model.LastLogBrush = new SolidColorBrush(dashboardColor);
                Run r = new Run(message)
                {
                    Tag = DateTime.Now,
                    Foreground = new SolidColorBrush(color)
                };
                try
                {
                    while (logBlock.Inlines.Count > 250)
                    {
                        logBlock.Inlines.Remove(logBlock.Inlines.LastInline);
                    }
                }
                catch
                {

                }
                int count = logBlock.Inlines.Count;
                if (count == 0) logBlock.Inlines.Add(r);
                else
                {
                    logBlock.Inlines.InsertBefore(logBlock.Inlines.FirstInline, r);
                }
            });
        }
        public void SafeInvoke(Action action)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                if (!Model.Closing)
                {
                    action();
                }
            }));
        }
        #endregion

        #region Start & Stop:
        private void BuStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void BuStop_Click(object sender, RoutedEventArgs e)
        {
            Stop(true);
        }

        public void Start()
        {
            if (model.Started) return;
            model.Started = true;
            model.FeederOk = false;
            LogClear();
            HiddenLogs.LogHeader(model);

            threadStop = new ManualResetEvent(false);
            threadStopped = new ManualResetEvent(false);
            new Thread(ThreadProc).Start();
            Model.OnUpdateDashboardStatus();
        }

        public void Stop(bool wait)
        {
            if (!model.Started) return;
            threadStop.Set();
            if (wait)
            {
                threadStopped.WaitOne();
                threadStop.Dispose();
                threadStopped.Dispose();
            }
            model.Started = false;
            model.FeederOk = false;
            Model.OnUpdateDashboardStatus();
        }
        #endregion

        decimal prevSlowBid;
        decimal prevSlowAsk;
        decimal prevFastBid;
        decimal prevFastAsk;

        Helpers.MovingAverage averageBid;
        Helpers.MovingAverage averageAsk;
        Helpers.MovingAverage averageSpread;
        Helpers.MovingAverage averageFastChange;
        Helpers.MovingAverage averageSlowChange;
        Helpers.MovingAverage averageSlowTimeBetweenTicks;
        Helpers.MovingAverage averageFastTimeBetweenTicks;
        Helpers.Ring fastBidRing;
        Helpers.Ring fastAskRing;

        decimal gapBuy;
        decimal gapSell;
        IConnector fastConnector;
        IConnector slowConnector;
        string swDebugPath;
        string swQuotesPath;
        string swLogPath;
        System.IO.FileStream fsData;
        DateTime lastOpenCloseTime;
        string closeSignal;

        private string EscapePath(string path)
        {
            char[] invalid = System.IO.Path.GetInvalidPathChars();
            foreach (var c in invalid)
            {
                path = path.Replace(c, ' ');
            }
            return path;
        }

        DateTime lastClickOpenTime;
        DateTime lastClickCloseTime;

        private void ThreadProc()
        {
            decimal? stopBalance = null;
            decimal? trailSl = null;
            bool balanceOk = true;

            model.Fast.InitView();
            model.Slow.InitView();

            lastClickCloseTime = DateTime.UtcNow.AddDays(-1);
            lastClickOpenTime = DateTime.UtcNow.AddDays(-1);

            fastConnector = model.Fast.CreateConnector(this, threadStop, model.SleepMs, Dispatcher);
            slowConnector = model.Slow.CreateConnector(this, threadStop, model.SleepMs, Dispatcher);
            fastConnector.Tick += FastConnector_Tick;
            slowConnector.TickOption += SlowConnector_Tick;
            fastConnector.LoggedIn += FastConnector_LoggedIn;
            slowConnector.LoggedIn += SlowConnector_LoggedIn;

            model.LogInfo(model.Title + " logging in...");
            while (!threadStop.WaitOne(100))
            {
                if (fastConnector.IsLoggedIn && slowConnector.IsLoggedIn)
                {
                    model.LogInfo(model.Title + " logged in OK.");
                    break;
                }
            }
            if (!threadStop.WaitOne(0))
            {
                if (fastConnector.IsLoggedIn)
                {
                    OnFastLogin();
                }
                if (slowConnector.IsLoggedIn)
                {
                    OnSlowLogin();
                    //OnBuySignal(444, "BTCUSDT", 0.01m, 28700, 1000);
                }
            }
            if (model.Log)
            {
                string stime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string logfolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\.logs";
                logfolder = System.IO.Path.Combine(logfolder, EscapePath(model.Title));
                try
                {
                    System.IO.Directory.CreateDirectory(logfolder);
                }
                catch
                {

                }
                swLogPath = System.IO.Path.Combine(logfolder, "lg_" + stime + ".log");
                swDebugPath = System.IO.Path.Combine(logfolder, "db_" + stime + ".log");
                swQuotesPath = System.IO.Path.Combine(logfolder, "qu_" + stime + ".log");
                System.IO.File.AppendAllText(swDebugPath, "GapBuy;GapSell;FastCoef;Position;AvBid;AvAsk;Spread;SpreadK;MinLev;MinLevClose;FixTp;FixSl;TrailSl;FastSpread;dTFast;tpsFast;dTSlow;tpsSlow;\r\n");
                System.IO.File.AppendAllText(swQuotesPath, "LocalTime;FastTime;FastBid;FastAsk;SlowBid;SlowAsk;GapLong;GapShort;FastCoef;dTFast;tpsFast;dTSlow;tpsSlow;\r\n");
            }
            else
            {
                swLogPath = null;
                swDebugPath = null;
                swQuotesPath = null;
            }
            if (model.SaveTicks)
            {
                string stime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string datafolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\.data";
                datafolder = System.IO.Path.Combine(datafolder, EscapePath(model.Title));
                try
                {
                    System.IO.Directory.CreateDirectory(datafolder);
                }
                catch
                {

                }
                fsData = new System.IO.FileStream(datafolder + "\\" + stime + ".1leg", System.IO.FileMode.Create);
            }

            TimeSpan startTime = model.Open.StartTimeSpan();
            TimeSpan endTime = model.Open.EndTimeSpan();

            lastOpenCloseTime = DateTime.UtcNow;
            while (!threadStop.WaitOne(model.SleepMs))
            {
                decimal fastAsk = model.Fast.Ask;
                decimal fastBid = model.Fast.Bid;
                decimal slowBid = model.Slow.Bid;
                decimal slowAsk = model.Slow.Ask;

                if (fastBid == 0 || fastAsk == 0 || slowBid == 0 || slowAsk == 0) continue;

                bool skip = true;
                if (fastBid != prevFastBid) skip = false;
                if (fastAsk != prevFastAsk) skip = false;
                if (slowBid != prevSlowBid) skip = false;
                if (slowAsk != prevSlowAsk) skip = false;

                decimal fastQuoteUpdateTimeMs = (decimal)(DateTime.Now - model.Fast.Time).TotalMilliseconds;
                decimal slowQuoteUpdateTimeMs = (decimal)(DateTime.Now - model.Slow.Time).TotalMilliseconds;
                model.FeederOk = fastConnector.IsLoggedIn && fastQuoteUpdateTimeMs < 200000 && slowConnector.IsLoggedIn && slowQuoteUpdateTimeMs < 200000;
                if (!model.FeederOk)
                {
                    skip = true;
                }
                if (fastBid>0)
                {
                    decimal k = slowBid / fastBid;
                    if (k > 3 || k < 0.3M) skip = true;
                }
                if (fastBid > fastAsk || fastAsk<=0) skip = true;

                if (!skip)
                {
                    fastBidRing.Push(fastBid);
                    fastAskRing.Push(fastAsk);
                    model.Fast.CalculateViewSpread(model.Point);
                    model.Slow.CalculateViewSpread(model.Point);

                    bool tickSignalBuy = fastBid > prevFastBid;
                    bool tickSignalSell = fastAsk < prevFastAsk;
                    decimal slowGapBid = (slowBid - prevSlowBid) / model.Point;
                    decimal slowGapAsk = (slowAsk - prevSlowAsk) / model.Point;
                    decimal fastGapBid = (fastBidRing.Head(0) - fastBidRing.Head(model.Open.GapFastTicks)) / model.Point;
                    decimal fastGapAsk = (fastAskRing.Head(0) - fastAskRing.Head(model.Open.GapFastTicks)) / model.Point;

                    decimal fastChange = Math.Abs(fastBid - prevFastBid) + Math.Abs(fastAsk - prevFastAsk);
                    decimal slowChange = Math.Abs(slowBid - prevSlowBid) + Math.Abs(slowAsk - prevSlowAsk);
                    fastChange = averageFastChange.Process(fastChange);
                    slowChange = averageSlowChange.Process(slowChange);
                    decimal fastCoef = slowChange > 0 ? fastChange / slowChange : 10000000.0M;

                    prevFastBid = fastBid;
                    prevFastAsk = fastAsk;
                    prevSlowBid = slowBid;
                    prevSlowAsk = slowAsk;

                    //if (model.Open.AvtoShiftBid)
                    //{
                    //    if (model.Open.SignalMode == 0)
                    //    {
                    //        averageBid.Process((fastBid - slowBid) / model.Point);
                    //        gapSell = (fastBid - slowBid) / model.Point - averageBid.Output;

                    //        averageAsk.Process((fastBid - slowBid) / model.Point);
                    //        gapBuy = (fastBid - slowBid) / model.Point - averageAsk.Output;
                    //    }
                    //    else
                    //    {
                    //        averageBid.Process((fastAsk - slowBid) / model.Point);
                    //        gapSell = (fastAsk - slowBid) / model.Point - averageBid.Output;

                    //        averageAsk.Process((fastBid - slowAsk) / model.Point);
                    //        gapBuy = (fastBid - slowAsk) / model.Point - averageAsk.Output;
                    //    }
                    //}
                    //else
                    //{
                    //    if (model.Open.SignalMode == 0)
                    //    {
                    //        fastBid += model.Open.ShiftBid * model.Point;
                    //        fastAsk += model.Open.ShiftAsk * model.Point;
                    //        gapBuy = (fastBid - slowBid) / model.Point;
                    //        gapSell = (fastBid - slowBid) / model.Point;
                    //    }
                    //    else
                    //    {
                    //        fastBid += model.Open.ShiftBid * model.Point;
                    //        fastAsk += model.Open.ShiftAsk * model.Point;
                    //        gapBuy = (fastBid - slowAsk) / model.Point;
                    //        gapSell = (fastAsk - slowBid) / model.Point;
                    //    }
                    //}

                    //model.GapSell = gapSell;
                    //model.GapBuy = gapBuy;

                    if (model.AllowView)
                    {
                    }
                    if (fsData != null)
                    {
                        DateTime saveTime = DateTime.UtcNow;
                        SaveData(BitConverter.GetBytes((double)fastBid));
                        SaveData(BitConverter.GetBytes((double)fastAsk));
                        SaveData(BitConverter.GetBytes((double)slowBid));
                        SaveData(BitConverter.GetBytes((double)slowAsk));
                        SaveData(BitConverter.GetBytes((double)gapBuy));
                        SaveData(BitConverter.GetBytes((double)gapSell));
                        SaveData(BitConverter.GetBytes(saveTime.Ticks));
                    }

                    if (swQuotesPath != null)
                    {
                        System.IO.File.AppendAllText(swQuotesPath, DateTime.UtcNow.ToString("HH:mm:ss.ffffff") + ";" +
                                                           model.Fast.Time.ToString("HH:mm:ss.ffffff") + ";" +
                                                           model.FormatPrice(fastBid) + ";" +
                                                           model.FormatPrice(fastAsk) + ";" +
                                                           model.FormatPrice(slowBid) + ";" +
                                                           model.FormatPrice(slowAsk) + ";" +
                                                           ToStr2(gapBuy) + ";" +
                                                           ToStr2(gapSell) + ";"+
                                                           ToStr2(fastCoef) +
                                                           ToStr2(model.Fast.AverageTimeBetweenTicks) + ";"+
                                                           ToStr2(model.Fast.GetTicksPerSecond()) + ";"+
                                                           ToStr2(model.Slow.AverageTimeBetweenTicks) + ";"+
                                                           ToStr2(model.Slow.GetTicksPerSecond()) + ";\r\n");
                    }
                    decimal spread = (slowAsk - slowBid) / model.Point;
                    if (model.Open.UseAverageSpread)
                    {
                        spread=averageSpread.Process(spread);
                    }
                    decimal spreadK = 1.0M;
                    if (model.Open.AvtoSettings)
                    {
                        spreadK = spread;
                        if (spreadK < 1.0M) spreadK = 1.0M;
                    }

                    var orders = slowConnector.GetOrders(model.Slow.FullSymbol, model.Magic,1);
                    var order = orders.FirstOrDefault();
                    decimal profit = 0;
                    if (order==null)
                    {
                        trailSl = null;
                        model.Slow.Volume = 0;
                    }
                    else
                    {
                        if (order.Side == OrderSide.Buy)
                        {
                            profit = slowBid - order.OpenPrice;
                        }
                        else
                        {
                            profit = order.OpenPrice - slowAsk;
                        }
                        model.Slow.Volume = order.Volume*orders.Count;
                    }
                    decimal minlev = model.Open.MinimumLevel * spreadK+spread+model.Open.Comission;
                    decimal minlevclose = model.Close.MinimumLevelClose * spreadK;
                    decimal fixtp = model.Close.FixTP * spreadK+model.Open.Comission;
                    decimal fixsl = model.Close.FixSL * spreadK + spread;
                    decimal fastSpread = (fastAsk - fastBid) / model.Point;
                    decimal slowSpread = (slowAsk - slowBid) / model.Point;

                    decimal fixtrailstart = model.Close.FixTrailStart * spreadK + model.Open.Comission;
                    decimal fixtrailstop= model.Close.FixTrailStop * spreadK + spread;

                    if (swDebugPath != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(ToStr2(gapBuy) + ";");
                        sb.Append(ToStr2(gapSell) + ";");
                        sb.Append(ToStr2(fastCoef) + ";");
                        if (order!=null)
                        {
                            sb.Append(order.Side.ToString() + " " + model.FormatPrice(order.OpenPrice) + " [" + model.FormatPrice(profit) + " pts];");
                        }
                        else
                        {
                            sb.Append("none;");
                        }
                        decimal avbid_ = averageBid.Output;
                        decimal avask_ = averageAsk.Output;
                        sb.Append(model.FormatPrice(avbid_) + ";");
                        sb.Append(model.FormatPrice(avask_) + ";");
                        sb.Append(ToStr2(spread) + ";");
                        sb.Append(ToStr2(spreadK) + ";");
                        sb.Append(ToStr2(minlev) + ";");
                        sb.Append(ToStr2(minlevclose) + ";");
                        sb.Append(ToStr2(fixtp) + ";");
                        sb.Append(ToStr2(fixsl) + ";");
                        sb.Append(ToStr2(trailSl ?? -1000) + ";");
                        sb.Append(ToStr2(fastSpread) + ";");
                        sb.Append(ToStr2(model.Fast.AverageTimeBetweenTicks) + ";");
                        sb.Append(ToStr2(model.Fast.GetTicksPerSecond()) + ";");
                        sb.Append(ToStr2(model.Slow.AverageTimeBetweenTicks) + ";");
                        sb.Append(ToStr2(model.Slow.GetTicksPerSecond()) + ";\r\n");
                        System.IO.File.AppendAllText(swDebugPath, sb.ToString());
                    }

                    bool slowSignal = false;
                    if (Math.Abs(slowGapBid) <= -minlev) slowSignal = true;
                    if (Math.Abs(slowGapAsk) >= minlev) slowSignal = true;

                    model.Fast.Balance = fastConnector.Balance;
                    model.Slow.Balance = slowConnector.Balance;

                    if (slowConnector.Balance != null)
                    {
                        if (stopBalance == null)
                        {
                            stopBalance = slowConnector.Balance - (slowConnector.Balance * model.Open.RiskDeposit * 0.01M);
                            model.LogInfo("StopBalance = " + ToStr2(stopBalance.Value));
                        }
                        if (stopBalance != null)
                        {
                            if (slowConnector.Balance < stopBalance)
                            {
                                if (balanceOk)
                                {
                                    model.LogError("Drfvdown reached. Trading stopped.");
                                }
                                balanceOk = false;
                            }
                        }
                    }

                    bool fastGapOkForBuy = true;
                    bool fastGapOkForSell = true;
                    if (model.Open.MinGapFast > 0)
                    {
                        fastGapOkForBuy = fastGapAsk >= model.Open.MinGapFast;
                        fastGapOkForSell = fastGapBid <= -model.Open.MinGapFast;
                    }
                    bool fastSpreadOk = fastSpread >= model.Open.MinSpread && (fastSpread <= model.Open.MaxSpread || model.Open.MaxSpread == 0);
                    bool slowSpreadOk = slowSpread >= model.Open.MinSpreadSlow && (slowSpread <= model.Open.MaxSpreadSlow || model.Open.MaxSpreadSlow == 0);
                    if (fastSpreadOk && fastAsk >= fastBid && slowSpreadOk)
                    {
                        if (order == null)
                        {
                            if ((DateTime.UtcNow - lastOpenCloseTime).TotalMilliseconds >= 1500 && model.Open.IsInStartEndSpan(DateTime.Now, startTime, endTime))
                            {
                                decimal lot = model.Open.Lot;
                                if (model.Open.RiskPercent > 0 && slowConnector.Balance != null)
                                {
                                    lot = Models.OpenOrderSettingsModel.CalculateDynamicLot(slowConnector.Balance.Value, model.Open.RiskPercent, model.Slow.MinLot, model.Slow.LotStep);
                                }
                                bool fastCoefOk = true;
                                if (model.Open.MinFastCoef > 0)
                                {
                                    fastCoefOk = fastCoef >= model.Open.MinFastCoef;
                                }
                                bool maxGapOk = true;
                                bool freezeOk = true;
                                if (Model.UseFreezeTime && model.Open.FreezeTimeMs > 0)
                                {
                                    TimeSpan delta = DateTime.Now - model.Slow.Time;
                                    freezeOk = delta.TotalMilliseconds >= model.Open.FreezeTimeMs;
                                }

                                if (model.Fast.GetTicksPerSecond() >= model.Open.MinimumTPS)
                                {
                                    bool buyCond = gapBuy >= minlev && tickSignalBuy && fastCoefOk && model.AllowOpen && !slowSignal && Math.Abs(gapBuy) > spread && gapBuy >= model.Open.MinGapForOpen && balanceOk;
#if DEBUG
                                    buyCond = true;
#endif
                                    if (buyCond)
                                    {
                                        if (model.Open.MaxGapForOpen > 0)
                                        {
                                            maxGapOk = gapBuy <= model.Open.MaxGapForOpen;
                                        }
                                        if (maxGapOk && freezeOk && fastGapOkForBuy)
                                        {
                                            if (model.Open.NTrades > 1)
                                            {
                                                MyParallel.Invoke(model.Open.NTrades, () => { OnBuySignal(fastSpread, model.Slow.FullSymbol, lot, slowAsk, gapBuy); });
                                            }
                                            else
                                            {
                                                OnBuySignal(fastSpread, model.Slow.FullSymbol, lot, slowAsk, gapBuy);
                                            }
                                        }
                                    }
                                    else
                                    if (gapSell < -minlev && tickSignalSell && fastCoefOk && model.AllowOpen && !slowSignal && Math.Abs(gapSell) > spread && Math.Abs(gapSell) >= model.Open.MinGapForOpen && balanceOk)
                                    {
                                        if (model.Open.MaxGapForOpen > 0)
                                        {
                                            maxGapOk = gapSell >= -model.Open.MaxGapForOpen;
                                        }
                                        if (maxGapOk && freezeOk && fastGapOkForSell)
                                        {
                                            if (model.Open.NTrades > 1)
                                            { 
                                                MyParallel.Invoke(model.Open.NTrades, () => { OnSellSignal(fastSpread, model.Slow.FullSymbol, lot, slowBid, gapSell); });
                                            }
                                            else
                                            {
                                                OnSellSignal(fastSpread, model.Slow.FullSymbol, lot, slowBid, gapSell);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (order.Side == OrderSide.Buy)
                            {
                                bool closed = false;
                                closeSignal = "";

                                bool timerok = (slowConnector.CurrentTime - order.OpenTime).TotalSeconds >= model.Open.MinOrderTimeSec;
                                if (model.Close.CloseTimerSec > 0)
                                {
                                    if ((slowConnector.CurrentTime - order.OpenTime).TotalSeconds >= model.Close.CloseTimerSec)
                                    {
                                        closed = true;
                                        closeSignal = "by TIMER";
                                    }
                                }
                                if (!closed && model.Close.MinimumLevelClose != 0 && timerok)
                                {
                                    if (gapBuy <= -minlevclose) 
                                    {
                                        closed = true;
                                        closeSignal = "by GAP";
                                    }
                                }
                                if (!closed && model.Close.CloseTimerSec == 0)
                                {
                                    if (profit >= (fixtp * model.Point) && timerok)
                                    {
                                        closed = true;
                                        closeSignal = "by TP";
                                    }
                                    else if (profit <= (-fixsl * model.Point))
                                    {
                                        closed = true;
                                        closeSignal = "by SL";
                                    }
                                }
                                if (!closed && model.Close.Trailing && timerok && model.Close.CloseTimerSec == 0)
                                {
                                    if (trailSl.HasValue)
                                    {
                                        if (profit <= (-trailSl * model.Point))
                                        {
                                            closed = true;
                                            closeSignal = "by Trail";
                                        }
                                    }
                                    if (!closed)
                                    {
                                        if (profit >= (fixtrailstart * model.Point))
                                        {
                                            decimal newtrailSl = -((profit / model.Point) - fixtrailstop);
                                            if (!trailSl.HasValue)
                                            {
                                                trailSl = newtrailSl;
                                            }
                                            else
                                            {
                                                if (newtrailSl < trailSl.Value) trailSl = newtrailSl;
                                            }
                                        }
                                    }
                                }
                                if (closed)
                                {
                                    if (orders.Count > 1)
                                    {
                                        closeSignal += " multi";
                                        MyParallel.For(0, orders.Count, (int no) => { OnCloseSignal(fastSpread, orders[no], slowBid, gapBuy); });
                                    }
                                    else
                                    {
                                        OnCloseSignal(fastSpread, order, slowBid, gapBuy);
                                    }
                                }
                            }
                            else
                            if (order.Side == OrderSide.Sell)
                            {
                                bool closed = false;
                                closeSignal = "";
                                bool timerok = (slowConnector.CurrentTime - order.OpenTime).TotalSeconds >= model.Open.MinOrderTimeSec;
                                if (model.Close.CloseTimerSec > 0)
                                {
                                    if ((slowConnector.CurrentTime - order.OpenTime).TotalSeconds >= model.Close.CloseTimerSec)
                                    {
                                        closed = true;
                                        closeSignal = "by TIMER";
                                    }
                                }
                                if (!closed && model.Close.MinimumLevelClose != 0 && timerok)
                                {
                                    if (gapSell >= minlevclose) 
                                    {
                                        closed = true;
                                        closeSignal = "by GAP";
                                    }
                                }
                                if (!closed && model.Close.CloseTimerSec == 0)
                                {
                                    if (profit >= (fixtp * model.Point) && timerok)
                                    {
                                        closed = true;
                                        closeSignal = "by TP";
                                    }
                                    else if (profit <= (-fixsl * model.Point))
                                    {
                                        closed = true;
                                        closeSignal = "by SL";
                                    }
                                }
                                if (!closed && model.Close.Trailing && timerok && model.Close.CloseTimerSec == 0)
                                {
                                    if (trailSl.HasValue)
                                    {
                                        if (profit <= (-trailSl * model.Point))
                                        {
                                            closed = true;
                                            closeSignal = "by Trail";
                                        }
                                    }
                                    if (!closed)
                                    {
                                        if (profit >= (fixtrailstart * model.Point))
                                        {
                                            decimal newtrailSl = -((profit / model.Point) - fixtrailstop);
                                            if (!trailSl.HasValue)
                                            {
                                                trailSl = newtrailSl;
                                            }
                                            else
                                            {
                                                if (newtrailSl < trailSl.Value) trailSl = newtrailSl;
                                            }
                                        }
                                    }
                                }
                                if (closed)
                                {
                                    if (orders.Count > 1)
                                    {
                                        closeSignal += " multi";
                                        MyParallel.For(0, orders.Count, (int no) => { OnCloseSignal(fastSpread, orders[no], slowBid, gapBuy); });
                                    }
                                    else
                                    {
                                        OnCloseSignal(fastSpread, order, slowBid, gapBuy);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            slowConnector.TickOption -= SlowConnector_Tick;
            fastConnector.Tick -= FastConnector_Tick;
            slowConnector.LoggedIn -= SlowConnector_LoggedIn;
            fastConnector.LoggedIn -= FastConnector_LoggedIn;
            ConnectorsFactory.Current.CloseConnector(model.Slow.Name,true);
            ConnectorsFactory.Current.CloseConnector(model.Fast.Name,true);

            swQuotesPath = null;
            swDebugPath = null;
            swLogPath = null;
            if (fsData != null)
            {
                fsData.Flush();
                fsData.Dispose();
                fsData = null;
            }
            threadStopped.Set();
        }


        void OnSlowLogin()
        {
            slowConnector.Fill = (FillPolicy)model.Open.Fill;
            slowConnector.Subscribe(model.Slow.FullSymbol, model.Slow.GetSymbolId());
        }
        void OnFastLogin()
        {
            fastConnector.Fill = (FillPolicy)model.Open.Fill;
            var symbId = model.Fast.GetSymbolId();
            fastConnector.Subscribe(model.Fast.FullSymbol, symbId);
        }

        private void SlowConnector_LoggedIn(object sender, EventArgs e)
        {
            OnSlowLogin();
        }

        private void FastConnector_LoggedIn(object sender, EventArgs e)
        {
            OnFastLogin();
        }

        void SaveData(byte[] data)
        {
            fsData.Write(data, 0, data.Length);
        }
        private void SlowConnector_Tick(object sender, TickEventArgsOptions e)
        {
            if (e.Symbol == model.Slow.FullSymbol)
            {
                if (model.Slow.Time.Year>2000)
                {
                    TimeSpan delta = DateTime.Now - model.Slow.Time;
                    //model.Slow.AverageTimeBetweenTicks = averageSlowTimeBetweenTicks.Process((decimal)delta.TotalMilliseconds);
                    decimal tps = model.Slow.GetTicksPerSecond();
                    if (tps > model.Slow.MaxTPS) model.Slow.MaxTPS = tps;
                }
                //model.Slow.Bid = e.Bid;
                //model.Slow.Ask = e.Ask;
                model.Slow.Time = DateTime.Now;
            }
        }

        private void FastConnector_Tick(object sender, TickEventArgs e)
        {
            if (e.Symbol == model.Fast.FullSymbol)
            {
                if (model.Fast.Time.Year > 2000)
                {
                    TimeSpan delta = DateTime.Now - model.Fast.Time;
                    //model.Fast.AverageTimeBetweenTicks = averageFastTimeBetweenTicks.Process((decimal)delta.TotalMilliseconds);
                    decimal tps = model.Fast.GetTicksPerSecond();
                    if (tps > model.Fast.MaxTPS) model.Fast.MaxTPS = tps;
                }
                if (BinanceOptionClient.OptChains.Count != 0)
                {

                }
                model.Fast.Bid = e.Bid;
                model.Fast.Ask = e.Ask;
                model.Fast.Time = DateTime.Now;
            }
        }

        int GetMs(DateTime begin0)
        {
            return (int)((DateTime.UtcNow - begin0).TotalMilliseconds);
        }

        protected void OnBuySignal(decimal fastSpread, string symbol, decimal lot, decimal price, decimal gap)
        {
            DateTime begin0 = DateTime.UtcNow;
            if (Model.Options.Clicker.UseClickerForOpen)
            {
                if ((DateTime.UtcNow - lastClickOpenTime).TotalSeconds > 3)
                {
                    Model.Options.Clicker.ClickBuy();
                    model.LogInfo("Click buy in " + GetMs(begin0) + " ms.");
                    lastClickOpenTime = DateTime.UtcNow;
                }
                return;
            }

            if (model.Open.OrderType == OrderType.Limit)
            {
                price -= model.Open.PendingDistance * model.Point;
            }
            else if (model.Open.OrderType == OrderType.Stop)
            {
                price += model.Open.PendingDistance * model.Point;
            }
            string signal = "BUY " + symbol + " " + ToStr2(lot) + " signal received at " + model.FormatPrice(price) + ", fastSpread=" + ToStr2(fastSpread);
            if (model.AllowEMail)
            {
                Model.EMailSender.Push("Westernpips Private 7 " + model.Title + " signal",signal,Model.Options.Smtp.Clone());
            }
            model.LogInfo(signal);
            if (model.Open.OpenTimerMs>0)
            {
                model.LogInfo("Wait " + model.Open.OpenTimerMs + " ms");
                threadStop.WaitOne(model.Open.OpenTimerMs);
            }
            if (Model.TradeProcessingEngineError)
            {
                model.LogError("Trade processing engine error");
                return;
            }

            var result = slowConnector.Open(symbol, price, lot, OrderSide.Buy, model.Magic, model.Slippage,1, model.Open.OrderType,model.Open.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                decimal slippage = -(result.OpenPrice - price) / model.Point;
                model.LogInfo("BUY OK " + symbol + " at " + model.FormatPrice(result.OpenPrice) + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) +
                    ";Execution=" + ToStrMs(result.ExecutionTime) + " ms; Execution2="+GetMs(begin0)+" ms");
            }
            else
            {
                model.LogError(slowConnector.ViewId+" "+result.Error);
                model.LogInfo("BUY FAILED " + symbol + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price));
            }
            lastOpenCloseTime = DateTime.UtcNow;
        }

        protected void OnSellSignal(decimal fastSpread, string symbol, decimal lot, decimal price, decimal gap)
        {
            DateTime begin0 = DateTime.UtcNow;
            if (Model.Options.Clicker.UseClickerForOpen)
            {
                if ((DateTime.UtcNow - lastClickOpenTime).TotalSeconds > 3)
                {
                    Model.Options.Clicker.ClickSell();
                    model.LogInfo("Click sell in " + GetMs(begin0) + " ms.");
                    lastClickOpenTime = DateTime.UtcNow;
                }
                return;
            }
            if (model.Open.OrderType == OrderType.Limit)
            {
                price += model.Open.PendingDistance * model.Point;
            }
            else if (model.Open.OrderType == OrderType.Stop)
            {
                price -= model.Open.PendingDistance * model.Point;
            }
            string signal = "SELL " + symbol + " " + ToStr2(lot) + " signal received at " + model.FormatPrice(price) + ", fastSpread=" + ToStr2(fastSpread);
            model.LogInfo(signal);
            if (model.AllowEMail)
            {
                Model.EMailSender.Push("Westernpips Private 7 " + model.Title + " signal", signal, Model.Options.Smtp.Clone());
            }
            if (model.Open.OpenTimerMs > 0)
            {
                model.LogInfo("Wait " + model.Open.OpenTimerMs + " ms");
                threadStop.WaitOne(model.Open.OpenTimerMs);
            }
            if (Model.TradeProcessingEngineError)
            {
                model.LogError("Trade processing engine error");
                return;
            }

            var result = slowConnector.Open(symbol, price, lot, OrderSide.Sell, model.Magic, model.Slippage,1, model.Open.OrderType,model.Open.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                decimal slippage = -(price - result.OpenPrice) / model.Point;
                model.LogInfo("SELL OK " + symbol + " at " + model.FormatPrice(result.OpenPrice) + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) +
                    ";Execution=" + ToStrMs(result.ExecutionTime) + " ms; Execution2=" + GetMs(begin0) + " ms");
            }
            else
            {
                model.LogError(slowConnector.ViewId+" "+result.Error);
                model.LogInfo("SELL FAILED " + symbol + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price));
            }
            lastOpenCloseTime = DateTime.UtcNow;
        }

        internal void OnCloseSignal(decimal fastSpread, OrderInformation order, decimal price, decimal gap)
        {
            DateTime begin0 = DateTime.UtcNow;
            if (Model.Options.Clicker.UseClickerForClose)
            {
                if ((DateTime.UtcNow - lastClickCloseTime).TotalSeconds > 3)
                {
                    Model.Options.Clicker.ClickClose();
                    model.LogInfo("Click close in " + GetMs(begin0) + " ms.");
                    lastClickCloseTime = DateTime.UtcNow;
                }
                return;
            }


            if (model.Open.OrderType == OrderType.Limit)
            {
                if (order.Side == OrderSide.Buy) price += model.Open.PendingDistance * model.Point;
                else price -= model.Open.PendingDistance * model.Point;
            }
            else if (model.Open.OrderType == OrderType.Stop)
            {
                if (order.Side == OrderSide.Buy) price -= model.Open.PendingDistance * model.Point;
                else price += model.Open.PendingDistance * model.Point;
            }

            model.LogInfo("CLOSE " + order.Symbol + " " + ToStr2(order.Volume) + " signal received at " + model.FormatPrice(price) + ", fastSpread=" + ToStr2(fastSpread));
            if (Model.TradeProcessingEngineError)
            {
                model.LogError("Trade processing engine error");
                return;
            }

            var result = slowConnector.Close(order.Symbol, order.Id, price, order.Volume, order.Side, model.Slippage, model.Close.OrderType,model.Close.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                if (result != null)
                {
                    decimal slippage = -(order.Side == OrderSide.Buy ? (price - result.ClosePrice) / model.Point : (result.ClosePrice - price) / model.Point);
                    model.LogInfo("CLOSE OK " + order.Symbol + " at " + model.FormatPrice(result.ClosePrice) + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) + " " + closeSignal + " " +
                        ";Execution=" + ToStrMs(result.ExecutionTime) + " ms");
                }
            }
            else
            {
                model.LogError(slowConnector.ViewId+" "+result.Error);
                model.LogError("CLOSE FAILED " + order.Symbol + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + " " + closeSignal);
            }
            lastOpenCloseTime = DateTime.UtcNow;
        }

        string ToStr1(decimal value)
        {
            return value.ToString("F1", CultureInfo.InvariantCulture);
        }
        string ToStr2(decimal value)
        {
            return value.ToString("F2", CultureInfo.InvariantCulture);
        }
        string ToStrMs(TimeSpan span)
        {
            return span.TotalMilliseconds.ToString("F3", CultureInfo.InvariantCulture);
        }
        void IConnectorLogger.LogInfo(string msg)
        {
            LogInfo(msg);
        }

        void IConnectorLogger.LogError(string msg)
        {
            LogError(msg);
        }

        void IConnectorLogger.LogWarning(string msg)
        {
            LogWarning(msg);
        }

        private void BuLoad_Click(object sender, RoutedEventArgs e)
        {
            Models.PresetModel.LoadDialog(model);
        }

        private void BuSave_Click(object sender, RoutedEventArgs e)
        {
            Models.PresetModel.SaveDialog(model);
        }
        private void LogClear_Click(object sender, RoutedEventArgs e)
        {
            LogClear();
        }

        private void TbOpenOrderType_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrderTypeEditor dlg = new OrderTypeEditor(model.Open.OrderType, model.Open.PendingDistance, model.Open.PendingLifeTimeMs, model.Open.Fill)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (dlg.ShowDialog()==true)
            {
                model.Open.OrderType = dlg.OrderType;
                model.Open.Fill = dlg.Fill.Value;
                model.Open.PendingDistance = dlg.PendingDistance;
                model.Open.PendingLifeTimeMs = dlg.PendingLifeTime;
            }
        }

        private void TbCloseOrderType_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrderTypeEditor dlg = new OrderTypeEditor(model.Close.OrderType, model.Close.PendingDistance, model.Close.PendingLifeTimeMs, null)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (dlg.ShowDialog() == true)
            {
                model.Close.OrderType = dlg.OrderType;
                model.Close.PendingDistance = dlg.PendingDistance;
                model.Close.PendingLifeTimeMs = dlg.PendingLifeTime;
            }
        }
    }
}
