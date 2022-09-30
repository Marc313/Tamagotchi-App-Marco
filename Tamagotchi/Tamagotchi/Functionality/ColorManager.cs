using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tamagotchi
{
    public static class ColorManager
    {
        private static Color HealthyColor => Color.LimeGreen;
        private static Color NotGreatColor => Color.FromHex("a6f67a");
        private static Color DangerColor => Color.Orange;
        private static Color EmergencyColor => Color.Red;

        public static Color GetColorFromState(Need.State state)
        {
            switch (state)
            {
                case Need.State.HEALTHY:
                    return HealthyColor;
                case Need.State.NOTGREAT:
                    return NotGreatColor;
                case Need.State.DANGER:
                    return DangerColor;
                case Need.State.EMERGENCY:
                    return EmergencyColor;
                default:
                    return HealthyColor;
            }
        }
    }
}
