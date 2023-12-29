using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using VisualMarketsEngine;

namespace BinanceOptionsApp
{
    public partial class Analyzer : Window
    {
        readonly string path;
        public Analyzer(string path)
        {
            InitializeComponent();
            this.path = path;
        }
        readonly byte[] tmp = new byte[8];
        double ReadDouble(FileStream fs)
        {
            int readed = fs.Read(tmp, 0, 8);
            if (readed==8)
            {
                return BitConverter.ToDouble(tmp,0);
            }
            return 0.0;
        }
        long ReadLong(FileStream fs)
        {
            int readed = fs.Read(tmp, 0, 8);
            if (readed == 8)
            {
                return BitConverter.ToInt64(tmp, 0);
            }
            return 0;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Analyzer - " + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetDirectoryName(path));

            chartGroup.Settings.ScaleDateFormat2 = "HH:mm:ss.ffffff";
            chartGroup.Settings.Foreground = Color.FromRgb(0xff, 0xff, 0xff);
            chartGroup.Settings.Background = Color.FromRgb(0x00, 0x23, 0x44);
            chartGroup.Settings.NavigatorBackground = Color.FromRgb(0x00, 0x6d, 0xc5);
            chartGroup.Settings.NavigatorThumb = Color.FromRgb(0x20, 0x8d, 0xe5);
            chartGroup.Settings.TransparentPanel = Color.FromRgb(0x00, 0x23, 0x44);
            chartGroup.Settings.ChartBackground = Color.FromRgb(0x52, 0xbb, 0xea);
            chartGroup.Settings.GridPen = Color.FromArgb(50, 0x00, 0x23, 0x44);
            chartGroup.Clear();

            List<DoubleTimeValue> fastBids = new List<DoubleTimeValue>();
            List<DoubleTimeValue> fastAsks = new List<DoubleTimeValue>();
            List<DoubleTimeValue> slowBids = new List<DoubleTimeValue>();
            List<DoubleTimeValue> slowAsks = new List<DoubleTimeValue>();
            List<DoubleTimeValue> gapBuys = new List<DoubleTimeValue>();
            List<DoubleTimeValue> gapSells = new List<DoubleTimeValue>();

            var chartPrice = chartGroup.CreateChart("Price", true, 1.0, 7);
            int chartFastBidFlow = chartPrice.AddDoubleTimeFlow("FastBid", TimeFrame.Tick, Color.FromRgb(0xbf, 0xd7, 0x30), 2, false);
            int chartSlowBidFlow = chartPrice.AddDoubleTimeFlow("SlowBid", TimeFrame.Tick, Color.FromArgb(100, 0xbf, 0xd7, 0x30), 2, false);
            int chartFastAskFlow = chartPrice.AddDoubleTimeFlow("FastAsk", TimeFrame.Tick, Color.FromRgb(0xd4, 0x66, 0x00), 2, false);
            int chartSlowAskFlow = chartPrice.AddDoubleTimeFlow("SlowAsk", TimeFrame.Tick, Color.FromArgb(100, 0xd4, 0xd7, 0x30), 2, false);
            var chartGap = chartGroup.CreateChart("Gap", true, 1.0, 7);
            int chartGapBuyFlow = chartGap.AddDoubleTimeFlow("GapBuy", TimeFrame.Tick, Color.FromRgb(0xff, 0xff, 0xff), 2, false);
            int chartGapSellFlow = chartGap.AddDoubleTimeFlow("GapSell", TimeFrame.Tick, Color.FromArgb(100, 0xff, 0xff, 0xff), 2, false);

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                while (true)
                {
                    double fastBid = ReadDouble(fs);
                    double fastAsk = ReadDouble(fs);
                    double slowBid = ReadDouble(fs);
                    double slowAsk = ReadDouble(fs);
                    double gapBuy = ReadDouble(fs);
                    double gapSell = ReadDouble(fs);
                    long ticks = ReadLong(fs);
                    if (ticks == 0) break;
                    DateTime tm = new DateTime(ticks);
                    fastBids.Add(new DoubleTimeValue(fastBid, tm));
                    fastAsks.Add(new DoubleTimeValue(fastAsk, tm));
                    slowBids.Add(new DoubleTimeValue(slowBid, tm));
                    slowAsks.Add(new DoubleTimeValue(slowAsk, tm));
                    gapBuys.Add(new DoubleTimeValue(gapBuy, tm));
                    gapSells.Add(new DoubleTimeValue(gapSell, tm));
                }
            }

            chartPrice.AddDoubleTimeValuesTo(chartFastBidFlow, fastBids.ToArray());
            chartPrice.AddDoubleTimeValuesTo(chartSlowBidFlow, slowBids.ToArray());
            chartPrice.AddDoubleTimeValuesTo(chartFastAskFlow, fastAsks.ToArray());
            chartPrice.AddDoubleTimeValuesTo(chartSlowAskFlow, slowAsks.ToArray());
            chartGap.AddDoubleTimeValuesTo(chartGapBuyFlow, gapBuys.ToArray());
            chartGap.AddDoubleTimeValuesTo(chartGapSellFlow, gapSells.ToArray());
        }
    }
}
