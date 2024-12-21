using priv.Model;
using priv.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace priv.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // Устанавливаем DataContext на ViewModel
        }
      

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = SelectItem.SelectedItem as Habit;
            if (SelectItem.SelectedItem != null)
            {
                string selectedItemText = selectedItem.Name;
                MainFrame.Navigate(new Page1(selectedItemText));
            }
        }
    }
}
