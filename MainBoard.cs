using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_Damian_I_Marek
{
    public partial class MainBoard : Form
    {

        private Engine engine;
        //private string path = Directory.GetCurrentDirectory() + "\\lista.txt";
        private string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\lista.txt";
        //private string path =  Application.StartupPath + "\\lista.txt";
        private Components.Password_Info password;
        private Components.Board_Info board;
        private Clues clueBoard;
        


        public MainBoard()
        {
            //inicjuje silnik
            engine = new Engine();

            //wczytuje słowa 
            engine.ReadWords(path);

            InitializeComponent();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }



        /// <summary>
        /// Rysuje Plansze KOLUMNY I WIERSZE
        /// </summary>
        public void Draw()
        {
            //czyści kolumny i wiersze
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            for (int i = 0; i < board.Column; i++)
            {
                //dodaje kolumny do data grid view
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            }

            dataGridView1.Rows.Add(board.Row);

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.Width = dataGridView1.Width / dataGridView1.Columns.Count;
            }

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.Height = dataGridView1.Height / dataGridView1.Rows.Count;
            }

        }



        /// <summary>
        /// Nadaje atrybuty kiedy nie ma litery
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        private void SetEmpty(int column, int row)
        {
            //Puste pole jest tylko do odczytu i nie mozna go edyowac
            dataGridView1[column, row].ReadOnly = true;
            //Tło jest szare jeśli klikniemy na komórke która nie jest odpowiedzią 
            dataGridView1[column, row].Style.SelectionBackColor = System.Drawing.Color.Black;
            //kolor tła pustego pola
            dataGridView1[column, row].Style.BackColor = System.Drawing.Color.Black;

            dataGridView1[column, row].Tag = board.BoardArea[row, column];

        }



        /// <summary>
        /// Nadaje atrybuty kiedy jest litera
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        private void SetLetter(int column, int row)
        {
            // pole z literą można edytować
            dataGridView1[column, row].ReadOnly = false;

            dataGridView1[column, row].Tag = board.BoardArea[row, column];

            //kolor tła komórki z literą 
            dataGridView1[column, row].Style.BackColor = Color.White;

            // dataGridView1[column, row].Style.Font = new Font("Arial", 10F, GraphicsUnit.Pixel);

            // Jeśli kratka zawiera litery naszego hasła zamienia kolor tej kratki na orange
            if (IsPasswordCell(column, row))
            {
                //IsPasswordField
                dataGridView1[column, row].Style.BackColor = Color.Blue;
            }
        }



        /// <summary>
        /// Czy Komórka zawiera litere hasła
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsPasswordCell(int column, int row)
        {
            // deklaracja zmiennej do sprawdzania czy kratka zawiera litere z naszego hasła
            bool PasswordCell = false;

            // zapisuje do zmiennej allCoord wszystkie współrzędne
            var allCoord = board.SingleElements.SelectMany(x => x.Coordinate_Info).ToArray();

            // przypisuje do zmiennej wszystkie koordynaty 
            // gdzie x = x i y = y i passwordchecker == true
            // to przypisz
            var onlyPassCoord = allCoord.Select(x => x)
                .Where(x => x.Coordinate.X == row && x.Coordinate.Y == column && x.Passwordchcecker == true)
                .FirstOrDefault();

            //Jeśli zmienna onlyPassCoord zawiera cokolwiek to PasswordCell = true
            if (onlyPassCoord != null)
            {
                PasswordCell = true;
            }

            return PasswordCell;
        }


        /// <summary>
        /// Ustawia litery 
        /// </summary>
        public void Aligment()
        {
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int row = 0; row < board.Row; row++)
            {
                for (int column = 0; column < board.Column; column++)
                {
                    //if (board.BoardArea[row, column] == '\0')
                    if (board.BoardArea[row, column] == '\0')
                    {
                        //setEmpty
                        SetEmpty(column, row);
                    }
                    else
                    {
                        //setletterfield
                        SetLetter(column, row);
                    }
                }
            }


        }


        /// <summary>
        /// Sprawdza czy komórka zawiera litere
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsLetterCell(int column, int row)
        {
            bool LetterCell = false;

            //!string.IsNullOrEmpty(dataGridView1[column, row].Tag.ToString()) możliwe ze to sie przyda

            //Jeśli komórka zawiera wartość
            //'\0' nullchecker
            if (dataGridView1[column, row].Tag != null && Convert.ToChar(dataGridView1[column, row].Tag) != '\0')
            {
                LetterCell = true;
            }

            return LetterCell;
        }



        /// <summary>
        /// Ta zmienna odpowiada za to że jesli uzupełnimy ostatnie wolne miejsce do wpisania to przeskoczymy do następnego wiersza 
        /// </summary>
        private void GoToNextCell()
        {
            // zmienna column przyjmuje index obecnej kolumny
            var column = dataGridView1.CurrentCell.ColumnIndex;

            // zmienna column przyjmuje index obecnego wiersza
            var row = dataGridView1.CurrentCell.RowIndex;

            //Jeśli liczba kolumn planszy -1 > obecna kolumna
            //jeśli damy -2 to automatycznie nie przeskoczy do ostatniej kolumny planszy 
            if (board.Column - 1 > column)
            {
                column++;

                //jeśli w tym samym row ale w nastepnej kolumnie jest litera
                if (IsLetterCell(column, row))
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[column];
                    return;
                }
            }

            //przechodzenie row niżej po wypełnieniu obecnego
            if (board.Row - 1 > row)
            {
                row++;

                // Jeśli uzupełnimy ostatnią komórkę 
                if (board.SingleElements.Length == row)
                {
                    //to przypisuje współrzedne 0 0  unikniemy wyjścia poza tablice
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                    return;
                }


                var nextRowCoordinates = board.SingleElements.SelectMany(x => x.Coordinate_Info).Where(x => x.Coordinate.X == row).ToArray();
                column = nextRowCoordinates[0].Coordinate.Y;


                //jeśli z automatu uzupełnimy ostatnią komórkę z literą 
                /*
                var nextRowCoor = board.SingleElements
                    .SelectMany(x => x.Coordinate_Info)
                    .Where(x => x.Coordinate.X == row)
                    .ToArray();

                column = nextRowCoor[0].Coordinate.Y;
                */

                if (IsLetterCell(column, row))
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[column];
                    return;
                }
                //TODO
            }


        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //„Control.KeyPress” może pojawić się tylko po lewej stronie wyrażenia += lub -=

            e.Control.KeyPress -= dataGridView1_KeyPress;
            e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
        }



        /// <summary>
        /// Naciśniecie klawisza
        /// </summary>
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //jeśli obecna komórka jest pusta 
            if (dataGridView1.CurrentCell == null || dataGridView1.CurrentCell.Tag.ToString() == string.Empty || dataGridView1.CurrentCell.Tag == null)
            {
                return;
            }

            //Wpisywana wartośc zostaje przekonwertowana na duzą litere i umieszczona w komórce 
            dataGridView1.CurrentCell.Value = e.KeyChar.ToString().ToUpper();

            //Metoda która odpowieada za automatyczne przechodzenie po row i columnach
            GoToNextCell();

            e.Handled = true;
        }


        /// <summary>
        ///  Zyświetla okno z pytaniami 
        /// </summary>
        private void CreateClueBoard()
        {
            clueBoard = new Clues(board.SingleElements);
            clueBoard.StartPosition = FormStartPosition.CenterScreen;
            clueBoard.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 + this.Width / 2, this.ClientSize.Height / 2);
            clueBoard.Show();

        }
  

        private void MainBoard_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///  Zamyka aplikacje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        ///  Sprawdza poprawnośc naszych odpowiedzi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {

            var errors = 0;

            for (int row = 0; row < board.Row; row++)
            {
                for (int col = 0; col < board.Column; col++)
                {
                    if (IsLetterCell(col, row) == false)
                    {
                        continue;
                    }

                    if (dataGridView1[col, row].Value == null || dataGridView1[col, row].Value.ToString() != board.BoardArea[row, col].ToString())
                    {
                        dataGridView1[col, row].Value = board.BoardArea[row, col];
                        dataGridView1[col, row].Style.ForeColor = Color.Red;
                        errors++;
                    }
                    else
                    {
                        dataGridView1[col, row].Style.ForeColor = DefaultForeColor;
                    }
                }
            }

            if (errors > 0)
            {
                MessageBox.Show($"Popełniłeś {errors} błędów.", "Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Gratulacje udało ci sie rozwiązać krzyżówkę", "Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        /// <summary>
        /// Tworzy Prostą krzyżówkę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProstaKrzyzowka_Click(object sender, EventArgs e)
        {

            // var form = new Form_haslo();
            var form = new Form_haslo();

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }


            password = form.password_Info;

            try
            {

                board = engine.Generate(password);

                //Rysuje plansze
                Draw();

                //Ustawia komórki
                Aligment();

                // Tworzy okienko z podpowiedzimi
                CreateClueBoard();

          
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Houston mamy problem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // dodać że mozna sprawdzać
            //Domyślnie ta opcja nie jest dostępna  bo nie ma czego sprawdzać 
            //jeśli jeszcze nie mamy krzyżówki
            btnCheck.Enabled = true;
        }



        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Projekt wykonał Damian Lewandowski i Marek Kucharski (grupa K37)", "Wykonali");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/GingerNinjaa/Projekt-Damiana-i-Marka");
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
       


       

    }
}
