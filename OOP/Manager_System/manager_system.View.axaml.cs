using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Manager_System.ViewModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using entry.ViewModels;
using entry.Views;
using Bank_System.ViewModel;
using System.Windows;
using System.Reactive;
using System;

namespace Manager_System.View
{
    public partial class Manager: Window
    {

        new public MainWindow Owner { get; set; }=new MainWindow();
        MainWindowViewModel main = new MainWindowViewModel();
        public List<Person> person = new List<Person>();
        int Bank_num=0;

        public Manager()
        {
            InitializeComponent();
        }

        public Manager(MainWindowViewModel main, List<Person> person, int Bank_num)
        {
            this.Bank_num=Bank_num;
            this.person=person;
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
            this.Owner.main[Bank_num].person=this.person;
            this.Owner.main[Bank_num]=this.main;
            this.Owner.Show();
        }

        public void appr(object sender, RoutedEventArgs e)
        {
            try 
            {
                main.setnum();
                person[main.num_].Get_credit(main);
                this.Owner.main[Bank_num].logs.Add(new log("манагер одобрил кредит"));
                main.remove_operations();
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {

            }
        }

         public void cancel(object sender, RoutedEventArgs e)
        {

            try 
            {
                if(main.temp_events.operation==0)
                {
                    main.temp_events.temp_person.search_acc=main.temp_events.temp_person.accounts.BinarySearch(main.temp_events.temp_bank);
                    main.temp_events.temp_person.acc_withdraw=main.temp_events.temp_money;
                    main.temp_events.temp_person.withdraw_money(main, 0, 1);
                    this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил +Бабки"));
                    main.remove_event();
                    InitializeComponent();
                }
                if(main.temp_events.operation==1)
                {
                    main.temp_events.temp_person.search_acc=main.temp_events.temp_person.accounts.BinarySearch(main.temp_events.temp_bank);
                    main.temp_events.temp_person.acc_put=main.temp_events.temp_money;
                    main.temp_events.temp_person.put_money(main,0, 1);
                    this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил "));
                    main.remove_event();
                    InitializeComponent();
                }
                if(main.temp_events.operation==2)
                {
                    person[0].transfer(main, main.temp_events.temp_recipient_bank, main.temp_events.temp_bank, main.temp_events.temp_money);
                    main.remove_transfer();
                    this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил перевод"));
                    InitializeComponent();
                }
            }
            catch (ArgumentOutOfRangeException) 
            {

            }
        }

        public void reject(object sender, RoutedEventArgs e)
        {
            try 
            {
                main.remove_operations();
                person[main.num_].credit_exists=false;
                this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил кредит"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {

            }
        }

        public void show_info(object sender, RoutedEventArgs e)
        {
            main.temp_person=main.temp_person1;
            InitializeComponent();
        }

        public void appr_person(object sender, RoutedEventArgs e)
        {
            if(main.temp_person1.first_name!="")
            {
                this.Owner.main[Bank_num].person.Add(main.temp_person1);
                main.app_person.Remove(main.temp_person1);
                main.temp_person1=new Person();
                main.temp_person=new Person();
                this.Owner.main[Bank_num].logs.Add(new log("манагер одобрил регистрацию"));
                InitializeComponent();
            }
        }

        public void reject_person(object sender, RoutedEventArgs e)
        {
            if(main.temp_person1.first_name!="")
            {
                main.app_person.Remove(main.temp_person1);
                main.temp_person1=new Person();
                main.temp_person=new Person();
                this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил регистрацию"));
                InitializeComponent();
            }
        }

        public void appr_salary(object sender, RoutedEventArgs e)
        {
            main.person[main.appr_sal.person_num].accounts[main.person[main.appr_sal.person_num].company_acc].salary_app=true;
            main.person[main.appr_sal.person_num].accounts[main.person[main.appr_sal.person_num].company_acc].salary_time=DateTime.Now.AddMonths(1);
            main.salary_accounts.Remove(main.appr_sal);
            this.Owner.main[Bank_num].logs.Add(new log("манагер одобрил З/П"));
            InitializeComponent();
        }

        public void reject_salary(object sender, RoutedEventArgs e)
        {
            main.salary_accounts.Remove(main.appr_sal);
            this.Owner.main[Bank_num].logs.Add(new log("манагер отклонил З/П"));
            InitializeComponent();
        }
    }
}