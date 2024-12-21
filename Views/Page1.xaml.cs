using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using priv.ViewModels;

namespace priv.Views
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(string labelText)
        {
            InitializeComponent();
            DataContext = new Page1ViewModel(labelText);
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); // Показываем главное окно
            Window.GetWindow(this)?.Close();
        }

        private void btnback_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); // Показываем главное окно
            Window.GetWindow(this)?.Close();
        }
    }
}
