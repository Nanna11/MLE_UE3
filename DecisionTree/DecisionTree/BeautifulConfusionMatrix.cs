﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree
{
    public static class BeautifulConfusionMatrix
    {
        public static List<int> BiggestInRows(int[,] Confusion)
        {
            List<int> biggestOfRows = new List<int>();
            int TMPbiggest = 0;
            int rowCount = Confusion.GetLength(0);
            int columnCount = Confusion.GetLength(1);

            //todo: get rowcount of Confusion Matrix and delete all countOfMatrix to then add the rowcount there
            for(int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if(TMPbiggest == 0)
                        TMPbiggest = Confusion[i, j];
                    else if(Confusion[i, j] > TMPbiggest)
                    {
                        TMPbiggest = Confusion[i, j];
                    }
                }
                biggestOfRows.Add(TMPbiggest);
                TMPbiggest = 0;
            }

            return biggestOfRows;
        }

        public static void AddBlankSpaces(int[,] Confusion, int number, int column)
        {
            int amount = CalcNeededBlankSpaces(Confusion, number, column);
            
            for (int i = 0; i < amount; i++)
            {
                Console.Write(" ");
            }
        }

        private static int CalcNeededBlankSpaces(int[,] Confusion, int number, int column)
        {
            List<int> biggest = BiggestInRows(Confusion);
            int amountOfBiggest = 0;
            int amountOfNumber = 0;
            int tmp = biggest[column];

            if (number != biggest[column])
            {
                do
                {
                    tmp = tmp / 10;
                    amountOfBiggest++;
                } while (tmp > 0);

                do
                {
                    number = number / 10;
                    amountOfNumber++;
                } while (number > 0);
            }

            return amountOfBiggest - amountOfNumber;
        }
    }


}
