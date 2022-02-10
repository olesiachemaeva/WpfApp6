using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        //public void InitializeComponent();
        {
            InitializeComponent();
        }
        public class WeatherControl : DependencyObject
        {
            public static readonly DependencyProperty temperatureProperty;
            public int temperature
            {
                get => (int)GetValue(temperatureProperty);
                set => SetValue(temperatureProperty, value);
            }
            static string[] WindDirection =
            {
            "Северный", 
                "Северо-Восточный", 
                "Восточный", 
                "Юго-Восточный", 
                "Южный", 
                "Юго-Западный", 
                "Западный", 
                "Северо-Западный",
        };
            string direction;
            public string windDirection
            {
                get
                {
                    return direction;
                }
                set
                {
                    for (int i = 0; i < WindDirection.Length; ++i)
                        if (value.Equals(WindDirection[i], StringComparison.CurrentCultureIgnoreCase))
                        {
                            direction = value;
                            return;
                        }
                }
            }
            public int windSpeed { get; set; }
            static string[] WeatherType =
            {
            "Солнечно", 
                "Облачно" , 
                "Дождь" , 
                "Снег"
        };
            string weather;
            public enum Precipitation : int
            {
                Солнечно, 
                Облачно, 
                Дождь, 
                Снег
            }
            public string precipitation { get; set; }
            public WeatherControl(int temperature, string windDirection, int windSpeed, string precipitation)
            {
                this.temperature = temperature;
                this.windDirection = windDirection;
                this.windSpeed = windSpeed;
                this.precipitation = Enum.GetName(typeof(Precipitation), precipitation);
            }
            static WeatherControl()
            {
                temperatureProperty = DependencyProperty.Register(
                        nameof(temperature),
                        typeof(int),
                        typeof(WeatherControl),
                        new FrameworkPropertyMetadata(
                            0,
                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                            FrameworkPropertyMetadataOptions.AffectsRender,
                            null,
                            new CoerceValueCallback(CoerceTemperatyre)),
                        new ValidateValueCallback(ValidateTemperature));
            }
            private static bool ValidateTemperature(object value)
            {
                int v = (int)value;
                if (v > -50 && v < 55)
                    return true;
                else
                    return false;
            }
            private static object CoerceTemperatyre(DependencyObject d, object baseValue)
            {
                int v = (int)baseValue;
                if (v > -51 && v < 51)
                    return v;
                else
                    return 0;
            }
        }
    }
}
