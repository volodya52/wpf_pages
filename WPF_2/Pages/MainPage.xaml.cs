using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WPF_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<Pacient> Pacients { get; set; } = new ObservableCollection<Pacient>();
        public Pacient SelectedPatient { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public UsersCount UserCountInfo { get; set; } = new UsersCount();

        public MainPage(Doctor doctor)
        {
            InitializeComponent();
            CurrentDoctor = doctor;
            DataContext = this;
            LoadPacients();
            AllUsers();
        }

        private void LoadPacients()
        {
            Pacients.Clear();
            var patientFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "P_*.json");
            if (patientFiles.Length == 0)
            {
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                patientFiles = Directory.GetFiles(appDir, "P_*.json");
            }

            foreach (var file in patientFiles)
            {
                string json = File.ReadAllText(file);
                var patient = JsonSerializer.Deserialize<Pacient>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                if (patient != null)
                {
                    if (patient.Appoitments == null)
                        patient.Appoitments = new ObservableCollection<Appoitments>();

                    patient.OnPropertyChanged(nameof(patient.Age));
                    patient.OnPropertyChanged(nameof(patient.IsAdult));
                    patient.OnPropertyChanged(nameof(patient.IsAdultText));
                    Pacients.Add(patient);
                }     
            }
                OnPropertyChanged(nameof(Pacients));
        }

        private void AllUsers()
        {
            string searchPath = Directory.GetCurrentDirectory();
            int pacientsCount = Directory.GetFiles(searchPath, "P_*.json").Length;
            int doctorsCount = Directory.GetFiles(searchPath, "D_*.json").Length;
            if (pacientsCount == 0 && doctorsCount == 0)
            {
                searchPath = AppDomain.CurrentDomain.BaseDirectory;
                pacientsCount = Directory.GetFiles(searchPath, "P_*.json").Length;
                doctorsCount = Directory.GetFiles(searchPath, "D_*.json").Length;
            }
                UserCountInfo.Count = pacientsCount + doctorsCount;
                OnPropertyChanged(nameof(UserCountInfo));
        }

        private void ButtonAddPacient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPacient(Pacients));
        }

        private void RedactButton(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                NavigationService.Navigate(new RedactPacient(Pacients, SelectedPatient));
            }
            else
            {
                MessageBox.Show("Выберите пациента для редактирования");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPriem(Pacients,SelectedPatient,CurrentDoctor));
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new StartPriem(Pacients, SelectedPatient,CurrentDoctor));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }
    }
}
