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
using System.Data.Entity;

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

        private UniversityContext db;

        public MainViewModel()
        {
            ExpandCommand = new Command(Expand);
            AddCommand = new Command(Add);
            ViewCommand = new Command(View);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            db = new UniversityContext();

            //db.Classes.Add(new SClass("UObject", "UObjectWindow"));
            //db.Classes.Add(new SClass("UPerson", "UPersonWindow"));
            //db.Classes.Add(new SClass("UStudyingPerson", "UStudyingPersonWindow"));
            //db.Classes.Add(new SClass("UWorkingPerson", "UWorkingPersonWindow"));
            //db.SaveChanges();

            //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 1 });
            //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 2 });
            //db.ClassesRules.Add(new SClassRules() { ClassId = 2, ClassIdInside = 3 });
            //db.ClassesRules.Add(new SClassRules() { ClassId = 2, ClassIdInside = 4 });
            //db.SaveChanges();

            //UObject fpmi = new UObject("ФПМІ", 1);
            //fpmi.Childrens = new ObservableCollection<UObject>();
            //fpmi.Childrens.Add(new UObject("Деканат", 1)
            //{
            //    Childrens = new ObservableCollection<UObject>()
            //        {
            //            new UObject("Декан", 1)
            //            {
            //                Childrens = new ObservableCollection<UObject>()
            //                {
            //                    new UPerson("Дияк І.І.", Convert.ToDateTime("1987-09-05"),"Любінська 10, кв 4",2)
            //                }
            //            }
            //        }
            //}
            //);
            //fpmi.Childrens.Add(new UObject("КІС", 1));
            //fpmi.Childrens.Add(new UObject("КДАІС", 1));
            //fpmi.Childrens.Add(new UObject("КП", 1));
            //fpmi.Childrens.Add(new UObject("КПМ", 1));
            //db.Objects.Add(fpmi);
            //db.SaveChanges();

            Faculties = new ObservableCollection<UObject>();

            UObject root = db.Objects.Where(o => o.ParentId == null)
                .Include(o => o.Class)
                .Include(o => o.Class.AllowedChildrens.Select(y => y.ClassInside))
                .FirstOrDefault();
            root.Parent = new UObject("Немає", 1);
            if (root != null)
            {
                Faculties.Add(root);
                root.Childrens = new ObservableCollection<UObject> { loadingObject };
            }
        }

        private void Expand(object obj)
        {
            var data = ((obj as RoutedEventArgs).Source as TreeViewItem).DataContext as UObject;
            AppendChildrenByParent(data);
        }

        private void AppendChildrenByParent(UObject parent)
        {
            using (UniversityContext db = new UniversityContext())
            {
                var children = new ObservableCollection<UObject>(db.Objects.Where(o => o.ParentId == parent.Id)
                    .Include(o => o.Class)
                    .Include(o => o.Class.AllowedChildrens.Select(y => y.ClassInside))
                    .ToList());
                if (parent != null)
                {
                    parent.Childrens.Remove(loadingObject);
                    foreach (var child in children)
                    {
                        if (!parent.Childrens.Any(c => c.Id == child.Id))
                        {
                            child.Parent = parent;
                            parent.Childrens.Add(child);
                            child.Childrens = new ObservableCollection<UObject> {loadingObject};
                        }
                    }
                }
            }
        }

        private void View(object parameter)
        {
            string className = parameter.GetType().Name;
            string formName = "UObjectWindow";
            string formNameFromDb = db.Classes.Where(c => c.Name == className).SingleOrDefault().FormName;
            if (formNameFromDb != null)
            {
                formName = formNameFromDb;
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[2] { parameter as UObject, FormType.View });
            form.Show();
        }

        private void Edit(object parameter)
        {
            string className = parameter.GetType().Name;
            string formName = "UObjectWindow";
            string formNameFromDb = db.Classes.Where(c => c.Name == className).SingleOrDefault().FormName;
            if (formNameFromDb != null)
            {
                formName = formNameFromDb;
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[2] { parameter as UObject, FormType.Edit });
            form.Show();
        }

        private void Add(object parameter)
        {
            object[] parameters = (object[])parameter;
            UObject uObject = (UObject)parameters[0];
            string className = (string)parameters[1];
            string formName = "UObjectWindow";
            string formNameFromDb = db.Classes.Where(c => c.Name == className).SingleOrDefault().FormName;
            if (formNameFromDb != null)
            {
                formName = formNameFromDb;
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[3] { uObject, FormType.Add, className });
            form.Show();
        }

        private void Delete(object parameter)
        {
            UObject current = parameter as UObject;
            if (MessageBox.Show($"Ви впевнені що хочете видалити \"{current.Name}\"?\nВидалення призведе до знищення всіх похідних обєктів!",
                    "Підтвердження",
                    MessageBoxButton.OK,
                    MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                RecursiveDeleteFromDb(current.Id);
                db.Objects.Remove(db.Objects.Where(o => o.Id == current.Id).SingleOrDefault());
                db.SaveChanges();
                RecursiveDeleteFromUI(current);
                current.Parent.Childrens.Remove(current);
            }
        }

        private void RecursiveDeleteFromUI(UObject root)
        {
            for(int i = root.Childrens.Count-1; i>=0; --i)
            {
                RecursiveDeleteFromUI(root.Childrens[i]);
                root.Childrens.Remove(root.Childrens[i]);
            }
        }

        private void RecursiveDeleteFromDb(int rootId)
        {
            UObject root = db.Objects.Where(o => o.Id == rootId).Include(o => o.Childrens).SingleOrDefault();
            for (int i = root.Childrens.Count - 1; i >= 0; --i)
            {
                RecursiveDeleteFromDb(root.Childrens[i].Id);
                db.Objects.Remove(root.Childrens[i]);
            }
            db.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
