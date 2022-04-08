using System;
using System.Reactive;
using ReactiveUI;
using entry.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace Company_Manager_System.ViewModel
{
   public class Company_Manager_System_ViewModel : ViewModelBase
   { }

   public class stuff
    {

        public string first_name{get; set;}="";
        public string last_name{get; set;}="";
        public string patronymic{get; set;}="";
        public TextBlock passport_series{get; set;}=new TextBlock();
        public string passport_num{get; set;}="";
        public string index_num{get; set;}="";
        public string phone{get; set;}="";
        public string email{get; set;}="";

        public stuff()
        {

        }
    }

   public class company
   {
       public int comp_num{get; set;}=0;
       public string type{get; set;}="";
       public string name{get; set;}="";
       public int UNP{get;set;}=0;
       public string addres{get; set;}="";
       public string login{get; set;}="";
       public string Bank_name{get; set;}="";
       public List<stuff> persons{get; set;}=new List<stuff>();
       public stuff temp_person{get;set;}=new stuff();

       public void add_person()
       {
           if(temp_person.first_name!=""&&temp_person.last_name!=""&&temp_person.patronymic!=""&&temp_person.passport_num!=""&&
           temp_person.index_num!=""&&temp_person.phone!=""&&temp_person.email!=""&&temp_person.passport_series.Text!="")
           persons.Add(temp_person);
       }

       public company()
       {

       }

       public company(string type, string name, int UNP, string addres, string login, int comp_num, string Bank_name)
       {
           this.Bank_name=Bank_name;
           this.type=type;
           this.name=name;
           this.UNP=UNP;
           this.addres=addres;
           this.login=login;
           this.comp_num=comp_num;
       }
   }

}