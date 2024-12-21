using priv.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using priv.Views;
using System.Windows;
using System.Security.Policy;

namespace priv.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _newHabit;
        public string NewHabit
        {
            get => _newHabit;
            set
            {
                _newHabit = value;
                OnPropertyChanged(nameof(NewHabit));
            }
        }

        // SelectedHabit
        private Habit selectedHabit;
        public Habit SelectedHabit
        {
            get { return selectedHabit;; }
            set
            {
                selectedHabit = value;
                OnPropertyChanged("SelectedHabit");
            }
        }

        public ObservableCollection<Habit> Habits { get; set; }
        public RelayCommand AddHabitCommand { get; set; }
        public RelayCommand DeleteHabitCommand { get; set; }
        public RelayCommand EditHabitCommand { get; set; }

        public MainViewModel()
        {
            Habits = new ObservableCollection<Habit>();
            LoadHabitsFromDatabase();
            AddHabitCommand = new RelayCommand(AddHabit);
            DeleteHabitCommand = new RelayCommand(DeleteHabit);
            EditHabitCommand = new RelayCommand(EditHabit);
        }

        private void LoadHabitsFromDatabase()
        {
            using (DataBase db = new DataBase())
            {
                db.OpenConnection();
                string query = "SELECT Priv FROM Privychki1";
                DataTable dataTable = db.SelectData(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    Habits.Add(new Habit { Name = row["Priv"].ToString() });
                }
            }
        }

        private void AddHabit()
        {
            MessageBox.Show("qwe");
            if (!string.IsNullOrWhiteSpace(NewHabit))
            {
                using (DataBase db = new DataBase())
                {
                    db.OpenConnection();
                    string query = "INSERT INTO Privychki1 (Priv) VALUES (@Priv)";
                    using (SqlCommand command = new SqlCommand(query, db.GetConnection()))
                    {
                        command.Parameters.AddWithValue("@Priv", NewHabit);
                        command.ExecuteNonQuery();
                    }
                }
                Habits.Add(new Habit { Name = NewHabit });
                NewHabit = string.Empty; // Очистка поля ввода
            }
        }

        private void DeleteHabit()
        {
            if (SelectedHabit != null)
            {
                using (DataBase db = new DataBase())
                {
                    db.OpenConnection();
                    string query1 = "DELETE FROM Privychki1 WHERE Priv = @Priv";
                    using (SqlCommand command1 = new SqlCommand(query1, db.GetConnection()))
                    {
                        command1.Parameters.AddWithValue("@Priv", SelectedHabit.Name);
                        command1.ExecuteNonQuery();
                    }
                }
                Habits.Remove(SelectedHabit);
            }
        }

        private bool CanDeleteHabit()
        {
            return SelectedHabit != null;
        }

        private void EditHabit()
        {
            //  // Логика для редактирования привычки
            var habitNames = Habits.Select(h => h.Name).ToList();
            var editor = new AddPriw(new ObservableCollection<string>(habitNames)); // Создаем ObservableCollection
            editor.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

