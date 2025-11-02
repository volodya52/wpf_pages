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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.ObjectModel;

namespace WPF_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для RedactPacient.xaml
    /// </summary>
    public partial class RedactPacient : Page
    {
        private ObservableCollection<Pacient> _pacients;
        public Pacient CurrentPacient { get; set; }
       
        public RedactPacient(ObservableCollection<Pacient> pacients, Pacient pacient)
        {
            InitializeComponent();
            _pacients = pacients;
            CurrentPacient = pacient;
            DataContext = this;


        }

        private void SearchButton(object sender, RoutedEventArgs e)
        {
           
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
           

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(CurrentPacient, options);

            File.WriteAllText($"P_{CurrentPacient.PacientId}.json", json);
            MessageBox.Show($"Данные пациента {CurrentPacient.PacientId} обновлены");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
