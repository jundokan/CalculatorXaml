using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModel
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        string _resultText = "";
        double _temporaryResult = 0;
        string operation = "";
                
        public CalculatorViewModel()
        {
            NumberCommand = new Command<string>(NumberClicked);
            AddCommand = new Command(AddClicked);
            SubtractCommand = new Command(CubtractClicked);
            MultiplyCommand = new Command(MultiplyClicked);
            ResultCommand = new Command(ResultClicked);
            DoubleDotCommand = new Command(DoubleDotClicked);
            ClearCommand = new Command(ClearClicked);
            BackspaceCommand = new Command(BackspaceClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NumberCommand { protected set; get; } 
        public ICommand AddCommand { protected set; get; }
        public ICommand SubtractCommand { protected set; get; }
        public ICommand MultiplyCommand { protected set; get; }
        public ICommand ResultCommand { protected set; get; }
        public ICommand DoubleDotCommand { protected set; get; }
        public ICommand ClearCommand { private set; get; }
        public ICommand BackspaceCommand { private set; get; }


        public string ResultText
        {
            protected set
            {
                if (_resultText != value)
                {
                    _resultText = value;
                    OnPropertyChanged("ResultText");
                }
            }
            get { return _resultText; }
        }

        public double TemporaryResult
        {
            protected set
            {
                if (_temporaryResult != value)
                {
                    _temporaryResult = value;
                    OnPropertyChanged("TemporaryResult");
                }
            }
            get { return _temporaryResult; }
        }

        private void NumberClicked(string number)
        {   
            if (String.IsNullOrEmpty(ResultText) || ResultText == "0")
            {
                ResultText = number;
            }
            else
                ResultText += number;
        }

        private void AddClicked(object obj)
        {
            TemporaryResult = Double.Parse(ResultText);
            operation = "+";
            ResultText = "0";
        }

        private void CubtractClicked(object obj)
        {
            TemporaryResult = Double.Parse(ResultText);
            operation = "-";
            ResultText = "0";
        }

        private void MultiplyClicked(object obj)
        {
            TemporaryResult = Double.Parse(ResultText);
            operation = "*";
            ResultText = "0";
        }

        private void ResultClicked()
        {
            switch (operation)
            {
                case "+":
                    TemporaryResult += Double.Parse(ResultText);                    
                    break;
                case "-":
                    TemporaryResult -= Double.Parse(ResultText);                    
                    break;
                case "*":
                    TemporaryResult *= Double.Parse(ResultText);                    
                    break;
                default:
                    break;
            }

            ResultText = TemporaryResult.ToString();          
        }

        private void DoubleDotClicked(object obj)
        {
            if(!ResultText.Contains("."))
            {
                ResultText += ".";
            }
        }

        private void ClearClicked(object obj)
        {
            TemporaryResult = 0;
            ResultText = "0";
        }

        private void BackspaceClicked()
        {
            ResultText = ResultText.Substring(0, ResultText.Length - 1);

            if(ResultText.Length == 0)
            {
                ResultText = "0";
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
