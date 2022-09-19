using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.Views.homes
{
    public partial class MessagePageViewModel : INotifyPropertyChanged
    {
        Color color;
        string name;
        float hue;
        float saturation;
        float luminosity;

        public event PropertyChangedEventHandler PropertyChanged;

        public float Hue
        {
            get
            {
                return hue;
            }
            set
            {
                if (hue != value)
                {
                    Color = Color.FromHsla(value, saturation, luminosity);
                }
            }
        }

        public float Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                if (saturation != value)
                {
                    Color = Color.FromHsla(hue, value, luminosity);
                }
            }
        }

        public float Luminosity
        {
            get
            {
                return luminosity;
            }
            set
            {
                if (luminosity != value)
                {
                    Color = Color.FromHsla(hue, saturation, value);
                }
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    hue = color.GetHue();
                    saturation = color.GetSaturation();
                    luminosity = color.GetLuminosity();
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hue"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Saturation"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Luminosity"));
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));

                    Name = color.ToString();
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

    }
}
