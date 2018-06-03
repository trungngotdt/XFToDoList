using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using XFAppToDoList.Models;

namespace XFAppToDoList.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private string content;
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Content = "App demo";
        }

        public string Content { get => content; set => content = value; }
    }
}
