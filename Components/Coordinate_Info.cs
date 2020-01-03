using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projekt_Damian_I_Marek.Components
{
   public class Coordinate_Info
    {
        public char Letter { get; set; }
        public bool Passwordchcecker { get; set; }
        public Point Coordinate { get; set; }

        public Coordinate_Info(char letter, bool passwordchecker,Point coordinate)
        {
            this.Letter = letter;
            this.Passwordchcecker = passwordchecker;
            this.Coordinate = coordinate;
        }
    }
}
