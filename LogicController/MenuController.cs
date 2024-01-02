using Microsoft.Extensions.Options;

namespace LogicController
{
    public struct Option
    {
        public string Display { get; }
        public Action Function { get; }
        public Option(string display, Action function)
        {
            Display = display;
            Function = function;
        }
    }

    public static class MenuController
    {
        public static void DisplayMenu(Option[] options, bool playersLoaded, bool shipsConfigured)
        {
            bool isValidInput = false;
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i].Display}");
            }

            int userInput;
            do
            {
                Console.WriteLine("Enter a the number of a displayed option: ");
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput < 1 || userInput > options.Length)
                    {
                        isValidInput = true;
                    }
                    if ((userInput == 1 && playersLoaded) || (userInput == 2 && (shipsConfigured || !playersLoaded)) || (userInput == 3 && (!shipsConfigured)))
                    {
                        Console.WriteLine("Option disabled");
                    }

                }
            }
            while (isValidInput);
            options[userInput - 1].Function.Invoke();
        }
    }
}
