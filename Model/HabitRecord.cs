using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace priv.Model
{
    public class HabitRecord
    {
        private Habit _habit;
        private DateTime _dateTime;
        private int _count;

        public Habit Habit
        {
            get => _habit;
            set
            {
                if (_habit != value)
                {
                    _habit = value;
                    OnPropertyChanged(nameof(Habit));
                }
            }
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
