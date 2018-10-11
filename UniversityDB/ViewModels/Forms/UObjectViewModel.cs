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

        public UObjectViewModel(int id)
        {
            db = new UniversityContext();
            Info = db.Objects.Where(o => o.Id == id).SingleOrDefault();

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        private void Save(object parametr)
        {
            db.SaveChanges();
            MessageBox.Show("Успішно збережено!", "Важливе повідомлення");
        }

        private void Cancel(object parametr)
        {
            Window currentWindow = (Window)parametr;
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
