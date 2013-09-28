using System;
using System.Collections;
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
            d1.checkLetterInPhrase('d');
            d1.checkLetterInPhrase('d');
            d1.checkLetterInPhrase('d');
            d1.checkLetterInPhrase('s');
            d1.checkLetterInPhrase('d');
            phraseLabel.Text = d1.displayPhrase();
            nameLabel.Text = "Player " + d1.displayName();
            displayBodyParts();
        }

        private void clearBodyParts()
        {
            headShape.Visible = false;
            rightArmShape.Visible = false;
            leftArmShape.Visible = false;
            rightLegShape.Visible = false;
            leftLegShape.Visible = false;
        }

        private void displayBodyParts()
        {
            ArrayList temp = d1.returnBodyParts();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Equals("Head"))
                {
                    headShape.Visible = true;
                }
                if (temp[i].Equals("RightArm"))
                {
                    rightArmShape.Visible = true;
                }
                if (temp[i].Equals("LeftArm"))
                {
                    leftArmShape.Visible = true;
                }
                if (temp[i].Equals("RightLeg"))
                {
                    rightLegShape.Visible = true;
                }
                if (temp[i].Equals("LeftLeg"))
                {
                    leftLegShape.Visible = true;
                }
            }
        }

    }
}
