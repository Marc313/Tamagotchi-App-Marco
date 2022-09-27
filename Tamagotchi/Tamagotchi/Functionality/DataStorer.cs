using System;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Tamagotchi
{
    public class DataStorer
    {
        // Koppel aan app

        private string CreatureKey = "Creature";

        // CRUD Operations: Create, Read, Update, Delete
        public bool CreateData(Creature creature)
        {
            string creatureSerialized = JsonConvert.SerializeObject(creature);
            Preferences.Set(CreatureKey, creatureSerialized);

            return true;
        }

        public Creature ReadData()
        {
            string creatureSerialized = Preferences.Get(CreatureKey, "");

            Creature creature = null;

            try
            {
                creature = JsonConvert.DeserializeObject<Creature>(creatureSerialized);
            }
            catch (Exception e)
            {
                return null;
            }

            return creature;
        }

        public bool UpdateData(Creature creature)
        {
            if (Preferences.ContainsKey(CreatureKey))
            {
                string creatureSerialized = JsonConvert.SerializeObject(creature);
                Preferences.Set(CreatureKey, creatureSerialized);
                return true;
            }
            
            return false;
        }

        public bool DeleteData(Creature creature)
        {
            Preferences.Remove(CreatureKey);
            return true;
        }
    }
}
