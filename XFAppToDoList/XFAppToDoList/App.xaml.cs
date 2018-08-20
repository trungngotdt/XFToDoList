using Prism;
using Prism.Ioc;
using XFAppToDoList.ViewModels;
using XFAppToDoList.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using XDependencyService = Xamarin.Forms.DependencyService;
using XFAppToDoList.MyUtilities;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFAppToDoList
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //System.Diagnostics.Debug.WriteLine(XDependencyService.Get<IDirectoryHelper>().CreateFolder(Application.Current.ToString(), EFolders.None));
            //System.Diagnostics.Debug.WriteLine(XDependencyService.Get<IDirectoryHelper>().CreateFolder(Application.Current.ToString()+@"/"+EFolders.Data,EFolders.None));
            //System.Diagnostics.Debug.WriteLine(XDependencyService.Get<IDirectoryHelper>().CreateFolder(Application.Current.ToString() + @"/"+ EFolders.Log,EFolders.None));
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AboutPage>();
            containerRegistry.RegisterForNavigation<DetailPage>();
        }
    }
}
