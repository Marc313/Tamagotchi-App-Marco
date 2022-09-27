using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovingImage : ContentView
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(MovingImage));

        public string Source
        {
            get => GetValue(SourceProperty) as string;
            set => SetValue(SourceProperty, value);
        }

        public MovingImage()
        {
            InitializeComponent();
            MovingButton.Source = Source;
            StartButtonAnimation();
        }

        private async void StartButtonAnimation()
        {
            await MovingButton.TranslateTo(-100, 0, 500);
            await MovingButton.TranslateTo(100, 0, 500);
            StartButtonAnimation();
        }
    }
}