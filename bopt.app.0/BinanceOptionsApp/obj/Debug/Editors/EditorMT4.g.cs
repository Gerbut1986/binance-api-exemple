﻿#pragma checksum "..\..\..\Editors\EditorMT4.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "63B191B43100CF3BB454EA151A0CAC9EBBB3B5AC1E0BD178F26FC960CBA82F14"
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


namespace WesternpipsPrivate7.Editors {
    
    
    /// <summary>
    /// EditorMT4
    /// </summary>
    public partial class EditorMT4 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\Editors\EditorMT4.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openSrv;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Editors\EditorMT4.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buAccept;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Editors\EditorMT4.xaml"
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
            System.Uri resourceLocater = new System.Uri("/WesternpipsPrivate7;component/editors/editormt4.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Editors\EditorMT4.xaml"
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
            
            #line 9 "..\..\..\Editors\EditorMT4.xaml"
            ((WesternpipsPrivate7.Editors.EditorMT4)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Editors\EditorMT4.xaml"
            ((WesternpipsPrivate7.Editors.EditorMT4)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.openSrv = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Editors\EditorMT4.xaml"
            this.openSrv.Click += new System.Windows.RoutedEventHandler(this.OpenSrv_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buAccept = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Editors\EditorMT4.xaml"
            this.buAccept.Click += new System.Windows.RoutedEventHandler(this.BuAccept_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buCancel = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\Editors\EditorMT4.xaml"
            this.buCancel.Click += new System.Windows.RoutedEventHandler(this.BuCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

