using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace AppBlock
{
    class AppUI : Window
    {
      
        public static void displayAttempt(String appName)
        {
            MessageBox.Show("Learn! App: " + appName + " is not available now.");
        }

      

        public void changeThemeColour(String hexColour)
        {
            
        }

        public static void showGrid(Grid gridName)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            gridName.BeginAnimation(Grid.OpacityProperty, da);
            
        }

        public static void hideGrid(Grid gridName)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            gridName.BeginAnimation(Grid.OpacityProperty, da);

           
        }

    }
}
