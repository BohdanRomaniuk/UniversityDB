using System.Windows;
using UniversityDB.Infrastructure.Enums;
using UniversityDB.Models;
using UniversityDB.ViewModels.Forms;

namespace UniversityDB.Forms
{
    public partial class UPersonWindow : Window
    {
        //Adding
        public UPersonWindow()
        {
            DataContext = new UObjectViewModel();
            InitializeComponent();
        }

        //View and Edit
        public UPersonWindow(UObject elem, FormType type)
        {
            DataContext = new UObjectViewModel(elem, type);
            InitializeComponent();
        }
    }
}
