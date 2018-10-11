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
using System.Windows.Controls;

namespace UniversityDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<UObject> faculties;

        // holds reference to the object, on expand every element will be 
        // added to this list and to children of the parent element. allObject is used
        // to avoid searching object with specific id in the tree structure
        // when doing specific operations by id
        private List<UObject> allObjects = new List<UObject>();

        // used to make every tree node expandable, will be replaced with real objects
        // as soon as they will be got from the DB
        private UObject dummyLoadingObject = new UObject("Loading", 0);

        public List<UObject> Faculties
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

        public ICommand ExpandingCommand { get; set; }

        public MainViewModel()
        {
            ExpandingCommand = new Command(ExecuteExpandingCommand);
            //using (var db = new UniversityContext())
            //{
            //    UObject fpmi = new UObject("ФПМІ", 1);
            //    fpmi.Childrens = new List<UObject>();
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
                UObject root = db.Objects.FirstOrDefault();
                Faculties = new List<UObject>();
                if (root != null)
                {
                    Faculties.Add(root);
                    allObjects.Add(root);
                    root.Childrens = new List<UObject> { dummyLoadingObject };
                }
            }
        }

        // Should be moved out of here
        private void ExecuteExpandingCommand(object obj)
        {
            var args = obj as RoutedEventArgs;
            var treeViewItem = args.Source as TreeViewItem;
            var data = treeViewItem.DataContext as UObject; // Here is all we need
            var id = data.Id;
            AppendChildrenByParentId(id);
        }

        //Used to get objects on expanding event, from event 
        private void AppendChildrenByParentId(int parentId)
        {
            using (var db = new UniversityContext())
            {
                var children = db.Objects.Where(o => o.ParentId == parentId).ToList();
                var parent = allObjects.Where(o => o.Id == parentId).FirstOrDefault();
                if (parent != null && children.Count > 0)
                {
                    parent.Childrens = children;
                    allObjects.AddRange(children);
                }
                else if (parent != null)
                {
                    parent.Childrens = null;
                }
                // ui isn't updating
                OnPropertyChanged(nameof(Faculties));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
