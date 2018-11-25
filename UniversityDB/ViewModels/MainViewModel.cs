using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UniversityDB.Infrastructure;
using UniversityDB.Models;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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

        private UniversityContext db;

        public MainViewModel()
        {
            ExpandCommand = new Command(Expand);
            YourCommand = new Command(Your);
            db = new UniversityContext();

            //using (var db = new UniversityContext())
            //{
            //    db.Classes.Add(new SClass("UObject", "Об'єкт", "UObjectWindow"));
            //    db.Classes.Add(new SClass("UPerson", "Особу", "UPersonWindow"));
            //    db.Classes.Add(new SClass("UStudyingPerson", "Особу, яка вчиться", "UStudyingPersonWindow"));
            //    db.Classes.Add(new SClass("UWorkingPerson", "Особу, яка працює", "UWorkingPersonWindow"));

            //    db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 1 });
            //    db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 2 });
            //    db.ClassesRules.Add(new SClassRules() { ClassId = 2, ClassIdInside = 3 });
            //    db.ClassesRules.Add(new SClassRules() { ClassId = 2, ClassIdInside = 4 });

            //    UObject fpmi = new UObject("ФПМІ", 1);
            //    fpmi.Childrens = new ObservableCollection<UObject>();
            //    fpmi.Childrens.Add(new UObject("Деканат", 1)
            //    {
            //        Childrens = new ObservableCollection<UObject>()
            //        {
            //            new UObject("Декан", 1)
            //            {
            //                Childrens = new ObservableCollection<UObject>()
            //                {
            //                    new UPerson("Дияк І.І.", Convert.ToDateTime("1987-09-05"),"Любінська 10, кв 4",2)
            //                }
            //            }
            //        }
            //    }
            //    );
            //    fpmi.Childrens.Add(new UObject("КІС", 1));
            //    fpmi.Childrens.Add(new UObject("КДАІС", 1));
            //    fpmi.Childrens.Add(new UObject("КП", 1));
            //    fpmi.Childrens.Add(new UObject("КПМ", 1));
            //    db.Objects.Add(fpmi);
            //    db.SaveChanges();
            //}

            Faculties = new ObservableCollection<UObject>();

            UObject root = db.Objects.Where(o => o.ParentId == null)
                .Include(o => o.Class)
                .Include(o => o.Class.AllowedChildrens.Select(y => y.ClassInside))
                .Include(o => o.Childrens)
                .FirstOrDefault();
            root.Parent = new UObject("Немає", 1);
            if (root != null)
            {
                Faculties.Add(root);
                if (root.Childrens.Any())
                {
                    root.Childrens = new ObservableCollection<UObject> { loadingObject };
                }
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
                    .Include(o => o.Childrens)
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
                            if (child.Childrens.Any())
                            {
                                child.Childrens = new ObservableCollection<UObject> { loadingObject };
                            }
                        }
                    }
                }
            }
        }

        public ICommand YourCommand { get; set; }

        private void Your(object parameter)
        {
            MessageBox.Show("HELLO");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
