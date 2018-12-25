using System.Windows;
using UniversityDB.Infrastructure.Enums;
using UniversityDB.Models;
using UniversityDB.ViewModels;

namespace UniversityDB.Forms
{
    public partial class UTransportInventoryWindow : Window
    {
        //View and Edit
        public UTransportInventoryWindow(UObject elem, FormType type)
        {
            DataContext = new UObjectViewModel(elem, type);
            InitializeComponent();
        }

        //Adding
        public UTransportInventoryWindow(UObject elem, FormType type, string className)
        {
            DataContext = new UObjectViewModel(elem, type, className);
            InitializeComponent();
        }
    }
}
