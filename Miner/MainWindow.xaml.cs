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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Miner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitBoard();
            SetMineLocation(8);
            //ImageSource imageSource =
            //    new BitmapImage(new Uri("Bomb.jpg", UriKind.Relative));
            //cell_img.image.Source = imageSource;

        }

        private void SetMineLocation(int CountMine)
        {
            Random rnd = new Random(DateTimeOffset.Now.Second);
            int rand;
            for (int i = 0; i < CountMine; i++)
            {
                rand = rnd.Next()% (cells.Count-i);
                cells[rand].setBomb();
                modifyCountOfMineAraund(rand);
            }
        }

        private void modifyCountOfMineAraund(int number)
        {
            if (number>0 && (number)%8 !=0 )
                if (!cells[number-1].isMine)
                    cells[number - 1].qrowCountOfMineAround();
            if(number<63 && (number)%8 !=7)
                if (!cells[number + 1].isMine)
                    cells[number + 1].qrowCountOfMineAround();
            if (number > 7)
                if (!cells[number - 8].isMine)
                    cells[number - 8].qrowCountOfMineAround();
            if (number < 55)
                if (!cells[number + 8].isMine)
                    cells[number + 8].qrowCountOfMineAround();
            if (number > 8 && (number) % 8 != 0)
                if (!cells[number - 9].isMine)
                    cells[number - 9].qrowCountOfMineAround();
            if (number < 55 && (number) % 8 != 7)
                if (!cells[number + 9].isMine)
                    cells[number + 9].qrowCountOfMineAround();
            if (number > 7 && (number) % 8 != 7)
                if (!cells[number - 7].isMine)
                    cells[number - 7].qrowCountOfMineAround();
            if (number < 57 && (number) % 8 != 0)
                if (!cells[number + 7].isMine)
                    cells[number + 7].qrowCountOfMineAround();
        }

        private List<Cell> cells = new List<Cell>(64);
        private void InitBoard()
        {
            var myGrid = new Grid();
            this.AddChild(myGrid);
            myGrid.ShowGridLines = true;
            for (int i = 0; i < 9; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            
            for (int i = 0; i < 64; i++)
            {
                var cell = new Cell($"cell_{i}", i % 8, i / 8, i.ToString());

                /*if (i==1)
                {
                    cell.setBomb();
                }*/
                cells.Add(cell);
                
                myGrid.Children.Add(cell.button);
                Grid.SetColumn(cell.button, cell.X);
                Grid.SetRow(cell.button, cell.Y);

            }
            // Добавление кнопки в правый нижний угол
            var cell_99 = new Cell("cell_99", 9, 9);
            cell_99.button.Click += AllOpen;
            myGrid.Children.Add(cell_99.button);
            Grid.SetColumn(cell_99.button, cell_99.X);
            Grid.SetRow(cell_99.button, cell_99.Y);
            
            //cells[4].changeText("Bomb");

        }
        private void AllOpen(object sender, RoutedEventArgs e) {
            foreach (var item in cells)
            {
               item.button.Content = item.reverse;
                switch (item.reverse)
                {
                    case "BOMB": item.button.Background = Brushes.Red;break;
                    case "1": item.button.Background = Brushes.LightBlue; break;
                    case "2": item.button.Background = Brushes.LightGreen; break;
                    case "3": item.button.Background = Brushes.Yellow; break;
                    case "4": item.button.Background = Brushes.IndianRed; break;
                    default:
                        break;
                }
            }

        }

    }
}
