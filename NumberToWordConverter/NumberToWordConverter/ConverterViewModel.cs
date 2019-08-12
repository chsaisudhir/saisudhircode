using System;
using System.ComponentModel;

namespace NumberToWordConverter
{
    public class ConverterViewModel : INotifyPropertyChanged
    {
        public string Number
        {
            get => _number;
            set
              {
                _number = value;
                OnPropertyChanged("Number");
                ConvertCommand.RaiseCanExecuteChanged(value);

            }
        }
        public string ConvertedText { get => _convertedText; set { _convertedText = value; OnPropertyChanged("ConvertedText"); } }
        public RelayCommand m_ConvertCommand = null;
        private string _number;
        private string _convertedText;

        public RelayCommand ConvertCommand
        {
            get
            {
                return m_ConvertCommand;
            }
            set
            {
                m_ConvertCommand = value;
            }
        }
        public ConverterViewModel()
        {
            ConvertCommand = new RelayCommand(ExecuteConversion, CanExecuteConversion);
        }

        private bool CanExecuteConversion(object arg)
        {
            if (!string.IsNullOrEmpty(Number) && !Number.StartsWith("0"))
            {
                if (int.TryParse(Number, out int _number) && _number < 999999999)
                {
                    return true;
                }
            }
            return false;
        }

        private void ExecuteConversion(object obj)
        {
            ConvertedText = ConverterUtility.ConvertToWords(Number);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
