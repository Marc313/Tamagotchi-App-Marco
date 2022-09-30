using System;
using System.ComponentModel;

namespace Tamagotchi
{
    public class Need : INotifyPropertyChanged
    {
        public enum State
        {
            HEALTHY,
            NOTGREAT,
            DANGER,
            EMERGENCY
        }

        public State NeedState { 
            get
            {
                switch(Value)
                {
                    case <= 15 :
                        return State.EMERGENCY;
                    case <= 45 :
                        return State.DANGER;
                    case <= 75 :
                        return State.NOTGREAT;
                    case <= 100 :
                        return State.HEALTHY;
                    default:
                        return State.HEALTHY;
                } 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnValueChanged;

        public double Value { get; set; }

        private double maxValue = 100;
        private double penaltyPerSecond;

        public Need()
        {
            Value = 50.0;
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
            OnValueChanged.Invoke();
        }

        public void DecreaseValue(double decrease)
        {
            Value -= decrease;
            if (Value < 0) Value = 0;
            OnValueChanged.Invoke();
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
