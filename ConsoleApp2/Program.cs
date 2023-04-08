using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace BattleShip
{

    class Program
    {
        static void Main(string[] arg)
        {
            char choice;
            Map map = new Map();
            Ship ship = new Ship();

            Console.WriteLine("Hi! Welcome to the game \"Sea Battle\"");
            Console.Write("Enter your name: ");
            do
            {
                Console.WriteLine("If you want to get acquainted with the rules of the game press [R]");
                Console.WriteLine("If you're ready to start press [S]");
                Console.WriteLine("To exit, press [E]");
                Console.Write("Enter your choice: ");
                choice = char.ToLower(Console.ReadKey().KeyChar);
                Console.ReadKey();
                Console.WriteLine("");

                switch (choice)
                {
                    case 'r':
                        Rules();
                        Console.WriteLine();
                        break;
                    case 's':
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("If you want to arrange the ships youself, press [I]");
                            Console.WriteLine("If you want to arrange the ships randomly, press [R]");
                            Console.Write("Enter your choice: ");
                            choice = char.ToLower(Console.ReadKey().KeyChar);
                            Console.ReadKey();
                            Console.WriteLine("");
                            switch (choice)
                            {
                                case 'i':
                                    FillPlayerField(map.MyField);
                                    Console.WriteLine("");
                                    map.EnemyField = ship.RandomShipPlacement(map.EnemyField);//Arranges ships randomly
                                    map.PrintEnemyField(map.EnemyField);
                                    break;
                                case 'r':
                                    map.MyField = ship.RandomShipPlacement(map.MyField);
                                    map.PrintField(map.MyField);
                                    Console.WriteLine("");
                                    map.EnemyField = ship.RandomShipPlacement(map.EnemyField);
                                    map.PrintEnemyField(map.EnemyField);
                                    break;
                                default:
                                    Console.WriteLine("Wrong choice! Try again.");
                                    break;
                            }
                        } while (choice != 'i' && choice != 'r');
                        Fire(map.MyField, map.EnemyField); //Makes a shot
                        break;
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Try again.");
                        Console.ReadKey();
                        break;
                }

            }
            while (choice != 'e');
        }

        public static void Rules() //Output the rules of the game from the files
        {
            string path = "C:\\Users\\user\\source\\repos\\BattleShip\\BattleShip\\Rules of Sea War.docx";
            string[] words = File.ReadAllLines(path);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        public static int AnalyzeInputLetters(char input) //Converts a letter to a number
        {
            int y = 0;
            char[] letters = new char[] { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            for (int i = 0; i < letters.Length; i++)//Iterates through the array, if there are is an entered
                                                    //letter, then the array index is output
            {
                if (letters[i] == input)
                {
                    y = i;
                    return y;
                }
            }
            return -1;
        }

        public static void AnalyzeInputNumbers(string? number, out int y)//Checks whether th digit is entered
        {
            bool isNumber = int.TryParse(number, out y);
        }

        public static bool CheckCoordinates(int x, int y)
        {
            if (y < 1 || y > 10 || x == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect input! Try again!");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        private static void FillPlayerField(char[,] myField)//Enetring ship positions manually
        {
            Map map = new Map();
            Ship ship = new Ship();
            int x, y, position;

            map.PrintField(myField);

            for (int i = 4; i > 0; i--) //The number of decks on the ship is taken as i
            {
                for (int j = 5 - i; j > 0; j--)//The number of ships of the i deck ship is taken as j
                {
                    Console.WriteLine("Arragment of a {0} - deck ship. It remain to arrange the {1} ships", i, j);

                    int validationResult = 1;

                    do
                    {
                        do
                        {
                            Console.WriteLine("Enter your coordinates separated by a enter (ex. A1): ");
                            x = AnalyzeInputLetters(char.ToLower(Console.ReadKey().KeyChar));
                            AnalyzeInputNumbers(Console.ReadLine(), out y);
                        }
                        while (!CheckCoordinates(x, y));//Checks whether the input does not go beyond the boundaries

                        Console.WriteLine("1 - horizontal, 2 - vertical?");
                        AnalyzeInputNumbers(Console.ReadLine(), out position);
                        if (position == 1 || position == 2)
                        {
                            //Checks the placement of ships
                            validationResult = ship.ValidateCoordForShip(myField, x, y, position, i);
                            if (validationResult != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Worng position. Try again!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Incorrect input! Try again!");
                            Console.ResetColor();
                        }
                    }
                    while (validationResult != 0);

                    ship.CheckPosition(position, x, y, i, myField);//Puts the ship in accordance with the direction

                    Console.Clear();

                    map.PrintField(myField);
                }
            }
        }

        public static void Fire(char[,] myField, char[,] enemyField)
        {
            Ship ship = new Ship();
            Map map = new Map();
            Shots shot = new Shots();
            int[,] memorizingMyShots = new int[11, 11];
            int[,] memorizingEnemyShots = new int[11, 11];
            bool comment_for_me, comment_for_enemy, check;
            int x, y, a, z, amount_of_moves = 0;
            do
            {
                do//The user takes a shot
                {
                    do
                    {
                        Console.WriteLine("Enter firing position: ");
                        x = AnalyzeInputLetters(char.ToLower(Console.ReadKey().KeyChar));
                        AnalyzeInputNumbers(Console.ReadLine(), out y);
                    }
                    while (!CheckCoordinates(x, y));
                    check = shot.HasMyShotBeenFired(memorizingMyShots, x, y);
                    if (!check)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This coordinate already being shot.");
                        Console.ResetColor();
                    }
                }
                while (!check);

                amount_of_moves++; //Сколько шагов

                Console.WriteLine();

                do//the computer takes a shot
                {
                    a = ship.RandomForCoordinates();
                    z = ship.RandomForCoordinates();
                    check = shot.HasEnemyShotBeenFired(memorizingEnemyShots, a, z);
                }
                while (!check);

                Console.Clear();

                shot.SetMyShot(myField, a, z);
                Console.WriteLine();
                shot.SetEnemyShot(enemyField, x, y);
                comment_for_me = shot.CheckShips(myField);
                comment_for_enemy = shot.CheckShips(enemyField);

                //Checks who else has the remaining ships
                if (comment_for_me == true && comment_for_enemy == false)//
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Game Over! \nCongratulations! You win!");
                    Console.WriteLine("It took you {0} moves to do this!", amount_of_moves);
                    Console.ResetColor();
                }
                if (comment_for_me == false && comment_for_enemy == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Game Over! \nSorry! You lose!");
                    Console.ResetColor();
                }
            }
            while (comment_for_me != false || comment_for_enemy != false);
        }
    }
}

