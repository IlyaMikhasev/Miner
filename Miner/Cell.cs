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
        public Cell(string name, int x, int y, string _content = " ", bool printMine = false)
        {
            this.myButton = new Button();
            this.x = x;
            this.y = y;
            this.myButton.Height = 70;
            this.myButton.Width = 70;
            this.myButton.FontSize = 20;
            this.myButton.HorizontalAlignment = HorizontalAlignment.Center;
            this.myButton.VerticalAlignment = VerticalAlignment.Center;
            this.Name = name; 
            this.myButton.Content = _content;
            this.myButton.Click += MyButton_Click;
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            this.myButton.Content = this.ReverseSide;
           
        }
        private void setReverse(string _reverse) {
            this.ReverseSide = _reverse;
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
