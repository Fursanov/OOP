using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using entry.ViewModels;
using entry.Views;
using entry;
using System.ComponentModel;
using Aboba1.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bank_System.ViewModel;

namespace Aboba.View
{
    public partial class Aboba_View : Window
    {
        
        new public MainWindow Owner { get; set; }=new MainWindow();
        public Person newperson = new Person();
        int Bank_num=0;

        public Aboba_View()
        { 
            InitializeComponent();
        }

        public Aboba_View(int Bank_num)
        {
            this.Bank_num=Bank_num;
            newperson.num=this.Owner.main[Bank_num].person.Count;
            this.DataContext = newperson; 
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Owner.Show();
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {   
            if(newperson.input_finish()==true)
            {
                newperson.num=this.Owner.main[Bank_num].person.Count;
                this.Owner.main[Bank_num].app_person.Add(newperson);
                
                this.Close();
                this.Owner.main[Bank_num].logs.Add(new log(newperson.last_name+" "+newperson.first_name+" отправил запрос на регистрацию"));
            }
        }
    }
}