using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFAppToDoList.CustomControl
{
    public class ToggleButton:ContentView
    {
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(propertyName :"Checked",
                returnType: typeof(bool),
                declaringType: typeof(ToggleButton),
                defaultValue: false);

        public static readonly BindableProperty FillColorCheckProperty =
           BindableProperty.Create(propertyName: nameof(FillColorCheck),
             returnType: typeof(Color),
             declaringType: typeof(ToggleButton),
             defaultValue: Color.Transparent);
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ToggleButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ToggleButton), null);
        
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(propertyName :nameof(Icon),
                returnType: typeof(String),
                declaringType: typeof(ToggleButton),
                defaultValue: null);

        public static readonly BindableProperty FillColorUnCheckProperty =
            BindableProperty.Create(propertyName: nameof(FillColorUnCheck),
                returnType: typeof(Color),
                declaringType: typeof(ToggleButton),
                defaultValue: Color.Transparent);


        public String Icon
        {
            get => (String)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public Color FillColorUnCheck
        {
            get { return (Color)GetValue(FillColorUnCheckProperty); }
            set { SetValue(FillColorUnCheckProperty, value); }
        }

        public Color FillColorCheck
        {
            get { return (Color)GetValue(FillColorCheckProperty); }
            set { SetValue(FillColorCheckProperty, value); }
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set {SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        
        private ICommand _toggleCommand;
        public ICommand ToogleCommand
        {
            get
            {
                return _toggleCommand
                ?? (_toggleCommand = new Command(async () =>
                {
                    if (Checked)
                    {
                        Checked = false;
                    }
                    else
                    {
                        Checked = true;
                    }
                    await this.ScaleTo(0.8, 50, Easing.Linear);
                    await System.Threading.Tasks.Task.Delay(500);
                    await this.ScaleTo(1, 50, Easing.Linear);
                    if (Command != null)
                    {
                        Command.Execute(CommandParameter);
                    }
                }));
            }
        }

        public ToggleButton()
        {
            if(Device.RuntimePlatform!=Device.WPF)
            {
                GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = ToogleCommand
                });
            }
            
        }
    }
}
