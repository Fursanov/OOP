using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Aboba.View;
using Bank_System.View;
using Manager_System.View;
using Company_Manager_System.ViewModel;
using Company_Manager_System.View;
using Operator_System.View;
using entry.ViewModels;
using System.Windows;
using Admin.View;
using System;
using Bank_System.ViewModel;
using System.Collections.Generic;

namespace entry.Views

{

    public partial class MainWindow : Window
    {
        public List<MainWindowViewModel> main = new List<MainWindowViewModel>();
        public string check_login{get; set;}="";
        public string check_password{get; set;}="";
        public int Bank_num{get; set;}=0;

        public MainWindow()
        {
            main.Add(new MainWindowViewModel());
            main.Add(new MainWindowViewModel());
            main.Add(new MainWindowViewModel());
            main[0].related_company.Add(new company("asd0","asd0",0,"asd0","asd0",0,"AboBank"));
            main[0].related_company.Add(new company("asd1","asd1",1,"asd1","asd1",1,"AboBank"));
            main[0].related_company.Add(new company("asd2","asd2",2,"asd2","asd2",2,"AboBank"));
            main[1].related_company.Add(new company("asd3","asd3",3,"asd3","asd3",0,"хрен вам банк"));
            main[1].related_company.Add(new company("asd4","asd4",4,"asd4","asd4",1,"хрен вам банк"));
            main[1].related_company.Add(new company("asd5","asd5",5,"asd5","asd5",2,"хрен вам банк"));
            main[2].related_company.Add(new company("asd6","asd6",6,"asd6","asd6",0,"Банк Барыга"));
            main[2].related_company.Add(new company("asd7","asd7",7,"asd7","asd7",1,"Банк Барыга"));
            main[2].related_company.Add(new company("asd8","asd8",8,"asd8","asd8",2,"Банк Барыга"));
            main[2].related_company.Add(new company("asd9","asd9",9,"asd9","asd9",3,"Банк Барыга"));
            this.DataContext=this;
            InitializeComponent();
        }

        public void create_aboba(object sender, RoutedEventArgs e)
        {
           main[Bank_num].change_time();
           var ab = new Aboba_View(Bank_num);
           ab.Owner=this;
           ab.Show();
           this.Hide();

        }

        public void create_bank(object sender, RoutedEventArgs e)
        {
            main[Bank_num].change_time();
            foreach(Person item in main[Bank_num].person)
                if(item.login==check_login && item.password==check_password)
                {
                    var ba = new Bank(main[Bank_num], item, item.num, Bank_num);
                    ba.Owner=this;
                    ba.Show();
                    this.Hide();
                }
        }

        public void create_company(object sender, RoutedEventArgs e)
        {
            main[Bank_num].change_time();
            foreach(company item1 in main[Bank_num].related_company)
                if(check_login==item1.login)
                {
                    var co = new Company_Manager(main[Bank_num], Bank_num, item1);
                    co.Owner=this;
                    co.Show();
                    this.Hide();
                }
        }


        public void create_manager(object sender, RoutedEventArgs e)
        {
            main[Bank_num].change_time();
            var ma = new Manager(main[Bank_num], main[Bank_num].person, Bank_num);
            ma.Owner=this;
            ma.Show();
            this.Hide();
        }

        public void create_operator(object sender, RoutedEventArgs e)
        {
            main[Bank_num].change_time();
            var op = new Operator(main[Bank_num], main[Bank_num].person, Bank_num);
            op.Owner=this;
            op.Show();
            this.Hide();
        }

        public void create_admin(object sender, RoutedEventArgs e)
        {
            main[Bank_num].change_time();
            var ad = new Admin_acc(main[Bank_num], Bank_num);
            ad.Owner=this;
            ad.Show();
            this.Hide();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}