using System;
using System.ComponentModel;
using Xamarin.Essentials;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public Need Food { get; set; }
        public Need Hydration { get; set; }
        public Need Attention { get; set; }
        public Need Energy { get; set; }
        public Need SocialEnergy { get; set; }
        public Need Company { get; set; }

        //private string needsText;
        public string NeedsToString
        {
            get
            {
                return NeedsText();
            }
            set { }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Creature()
        {
            Food = new Need();
            Hydration = new Need();
            Attention = new Need();
            Energy = new Need();
            SocialEnergy = new Need();
            Company = new Need();

            App.OnStartEvent += CheckForTimePenalty;
            App.OnResumeEvent += CheckForTimePenalty;
        }

        public string NeedsText()
        {
            string stats = $"Hunger: {Food?.ValueToOneDecimal()}\n" +
                            $"Thirst: {Hydration?.ValueToOneDecimal()}\n" +
                            $"Attention: {Attention?.ValueToOneDecimal()}\n" +
                            $"Energy: {Energy?.ValueToOneDecimal()}\n" +
                            $"Social Energy: {SocialEnergy?.ValueToOneDecimal()}\n" +
                            $"Loneliness: {Company?.ValueToOneDecimal()}";

            return stats;
        }

        public void SetNeedsText()
        {
            NeedsToString = NeedsText();
        }

        public string ToJson()
        {
            return NeedsToString;
        }

        public void ReceiveAllTimePenalties(double timePassed)
        {
            Food.ReceiveTimePenalty(timePassed);
            Hydration.ReceiveTimePenalty(timePassed);
            Attention.ReceiveTimePenalty(timePassed);
            Energy.ReceiveTimePenalty(timePassed);
            SocialEnergy.ReceiveTimePenalty(timePassed);
            Company.ReceiveTimePenalty(timePassed);
        }

        private void CheckForTimePenalty()
        {
            double timePassed = Preferences.Get("secondsPassed", 0.0);
            Console.WriteLine($"{timePassed} seconds have passed!");
            ReceiveAllTimePenalties(timePassed);
        }
    }
}
