using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Projekt_Damian_I_Marek
{
    class Engine : Interface.IReadFromFile
    {

        public List<Components.Word> Words { get; set; }
        private Components.Board_Info board;

        //TODO OPISAĆ CO TO JEST
        public Engine()
        {
            Words = new List<Components.Word>();
        }


        /// <summary>
        /// Czyta dane z pliku
        /// </summary>
        /// <param name="path"></param>
        public void ReadWords(String path = "")
        {
            try
            {
                // przypisuje zawartość pliku tekstowego do zmiennej "allLine"
                var allLine = File.ReadAllLines(path);

                foreach (var line in allLine)
                {
                    var allWords = line.Split(new string[] { ";" }, StringSplitOptions.None);

                    for (var i = 0; i < allWords.Length - 1; i+=2) // -1 bo wywala poza tablice 
                    {
                        /*  Dodaje do listy Words Klase Word 
                         *  word przyjmuje 2 parametry
                         *  Word(Value, Odpowiedź)
                         */

                        //Words.Add(new Components.Word(allWords[i + 1], allWords[i]));
                        Words.Add(new Components.Word(allWords[i ], allWords[i +1]));
                    }

                }

            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException($"Nie mogę znaleść pliku {e.FileName}");
            }

        }

        /// <summary>
        /// Przesówa współrzędne 
        /// </summary>
        /// <param name="elements"></param>
        public void ElementsIn(Components.SingleElement[] elements)
        {
            //przesunięcie współrzędniej y
            var shift = Math.Abs(elements.SelectMany(x => x.Coordinate_Info)
                                 .Min(x => x.Coordinate.Y));

            foreach (var item in elements)
            {
                foreach (var item2 in item.Coordinate_Info)
                {
                    item2.Coordinate = new Point(item2.Coordinate.X, item2.Coordinate.Y + shift);
                }
            }
        }

        public Components.Coordinate_Info[] First_XY_Strait_Crossword(Components.Word word, char password)
        {
            // zmienna 
            var coordinate_Info = new List<Components.Coordinate_Info>();
            var passLetterWasSet = false;

            for (int i = 0; i < word.Value.Length; i++)
            {
                //zmienna przechowuje litere z hasła
                var letter = word.Value[i];

                var passwordchcecker = false;

                //Sprawdza czy zmienna letter (która przechowuje Litere :0)
                if (!passLetterWasSet && password.Equals(letter))
                {
                    passwordchcecker = true;
                    passLetterWasSet = true;
                }

                /*
                 * Ta zmienna przechowuje klase Coordinate_Info
                 * klasa przechowuje Litere, bool czy to jeest litera,i jej koordynaty w pierwszej lini 
                 * dlatego mamy (0,i) 
                 */
                var coordinateinfo = new Components.Coordinate_Info(letter, passwordchcecker, new Point(0, i));

                /*
                 * dodajemy do tablicy coordinate_Info
                 *  zmienna coordinateinfo
                 * 
                 */
                coordinate_Info.Add(coordinateinfo);

            }

            //zwraca tablice z współrzędnymi 
            return coordinate_Info.ToArray();
        }
        public Components.Coordinate_Info[] Next_XY_Strait_Crossword(Point LastCoordinatePass, Components.Word word, char password)
        {
            // opisać 
            var coordinate_Info = new List<Components.Coordinate_Info>();

            var currentCoordPass = new Point();

            var currentCoordPassCounter = 0;

            for (int i = 0; i < word.Value.Length; i++)
            {
                var letter = word.Value[i];

                //Sprawdza czy zmienna letter (przechowuje litere)
                if (password.Equals(letter))
                {
                    currentCoordPass = new Point(LastCoordinatePass.X + 1, i);
                    currentCoordPassCounter = i;
                    break;
                }
            }

            var fakeCounter = 0;
            var passLetterWasSet = false;
            var startY = LastCoordinatePass.Y - currentCoordPass.Y;
            for (var i = startY; i < word.Value.Length + startY; i++)
            {
                var letter = word.Value[fakeCounter];
                var passwordchecker = false;

                if (!passLetterWasSet && fakeCounter == currentCoordPassCounter)
                {
                    passwordchecker = true;
                    passLetterWasSet = true;
                }

                var coordInfo = new Components.Coordinate_Info(letter, passwordchecker, new Point(LastCoordinatePass.X + 1,i));

                coordinate_Info.Add(coordInfo);

                fakeCounter++;
            }


            return coordinate_Info.ToArray();
        }
        public Components.Coordinate_Info[] Create_Coordinate_Strait_Crossword(List<Components.SingleElement> element, Components.Word word, char password)
        {
            Components.Coordinate_Info LastCoordinatePass = null;

            if (element.Count > 0)
            {
                LastCoordinatePass = element[element.Count - 1]
                    .Coordinate_Info.Select(c => c)
                    .Where(c => c.Passwordchcecker)
                    .FirstOrDefault(); 
            }

            if (LastCoordinatePass == null)
            {
                return First_XY_Strait_Crossword(word, password);
            }
            else
            {
                return Next_XY_Strait_Crossword(LastCoordinatePass.Coordinate, word, password);
            }

        }

        //********************************
        public void Strait_Crossword(string password , List<Components.SingleElement> element)
        {
            // tworzenie zmiennej losowej
            var random = new Random();

            foreach(var letter in password.ToCharArray())
            {
                var word = Words.Select(v => v)
                    .Where(v => v.Characters.Contains(letter) 
                    && v.Length < 15)
                    .OrderBy(v => random.Next()).FirstOrDefault();

                if (word == null)
                {
                    //wyłapuje czy w haśle znajduje sie litera która nie może być hasłem
                    throw new Exception($"Litera: '{letter}' nie istnieje w bazie danych.");
                }

                var coordinateInfo = Create_Coordinate_Strait_Crossword(element, word, letter);

                element.Add(new Components.SingleElement(coordinateInfo, word));
            }
        }

        public Components.SingleElement[] GetElement(Components.Password_Info password)
        {
            var elements = new List<Components.SingleElement>();

            Strait_Crossword(password.Password, elements);

            return elements.ToArray();
        }

        private int GetNumberOfColumns(Components.SingleElement[] elements)
        {
            return elements.SelectMany(x => x.Coordinate_Info).Max(x => x.Coordinate.Y) + 1;
        }

        public void Generate_Strait_Crossword(Components.Password_Info password)
        {
            var elements = GetElement(password);

            ElementsIn(elements);

            var rows = password.Password.Length;
            var columns = GetNumberOfColumns(elements);

            if (columns > rows)
            {
                rows = columns;
            }

            if(rows> columns)
            {
                columns = rows;
            }

            board = new Components.Board_Info(rows, columns, elements);

        }

        // jeśli robimy pojedyńczą krzyżowke to to można pominąć 
        public Components.Board_Info Generate(Components.Password_Info password)
        {
             board = null;
            Generate_Strait_Crossword(password);

            return board;
        }
    }
}
