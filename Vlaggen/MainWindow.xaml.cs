using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Vlaggen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string alphabetIndex;
        private BitmapImage bitMapFlag;
        private string flagName;
        private Random generator;
        private Image imageFlag;
        private int nameIndex;
        private int randomNumber;

        public MainWindow()
        {
            InitializeComponent();

            // Creates a single instance of Random
            generator = new Random();
            //generator = new Random(Guid.NewGuid().GetHashCode());
        }

        private enum Names
        {
            unknown,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            Y,
            Z
        }

        private void ButtonNextFlag_Click(object sender, RoutedEventArgs e)
        {
            // Sets next flag button text from start to Next flag, only for the first time
            if (buttonNextFlag.Content.ToString() == "Start")
            {
                buttonNextFlag.Content = "Next Flag =>";
            }

            canvasFlag.Children.Clear();
            labelFlagName.Content = "";
            nameIndex = GetRandomNumber(1, GetFolderAmount() + 1);
            //name = generator.Next(1, 26);

            alphabetIndex = ((Names)nameIndex).ToString();
            //Names varName = (Names)nameIndex;
            //alphabetIndex = varName.ToString();

            SetUpBitmapImage();

            canvasFlag.Children.Add(imageFlag);
        }

        // Shows the name of the flag, retrieves this from the path name
        private void ButtonShowName_Click(object sender, RoutedEventArgs e)
        {
            // Checks if there is an image selected NOT NULL, and checks if the remainder from
            // the previous path contains vlaggen to prevent NoPathFound exeption
            if (flagName != null && flagName.Contains("Vlaggen"))
            {
                int indexStart = flagName.IndexOf("Images") + 9;
                int indexEnd = flagName.IndexOf(".png");
                int lenght = indexEnd - indexStart;
                flagName = flagName.Substring(indexStart, lenght);
                labelFlagName.Content = flagName;
            }
        }

        // Returns the amount of directories inside a subdirectory of images
        private int GetFolderAmount()
        {
            // Sets directory name to solution directory (where images directory location is),
            // then counts the directories.
            string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            wantedPath += "\\Images\\";
            int folderAmount = Directory.GetDirectories(wantedPath).Length;
            return folderAmount;
        }

        // Returns the file name inside a randomly chosen subdirectory
        private string GetRandomFlagName(string alphabetIndexer)
        {
            // Sets directory name to solution directory (where images directory location is),
            // then gets all flag names from the subdirectory and picks 1 randomly.
            string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            wantedPath += "\\Images\\" + alphabetIndexer;
            string[] flagNameArray = Directory.GetFiles(wantedPath, "*.png");
            //string[] flagNameArray = Directory.GetFiles(wanted_path + "\\Images\\" + alphabetIndexer, "*.png");
            int arrayIndex = generator.Next(flagNameArray.Length);
            flagName = flagNameArray[arrayIndex];
            return flagName;
        }

        // Returns a randomly chosen number between 1 and the total amount of subdirectories
        private int GetRandomNumber(int start, int end)
        {
            // Prevents from getting dupplicate flags, by checking if the same subdirectory name is used.
            do
            {
                randomNumber = generator.Next(start, end);
            }
            while (randomNumber == nameIndex);

            return randomNumber;
        }

        // Sets up sources, image, BitmapImage (UriSource)
        private void SetUpBitmapImage()
        {
            // Creates the image element.
            imageFlag = new Image();
            imageFlag.Width = canvasFlag.Width;
            imageFlag.Height = canvasFlag.Height;

            // Creates source.
            bitMapFlag = new BitmapImage();

            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bitMapFlag.BeginInit();
            bitMapFlag.UriSource = new Uri(GetRandomFlagName(alphabetIndex), UriKind.RelativeOrAbsolute);
            bitMapFlag.EndInit();

            // Sets the image source.
            imageFlag.Source = bitMapFlag;
        }
    }
}