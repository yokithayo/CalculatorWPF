using System;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using CalculatorWPF.Models;
using System.Windows;

namespace CalculatorWPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly CalculateModel calculator = new CalculateModel();
        public ICommand NumButtonCommand => new DelegateCommand<string>(NumButtonPress);
        public ICommand DeleteButtonCommand => new DelegateCommand<string>(DeleteButtonPress);
        public ICommand OperationButtonCommand => new DelegateCommand<string>(OperationButtonPress);
        public ICommand MinimizeWindowCommand => new DelegateCommand(MinimizeWindow);
        public ICommand MaximizeWindowCommand => new DelegateCommand(MaximizeWindow);
        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        private string display;
        private string expression;
        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                RaisePropertyChanged("Display");
            }
        }
        public string Expression
        {
            get { return expression; }
            set
            {
                expression = value;
                RaisePropertyChanged("Expression");
            }
        }

        public MainViewModel()
        {
            display = "0";
            calculator.FirstOperand = string.Empty;
            calculator.SecondOperand = string.Empty;
            calculator.Operation = string.Empty;
            expression = string.Empty;
        }
        private void NumButtonPress(string button)
        {
            switch (button)
            {
                case ".":

                    if (!display.Contains("."))
                    {
                        Display += ",";
                    }
                    break;
                case "+/-":
                    if (display.StartsWith("-"))
                    {
                        Display = display.Substring(1);
                    }
                    else
                    {
                        Display = "-" + display;
                    }
                    break;
                default:
                    if (display == "0")
                    {
                        Display = button;
                    }
                    else
                    {
                        Display += button;
                    }
                    break;
            }
        }
        private void DeleteButtonPress(string button)
        {
            switch (button)
            {
                case "C":
                    Display = "0";
                    Expression = string.Empty;
                    calculator.FirstOperand = string.Empty;
                    calculator.SecondOperand = string.Empty;
                    calculator.Operation = string.Empty;
                    break;
                case "⌫":
                    if (display.Length > 1)
                    {
                        Display = display.Substring(0, display.Length - 1);
                    }
                    else
                    {
                        Display = "0";
                    }
                    break;
            }
        }
        private void OperationButtonPress(string operation)
        {
            if (operation == "=")
            {
                calculator.SecondOperand = Display;
                try
                {
                    Display = calculator.Calculate();
                    Expression += calculator.SecondOperand + "=";
                }
                catch (Exception ex)
                {
                    Display = "Error";
                    Expression = ex.Message;
                }
                calculator.Operation = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(calculator.Operation))
                {
                    Expression = Expression.Substring(0, Expression.Length - 1);
                }
                if (calculator.Operation == string.Empty)
                {
                    Expression = string.Empty;
                    calculator.FirstOperand = Display;
                    Expression += Display + operation;
                    Display = "0";
                }
                else
                {
                    calculator.SecondOperand = Display;
                    Expression += Display + operation;
                    try
                    {
                        Display = calculator.Calculate();
                        Expression = Display + operation;
                    }
                    catch (Exception ex)
                    {
                        Display = "Error";
                        Expression = ex.Message;
                    }
                }
                calculator.Operation = operation;
            }
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void MaximizeWindow()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
    }
}