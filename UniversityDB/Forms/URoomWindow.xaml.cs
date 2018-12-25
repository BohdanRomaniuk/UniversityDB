using System.Windows;
using UniversityDB.Infrastructure.Enums;
using UniversityDB.Models;
using UniversityDB.ViewModels;

namespace UniversityDB.Forms
{
    public partial class URoomWindow : Window
    {
        //View and Edit
        public URoomWindow(UObject elem, FormType type)
        {
            DataContext = new UObjectViewModel(elem, type);
            InitializeComponent();
        }

        //Adding
        public URoomWindow(UObject elem, FormType type, string className)
        {
            DataContext = new UObjectViewModel(elem, type, className);
            InitializeComponent();
        }
    }
}
