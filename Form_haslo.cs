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
    public partial class Form_haslo : Form
    {
        public Form_haslo()
        {
            InitializeComponent();
        }
        public Components.Password_Info password_Info { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            // Wyjście z aplikacji
            //Application.Exit();

            DialogResult = DialogResult.Cancel;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            // Hasło powinno mieć conajniej 3 znaki
            if (textPassword.Text.Length < 3)
            {
                MessageBox.Show("Hasło powinno mieć przzynajmniej 3 znaki", "Złe hasło", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            password_Info = new Components.Password_Info(textPassword.Text);
            DialogResult = DialogResult.OK;

            //zamykanie tego okna 
          //  this.Hide();

            // otwieranie kolejnego okna 
         //   new MainBoard().Show();

            

        }
        
        private void textPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
