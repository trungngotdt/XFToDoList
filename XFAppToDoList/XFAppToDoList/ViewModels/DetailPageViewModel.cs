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
        private string action;
        private DateTime date;
        private TimeSpan time;
        private string titleJobs;
        private string detail;
        private bool available;
        private int index;

        private DelegateCommand<object> commandClickOk;
        public DelegateCommand<object> CommandClickOk =>
            commandClickOk ?? (commandClickOk = new DelegateCommand<object>((para) => { ExecuteCommandClickOkAsync(para); }));
        private DelegateCommand commandCancel;
        public DelegateCommand CommandCancel =>
            commandCancel ?? (commandCancel = new DelegateCommand(ExecuteCommandCancel));

        void ExecuteCommandCancel()
        {
            NavigationService.GoBackAsync();
        }


        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Detail Page";
        }

        public DateTime Date { get => date; set => date = value; }
        public TimeSpan Time { get => time; set => time = value; }
        public string Detail { get => detail; set => detail = value; }
        public string TitleJobs { get => titleJobs; set => titleJobs = value; }
        public bool Available { get => available; set => available = value; }
        public string Action { get => action; set => action = value; }

        async void ExecuteCommandClickOkAsync(object parameters)
        {
            var jobEdit = new Jobs(TitleJobs, Detail, Available, new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Local));
            var p = new NavigationParameters { {"action",Action },{"value","have" }, { "From", "DetailPage" }, { "id", index }, { "item", jobEdit } };
            await NavigationService.GoBackAsync(p);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Action = parameters["action"].ToString();
            if (Action.Equals("update"))
            {
                var job = parameters["SelectedItem"] as Jobs;
                index = (int)parameters["id"];
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
}
