using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UniversityDB.Infrastructure;
using UniversityDB.Models;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using UniversityDB.Infrastructure.Enums;
using System.Reflection;
using System.Windows.Controls;
using System;

namespace UniversityDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<UObject> faculties;
        private UObject loadingObject = new UObject("Loading", 1);

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

        public ICommand ExpandCommand { get; set; }
        public ICommand AddCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            ExpandCommand = new Command(Expand);
            AddCommand = new Command(Add);
            ViewCommand = new Command(View);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            //using (var db = new UniversityContext())
            //{
            //    db.Classes.Add(new SClass("UObject", "UObjectWindow"));
            //    db.SaveChanges();

            //    UObject fpmi = new UObject("ФПМІ", 1);
            //    fpmi.Childrens = new ObservableCollection<UObject>();
            //    fpmi.Childrens.Add(new UObject("Деканат", 1)
            //    {
            //        Childrens = new ObservableCollection<UObject>() { new UObject("Декан", 1) }
            //    }
            //    );
            //    fpmi.Childrens.Add(new UObject("КІС", 1));
            //    fpmi.Childrens.Add(new UObject("КДАІС", 1));
            //    fpmi.Childrens.Add(new UObject("КП", 1));
            //    fpmi.Childrens.Add(new UObject("КПМ", 1));
            //    db.Objects.Add(fpmi);
            //    db.SaveChanges();
            //}
            using (var db = new UniversityContext())
            {
                UObject root = db.Objects.FirstOrDefault();
                Faculties = new ObservableCollection<UObject>();
                if (root != null)
                {
                    Faculties.Add(root);
                    root.Childrens = new ObservableCollection<UObject> { loadingObject };
                }
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

        private void Expand(object obj)
        {
            var data = ((obj as RoutedEventArgs).Source as TreeViewItem).DataContext as UObject;
            AppendChildrenByParent(data);
        }

        private void AppendChildrenByParent(UObject parent)
        {
            using (var db = new UniversityContext())
            {
                var children = new ObservableCollection<UObject>(db.Objects.Where(o => o.ParentId == parent.Id).ToList());
                if (parent != null)
                {
                    parent.Childrens.Remove(loadingObject);
                    foreach (var child in children)
                    {
                        if (!parent.Childrens.Any(c => c.Id == child.Id))
                        {
                            parent.Childrens.Add(child);
                            child.Childrens = new ObservableCollection<UObject> { loadingObject };
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
