using System.Windows;
using UniversityDB.Infrastructure.Enums;
using UniversityDB.Models;
using UniversityDB.ViewModels.Forms;

namespace UniversityDB.Forms
{
    public partial class UObjectWindow : Window
    {
        //Adding
        public UObjectWindow()
        {
            DataContext = new UObjectViewModel();
            InitializeComponent();
        }

        //View and Edit
        public UObjectWindow(UObject elem, FormType type)
        {
            DataContext = new UObjectViewModel(elem, type);
            InitializeComponent();
        }
    }
}
