using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace NOtepad
{
    class Design
    {
          //ROTATION ANIMATIONS
        public static void CanvasAnimateRotation(Canvas obj, double angle)
        {
            

            var da = new DoubleAnimation(0, angle, new Duration(TimeSpan.FromMilliseconds(120)));
            var rt = new RotateTransform();
            obj.RenderTransform = rt;
            obj.RenderTransformOrigin = new Point(0.5, 0.5);
            
            rt.BeginAnimation(RotateTransform.AngleProperty, da);

        }
        public static void EllipseAnimateRotation(Ellipse obj, double angle)
        {


            var da = new DoubleAnimation(0, angle, new Duration(TimeSpan.FromMilliseconds(120)));
            var rt = new RotateTransform();
            obj.RenderTransform = rt;
            obj.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, da);

        }



        //SIZE ANIMATIONS
        public static void RectangleAnimateSize(int Size, Rectangle obj, double Duration, double DecelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.DecelerationRatio = DecelRat;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            if (Size == 1) obj.BeginAnimation(Rectangle.WidthProperty, anim);
            if (Size == 2) obj.BeginAnimation(Rectangle.HeightProperty, anim);
        }
        public static void CanvasAnimateSize(int Size, Canvas obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            if (Size == 1) obj.BeginAnimation(Canvas.WidthProperty, anim);
            if (Size == 2) obj.BeginAnimation(Canvas.HeightProperty, anim);
        }
        public static void EllipseAnimateSize(int Size, Ellipse obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            if (Size == 1)
            {
                obj.BeginAnimation(Ellipse.WidthProperty, anim);

                var left = obj.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                obj.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(left, left-from+to, TimeSpan.FromMilliseconds(200));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (Size == 2)
            {
                obj.BeginAnimation(Ellipse.HeightProperty, anim);

                var top = obj.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                obj.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, top+from-to, TimeSpan.FromMilliseconds(200));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }

           
        }

        //OPACITY ANIMATIONS
        public static void ButtonAnimateOpacity(System.Windows.Controls.Button obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            anim.DecelerationRatio = 1;
            if (to == 0)
                anim.Completed += (s, e) =>
                {
                    obj.Visibility = Visibility.Collapsed;
                };

            obj.BeginAnimation(Canvas.OpacityProperty, anim);

            if (from == 0) obj.Visibility = Visibility.Visible;
        }


        public static void CanvasAnimateOpacity(Canvas obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            anim.DecelerationRatio = 1;
            if (to == 0)
                anim.Completed += (s, e) =>
                {
                    obj.Visibility = Visibility.Collapsed;
                };
            
            obj.BeginAnimation(Canvas.OpacityProperty, anim);

            if (from == 0) obj.Visibility = Visibility.Visible;
        }
        public static void EllipseAnimateOpacity(Ellipse obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            anim.DecelerationRatio = 1;
            if (to == 0)
                anim.Completed += (s, e) =>
                {
                    obj.Visibility = Visibility.Hidden;
                };

            obj.BeginAnimation(Canvas.OpacityProperty, anim);

            if (from == 0) obj.Visibility = Visibility.Visible;
        }
        public static void ImageAnimateOpacity(Image obj, double Duration, double AccelRat, double from, double to)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(Duration));
            anim.DecelerationRatio = AccelRat;

            
            
                anim.Completed += (s, e) =>
                {
                    obj.Opacity = to;
                };

            obj.BeginAnimation(Image.OpacityProperty, anim);

            //if (from == 0) obj.Visibility = Visibility.Visible;
        }
       
        public static void MoveTo(Canvas target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(left, newX, TimeSpan.FromMilliseconds(180));
                anim1.DecelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, newX, TimeSpan.FromMilliseconds(180));
                anim1.DecelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }
                 
            
        }
        public static void ReverseMoveTo(Canvas target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, left, TimeSpan.FromMilliseconds(100));
                anim1.AccelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, top, TimeSpan.FromMilliseconds(100));
                anim1.AccelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }
        }
        public static void EllipseMoveTo(Ellipse target, double newX, int direction)
        {
            if (direction == 1)
            {
                var right = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(right, newX, TimeSpan.FromMilliseconds(100));
                anim1.DecelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, newX, TimeSpan.FromMilliseconds(100));
                anim1.DecelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }


        }
        public static void EllipseReverseMoveTo(Ellipse target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, left, TimeSpan.FromMilliseconds(140));
                anim1.AccelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, top, TimeSpan.FromMilliseconds(140));
                anim1.AccelerationRatio = 1;
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }
        }
        public static void RectangleMoveTo(Rectangle target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(left, newX, TimeSpan.FromMilliseconds(80));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, newX, TimeSpan.FromMilliseconds(120));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }


        }
        public static void RectangleReverseMoveTo(Rectangle target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, left, TimeSpan.FromMilliseconds(60));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, top, TimeSpan.FromMilliseconds(120));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }
        }
        public static void ImageMoveTo(Image target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(left, newX, TimeSpan.FromMilliseconds(80));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(top, newX, TimeSpan.FromMilliseconds(120));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }


        }
        public static void ImageReverseMoveTo(Image target, double newX, int direction)
        {
            if (direction == 1)
            {
                var left = target.Margin.Left;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, left, TimeSpan.FromMilliseconds(80));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
            if (direction == 2)
            {
                var top = target.Margin.Top;
                TranslateTransform trans = new TranslateTransform();
                target.RenderTransform = trans;
                DoubleAnimation anim1 = new DoubleAnimation(newX, top, TimeSpan.FromMilliseconds(120));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }
        }

        public static void IncreasuUpper(Rectangle target, double size)
        {
            RectangleAnimateSize(2, target, 120, 0.5, target.Height, target.Height + size);
            RectangleMoveTo(target,target.Margin.Top-size,2);
        }
        public static void DecreaseUpper(Rectangle target, double size)
        {
            RectangleAnimateSize(2, target, 120, 0.5, target.Height, target.Height-size);
            RectangleReverseMoveTo(target, target.Margin.Top - size, 2);
        }


        public static void mouseOnEllipse(Ellipse firstButton)
        {
            TranslateTransform transFirstButton = new TranslateTransform();
            firstButton.RenderTransform = transFirstButton;
            

            DoubleAnimation moveFirstX = new DoubleAnimation();
            DoubleAnimation moveFirstY = new DoubleAnimation();
            DoubleAnimation accessFirst = new DoubleAnimation();
          
            moveFirstX.From = 0;
            moveFirstX.To = 5;
            moveFirstY.From = 0;
            moveFirstY.To = -5;
            accessFirst.From = 34;
            accessFirst.To = 44;
         
            moveFirstX.DecelerationRatio = 1;
            moveFirstY.DecelerationRatio = 1;
            accessFirst.DecelerationRatio = 1;
          
            moveFirstX.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
            moveFirstY.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
            accessFirst.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
           

            transFirstButton.BeginAnimation(TranslateTransform.XProperty, moveFirstX);
            transFirstButton.BeginAnimation(TranslateTransform.YProperty, moveFirstY);
            firstButton.BeginAnimation(Ellipse.HeightProperty, accessFirst);
            firstButton.BeginAnimation(Ellipse.WidthProperty, accessFirst);
           
        }

        public static void mouseOffEllipse(Ellipse firstButton)
        {
            TranslateTransform transFirstButton = new TranslateTransform();
            firstButton.RenderTransform = transFirstButton;
            TranslateTransform transSecondButton = new TranslateTransform();
          
            DoubleAnimation moveFirstX = new DoubleAnimation();
            DoubleAnimation moveFirstY = new DoubleAnimation();
            DoubleAnimation accessFirst = new DoubleAnimation();

            moveFirstX.From = 5;
            moveFirstX.To = 0;
            moveFirstY.From = -5;
            moveFirstY.To = 0;
            accessFirst.From = 44;
            accessFirst.To = 34;

            moveFirstX.AccelerationRatio = 1;
            moveFirstY.AccelerationRatio = 1;
            accessFirst.AccelerationRatio = 1;
          
            moveFirstX.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
            moveFirstY.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
            accessFirst.Duration = new Duration(TimeSpan.FromSeconds(0.1f));
         

            transFirstButton.BeginAnimation(TranslateTransform.XProperty, moveFirstX);
            transFirstButton.BeginAnimation(TranslateTransform.YProperty, moveFirstY);
            firstButton.BeginAnimation(Ellipse.HeightProperty, accessFirst);
            firstButton.BeginAnimation(Ellipse.WidthProperty, accessFirst);

        }

        public static void RectangleChangeColor(Rectangle rect, String to)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.To = (Color)ColorConverter.ConvertFromString(to);
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            rect.Fill.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }

       public static void EllipseChangeColor(Ellipse rect, String to)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.To = (Color)ColorConverter.ConvertFromString(to);
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            rect.Fill.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }

        public static void GridChangeColor(Grid rect, String to)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.To = (Color)ColorConverter.ConvertFromString(to);
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            rect.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }
        public static void BorderChangeColor(Border rect, String to)
        {
            ColorAnimation animation = new ColorAnimation();
            animation.To = (Color)ColorConverter.ConvertFromString(to);
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            rect.Background.BeginAnimation(SolidColorBrush.ColorProperty , animation);

        }

    }
}
