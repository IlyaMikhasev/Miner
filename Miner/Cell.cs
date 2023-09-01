using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Runtime.Remoting.Contexts;

namespace Miner
{
    internal class Cell
    {
        private Button myButton;
        static int count = 0;
        public Button button { get { return myButton;} }
        private string Name;
        public string name { get { return Name; } }
        private int x;
        public int X {get {return x;} }
        private int y;
        public int Y { get { return y; } }
        private bool IsMine = false;
        public bool isMine { get { return IsMine; } }
        private string ReverseSide = "";
        public string reverse { get { return ReverseSide; } }
        private int CountMineArround = 0;
        public int countMineArround { get { return CountMineArround; } }
        public static int countUnlockPud = 0;
        public Cell(string name, int x, int y, string _content = " ", bool printMine = false)
        {
            this.myButton = new Button();
            this.x = x;
            this.y = y;
            this.myButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.myButton.FontSize = 20;
            this.Name = name; 
            this.myButton.Content = _content;
            this.myButton.Click += MyButton_Click;
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsMine) {
                Image image = new Image();
                BitmapImage bitmapImage = new BitmapImage(new Uri("/IMG/Bomb.jpg", UriKind.Relative));
                image.Source = bitmapImage;
                this.button.Content = image;
                var resaultdialog = MessageBox.Show("Game Over","Вы подорвались",MessageBoxButton.OKCancel);
                if (resaultdialog== MessageBoxResult.OK)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
            else
            {
                if (this.myButton.Content != this.ReverseSide)
                {
                    countUnlockPud++;
                }
                this.myButton.Content = this.ReverseSide;
                switch (ReverseSide)
                {
                    case "": this.button.Background = Brushes.White; break;
                    case "1": this.button.Background = Brushes.LightGreen; break;
                    case "2": this.button.Background = Brushes.LightBlue; break;
                    case "3": this.button.Background = Brushes.Yellow; break;
                    case "4": this.button.Background = Brushes.IndianRed; break;
                    case "5": this.button.Background = Brushes.Red; break;
                    default:
                        break;
                }
            }
        }

        public void setBomb() {
            this.IsMine = true;
            this.ReverseSide = "BOMB";
        }
        public void qrowCountOfMineAround() {
            this.ReverseSide = $"{++CountMineArround}";
        }
    }
}
