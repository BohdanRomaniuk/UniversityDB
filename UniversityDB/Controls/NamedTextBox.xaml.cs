using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UniversityDB.Controls
{
    public partial class NamedTextBox : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(NamedTextBox), new PropertyMetadata("Error"));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(NamedTextBox), new PropertyMetadata("Id"));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(NamedTextBox), new PropertyMetadata(false));

        public NamedTextBox()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ControlLoaded);
        }

        void ControlLoaded(object sender, RoutedEventArgs e)
        {
            //if (Text == "Id" || Text == "Class.Name" || Text == "Parent.Name")
            //{
            //    IsReadOnly = true;
            //}
            Binding textBinding = new Binding()
            {
                Source = DataContext,
                Path = new PropertyPath("Current." + Text),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, textBinding);
        }
    }
}
