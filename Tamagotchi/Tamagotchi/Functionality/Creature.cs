using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Essentials;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public enum CreatureState { HAPPY, TIRED, BORED, UNDERNOURISHED }

        public CreatureState CurrentState
        {
            get
            {
                if (LowestNeed().Value > 60) return CreatureState.HAPPY;
                else if (LowestNeed() == Food || LowestNeed() == Hydration) return CreatureState.UNDERNOURISHED;
                else if (LowestNeed() == Attention || LowestNeed() == Company) return CreatureState.BORED;
                else if (LowestNeed() == Energy || LowestNeed() == SocialEnergy) return CreatureState.TIRED;
                return CreatureState.HAPPY;
            }
        }

        public Need Food { get; set; }
        public Need Hydration { get; set; }
        public Need Attention { get; set; }
        public Need Energy { get; set; }
        public Need SocialEnergy { get; set; }
        public Need Company { get; set; }

        public event Action OnCreatureChanged;

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
            double needPenalty1 = 1.0 / 864.0;  // In 24 hours, this will deplete a value of 100
            double needPenatly2 = needPenalty1 / 1.5;
            double needPenatly3 = needPenalty1 / 2;

            needs = new List<Need>();
            needs.Add(Food = new Need(needPenalty1));
            needs.Add(Hydration = new Need(needPenalty1));
            needs.Add(Attention = new Need(needPenatly2));
            needs.Add(Energy = new Need(needPenatly2));
            needs.Add(SocialEnergy = new Need(needPenatly2));
            needs.Add(Company = new Need(needPenatly3));

            App.OnStartEvent += CheckForTimePenalty;
            App.OnResumeEvent += CheckForTimePenalty;

            foreach (Need need in needs)
            {
                need.OnValueChanged += InvokeCreatureChanged;
            }
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

        public double SecondsToFirstEmergency()
        {
            double lowestSeconds = double.MaxValue;
            foreach(Need need in needs)
            {
                double seconds = need.SecondsToEmergency();
                if (need.SecondsToEmergency() < lowestSeconds)
                {
                    lowestSeconds = need.SecondsToEmergency();
                }
            }
            return lowestSeconds;
        }

        private void CheckForTimePenalty()
        {
            double timePassed = Preferences.Get("secondsPassed", 0.0);
            Console.WriteLine($"{timePassed} seconds have passed!");
            ReceiveAllTimePenalties(timePassed);
        }

        private void InvokeCreatureChanged()
        {
            OnCreatureChanged?.Invoke();
        }

        private Need LowestNeed()
        {
            Need lowestNeed = null;
            double lowestValue = double.MaxValue;
            foreach (Need need in needs)
            {
                if (need.Value < lowestValue)
                {
                    lowestValue = need.Value;
                    lowestNeed = need;
                }
            }

            return lowestNeed;
        }
    }
}
