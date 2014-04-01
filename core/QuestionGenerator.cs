using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;


namespace core
{
    public class QuestionGenerator
    {
        private static readonly string[] allNumbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private static readonly string[] noZeroNumbers = { "2", "3", "4", "5", "6", "7", "8", "9" };
        private static readonly string[] oddNumbers = { "3", "5", "7", "9", "7", "5", "9", "6", "9", "7" };

        public Question createQuestion(string operation, int digits)
        {
            int num1 = 0, num2 = 0;
            double doubleAnswer = 0;
            String answerStr = String.Empty;
            StringBuilder number;

            number = new StringBuilder();
            for (int i = 0; i < digits; i++)
            {
                number.Append(noZeroNumbers[RandomNumber(0, 8)]);
            }
            num1 = Convert.ToInt32(number.ToString());
            System.Threading.Thread.Sleep(new Random().Next(1, 5));

            number = new StringBuilder();
            for (int i = 0; i < digits; i++)
            {
                number.Append(oddNumbers[RandomNumber(0, 10)]);
            }
            num2 = Convert.ToInt32(number.ToString());

            if (num2 == 0)
                num2 += 1;

            switch (operation)
            {
                case "*":
                    answerStr = (num1 * num2).ToString();
                    break;
                case "+":
                    answerStr = (num1 + num2).ToString();
                    break;
                case "-":
                    answerStr = (num1 - num2).ToString();
                    break;
                case "/":
                    doubleAnswer = Convert.ToDouble(num1) / Convert.ToDouble(num2);
                    answerStr = String.Format("{0:0.##}", doubleAnswer);
                    break;
                default:
                break;
            }

            return new Question(num1.ToString() + " " + operation + " " + num2.ToString(), answerStr);
        }

        private int RandomNumber(int min, int max)
        {
            System.Threading.Thread.Sleep(new Random().Next(1,3));
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
