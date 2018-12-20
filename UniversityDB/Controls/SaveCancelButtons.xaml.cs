using System.Windows;
using System.Windows.Controls;

namespace UniversityDB.Controls
{
    public partial class SaveCancelButtons : UserControl
    {
        public Window CurrentWindow
        {
            get { return (Window)GetValue(CurrentWindowProperty); }
            set { SetValue(CurrentWindowProperty, value); }
        }


        public static readonly DependencyProperty CurrentWindowProperty =
            DependencyProperty.Register(nameof(CurrentWindow), typeof(Window), typeof(SaveCancelButtons));

        public SaveCancelButtons()
        {
            InitializeComponent();
        }
    }
}
