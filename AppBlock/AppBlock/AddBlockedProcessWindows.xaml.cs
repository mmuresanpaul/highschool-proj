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
using System.Windows.Shapes;

namespace AppBlock
{
    /// <summary>
    /// Interaction logic for AddBlockedProcessWindows.xaml
    /// </summary>
    public partial class AddBlockedProcessWindows : Window
    {
        public String userInput;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

      

        public AddBlockedProcessWindows()
        {
            InitializeComponent();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //this.Focus();
           // this.Activate();
        }

        private void addButtonGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void addButtonGrid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void addButtonGrid_MouseLeave(object sender, MouseEventArgs e)
        {

        }

       
    }
}
