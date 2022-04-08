using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using entry.Views;
using System;
using System.Reactive;
using ReactiveUI;
using entry;
using System.Windows;
using Bank_System.ViewModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using entry.ViewModels;

namespace Bank_System.View
{

    public partial class Bank : Window
    {
        new public MainWindow Owner { get; set; }=new MainWindow();

        public Person person = new Person();

        int person_num=0;
        int Bank_num=0;

        MainWindowViewModel main = new MainWindowViewModel();
    

        public Bank(MainWindowViewModel main, Person person, int person_num, int Bank_num)
        {
            this.main=main;
            this.person=person;
            this.person_num=person_num;
            this.Bank_num=Bank_num;
            this.DataContext = person; 
            InitializeComponent();
        }

        public Bank()
        {
            this.DataContext = person; 
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Owner.main[Bank_num] = this.main;
            this.Owner.main[Bank_num].person[person_num]=this.person;
            this.Owner.Show();
        }

        public void add_account(object sender, RoutedEventArgs e)
        {
            person.add_acc_f();
            this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" создал новый счёт"));
            InitializeComponent();
        }

        public void add_savings_account(object sender, RoutedEventArgs e)
        {
            person.add_sav_acc_f();
            this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" создал новый накопительный счёт"));
            InitializeComponent();
        }

        public void Get_zp(object sender, RoutedEventArgs e)
        {
            person.get_zp(main, person_num);
            this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" отправил запрос на З/П"));
            InitializeComponent();
        }

        public void freez_account(object sender, RoutedEventArgs e)
        {
            try
            {
                person.freez_acc();
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" заморозил/разморозил счёт"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
        }

        public void rem_account(object sender, RoutedEventArgs e)
        {
            try
            {
                person.rem_acc();
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" удалил счёт"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
        }

         public void get_credit(object sender, RoutedEventArgs e)
        {
            if(person.credit_exists==false && person.money_credit>0)
            {
                person.Set_credit();
                main.add_operations(person.first_name+" "+person.last_name+" запрашивает "+person.credit_+" в размере "+
                person.money_credit+" на "+person.time_, person.num);
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" запросил кредит"));
            }
        }

        public void rem_all(object sender, RoutedEventArgs e)
        {
            try
            {
                person.rem_All();
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" удалил все счета"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
        }

        public void transfer(object sender, RoutedEventArgs e)
        {
            try 
            {
                if(person.accounts[person.sender].acc_money>=person.recipient_money)
                    foreach(Person item1 in this.Owner.main[Bank_num].person)
                        if(item1.first_name==person.recipient_firstname && item1.last_name==person.recipient_lastname)
                            foreach(bank_account item in item1.accounts)
                                if(item.number==person.recipient_number)
                                {
                                    item1.transfer(main, item, person.accounts[person.sender], person.recipient_money);
                                    main.add_event(person.first_name+" "+person.last_name+
                                    " перевёл "+person.recipient_money+" пользователю "+item1.first_name+" "+item1.last_name,
                                    item, person.accounts[person.sender], person.recipient_money, 2);
                                    this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" сделал перевод"));
                                    InitializeComponent();
                                }   
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
            catch (InvalidCastException) 
            {
            
            }

        }

        public void put_account(object sender, RoutedEventArgs e)
        {
            try 
            {
                person.put_money(main, 0, 0);
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" положил деньги на счёт"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
        }

        public void withdraw_account(object sender, RoutedEventArgs e)
        {
            try 
            {
                person.withdraw_money(main, 0, 0);
                this.Owner.main[Bank_num].logs.Add(new log(person.last_name+" "+person.first_name+" снял деньги со счёта"));
                InitializeComponent();
            }
            catch (ArgumentOutOfRangeException) 
            {
            
            }
        }
    }
}