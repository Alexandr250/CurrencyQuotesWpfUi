using CurrencyQuotesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyQuotesWpfUi
{
    public class ReloadCurrenciesCommand : ICommand {
        IExchangeRatesRepository _exchangeRatesRepository;

        public event EventHandler CanExecuteChanged;

        public ReloadCurrenciesCommand(IExchangeRatesRepository exchangeRatesRepository) {
            _exchangeRatesRepository = exchangeRatesRepository;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _exchangeRatesRepository.Update();
        }
    }
}
