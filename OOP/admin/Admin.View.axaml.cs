using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Admin.ViewModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using entry.ViewModels;
using entry.Views;
using Bank_System.ViewModel;
using System.Windows;
using System.Reactive;
using System;

namespace Admin.View
{
    public partial class Admin_acc: Window
    {

        new public MainWindow Owner { get; set; }=new MainWindow();
        MainWindowViewModel main = new MainWindowViewModel();
        int Bank_num=0;

        public Admin_acc()
        {
            InitializeComponent();
        }

        public Admin_acc(MainWindowViewModel main, int Bank_num)
        {
            this.Bank_num=Bank_num;
            this.main=main;
            this.DataContext = this.main; 
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Owner.main[Bank_num]=this.main;
            this.Owner.Show();
        }

         
    }
}