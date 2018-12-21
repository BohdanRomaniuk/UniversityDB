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
        private UObject loadingObject = new UObject("Завнтаження...", 1);

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
            db = new UniversityContext();
            ExpandCommand = new Command(Expand);

            //DB Creation
            if (db.Objects.Count() == 0)
            {
                CreateDb();
            }

            Faculties = new ObservableCollection<UObject>();
            UObject root = db.Objects.Where(o => o.ParentId == null)
                .Include(o => o.Class)
                .Include(o => o.Class.AllowedChildrens.Select(y => y.ClassInside))
                .FirstOrDefault();
            root.Parent = new UObject("Немає", 1);
            if (root != null)
            {
                Faculties.Add(root);
                AppendChildrenByParent(root);
            }
        }

        private void Expand(object obj)
        {
            var data = ((obj as RoutedEventArgs).Source as TreeViewItem).DataContext as UObject;
            AppendChildrenByParent(data);
        }

        private void AppendChildrenByParent(UObject parent)
        {
            parent.Childrens.Clear();
            var childs = new ObservableCollection<UObject>(db.Objects.Where(o => o.ParentId == parent.Id)
            .Include(o => o.Class)
            .Include(o => o.Class.AllowedChildrens.Select(y => y.ClassInside))
            .ToList());

            if (parent != null)
            {
                if (parent.Childrens.Count() == 0)
                {
                    foreach (var child in childs)
                    {
                        child.Parent = parent;
                        var subChildsCount = db.Objects.Count(o => o.ParentId == child.Id);
                        if (subChildsCount != 0)
                        {
                            child.Childrens = new ObservableCollection<UObject> { loadingObject };
                        }
                        parent.Childrens.Add(child);
                    }
                }
                else
                {
                    foreach (var child in parent.Childrens)
                    {
                        var subChildsCount = db.Objects.Count(o => o.ParentId == child.Id);
                        if (subChildsCount != 0)
                        {
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
            db.Classes.Add(new SClass("UObject", "Об'єкт", "UObjectWindow"));

            db.Classes.Add(new SClass("UPerson", "Особу", "UPersonWindow")); 
            db.Classes.Add(new SClass("UStudyingPerson", "Особу, яка вчиться", "UStudyingPersonWindow"));
            db.Classes.Add(new SClass("UStudent", "Студента", "UStudentWindow"));
            db.Classes.Add(new SClass("UWorkingPerson", "Особу, яка працює", "UWorkingPersonWindow"));
            db.Classes.Add(new SClass("UTeacher", "Вчителя", "UTeacherWindow"));

            db.Classes.Add(new SClass("UDepartment", "Відділ", "UDepartmentWindow"));
            db.Classes.Add(new SClass("UFaculty", "Факультет", "UFacultyWindow"));
            db.Classes.Add(new SClass("UDeanery", "Деканат", "UDeaneryWindow"));

            db.Classes.Add(new SClass("UInventory", "Інвентар", "UInventoryWindow"));
            db.Classes.Add(new SClass("URoom", "Аудиторія", "URoomWindow"));

            db.Classes.Add(new SClass("UEvent", "Подія", "UEventWindow"));
            db.Classes.Add(new SClass("UExcursion", "Екскурсія", "UExcursionWindow"));
            db.Classes.Add(new SClass("UFestival", "Фестиваль", "UFestivalWindow"));
            db.Classes.Add(new SClass("UMeeting", "Зустріч", "UMeetingWindow"));

            db.Classes.Add(new SClass("UDocument", "Документ", "UDocumentWindow"));
            db.Classes.Add(new SClass("UContract", "Договір", "UContractWindow"));
            db.Classes.Add(new SClass("UStudentTicket", "Учнівський", "UStudentTicketWindow"));
            db.Classes.Add(new SClass("UGradebook", "Заліковка", "UGradebookWindow"));
            db.Classes.Add(new SClass("UPrintEdition", "Друковане видання", "UPrintEditionWindow"));
            db.Classes.Add(new SClass("UNewspaper", "Газета", "UNewspaperWindow"));
            db.Classes.Add(new SClass("UMagazine", "Журнал", "UMagazineWindow"));

            db.Classes.Add(new SClass("UProcess", "Процес", "UProcessWindow"));
            db.Classes.Add(new SClass("URepair", "Ремонт", "URepairWindow"));
            db.Classes.Add(new SClass("USymbol", "Символ", "USymbolWindow"));
            db.Classes.Add(new SClass("UStudyingRoom", "Лекційна аудиторія", "UStudyingRoomWindow"));
            db.Classes.Add(new SClass("UPracticeRoom", "Лабораторія", "UPracticeRoomWindow"));
            db.Classes.Add(new SClass("UDiningRoom", "Їдальня", "UDiningRoomWindow"));
            db.Classes.Add(new SClass("UBuilding", "Будівля", "UBuildingWindow"));
            db.Classes.Add(new SClass("UStudyingBuilding", "Навчальний корпус", "UStudyingBuildingWindow"));
            db.Classes.Add(new SClass("USportBuilding", "Спорткомплекс", "USportBuildingWindow"));
            db.Classes.Add(new SClass("UBotanicGardenBuilding", "Ботанічний сад", "UBotanicGardenBuildingWindow"));
            db.Classes.Add(new SClass("UClinicBuilding", "Поліклініка", "UClinicBuildingWindow"));
            db.Classes.Add(new SClass("UCampBuilding", "Табір", "UCampBuildingWindow"));
            db.Classes.Add(new SClass("UEducationalSubject", "Навчальний предмет", "UEducationalSubjectWindow"));
            db.Classes.Add(new SClass("UReporting", "Звітність", "UReportingWindow"));
            db.Classes.Add(new SClass("UTechniqueInventory", "Технічний інвентар", "UTechniqueInventoryWindow"));
            db.Classes.Add(new SClass("UTransportInventory", "Транспорт", "UTransportInventoryWindow"));
            db.Classes.Add(new SClass("UPlan", "План", "UPlanWindow"));
            db.Classes.Add(new SClass("UMaterial", "Матеріал", "UMaterialWindow"));
            db.SaveChanges();
            for (int i = 0; i < db.Classes.Count(); ++i)
            {
                db.ClassesRules.Add(new SClassRules() { ClassId = 1, ClassIdInside = i + 1 });
                if (i != 0)
                {
                    db.ClassesRules.Add(new SClassRules() { ClassId = i + 1, ClassIdInside = 1 });
                }
            }


            db.SaveChanges();
            UObject lnu = new UObject("ЛНУ ім. І. Франка", 1);
            UObject faculties = new UObject("Факультети", 1);
            UObject majors = new UObject("Керівники", 1);
            UObject symbols = new UObject("Символи", 1);

            UObject fpmi = new UFaculty("ФПМІ", "вул. Університетська 1", "ami.lnu.edu.ua", 8);
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
