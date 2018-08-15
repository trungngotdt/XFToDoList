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
using System.ComponentModel;
using Android.Graphics;
using CToggleButton = XFAppToDoList.CustomControl.ToggleButton;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(XFAppToDoList.CustomControl.ToggleButton), typeof(ToggleButtonRendererAndroid))]
namespace XFAppToDoList.Droid
{
    public class ToggleButtonRendererAndroid:ViewRenderer<CToggleButton, ButtonRenderer>
    {
        private GradientDrawable _gradientBackground;

        public ToggleButtonRendererAndroid() : base()
        {

        }

        public ToggleButtonRendererAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CToggleButton> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Only enable hardware accelleration on lollipop
                if ((int)Android.OS.Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    ButtonRenderer button = new ButtonRenderer();
                    
                    SetNativeControl(button);
                    Paint(button);
                }
            }
        }
        private void Paint(ButtonRenderer view)
        {
            _gradientBackground = new GradientDrawable();
            _gradientBackground.SetShape(ShapeType.Rectangle);
            _gradientBackground.SetColor(Android.Graphics.Color.Bisque);
            // Thickness of the stroke line  
            _gradientBackground.SetStroke(1, Android.Graphics.Color.Red);
            // Radius for the curves  
            _gradientBackground.SetCornerRadius(20);
            // set the background of the label  
            Control.SetBackground(_gradientBackground);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName ==CToggleButton.BackgroundColorProperty.PropertyName)
            {
                this.Invalidate();
            }
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
        }

        /*
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            var path = new Path();
            var radius = (float)Math.Min(Width, Height) / 2f;
            path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);


            canvas.Save();
            canvas.ClipPath(path);
        }*/



        /*
        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {

                var radius = (float)Math.Min(Width, Height) / 2f;

                var borderThickness = 0; //(Element).BorderWidth;

                float strokeWidth = 0f;

                if (borderThickness > 0)
                {
                    var logicalDensity = Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (float)Math.Ceiling(borderThickness * logicalDensity + .5f);
                }

                radius -= strokeWidth / 2f;




                var path = new Path();
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);


                canvas.Save();
                canvas.ClipPath(path);



                var paint = new Paint
                {
                    AntiAlias = true
                };
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = (Element).BackgroundColor.ToAndroid();
                canvas.DrawPath(path, paint);
                paint.Dispose();


                var result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2f, Height / 2f, radius, Path.Direction.Ccw);


                if (strokeWidth > 0.0f)
                {
                    paint = new Paint
                    {
                        AntiAlias = true,
                        StrokeWidth = strokeWidth
                    };
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = (Element).BackgroundColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }*/
    }

}