using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace priv.ViewModels
{
    internal class AddPrivViewModel : INotifyPropertyChanged
    {
        private string _selectedHabit;
        public string SelectedHabit
        {
            get => _selectedHabit;
            set
            {
                _selectedHabit = value;
                OnPropertyChanged(nameof(SelectedHabit));
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private string _countText;
        public string CountText
        {
            get => _countText;
            set
            {
                _countText = value;
                OnPropertyChanged(nameof(CountText));
            }
        }

        public ObservableCollection<string> Habits { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }

        public AddPrivViewModel(ObservableCollection<string> habits)
        {
            Habits = habits;
            SaveCommand = new RelayCommand(Save);
            ExitCommand = new RelayCommand(Exit);
        }

        private void Save()
        {
            if (SelectedDate == null || string.IsNullOrWhiteSpace(CountText) || !int.TryParse(CountText, out int count))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            string connectionString = @"Server=DESKTOP-K2O72KN;Database=KumenkoLA;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;encrypt=false";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO [dbo].[Privychki] ([priv1], [ДатаВремя], [КоличествоРаз]) VALUES (@priv1, @ДатаВремя, @КоличествоРаз)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@priv1", SelectedHabit);
                        command.Parameters.AddWithValue("@ДатаВремя", SelectedDate.Value);
                        command.Parameters.AddWithValue("@КоличествоРаз", count);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Данные успешно сохранены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                }
            }
        }

        private void Exit()
        {
            // Логика выхода, например, закрытие окна
            //Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
