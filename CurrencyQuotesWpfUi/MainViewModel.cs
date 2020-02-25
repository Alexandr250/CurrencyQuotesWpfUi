using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CurrencyQuotesCore;

namespace CurrencyQuotesWpfUi
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IExchangeRatesRepository _repository;
        private string _filter = string.Empty;

        public string Filter {
            get => _filter;
            set {
                _filter = value;

                OnPropertyChanged(nameof(Filter));
                OnPropertyChanged(nameof(Currencies));
            }
        }

        public IEnumerable<CurrencyPresenter> Currencies {
            get {
                return _repository.WithFilter(_filter).Select(c => new CurrencyPresenter(c, _repository.WithFilter("usd").FirstOrDefault()));
            }
        }

        public ICommand UpdateCommand { get; }

        public MainViewModel() {
            _repository = new ExchangeRatesLocalRepository(new System.IO.FileInfo("daily.json"));
            UpdateCommand = new ReloadCurrenciesCommand(_repository);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region Test from ConverterViewModel
        private CurrencyPresenter _selectedCurrencyFrom;
        private CurrencyPresenter _selectedCurrencyTo;
        private double _valueFrom;

        public CurrencyPresenter SelectedCurrencyFrom {
            get {
                return _selectedCurrencyFrom;
            }
            set {
                _selectedCurrencyFrom = value;
                OnPropertyChanged(nameof(SelectedCurrencyFrom));
                OnPropertyChanged(nameof(ConvertResult));
            }
        }

        public CurrencyPresenter SelectedCurrencyTo {
            get {
                return _selectedCurrencyTo;
            }
            set {
                _selectedCurrencyTo = value;
                OnPropertyChanged(nameof(SelectedCurrencyTo));
                OnPropertyChanged(nameof(ConvertResult));
            }
        }

        public double ValueFrom {
            get {
                return _valueFrom;
            }
            set {
                _valueFrom = value;
                OnPropertyChanged(nameof(ValueFrom));
                OnPropertyChanged(nameof(ConvertResult));
            }
        }

        public string ConvertResult {
            get {
                if (SelectedCurrencyFrom == null || SelectedCurrencyTo == null)
                    return "0.0";

                return Math.Round(ValueFrom * ((SelectedCurrencyFrom.Value / SelectedCurrencyFrom.Nominal) / (SelectedCurrencyTo.Value / SelectedCurrencyTo.Nominal)), 4).ToString();
            }
        }
        #endregion
    }
}
