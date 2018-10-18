using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UniversityDB.Infrastructure;
using UniversityDB.Infrastructure.Enums;
using UniversityDB.Models;

namespace UniversityDB.ViewModels
{
    public class UObjectViewModel : INotifyPropertyChanged
    {
        private readonly UniversityContext db;
        private UObject current;
        private bool isReadOnly;
        private UObject Parent { get; set; }
        private FormType Type { get; set; }
        public UObject Current
        {
            get
            {
                return current;
            }
            set
            {
                OnPropertyChanged(nameof(Current));
                current = value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;

            }
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public UObjectViewModel()
        {
            db = new UniversityContext();
            IsReadOnly = false;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        //View and Edit
        public UObjectViewModel(UObject elem, FormType type)
        {
            db = new UniversityContext();
            Type = type;
            Current = elem;

            IsReadOnly = (Type == FormType.View) ? true : false;

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        //Adding
        public UObjectViewModel(UObject elem, FormType type, string elemName)
        {
            db = new UniversityContext();
            Type = type;
            Parent = elem;

            Type objectType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Models.{elemName}");
            Current = (UObject)Activator.CreateInstance(objectType);
            Current.Parent = elem;
            Current.ParentId = elem.Id;
            SClass classInfo = db.Classes.Where(c => c.Name == elemName).SingleOrDefault();
            Current.Class = classInfo;
            Current.ClassId = classInfo.Id;
            IsReadOnly = (Type == FormType.View) ? true : false;

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        private void Save(object parameter)
        {
            if (Type == FormType.Add)
            {
                //Saving to DB
                Current.Class = null;
                Current.Parent = null;
                db.Objects.Add(Current);
                db.SaveChanges();

                //Selecting From DB
                if (Parent.Childrens == null)
                {
                    Parent.Childrens = new ObservableCollection<UObject>();
                }
                UObject objectFromDb = db.Objects.Where(o => o.Name == Current.Name && o.ParentId == Parent.Id)
                                                 .Include(o => o.Class)
                                                 .SingleOrDefault();
                //Fix deleting bug
                objectFromDb.Parent = Parent;
                Parent.Childrens.Add(objectFromDb);
            }
            else if (Type == FormType.Edit)
            {
                UObject objectFromDb = db.Objects.Where(o => o.Id == Current.Id).SingleOrDefault();
                Current.CopyPropertiesTo(objectFromDb);
                db.SaveChanges();
            }
            CloseWindow((Window)parameter);
        }

        private void Cancel(object parameter)
        {
            //Update name if it was changed but not saved to Db
            if (Type == FormType.Edit)
            {
                Current.Name = db.Objects.Where(o => o.Id == Current.Id)
                                .Include(o => o.Parent)
                                .Include(o => o.Class)
                                .SingleOrDefault().Name;
            }
            CloseWindow((Window)parameter);
        }

        private void CloseWindow(Window currentWindow)
        {
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
