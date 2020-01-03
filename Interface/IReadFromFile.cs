using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Damian_I_Marek.Interface
{
    /// <summary>
    /// Interface który zawiera metodę do czytania z pliku
    /// </summary>
   public interface IReadFromFile
    {
        //Metoda do czytania z pliku 
        void ReadWords(string path = "");
    }
}
