﻿#pragma checksum "..\..\..\Views\CsoportokView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E7A83506A78ACE5D26D8446521AEF2B0188A79E504B46BA91A7ADCA81E8A419D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IktatogRPCClient.Views;
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


namespace IktatogRPCClient.Views {
    
    
    /// <summary>
    /// CsoportokView
    /// </summary>
    public partial class CsoportokView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CsoportokIsVisible;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ValaszthatoTelephely;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox TelephelyCsoportjai;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateCsoport;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifyCsoport;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveCsoport;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CreationIsVisible;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\CsoportokView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl ActiveItem;
        
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
            System.Uri resourceLocater = new System.Uri("/IktatogRPCClient;component/views/csoportokview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CsoportokView.xaml"
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
            this.CsoportokIsVisible = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ValaszthatoTelephely = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TelephelyCsoportjai = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.CreateCsoport = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.ModifyCsoport = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.RemoveCsoport = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.CreationIsVisible = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.ActiveItem = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

