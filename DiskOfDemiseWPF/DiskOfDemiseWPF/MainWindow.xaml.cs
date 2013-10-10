using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SS = System.Speech;
using System.Speech.Recognition;
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
using System.Threading;

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
        private Storyboard myStoryboard;
        DiskOfDemiseGame d1;
        Player[] players;
        Boolean gameOver;

        /// <summary>
        /// kinect global variables
        /// </summary>
        KinectSensor sensor = null;
        GestureController gestureController = null;
        String mostRecentGesture;

        /// <summary>
        /// color stream global variables
        /// </summary>
        private byte[] colorPixelData;          // image source for image control
        private WriteableBitmap outputImage;    // bitmap for color stream image

        //voice recog
        private SS.Recognition.RecognizerInfo priRI;

        /// <summary>
        /// method for actions taken when a gesture is recognized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhenGestureRecognized(object sender, GestureEventArgs e)
        {
            /// disable gesture service
            /// GestureServiceOff();
            /// output gesture type to console
            mostRecentGesture = e.gestureType;
            System.Console.Write(e.gestureType + "\n");
            /// spin wheel
            if (e.gestureType == "swipe_left" || e.gestureType == "swipe_right" ||
                e.gestureType == "kick_left" || e.gestureType == "kick_right")
            {
                double randomDouble; ;
                Random random = new Random();
                randomDouble = 180.00 + random.NextDouble() * 720;
                if (e.gestureType == "swipe_left" || e.gestureType == "kick_left")
                {
                    randomDouble *= -1;
                }
                this.spinWheel(randomDouble);
            }
        }

        private void findBodyPart(Double number)
        {
            while (number > 360)
            {
                number -= 360;
            }
            if (number <= 72)
            {
                d1.setBodyPart("Head");
            }
            else if (number > 72 && number <= 144)
            {
                d1.setBodyPart("RightArm");
            }
            else if (number > 144 && number <= 216)
            {
                d1.setBodyPart("LeftArm");
            }
            else if (number > 216 && number <= 288)
            {
                d1.setBodyPart("RightLeg");
            }
            else if (number > 288 && number <= 360)
            {
                d1.setBodyPart("LeftLeg");
            }
            Console.WriteLine(number);
        }
        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.sensor.SkeletonStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
                this.sensor.Start();

                //INITIALIZE AUDIOSOURCE
                KinectAudioSource source = sensor.AudioSource;
                source.EchoCancellationMode = EchoCancellationMode.None;
                source.AutomaticGainControlEnabled = false;

                SS.Recognition.RecognizerInfo ri = SS.Recognition.SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault();
                using (var spRecEng = new SS.Recognition.SpeechRecognitionEngine(ri.Id))
                {

                    //Choices letters = new Choices(new string[] { "A", "B" });
                    Choices letters = new Choices(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

                    GrammarBuilder gb = new GrammarBuilder("Guess");
                    gb.Append(letters);

                    Grammar grammar = new Grammar(gb);
                    grammar.Name = "DisK of Demise";

                    spRecEng.LoadGrammarCompleted += new EventHandler<LoadGrammarCompletedEventArgs>(LoadGrammarCompleted);
                    spRecEng.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
                    spRecEng.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(SpeechRejected);

                    spRecEng.SetInputToDefaultAudioDevice();

                    spRecEng.LoadGrammarAsync(grammar);

                    // spRecEng.RecognizeAsync(RecognizeMode.Multiple);


                }
                priRI = ri;
                //System.Console.Write("kinect initialized\n");
            }
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
            /// initialize and add kick right gesture to controller
            GestureSegment[] kickRight = new GestureSegment[3];
            kickRight[0] = new KickRightSegment();
            kickRight[1] = new KickRightSegment();
            kickRight[2] = new KickRightSegment();
            gestureController.AddGesture("kick_right", kickRight);
            /// initialize and add kick left gesture to controller
            GestureSegment[] kickLeft = new GestureSegment[3];
            kickLeft[0] = new KickLeftSegment();
            kickLeft[1] = new KickLeftSegment();
            kickLeft[2] = new KickLeftSegment();
            gestureController.AddGesture("kick_left", kickLeft);
            /// initialize and add kick left gesture to controller
            GestureSegment[] raiseHandRight = new GestureSegment[3];
            raiseHandRight[0] = new RaiseHandRightSegment();
            raiseHandRight[1] = new RaiseHandRightSegment();
            raiseHandRight[2] = new RaiseHandRightSegment();
            /// gestureController.AddGesture("raise_hand_right",raiseHandRight);
            ///
            System.Console.Write("gesture service initialized\n");
        }

        public void initGrammar()
        {

            SS.Recognition.RecognizerInfo ri = SS.Recognition.SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault();
            using (var spRecEng = new SS.Recognition.SpeechRecognitionEngine(ri.Id))
            {

                //Choices letters = new Choices(new string[] { "A", "B" });
                Choices letters = new Choices(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

                GrammarBuilder gb = new GrammarBuilder("Guess");
                gb.Append(letters);

                Grammar grammar = new Grammar(gb);
                grammar.Name = "DisK of Demise";

                spRecEng.LoadGrammarCompleted += new EventHandler<LoadGrammarCompletedEventArgs>(LoadGrammarCompleted);
                spRecEng.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
                spRecEng.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(SpeechRejected);

                spRecEng.SetInputToDefaultAudioDevice();

                spRecEng.LoadGrammarAsync(grammar);

                spRecEng.RecognizeAsync(RecognizeMode.Multiple);
                //Console.ReadLine();
            }
            sensor.Stop();
        }

        private void GestureServiceOff()
        {
            if (gestureController != null)
            {
                gestureController.GestureRecognized -= this.WhenGestureRecognized;
            }
        }

        private void GestureServiceOn()
        {
            if (gestureController != null)
            {
                gestureController.GestureRecognized += this.WhenGestureRecognized;
            }
        }

        static void LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            Console.WriteLine(e.Grammar.Name + " successfully loaded");
        }

        static void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Speech recognized: " + e.Result.Text);

            char letterUserGuessed = e.Result.Text[6];

            Console.WriteLine("You guessed the letter:" + letterUserGuessed);
        }

        static void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("Speech input failed. Please Repeat.");
        }

        private void newTurn(Player player)
        {

        }

        public MainWindow()
        {
            InitializeComponent();
            /// new (single player) game
            d1 = new DiskOfDemiseGame();
            players = new Player[1];
            players[0] = new Player("red");
            gameOver = false;
            /// kinect initializations
            initKinect();
            initGestureService();
            /// ???
            InitializeComponent();
            reset();
            /// game logic
            /*
            while (!gameOver)
            {
                newTurn(players[0]);
            }
            */
        }

        public void reset()
        {
            phraseLabel.Text = d1.displayPhrase();
            nameLabel.Text = " Player " + d1.displayName();
        }

        public void spinWheel(double addedAngle)
        {
            wheelPicture.RenderTransform = new RotateTransform();
            double currentAngle = ((RotateTransform)wheelPicture.RenderTransform).Angle;
            int duration = Math.Abs((int)addedAngle / 100);

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = currentAngle;
            myDoubleAnimation.To = currentAngle + addedAngle;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));

            //initGrammar();

            //Console.ReadLine();

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);

            ((RotateTransform)wheelPicture.RenderTransform).BeginAnimation(RotateTransform.AngleProperty, myDoubleAnimation);

            Thread oThread = new Thread(new ThreadStart(initGrammar));
            //oThread.Start();

            //while (oThread.IsAlive);

            //Thread.Sleep(1);

            // oThread.Abort();

            // oThread.Join();

            try
            {
                Console.WriteLine("Try to restart the initGrammar thread");
                oThread.Start();
                Thread.Sleep(1);
            }
            catch (ThreadStateException)
            {
                Console.Write("ThreadStateException trying to restart initGrammar. ");
                Console.WriteLine("Expected since aborted threads cannot be restarted.");
            }
        }

        public SS.Recognition.RecognizerInfo getRI()
        {
            return priRI;
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