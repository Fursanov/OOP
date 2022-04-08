using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Company_Manager_System.ViewModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using entry.Views;
using entry.ViewModels;

namespace Company_Manager_System.View
{

    public partial class Company_Manager: Window
    {

        new public MainWindow Owner { get; set; }= new MainWindow();

        public company this_company=new company();

        MainWindowViewModel main = new MainWindowViewModel();

        int Bank_num=0;

        public Company_Manager()
        {
            InitializeComponent();
        }

        void add_pers(object sender, RoutedEventArgs e)
        {
            this_company.add_person();
            InitializeComponent();
        }

        public Company_Manager(MainWindowViewModel main, int Bank_num, company comp)
        {
            this.main=main;
            this.Bank_num=Bank_num;
            this.this_company = comp;
            this.DataContext=this_company;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Owner.main[Bank_num].related_company[this_company.comp_num]=this.this_company;
            this.Owner.Show();
        }
    }
}