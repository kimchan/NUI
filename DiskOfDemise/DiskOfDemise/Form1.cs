using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskOfDemise
{
    public partial class Form1 : Form
    {
        DiskOfDemise d1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            d1 = new DiskOfDemise();
            d1.checkLetterInPhrase('H');
            d1.checkLetterInPhrase('p');
            d1.checkLetterInPhrase('L');
            d1.checkLetterInPhrase('w');
            d1.checkLetterInPhrase('O');
            d1.checkLetterInPhrase('s');
            d1.checkLetterInPhrase('b');
            label1.Text = d1.displayPhrase();
        }
    }
}
