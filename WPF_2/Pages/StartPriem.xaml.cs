using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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

namespace WPF_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPriem.xaml
    /// </summary>
    public partial class StartPriem : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Pacient> _pacients;
        private Pacient _currentPatient;
        private Appoitments _newAppointment = new Appoitments();
        private ObservableCollection<Appoitments> _appointments = new ObservableCollection<Appoitments>();

        public Pacient CurrentPatient
        {
            get => _currentPatient;
            set
            {
                _currentPatient = value;
                OnPropertyChanged();
            }
        }

        public Appoitments NewAppointment
        {
            get => _newAppointment;
            set
            {
                _newAppointment = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Appoitments> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged();
            }
        }

        public Doctor Doctor { get; set; }

        public StartPriem(ObservableCollection<Pacient> patients, Pacient patient, Doctor doctor)
        {
            InitializeComponent();
            _pacients = patients;
            CurrentPatient = patient;
            DataContext = this;
            Doctor = doctor;

            
            NewAppointment.Date = DateTime.Now;
            NewAppointment.DoctorId = doctor.DoctorId.ToString();

            LoadAppointments();
        }

        private void LoadAppointments()
        {
            if (CurrentPatient?.Appoitments != null)
            {
                Appointments = new ObservableCollection<Appoitments>(CurrentPatient.Appoitments);
            }
            else
            {
                Appointments = new ObservableCollection<Appoitments>();
            }
        }

        private void LoadPatientDataFromFile()
        {
            try
            {
                string fileName = $"P_{CurrentPatient.PacientId}.json";
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    var existingPatient = JsonSerializer.Deserialize<Pacient>(json, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (existingPatient?.Appoitments != null)
                    {
                        CurrentPatient.Appoitments = new ObservableCollection<Appoitments>(existingPatient.Appoitments);
                        Appointments = new ObservableCollection<Appoitments>(CurrentPatient.Appoitments);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(NewAppointment.Diagnosis))
            {
                MessageBox.Show("Введите диагноз");
                return;
            }

            var newAppointment = new Appoitments
            {
                Date = NewAppointment.Date, 
                DoctorId = Doctor.DoctorId.ToString(),
                Diagnosis = NewAppointment.Diagnosis,
                Recomendations = NewAppointment.Recomendations ?? string.Empty,
            };

           
            LoadPatientDataFromFile();

           
            if (CurrentPatient.Appoitments == null)
                CurrentPatient.Appoitments = new ObservableCollection<Appoitments>();

           
            CurrentPatient.Appoitments.Add(newAppointment);
            Appointments.Add(newAppointment);

            MessageBox.Show("Прием добавлен");
            SavePatientData();

            NewAppointment = new Appoitments
            {
                Date = DateTime.Now,
                DoctorId = Doctor.DoctorId.ToString()
            };

           
            OnPropertyChanged(nameof(NewAppointment));
            OnPropertyChanged(nameof(Appointments));
        }

        private void SavePatientData()
        {
            try
            {
                string fileName = $"P_{CurrentPatient.PacientId}.json";
                string json = JsonSerializer.Serialize(CurrentPatient, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RedactPacient(_pacients, CurrentPatient));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

