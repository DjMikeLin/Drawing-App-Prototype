﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B69618A497D46931CCA3C17F25311EE4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LIN_YU_HW4.View;
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


namespace LIN_YU_HW4.View {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pane;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pane2;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Shape;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_FillColor;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_StrokeColor;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border border;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
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
            System.Uri resourceLocater = new System.Uri("/LIN_YU_HW4;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 8 "..\..\MainWindow.xaml"
            ((LIN_YU_HW4.View.MainWindow)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.Window_KeyUp_ESC);
            
            #line default
            #line hidden
            
            #line 9 "..\..\MainWindow.xaml"
            ((LIN_YU_HW4.View.MainWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown_CTRL_X);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_Reset);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 21 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_Save_As);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 22 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_Exit);
            
            #line default
            #line hidden
            return;
            case 5:
            this.pane = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.pane2 = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.ComboBox_Shape = ((System.Windows.Controls.ComboBox)(target));
            
            #line 51 "..\..\MainWindow.xaml"
            this.ComboBox_Shape.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_Shape_Selected);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ComboBox_FillColor = ((System.Windows.Controls.ComboBox)(target));
            
            #line 52 "..\..\MainWindow.xaml"
            this.ComboBox_FillColor.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_FillColor_Selected);
            
            #line default
            #line hidden
            return;
            case 9:
            this.textBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\MainWindow.xaml"
            this.textBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ComboBox_StrokeColor = ((System.Windows.Controls.ComboBox)(target));
            
            #line 54 "..\..\MainWindow.xaml"
            this.ComboBox_StrokeColor.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_StrokeColor_Selected);
            
            #line default
            #line hidden
            return;
            case 11:
            this.border = ((System.Windows.Controls.Border)(target));
            return;
            case 12:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 58 "..\..\MainWindow.xaml"
            this.canvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 58 "..\..\MainWindow.xaml"
            this.canvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.Canvas_MouseMove);
            
            #line default
            #line hidden
            
            #line 58 "..\..\MainWindow.xaml"
            this.canvas.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
