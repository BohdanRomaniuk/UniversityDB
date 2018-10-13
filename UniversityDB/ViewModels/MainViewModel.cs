using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniversityDB.Infrastructure;
using UniversityDB.Models;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using UniversityDB.Forms;
using UniversityDB.Infrastructure.Enums;
using System.Reflection;
using System.Globalization;

namespace UniversityDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<UObject> faculties;
        public ObservableCollection<UObject> Faculties
        {
            get
            {
                return faculties;
            }
            set
            {
                faculties = value;
                OnPropertyChanged(nameof(Faculties));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            AddCommand = new Command(Add);
            ViewCommand = new Command(View);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            //using (var db = new UniversityContext())
            //{
            //    UObject fpmi = new UObject("ФПМІ", 1);
            //    fpmi.Childrens = new ObservableCollection<UObject>();
            //    fpmi.Childrens.Add(new UObject("Деканат", 1));
            //    fpmi.Childrens.Add(new UObject("КІС", 1));
            //    fpmi.Childrens.Add(new UObject("КДАІС", 1));
            //    fpmi.Childrens.Add(new UObject("КП", 1));
            //    fpmi.Childrens.Add(new UObject("КПМ", 1));
            //    db.Objects.Add(fpmi);
            //    db.SaveChanges();
            //}
            using (var db = new UniversityContext())
            {
                UObject root = db.Objects.Include(o => o.Childrens.Select(e => e.Childrens.Select(a=>a.Childrens))).FirstOrDefault();
                Faculties = new ObservableCollection<UObject>();
                Faculties.Add(root);
            }
        }

        private void View(object parameter)
        {
            string objectTypeName = parameter.GetType().Name;
            string formName = "UObjectWindow";
            using (UniversityContext db = new UniversityContext())
            {
                //not working before merging Romans PR
                //Select FormName of current Object and set it to formName variable
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[2] { parameter as UObject, FormType.View });
            form.Show();
        }

        private void Edit(object parameter)
        {
            string objectTypeName = parameter.GetType().Name;
            string formName = "UObjectWindow";
            using (UniversityContext db = new UniversityContext())
            {
                //not working before merging Romans PR
                //Select FormName of current Object and set it to formName variable
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[2] { parameter as UObject, FormType.Edit });
            form.Show();
        }

        private void Add(object parameter)
        {
            string objectTypeName = parameter.GetType().Name;
            string formName = "UObjectWindow";
            using (UniversityContext db = new UniversityContext())
            {
                //not working before merging Romans PR
                //Select FormName of current Object and set it to formName variable
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[2] { parameter as UObject, FormType.Add });
            form.Show();
        }

        private void Delete(object parameter)
        {
            var elem = parameter as UObject;
            if (MessageBox.Show($"Ви впевнені що хочете видалити \"{elem.Name}\"?\nВидалення призведе до знищення всіх похідних обєктів!", "Підтвердження",
                    MessageBoxButton.OK,
                    MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //not working before merging Romans PR
                var parent = new UObject();// parameter.Parent;
                parent.Childrens.Remove(elem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
