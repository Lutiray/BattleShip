using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Shots
    {
        Map map = new Map();

        private void SetShot(char[,] field, int x, int y)
        {
            if (field[y, x] == 'o')
            {
                field[y, x] = '*';
            }
            else
            {
                field[y, x] = 'x';
            }
        }

        public void SetMyShot(char[,] field, int x, int y)//Checks if the enemy shot hit
        {
            SetShot(field, x, y);
            map.PrintField(field);
        }

        public void SetEnemyShot(char[,] field, int x, int y)//Checks if the my shot hit
        {
            SetShot(field, x, y);
            map.PrintEnemyField(field);
        }

        public bool CheckShips(char[,] field)//Checks if there are still ships that have not been sunk
        {
            return field.Cast<char>().Any(c => c == 'o');
        }

        public bool HasMyShotBeenFired(int[,] check, int x, int y)//Checks whether a my shot has already been fired
        {
            if (check[y, x] == 0)
            {
                check[y, x] = 1;
                return true;
            }
            return false;
        }

        public bool HasEnemyShotBeenFired(int[,] check, int x, int y)//Checks whether a enemy shot has already been fired
        {
            return HasMyShotBeenFired(check, x, y);
        }
    }
}
