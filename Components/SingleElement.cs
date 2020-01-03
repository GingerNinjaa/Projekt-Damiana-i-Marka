using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Damian_I_Marek.Components
{
    /// <summary>
    /// Klasa łącznik  która pobiera słowo z pliku tekstowego i koordynatey liter
    /// </summary>
     public class SingleElement
    {
        public Coordinate_Info[] Coordinate_Info { get;  set; }
        public Word Word { get; set; }

        public SingleElement(Coordinate_Info[] coordinate_Info, Word word)
        {
            this.Coordinate_Info = coordinate_Info;
            this.Word = word;
        }
    }
}
