using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using XFAppToDoList.Models;

namespace XFAppToDoList.ViewModels
{
	public class DetailPageViewModel : ViewModelBase
	{
        private DateTime date;
        private DateTime time;
        private string titleJobs;
        private string detail;
        private bool available;

        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "About Page";
        }

        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Detail { get => detail; set => detail = value; }
        public string TitleJobs { get => titleJobs; set => titleJobs = value; }
        public bool Available { get => available; set => available = value; }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var job = parameters["SelectedItem"] as Jobs;
            TitleJobs = job.Title;
            Date = job.Date.Date;
            Detail = job.Detail;
            //Time = job.Date.TimeOfDay;
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Detail");
            RaisePropertyChanged("TitleJobs");
        }
    }
}
