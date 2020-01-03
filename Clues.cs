using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_Damian_I_Marek
{
    
    public partial class Clues : Form
    {
        Components.SingleElement[] clue;

        public Clues(Components.SingleElement[] elements)
        {
            clue = elements;
            InitializeComponent();

            // tworzy kolumne
            CreateColumns();
            CreateList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CreateColumns()
        {
            Clues_table.Columns.Add("Numer");
            Clues_table.Columns.Add("Podpowiedź");

            // Set 100% width for the last column
            Clues_table.Columns[Clues_table.Columns.Count - 1].Width = -2;
        }


        private void CreateList()
        {
            var counter = 1;

            foreach (Components.SingleElement element in clue)
            {
                string[] clue = { counter.ToString(), element.Word.Description };
                var item = new ListViewItem(clue);

                Clues_table.Items.Add(item);

                counter++;
            }

        }

        private void Clues_table_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
