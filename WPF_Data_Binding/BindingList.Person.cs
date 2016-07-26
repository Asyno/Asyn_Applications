using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace WPF_Data_Binding
{
    public partial class BindingList : Window
    {
        ObservableCollection<Person> liste = new ObservableCollection<Person>();

        public ObservableCollection<Person> GetPerson()
        {
            liste.Add(new Person { Name = "Schulze", Alter = 56, Adresse = "Bremen" });
            liste.Add(new Person { Name = "Hans", Alter = 30, Adresse = "Hamburg" });
            liste.Add(new Person { Name = "Franz", Alter = 45, Adresse = "Köln" });
            liste.Add(new Person { Name = "Müller", Alter = 26, Adresse = "Hannover" });

            return liste;
        }
    }
}
