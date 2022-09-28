using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tamagotchi
{
    public class StyleManager
    {
        public Style HealthyStyle { get; private set; }
        public Style NotGreatStyle { get; private set; }
        public Style DangerStyle { get; private set; }
        public Style EmergencyStyle { get; private set; }

        public StyleManager()
        {
            HealthyStyle = Application.Current.Resources["HealthyStyle"] as Style;
            NotGreatStyle = Application.Current.Resources["NotGreatStyle"] as Style;
            DangerStyle = Application.Current.Resources["DangerStyle"] as Style;
            EmergencyStyle = Application.Current.Resources["EmergencyStyle"] as Style;
        }

        public Style GetStyleFromState(Need.State state)
        {
            switch (state)
            {
                case Need.State.HEALTHY:
                    return HealthyStyle;
                case Need.State.NOTGREAT:
                    return NotGreatStyle;
                case Need.State.DANGER:
                    return DangerStyle;
                case Need.State.EMERGENCY:
                    return EmergencyStyle;
                default:
                    return HealthyStyle;
            }
        }
    }
}
