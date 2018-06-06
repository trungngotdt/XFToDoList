using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XFAppToDoList.Models;
using Prism.Services;

namespace XFAppToDoList.ViewModels
{
    public class MainPageViewModel : ViewModelBase //ContentPage
    {
        private IPageDialogService getPageDialogService;
        private string title;
        private DelegateCommand<object> commandPressed;
        private DelegateCommand<ListView> commandClickBtnAbout;
        private DelegateCommand commandAddJob;
        public DelegateCommand CommandAddJob =>
            commandAddJob ?? (commandAddJob = new DelegateCommand(ExecuteCommandAddJob));

        void ExecuteCommandAddJob()
        {
            NavigationService.NavigateAsync("DetailPage",new NavigationParameters { {"action","insert" } });
        }


        public DelegateCommand<ListView> CommandClickBtnAbout =>
            commandClickBtnAbout ?? (commandClickBtnAbout = new DelegateCommand<ListView>(async (l) => { await ExecuteCommandPopUpAsync(l); }));

        async Task ExecuteCommandPopUpAsync(ListView listView)
        {
            var p = new NavigationParameters
            {
                { "SelectedItem", listView.SelectedItem }
            };
            await NavigationService.NavigateAsync("AboutPage", p);
        }
        private ObservableCollection<Jobs> listToDo;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            getPageDialogService = pageDialogService;
            GetTitle = "Main Page";
            ListToDo.Add(new Jobs("Job a", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job b", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job c", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job d", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job e", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job f", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job g", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job h", "Job", true, DateTime.Now));
            for (int i = 0; i < 50; i++)
            {
                ListToDo.Add(new Jobs($"Job {i}", "Job", true, DateTime.Now));
            }
        }

        public ObservableCollection<Jobs> ListToDo
        {
            get
            {
                if (listToDo == null)
                {
                    listToDo = new ObservableCollection<Jobs>();
                }
                return listToDo;
            }
            set => listToDo = value;
        }
        public DelegateCommand<object> CommandPressed =>
            commandPressed ?? (commandPressed = new DelegateCommand<object>(ExecuteCommandPressed));

        public string GetTitle { get => title; set => title = value; }
        public IPageDialogService GetPageDialogService { get => getPageDialogService; set => getPageDialogService = value; }

        void ExecuteCommandPressed(object element)
        {
            try
            {
                var item = (element as ListView).SelectedItem as Jobs;
                var p = new NavigationParameters
                {
                    {"action","update" },
                    {"value","have" },
                    {"From","MainPage" },
                    { "SelectedItem", item },
                    {"id",ListToDo.IndexOf(item) }
                };
                NavigationService.NavigateAsync("DetailPage", p);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                try
                {
                    if (parameters.GetValue<string>("From").Equals("DetailPage"))
                    {
                        var index = (int)parameters["id"];
                        var item = parameters["item"] as Jobs;
                        if (parameters["action"].ToString().Equals("update"))
                        {
                            if (!ListToDo[index].Equals(item))
                            {
                                var temp = ListToDo[index];
                                var getAllProperties = item.GetType().GetProperties();
                                foreach (var property in getAllProperties)
                                {
                                    temp.GetType().GetProperty(property.Name).SetValue(temp, property.GetValue(item, null));
                                }
                            }
                        }
                        else if(parameters["action"].ToString().Equals("insert"))
                        {
                            ListToDo.Add(item);
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }
    }
}
