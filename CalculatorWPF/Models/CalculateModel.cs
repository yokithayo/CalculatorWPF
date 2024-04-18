using System;

namespace CalculatorWPF.Models
{
    public class CalculateModel
    {
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Calculate()
        {
            double firstOperand = Convert.ToDouble(FirstOperand);
            double secondOperand = Convert.ToDouble(SecondOperand);
            try
            {
                switch (Operation)
                {
                    case "+":
                        return (firstOperand + secondOperand).ToString();
                    case "-":
                        return (firstOperand - secondOperand).ToString();
                    case "×":
                        return (firstOperand * secondOperand).ToString();
                    case "÷":
                        if (secondOperand != 0)
                        {
                            return (firstOperand / secondOperand).ToString();
                        }
                        else
                        {
                        throw new DivideByZeroException("Division by zero"); 
                        }      
                    default:
                        throw new ArgumentException("Invalid operation");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
    }
}
