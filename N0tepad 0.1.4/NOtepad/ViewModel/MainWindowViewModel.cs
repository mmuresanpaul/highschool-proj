using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using OpenWeather.Model;
using OpenWeather.Command;
using OpenWeather.HelperClass;

namespace OpenWeather.ViewModel
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private readonly ICommand _weatherCommand;

        public ICommand WeatherCommand
        {
            get{ return _weatherCommand; }
        }

        public MainWindowViewModel()
        {
            _weatherCommand = new AsyncCommand(() => GetWeather(), () => CanGetWeather());
        }

        private List<WeatherDetails> _forecast = new List<WeatherDetails>();

        public List<WeatherDetails> Forecast
        {
            get { return _forecast; }
            set
            {
                _forecast = value;
                OnPropertyChanged("Forecast");
            }
        }

        private WeatherDetails _currentWeather = new WeatherDetails();

        public WeatherDetails CurrentWeather
        {
            get { return _currentWeather; }
            set
            {
                _currentWeather = value;
                OnPropertyChanged("CurrentWeather");
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        public async Task GetWeather()
        {
            List<WeatherDetails> weatherInfo = await Weather.GetWeather(Location);
            if (weatherInfo.Count != 0)
            {
                CurrentWeather = weatherInfo.First();
                Forecast = weatherInfo.Skip(1).ToList();
            }
        }

        public Boolean CanGetWeather()
        {
            if (Location != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}  