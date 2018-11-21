using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UniversityDB.ViewModels;

namespace UniversityDB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }

    public class ContextAction : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICommand Action { get; set; }

        public List<ContextAction> Subs { get; set; }

        public ContextAction()
        {
            Subs = new List<ContextAction>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
