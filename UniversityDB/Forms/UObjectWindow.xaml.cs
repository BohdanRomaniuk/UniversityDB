using System.Windows;
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
        public UObjectWindow(int id)
        {
            DataContext = new UObjectViewModel(id);
            InitializeComponent();
        }
    }
}
