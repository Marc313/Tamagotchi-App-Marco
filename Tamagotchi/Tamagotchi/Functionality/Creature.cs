using System;
using System.Collections.Generic;
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

        private List<Need> needs;

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
            needs = new List<Need>();
            needs.Add(Food = new Need());
            needs.Add(Hydration = new Need());
            needs.Add(Attention = new Need());
            needs.Add(Energy = new Need());
            needs.Add(SocialEnergy = new Need());
            needs.Add(Company = new Need());

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
            foreach (Need need in needs)
            {
                need.ReceiveTimePenalty(timePassed);
            }
        }

        private void CheckForTimePenalty()
        {
            double timePassed = Preferences.Get("secondsPassed", 0.0);
            Console.WriteLine($"{timePassed} seconds have passed!");
            ReceiveAllTimePenalties(timePassed);
        }
    }
}
