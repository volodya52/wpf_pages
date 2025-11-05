using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_2
{
    public class Pacient : INotifyPropertyChanged
    {
        private string _name = "";
        private string _surname = "";
        private string _lastName = "";
        private DateTime _birthday = DateTime.Now;
        private long _phoneNumber;
        private ObservableCollection<Appoitments> _appoitments = new ObservableCollection<Appoitments>();

        public int PacientId
        {
            get;
            set;
        }

        public string PacientName
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientSurname
        {
            get => _surname;
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientLastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime PacientBirthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(IsAdult));
                    OnPropertyChanged(nameof(IsAdultText));
                }
            }
        }

        public long PacientPhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Appoitments> Appoitments
        {
            get => _appoitments;
            set
            {
                if (_appoitments != value)
                {
                    _appoitments = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - PacientBirthday.Year;
                if (PacientBirthday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        [JsonIgnore]
        public bool IsAdult
        {
            get => Age >= 18;
        }

        [JsonIgnore]
        public string IsAdultText
        {
            get => IsAdult ? "Совершеннолетний" : "Несовершеннолетний";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
