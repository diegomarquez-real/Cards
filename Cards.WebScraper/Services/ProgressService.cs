using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper.Services
{
    public class ProgressService : Abstractions.IProgressService
    {
        public void ProgressBar(int current, int total)
        {
            int progressBarSize = 40, position = 0;
            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = progressBarSize;
            Console.Write("]");
            Console.CursorLeft = 1;
            float currentBarPercent = ((float)progressBarSize / total) * current;

            for (int i = 0; i < currentBarPercent; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = ++position;

                if (i == currentBarPercent - 1)
                {
                    Console.CursorLeft = 1;
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            int currentPercent = (int)((float)current / total * 100);
            Console.CursorLeft = progressBarSize + 2;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(String.Format("{0}%{1}", currentPercent, current == total ? "\n" : ""));
        }
    }
}