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
using System.Media;
using System.Windows.Threading;

namespace Alarm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer alarma = new MediaPlayer();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            exit.Opacity = 0.7;
            play.Opacity = 0.7;
            stoop.Opacity = 0.7;
            pause.Opacity = 0.7;
            output.Opacity = 0.8;

            alarma.Open(new Uri("Alarma/alarm.mp3", UriKind.Relative));
            window.Topmost = true;
            
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0,0,1);
            
        }
        int sec, min, hour;
        int sec2, min2, hour2;
        string secc, minn, hourr;
        int count = 0;
        private void timer_tick(object sender, EventArgs e)
        {
            sec++;
            sec2++;
            
            if(sec==60)
            { 
                min++;
                sec = 0;
            }
            if(min==60)
            {
                hour++;
                min = 0;
                count++;
                
                
            }


           // this.WindowState = WindowState.Minimized;
           


            if (sec < 10) secc = 0 + sec.ToString();
            else secc = sec.ToString();
            if (min < 10) minn = 0 + min.ToString();
            else minn = min.ToString();
            if (hour < 10) hourr = 0 + hour.ToString();
            else hourr = hour.ToString();
            output.Content = hourr + ":" + minn + ":" + secc;

            if (hour2==1)
            {
                alarma.Play();
                
                
            }

            if (sec2 == 60)
            {
                min2++;
                sec2 = 0;
            }
            if (min2 == 60)
            {
                hour2++;
                min2 = 0;
                


            }
        }
   



        private void exit_click(object sender, MouseButtonEventArgs e)
        {
            window.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exit_focus(object sender, MouseEventArgs e)
        {
            exit.Opacity = 0.9;
        }

        private void exit_nofocus(object sender, MouseEventArgs e)
        {
            exit.Opacity = 0.7;
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
        }

        private void Button_Click_1(object sender, MouseButtonEventArgs e)
        {
           timer.Stop();
            sec2 = 0;
            min2 = 0;
            hour2 = 0;
        }

        private void Button_Click_2(object sender, MouseButtonEventArgs e)
        {
            
            alarma.Stop();
            hour2 = 0;
        }

        private void play_focus(object sender, MouseEventArgs e)
        {
            play.Opacity = 0.9;
        }

        private void play_nofocus(object sender, MouseEventArgs e)
        {
            play.Opacity = 0.7;
        }

        private void pause_focus(object sender, MouseEventArgs e)
        {
            pause.Opacity = 0.9;
        }

        private void pause_nofocus(object sender, MouseEventArgs e)
        {
            pause.Opacity = 0.7;
        }

        private void stop_focus(object sender, MouseEventArgs e)
        {
            stoop.Opacity = 0.9;
        }

        private void stop_nofocus(object sender, MouseEventArgs e)
        {
            stoop.Opacity = 0.7;
        }

    }
}
