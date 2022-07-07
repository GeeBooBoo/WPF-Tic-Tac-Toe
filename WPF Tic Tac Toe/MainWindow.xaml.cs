using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;

namespace WPF_Tic_Tac_Toe
{
    public partial class MainWindow : Window
    {
        private MarkTypeArray[] mark;
        private bool _PlayerTurn;
        private bool _GameEnded;

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            mark = new MarkTypeArray[9];

            for (int i = 0; i < mark.Length; i++)
            {
                mark[i] = MarkTypeArray.free;
            }

            _PlayerTurn = true;

            cel1.Content = cel2.Content = cel3.Content = cel4.Content = cel5.Content = cel6.Content = cel7.Content = cel8.Content = cel9.Content = string.Empty;

            _GameEnded = false;

            headerLabel.Foreground = Brushes.Black;
            headerLabel.Content = "Tic Tac Toe";
        }

        private void cel_Click(object sender, RoutedEventArgs e)
        {
            if (_GameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            if (mark[index] != MarkTypeArray.free)
                return;

            mark[index] = _PlayerTurn ? MarkTypeArray.cross : MarkTypeArray.circle;
            
            headerLabel.Content = _PlayerTurn ? " O turn now" : " X turn now";

            button.Content = _PlayerTurn ? "X" : "O";
            if (!_PlayerTurn)
                button.Foreground = Brushes.Red;
            else
                button.Foreground = Brushes.Blue;

            _PlayerTurn ^= true;

            CheckWinner();

        }

        private void winnerStyle(bool _PlayerTurn)
        {
            if (_PlayerTurn == true)
            {
                headerLabel.Content = "O has Won";
                headerLabel.Foreground = Brushes.Red;
            }
            else
            {
                headerLabel.Content = "X has Won";
                headerLabel.Foreground = Brushes.Blue;
            }
        }

        private void CheckWinner()
        {
            //check horizontal
            if (mark[0] != MarkTypeArray.free && (mark[0] & mark[1] & mark[2]) == mark[0])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            if (mark[3] != MarkTypeArray.free && (mark[3] & mark[4] & mark[5]) == mark[3])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            if (mark[6] != MarkTypeArray.free && (mark[6] & mark[7] & mark[8]) == mark[6])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            //check vertical
            if (mark[0] != MarkTypeArray.free && (mark[0] & mark[3] & mark[6]) == mark[0])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            if (mark[1] != MarkTypeArray.free && (mark[1] & mark[4] & mark[7]) == mark[1])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            if (mark[2] != MarkTypeArray.free && (mark[2] & mark[5] & mark[8]) == mark[2])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            //check diagonal
            if (mark[0] != MarkTypeArray.free && (mark[0] & mark[4] & mark[8]) == mark[0])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            if (mark[2] != MarkTypeArray.free && (mark[2] & mark[4] & mark[6]) == mark[2])
            {
                _GameEnded = true;
                winnerStyle(_PlayerTurn);
            }

            //draw
            if (!mark.Any(f => f == MarkTypeArray.free))
            {
                _GameEnded = true;
                headerLabel.Content = "Draw";
                headerLabel.Foreground = Brushes.Gray;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
