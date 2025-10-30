using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_2
{
    class PacientEdit:INotifyPropertyChanged
    {
        private string _name = "";
        private string _surname = "";
        private string _lastName = "";
        private string _birthday = "";
        private string _lastAppoitment = "";
        private string _doctor = "";
        private string _diagnosis = "";
        private string _recomendation = "";

        
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

        public string PacientBirthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientLastAppoitment
        {
            get => _lastAppoitment;
            set
            {
                if (_lastAppoitment != value)
                {
                    _lastAppoitment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientDoctor
        {
            get => _doctor;
            set
            {
                if (_doctor != value)
                {
                    _doctor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientDiagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PacientRecomendation
        {
            get => _recomendation;
            set
            {
                if (_recomendation != value)
                {
                    _recomendation = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
