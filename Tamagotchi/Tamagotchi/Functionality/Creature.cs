using System;
using System.ComponentModel;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public Need hunger;
        public Need thirst;

        public Need Hunger { 
            get { return hunger; } 
            set 
            {
                hunger = value;
                OnPropertyChanged(nameof(Hunger));
                OnPropertyChanged(nameof(NeedsToString));
            } 
        }

        public Need Thirst { get; set; }
        public Need Attention { get; set; }
        public Need Energy { get; set; }
        public Need SocialEnergy { get; set; }
        public Need Loneliness { get; set; }

        //private string needsText;
        public string NeedsToString
        {
            get
            {
                return NeedsText();
            }
            set { }
        }

        public string NeedsText()
        {
            string stats = $"Hunger: {Hunger?.ValueToOneDecimal()}\n" +
                            $"Thirst: {Thirst?.ValueToOneDecimal()}\n" +
                            $"Attention: {Attention?.ValueToOneDecimal()}\n" +
                            $"Energy: {Energy?.ValueToOneDecimal()}\n" +
                            $"Social Energy: {SocialEnergy?.ValueToOneDecimal()}\n" +
                            $"Loneliness: {Loneliness?.ValueToOneDecimal()}";

            return stats;
        }

        public void SetNeedsText()
        {
            NeedsToString = NeedsText();
        }

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

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            SetNeedsText();
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
            return NeedsToString;
        }
    }
}
