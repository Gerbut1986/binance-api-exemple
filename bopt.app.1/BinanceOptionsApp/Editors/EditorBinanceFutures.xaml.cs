namespace BinanceOptionsApp.Editors
{
    using System.Windows;
    using global::MultiTerminal.Connections.Models;

    public partial class EditorBinanceFutures : Window, IEditorBase<BinanceFutureConnectionModel>
    {
        public BinanceFutureConnectionModel Model { get; set; }

        public void Construct(BinanceFutureConnectionModel source)
        {
            Model = new BinanceFutureConnectionModel();
            Model.From(source);
        }
        public EditorBinanceFutures()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Title = "BINANCE " + App.LanguageKey("locDlgConnection");
            cbTradeType.Items.Add(AccountTradeType.SPOT.ToString());
            cbTradeType.Items.Add(AccountTradeType.MARGIN.ToString());
            cbTradeType.SelectedIndex = (int)Model.AccountTradeType;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void BuAccept_Click(object sender, RoutedEventArgs e)
        {
            Model.AccountTradeType = (AccountTradeType)cbTradeType.SelectedIndex;
            DialogResult = true;
        }

        private void BuCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
