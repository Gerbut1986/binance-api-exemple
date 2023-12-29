using System.Windows;
using System.Windows.Controls;

namespace BinanceOptionsApp
{
    public partial class ProviderDescription : UserControl
    {
        public ProviderDescription()
        {
            InitializeComponent();
        }
        private void BuDeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Models.ProviderModel;
            model.Parent.FastProviders.Remove(model);
            model.Parent.SlowProviders.Remove(model);
        }
        public bool ShowInternalProviders { get; set; }
        private void BuEditProvider_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Models.ProviderModel;
            var dlg = new ProviderEditDialog(model,ShowInternalProviders)
            {
                Owner = Application.Current.MainWindow
            };
            if (dlg.ShowDialog() == true)
            {
                model.EditFrom(dlg.Model);
            }
        }
    }
}
