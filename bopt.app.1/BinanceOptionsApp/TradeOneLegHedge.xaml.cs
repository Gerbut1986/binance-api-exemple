using MultiTerminal.Connections;
using MultiTerminal.Connections.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using VisualMarketsEngine;


namespace BinanceOptionsApp
{
    public partial class TradeOneLegHedge : UserControl, IConnectorLogger, ITradeTabInterface
    {
        Models.TradeModel model;
        ManualResetEvent threadStop;
        ManualResetEvent threadStopped;
        readonly object loglock = new object();
        public TradeOneLegHedge()
        {
            InitializeComponent();
        }
        public void InitializeTab()
        {
            model = DataContext as Models.TradeModel;
            //fast.InitializeProviderControl(model.Fast,true);
            //slow1.InitializeProviderControl(model.Slow,false);
            //slow2.InitializeProviderControl(model.Slow2,false);

            model.LogError = LogError;
            model.LogInfo = LogInfo;
            model.LogWarning = LogWarning;
            model.LogClear = LogClear;
            HiddenLogs.LogHeader(model);
            tbFreezeTime.IsEnabled = Model.UseFreezeTime;
        }
        public void RestoreNullCombo(ConnectionModel cm)
        {
            //fast.RestoreNullCombo(cm);
            //slow1.RestoreNullCombo(cm);
            //slow2.RestoreNullCombo(cm);
        }
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

            ClearChart();
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
        decimal prevSlowBid;
        decimal prevSlowAsk;
        decimal prevFastBid;
        decimal prevFastAsk;

        Helpers.MovingAverage averageBid;
        Helpers.MovingAverage averageAsk;
        Helpers.MovingAverage averageSpread;
        Helpers.MovingAverage averageBid2;
        Helpers.MovingAverage averageAsk2;
        Helpers.MovingAverage averageSpread2;
        Helpers.MovingAverage averageFastChange;
        Helpers.MovingAverage averageSlowChange;
        Helpers.MovingAverage averageSlow2TimeBetweenTicks;
        Helpers.MovingAverage averageSlowTimeBetweenTicks;
        Helpers.MovingAverage averageFastTimeBetweenTicks;
        Helpers.Ring fastBidRing;
        Helpers.Ring fastAskRing;



        decimal gapBuy;
        decimal gapSell;
        decimal gapBuy2;
        decimal gapSell2;

        IConnector fastConnector;
        IConnector slowConnector;
        IConnector slowConnector2;
        string swDebugPath;
        string swQuotesPath;
        string swLogPath;
        System.IO.FileStream fsData;
        DateTime lastOpenCloseTime;
        int volumeDirection;

        class HDeal
        {
            public string Id { get; set; }
            public string HedgeId { get; set; }
            public OrderInformation Order { get; set; }
            public OrderInformation OrderH { get; set; }
            public decimal? TrailSL { get; set; }
            public decimal Profit { get; set; }
            public bool Closing { get; set; }
            public decimal ProfitInMoney { get; set; }
        }

        readonly List<HDeal> deals = new List<HDeal>();
        DateTime lastDealTime = DateTime.MinValue;

        private string EscapePath(string path)
        {
            char[] invalid = System.IO.Path.GetInvalidPathChars();
            foreach (var c in invalid)
            {
                path = path.Replace(c, ' ');
            }
            return path;
        }

        DoubleTimeValue[] chartValues;
        ChartGroup.InternalChart chartPrice;
        ChartGroup.InternalChart chartGap;
        int chartFastBidFlow;
        int chartSlowBidFlow;
        int chartFastAskFlow;
        int chartSlowAskFlow;
        int chartGapBuyFlow;
        int chartGapSellFlow;

        public void ClearChart()
        {
            chartGroup.Clear();
            chartGroup.Settings.FirstShowMaximumBars = 150;
            chartGroup.Settings.ShowLegend = false;
            chartGroup.Settings.ShiftOnUpdate = true;
            chartGroup.Settings.ScaleDateFormat2 = "HH:mm:ss.ffffff";
            chartGroup.Settings.Foreground = Color.FromRgb(0xff, 0xff, 0xff);
            chartGroup.Settings.Background = Color.FromRgb(0x00, 0x23, 0x44);
            chartGroup.Settings.NavigatorBackground = Color.FromRgb(0x00, 0x6d, 0xc5);
            chartGroup.Settings.NavigatorThumb = Color.FromRgb(0x20, 0x8d, 0xe5);
            chartGroup.Settings.TransparentPanel = Color.FromRgb(0x00, 0x23, 0x44);
            chartGroup.Settings.ChartBackground = Color.FromRgb(0x52, 0xbb, 0xea);
            chartGroup.Settings.GridPen = Color.FromArgb(50, 0x00, 0x23, 0x44);

            chartValues = new DoubleTimeValue[1];
            if (model.AllowView)
            {
                chartPrice = chartGroup.CreateChart("Price", true, 1.0, model.Digits);
                chartFastBidFlow = chartPrice.AddDoubleTimeFlow("FastBid", TimeFrame.Tick, Color.FromRgb(0xbf, 0xd7, 0x30), 2, false);
                chartSlowBidFlow = chartPrice.AddDoubleTimeFlow("SlowBid", TimeFrame.Tick, Color.FromArgb(100, 0xbf, 0xd7, 0x30), 2, false);
                chartFastAskFlow = chartPrice.AddDoubleTimeFlow("FastAsk", TimeFrame.Tick, Color.FromRgb(0xd4, 0x66, 0x00), 2, false);
                chartSlowAskFlow = chartPrice.AddDoubleTimeFlow("SlowAsk", TimeFrame.Tick, Color.FromArgb(100, 0xd4, 0xd7, 0x30), 2, false);
                chartGap = chartGroup.CreateChart("Gap", true, 1.0, model.Digits);
                chartGapBuyFlow = chartGap.AddDoubleTimeFlow("GapBuy", TimeFrame.Tick, Color.FromRgb(0xff, 0xff, 0xff), 2, false);
                chartGapSellFlow = chartGap.AddDoubleTimeFlow("GapSell", TimeFrame.Tick, Color.FromArgb(100, 0xff, 0xff, 0xff), 2, false);
            }
        }

        private void ThreadProc()
        {
         #if USE_1LEG_HEDGE
            decimal? stopBalance = null;
            bool balanceOk = true;

            model.Fast.InitView();
            model.Slow.InitView();
            model.Slow2.InitView();
            model.GapBuy = 0;
            model.GapSell = 0;
            volumeDirection = 0;

            prevSlowAsk = 0;
            prevSlowBid = 0;
            prevFastAsk = 0;
            prevFastBid = 0;

            fastBidRing = new Helpers.Ring(model.Open.GapFastTicks + 1);
            fastAskRing = new Helpers.Ring(model.Open.GapFastTicks + 1);
            averageBid = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageAsk = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageSpread = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageBid2 = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageAsk2 = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageSpread2 = new Helpers.MovingAverage(model.Open.AvtoShiftPeriod);
            averageFastChange = new Helpers.MovingAverage(model.Open.FastCoefPeriod);
            averageSlowChange = new Helpers.MovingAverage(model.Open.FastCoefPeriod);
            averageSlow2TimeBetweenTicks = new Helpers.MovingAverage(100);
            averageSlowTimeBetweenTicks = new Helpers.MovingAverage(100);
            averageFastTimeBetweenTicks = new Helpers.MovingAverage(100);
            gapSell = 0;
            gapBuy = 0;
            gapSell2 = 0;
            gapBuy2 = 0;
            deals.Clear();
            lastDealTime = DateTime.MinValue;

            fastConnector = model.Fast.CreateConnector(this, threadStop, model.SleepMs, Dispatcher);
            slowConnector = model.Slow.CreateConnector(this, threadStop, model.SleepMs, Dispatcher);
            slowConnector2 = model.Slow2.CreateConnector(this, threadStop, model.SleepMs, Dispatcher);
            fastConnector.Tick += FastConnector_Tick;
            slowConnector.Tick += SlowConnector_Tick;
            slowConnector2.Tick += SlowConnector2_Tick;
            fastConnector.LoggedIn += FastConnector_LoggedIn;
            slowConnector.LoggedIn += SlowConnector_LoggedIn;
            slowConnector2.LoggedIn += SlowConnector2_LoggedIn;

            model.LogInfo(model.Title + " logging in...");
            while (!threadStop.WaitOne(100))
            {
                if (fastConnector.IsLoggedIn && slowConnector.IsLoggedIn && slowConnector2.IsLoggedIn)
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
                }
                if (slowConnector2.IsLoggedIn)
                {
                    OnSlow2Login();
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
                System.IO.File.AppendAllText(swDebugPath, "GapBuy;GapSell;GapBuy2;GapSell2;FastCoef;AvBid;AvAsk;Spread;SpreadK;MinLev;MinLevH;MinLevClose;FixTp;FixSl;FastSpread;dTFast;tpsFast;dTSlow;tpsSlow;\r\n");
                System.IO.File.AppendAllText(swQuotesPath, "LocalTime;FastTime;FastBid;FastAsk;SlowBid;SlowAsk;SlowBid2;SlowAsk2;GapLong;GapShort;GapLong2;GapShort2;FastCoef;dTFast;tpsFast;dTSlow;tpsSlow;\r\n");
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
                decimal slowBid2 = model.Slow2.Bid;
                decimal slowAsk2 = model.Slow2.Ask;

                if (fastBid == 0 || fastAsk == 0 || slowBid == 0 || slowAsk == 0 || slowBid2==0 || slowAsk2==0) continue;

                bool skip = true;
                if (fastBid != prevFastBid) skip = false;
                if (fastAsk != prevFastAsk) skip = false;
                if (slowBid != prevSlowBid) skip = false;
                if (slowAsk != prevSlowAsk) skip = false;

                decimal fastQuoteUpdateTimeMs = (decimal)(DateTime.Now - model.Fast.Time).TotalMilliseconds;
                decimal slowQuoteUpdateTimeMs = (decimal)(DateTime.Now - model.Slow.Time).TotalMilliseconds;
                decimal slow2QuoteUpdateTimeMs = (decimal)(DateTime.Now - model.Slow2.Time).TotalMilliseconds;
                model.FeederOk = fastConnector.IsLoggedIn && fastQuoteUpdateTimeMs < 200000 && slowConnector.IsLoggedIn && slowQuoteUpdateTimeMs < 200000 && slowConnector2.IsLoggedIn && slow2QuoteUpdateTimeMs < 200000;
                if (!model.FeederOk)
                {
                    skip = true;
                }
                if (fastBid > 0)
                {
                    decimal k = slowBid / fastBid;
                    if (k > 3 || k < 0.3M) skip = true;
                }
                if (fastBid > fastAsk || fastAsk <= 0) skip = true;

                if (!skip)
                {
                    fastBidRing.Push(fastBid);
                    fastAskRing.Push(fastAsk);

                    model.Fast.CalculateViewSpread(model.Point);
                    model.Slow.CalculateViewSpread(model.Point);
                    model.Slow2.CalculateViewSpread(model.Point);


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

                    if (model.Open.AvtoShiftBid)
                    {
                        if (model.Open.SignalMode == 0)
                        {
                            averageBid.Process((fastBid - slowBid) / model.Point);
                            gapSell = (fastBid - slowBid) / model.Point - averageBid.Output;

                            averageAsk.Process((fastBid - slowBid) / model.Point);
                            gapBuy = (fastBid - slowBid) / model.Point - averageAsk.Output;
                        }
                        else
                        {
                            averageBid.Process((fastAsk - slowBid) / model.Point);
                            gapSell = (fastAsk - slowBid) / model.Point - averageBid.Output;

                            averageAsk.Process((fastBid - slowAsk) / model.Point);
                            gapBuy = (fastBid - slowAsk) / model.Point - averageAsk.Output;
                        }
                    }
                    else
                    {
                        if (model.Open.SignalMode == 0)
                        {
                            fastBid += model.Open.ShiftBid * model.Point;
                            fastAsk += model.Open.ShiftAsk * model.Point;
                            gapBuy = (fastBid - slowBid) / model.Point;
                            gapSell = (fastBid - slowBid) / model.Point;
                        }
                        else
                        {
                            fastBid += model.Open.ShiftBid * model.Point;
                            fastAsk += model.Open.ShiftAsk * model.Point;
                            gapBuy = (fastBid - slowAsk) / model.Point;
                            gapSell = (fastAsk - slowBid) / model.Point;
                        }
                    }

                    averageBid2.Process((slowBid - slowBid2) / model.Point);
                    gapSell2 = (slowBid - slowBid2) / model.Point - averageBid2.Output;

                    averageAsk2.Process((slowAsk - slowAsk2) / model.Point);
                    gapBuy2 = (slowAsk - slowAsk2) / model.Point - averageAsk2.Output;

                    model.GapSell = gapSell;
                    model.GapBuy = gapBuy;
                    model.GapBuy2 = gapBuy2;
                    model.GapSell2 = gapSell2;

                    if (model.AllowView)
                    {
                        SafeInvoke(() =>
                        {
                            DateTime chartTime = DateTime.UtcNow;
                            chartValues[0] = new DoubleTimeValue((double)fastBid, chartTime);
                            chartPrice.AddDoubleTimeValuesTo(chartFastBidFlow, chartValues);
                            chartValues[0] = new DoubleTimeValue((double)slowBid, chartTime);
                            chartPrice.AddDoubleTimeValuesTo(chartSlowBidFlow, chartValues);
                            chartValues[0] = new DoubleTimeValue((double)fastAsk, chartTime);
                            chartPrice.AddDoubleTimeValuesTo(chartFastAskFlow, chartValues);
                            chartValues[0] = new DoubleTimeValue((double)slowAsk, chartTime);
                            chartPrice.AddDoubleTimeValuesTo(chartSlowAskFlow, chartValues);
                            chartValues[0] = new DoubleTimeValue((double)gapBuy, chartTime);
                            chartGap.AddDoubleTimeValuesTo(chartGapBuyFlow, chartValues);
                            chartValues[0] = new DoubleTimeValue((double)gapSell, chartTime);
                            chartGap.AddDoubleTimeValuesTo(chartGapSellFlow, chartValues);
                        });
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
                                                           model.FormatPrice(slowBid2) + ";" +
                                                           model.FormatPrice(slowAsk2) + ";" +
                                                           ToStr2(gapBuy) + ";" +
                                                           ToStr2(gapSell) + ";" +
                                                           ToStr2(gapBuy2) + ";" +
                                                           ToStr2(gapSell2) + ";" +
                                                           ToStr2(fastCoef) +
                                                           ToStr2(model.Fast.AverageTimeBetweenTicks) + ";" +
                                                           ToStr2(model.Fast.GetTicksPerSecond()) + ";" +
                                                           ToStr2(model.Slow.AverageTimeBetweenTicks) + ";" +
                                                           ToStr2(model.Slow.GetTicksPerSecond()) + ";\r\n");
                    }
                    decimal spread = (slowAsk - slowBid) / model.Point;
                    decimal spread2 = (slowAsk2 - slowBid2) / model.Point;
                    if (model.Open.UseAverageSpread)
                    {
                        spread = averageSpread.Process(spread);
                        spread2 = averageSpread2.Process(spread2);
                    }
                    decimal spreadK = 1.0M;
                    decimal spreadK2 = 1.0M;
                    if (model.Open.AvtoSettings)
                    {
                        spreadK = spread;
                        spreadK2 = spread2;
                        if (spreadK < 1.0M) spreadK = 1.0M;
                        if (spreadK2 < 1.0M) spreadK2 = 1.0M;
                    }


                    var _orders1 = slowConnector.GetOrders(model.Slow.FullSymbol, model.Magic, 1);
                    var _orders2 = slowConnector2.GetOrders(model.Slow2.FullSymbol, model.Magic, 1);
                    volumeDirection = 0;
                    foreach (var deal in deals)
                    {
                        deal.Profit = 0;
                        deal.Order = null;
                        deal.OrderH = null;
                        deal.ProfitInMoney = 0;
                        if (!string.IsNullOrEmpty(deal.Id))
                        {
                            deal.Order = _orders1.FirstOrDefault(x => x.Id == deal.Id);
                            if (deal.Order != null)
                            {
                                if (deal.Order.Side == OrderSide.Buy)
                                {
                                    deal.Profit = slowBid - deal.Order.OpenPrice;
                                    volumeDirection += 1;
                                }
                                else
                                {
                                    deal.Profit = deal.Order.OpenPrice - slowAsk;
                                    volumeDirection -= 1;
                                }
                                deal.ProfitInMoney += deal.Order.PnL;
                            }
                        }
                        if (!string.IsNullOrEmpty(deal.HedgeId))
                        {
                            deal.OrderH = _orders2.FirstOrDefault(x => x.Id == deal.HedgeId);
                            if (deal.OrderH != null)
                            {
                                if (deal.OrderH.Side == OrderSide.Buy)
                                {
                                    deal.Profit+= slowBid2 - deal.OrderH.OpenPrice;
                                }
                                else
                                {
                                    deal.Profit+= deal.OrderH.OpenPrice - slowAsk2;
                                }
                                deal.ProfitInMoney += deal.OrderH.PnL;
                            }
                        }
                    }
                    decimal minlev = model.Open.MinimumLevel * spreadK + spread + model.Open.Comission;
                    decimal minlevH = model.Open.MinimumHedgeLevel * spreadK2 + spread2 + model.Open.Comission;
                    decimal minlevclose = model.Close.MinimumLevelClose * spreadK;
                    decimal fixtp = model.Close.FixTP * spreadK + model.Open.Comission;
                    decimal fixsl = model.Close.FixSL * spreadK + spread;
                    decimal fastSpread = (fastAsk - fastBid) / model.Point;
                    decimal slowSpread = (slowAsk - slowBid) / model.Point;

                    decimal fixtrailstart = model.Close.FixTrailStart * spreadK + model.Open.Comission;
                    decimal fixtrailstop = model.Close.FixTrailStop * spreadK + spread;

                    if (swDebugPath != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(ToStr2(gapBuy) + ";");
                        sb.Append(ToStr2(gapSell) + ";");
                        sb.Append(ToStr2(gapBuy2) + ";");
                        sb.Append(ToStr2(gapSell2) + ";");
                        sb.Append(ToStr2(fastCoef) + ";");
                        decimal avbid_ = averageBid.Output;
                        decimal avask_ = averageAsk.Output;
                        sb.Append(model.FormatPrice(avbid_) + ";");
                        sb.Append(model.FormatPrice(avask_) + ";");
                        sb.Append(ToStr2(spread) + ";");
                        sb.Append(ToStr2(spreadK) + ";");
                        sb.Append(ToStr2(minlev) + ";");
                        sb.Append(ToStr2(minlevH) + ";");
                        sb.Append(ToStr2(minlevclose) + ";");
                        sb.Append(ToStr2(fixtp) + ";");
                        sb.Append(ToStr2(fixsl) + ";");
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
                    model.Slow2.Balance = slowConnector2.Balance;

                    if (slowConnector.Balance != null && slowConnector2.Balance != null)
                    {
                        decimal totalBalance = slowConnector.Balance.Value + slowConnector2.Balance.Value;
                        if (stopBalance == null)
                        {
                            stopBalance = totalBalance - (totalBalance * model.Open.RiskDeposit * 0.01M);
                            model.LogInfo("StopBalance = " + ToStr2(stopBalance.Value));
                        }
                        if (stopBalance != null)
                        {
                            if (totalBalance < stopBalance)
                            {
                                if (balanceOk)
                                {
                                    model.LogError("Drawdown reached. Trading stopped.");
                                }
                                balanceOk = false;
                            }
                        }
                    }

                    bool fastGapOkForBuy = true;
                    bool fastGapOkForSell = true;
                    if (model.Open.MinGapFast>0)
                    {
                        fastGapOkForBuy = fastGapAsk >= model.Open.MinGapFast;
                        fastGapOkForSell = fastGapBid <= -model.Open.MinGapFast;
                    }
                    bool fastSpreadOk = fastSpread >= model.Open.MinSpread && (fastSpread <= model.Open.MaxSpread || model.Open.MaxSpread == 0);
                    bool slowSpreadOk = slowSpread >= model.Open.MinSpreadSlow && (slowSpread <= model.Open.MaxSpreadSlow || model.Open.MaxSpreadSlow == 0);
                    if (fastSpreadOk && fastAsk >= fastBid && slowSpreadOk)
                    {
                        if ((DateTime.UtcNow - lastOpenCloseTime).TotalMilliseconds >= 1500 && model.Open.IsInStartEndSpan(DateTime.Now, startTime, endTime) &&
                            (DateTime.UtcNow - lastDealTime).TotalSeconds>=(double)model.Open.HTimeSeconds && deals.Count<model.Open.HMaxTrades)
                        {
                            decimal lot = model.Open.Lot;
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
                            bool hedgeBuy = true;
                            bool hedgeSell = true;
                            if (model.Open.MinimumHedgeLevel>0)
                            {
                                hedgeBuy = gapBuy2 >= minlevH;
                                hedgeSell = gapSell2 <= -minlevH;
                            }
                            if (model.Fast.GetTicksPerSecond() >= model.Open.MinimumTPS)
                            {
                                if (gapBuy >= minlev && tickSignalBuy && fastCoefOk && model.AllowOpen && !slowSignal && Math.Abs(gapBuy) > spread && gapBuy >= model.Open.MinGapForOpen && balanceOk)
                                {
                                    if (model.Open.MaxGapForOpen > 0)
                                    {
                                        maxGapOk = gapBuy <= model.Open.MaxGapForOpen;
                                    }
                                    if (maxGapOk && freezeOk && hedgeSell && volumeDirection<=0 && fastGapOkForBuy)
                                    {
                                        OnBuySignal(fastSpread, model.Slow.FullSymbol, lot, slowAsk, gapBuy);
                                    }
                                }
                                else
                                if (gapSell < -minlev && tickSignalSell && fastCoefOk && model.AllowOpen && !slowSignal && Math.Abs(gapSell) > spread && Math.Abs(gapSell) >= model.Open.MinGapForOpen && balanceOk)
                                {
                                    if (model.Open.MaxGapForOpen > 0)
                                    {
                                        maxGapOk = gapSell >= -model.Open.MaxGapForOpen;
                                    }
                                    if (maxGapOk && freezeOk && hedgeBuy && volumeDirection>=0 && fastGapOkForSell)
                                    {
                                        OnSellSignal(fastSpread, model.Slow.FullSymbol, lot, slowBid, gapSell);
                                    }
                                }
                            }
                        }
                        foreach (var deal in deals)
                        {
                            if (deal.Closing)
                            {
                                OnCloseSignal(deal, "");
                                break;
                            }
                            if (deal.Order != null)
                            {
                                if (deal.Order.Side == OrderSide.Buy)
                                {
                                    bool timerok = (slowConnector.CurrentTime - deal.Order.OpenTime).TotalSeconds >= model.Open.MinOrderTimeSec;

                                    if (model.Close.CloseTimerSec > 0)
                                    {
                                        if ((slowConnector.CurrentTime - deal.Order.OpenTime).TotalSeconds >= model.Close.CloseTimerSec)
                                        {
                                            OnCloseSignal(deal, "by TIMER");
                                            break;
                                        }
                                    }
                                    if (deal.OrderH != null)
                                    {
                                        if (deal.ProfitInMoney >= model.Close.HProfit && model.Close.HProfit>0)
                                        {
                                            OnCloseSignal(deal, "by HPROFIT($)");
                                            break;
                                        }
                                        if (deal.ProfitInMoney<=-model.Close.HStop && model.Close.HStop>0)
                                        {
                                            OnCloseSignal(deal, "by HSTOP($)");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (model.Close.MinimumLevelClose != 0 && timerok)
                                        {
                                            if (gapBuy <= -minlevclose) { OnCloseSignal(deal, "by GAP"); break; }
                                        }
                                        if (model.Close.CloseTimerSec == 0)
                                        {
                                            if (deal.Profit >= (fixtp * model.Point) && timerok)
                                            {
                                                OnCloseSignal(deal, "by TP");
                                                break;
                                            }
                                            else if (deal.Profit <= (-fixsl * model.Point))
                                            {
                                                OnCloseSignal(deal, "by SL");
                                                break;
                                            }
                                        }
                                        if (model.Close.Trailing && timerok && model.Close.CloseTimerSec == 0)
                                        {
                                            if (deal.TrailSL.HasValue)
                                            {
                                                if (deal.Profit <= (-deal.TrailSL * model.Point))
                                                {
                                                    OnCloseSignal(deal, "by Trail");
                                                    break;
                                                }
                                            }
                                            if (deal.Profit >= (fixtrailstart * model.Point))
                                            {
                                                decimal newtrailSl = -((deal.Profit / model.Point) - fixtrailstop);
                                                if (!deal.TrailSL.HasValue)
                                                {
                                                    deal.TrailSL = newtrailSl;
                                                }
                                                else
                                                {
                                                    if (newtrailSl < deal.TrailSL.Value) deal.TrailSL = newtrailSl;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                if (deal.Order.Side == OrderSide.Sell)
                                {
                                    bool timerok = (slowConnector.CurrentTime - deal.Order.OpenTime).TotalSeconds >= model.Open.MinOrderTimeSec;
                                    if (model.Close.CloseTimerSec > 0)
                                    {
                                        if ((slowConnector.CurrentTime - deal.Order.OpenTime).TotalSeconds >= model.Close.CloseTimerSec)
                                        {
                                            OnCloseSignal(deal, "by TIMER");
                                            break;
                                        }
                                    }
                                    if (deal.OrderH != null)
                                    {
                                        if (deal.ProfitInMoney >= model.Close.HProfit)
                                        {
                                            OnCloseSignal(deal, "by HPROFIt");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (model.Close.MinimumLevelClose != 0 && timerok)
                                        {
                                            if (gapSell >= minlevclose) { OnCloseSignal(deal, "by GAP"); break; }
                                        }
                                        if (model.Close.CloseTimerSec == 0)
                                        {
                                            if (deal.Profit >= (fixtp * model.Point) && timerok)
                                            {
                                                OnCloseSignal(deal, "by TP");
                                                break;
                                            }
                                            else if (deal.Profit <= (-fixsl * model.Point))
                                            {
                                                OnCloseSignal(deal, "by SL");
                                                break;
                                            }
                                        }
                                        if (model.Close.Trailing && timerok && model.Close.CloseTimerSec == 0)
                                        {
                                            if (deal.TrailSL.HasValue)
                                            {
                                                if (deal.Profit <= (-deal.TrailSL * model.Point))
                                                {
                                                    OnCloseSignal(deal, "by Trail");
                                                    break;
                                                }
                                            }
                                            if (deal.Profit >= (fixtrailstart * model.Point))
                                            {
                                                decimal newtrailSl = -((deal.Profit / model.Point) - fixtrailstop);
                                                if (!deal.TrailSL.HasValue)
                                                {
                                                    deal.TrailSL = newtrailSl;
                                                }
                                                else
                                                {
                                                    if (newtrailSl < deal.TrailSL.Value) deal.TrailSL = newtrailSl;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            slowConnector2.Tick -= SlowConnector2_Tick;
            slowConnector.Tick -= SlowConnector_Tick;
            fastConnector.Tick -= FastConnector_Tick;
            slowConnector2.LoggedIn -= SlowConnector2_LoggedIn;
            slowConnector.LoggedIn -= SlowConnector_LoggedIn;
            fastConnector.LoggedIn -= FastConnector_LoggedIn;
            ConnectorsFactory.Current.CloseNonStateConnectors(fastConnector);
            ConnectorsFactory.Current.CloseNonStateConnectors(slowConnector);
            ConnectorsFactory.Current.CloseNonStateConnectors(slowConnector2);
            ConnectorsFactory.Current.CloseConnector(model.Slow2.Name,true);
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
#endif
            threadStopped.Set();
        }

        void OnSlow2Login()
        {
            slowConnector2.Fill = (FillPolicy)model.Open.Fill;
            slowConnector2.Subscribe(model.Slow2.FullSymbol, model.Slow2.GetSymbolId());
        }
        void OnSlowLogin()
        {
            slowConnector.Fill = (FillPolicy)model.Open.Fill;
            slowConnector.Subscribe(model.Slow.FullSymbol, model.Slow.GetSymbolId());
        }
        void OnFastLogin()
        {
            fastConnector.Fill = (FillPolicy)model.Open.Fill;
            fastConnector.Subscribe(model.Fast.FullSymbol, model.Fast.GetSymbolId());
        }

        private void SlowConnector2_LoggedIn(object sender, EventArgs e)
        {
            OnSlow2Login();
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
        private void SlowConnector2_Tick(object sender, TickEventArgs e)
        {
            if (e.Symbol == model.Slow2.FullSymbol)
            {
                if (model.Slow2.Time.Year > 2000)
                {
                    TimeSpan delta = DateTime.Now - model.Slow2.Time;
                    model.Slow2.AverageTimeBetweenTicks = averageSlow2TimeBetweenTicks.Process((decimal)delta.TotalMilliseconds);
                    decimal tps = model.Slow2.GetTicksPerSecond();
                    if (tps > model.Slow2.MaxTPS) model.Slow2.MaxTPS = tps;
                }
                model.Slow2.Bid = e.Bid;
                model.Slow2.Ask = e.Ask;
                model.Slow2.Time = DateTime.Now;
            }
        }
        private void SlowConnector_Tick(object sender, TickEventArgs e)
        {
            if (e.Symbol == model.Slow.FullSymbol)
            {
                if (model.Slow.Time.Year > 2000)
                {
                    TimeSpan delta = DateTime.Now - model.Slow.Time;
                    model.Slow.AverageTimeBetweenTicks = averageSlowTimeBetweenTicks.Process((decimal)delta.TotalMilliseconds);
                    decimal tps = model.Slow.GetTicksPerSecond();
                    if (tps > model.Slow.MaxTPS) model.Slow.MaxTPS = tps;
                }
                model.Slow.Bid = e.Bid;
                model.Slow.Ask = e.Ask;
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
                    model.Fast.AverageTimeBetweenTicks = averageFastTimeBetweenTicks.Process((decimal)delta.TotalMilliseconds);
                    decimal tps = model.Fast.GetTicksPerSecond();
                    if (tps > model.Fast.MaxTPS) model.Fast.MaxTPS = tps;
                }
                model.Fast.Bid = e.Bid;
                model.Fast.Ask = e.Ask;
                model.Fast.Time = DateTime.Now;
            }
        }

        protected void OnBuySignal(decimal fastSpread, string symbol, decimal lot, decimal price, decimal gap)
        {
            #if USE_1LEG_HEDGE
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
                Model.EMailSender.Push("Westernpips Private 7 " + model.Title + " signal", signal, Model.Options.Smtp.Clone());
            }
            model.LogInfo(signal);
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

            var result = slowConnector.Open(symbol, price, lot, OrderSide.Buy, model.Magic, model.Slippage, 1, model.Open.OrderType, model.Open.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                HDeal deal = new HDeal()
                {
                    Id = result.Id
                };
                decimal slippage = -(result.OpenPrice - price) / model.Point;
                model.LogInfo("BUY OK " + symbol + " at " + model.FormatPrice(result.OpenPrice) + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) +
                    ";Execution=" + ToStrMs(result.ExecutionTime) + " ms");
                if (slippage<=model.Open.MinimumHedgeSlippageInPoints)
                {
                    var result2 = slowConnector2.Open(model.Slow2.FullSymbol, model.Slow2.Bid, model.Open.LotSlow, OrderSide.Sell, model.Magic, model.Slippage, 1, model.Open.OrderType, model.Open.PendingLifeTimeMs);
                    if (string.IsNullOrEmpty(result2.Error))
                    {
                        deal.HedgeId = result2.Id;
                    }
                }
                deals.Add(deal);
                lastDealTime = DateTime.UtcNow;
            }
            else
            {
                model.LogError(slowConnector.ViewId + " " + result.Error);
                model.LogInfo("BUY FAILED " + symbol + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price));
            }
            lastOpenCloseTime = DateTime.UtcNow;
#endif
        }
        protected void OnSellSignal(decimal fastSpread, string symbol, decimal lot, decimal price, decimal gap)
        {
      #if USE_1LEG_HEDGE
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

            var result = slowConnector.Open(symbol, price, lot, OrderSide.Sell, model.Magic, model.Slippage, 1, model.Open.OrderType, model.Open.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                HDeal deal = new HDeal()
                {
                    Id = result.Id
                };
                decimal slippage = -(price - result.OpenPrice) / model.Point;
                model.LogInfo("SELL OK " + symbol + " at " + model.FormatPrice(result.OpenPrice) + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) +
                    ";Execution=" + ToStrMs(result.ExecutionTime) + " ms");
                if (slippage <= model.Open.MinimumHedgeSlippageInPoints)
                {
                    var result2=slowConnector2.Open(model.Slow2.FullSymbol, model.Slow2.Ask, model.Open.LotSlow, OrderSide.Buy, model.Magic, model.Slippage, 1, model.Open.OrderType, model.Open.PendingLifeTimeMs);
                    if (string.IsNullOrEmpty(result2.Error))
                    {
                        deal.HedgeId = result2.Id;
                    }
                }
                deals.Add(deal);
                lastDealTime = DateTime.UtcNow;
            }
            else
            {
                model.LogError(slowConnector.ViewId + " " + result.Error);
                model.LogInfo("SELL FAILED " + symbol + ";Gap=" + ToStr2(gap) + ";Price=" + model.FormatPrice(price));
            }
            lastOpenCloseTime = DateTime.UtcNow;
#endif
        }
        void OnCloseSignal(IConnector connector, OrderInformation order, string extraMessage, bool hedge, HDeal deal)
        {
        #if USE_1LEG_HEDGE
            decimal price;
            if (hedge)
            {
                price = order.Side == OrderSide.Buy ? model.Slow2.Bid : model.Slow2.Ask;
            }
            else
            {
                price = order.Side == OrderSide.Buy ? model.Slow.Bid : model.Slow.Ask;
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

            model.LogInfo("CLOSE " + order.Symbol + " " + ToStr2(order.Volume) + " signal received at " + model.FormatPrice(price));
            if (Model.TradeProcessingEngineError)
            {
                model.LogError("Trade processing engine error");
                return;
            }

            var result = connector.Close(order.Symbol, order.Id, price, order.Volume, order.Side, model.Slippage, model.Close.OrderType, model.Close.PendingLifeTimeMs);
            if (string.IsNullOrEmpty(result.Error))
            {
                decimal slippage = -(order.Side == OrderSide.Buy ? (price - result.ClosePrice) / model.Point : (result.ClosePrice - price) / model.Point);
                model.LogInfo("CLOSE OK " + order.Symbol + " at " + model.FormatPrice(result.ClosePrice) + ";Price=" + model.FormatPrice(price) + ";Slippage=" + ToStr1(slippage) + " " + extraMessage + " " +
                    ";Execution=" + ToStrMs(result.ExecutionTime) + " ms");
                if (hedge)
                {
                    deal.HedgeId = null;
                    deal.OrderH = null;
                }
                else
                {
                    deal.Id = null;
                    deal.Order = null;
                }
            }
            else
            {
                model.LogError(slowConnector.ViewId + " " + result.Error);
                model.LogError("CLOSE FAILED " + order.Symbol + ";Price=" + model.FormatPrice(price) + " " + extraMessage);
            }
            lastOpenCloseTime = DateTime.UtcNow;
#endif
        }
        void OnCloseSignal(HDeal deal,string extraMessage)
        {
        #if USE_1LEG_HEDGE
            deal.Closing = true;
            if (deal.Order != null) OnCloseSignal(slowConnector, deal.Order, extraMessage,false,deal);
            if (deal.OrderH != null) OnCloseSignal(slowConnector2, deal.OrderH, extraMessage,true,deal);
            if (deal.Order == null && deal.OrderH == null) deals.Remove(deal);
#endif
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
            if (dlg.ShowDialog() == true)
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
