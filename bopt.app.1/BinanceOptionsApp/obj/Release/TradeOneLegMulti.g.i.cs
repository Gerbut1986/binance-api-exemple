﻿#pragma checksum "..\..\TradeOneLegMulti.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3542692EE2A24D444D301B2B8AFB454E88C5DBE85CE329C1C17354A06B22431B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BinanceOptionsApp;
using BinanceOptionsApp.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using VisualMarketsEngine.PropertyGrid;


namespace BinanceOptionsApp {
    
    
    /// <summary>
    /// TradeOneLegMulti
    /// </summary>
    public partial class TradeOneLegMulti : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buStart;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buStop;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buChart;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buAddFastProvider;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buAddSlowProvider;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buLoad;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buSave;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\TradeOneLegMulti.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BinanceOptionsApp.TradeVisualizer visual;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WesternpipsPrivate7;component/tradeonelegmulti.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TradeOneLegMulti.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.buStart = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\TradeOneLegMulti.xaml"
            this.buStart.Click += new System.Windows.RoutedEventHandler(this.BuStart_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buStop = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\TradeOneLegMulti.xaml"
            this.buStop.Click += new System.Windows.RoutedEventHandler(this.BuStop_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buChart = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\TradeOneLegMulti.xaml"
            this.buChart.Click += new System.Windows.RoutedEventHandler(this.BuChart_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buAddFastProvider = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\TradeOneLegMulti.xaml"
            this.buAddFastProvider.Click += new System.Windows.RoutedEventHandler(this.BuAddFastProvider_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.buAddSlowProvider = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\TradeOneLegMulti.xaml"
            this.buAddSlowProvider.Click += new System.Windows.RoutedEventHandler(this.BuAddSlowProvider_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buLoad = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\TradeOneLegMulti.xaml"
            this.buLoad.Click += new System.Windows.RoutedEventHandler(this.BuLoad_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.buSave = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\TradeOneLegMulti.xaml"
            this.buSave.Click += new System.Windows.RoutedEventHandler(this.BuSave_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.visual = ((BinanceOptionsApp.TradeVisualizer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

