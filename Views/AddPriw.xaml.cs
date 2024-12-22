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
using System.Windows.Shapes;
using priv.Model;
using priv.ViewModels;
using priv.Views;

namespace priv.Views
{
    /// <summary>
    /// Логика взаимодействия для AddPriw.xaml
    /// </summary>
    public partial class AddPriw : Window
    {
        public AddPriw(ObservableCollection<string> habits)
        {
            InitializeComponent();
            DataContext = new AddPrivViewModel(habits);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
