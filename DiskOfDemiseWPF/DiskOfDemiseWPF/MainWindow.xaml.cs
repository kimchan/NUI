using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiskOfDemiseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DiskOfDemiseGame d1;
        private Storyboard myStoryboard;

        public MainWindow()
        {
            d1 = new DiskOfDemiseGame();

            InitializeComponent();
            reset();

            //wheelPicture.RenderTransform = new RotateTransform(5);

            spinWheel(1000);
            
        }

        public void reset()
        {
            phraseLabel.Text = d1.displayPhrase();
            nameLabel.Text = " Player " + d1.displayName();
        }

        public void spinWheel(double addedAngle)
        {
            wheelPicture.RenderTransform = new RotateTransform();

            double currentAngle = ((RotateTransform) wheelPicture.RenderTransform).Angle;
           
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = currentAngle;
            myDoubleAnimation.To = currentAngle+addedAngle;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(8));

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);

            ((RotateTransform) wheelPicture.RenderTransform).BeginAnimation(RotateTransform.AngleProperty, myDoubleAnimation);
        }
    }
}



/*

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

*/