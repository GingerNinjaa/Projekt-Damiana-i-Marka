using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Damian_I_Marek.Components
{
    /// <summary>
    /// Klasa która przypisuje litery do pól w krzyżówce
    /// </summary>
  public  class Board_Info
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public char[,] BoardArea { get; set; }
        public SingleElement[] SingleElements { get; set; }

        public Board_Info(int row, int column,SingleElement[] singleElements)
        {
            if (row < 0 || column < 0)
                throw new ArgumentException($"Given rows or columns are negative. Rows: {row}, columns: {column}."); // zabezpieczenie przed podaniem ujemnej liczby kolumn i wierszy


            this.Row = row;
            this.Column = column;
            this.SingleElements = singleElements;

            this.BoardArea = new char[this.Row, this.Column];   //Przypisuje wartośc przestrzeni z kolumnami i wierszami 


            FillBoardArea();
        }

        /// <summary>
        /// Metoda która w 2 pętlach foreach przypisuje każdej literze jeje koordynate
        /// </summary>
        void FillBoardArea() 
        {
            foreach (var element in SingleElements)
            {
                foreach (var item in element.Coordinate_Info)
                {
                    BoardArea[item.Coordinate.X, item.Coordinate.Y] = item.Letter;
                    // przypisuje każdej literze określone parametry
                    //przykład

                    /*
                     *   0  1  2  3  4  5  6  7  8  9  10
                     *  0[] [] [t] [e] [s] [t] [] [] [] [] []
                     *  1[] [] [] [] [] [] [] [] [] [] []
                     *  2[] [] [] [] [] [] [] [] [] [] []
                     * 
                     */

                }
            }
        }
    }
}
