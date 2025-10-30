using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode; 

namespace WPF_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private Doctor doctor = new Doctor( );
        public Registration()
        {
            
            InitializeComponent();
            DataContext = doctor;
        }

        private void RegistrationButton (object sender, EventArgs e)
        {
            int a = 0;
            Random rnd = new Random( );
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            a = rnd.Next(10000, 1000000);
            string jsonString = JsonSerializer.Serialize(doctor, options);
            File.WriteAllText($"D_{doctor.DoctorId}.json", jsonString);
            doctor.IsLoggedIn = true;
            MessageBox.Show($"Регистрация успешна.\nID:{doctor.DoctorId}");
            NavigationService.Navigate(new MainPage(doctor));

        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage( ));
        }
    }
}
