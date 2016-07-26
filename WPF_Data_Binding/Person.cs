using System;
using System.ComponentModel;

namespace WPF_Data_Binding
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                if (propertyName == "Alter")
                {
                    if (_Alter < 0)
                        return "Das Alter darf nicht negativ sein.";
                }
                return null;
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private bool _Rente;
        public bool Rente { get { return _Rente; } }

        private int? _Alter;
        public int? Alter
        {
            get { return _Alter; }
            set
            {
                /*if (value < 0)
                    throw new ArgumentException("Das Alter kann nicht negativ sein.");
                else*/
                    _Alter = value;
                if (Alter >= 65) _Rente = true;
                else _Rente = false;
            }
        }

        private string _Adresse;
        public string Adresse
        {
            get { return _Adresse; }
            set
            {
                _Adresse = value;
                OnPropertyChanged(new PropertyChangedEventArgs(null));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
