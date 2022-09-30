using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi
{
    public static class CreatureStateResponse
    {
        private static string HappyImage => "KPHappy.png";
        private static string TiredImage => "KPTired.png";
        private static string BoredImage => "KPBored.webp";
        private static string UndernourishedImage => "KPThirsty.png";

        private static string HappyText => "Your tamagotchi is happy!";
        private static string TiredText => "Your tamagotchi is getting tired. Put him to bed or give it some time alone.";
        private static string BoredText => "Your tamagotchi is bored. Play with it or invite a friend.";
        private static string UndernourishedText => "Your tamagotchi wants some sustanance. Give it some food or a drink.";

        public static string GetImageSourceFromState(Creature.CreatureState state)
        {
            switch (state)
            {
                case Creature.CreatureState.HAPPY:
                    return HappyImage;
                case Creature.CreatureState.TIRED:
                    return TiredImage;
                case Creature.CreatureState.BORED:
                    return BoredImage;
                case Creature.CreatureState.UNDERNOURISHED:
                    return UndernourishedImage;
                default: return HappyImage;
            }
        }

        public static string GetTextFromState(Creature.CreatureState state)
        {
            switch (state)
            {
                case Creature.CreatureState.HAPPY:
                    return HappyText;
                case Creature.CreatureState.TIRED:
                    return TiredText;
                case Creature.CreatureState.BORED:
                    return BoredText;
                case Creature.CreatureState.UNDERNOURISHED:
                    return UndernourishedText;
                default: return HappyText;
            }
        }
    }
}
