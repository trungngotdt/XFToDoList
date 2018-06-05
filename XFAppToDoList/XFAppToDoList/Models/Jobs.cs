using System;
using System.Collections.Generic;
using System.Text;

namespace XFAppToDoList.Models
{
    public class Jobs
    {
        private string title;
        private string detail;
        private bool available;
        private DateTime date;

        public string Detail { get => detail; set => detail = value; }
        public bool Available { get => available; set => available = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Title { get => title; set => title = value; }

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

        public override string ToString()
        {
            return "Title : "+this.Title+ "Detail : "+this.Detail+"Status : "+this.Available+"Time : "+this.Date.ToString();
        }
    }
}
