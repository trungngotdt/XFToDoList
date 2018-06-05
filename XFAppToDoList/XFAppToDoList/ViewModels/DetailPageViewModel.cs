using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using XFAppToDoList.Models;
using System.Globalization;

namespace XFAppToDoList.ViewModels
{
	public class DetailPageViewModel : ViewModelBase
	{
        private DateTime date;
        private TimeSpan time;
        private string titleJobs;
        private string detail;
        private bool available;
        private int index;

        private DelegateCommand<object> commandClickOk;
        public DelegateCommand<object> CommandClickOk =>
            commandClickOk ?? (commandClickOk = new DelegateCommand<object>((para)=> {ExecuteCommandClickOkAsync(para); }));

        
        
        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Detail Page";
        }

        public DateTime Date { get => date; set => date = value; }
        public TimeSpan Time { get => time; set => time = value; }
        public string Detail { get => detail; set => detail = value; }
        public string TitleJobs { get => titleJobs; set => titleJobs = value; }
        public bool Available { get => available; set => available = value; }

        void ExecuteCommandClickOkAsync(object parameters)
        {
            var jobEdit = new Jobs(TitleJobs, Detail, Available, new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Local));
            var p = new NavigationParameters { { "From", "DetailPage" }, { "id", index }, { "item", jobEdit } };
           NavigationService.GoBackAsync(p);
        }
        
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var job = parameters["SelectedItem"] as Jobs;
            index =(int) parameters["id"];
            TitleJobs = job.Title;
            Date = job.Date.Date;
            Detail = job.Detail;
            Time = job.Date.TimeOfDay;
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Detail");
            RaisePropertyChanged("TitleJobs");
            RaisePropertyChanged("Time");
        }
    }
}
