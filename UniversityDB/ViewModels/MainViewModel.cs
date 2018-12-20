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
using GongSolutions.Wpf.DragDrop;
using System;

namespace UniversityDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDropTarget
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

            //DB Creation
            if (db.Objects.Count() == 0)
            {
                CreateDb();
            }
            
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

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is UObject &&
                    dropInfo.TargetItem is UObject)
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            //Database and UI update logic
            if ((dropInfo.TargetItem != (dropInfo.Data as UObject).Parent))
            {
                var dropInfoTarget = dropInfo.TargetItem as UObject;
                var dropInfoData = dropInfo.Data as UObject;
                using (UniversityContext db = new UniversityContext())
                {
                    var target = db.Objects.Where(o => o.Id == dropInfoTarget.Id).Include(c => c.Class).FirstOrDefault();
                    var dragged = db.Objects.Where(o => o.Id == dropInfoData.Id).Include(c => c.Class)
                        .Include(o => o.Parent).FirstOrDefault();

                    // Check
                    if (db.ClassesRules.Any(c => c.ClassId == target.ClassId && c.ClassIdInside == dragged.ClassId))
                    {
                        // Update UI
                        dropInfoData.Parent?.Childrens.Remove(dropInfoData);

                        dropInfoTarget.Childrens.Add(dropInfoData);
                        dropInfoData.Parent = dropInfoTarget;

                        // Update DB
                        dragged?.Parent.Childrens.Remove(dragged);
                        target?.Childrens.Add(dragged);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Об'єкт не може містити синівських елементів такого типу");
                    }
                }
            }
        }


        private void CreateDb()
        {
            using (var db = new UniversityContext())
            {
                db.Classes.Add(new SClass("UObject", "Об'єкт", "UObjectWindow")); //1

                db.Classes.Add(new SClass("UPerson", "Особу", "UPersonWindow")); //2 
                db.Classes.Add(new SClass("UStudyingPerson", "Особу, яка вчиться", "UStudyingPersonWindow")); //3
                db.Classes.Add(new SClass("UStudent", "Студента", "UStudentWindow")); //4
                db.Classes.Add(new SClass("UWorkingPerson", "Особу, яка працює", "UWorkingPersonWindow")); //5
                db.Classes.Add(new SClass("UTeacher", "Вчителя", "UTeacherWindow")); //6

                db.Classes.Add(new SClass("UDepartment", "Відділ", "UDepartmentWindow")); //7 
                db.Classes.Add(new SClass("UFaculty", "Факультет", "UFacultyWindow")); //8 
                db.Classes.Add(new SClass("UDeanery", "Деканат", "UDeaneryWindow")); //9

                db.Classes.Add(new SClass("UInventory", "Інвентар", "UInventoryWindow")); //10
                db.Classes.Add(new SClass("URoom", "Аудиторія", "URoomWindow")); //11

                db.Classes.Add(new SClass("UEvent", "Подія", "UEventWindow")); //12
                db.Classes.Add(new SClass("UExcursion", "Екскурсія", "UExcursionWindow")); //13
                db.Classes.Add(new SClass("UFestival", "Фестиваль", "UFestivalWindow")); //14
                db.Classes.Add(new SClass("UMeeting", "Зустріч", "UMeetingWindow")); //15

                db.Classes.Add(new SClass("UDocument", "Документ", "UDocumentWindow")); //16
                db.Classes.Add(new SClass("UContract", "Договір", "UContractWindow")); //17
                db.Classes.Add(new SClass("UStudentTicket", "Учнівський", "UStudentTicketWindow")); //18
                db.Classes.Add(new SClass("UGradebook", "Заліковка", "UGradebookWindow")); //19
                db.Classes.Add(new SClass("UPrintEdition", "Друковане видання", "UPrintEditionWindow")); // 20
                db.Classes.Add(new SClass("UNewspaper", "Газета", "UNewspaperWindow")); //21
                db.Classes.Add(new SClass("UMagazine", "Журнал", "UMagazineWindow")); //22

                db.Classes.Add(new SClass("UProcess", "Процес", "UProcessWindow")); //23
                db.Classes.Add(new SClass("URepair", "Ремонт", "URepairWindow")); //24
                db.Classes.Add(new SClass("USymbol", "Символ", "USymbolWindow")); //25
                db.Classes.Add(new SClass("UStudyingRoom", "Лекційна аудиторія", "UStudyingRoomWindow"));//26
                db.Classes.Add(new SClass("UPracticeRoom", "Лабораторія", "UPracticeRoomWindow"));
                db.Classes.Add(new SClass("UDiningRoom", "Їдальня", "UDiningRoomWindow"));
                db.Classes.Add(new SClass("UBuilding", "Будівля", "UBuildingWindow"));
                db.Classes.Add(new SClass("UStudyingBuilding", "Навчальний корпус", "UStudyingBuildingWindow"));
                db.Classes.Add(new SClass("USportBuilding", "Спорткомплекс", "USportBuildingWindow"));
                db.Classes.Add(new SClass("UBotanicGardenBuilding", "Ботанічний сад", "UBotanicGardenBuildingWindow"));
                db.Classes.Add(new SClass("UClinicBuilding", "Поліклініка", "UClinicBuildingWindow"));
                db.Classes.Add(new SClass("UCampBuilding", "Табір", "UCampBuildingWindow"));//34
                db.SaveChanges();
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 2 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 3 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 4 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 5 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 6 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 7 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 8 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 9 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 10 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 11 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 12 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 13 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 14 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 15 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 16 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 17 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 18 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 19 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 20 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 21 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 22 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 23 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 24 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 25 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 26 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 27 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 28 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 29 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 30 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 31 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 32 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 33 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = 34 });

                db.ClassesRules.Add(new SClassRules() { ClassId = 2, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 3, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 4, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 5, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 6, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 7, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 8, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 9, ClassIdInside = 1 });
                db.ClassesRules.Add(new SClassRules() { ClassId = 10, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 11, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 12, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 13, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 14, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 15, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 16, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 17, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 18, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 19, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 20, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 21, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 22, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 23, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 24, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 25, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 26, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 27, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 28, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 29, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 30, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 31, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 32, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 33, ClassIdInside = 1 });
                //db.ClassesRules.Add(new SClassRules() { ClassId = 34, ClassIdInside = 1 });
                db.SaveChanges();
                UObject lnu = new UObject("ЛНУ ім. І. Франка", 1);
                UObject faculties = new UObject("Факультети", 1);
                UObject majors = new UObject("Керівники", 1);
                UObject symbols = new UObject("Символи", 1);

                UObject fpmi = new UFaculty("Прикладної математики та інформатики", "вул. Університетська 1", "ami.lnu.edu.ua", 8);
                fpmi.Childrens = new ObservableCollection<UObject>();
                fpmi.Childrens.Add(new UDeanery("Деканат", "вул Університська 1", "0323232", 1)
                {
                    Childrens = new ObservableCollection<UObject>()
                    {
                        new UObject("Декан", 1)
                        {
                            Childrens = new ObservableCollection<UObject>()
                            {
                                new UPerson("Дияк І.І.", Convert.ToDateTime("1987-09-05"),"Любінська 10, кв 4",2)
                            }
                        }
                    }
                }
                );
                UObject cafedras = new UObject("Кафедри", 1);

                cafedras.Childrens.Add(new UObject("КІС", 1));
                cafedras.Childrens.Add(new UObject("КДАІС", 1));
                cafedras.Childrens.Add(new UObject("КП", 1));
                cafedras.Childrens.Add(new UObject("КПМ", 1));

                fpmi.Childrens.Add(cafedras);

                faculties.Childrens = new ObservableCollection<UObject>();
                faculties.Childrens.Add(fpmi);
                faculties.Childrens.Add(new UFaculty("Біологічний", "вул. Михайла Грушевського, 4", "bioweb.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Географічний", "вул. Дорошенка, 41", "geography.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Економічний", "проспект Свободи, 18", "econom.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Іноземних мов", "вул. Університетська 1/415", "lingua.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Філософський", "вул. Університетська, 1", "filos.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Хімічний", "вул. Кирила і Мефодія, 6", "chem.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Філологічний", "вул. Університетська, 1", "philology.lnu.edu.ua", 8));
                faculties.Childrens.Add(new UFaculty("Юридичний", "вул. Січових Стрільців, 14", "law.lnu.edu.ua", 8));

                lnu.Childrens = new ObservableCollection<UObject>();
                lnu.Childrens.Add(faculties);
                lnu.Childrens.Add(majors);
                lnu.Childrens.Add(symbols);
                db.Objects.Add(lnu);
                db.SaveChanges();
            }
        }
    }
}
