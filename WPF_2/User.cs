using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_2
{
    class User:INotifyPropertyChanged
    {
        private int _id=0;
        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged( );
                }
            }
        }

        private string _name = "Имя пользователя";
        public string Name
        {
            get=> _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged( );
                }
            }
        }

        private string _email = "user@example.com";
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged( );
                }
            }
        }

        private int _age = 18;
        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged( );
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
