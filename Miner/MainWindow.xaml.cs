using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Miner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int size = 40;
        Grid myGrid;
        TextBox textBox;
        Label label1 = new Label();
        int time = 0;
        DispatcherTimer timer = new DispatcherTimer();
        Button b_start = new Button();
        List<Gamer> gamers = new List<Gamer>();
        
        public MainWindow()
        {
            InitializeComponent();
            this.Width = size*8;
            this.Height = Width;
            InitBoard();
            addMenu();
            load_score();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += secundomer;
            SetMineLocation(8);

        }
        private void load_score() {
            Gamer.Deserealize_it("Gamers_score.xml", out gamers);
        }
        private void save_score() {
            gamers.Add(new Gamer(textBox.Text, int.Parse(label1.Content.ToString())));
            Gamer.Serealize_it(gamers,"Gamers_score.xml");
        }
        private void secundomer(object sender, EventArgs e) {
            time += 1;
            label1.Content = time.ToString();
            isWiner();
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
        private void isWiner() {
            if (Cell.countUnlockPud>=55)
            {
                timer.Stop();
                MessageBox.Show($"You Winner\nScore: {label1.Content}");
                save_score();
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
            myGrid = new Grid();
            this.AddChild(myGrid);
            myGrid.ShowGridLines = false;
            myGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < 8; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 64; i++)
            {
                var cell = new Cell($"cell_{i}", i % 8, i / 8);

                cells.Add(cell);
                
                myGrid.Children.Add(cell.button);
                Grid.SetColumn(cell.button, cell.X);
                Grid.SetRow(cell.button, cell.Y+1);
            }
        }
        private void addMenu() {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            myGrid.Children.Add(stackPanel);
            Grid.SetColumn(stackPanel, 0);
            Grid.SetRow(stackPanel, 0);
            Grid.SetColumnSpan(stackPanel, 8);
            Label label = new Label();
            label.Content = "Name users";
            stackPanel.Children.Add(label);
            textBox = new TextBox();
            textBox.Width = 50;
            textBox.Text = string.Empty;
            stackPanel.Children.Add(textBox);
            label1.Width = 70;
            label1.Content = time.ToString();
            stackPanel.Children.Add(label1);            
            b_start.Click += B_start_Click;
            b_start.Content = "start";
            stackPanel.Children.Add(b_start);
            Button btn_score = new Button();
            btn_score.Content = "Gamers score";
            btn_score.Click += Btn_score_Click;
            stackPanel.Children.Add(btn_score);
        }

        private void Btn_score_Click(object sender, RoutedEventArgs e)
        {
            string rating = string.Empty;
            foreach (var gamer in gamers)
            {
                rating += gamer._name + "\t" + gamer._score + "\n";
            }
            MessageBox.Show(rating,"Rating gamers");
        }

        private void B_start_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == string.Empty) {
                MessageBox.Show("Необходимо ввести имя!");
                return;
            }
            timer.Start();
            b_start.IsEnabled = false;
            textBox.IsEnabled = false;
            
        }


    }
}
