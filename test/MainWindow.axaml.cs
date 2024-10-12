using Avalonia.Controls;
using System.ComponentModel;
using test.ViewModels;

namespace test
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