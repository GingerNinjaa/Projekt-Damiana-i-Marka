using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Damian_I_Marek.Components
{
  public  class Word
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public char[] Characters { get; set; } //tablica znaków która przechowuje hasło w formacie [T][E][S][T]

        public Word(string value, string description)
        {

            this.Value = value.ToUpper(); //odpoweidź z pliku tekstowego też będzie wyświetlana z duzej litery
            this.Description = description; //Podpowiedź do pytania 
            this.Length = this.Value.Length; // Długość hasła z pliku tektsowego jest pobierana z zmiennej "Value.Length"
            this.Characters = this.Value.ToCharArray(); // przypisuje do tablicy hasło z pliku tekstowego

        }
    }
}
