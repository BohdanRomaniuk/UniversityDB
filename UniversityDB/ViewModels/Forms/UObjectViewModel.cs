using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UniversityDB.Infrastructure;
using UniversityDB.Models;

namespace UniversityDB.ViewModels.Forms
{
    public class UObjectViewModel : INotifyPropertyChanged
    {
        private readonly UniversityContext db;
        private UObject info;
        public UObject Info
        {
            get
            {
                return info;
            }
            set
            {
                OnPropertyChanged(nameof(Info));
                info = value;
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public UObjectViewModel()
        {
            db = new UniversityContext();

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        public UObjectViewModel(UObject elem)
        {
            db = new UniversityContext();
            Info = elem;

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        private void Save(object parameter)
        {
            db.SaveChanges();
            if (MessageBox.Show("Зміни успішно збережено!", "Важливе повідомлення", MessageBoxButton.OK, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
            {
                Cancel(parameter);
            }
        }

        private void Cancel(object parameter)
        {
            Window currentWindow = (Window)parameter;
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
