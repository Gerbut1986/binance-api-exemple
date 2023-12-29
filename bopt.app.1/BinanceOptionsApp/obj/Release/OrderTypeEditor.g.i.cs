﻿#pragma checksum "..\..\OrderTypeEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "773860CB4A90558A921C6F2DC858BE4C1B34D8B51B2DA8211F4E2604622DB928"
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
using BinanceOptionsApp.CustomStyles;
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


namespace BinanceOptionsApp {
    
    
    /// <summary>
    /// OrderTypeEditor
    /// </summary>
    public partial class OrderTypeEditor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbOrderType;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbFill;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFill;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPendingDistance;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPendingLifetime;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buOk;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\OrderTypeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/WesternpipsPrivate7;component/ordertypeeditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OrderTypeEditor.xaml"
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
            
            #line 14 "..\..\OrderTypeEditor.xaml"
            ((BinanceOptionsApp.OrderTypeEditor)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\OrderTypeEditor.xaml"
            ((BinanceOptionsApp.OrderTypeEditor)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbOrderType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.lbFill = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.cbFill = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.tbPendingDistance = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbPendingLifetime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.buOk = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\OrderTypeEditor.xaml"
            this.buOk.Click += new System.Windows.RoutedEventHandler(this.BuOk_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buCancel = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\OrderTypeEditor.xaml"
            this.buCancel.Click += new System.Windows.RoutedEventHandler(this.BuCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
