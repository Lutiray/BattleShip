using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Map
    {
        public const int length = 12;
        public char[,] MyField = new char[length, length];
        public char[,] EnemyField = new char[length, length];

        private char[,] FillingOutTheMap(char[,] field)//Print header: number of row and letters of columns
        {
            char headerLetters = 'A';
            char headerNumbers = '1';
            field[0, 0] = '0';
            for (int i = 1; i < length - 1; i++, headerLetters++, headerNumbers++)
            {
                field[0, i] = headerLetters;
                field[i, 0] = headerNumbers;
            }
            return field;
        }

        public void PrintField(char[,] field)//Print the title and users field
        {
            FillingOutTheMap(field);
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    if (field[i, j] == 'o')//if there is a ship
                        Console.Write(" o ");
                    else if (field[i, j] == 0)//if there is water
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" . ");
                        Console.ResetColor();
                    }
                    else if (field[j, i] == field[0, 10])
                    {
                        Console.Write("10 ");
                    }
                    else
                    {
                        Console.Write(" {0} ", field[i, j]);//if there is a shot or is a header
                    }
                }
                Console.WriteLine();
            }
        }

        public void PrintEnemyField(char[,] field)//Print the title and enemy field
        {
            FillingOutTheMap(field);
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    if (field[i, j] == 'o' || field[i, j] == 0) //If there is water or a ship
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" . ");
                        Console.ResetColor();
                    }
                    else if (field[j, i] == field[0, 10])
                    {
                        Console.Write("10 ");
                    }
                    else
                    {
                        Console.Write(" {0} ", field[i, j]);//if there is a shot
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
