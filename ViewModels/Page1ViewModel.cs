using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using priv.Model;


namespace priv.ViewModels
{
    internal class Page1ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HabitRecord> _habitData;
        public ObservableCollection<HabitRecord> HabitData
        {
            get => _habitData;
            set
            {
                _habitData = value;
                OnPropertyChanged(nameof(HabitData));
            }
        }

        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                OnPropertyChanged(nameof(LabelText));
            }
        }

        private PlotModel _plotModel;
        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }

        public ICommand BackCommand { get; }

        public Page1ViewModel(string labelText)
        {
            LabelText = labelText;
            HabitData = new ObservableCollection<HabitRecord>();
            BackCommand = new RelayCommand(Back);
            LoadDataAndPlotGraph(labelText);
        }

        private void LoadDataAndPlotGraph(string habit)
        {
            string connectionString = @"Server=DESKTOP-K2O72KN;Database=KumenkoLA;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;encrypt=false";

            PlotModel = new PlotModel { Title = "График привычки" };
            var lineSeries = new LineSeries { Title = habit, MarkerType = MarkerType.Circle };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ДатаВремя, КоличествоРаз " +
                               "FROM [dbo].[Privychki] " +
                               "WHERE priv1 = @habit " +
                               "ORDER BY ДатаВремя";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@habit", habit);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime dateTime = reader.GetDateTime(0);
                            int totalCount = reader.GetInt32(1);
                            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTime), totalCount));

                            HabitData.Add(new HabitRecord { DateTime = dateTime, Count = totalCount });

                            var textAnnotation = new TextAnnotation
                            {
                                Text = $"{dateTime:dd/MM/yyyy HH:mm}\n{totalCount} раз",
                                TextPosition = new DataPoint(DateTimeAxis.ToDouble(dateTime), totalCount),
                                FontSize = 10,
                                Stroke = OxyColors.Transparent,
                                Background = OxyColors.White
                            };

                            textAnnotation.TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center;
                            textAnnotation.TextVerticalAlignment = OxyPlot.VerticalAlignment.Bottom;

                            PlotModel.Annotations.Add(textAnnotation);
                        }
                    }
                }
            }

            PlotModel.Series.Add(lineSeries);
        }

        private void Back()
        {
            // Логика для возврата на главное окно
            // Например, можно использовать событие или делегат для уведомления представления
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
