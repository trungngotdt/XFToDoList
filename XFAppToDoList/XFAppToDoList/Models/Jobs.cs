using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFAppToDoList.Models
{
    public class Jobs: INotifyPropertyChanged
    {
        private string title;
        private string detail;
        private bool available;
        private DateTime date;

        public string Detail { get => detail; set
            {
                if (detail!=value)
                {
                    detail = value;
                    OnPropertyChanged("Detail");
                }
                
            }
        }
        public bool Available { get => available;set
            {
                if (available != value)
                {
                    available = value;
                    OnPropertyChanged("Available");
                }

            }
        }
        public DateTime Date { get => date; set
            {
                if (date!= value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }

            }
        }
        public string Title { get => title; set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }

            }
        }

        public Jobs()
        {
             
        }
        public Jobs(string title,string detail,bool available,DateTime date)
        {
            this.Title = title;
            this.Detail = detail;
            this.Available = available;
            this.Date = date;
        }

        public override bool Equals(object obj)
        {
            if (obj is Jobs)
            {
                var guest = obj as Jobs;
                return this.Title.Equals(guest.Title) && this.Detail.Trim().Equals(guest.Detail.Trim())
                    && this.Available.Equals(guest.Available) && this.Date.Equals(guest.Date);
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }        

        public override string ToString()
        {
            return "Title : "+this.Title+ "Detail : "+this.Detail+"Status : "+this.Available+"Time : "+this.Date.ToString();
        }
    }
}
