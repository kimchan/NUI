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
using Microsoft.Kinect;
using DiskOfDemiseWPF.Gesture;
using DiskOfDemiseWPF.Gesture.Parts;
using DiskOfDemiseWPF.Gesture.Parts.SwipeLeft;
using DiskOfDemiseWPF.Gesture.Parts.SwipeRight;
using DiskOfDemiseWPF.EventArguments;

namespace DiskOfDemiseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// global variables
        /// </summary>
        DiskOfDemiseGame d1;
        KinectSensor sensor = null;
        GestureController gestureController = null;
        private Storyboard myStoryboard;

        /// <summary>
        /// color stream variables
        /// </summary>
        private byte[] colorPixelData;          // image source for image control
        private WriteableBitmap outputImage;    // bitmap for color stream image

        /// <summary>
        /// method for actions taken when a gesture is recognized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhenGestureRecognized(object sender, GestureEventArgs e)
        {
            /// output gesture type to console
            System.Console.Write(e.gestureType + "\n");

            /// spin wheel
            double randomDouble; ;
            Random random = new Random();
            randomDouble = random.NextDouble()*540;

            if(e.gestureType == "swipe left (from right)")
            {
                randomDouble *= -1;
            }
            this.spinWheel(randomDouble);
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skeleton in skeletons)
                    {
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            gestureController.UpdateGestures(skeleton);
                        }
                    }
                }
            }
        }

        private void sensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    // using standard SDK
                    this.colorPixelData = new byte[colorFrame.PixelDataLength];

                    colorFrame.CopyPixelDataTo(this.colorPixelData);

                    this.outputImage = new WriteableBitmap(colorFrame.Width, colorFrame.Height,
                    96,  // DpiX
                    96,  // DpiY
                    PixelFormats.Bgr32, null);

                    this.outputImage.WritePixels(new Int32Rect(0, 0, colorFrame.Width, colorFrame.Height),
                        this.colorPixelData, colorFrame.Width * 4, 0);
                    this.kinectColorImage.Source = this.outputImage;
                }
            }
        }

        /// <summary>
        /// initialize the kinect sensor
        /// </summary>
        private void initKinect()
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }
            if (this.sensor != null)
            {
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.sensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(sensorColorFrameReady);
                ///this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.sensor.SkeletonStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
                this.sensor.Start();
            }
            System.Console.Write("kinect initialized\n");
        }

        private void initGestureService()
        {
            /// initialize gesture controller
            gestureController = new GestureController();
            gestureController.GestureRecognized += this.WhenGestureRecognized;
            /// initialize and add swipe right to controller
            GestureSegment[] swipeRight = new GestureSegment[3];
            swipeRight[0] = new SwipeRightSegment1();
            swipeRight[1] = new SwipeRightSegment2();
            swipeRight[2] = new SwipeRightSegment3();
            gestureController.AddGesture("swipe right (from left)", swipeRight);
            /// initialize and add swipe left to controller
            GestureSegment[] swipeLeft = new GestureSegment[3];
            swipeLeft[0] = new SwipeLeftSegment1();
            swipeLeft[1] = new SwipeLeftSegment2();
            swipeLeft[2] = new SwipeLeftSegment3();
            gestureController.AddGesture("swipe left (from right)", swipeLeft);
            /// signal
            System.Console.Write("gesture service initialized\n");
        }

        public MainWindow()
        {
            // new game
            d1 = new DiskOfDemiseGame();

            /// initializations
            InitializeComponent();
            initKinect();
            initGestureService();

            InitializeComponent();
            reset();

            /// spinWheel(1000);
            
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
            int duration = Math.Abs((int)addedAngle / 100);
           
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = currentAngle;
            myDoubleAnimation.To = currentAngle+addedAngle;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));

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