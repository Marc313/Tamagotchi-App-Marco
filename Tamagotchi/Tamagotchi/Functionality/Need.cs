namespace Tamagotchi
{
    public class Need
    {
        public double value;
        public double maxValue = 100;
        private double penaltyPerSecond;

        public Need()
        {
            value = maxValue;
            penaltyPerSecond = .1f;
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
