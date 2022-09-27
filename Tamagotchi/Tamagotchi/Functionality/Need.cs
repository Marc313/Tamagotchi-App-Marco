﻿using System.ComponentModel;

namespace Tamagotchi
{
    public class Need : INotifyPropertyChanged
    {
        public double Value {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public double value;
        public double maxValue = 100;
        private double penaltyPerSecond;

        public event PropertyChangedEventHandler PropertyChanged;

        public Need()
        {
            value = maxValue;
            penaltyPerSecond = .1f;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Need(double penaltyPerSecond)
        {
            this.penaltyPerSecond = penaltyPerSecond;
        }

        public void IncreaseValue(double increase)
        {
            value += increase;
            if (value > maxValue) value = maxValue;
        }

        public void DecreaseValue(double decrease)
        {
            value -= decrease;
            if (value < 0) value = 0;
        }

        public void ReceiveTimePenalty(double timePassed)
        {
            DecreaseValue(timePassed * penaltyPerSecond);
        }

        public string ValueToOneDecimal()
        {
            string valueString = value.ToString();
            if (valueString.Length <= 3)
            {
                return valueString;
            }
            else
            {
                return value.ToString().Substring(0, 4);
            }
        }
    }
}
