using CurrencyQuotesCore.Entites;
using System;

namespace CurrencyQuotesWpfUi {
    public class CurrencyPresenter {
        private readonly Currency _convertableCurrency;

        public Currency Currency { get; private set; }

        public bool IsSelected { get; set; }

        public string Id => Currency.Id;
        
        public string Code => Currency.Code;

        public string Designation => Currency.Designation;

        public int Nominal => Currency.Nominal;

        public string Name => Currency.Name;

        public double Value => Math.Round( Currency.Value / Nominal, 4 );

        public double PreviousValue => Currency.PreviousValue;

        public double ExchangeRateToConvertable {
            get {
                if ( _convertableCurrency == null )
                    return 0;

                double rate = Math.Round( ( _convertableCurrency.Nominal / _convertableCurrency.Value ) * Value, 4 );

                return rate;
            }
        }

        public CurrencyPresenter( Currency currency, Currency convertableCurrency ) {
            Currency = currency;
            _convertableCurrency = convertableCurrency;
        }        
    }
}
