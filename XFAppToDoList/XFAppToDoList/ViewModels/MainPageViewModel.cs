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
using XFAppToDoList.MyUtilities;
using XDependencyService = Xamarin.Forms.DependencyService;

namespace XFAppToDoList.ViewModels
{
    public class MainPageViewModel : ViewModelBase //ContentPage
    {
        
        #region field
        private int countItemSelect;

        
        private IPageDialogService getPageDialogService;
        private string title;
        private bool isDeleteMode;
        private bool isEnableBtnDeleteSelect;
        private bool isNormalMode;
        private bool isCheckBtnDeleteAll;
        private DelegateCommand commandBtnDeleteSelectPress;
        private  DelegateCommand<Element> commandLsvToDoSizeChanged;
       

       

        
        private DelegateCommand commandBtnCancelPress;
        private DelegateCommand commandBtnChoicePressed;
        private DelegateCommand<object> commandItemPressed;
        private DelegateCommand<ListView> commandClickBtnAbout;
        private DelegateCommand commandAddJob;
        private ObservableCollection<Jobs> listToDo;
        
        #endregion


        #region Property

        public DelegateCommand<Element> CommandLsvToDoSizeChanged =>
            commandLsvToDoSizeChanged ?? (commandLsvToDoSizeChanged = new DelegateCommand<Element>(ExecuteCommandLsvToDoSizeChanged));

        public DelegateCommand CommandBtnChoicePressed =>
            commandBtnChoicePressed ?? (commandBtnChoicePressed = new DelegateCommand(ExecuteCommandBtnChoicePressed));

        public DelegateCommand CommandAddJob =>
            commandAddJob ?? (commandAddJob = new DelegateCommand(ExecuteCommandAddJob));

        public DelegateCommand<ListView> CommandClickBtnAbout =>
            commandClickBtnAbout ?? (commandClickBtnAbout = new DelegateCommand<ListView>(async (l) => { await ExecuteCommandPopUpAsync(l); }));

        public DelegateCommand CommandBtnCancelPress =>
            commandBtnCancelPress ?? (commandBtnCancelPress = new DelegateCommand(ExecuteCommandBtnCancelPress));

        public DelegateCommand CommandBtnDeleteSelectPress =>
             commandBtnDeleteSelectPress ?? ( commandBtnDeleteSelectPress = new DelegateCommand(ExecuteCommandBtnDeleteSelectPress));

        public ObservableCollection<Jobs> ListToDo
        {
            get
            {
                listToDo = listToDo ?? new ObservableCollection<Jobs>();
               
                return listToDo;
            }
            set => listToDo = value;
        }

        public DelegateCommand<object> CommandItemPressed =>
            commandItemPressed ?? (commandItemPressed = new DelegateCommand<object>(async(o)=> { await ExecuteCommandItemPressedAsync(o); }));

        public string GetTitle { get => title; set => title = value; }

        public IPageDialogService GetPageDialogService { get => getPageDialogService; set => getPageDialogService = value; }

        public bool IsDeleteMode { get => isDeleteMode; set { isDeleteMode = value; RaisePropertyChanged("IsDeleteMode"); } }

        public bool IsEnableBtnDeleteSelect { get => isEnableBtnDeleteSelect; set {isEnableBtnDeleteSelect = value; RaisePropertyChanged("IsEnableBtnDeleteSelect"); } }

        public bool IsNormalMode { get => isNormalMode; set { isNormalMode = value; RaisePropertyChanged("IsNormalMode"); } }

        public bool IsCheckBtnDeleteAll { get => isCheckBtnDeleteAll;
            set {isCheckBtnDeleteAll = value;Debug.WriteLine("KKKKKKKK"); RaisePropertyChanged("IsCheckBtnDeleteAll"); } }

        #endregion


        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            IsNormalMode = true;
            XDependencyService.Get<IDirectoryHelper>().ReadData(@"yourfoldername/te.txt");
            getPageDialogService = pageDialogService;
            GetTitle = "Main Page";           
            ListToDo.Add(new Jobs("Job a", "Job", true, DateTime.Now));
            ListToDo.Add(new Jobs("Job b", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job c", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job d", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job e", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job f", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job g", "Job", false, DateTime.Now));
            ListToDo.Add(new Jobs("Job h", "Job", false, DateTime.Now));
            for (int i = 0; i < 50; i++)
            {
                ListToDo.Add(new Jobs($"Job {i}", "Job", false, DateTime.Now));
            }
        }
        
        /// <summary>
        /// Change Delete mode to Normal mode or Normal mode to Delete mode
        /// </summary>
        void ChangeMode()
        {
            IsDeleteMode = !IsDeleteMode;
            IsNormalMode = !IsNormalMode;
        }

        async Task ExecuteCommandPopUpAsync(ListView listView)
        {
            var p = new NavigationParameters
            {
                { "SelectedItem", listView.SelectedItem }
            };
            await NavigationService.NavigateAsync("AboutPage", p);
        }

        void ExecuteCommandBtnDeleteSelectPress()
        {
            if(IsCheckBtnDeleteAll)
            {
                ListToDo.Clear();
            }
            else
            {
                var count = ListToDo.ToList().Count;
                for (int i = 0; i < count; i++)
                {
                    if(ListToDo[i].Available)
                    {
                        ListToDo.RemoveAt(i);
                        i--;
                        count--;
                    }
                }
            }
        }

        async Task ExecuteCommandItemPressedAsync(object element)
        {
            var item = (element as ListView).SelectedItem as Jobs;
            if (IsDeleteMode)
            {
                item.Available = !item.Available;
                countItemSelect = (item.Available) ? countItemSelect + 1 : countItemSelect - 1;
            }
            else
            {
                try
                {
                    var p = new NavigationParameters
                    {
                        { "action","update" },
                        { "value","have" },
                        { "From","MainPage" },
                        { "SelectedItem", item },
                        { "id",ListToDo.IndexOf(item) }
                    };
                    await NavigationService.NavigateAsync("DetailPage", p);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        void ExecuteCommandBtnCancelPress()
        {
            ChangeMode();
        }

        void ExecuteCommandBtnChoicePressed()
        {
            ChangeMode();
        }

        void ExecuteCommandAddJob()
        {
            NavigationService.NavigateAsync("DetailPage", new NavigationParameters { { "action", "insert" } });
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
                        else if (parameters["action"].ToString().Equals("insert"))
                        {
                            ListToDo.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        void ExecuteCommandLsvToDoSizeChanged(Element parameter)
        {

            if(Device.RuntimePlatform.Equals(Device.WPF))
            {
                if (parameter is ListView)
                {
                    var temp = (parameter as ListView).ItemTemplate;
                    (parameter as ListView).ItemTemplate = null;
                    (parameter as ListView).ItemTemplate = temp;
                }

            }

        }
    }
}
