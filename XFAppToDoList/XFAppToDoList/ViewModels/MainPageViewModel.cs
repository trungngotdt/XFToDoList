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
        public DelegateCommand<ListView> CommandClickBtnAbout =>
            commandClickBtnAbout ?? (commandClickBtnAbout = new DelegateCommand<ListView>(async (l) => { await ExecuteCommandPopUpAsync(l); }));

        async Task ExecuteCommandPopUpAsync(ListView listView)
        {
            var p = new NavigationParameters
            {
                { "SelectedItem", listView.SelectedItem }
            };

            //var result=await Application.Current.MainPage.DisplayActionSheet("A", "a", "a", new string[] { "l" });
            await NavigationService.NavigateAsync("AboutPage", p);
            /*ActionSheetButton.CreateButton("", () => { });
            IActionSheetButton option1Action = ActionSheetButton.CreateButton("Option 1", () => { Debug.WriteLine("Option 1"); });
            IActionSheetButton option2Action = ActionSheetButton.CreateButton("Option 2", new DelegateCommand(() => { Debug.WriteLine("Option 2"); }));
            IActionSheetButton cancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => { Debug.WriteLine("Cancel"); }));
            */
            //var result= await GetPageDialogService.DisplayActionSheetAsync("Choose","Cancel",null,new string[] { "Option1" });
            //listToDo.RemoveAt(10);
            // Debug.WriteLine("Action: " + action);
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
            commandPressed ?? (commandPressed = new DelegateCommand<object>(async (o) => await ExecuteCommandPressedAsync(o)));

        public string GetTitle { get => title; set => title = value; }
        public IPageDialogService GetPageDialogService { get => getPageDialogService; set => getPageDialogService = value; }

        async Task ExecuteCommandPressedAsync(object element)
        {
            try
            {
                var item = (element as ListView).SelectedItem as Jobs;
                var p = new NavigationParameters
                {
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
                        var item= parameters["item"] as Jobs;
                    if (!ListToDo[(int)parameters["id"]].Equals(parameters["item"] as Jobs))
                    {
                            
                        ListToDo[(int)parameters["id"]] =
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
