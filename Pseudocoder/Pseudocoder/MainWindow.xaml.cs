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

namespace Pseudocoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            String inputCode = new TextRange(javaText.Document.ContentStart, javaText.Document.ContentEnd).Text;
            inputCode = inputCode.Replace(@":", "->");
            inputCode = inputCode.Replace(@"{", string.Empty);
            inputCode = inputCode.Replace(@"}", "end");
            inputCode = inputCode.Replace(@"().", ": ");
            inputCode = inputCode.Replace(@"()", " ");
            inputCode = inputCode.Replace(@"(", ": ");
            inputCode = inputCode.Replace(@")", " ");
            inputCode = inputCode.Replace(@";", string.Empty);
            inputCode = inputCode.Replace(@"private static void main", "main method");
            inputCode = inputCode.Replace(@"private", "field");
            inputCode = inputCode.Replace(@"public class", "class");
            inputCode = inputCode.Replace(@"public void", "method");
            inputCode = inputCode.Replace(@"public int", "method");
            inputCode = inputCode.Replace(@"public String", "method");
            inputCode = inputCode.Replace(@"public Room", "method");
            inputCode = inputCode.Replace(@"public Player", "method");
            inputCode = inputCode.Replace(@"System.out.println", "print");
            inputCode = inputCode.Replace(@"public static void", "method");
            inputCode = inputCode.Replace(@"public static int", "method");
            inputCode = inputCode.Replace(@".", ": ");
            inputCode = inputCode.Replace(@"&&", "and");








            pseudoText.Document.Blocks.Clear();
            pseudoText.AppendText(inputCode.Replace(@"{", string.Empty));


        }
    }
}
