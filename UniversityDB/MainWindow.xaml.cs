using System.Windows;
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
}
