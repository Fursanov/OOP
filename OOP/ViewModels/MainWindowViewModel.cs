using System;
using System.Collections.Generic;
using System.Text;
using entry.Views;
using Bank_System.ViewModel;
using Company_Manager_System.ViewModel;

namespace entry.ViewModels
{

    public class log
    {
        public string name{get; set;}="";

        public log()
        {}

        public log(string name)
        {
            this.name=name;
        }
    }

    public class salary
    {
        public string name{get; set;}="";
        public string company_name{get; set;}="";
        public bank_account salary_acc{get;set;}=new bank_account("");
        public int person_num=0;

        public salary()
        {}

        public salary(string name, string company_name, bank_account salary_acc, int person_num)
        {
            this.name=name;
            this.company_name=company_name;
            this.salary_acc=salary_acc;
            this.person_num=person_num;
        }
    }

    public class Events
    {
        public string eve{get; set;}="";
        public Person temp_person{get; set;}=new Person();
        public bank_account temp_bank{get; set;}=new bank_account("");
        public double temp_money{get; set;}=0;
        public bank_account temp_recipient_bank{get; set;}=new bank_account("");
        public int operation{get; set;}=0;

        public Events(string eve)
        {
            this.eve=eve;
        }
        
        public Events(string eve, Person temp_person, bank_account temp_bank, double temp_money, int operation)
        {
            this.temp_person=temp_person;
            this.temp_bank=temp_bank;
            this.temp_money=temp_money;
            this.operation=operation;
            this.eve=eve;
        }

        public Events(string eve, bank_account temp_bank, bank_account temp_recipient_bank, double temp_money, int operation)
        {
            this.temp_bank=temp_bank;
            this.temp_money=temp_money;
            this.temp_recipient_bank=temp_recipient_bank;
            this.operation=operation;
            this.eve=eve;
        }
    }

    public class Operations
    {
        public int num{get; set;}=0;
        public string apr{get; set;}="";
        
        public Operations(string apr, int num)
        {
            this.apr=apr;
            this.num=num;
        }
    }


    public class MainWindowViewModel : ViewModelBase
    {
        public DateTime date{get; set;} = new DateTime();
        public List<Person> person = new List<Person>();
        public List<Events> events{get;} = new List<Events>();
        public Events temp_events{get;set;}=new Events("");
        public List<Operations> operations{get;} = new List<Operations>();
        public List<Person> app_person{get; set;} = new List<Person>();
        public Person temp_person{get; set;} = new Person();
        public Person temp_person1{get; set;} = new Person();
        public Operations temp{get; set;} = new Operations("",0);
        public int num_{get; set;}=0;
        public List<company> related_company=new List<company>();
        public List<salary> salary_accounts{get; set;}=new List<salary>();
        public salary appr_sal{get; set;}=new salary();
        public List<log> logs{get; set;}=new List<log>();

        public void add_sal_acc(string name, string company_name, bank_account salary_acc, int person_num)
        {
            foreach(company item in related_company)
                if(item.name==company_name)
                    foreach(stuff item1 in item.persons)
                        if(item1.first_name==person[person_num].first_name&&item1.last_name==person[person_num].last_name
                        && item1.patronymic==person[person_num].patronymic)
                            salary_accounts.Add(new salary(name, company_name, salary_acc, person_num));
                        else person[person_num].salary_exists=false;
                else person[person_num].salary_exists=false;
        }        

        public void setnum()
        {
            num_=temp.num;
        }

        public void add_event(string e)
        {
            temp_events=new Events(e);
            events.Add(temp_events);
        }

        public void change_time()
        {
            this.date=DateTime.Now;
            foreach(Person pers in this.person)
                if(pers.credit_exists==true && pers.credit_app==true)
                {
                    DateTime temp=pers.credit_time;
                    while(temp<date)
                        {
                            pers.acc_withdraw=pers.credit_withdraw;
                            pers.search_acc=pers.credit_account;
                            pers.withdraw_money(this,1,1);
                            temp=temp.AddMonths(1);
                            if(pers.credit_stop<temp)
                            {
                                pers.credit_exists=false;
                                pers.credit_app=false;
                                break;
                            }
                        }
                    pers.credit_time=temp;
                }
            foreach(Person pers in this.person)
                if(pers.salary_exists==true)
                    foreach(bank_account banks in pers.accounts)
                        if(banks.salary_app==true)
                        {
                            DateTime temp=banks.salary_time;
                            while(temp<date)
                                {
                                    banks.acc_money+=10000;
                                    temp=temp.AddMonths(1);
                                }
                            banks.salary_time=temp;
                        }
        }

        public void add_event(string e, Person p, bank_account b, double m, int o)
        {
            events.Add(new Events(e, p, b, m, o));
        }

        public void add_event(string e, bank_account b, bank_account r_b, double m, int o)
        {
            events.Add(new Events(e, b, r_b, m, o));
        }

        public void add_operations(string o, int n)
        {
            operations.Add(new Operations(o, n));
        }

        public void remove_operations()
        {
            operations.Remove(temp);
        }
        
        public void remove_event()
        {
            events.Remove(temp_events);
            events.Remove(events[events.Count-1]);
        }

        public void remove_transfer()
        {
            events.Remove(temp_events);
        }

        public void exception()
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("ошибка",  "Пожалуйста, не ломай       ");
                    messageBoxStandardWindow.Show();
        }
    }
    
}
