// Updated by XamlIntelliSenseFileGenerator 10/17/2020 3:38:47 PM
#pragma checksum "..\..\ModelCreationPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0D3AAEC8FE06F66B4AE456E0CAA2D4712AB14A9043AB63AF96F4CD012BB4F66D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MLGui;
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


namespace MLGui
{


    /// <summary>
    /// ModelCreationPage
    /// </summary>
    public partial class ModelCreationPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {


#line 26 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox IndependentCategoricalColumnSelector;

#line default
#line hidden


#line 27 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddIndependentColumnsButton;

#line default
#line hidden


#line 30 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox IndependentColumnContinuousSelector;

#line default
#line hidden


#line 31 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SecondColumnAddButton;

#line default
#line hidden


#line 38 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DependentColumnSelector;

#line default
#line hidden


#line 39 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddDependentColumnButton;

#line default
#line hidden


#line 42 "..\..\ModelCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateModelButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MLGui;component/modelcreationpage.xaml", System.UriKind.Relative);

#line 1 "..\..\ModelCreationPage.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.IndependentCategoricalColumnSelector = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 2:
                    this.AddIndependentColumnsButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 3:
                    this.IndependentColumnContinuousSelector = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 4:
                    this.SecondColumnAddButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 5:
                    this.IndependentRowSelector = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 6:
                    this.AddRowButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 7:
                    this.DependentColumnSelector = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 8:
                    this.AddDependentColumnButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 9:
                    this.CreateModelButton = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

