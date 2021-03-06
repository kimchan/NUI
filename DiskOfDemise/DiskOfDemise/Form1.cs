﻿using System;
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
        Bitmap bitmap1;

        public Form1()
        {
            InitializeComponent();
            d1 = new DiskOfDemise();
            bitmap1 = (Bitmap)Bitmap.FromFile(@"C:\Users\Kim\Documents\GitHub\NUI\DiskOfDemise\DiskOfDemise\Resources\RedHead.png");
            wheelImage.Image = bitmap1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            d1.checkLetterInPhrase('H');
            d1.checkLetterInPhrase('O');
            spinWheel();
            spinWheel();
            clearBodyParts();
            phraseLabel.Text = d1.displayPhrase();
            nameLabel.Text = "Player " + d1.displayName();
            displayBodyParts();
            colorBodyParts(d1.displayName());

        }

        private void spinWheel()
        {
                    bitmap1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    wheelImage.Image = bitmap1;
                    wheelImage.Invalidate();
        }


        private void colorBodyParts(String color)
        {
            Color bodyColor = Color.Black;
            if (color.Equals("Red"))
            {
                bodyColor = Color.Red;
            }
            else if (color.Equals("Yellow"))
            {
                bodyColor = Color.Yellow;
            }
            else if (color.Equals("Green"))
            {
                bodyColor = Color.Green;
            }
            else if (color.Equals("Blue"))
            {
                bodyColor = Color.Blue;
            }

            headShape.BorderColor = bodyColor;
            rightArmShape.BorderColor = bodyColor;
            leftArmShape.BorderColor = bodyColor;
            rightLegShape.BorderColor = bodyColor;
            leftLegShape.BorderColor = bodyColor;
            bodyShape.BorderColor = bodyColor;
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
