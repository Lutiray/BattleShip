using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Ship
    {
        private const int deck = 4;
        private const int ships = 5;

        public char[,] RandomShipPlacement(char[,] field)//Arranges ships randomly
        {
            int position;
            int x, y;
            int validationResult;
            for (int i = deck; i > 0; i--) //The number of decks on the ship is taken as i
            {
                for (int j = ships - i; j > 0; j--)//The number of ships of the i deck ship is taken as j
                {
                    do
                    {
                        position = RandomForPosition();//Adjust the direction of the ship
                        y = RandomForCoordinates();
                        x = RandomForCoordinates();
                        validationResult = ValidateCoordForShip(field, x, y, position, i);//Checks the placement of ships
                    } while (validationResult != 0);

                    CheckPosition(position, x, y, i, field);//Puts the ship in accordance with the direction
                }
            }
            return field;
        }

        //Puts the ship in accordance with the direction
        public char[,] CheckPosition(int pos, int x, int y, int deck, char[,] field)
        {
            if (pos == 1)//if horizontal
            {
                for (int h = 0; h < deck; h++)
                {
                    field[y, x + h] = 'o';
                }
            }

            else if (pos == 2)//if vertical
            {
                for (int v = 0; v < deck; v++)
                {
                    field[y + v, x] = 'o';
                }
            }
            return field;
        }

        //Checks the placement of ships
        public int ValidateCoordForShip(char[,] field, int x, int y, int position, int shipType)
        {
            if (position == 1)//if horizontal
            {
                if ((x + shipType) < Map.length - 1)//does the ship go beyond the boundaries of the field
                {
                    for (int i = 0; i <= shipType; i++)
                    {
                        if ('o' == field[y, x + i]
                            || 'o' == field[y, x - 1] // checks right and left
                            || 'o' == field[y - 1, x - 1]//check on all diagonals
                            || 'o' == field[y - 1, x + i]//and from
                            || 'o' == field[y + 1, x - 1]//top
                            || 'o' == field[y + 1, x + i]//to bottom
                            || 'o' == field[y, x + i - 1])//check that the end does not boorder with another ship
                        {
                            return -1;
                        }
                    }
                }
                else return -1;
            }

            else if (position == 2)//if vertical
            {
                if ((y + shipType) < Map.length - 1)//does the ship go beyond the boundaries of the field
                {
                    for (int i = 0; i <= shipType; i++)
                    {
                        if ('o' == field[y + i, x]
                            || 'o' == field[y - 1, x]// checks right and left
                            || 'o' == field[y - 1, x - 1]//check on all diagonals
                            || 'o' == field[y + i, x - 1]//and from
                            || 'o' == field[y - 1, x + 1]//top
                            || 'o' == field[y + i, x + 1]//to bottom
                            || 'o' == field[y + i - 1, x])//check that the end does not boorder with another ship
                        {
                            return -1;
                        }
                    }
                }
                else return -1;
            }
            return 0;
        }

        private int RandomForPosition()//Random for ship direction
        {
            int random = new Random().Next(1, 3);
            return random;
        }

        public int RandomForCoordinates()//Random for ship coordinates
        {
            int random = new Random().Next(1, 11);
            return random;
        } 
    }
}
