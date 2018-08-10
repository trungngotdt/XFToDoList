using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using XFAppToDoList.CustomControl;
using XFAppToDoList.UWP;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using Windows.UI.Xaml;

[assembly: ExportRenderer(typeof(ToggleButton), typeof(ToggleButtonRenderer))]
namespace XFAppToDoList.UWP
{
    public class ToggleButtonRenderer : ViewRenderer<ToggleButton, FormsButton>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ToggleButton> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    FormsButton button = new FormsButton();
                    SetNativeControl(button);
                }
                UpdateTemplate();
            }


        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Debug.WriteLine(e.PropertyName);
            if (e.PropertyName == ToggleButton.CheckedProperty.PropertyName||e.PropertyName == ToggleButton.IconProperty.PropertyName)
            {
                UpdateTemplate();
            }
        }

        void UpdateTemplate()
        {
            var fillColorProperty = (Xamarin.Forms.Color)Element.GetType().GetProperty("FillColor" + (Element.Checked ? "" : "Un") + "Check").GetValue(Element);
            var color = new Windows.UI.Color
            {
                A = Convert.ToByte(fillColorProperty.A * 255),
                R = Convert.ToByte(fillColorProperty.R * 255),
                G = Convert.ToByte(fillColorProperty.G * 255),
                B = Convert.ToByte(fillColorProperty.B * 255)
            };
            SetControlTemplate(color.ToString(), Element.Icon);
        }

        void SetControlTemplate(string color,string imageSource)
        {
            string template = "<ControlTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' TargetType=\"Button\">" +
                "<Grid><Ellipse Fill ="+ "\"" + color+ "\"" + "/><Ellipse Stroke =\"Black\" StrokeThickness=\"0.5\">" +
                "<Ellipse.Fill><ImageBrush Stretch=\"None\" ImageSource = " + "\""+imageSource+ "\"" + "/></Ellipse.Fill ></Ellipse></Grid>" +
                "</ControlTemplate>";
            Control.Template = (ControlTemplate)Windows.UI.Xaml.Markup.XamlReader.Load(template);
        }
    }
}
