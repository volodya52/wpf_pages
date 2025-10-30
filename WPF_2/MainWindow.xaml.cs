using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WPF_2.Pages;

namespace WPF_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {     
        public MainWindow ()
        {
            InitializeComponent( );
            MainFrame.Navigate(new Registration( ));
        }

        private void ChangeThemeMenuItem_Click (object sender, RoutedEventArgs e)
        {
            ThemeHelper.Toggle( );
        }


    }
}