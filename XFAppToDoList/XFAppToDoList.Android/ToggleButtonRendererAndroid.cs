using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AButton = Android.Widget.Button;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFAppToDoList.Droid;
using XFAppToDoList.CustomControl;

[assembly: ExportRenderer(typeof(XFAppToDoList.CustomControl.ToggleButton), typeof(ToggleButtonRendererAndroid))]
namespace XFAppToDoList.Droid
{
    public class ToggleButtonRendererAndroid:ViewRenderer<CustomControl.ToggleButton,AButton>
    {

    }
}