﻿#pragma checksum "..\..\Chat.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7AC099AF0C8C7D0B5F006C85439077AD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
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


namespace Chat_Client_Interface {
    
    
    /// <summary>
    /// Chat
    /// </summary>
    public partial class Chat : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Chat_Client_Interface.Chat SecondWindow;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox chatbox;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox chatWindow;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox eKey;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox encription;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock keyError;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listbox;
        
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
            System.Uri resourceLocater = new System.Uri("/Chat Client Interface;component/chat.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Chat.xaml"
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
            this.SecondWindow = ((Chat_Client_Interface.Chat)(target));
            
            #line 4 "..\..\Chat.xaml"
            this.SecondWindow.Closed += new System.EventHandler(this.Window_Closed_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.chatbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 10 "..\..\Chat.xaml"
            this.chatbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Chat.xaml"
            this.chatbox.MouseEnter += new System.Windows.Input.MouseEventHandler(this.chatbox_MouseEnter);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Chat.xaml"
            this.chatbox.MouseLeave += new System.Windows.Input.MouseEventHandler(this.chatbox_MouseLeave);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Chat.xaml"
            this.chatbox.KeyDown += new System.Windows.Input.KeyEventHandler(this.chatbox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.chatWindow = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\Chat.xaml"
            this.chatWindow.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.eKey = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\Chat.xaml"
            this.eKey.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.eKey_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.encription = ((System.Windows.Controls.CheckBox)(target));
            
            #line 13 "..\..\Chat.xaml"
            this.encription.Checked += new System.Windows.RoutedEventHandler(this.encription_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.keyError = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.listbox = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
