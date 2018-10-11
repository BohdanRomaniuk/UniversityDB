using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UniversityDB.Infrastructure;
using UniversityDB.ViewModels;

namespace UniversityDB.Models
{
    [Table("Objects")]
    public class UObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Class { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public UObject Parent { get; set; }
        public List<UObject> Childrens { get; set; }

        public UObject()
        {
            ViewCommand = new Command(View);
            ExpandingCommand = new Command(ExecuteExpandingCommand);
        }

        private void View(object parametr)
        {
            MessageBox.Show(parametr.ToString(), "Інфо");
        }

        public ICommand ViewCommand { get; private set; }

        public ICommand ExpandingCommand { get; set; }

        // Should be moved out of here
        private void ExecuteExpandingCommand(object obj)
        {
            var args = obj as RoutedEventArgs;
            var treeViewItem = args.Source as TreeViewItem;
            var data = treeViewItem.DataContext as UObject; // Here is all we need
            var id = data.Id;
            MessageBox.Show(@"Expanded: " + id);
        }

        public UObject(string _name, int _class)
        {
            Name = _name;
            Class = _class;
        }
    }
}
