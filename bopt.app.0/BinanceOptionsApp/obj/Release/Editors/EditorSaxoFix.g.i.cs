﻿#pragma checksum "..\..\..\Editors\EditorSaxoFix.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "604D899427AC9EDA0B6CE81710F63750084538981804675F97CF9031252E3ECA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WesternpipsPrivate7.Controls;
using WesternpipsPrivate7.CustomStyles;
using WesternpipsPrivate7.Editors;


namespace WesternpipsPrivate7.Editors {
    
    
    /// <summary>
    /// EditorSaxoFix
    /// </summary>
    public partial class EditorSaxoFix : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\Editors\EditorSaxoFix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tLoginQuote;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Editors\EditorSaxoFix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tPasswordQuote;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Editors\EditorSaxoFix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tLoginTrade;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Editors\EditorSaxoFix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tPasswordTrade;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Editors\EditorSaxoFix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buAccept;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Editors\EditorSaxoFix.xaml"
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
            System.Uri resourceLocater = new System.Uri("/WesternpipsPrivate7;component/editors/editorsaxofix.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Editors\EditorSaxoFix.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 10 "..\..\..\Editors\EditorSaxoFix.xaml"
            ((WesternpipsPrivate7.Editors.EditorSaxoFix)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\Editors\EditorSaxoFix.xaml"
            ((WesternpipsPrivate7.Editors.EditorSaxoFix)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tLoginQuote = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.tPasswordQuote = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.tLoginTrade = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.tPasswordTrade = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.buAccept = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Editors\EditorSaxoFix.xaml"
            this.buAccept.Click += new System.Windows.RoutedEventHandler(this.BuAccept_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.buCancel = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Editors\EditorSaxoFix.xaml"
            this.buCancel.Click += new System.Windows.RoutedEventHandler(this.BuCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

