using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Serialization;

namespace BinanceOptionsApp.Models
{
    public class ConfigModel : BaseModel
    {
        public ConfigModel()
        {
            Tabs = new ObservableCollection<TradeModel>();
        }

        private ObservableCollection<TradeModel> _Tabs;
        public ObservableCollection<TradeModel> Tabs
        {
            get { return _Tabs; }
            set { if (_Tabs != value) { _Tabs = value; OnPropertyChanged(); } }
        }
        public static string ConfigPathname()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            string folder = System.IO.Path.GetDirectoryName(location);
            return System.IO.Path.Combine(folder, ".cfg","main.xml");
        }
        private static XmlSerializer CreateSerializer()
        {
            return new XmlSerializer(typeof(ConfigModel), new Type[] { typeof(BaseModel), typeof(TradeModel), typeof(ProviderModel), typeof(OpenOrderSettingsModel), typeof(CloseOrderSettingsModel) });
        }

        public static ConfigModel Load()
        {
            return Load(ConfigPathname());
        }
        public static ConfigModel Load(string filename)
        {
            ConfigModel res = null;
            try
            {
                using System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);
                var xs = CreateSerializer();
                res = xs.Deserialize(fs) as ConfigModel;
            }
            catch
            {
            }
            if (res == null) res = new ConfigModel();
            if (res.Tabs == null) res.Tabs = new ObservableCollection<TradeModel>();
            foreach (var tab in res.Tabs)
            {
                if (tab.Fast == null) tab.Fast = new ProviderModel();
                if (tab.Slow == null) tab.Slow = new ProviderModel();
                if (tab.Slow2 == null) tab.Slow2 = new ProviderModel();
                if (tab.Providers == null) tab.Providers = new ObservableCollection<ProviderModel>();
                if (tab.FastProviders == null) tab.FastProviders = new ObservableCollection<ProviderModel>();
                if (tab.SlowProviders == null) tab.SlowProviders = new ObservableCollection<ProviderModel>();
                tab.Fast.Parent = tab;
                tab.Slow.Parent = tab;
                tab.Slow2.Parent = tab;
                foreach (var provider in tab.Providers) provider.Parent = tab;
                foreach (var provider in tab.SlowProviders) provider.Parent = tab;
                foreach (var provider in tab.FastProviders) provider.Parent = tab;
                if (tab.Close == null) tab.Close = new CloseOrderSettingsModel();
                if (tab.Open == null) tab.Open = new OpenOrderSettingsModel();
                if (tab.Algo == TradeAlgorithm.OneLegMulti)
                {
                    if (tab.AlgoOneLegMulti == null)
                    {
                        tab.AlgoOneLegMulti = new AlgoOneLegMultiModel();
                    }
                    if (tab.AlgoControl == null)
                    {
                        tab.AlgoControl = new AlgoControlModel();
                    }
                }
            }
            return res;
        }
        public void Save()
        {
            Save(ConfigPathname());
        }
        public void Save(string filename)
        {
            try
            {
                using System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                var xs = CreateSerializer();
                xs.Serialize(fs, this);
            }
            catch
            {
            }
        }
    }

}
