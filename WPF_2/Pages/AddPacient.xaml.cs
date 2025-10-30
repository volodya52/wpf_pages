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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.ObjectModel;

namespace WPF_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPacient.xaml
    /// </summary>
    public partial class AddPacient : Page
    {
        private ObservableCollection<Pacient> _pacient { get; set; }=new();
        public Pacient CurrentPacient { get; set; } = new Pacient( );
        public AddPacient(ObservableCollection<Pacient>pacients)
        {
            InitializeComponent();
            _pacient = pacients;
            DataContext = CurrentPacient;
            CurrentPacient.Appoitments = new ObservableCollection<Appoitments>( );
        }

        private void ButtonAddPacient(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random( );
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            CurrentPacient.PacientId = rnd.Next(10000, 1000000);
            string jsonString = JsonSerializer.Serialize(CurrentPacient, options);
            File.WriteAllText($"P_{CurrentPacient.PacientId}.json", jsonString);
            MessageBox.Show($"Пациент добавлен.\nID:{CurrentPacient.PacientId}");
            _pacient.Add(CurrentPacient);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
