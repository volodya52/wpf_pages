using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        Doctor doctor = new Doctor( );
        public SignInPage()
        {
            InitializeComponent();
            DataContext = doctor;
        }

        private void SignIn (object sender, EventArgs e)
        {
           
            string doctorSignIn = $"D_{doctor.DoctorId}.json";
            string doctorJsonString = File.ReadAllText(doctorSignIn);
            var doctorJsonRead = JsonSerializer.Deserialize<Doctor>(doctorJsonString);
            if (doctorJsonRead.DoctorPassword == doctor.DoctorPassword)
            {

                doctor.DoctorName = doctorJsonRead.DoctorName;
                doctor.DoctorSurname = doctorJsonRead.DoctorSurname;
                doctor.DoctorLastName = doctorJsonRead.DoctorLastName;
                doctor.DoctorSpeciality = doctorJsonRead.DoctorSpeciality;
                doctor.IsLoggedIn = true;
                MessageBox.Show($"Добро пожаловать, {doctor.DoctorName} {doctor.DoctorSurname}.");
                NavigationService.Navigate(new MainPage(doctor));
            }
            else
            {
                MessageBox.Show("Неверный пароль.");
            }
        }
    }
}
