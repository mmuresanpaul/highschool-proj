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
using System.Diagnostics;

namespace AppBlock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public static TimeSlot allocatedTimeCopy = new TimeSlot(new Time(11,34, 0), new Time(12, 21, 0));
        public Processes processes = new Processes(allocatedTimeCopy);

        AddBlockedProcessWindows addWindows = new AddBlockedProcessWindows();

        
        
        public void changeThemeColour(byte r, byte g, byte b)
        {
            addButtonBorder.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            StateLabel.Foreground = new SolidColorBrush(Color.FromRgb(r, g, b));
            IconEllipse.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public void addItemsToList()
        {
            blockedAppsList.Items.Clear();
            blockedAppsList.Items.Add("Time Slot: " + processes.getAllocatedTime().getFromTime().getTime() + "->" + processes.getAllocatedTime().getToTime().getTime());
            for (int i = 0; i < processes.getBlockedProcessesNo(); i++)
                blockedAppsList.Items.Add(processes.getBlockedProcessName(i));
        }

        public MainWindow()
        {
            
            InitializeComponent();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
          

            processes.addBlockedProcess("lol.launcher");
            processes.addBlockedProcess("steam");
            processes.addBlockedProcess("battle.net");
            processes.addBlockedProcess("origin");

            FileManipulation data = new FileManipulation(allocatedTimeCopy, processes);
            processes.setAllocatedTime(data.getTimeSlot());
            processes = data.getProcesses();

            addItemsToList();
        }


        #region TIMER
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            /*
            //Blocks all windows
             if(DateTime.Now.DayOfWeek.ToString() != "Sunday" && DateTime.Now.DayOfWeek.ToString() != "Friday")
                    System.Diagnostics.Process.Start("shutdown.exe", "-r -t 5");

            debugg.Content = (DateTime.Now.DayOfWeek-1).ToString();
            */
            
            if (Processes.isAllowed(processes.getAllocatedTime().getFromTime(), processes.getAllocatedTime().getToTime()))
            {             
              
                StateLabel.Content = "Unlocked";
                changeThemeColour(0, 122, 255);
                timeUntilLabel.Content = "Time left: " + Time.timeUntil(processes.getAllocatedTime().getToTime());
            }
            else
            {
                StateLabel.Content = "Locked";
                changeThemeColour(255, 59, 48);
                processes.killProcesses();
                timeUntilLabel.Content = "Time left: " + Time.timeUntil(processes.getAllocatedTime().getFromTime());
            }
            

        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        #region ADD BUTTON EVENTS
        private void addButtonGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            addButtonBorder.Opacity = 0.8;
        }

        private void addButtonGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            addButtonBorder.Opacity = 0.9;
        }

        private void addButtonGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /* addWindows.Show();       */
            if (addButtonGrid.Opacity != 0)
            {
                AppUI.showGrid(addProccesGrid);
                AppUI.hideGrid(addButtonGrid);
            }
                        
        }
        #endregion


        private void blockButtonGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppUI.hideGrid(blockButtonGrid);
            processes.addBlockedProcess(inputName.Text);
            addItemsToList();
        }

        #region CANCEL BUTTON EVENTS
        private void cancelButtonGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppUI.hideGrid(addProccesGrid);
            AppUI.showGrid(addButtonGrid);
        }

        private void cancelButtonGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            cancelButtonGrid.Opacity = 0.8;
        }
       
        private void cancelButtonGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            cancelButtonGrid.Opacity = 0.9;
        }
        #endregion

        private void blockButtonGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            blockButtonGrid.Opacity = 0.8;
        }

        private void blockButtonGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            blockButtonGrid.Opacity = 0.9;
        }

       
    }

}

