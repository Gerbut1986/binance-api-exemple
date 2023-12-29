
using MultiTerminal.Connections;
using MultiTerminal.Connections.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BinanceOptionsApp.Helpers;
using BinanceOptionsApp.Models;

namespace BinanceOptionsApp
{
    public partial class TradeOneLegMulti : UserControl, ITradeTabInterface, ITradeVisualizerListener, ITradeProcessorListener
    {
        bool backtestMode;
        TradeModel model;
        AlgoOneLegMultiModel algo;
        AlgoControlModel control;
        readonly ManualResetEvent threadStop = new ManualResetEvent(false);
        readonly ManualResetEvent threadStopped = new ManualResetEvent(false);
        AlgoOneLegMultiDiagnosticsSetters diagnosticsSetters;
        AlgoOneLegMultiChartSetters liveChartSetters;
        AlgoOneLegMultiChartSetters backtestChartSetters;
        public TradeOneLegMulti()
        {
            InitializeComponent();
        }
        public void InitializeTab()
        {
            model = DataContext as TradeModel;
            algo = model.AlgoOneLegMulti;
            control = model.AlgoControl;
        }
        public void InitializeBacktest(TradeModel model)
        {
            DataContext = model;
            backtestMode = true;
            InitializeTab();
        }
        public void RestoreNullCombo(ConnectionModel cm)
        {
        }
        private void BuStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void BuStop_Click(object sender, RoutedEventArgs e)
        {
            Stop(false);
        }
        public void Start()
        {
            if (model.Started) return;
            if (model.SlowProviders.Count < 1) return;
            if (model.FastProviders.Count < 1) return;
            model.Started = true;
            model.FeederOk = true;
            algo.Started = true;
            visual.Start(this, control, algo.Digits, backtestMode);
            diagnosticsSetters = AlgoOneLegMultiDiagnostics.Configure(algo.Digits, model.FastProviders.Count, visual);
            liveChartSetters = null;
            backtestChartSetters = null;
            if (control.ViewChart)
            {
                liveChartSetters = AlgoOneLegMultiDiagnostics.ConfigureCharts(algo.Digits, model.FastProviders, model.SlowProviders, visual, false);
                if (backtestMode)
                {
                    backtestChartSetters = AlgoOneLegMultiDiagnostics.ConfigureCharts(algo.Digits, model.FastProviders, model.SlowProviders, visual, true);
                }
            }
            if (control.ViewDebug)
            {
                AlgoOneLegMultiDiagnostics.ConfigureDebug(diagnosticsSetters, visual);
            }
            eventsQueue.Clear();
            threadStop.Reset();
            threadStopped.Reset();
            Model.OnUpdateDashboardStatus();
            new Thread(ThreadProc).Start();
        }
        public void ShowDiagnostics(ITradeVisualizerDiagnosticsData data)
        {
            diagnosticsSetters.SetValues(data);
        }
        void ITradeVisualizerListener.ShowChart(ITradeVisualizerDiagnosticsData[] data, TradeOrderInformation[] liveOrders, TradeSignal[] tradeSignals)
        {
            if (liveChartSetters != null) liveChartSetters.Add(data, liveOrders, tradeSignals);
            if (backtestChartSetters != null) backtestChartSetters.Add(data, null, null);
        }
        public ITradeVisualizerDebugData[] GetDebug(List<ITradeVisualizerDiagnosticsData> data)
        {
            if (data!=null && data.Count>0)
            {
                ITradeVisualizerDebugData[] res = new ITradeVisualizerDebugData[data.Count];
                for (int i=0;i<data.Count;i++)
                {
                    var setters = diagnosticsSetters.Clone();
                    setters.SetValues(data[i]);
                    res[i] = setters;
                }
                return res;
            }
            return Array.Empty<ITradeVisualizerDebugData>();
        }
        public void Stop(bool wait)
        {
            if (!model.Started) return;
            threadStop.Set();
            if (wait)
            {
                threadStopped.WaitOne();
            }
            visual.Stop(wait);
            model.Started = false;
            algo.Started = false;
            model.FeederOk = false;
            Model.OnUpdateDashboardStatus();
        }
        public void SaveTraceInfo(string pathname)
        {
            model.SaveForBacktest(pathname);
        }
        void Save(FileStream fs,byte[] data)
        {
            fs.Write(data, 0, data.Length);
        }
        public void SaveDiagnosticsToTrace(ITradeVisualizerDiagnosticsData data, FileStream fs)
        {
            if (data is AlgoOneLegMultiDiagnostics d)
            {
                Save(fs, BitConverter.GetBytes((int)10)); // 10=diagnostics
                Save(fs, BitConverter.GetBytes((int)d.Type));
                Save(fs, BitConverter.GetBytes(d.TickNo));
                Save(fs, BitConverter.GetBytes(d.CurrentTime.Ticks));
                Save(fs, BitConverter.GetBytes(d.SlowBid));
                Save(fs, BitConverter.GetBytes(d.SlowAsk));
                Save(fs, BitConverter.GetBytes(d.SlowTime.Ticks));
                Save(fs, BitConverter.GetBytes(d.SlowSpreadPt));
                Save(fs, BitConverter.GetBytes(d.OrderDurationMs));
                Save(fs, BitConverter.GetBytes(d.OrderProfitPt));
                Save(fs, BitConverter.GetBytes(d.OrderVolume));
                Save(fs, BitConverter.GetBytes(d.SessionProfitPt));
                for (int i = 0; i < d.FastAsk.Length; i++)
                {
                    Save(fs, BitConverter.GetBytes(d.FastBid[i]));
                    Save(fs, BitConverter.GetBytes(d.FastAsk[i]));
                    Save(fs, BitConverter.GetBytes(d.FastTime[i].Ticks));
                    Save(fs, BitConverter.GetBytes(d.FastSpreadPt[i]));
                    Save(fs, BitConverter.GetBytes(d.ShiftedFastBid[i]));
                    Save(fs, BitConverter.GetBytes(d.ShiftedFastAsk[i]));
                    Save(fs, BitConverter.GetBytes(d.GapBuyPt[i]));
                    Save(fs, BitConverter.GetBytes(d.GapSellPt[i]));
                }
            }
        }
        void ITradeVisualizerListener.SaveOrderToTrace(TradeOrderInformation order, FileStream fs)
        {
            Save(fs, BitConverter.GetBytes((int)11)); // 11=order
            byte[] idbytes = Encoding.UTF8.GetBytes(order.Id);
            Save(fs, BitConverter.GetBytes(idbytes.Length));
            Save(fs, idbytes);
            Save(fs, BitConverter.GetBytes(order.OpenPrice));
            Save(fs, BitConverter.GetBytes(order.OpenTime.Ticks));
            Save(fs, BitConverter.GetBytes(order.OpenSlippagePt));
            Save(fs, BitConverter.GetBytes(order.OpenExecutionTimeMs));
            Save(fs, BitConverter.GetBytes(order.ClosePrice));
            Save(fs, BitConverter.GetBytes(order.CloseTime.Ticks));
            Save(fs, BitConverter.GetBytes(order.CloseSlippagePt));
            Save(fs, BitConverter.GetBytes(order.CloseExecutionTimeMs));
            Save(fs, BitConverter.GetBytes(order.ProfitPt));
            Save(fs, BitConverter.GetBytes((int)order.Side));
            Save(fs, BitConverter.GetBytes(order.Volume));
        }
        void ITradeVisualizerListener.SaveSignalToTrace(TradeSignal signal, FileStream fs)
        {
            Save(fs, BitConverter.GetBytes((int)12)); // 12=signal
            Save(fs, BitConverter.GetBytes((int)signal.Side));
            Save(fs, BitConverter.GetBytes(signal.Price));
            Save(fs, BitConverter.GetBytes(signal.Time.Ticks));
        }

        void ITradeProcessorListener.CommandCompleted(TradeProcessor trade, TradeCommand command)
        {
            eventsQueue.Enqueue(new CommandCompletedEvent());

            if (command is TradeCommandOpen to)
            {
                if (to.ResultOrder != null)
                {
                    visual.LogInfo($"OPEN OK at {trade.FormatPrice(to.ResultOrder.OpenPrice)}. Slippage={to.ResultOrder.OpenSlippagePt} pt. Execution={to.ResultOrder.OpenExecutionTimeMs} ms.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(to.ResultError))
                    {
                        visual.LogError($"OPEN FAILED. Error={to.ResultError}.");
                    }
                }
            } 
            else if (command is TradeCommandClose tc)
            {
                if (tc.ResultOrder != null)
                {
                    visual.HistoryOrder(tc.ResultOrder, backtestMode);
                    visual.LogInfo($"CLOSE OK at {trade.FormatPrice(tc.ResultOrder.ClosePrice)}. Slippage={tc.ResultOrder.CloseSlippagePt} pt. Execution={tc.ResultOrder.CloseExecutionTimeMs} ms.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(tc.ResultError))
                    {
                        visual.LogError($"CLOSE FAILED. Error={tc.ResultError}.");
                    }
                }
            }
        }

        readonly List<IConnector> fastConnectors = new List<IConnector>();
        readonly List<string> fastSymbols = new List<string>();
        IConnector slowConnector;
        class Event
        {
            public DateTime Time;
            public Event(DateTime time)
            {
                Time = time;
            }
        }
        class TickEvent : Event
        {
            public int No;
            public double Bid;
            public double Ask;
            public TickEvent(int no, double bid, double ask) : base(DateTime.UtcNow)
            {
                No = no;
                Bid = bid;
                Ask = ask;
            }
        }
        class CommandCompletedEvent : Event
        {
            public CommandCompletedEvent() : base(DateTime.UtcNow)
            {
            }
        }
        class HeartBeatEvent : Event
        {
            public HeartBeatEvent() : base(DateTime.UtcNow)
            {
            }
        }
        ThreadSafeQueue<Event> eventsQueue = new ThreadSafeQueue<Event>();

        private void ThreadProc()
        {
            fastConnectors.Clear();
            fastSymbols.Clear();
            foreach (var provider in model.FastProviders)
            {
                provider.Parent = model;
                var connector = provider.CreateConnector(visual, threadStop, 1, Dispatcher, false);
                connector.Tick += FastConnector_Tick;
                connector.LoggedIn += FastConnector_LoggedIn;
                fastConnectors.Add(connector);
                fastSymbols.Add(provider.FullSymbol);
            }
            slowConnector = model.SlowProviders[0].CreateConnector(visual, threadStop, 1, Dispatcher, false);
            slowConnector.Tick += SlowConnector_Tick;
            slowConnector.LoggedIn += SlowConnector_LoggedIn;

            visual.LogInfo(model.Title + " logging in...");
            while (!threadStop.WaitOne(100))
            {
                bool loggedIn = true;
                foreach (var connector in fastConnectors) if (!connector.IsLoggedIn) loggedIn = false;
                if (!slowConnector.IsLoggedIn) loggedIn = false;
                if (loggedIn)
                {
                    visual.LogInfo(model.Title + " logged in.");
                    break;
                }
            }
            if (!threadStop.WaitOne(0))
            {
                OnFastConnectorsLogin();
                OnSlowConnectorLogin();
            }

            string symbol = model.SlowProviders[0].FullSymbol;
            double point = Math.Pow(10, -algo.Digits);
            double pointInv = 1.0 / point;
            int fastCount = model.FastProviders.Count;
            int orderFastNo = 0;
            DoubleMovingAverage[] shiftMa = new DoubleMovingAverage[fastCount];
            for (int i=0;i<fastCount;i++)
            {
                shiftMa[i] = new DoubleMovingAverage(algo.AutoShiftPeriod);
            }

            bool quotesOk = false;
            TradeProcessor trade = new TradeProcessor(this,slowConnector, model.SlowProviders[0].FullSymbol, algo.Digits, point, 0, false);
            AlgoOneLegMultiDiagnostics data = new AlgoOneLegMultiDiagnostics(fastCount);
            double[] fastDirectionBuy = new double[fastCount];
            double[] fastDirectionSell = new double[fastCount];
            DateTime lastEventTime = DateTime.UtcNow;
            while (!threadStop.WaitOne(0))
            {
                Event[] events = eventsQueue.Dequeue(250);
                if (events.Length==0)
                {
                    if ((DateTime.UtcNow - lastEventTime).TotalSeconds>=1)
                    {
                        events = new Event[] { new HeartBeatEvent() };
                        lastEventTime = DateTime.UtcNow;
                    }
                }
                else
                {
                    lastEventTime = DateTime.UtcNow;
                }

                foreach (var @event in events)
                {
                    TickEvent newTick = null;
                    if (@event.Time > data.CurrentTime) data.CurrentTime = @event.Time;
                    if (@event is TickEvent tick)
                    {
                        data.Type = DiagnosticsType.Tick;
                        data.TickNo = tick.No;
                        if (tick.No == 0)
                        {
                            if (tick.Bid != data.SlowBid || tick.Ask != data.SlowAsk)
                            {
                                data.SlowBid = tick.Bid;
                                data.SlowAsk = tick.Ask;
                                data.SlowTime = tick.Time;
                                newTick = tick;
                            }
                        }
                        else
                        {
                            if (tick.Bid != data.FastBid[tick.No - 1] || tick.Ask != data.FastAsk[tick.No - 1])
                            {
                                data.FastBid[tick.No - 1] = tick.Bid;
                                data.FastAsk[tick.No - 1] = tick.Ask;
                                data.FastTime[tick.No - 1] = tick.Time;
                                newTick = tick;
                            }
                        }
                        if (!quotesOk)
                        {
                            quotesOk = data.SlowBid > 0 && data.SlowAsk > 0;
                            for (int i = 0; i < fastCount; i++)
                            {
                                if (data.FastBid[i] == 0) quotesOk = false;
                                if (data.FastAsk[i] == 0) quotesOk = false;
                            }
                        }
                    }
                    else if (@event is CommandCompletedEvent commandCompleted)
                    {
                        data.Type = DiagnosticsType.CommandCompleted;
                        data.TickNo = 0;
                    }
                    else if (@event is HeartBeatEvent heartBeat)
                    {
                        data.Type = DiagnosticsType.Heartbeat;
                        data.TickNo = 0;
                    }
                    if (!quotesOk) continue;

                    if (newTick != null)
                    {
                        double slowMid = (data.SlowBid + data.SlowAsk) * 0.5;
                        for (int i = 0; i < fastCount; i++)
                        {
                            if (newTick.No == 0 || newTick.No == (i + 1))
                            {
                                double newBid, newAsk;
                                if (algo.UseAutoShift)
                                {
                                    double fastMid = (data.FastBid[i] + data.FastAsk[i]) * 0.5;
                                    double avShift = Math.Round(shiftMa[i].Process(slowMid - fastMid), algo.Digits);
                                    newBid = data.FastBid[i] + avShift;
                                    newAsk = data.FastAsk[i] + avShift;
                                }
                                else
                                {
                                    newBid = data.FastBid[i] + algo.ManualShiftPt * point;
                                    newAsk = data.FastAsk[i] + algo.ManualShiftPt * point;
                                }
                                fastDirectionBuy[i] = data.ShiftedFastBid[i]>0 ? newBid - data.ShiftedFastBid[i] : 0;
                                fastDirectionSell[i] = data.ShiftedFastAsk[i]>0 ? newAsk - data.ShiftedFastAsk[i] : 0;
                                data.ShiftedFastBid[i] = newBid;
                                data.ShiftedFastAsk[i] = newAsk;
                            }
                        }
                        data.SlowSpreadPt = (int)Math.Round((data.SlowAsk - data.SlowBid) * pointInv, 0);
                        for (int i = 0; i < fastCount; i++)
                        {
                            data.FastSpreadPt[i] = (int)Math.Round((data.ShiftedFastAsk[i] - data.ShiftedFastBid[i]) * pointInv, 0);
                            data.GapBuyPt[i] = (int)Math.Round((data.ShiftedFastBid[i] - data.SlowAsk) * pointInv, 0);
                            data.GapSellPt[i] = (int)Math.Round((data.ShiftedFastAsk[i] - data.SlowBid) * pointInv, 0);
                            if (algo.DecreaseGapsByFastSpread)
                            {
                                data.GapBuyPt[i] -= data.FastSpreadPt[i];
                                data.GapSellPt[i] += data.FastSpreadPt[i];
                            }
                            if (algo.DecreaseGapsBySlowSpread)
                            {
                                data.GapBuyPt[i] -= data.SlowSpreadPt;
                                data.GapSellPt[i] += data.SlowSpreadPt;
                            }
                        }
                    }
                    if (!trade.IsBusy())
                    {
                        data.SessionProfitPt = trade.SessionProfitPt;
                        if (trade.Order == null)
                        {
                            data.OrderVolume = 0;
                            data.OrderDurationMs = 0;
                            data.OrderProfitPt = 0;

                            if (algo.AllowTradeOpen && newTick!=null && newTick.No>0)
                            {
                                orderFastNo = newTick.No - 1;
                                if (data.FastSpreadPt[orderFastNo] >= algo.MinSpreadFastPt &&
                                    (!algo.UseMaxSpreadFast || data.FastSpreadPt[orderFastNo] <= algo.MaxSpreadFastPt) &&
                                    (!algo.UseMaxSpreadSlow || data.SlowSpreadPt <= algo.MaxSpreadSlowPt))
                                {
                                    if (data.GapBuyPt[orderFastNo] >= algo.MinOpenGapPt && (!algo.FastTickDirectionConfirmation || fastDirectionBuy[orderFastNo] > 0))
                                    {
                                        trade.OpenOrder(OrderSide.Buy, data.SlowAsk, algo.Volume, algo.SlippagePt, data.CurrentTime,algo.OrderType,algo.PendingDistancePt,algo.PendingLifeTimeMs);
                                        visual.LogInfo($"BUY {symbol} {algo.Volume:F2} at {trade.FormatPrice(data.SlowAsk)} Gap={data.GapBuyPt[orderFastNo]} pt.");
                                        visual.TradeSignal(new TradeSignal() { Price = data.SlowAsk, Time = data.CurrentTime, Side = OrderSide.Buy });
                                    }
                                    else if (data.GapSellPt[orderFastNo] <= -algo.MinOpenGapPt && (!algo.FastTickDirectionConfirmation || fastDirectionSell[orderFastNo] < 0))
                                    {
                                        trade.OpenOrder(OrderSide.Sell, data.SlowBid, algo.Volume, algo.SlippagePt, data.CurrentTime,algo.OrderType,algo.PendingDistancePt,algo.PendingLifeTimeMs);
                                        visual.LogInfo($"SELL {symbol} {algo.Volume:F2} at {trade.FormatPrice(data.SlowBid)} Gap={data.GapSellPt[orderFastNo]} pt.");
                                        visual.TradeSignal(new TradeSignal() { Price = data.SlowBid, Time = data.CurrentTime, Side = OrderSide.Sell });
                                    }
                                }
                            }
                        }
                        else
                        {
                            data.OrderVolume = trade.Order.Side == OrderSide.Buy ? trade.Order.Volume : -trade.Order.Volume;
                            data.OrderDurationMs = trade.Order.DurationMs(DateTime.UtcNow);
                            data.OrderProfitPt = trade.Order.CalculateProfitPt(data.SlowBid, data.SlowAsk, pointInv);
                            if (data.OrderDurationMs >= algo.MinOrderDurationMs && newTick!=null)
                            {
                                bool close = false;
                                string closeReason = null;
                                OrderSide closeSide = trade.Order.Side;
                                double closePrice = 0;
                                if (trade.Order.Side == OrderSide.Buy)
                                {
                                    bool hold = algo.UseHoldIfGapAbove && data.GapBuyPt[orderFastNo] >= algo.HoldIfGapAbovePt;
                                    if (!hold)
                                    {
                                        if (data.OrderProfitPt >= algo.TakeProfitPt)
                                        {
                                            close = true;
                                            closeReason = "TP";
                                            closePrice = data.SlowBid;
                                        }
                                        else
                                        {
                                            if (data.OrderProfitPt <= -algo.StopLossPt)
                                            {
                                                close = true;
                                                closeReason = "SL";
                                                closePrice = data.SlowBid;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    bool hold = algo.UseHoldIfGapAbove && data.GapSellPt[orderFastNo] <= -algo.HoldIfGapAbovePt;
                                    if (!hold)
                                    {
                                        if (data.OrderProfitPt >= algo.TakeProfitPt)
                                        {
                                            close = true;
                                            closeReason = "TP";
                                            closePrice = data.SlowAsk;
                                        }
                                        else
                                        {
                                            if (data.OrderProfitPt <= -algo.StopLossPt)
                                            {
                                                close = true;
                                                closeReason = "SL";
                                                closePrice = data.SlowAsk;
                                            }
                                        }
                                    }
                                }
                                if (close)
                                {
                                    trade.CloseOrder(trade.Order.Id, trade.Order.Side, closePrice, trade.Order.Volume, algo.SlippagePt, data.CurrentTime, algo.OrderType,algo.PendingDistancePt,algo.PendingLifeTimeMs);
                                    visual.LogInfo($"CLOSE {(closeSide == OrderSide.Buy ? "BUY" : "SELL")} {symbol} at {trade.FormatPrice(closePrice)} by {closeReason}");
                                    visual.TradeSignal(new TradeSignal() { Price = closePrice, Time = data.CurrentTime, Side = closeSide == OrderSide.Buy ? OrderSide.Sell : OrderSide.Buy });
                                }
                            }
                        }
                    }
                    visual.Diagnostics(data.Clone());
                }
            }
            trade.Stop(true);
            slowConnector.Tick -= SlowConnector_Tick;
            slowConnector.LoggedIn -= SlowConnector_LoggedIn;
            ConnectorsFactory.Current.CloseConnector(model.SlowProviders[0].Name,true);
            for (int i = 0; i < fastConnectors.Count; i++)
            {
                var connector = fastConnectors[i];
                connector.Tick -= FastConnector_Tick;
                connector.LoggedIn -= FastConnector_LoggedIn;
                ConnectorsFactory.Current.CloseConnector(model.FastProviders[i].Name, true);
            }
            threadStopped.Set();
        }
        void OnFastConnectorsLogin()
        {
            for (int i=0;i<fastConnectors.Count;i++)
            {
                fastConnectors[i].Subscribe(fastSymbols[i], model.FastProviders[i].GetSymbolId());
            }
        }
        private void FastConnector_LoggedIn(object sender, EventArgs e)
        {
            OnFastConnectorsLogin();
        }
        void OnSlowConnectorLogin()
        {
            //slowConnector.Fill = (MultiTerminal.Connections.FillPolicy)algo.Fill;
            slowConnector.Subscribe(model.SlowProviders[0].FullSymbol, model.SlowProviders[0].GetSymbolId());
        }
        private void SlowConnector_LoggedIn(object sender, EventArgs e)
        {
            OnSlowConnectorLogin();
        }

        private void FastConnector_Tick(object sender, TickEventArgs e)
        {
            if (e.Bid > 0 && e.Ask > 0)
            {
                if (sender is IConnector connector)
                {
                    int index = 0;
                    if (fastConnectors.Count > 1)
                    {
                        index = -1;
                        for (int i = 0; i < fastConnectors.Count; i++)
                        {
                            if (fastSymbols[i] == e.Symbol)
                            {
                                index = i;
                                break;
                            }
                        }
                    }
                    if (index >= 0)
                    {
                        if (e.Symbol == fastSymbols[index])
                        {
                            double bid = Math.Round((double)e.Bid, algo.Digits);
                            double ask = Math.Round((double)e.Ask, algo.Digits);
                            eventsQueue.Enqueue(new TickEvent(index + 1, bid, ask));
                        }
                    }
                }
            }
        }
        private void SlowConnector_Tick(object sender, TickEventArgs e)
        {
            if (e.Bid > 0 && e.Ask > 0)
            {
                if (e.Symbol == model.SlowProviders[0].FullSymbol)
                {
                    double bid = Math.Round((double)e.Bid, algo.Digits);
                    double ask = Math.Round((double)e.Ask, algo.Digits);
                    eventsQueue.Enqueue(new TickEvent(0,bid,ask));
                }
            }
        }
        private void BuLoad_Click(object sender, RoutedEventArgs e)
        {
            PresetModel.LoadDialog(model);
        }
        private void BuSave_Click(object sender, RoutedEventArgs e)
        {
            PresetModel.SaveDialog(model);
        }
        void AddProvider(ObservableCollection<ProviderModel> destination, string destinationName, int maxCount, bool showInternalProviders)
        {
            if (destination.Count >= maxCount)
            {
                var mb = new MessageWindow($"Maximum count of {destinationName} providers is {maxCount}", MessageWindowType.Information)
                {
                    Owner = Application.Current.MainWindow
                };
                mb.ShowDialog();
                return;
            }
            var dlg = new ProviderEditDialog(new ProviderModel(), showInternalProviders)
            {
                Owner = Application.Current.MainWindow
            };
            if (dlg.ShowDialog() == true)
            {
                dlg.Model.Parent = model;
                destination.Add(dlg.Model);
            }
        }
        private void BuAddFastProvider_Click(object sender, RoutedEventArgs e)
        {
            AddProvider(model.FastProviders, "fast", 5,true);
        }
        private void BuAddSlowProvider_Click(object sender, RoutedEventArgs e)
        {
            AddProvider(model.SlowProviders, "slow", 1,false);
        }
        private void BuChart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
