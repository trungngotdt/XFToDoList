using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFAppToDoList.CustomControl;
using XFAppToDoList.Wpf;
using Xamarin.Forms.Platform.WPF;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using System.Windows;

[assembly: ExportRenderer(typeof(ToggleButton), typeof(ToggleButtonRendererWpf))]
namespace XFAppToDoList.Wpf
{
    public class ToggleButtonRendererWpf : ViewRenderer<ToggleButton, Button>
    { 
        protected override void OnElementChanged(ElementChangedEventArgs<ToggleButton> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    Button button = new Button();
                    button.Click += (sender,re)=>{ Element.Checked = !Element.Checked;Element.Command.Execute(Element.CommandParameter); };
                    SetNativeControl(button);
                }
                UpdateStyle();
                UpdateColor();
                if((Element.IsSet(ToggleButton.ContentProperty)&&Element.Content!=ToggleButton.ContentProperty.DefaultValue)
                    ||Element.IsSet(ToggleButton.IconProperty)&&!Element.Icon.Equals((string)ToggleButton.IconProperty.DefaultValue))
                {
                    UpdateContent();
                }
                
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            System.Diagnostics.Debug.WriteLine(e.PropertyName);
            if (e.PropertyName==ToggleButton.StyleProperty.PropertyName)
            {
                UpdateStyle();
            }
            else if(e.PropertyName==ToggleButton.ContentProperty.PropertyName || e.PropertyName == ToggleButton.IconProperty.PropertyName)
            {//Control.HorizontalAlignment=HorizontalAlignment
                UpdateContent();
            }
            else if (e.PropertyName == ToggleButton.CheckedProperty.PropertyName ||e.PropertyName==ToggleButton.BackgroundColorProperty.PropertyName)
            {
                UpdateColor();
            }
        }

        void UpdateStyle()
        {
            Control.Style = Element.Style == null ? (Style)Application.Current.Resources["MaterialDesignFloatingActionAccentButton"] :null;
        }
        
        void UpdateColor()
        {
            Control.Background = ((Xamarin.Forms.Color)Element.GetType().GetProperty("FillColor" + (Element.Checked ? "" : "Un") + "Check").GetValue(Element)).ToBrush();
        }

        void UpdateContent()
        {
            var stackContaner = new StackPanel();
            var imageSource = new BitmapImage(new Uri(Element.Icon));
            stackContaner.Children.Add(new Image() { Source = imageSource,Stretch=Stretch.None });
            Control.Content = stackContaner;
        }

    }
}
