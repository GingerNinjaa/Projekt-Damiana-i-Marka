using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Damian_I_Marek.Components
{
    /// <summary>
    /// Klasa która przechowuje hasło z formularza 
    /// </summary>
     public class Password_Info
    {
        public string Password { get; set; }
        
        public Password_Info(string pass)
        {
            this.Password = pass.ToUpper();
        }
    }
}
