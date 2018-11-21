using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UniversityDB.Infrastructure;
using UniversityDB.Infrastructure.Enums;

namespace UniversityDB.Models
{
    [Table("Objects")]
    public class UObject : INotifyPropertyChanged
    {
        private string name;
        private int classId;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [Required]
        public int ClassId
        {
            get
            {
                return classId;
            }
            set
            {
                classId = value;
                OnPropertyChanged(nameof(ClassId));
                CreateActions();
            }
        }
        [ForeignKey("ClassId")]
        public SClass Class { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public UObject Parent { get; set; }

        public ObservableCollection<UObject> Childrens { get; set; }

        [NotMapped]
        private UniversityContext db = new UniversityContext();
        [NotMapped]
        public ObservableCollection<ContextAction> Actions { get; set; }

        public UObject()
        {
            Childrens = new ObservableCollection<UObject>();
        }

        public UObject(string _name, int _classId)
        {
            Name = _name;
            ClassId = _classId;
            Childrens = new ObservableCollection<UObject>();
        }

        protected virtual void CreateActions()
        {
            Actions = new ObservableCollection<ContextAction>
            {
                new ContextAction() { Name = "Переглянути", Action = new Command(View) }
            };
            if (ClassId != 0)
            {
                var allowedChilds = db.Classes.Include(o => o.AllowedChildrens.Select(y => y.ClassInside))
                                              .Where(c => c.Id == ClassId)
                                              .SingleOrDefault()
                                              .AllowedChildrens;
                if(allowedChilds != null)
                {
                    Actions.Add(new ContextAction()
                    {
                        Name = "Додати",
                        Action = null,
                        Subs = allowedChilds.Select(e => new ContextAction() { Name = e.ClassInside.UkrName, Action = new Command(Add) }).ToList()
                    });
                }
            }
            Actions.Add(new ContextAction() { Name = "Редагувати", Action = new Command(Edit) });
            Actions.Add(new ContextAction() { Name = "Видалити", Action = new Command(Delete) });
        }

        private void View(object parameters)
        {
            object parameter = ((object[])parameters)[0];
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

        private void Edit(object parameters)
        {
            object parameter = ((object[])parameters)[0];
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

        private void Add(object parameters)
        {
            object[] parameter = (object[])parameters;
            UObject uObject = (UObject)parameter[0];
            string className = (string)parameter[1];
            string formName = "UObjectWindow";
            var classFromDb = db.Classes.Where(c => c.UkrName == className).SingleOrDefault();
            string formNameFromDb = classFromDb.FormName;
            className = classFromDb.Name;
            if (formNameFromDb != null)
            {
                formName = formNameFromDb;
            }
            Type formType = Assembly.GetExecutingAssembly().GetType($"UniversityDB.Forms.{formName}");
            Window form = (Window)Activator.CreateInstance(formType, new object[3] { uObject, FormType.Add, className });
            form.Show();
        }

        private void Delete(object parameters)
        {
            UObject current = ((object[])parameters)[0] as UObject;
            if (MessageBox.Show($"Ви впевнені що хочете видалити \"{current.Name}\"?\nВидалення призведе до знищення всіх похідних обєктів!",
                    "Підтвердження",
                    MessageBoxButton.OK,
                    MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                using (UniversityContext db = new UniversityContext())
                {
                    RecursiveDeleteFromDb(current.Id);
                    db.Objects.Remove(db.Objects.Where(o => o.Id == current.Id).SingleOrDefault());
                    db.SaveChanges();
                }
                RecursiveDeleteFromUI(current);
                current.Parent.Childrens.Remove(current);
            }
        }

        private void RecursiveDeleteFromUI(UObject root)
        {
            for (int i = root.Childrens.Count - 1; i >= 0; --i)
            {
                RecursiveDeleteFromUI(root.Childrens[i]);
                root.Childrens.Remove(root.Childrens[i]);
            }
        }

        private void RecursiveDeleteFromDb(int rootId)
        {
            using (UniversityContext db = new UniversityContext())
            {
                UObject root = db.Objects.Where(o => o.Id == rootId).Include(o => o.Childrens).SingleOrDefault();
                for (int i = root.Childrens.Count - 1; i >= 0; --i)
                {
                    RecursiveDeleteFromDb(root.Childrens[i].Id);
                    db.Objects.Remove(root.Childrens[i]);
                }
                db.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void CopyPropertiesTo(UObject another)
        {
            another.Name = Name;
            another.ParentId = ParentId;
            another.ClassId = ClassId;
        }
    }


}
