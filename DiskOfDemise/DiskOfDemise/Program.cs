using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskOfDemise
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            DiskOfDemise d1 = new DiskOfDemise();
            d1.checkLetterInPhrase('H');
            d1.checkLetterInPhrase('p');
            d1.checkLetterInPhrase('L');
            d1.checkLetterInPhrase('w');
            d1.checkLetterInPhrase('O');
            d1.checkLetterInPhrase('s');
            d1.checkLetterInPhrase('b');
        }
    }
}
