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
using LiteDB;

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
        private DelegateCommand<Element> commandLsvToDoSizeChanged;

        private DelegateCommand<object> commandToggleButtonPress;




        private DelegateCommand commandBtnCancelPress;
        private DelegateCommand commandBtnChoicePressed;
        private DelegateCommand<object> commandItemPressed;
        private DelegateCommand<ListView> commandClickBtnAbout;
        private DelegateCommand commandAddJob;
        private DelegateCommand commandBtnDeleteAllChangeState;




        private ObservableCollection<Jobs> listToDo;

        #endregion


        #region Property
        public DelegateCommand CommandBtnDeleteAllChangeState =>
                    commandBtnDeleteAllChangeState ?? (commandBtnDeleteAllChangeState = new DelegateCommand(ExecuteCommandBtnDeleteAllChangeState));
        public DelegateCommand<object> CommandToggleButtonPress =>
            commandToggleButtonPress ?? (commandToggleButtonPress = new DelegateCommand<object>(ExecuteCommandToggleButtonPress));


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
             commandBtnDeleteSelectPress ?? (commandBtnDeleteSelectPress = new DelegateCommand(ExecuteCommandBtnDeleteSelectPress));

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
            commandItemPressed ?? (commandItemPressed = new DelegateCommand<object>(async (o) => { await ExecuteCommandItemPressedAsync(o); }));

        public string GetTitle { get => title; set => title = value; }

        public IPageDialogService GetPageDialogService { get => getPageDialogService; set => getPageDialogService = value; }

        public bool IsDeleteMode { get => isDeleteMode; set { isDeleteMode = value; RaisePropertyChanged("IsDeleteMode"); } }

        public bool IsEnableBtnDeleteSelect { get => isEnableBtnDeleteSelect; set { isEnableBtnDeleteSelect = value; RaisePropertyChanged("IsEnableBtnDeleteSelect"); } }

        public bool IsNormalMode { get => isNormalMode; set { isNormalMode = value; RaisePropertyChanged("IsNormalMode"); } }

        public bool IsCheckBtnDeleteAll
        {
            get => isCheckBtnDeleteAll;
            set
            {
                isCheckBtnDeleteAll = value;

                RaisePropertyChanged("IsCheckBtnDeleteAll");
            }
        }

        #endregion


        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            IsNormalMode = true;
            //XDependencyService.Get<IDirectoryHelper>().CreateFolder(@"yourfoldername");
            getPageDialogService = pageDialogService;
            GetTitle = "Main Page";
            LoadDB();
           
            countItemSelect = ListToDo.Count(item => item.Available);
        }




        #region Helper
        /// <summary>
        /// Change Delete mode to Normal mode or Normal mode to Delete mode
        /// </summary>
        void ChangeMode()
        {
            IsDeleteMode = !IsDeleteMode;
            IsNormalMode = !IsNormalMode;
        }


        void ToggleButtonChangeState(object obj)
        {
            var temp = countItemSelect;
            var count = ListToDo.Count;
            var item = obj as Jobs;
            item.Available = !item.Available;
            countItemSelect = (item.Available) ? countItemSelect + 1 : countItemSelect - 1;
            if (countItemSelect == count || temp == count && countItemSelect < count)
            {
                IsCheckBtnDeleteAll = !IsCheckBtnDeleteAll;
                //ExecuteCommandBtnDeleteAllChangeState();
            }
        }

        void LoadDB()
        {
            try
            {
                var strConn = Xamarin.Forms.DependencyService.Get<IDirectoryHelper>().GetDBAddress("data.db");
                using (var db = new LiteDatabase(strConn))
                {
                    var jobs = db.GetCollection<Jobs>();
                    ListToDo = new ObservableCollection<Jobs>(jobs.FindAll());
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        #endregion

        #region Command

        void ExecuteCommandBtnDeleteAllChangeState()
        {
            var value = !IsCheckBtnDeleteAll;
            Parallel.ForEach(ListToDo, (item) => { item.Available = value; });
            countItemSelect = value ? listToDo.Count : 0;
        }

        void ExecuteCommandToggleButtonPress(object obj)
        {
            (obj as Jobs).Available = !(obj as Jobs).Available;
            ToggleButtonChangeState(obj);
            /*var temp = countItemSelect;
            var count = ListToDo.Count;
            var item = obj as Jobs;
            countItemSelect = (item.Available) ? countItemSelect + 1 : countItemSelect - 1;
            if(countItemSelect==count||temp==count&&countItemSelect<count)
            {
                ExecuteCommandBtnDeleteAllChangeState();
            }*/
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
            var strConn = Xamarin.Forms.DependencyService.Get<IDirectoryHelper>().GetDBAddress("data.db");
            using (var db = new LiteDatabase(strConn))
            {
                var jobCol = db.GetCollection<Jobs>();
                if (IsCheckBtnDeleteAll)
                {
                    ListToDo.Clear();
                    jobCol.Delete(x => x != null);
                }
                else
                {
                    var count = ListToDo.ToList().Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (ListToDo[i].Available)
                        {
                            jobCol.Delete(x => x.Id == listToDo.ElementAt(i).Id);
                            ListToDo.RemoveAt(i);

                            i--;
                            count--;
                        }
                    }
                }
            }

        }

        async Task ExecuteCommandItemPressedAsync(object element)
        {
            var item = (element as ListView).SelectedItem as Jobs;
            //item.Available = !item.Available;
            if (IsDeleteMode)
            {
                ToggleButtonChangeState(item);
                /*item.Available = !item.Available;
                countItemSelect = (item.Available) ? countItemSelect + 1 : countItemSelect - 1;*/
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

        /// <summary>
        /// Only apply for Wpf
        /// </summary>
        /// <param name="parameter"></param>
        void ExecuteCommandLsvToDoSizeChanged(Element parameter)
        {

            if (Device.RuntimePlatform.Equals(Device.WPF))
            {
                if (parameter is ListView)
                {
                    var temp = (parameter as ListView).ItemTemplate;
                    (parameter as ListView).ItemTemplate = null;
                    (parameter as ListView).ItemTemplate = temp;
                }

            }

        }
        #endregion
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                try
                {
                    var strConn = Xamarin.Forms.DependencyService.Get<IDirectoryHelper>().GetDBAddress("data.db");
                    if (parameters.GetValue<string>("From").Equals("DetailPage"))
                    {
                        using (var db = new LiteDatabase(strConn))
                        {
                            var jobCol = db.GetCollection<Jobs>();
                            var index = (int)parameters["id"];
                            var item = parameters["item"] as Jobs;
                            if (parameters["action"].ToString().Equals("update"))
                            {
                                if (!ListToDo[index].Equals(item))
                                {
                                    var temp = ListToDo[index];
                                    var getAllProperties = item.GetType().GetProperties();
                                    var job = jobCol.Find(x => x.Id == item.Id).FirstOrDefault();
                                    foreach (var property in getAllProperties)
                                    {
                                        temp.GetType().GetProperty(property.Name).SetValue(temp, property.GetValue(item, null));
                                        job.GetType().GetProperty(property.Name).SetValue(job, property.GetValue(item, null));
                                    }
                                }
                            }
                            else if (parameters["action"].ToString().Equals("insert"))
                            {
                                jobCol.Insert(item);
                                ListToDo.Add(item);
                            }
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


    }
}
