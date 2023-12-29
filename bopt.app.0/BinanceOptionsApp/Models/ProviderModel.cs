using System;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Serialization;
using MultiTerminal.Connections;
using MultiTerminal.Connections.Models;

namespace BinanceOptionsApp.Models
{
    public enum ProviderMaxMinState
    {
        None,
        Max,
        Min
    }

    public class ProviderModel : BaseModel
    {
        public ProviderModel()
        {
            Name = "Unknown";
            Symbol = "BTCUSDT";
            Prefix = "";
            Postfix = "";
            MultiLegOpenVolume = 0.01M;
            MinLot = 0.01M;
            LotStep = 0.01M;
            BidColor = Colors.Red;
            BidWidth = 1;
            AskColor = Colors.Blue;
            AskWidth = 1;
        }
        public void EditFrom(ProviderModel other)
        {
            Name = other.Name;
            Symbol = other.Symbol;
            Prefix = other.Prefix;
            Postfix = other.Postfix;
            MinLot = other.MinLot;
            LotStep = other.LotStep;
            BidColor = other.BidColor;
            BidWidth = other.BidWidth;
            AskColor = other.AskColor;
            AskWidth = other.AskWidth;
        }
        public ProviderModel EditClone()
        {
            ProviderModel res = new ProviderModel();
            res.EditFrom(this);
            return res;
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; OnPropertyChanged(); if (Parent != null) Parent.CreateTitle(); } }
        }
        public string FullSymbol
        {
            get
            {
                return Prefix + Symbol + Postfix;
            }
        }
        private string _Symbol;
        public string Symbol
        {
            get { return _Symbol; }
            set { if (_Symbol != value) { _Symbol = value; OnPropertyChanged(); OnPropertyChanged("FullSymbol"); if (Parent != null) Parent.CreateTitle(); } }
        }
        private string _Prefix;
        public string Prefix
        {
            get { return _Prefix; }
            set { if (_Prefix != value) { _Prefix = value; OnPropertyChanged(); OnPropertyChanged("FullSymbol"); } }
        }
        private string _Postfix;
        public string Postfix
        {
            get { return _Postfix; }
            set { if (_Postfix != value) { _Postfix = value; OnPropertyChanged(); OnPropertyChanged("FullSymbol"); } }
        }
        private decimal _MinLot;
        public decimal MinLot
        {
            get { return _MinLot; }
            set { if (_MinLot != value) { _MinLot = value; OnPropertyChanged(); } }
        }
        private decimal _LotStep;
        public decimal LotStep
        {
            get { return _LotStep; }
            set { if (_LotStep != value) { _LotStep = value; OnPropertyChanged(); } }
        }

        private Color _BidColor;
        public Color BidColor
        {
            get { return _BidColor; }
            set { if (_BidColor != value) { _BidColor = value; OnPropertyChanged(); } }
        }
        private int _BidWidth;
        public int BidWidth
        {
            get { return _BidWidth; }
            set { if (_BidWidth != value) { _BidWidth = value; OnPropertyChanged(); } }
        }

        private Color _AskColor;
        public Color AskColor
        {
            get { return _AskColor; }
            set { if (_AskColor != value) { _AskColor = value; OnPropertyChanged(); } }
        }
        private int _AskWidth;
        public int AskWidth
        {
            get { return _AskWidth; }
            set { if (_AskWidth != value) { _AskWidth = value; OnPropertyChanged(); } }
        }

        private decimal _AverageTimeBetweenTicks;
        [XmlIgnore]
        public decimal AverageTimeBetweenTicks
        {
            get { return _AverageTimeBetweenTicks; }
            set { if (_AverageTimeBetweenTicks != value) { _AverageTimeBetweenTicks = value; OnPropertyChanged(); } }
        }

        private decimal _MaxTPS;
        [XmlIgnore]
        public decimal MaxTPS
        {
            get { return _MaxTPS; }
            set { if (_MaxTPS != value) { _MaxTPS = value; OnPropertyChanged(); } }
        }

        public decimal GetTicksPerSecond()
        {
            return AverageTimeBetweenTicks > 0 ? 1000.0M / AverageTimeBetweenTicks : 0;
        }

        private DateTime _Time;
        [XmlIgnore]
        public DateTime Time
        {
            get { return _Time; }
            set { if (_Time != value) { _Time = value; OnPropertyChanged(); } }
        }

        private decimal _BidCalls;
        [XmlIgnore]
        public decimal BidCalls
        {
            get { return _BidCalls; }
            set { if (_BidCalls != value) { _BidCalls = value; OnPropertyChanged(); } }
        }

        private decimal _Bid;
        [XmlIgnore]
        public decimal Bid
        {
            get { return _Bid; }
            set { if (_Bid != value) { _Bid = value; OnPropertyChanged(); } }
        }

        private decimal _Ask_Calls;
        [XmlIgnore]
        public decimal Ask_Calls
        {
            get { return _Ask_Calls; }
            set { if (_Ask_Calls != value) { _Ask_Calls = value; OnPropertyChanged(); } }
        }

        private decimal _Ask;
        [XmlIgnore]
        public decimal Ask
        {
            get { return _Ask; }
            set { if (_Ask != value) { _Ask = value; OnPropertyChanged(); } }
        }

        public void CalculateViewSpread(decimal point)
        {
            int spread = (int)((Ask - Bid) / point);
            decimal ma= spreadMA.Process(spread);
            ViewSpread = spread.ToString()+" | "+ma.ToString("F2",CultureInfo.InvariantCulture);
        }

        private string _ViewSpread;
        [XmlIgnore]
        public string ViewSpread
        {
            get { return _ViewSpread; }
            set { if (_ViewSpread != value) { _ViewSpread = value; OnPropertyChanged(); } }
        }

        private Helpers.MovingAverage spreadMA = new Helpers.MovingAverage(100);
        public void InitView()
        {
            spreadMA = new Helpers.MovingAverage(100);
            Bid = 0;
            Ask = 0;
            ViewSpread = "";
            Time = DateTime.MinValue;
            Volume = 0;
            Balance = null;
            AverageTimeBetweenTicks = 0;
        }

        private decimal _Volume;
        [XmlIgnore]
        public decimal Volume
        {
            get { return _Volume; }
            set { if (_Volume != value) { _Volume = value; OnPropertyChanged(); } }
        }
        private decimal? _Balance;
        [XmlIgnore]
        public decimal? Balance
        {
            get { return _Balance; }
            set { if (_Balance != value) { _Balance = value; OnPropertyChanged(); } }
        }


        private ProviderMaxMinState _MaxMinState;
        [XmlIgnore]
        public ProviderMaxMinState MaxMinState
        {
            get { return _MaxMinState; }
            set { if (_MaxMinState != value) { _MaxMinState = value; OnPropertyChanged(); } }
        }

        private int _ViewNumber;
        public int ViewNumber
        {
            get { return _ViewNumber; }
            set { if (_ViewNumber != value) { _ViewNumber = value; OnPropertyChanged(); } }
        }

        private decimal _MultiLegOpenVolume;
        public decimal MultiLegOpenVolume
        {
            get { return _MultiLegOpenVolume; }
            set { if (_MultiLegOpenVolume != value) { _MultiLegOpenVolume = value; OnPropertyChanged(); } }
        }

        private decimal _MultiLegGap;
        [XmlIgnore]
        public decimal MultiLegGap
        {
            get { return _MultiLegGap; }
            set { if (_MultiLegGap != value) { _MultiLegGap = value; OnPropertyChanged(); } }
        }

        private decimal _MultiLegProfit;
        [XmlIgnore]
        public decimal MultiLegProfit
        {
            get { return _MultiLegProfit; }
            set { if (_MultiLegProfit != value) { _MultiLegProfit = value; OnPropertyChanged(); } }
        }

        private decimal _MultiLegOperationGap;
        [XmlIgnore]
        public decimal MultiLegOperationGap
        {
            get { return _MultiLegOperationGap; }
            set { if (_MultiLegOperationGap != value) { _MultiLegOperationGap = value; OnPropertyChanged(); } }
        }

        private TradeModel _Parent;
        [XmlIgnore]
        public TradeModel Parent
        {
            get { return _Parent; }
            set { if (_Parent != value) { _Parent = value; OnPropertyChanged(); } }
        }

        internal IConnector CreateConnector(IConnectorLogger logger, ManualResetEvent cancelToken, int sleepMs, Dispatcher dispatcher, bool hiddenLogs=true)
        {
            ConnectionModel connection = Model.AllConnections.FirstOrDefault(x => x.Name == Name);
            if (connection != null)
            {
                if (Model.IsBrokerPresent(connection.GetBrokerCode()))
                {
                    if (connection is BinanceConnectionModel)
                    {
                        return ConnectorsFactory.Current.CreateBinance(logger, cancelToken, connection as BinanceConnectionModel);
                    }
                    if (connection is TestnetConnectionModel)
                    {
                        //return ConnectorsFactory.Current.CreateBinancefutureTestnet(logger, cancelToken, connection as TestnetConnectionModel);
                    }
                    if (connection is BinanceOptionConnectionModel)
                    {
                        return ConnectorsFactory.Current.CreateBinanceOption(logger, cancelToken, connection as BinanceOptionConnectionModel);
                    }
                }
            }
            return new ProxyConnector();
        }

        internal string GetSymbolId()
        {
            ConnectionModel connection = Model.AllConnections.FirstOrDefault(x => x.Name == Name);
            string gbc = connection.GetBrokerCode();
            System.Collections.Generic.List<AllowedInstrument> allowedInstruments = Model.GetAllowedInstruments(gbc);
            if (allowedInstruments.Count>0)
            {
                AllowedInstrument smb = allowedInstruments.FirstOrDefault(x => x.Name == Symbol);
                if (smb != null) return smb.Id;
            }
            return Symbol;
        }
    }
}
