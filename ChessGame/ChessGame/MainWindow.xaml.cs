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


namespace ChessGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class ChessPiece
    {
        private Image piece = new Image();//the visual of the piece
        private int Team; //white or black aka 0 or 1;
        public String Type;// pawn, queen etc
        public int X;//positions
        public int Y;
        public bool isOnBoard;
        

        public void setData(int Team, String Type)
        {
            this.Team = Team;
            this.Type = Type;
        }

        public void attackMove(string Type, int pieceNo, ChessPiece[] pieces)
        {
            bool canAttack = true;
            switch (Type)
            {

                case "Pawn":
                    if (Team == 0)
                    {
                        canAttack = false;
                        int direction = -1;
                        int attackedPiece=0;
                        for (int i = 8; i < 16; i++)
                        {
                            if (pieces[i].isOnBoard == true && pieces[i].X == this.X + 1 && pieces[i].Y == this.Y - 1)
                            {
                                canAttack = true;
                                direction = 0;
                                attackedPiece = i;
                            }
                            if (pieces[i].isOnBoard==true && pieces[i].X == this.X + 1 && pieces[i].Y == this.Y + 1)
                            {
                                canAttack = true;
                                direction = 1;
                                attackedPiece = i;
                            }

                        }
                        if (canAttack == true && direction == 0)
                        {
                            setPosition(X + 1, Y - 1);
                            pieces[attackedPiece].removePiece();
                        }
                        if (canAttack == true && direction == 1)
                        {
                            setPosition(X + 1, Y + 1);
                            pieces[attackedPiece].removePiece();
                        }


                    }

                    else
                    {
                        canAttack = false;
                        int direction = -1;
                        int attackedPiece = -1;
                        for (int i = 0; i < 8; i++)
                        {
                            if (pieces[i].isOnBoard == true && pieces[i].X == this.X -1  && pieces[i].Y == this.Y - 1)
                            {
                                canAttack = true;
                                direction = 0;
                                attackedPiece = i;
                            }
                            if (pieces[i].isOnBoard == true && pieces[i].X == this.X -1  && pieces[i].Y == this.Y + 1)
                            {
                                canAttack = true;
                                direction = 1;
                                attackedPiece = i;
                            }

                        }
                        if (canAttack == true && direction == 0)
                        {
                            setPosition(X - 1, Y - 1);
                            pieces[attackedPiece].removePiece();
                        }
                        if (canAttack == true && direction == 1)
                        {
                            setPosition(X - 1, Y + 1);
                            pieces[attackedPiece].removePiece();
                        }

                    }
                    break;
            }
        }
        public void movePiece(string Type, int pieceNo, ChessPiece[] pieces)
        {
            bool canMove = true;
            switch (Type)
            {

                case "Pawn":
                    if (Team == 0)
                    {
                        canMove = true;
                        for (int i = 8; i <= 16 ; i++)
                        {
                            if (pieces[i].isOnBoard == true && pieces[i].X == this.X + 1 && pieces[i].Y == this.Y) canMove = false;
                            
                        }
                        if (canMove == true) setPosition(X + 1, Y);
                        
                    }
                    
                    else
                    {
                        canMove = true;
                        for (int i = 0; i <= 8 ; i++)
                        {
                            if (pieces[i].isOnBoard == true && pieces[i].X == this.X - 1 && pieces[i].Y == this.Y) canMove = false;
                            
                        }
                        if (canMove == true) setPosition(X - 1, Y);
                    }
                break;
            }
        }
        public void setPosition(int X, int Y)
        {
            if (isOnBoard == true)
            {
                if (this.X >= 0 && this.X < 8 && this.Y >= 0 && this.Y < 8)
                {
                    piece.Margin = new Thickness(80 * Y, 80 * X, 0, 0);
                    this.X = X;
                    this.Y = Y;
                }
            }
            
        }


        public void addPiece(int X, int Y)
        {
            createPiece(80 * Y, 80 * X);
            this.X = X;
            this.Y = Y;
            isOnBoard = true;
        }
        public void removePiece()
        {
            piece.Opacity = 0.1;
            isOnBoard = false;
        }
       
        private void createPiece(double left, double top)
        {                     
            piece.Width = 80;
            piece.Height = 80;
            piece.HorizontalAlignment = HorizontalAlignment.Left;
            piece.VerticalAlignment = VerticalAlignment.Top;
            piece.Margin = new Thickness(left, top, 0, 0);
          
            String URLType;
            if(Team==1)
                URLType = "WhitePieces";              
            else
                URLType = "BlackPieces";
               
           piece.Source = new BitmapImage(new Uri(@"/"+URLType+"/"+Type+".png", UriKind.Relative));
           ((MainWindow)System.Windows.Application.Current.MainWindow).grid.Children.Add(piece);                 
                
        }

        }
       
    public partial class MainWindow : Window
    {
        
        private int[,] chessPosition = new int[9,9];
        ChessPiece[] pieces = new ChessPiece[32];

        public void initializeBoard()
        {
           
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    addBoardSlot(i, j);
                }
            }


        }
        public void initializePieces()
        {
            for (int i = 0; i < 32; i++)
                pieces[i] = new ChessPiece();

            for (int i = 0; i < 8; i++)
            {
                pieces[i].setData(0, "Pawn");
                pieces[i].addPiece(1, i);
            }
            for (int i = 0; i < 8; i++)
            {
                pieces[i+8].setData(1, "Pawn");
                pieces[i+8].addPiece(6, i);
            }
        }

        public MainWindow()
        {
            
           
            InitializeComponent();
            initializeBoard();
            initializePieces();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();



        }

        double posX, posY;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Point p = Mouse.GetPosition(grid);
            posX = p.X;
            posY = p.Y;
            debugText.Content = Math.Floor(posX / 80).ToString() +" "+ Math.Floor(posY / 80).ToString();
        }

        public void createBoardSlot(int color, double left, double top)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 80;
            rect.Height = 80;
            if (color == 0) rect.Fill = Brushes.Wheat;
            else rect.Fill = Brushes.BurlyWood;
            rect.HorizontalAlignment = HorizontalAlignment.Left;
            rect.VerticalAlignment = VerticalAlignment.Top;
            rect.Margin = new Thickness(left, top, 0, 0);
            grid.Children.Add(rect);
        }
        public void addBoardSlot(int X, int Y)
        {
            if ((Y + X) % 2 == 0)
                createBoardSlot(1, 80 * X, 80 * Y);
            else
                createBoardSlot(0, 80 * X, 80 * Y);
        }

        private void chessBoard_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i <= 16; i++)
            {
                if (pieces[i].X == Math.Floor(posY / 80) && pieces[i].Y == Math.Floor(posX / 80))
                {
                    pieces[i].attackMove(pieces[i].Type, i, pieces);
                }
            }
        }

        private void chessBoard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for(int i=0;i<=16;i++)
            {
                if (pieces[i].X == Math.Floor(posY / 80) && pieces[i].Y == Math.Floor(posX / 80))
                {
                    pieces[i].movePiece(pieces[i].Type, i, pieces);
                }
            }         
        }

      

    }
}
