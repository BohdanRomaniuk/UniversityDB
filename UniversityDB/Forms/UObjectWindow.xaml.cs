using System.Windows;
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
        public UObjectWindow(UObject elem)
        {
            DataContext = new UObjectViewModel(elem);
            InitializeComponent(); InitializeComponent();
        }
    }
}
