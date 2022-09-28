using System.ComponentModel;

namespace Tamagotchi
{
    public class Need : INotifyPropertyChanged
    {
        public double Value { get; set; }

        public double maxValue = 100;
        private double penaltyPerSecond;

        public event PropertyChangedEventHandler PropertyChanged;

        public Need()
        {
            Value = maxValue;
            penaltyPerSecond = .1f;
        }

        public Need(double penaltyPerSecond)
        {
            this.penaltyPerSecond = penaltyPerSecond;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void IncreaseValue(double increase)
        {
            Value += increase;
            if (Value > maxValue) Value = maxValue;
        }

        public void DecreaseValue(double decrease)
        {
            Value -= decrease;
            if (Value < 0) Value = 0;
        }

        public void ReceiveTimePenalty(double timePassed)
        {
            DecreaseValue(timePassed * penaltyPerSecond);
        }

        public string ValueToOneDecimal()
        {
            string valueString = Value.ToString();
            if (valueString.Length <= 3)
            {
                return valueString;
            }
            else
            {
                return Value.ToString().Substring(0, 4);
            }
        }
    }
}
