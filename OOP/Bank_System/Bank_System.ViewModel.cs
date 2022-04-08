using System;
using System.Reactive;
using ReactiveUI;
using entry.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Company_Manager_System.ViewModel;

namespace Bank_System.ViewModel
{
   public class Bank_System_ViewModel : ViewModelBase
   { }

   public class bank_account
   {
      public int freez{get; set;}=0;
      public int acc{get; set;}=0;
      public string acc_name{get; set;}="";
      public double acc_money{get; set;}=0;
      public int number{get; set;}=0;
      public int sender{get; set;}=0;
      public bool salary_app{get; set;}=false;
      public DateTime salary_time=new DateTime();
       
      public bank_account(string Name)
      {
         acc_name=Name;
         Guid myGuid = Guid.NewGuid();
         Random a = new Random(myGuid.GetHashCode());
         number=a.Next(0, 99999999);
      }

      public bank_account(string Name, int Acc)
      {
         acc_name=Name;
         acc=Acc;
         Guid myGuid = Guid.NewGuid();
         Random a = new Random(myGuid.GetHashCode());
         number=a.Next(0, 99999999);
      }

   }

   public class Person 
   {

      public Person()
      {}

      public string first_name{get; set;}="";
      public string last_name{get; set;}="";
      public string patronymic{get; set;}="";
      public TextBlock passport_series{get; set;}=new TextBlock();
      public string passport_num{get; set;}="";
      public string index_num{get; set;}="";
      public string phone{get; set;}="";
      public string email{get; set;}="";

      public string login{get; set;}="";
      public string password{get; set;}="";
      public string repeat_password{get; set;}="";

      public bool input_finish()
      {
         if(first_name!=""&&last_name!=""&&patronymic!=""&&passport_series.Text!=""&&passport_num!=""&&
            index_num!=""&&phone!=""&&email!=""&& login!=""&&password!="")
            return true;
         else return false;
      }

      public int sender{get; set;}=0;
      public string recipient_lastname{get; set;}="";
      public string recipient_firstname{get; set;}="";
      public int recipient_number{get; set;}=0;
      public double recipient_money{get; set;}=0;

      public bool credit_exists{get; set;}=false;
      public bool credit_app{get; set;}=false;
      public bool salary_exists{get; set;}=false;
      public int num{get; set;}=0;
      public List<bank_account> accounts { get; } = new List<bank_account>();
      public int search_acc{get; set;}=0;
      public int search_num{get; set;}=0;
      public string add_acc{get; set;}="";
      public string add_sav_acc{get; set;}="";
      public int add_freez{get; set;}=0;
      public double acc_put{get; set;}=0;
      public double acc_withdraw{get; set;}=0;
      public int search_credit_acc{get; set;}=0;
      public double money_credit{get; set;}=0;
      public string credit_{get; set;}="";

      public DateTime credit_stop=new DateTime();
      public DateTime credit_time=new DateTime();
      public int credit_counter=0;
      public double credit_withdraw=0;
      public int credit_account=0;
      
      public string time_{get; set;}="";
      public int time{get; set;}=0;
      public int credit{get; set;}=0;

      public string search_company{get;set;}="";
      public int company_acc{get; set;}=0;


      public void get_zp(MainWindowViewModel main, int person_num)
      {
         if(salary_exists==false)
            foreach(company item in main.related_company)
               if(item.name==search_company)
               {
                  main.add_sal_acc(last_name+" "+first_name, search_company, accounts[company_acc],person_num);
                  salary_exists=true;
               }
      }

      public void add_acc_f()
      {
         foreach(bank_account item in accounts)
            if (add_acc == item.acc_name)
               return;   
         accounts.Add(new bank_account(add_acc, 0));
      }

      public void add_sav_acc_f()
      {
         foreach(bank_account item in accounts)
            if (add_sav_acc == item.acc_name)
               return;
         accounts.Add(new bank_account("!Накопительный счёт! "+add_sav_acc, 1));
      }

      public void freez_acc()
      {
            if(accounts[add_freez].freez==0)
               accounts[add_freez].freez=1;
            else
               accounts[add_freez].freez=0;
      }

      public void rem_acc()
      {
         accounts.Remove(accounts[search_num]);
      }

      public void rem_All()
      {
         int i=accounts.Count;
         for(; i >0; i--)
            accounts.Remove(accounts[0]);
      }

      public void put_money(MainWindowViewModel main, int check_credit, int admin)
      {
         if(accounts[search_acc].freez!=1 || admin==1)
         {
            accounts[search_acc].acc_money+=acc_put;
            if(check_credit==0)
               main.add_event(last_name+" "+first_name+" положил на счёт "+accounts[search_acc].number+" "+acc_put, this, accounts[search_acc], acc_put, 0);
            else
               main.add_event("+кредит");
         }
         else
         {

         }
      }

      public void transfer(MainWindowViewModel main, bank_account recipient, bank_account sender, double put)
      {
         if(recipient.freez!=1 && sender.freez!=1 && sender.acc_money>=put)
         {
            recipient.acc_money+=put;
            sender.acc_money-=put;
         }
         else
         {

         }
      }

      public void Set_credit()
      {
         credit_exists=true;
            switch(time)
            {
               case 0:
                  time_="3 месяца";
                  break;
               case 1:
                  time_="6 месяцев";
                  break;
               case 2:
                  time_="12 месяцев";
                  break;
               case 3:
                  time_="24 месяца";
                  break;
               case 4:
                  time_="48 месяцев";
                  break;
            }
         switch(credit)
            {
               case 0:
                  credit_="кредит";
                  break;
               case 1:
                  credit_="рассрочку";
                  break;
            }
      }

      public void Get_credit(MainWindowViewModel main)
      {
         credit_app=true;
         switch(time)
         {
            case 0:
               credit_withdraw=money_credit/3;
               credit_stop=DateTime.Now.AddMonths(4);
               break;
            case 1:
               credit_withdraw=money_credit/6;
               credit_stop=DateTime.Now.AddMonths(7);
               break;
            case 2:
               credit_withdraw=money_credit/12;
               credit_stop=DateTime.Now.AddMonths(13);
               break;
            case 3:
               credit_withdraw=money_credit/24;
               credit_stop=DateTime.Now.AddMonths(25);
               break;
            case 4:
               credit_withdraw=money_credit/48;
               credit_stop=DateTime.Now.AddMonths(49);
               break;
         }
         switch(credit)
         {
            case 0:
               credit_withdraw*=1.9;
               break;
            case 1:
               break;
         }
         acc_put=money_credit;
         search_acc=search_credit_acc;
         credit_time=DateTime.Now.AddMonths(1);
         credit_account=search_credit_acc;
         put_money(main, 1, 0);
         acc_put=0;
         search_acc=0;
      }

      public void withdraw_money(MainWindowViewModel main, int check_credit, int admin)
      {
         if(accounts[search_acc].acc_money>=acc_withdraw || admin==1)
            if(accounts[search_acc].freez!=1 || admin==1)
               {
                  accounts[search_acc].acc_money-=acc_withdraw;
                  if(check_credit==0)
                     main.add_event(last_name+" "+first_name+" снял со счёта "+accounts[search_acc].number+" "+acc_withdraw, this, accounts[search_acc], acc_withdraw, 1);
                  else
                     main.add_event("-кредит");
               }
            else
            {

            }
      }

   
   }
}