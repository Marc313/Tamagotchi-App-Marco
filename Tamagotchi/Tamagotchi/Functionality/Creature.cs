using System;
using System.ComponentModel;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public Need Hunger { get; set; }
        public Need Thirst { get; set; }
        public Need Attention { get; set; }
        public Need Energy { get; set; }
        public Need SocialEnergy { get; set; }
        public Need Loneliness { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Creature()
        {
            Hunger = new Need();
            Thirst = new Need();
            Attention = new Need();
            Energy = new Need();
            SocialEnergy = new Need();
            Loneliness = new Need();
        }

        public string NeedsToString()
        {
            string stats = $"Hunger: {Hunger.ValueToOneDecimal()}\n" +
                            $"Thirst: {Thirst.ValueToOneDecimal()}\n" +
                            $"Attention: {Attention.ValueToOneDecimal()}\n" +
                            $"Energy: {Energy.ValueToOneDecimal()}\n" +
                            $"Social Energy: {SocialEnergy.ValueToOneDecimal()}\n" +
                            $"Loneliness: {Loneliness.ValueToOneDecimal()}";

            return stats;
        }

        public void ReceiveAllTimePenalties(double timePassed)
        {
            Hunger.ReceiveTimePenalty(timePassed);
            Thirst.ReceiveTimePenalty(timePassed);
            Attention.ReceiveTimePenalty(timePassed);
            Energy.ReceiveTimePenalty(timePassed);
            SocialEnergy.ReceiveTimePenalty(timePassed);
            Loneliness.ReceiveTimePenalty(timePassed);
        }

        public string ToJson()
        {
            return NeedsToString();
        }
    }
}
