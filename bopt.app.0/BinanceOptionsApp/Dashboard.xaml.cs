using MultiTerminal.Connections.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using BinanceOptionsApp.Models;

namespace BinanceOptionsApp
{
    public partial class Dashboard : UserControl
    {
        private bool wasFirstLoad;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!wasFirstLoad)
            {
                wasFirstLoad = true;
                Model.UpdateDashboardStatus += Model_UpdateDashboardStatus;
                UpdateButtonsState();

                var connections = new List<ConnectionModel>();
                var types = ConnectionsModel.GetSupportedConnections();
                foreach (var type in types)
                {
                    if (Model.IsBrokerPresent(ConnectionModel.GetBrokerCode(type)))
                    {
                        connections.Add(Activator.CreateInstance(type) as ConnectionModel);
                    }
                }
                comboAddConnection.DisplayMemberPath = "BrokerDisplayName";
                comboAddConnection.ItemsSource = connections;
                comboAddConnection.SelectedIndex = 0;
                ConnectionSelectionChanged();

                var rm = (Application.Current as App).GetRM();
                //var bytes = (byte[])rm.GetObject("MT4EA");
                //string ver = Helpers.MetatraderInstance.GetEAVersion(bytes);
                //eaVersion.Text = "EA v" + ver;

                dgConnections.ItemsSource = Model.ConnectionsConfig.Connections;
            }
        }
        private void Model_UpdateDashboardStatus(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
        void UpdateButtonsState()
        {
            Model.Current.Save();
        }
        private void BuStart_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Model.Current.Save();
            (Application.Current.MainWindow as MainWindow).StartOne(b.DataContext as TradeModel);
        }

        private void BuStop_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Model.Current.Save();
            (Application.Current.MainWindow as MainWindow).StopOne(b.DataContext as TradeModel,true);
        }
        private void BuAddConnection_Click(object sender, RoutedEventArgs e)
        {
            buAddConnection.IsEnabled = false;
            buEditConnection.IsEnabled = false;
            ShowConnectionEditor(true, comboAddConnection.SelectedValue as ConnectionModel);
            buAddConnection.IsEnabled = true;
            buEditConnection.IsEnabled = true;
        }
        void ShowConnectionEditor(bool add, ConnectionModel source)
        {
            var editorType = ConnectionModel.GetConnectionEditor(source);
            if (editorType != null)
            {
                var editor = Activator.CreateInstance(editorType) as Window;
                editor.Owner = Application.Current.MainWindow;
                editorType.InvokeMember("Construct", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, editor, new object[] { source });
                if (editor.ShowDialog() == true)
                {
                    var modelProperty = editorType.GetProperty("Model");
                    ConnectionModel cm = modelProperty.GetValue(editor) as ConnectionModel;
                    if (add)
                    {
                        Model.ConnectionsConfig.Connections.Add(cm);
                        Model.AllConnections.Add(cm);
                    }
                    else
                    {
                        source.From(cm);
                    }
                }
            }
            else
            {
                if (add)
                {
                    Type type = source.GetType();
                    var cm = Activator.CreateInstance(type) as ConnectionModel;
                    cm.FillName();
                    Model.ConnectionsConfig.Connections.Add(cm);
                    Model.AllConnections.Add(cm);
                }
            }
            Model.ConnectionsConfig.Save();
        }

        private void BuRemoveConnection_Click(object sender, RoutedEventArgs e)
        {
            var item = dgConnections.SelectedValue as ConnectionModel;
            Model.ConnectionsConfig.Connections.Remove(item);
            Model.AllConnections.Remove(item);
            Model.ConnectionsConfig.Save();
        }

        private void BuEditConnection_Click(object sender, RoutedEventArgs e)
        {
            buAddConnection.IsEnabled = false;
            buEditConnection.IsEnabled = false;
            ShowConnectionEditor(false, dgConnections.SelectedValue as ConnectionModel);
            buAddConnection.IsEnabled = true;
            buEditConnection.IsEnabled = true;
        }

        private void DgConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConnectionSelectionChanged();
        }
        void ConnectionSelectionChanged()
        {
            buEditConnection.IsEnabled = dgConnections.SelectedValue != null;
            buRemoveConnection.IsEnabled = dgConnections.SelectedValue != null;
            buMoveUpConnection.IsEnabled = dgConnections.SelectedValue != null;
            buMoveDnConnection.IsEnabled = dgConnections.SelectedValue != null;
        }

        int MoveUp(ObservableCollection<ConnectionModel> source, ConnectionModel cm)
        {
            int index = source.IndexOf(cm);
            if (index <= 0) return index;
            source.Remove(cm);
            source.Insert(index - 1, cm);
            return index;
        }
        private void BuMoveUpConnection_Click(object sender, RoutedEventArgs e)
        {
            if (dgConnections.SelectedValue is ConnectionModel cm)
            {
                int index = MoveUp(Model.ConnectionsConfig.Connections, cm);
                if (index <= 0) return;
                MoveUp(Model.AllConnections, cm);
                Model.ConnectionsConfig.Save();
                dgConnections.SelectedIndex = index - 1;
                RestoreNullCombo(cm);
            }
        }
        void RestoreNullCombo(ConnectionModel cm)
        {
            if (cm != null)
            {
                var tabs = (Application.Current.MainWindow as MainWindow).TabItems;
                foreach (var tab in tabs)
                {
                    if (tab.Content is ITradeTabInterface cnt)
                    {
                        cnt.RestoreNullCombo(cm);
                    }
                }
            }
        }
        int MoveDown(ObservableCollection<ConnectionModel> source, ConnectionModel cm)
        {
            int index = source.IndexOf(cm);
            if (index < 0) return index;
            if (index == source.Count - 1) return index;
            source.Remove(cm);
            source.Insert(index + 1, cm);
            return index;
        }
        private void BuMoveDnConnection_Click(object sender, RoutedEventArgs e)
        {
            if (dgConnections.SelectedValue is ConnectionModel cm)
            {
                int index = MoveDown(Model.ConnectionsConfig.Connections, cm);
                if (index < 0) return;
                if (index == Model.ConnectionsConfig.Connections.Count - 1) return;
                MoveDown(Model.AllConnections, cm);
                Model.ConnectionsConfig.Save();
                dgConnections.SelectedIndex = index + 1;
                RestoreNullCombo(cm);
            }
        }
      
        private void BuChart_Click(object sender, RoutedEventArgs e)
        {
            string folder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".data");
            string filename = App.OpenFileDialog("1-leg|*.1leg;2-legs simple hedge|*.2legsh;2-legs lock|*.2legl", folder);
            if (!string.IsNullOrEmpty(filename))
            {
                Analyzer analyzer = new Analyzer(filename)
                {
                    Owner = Application.Current.MainWindow
                };
                analyzer.ShowDialog();
            }
        }
    }
}
