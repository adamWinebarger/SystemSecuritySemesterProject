﻿#pragma checksum "..\..\..\TesterAnswers.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FA974062E200F4D12EE061CBDCA8EF5CEC1F7CE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BAARS_4_Tester;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace BAARS_4_Tester {
    
    
    /// <summary>
    /// TesterAnswers
    /// </summary>
    public partial class TesterAnswers : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock nameLabel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Table;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showAdult;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showChild;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showOther;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label resultsLabel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\TesterAnswers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label resultsLabel_2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BAARS 4 Tester;component/testeranswers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TesterAnswers.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.nameLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Table = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.showAdult = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\TesterAnswers.xaml"
            this.showAdult.Click += new System.Windows.RoutedEventHandler(this.showAdult_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.showChild = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\TesterAnswers.xaml"
            this.showChild.Click += new System.Windows.RoutedEventHandler(this.showChild_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.showOther = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\TesterAnswers.xaml"
            this.showOther.Click += new System.Windows.RoutedEventHandler(this.showOther_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.resultsLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.resultsLabel_2 = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

