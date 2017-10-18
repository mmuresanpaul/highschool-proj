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
using System.Windows.Threading;

namespace cruce
{
    public partial class MainWindow : Window
    {
        bool turn = false;// false - player1 / true - player2
        int score1 , score2; 

        int[,] deck= new int[5,7];
        
        struct carte
        {
            public int number;
            public int color;
        } 
        struct mana
        {
            public carte carte1;
            public carte carte2;
            public carte carte3;
            public carte carte4;
        }

        mana mana1= new mana();
        mana mana2= new mana();

        carte player1_card = new carte();
        carte player2_card = new carte();
     
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();


            first_hand_init1();
            first_hand_init2();
        }

        void first_hand_init1()
        {
           new_card(1, 1);
           change_card_image(card1, mana1.carte1.color, mana1.carte1.number);

           new_card(1, 2);
           change_card_image(card2, mana1.carte2.color, mana1.carte2.number);

           new_card(1, 3);
           change_card_image(card3, mana1.carte3.color, mana1.carte3.number);

           new_card(1, 4);
           change_card_image(card4, mana1.carte4.color, mana1.carte4.number);
        }
        void first_hand_init2()
        {
            new_card(2, 1);
            change_card_image(card5, mana2.carte1.color, mana2.carte1.number);

            new_card(2, 2);
            change_card_image(card6, mana2.carte2.color, mana2.carte2.number);
            
            new_card(2, 3);
            change_card_image(card7, mana2.carte3.color, mana2.carte3.number);
            
            new_card(2,4);
            change_card_image(card8, mana2.carte4.color, mana2.carte4.number);    
        }

        Random rnd = new Random(); 
        int col=1, nr=1;
        void new_card(int hand,int card)
        {

            if (check_deck() == 0)
            {
                col = rnd.Next(1, 5);
                nr = rnd.Next(1, 7);


                switch (hand)
                {
                    case 1:
                        switch (card)
                        {
                            case 1:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana1.carte1.color = col;
                                    mana1.carte1.number = nr;
                                }
                                else
                                    new_card(hand, card);
                                break;

                            case 2:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana1.carte2.color = col;
                                    mana1.carte2.number = nr;
                                }
                                else
                                    new_card(hand, card);
                                break;

                            case 3:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana1.carte3.color = col;
                                    mana1.carte3.number = nr;
                                }
                                else
                                    new_card(hand, card);
                                break;

                            case 4:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana1.carte4.color = col;
                                    mana1.carte4.number = nr;
                                }
                                else
                                    new_card(hand, card);
                                break;

                        }
                        break;
                    case 2:
                        switch (card)
                        {
                            case 1:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana2.carte1.color = col;
                                    mana2.carte1.number = nr;
                                }
                                else new_card(hand, card);
                                break;

                            case 2:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana2.carte2.color = col;
                                    mana2.carte2.number = nr;
                                }
                                else new_card(hand, card);
                                break;

                            case 3:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana2.carte3.color = col;
                                    mana2.carte3.number = nr;
                                }
                                else new_card(hand, card);
                                break;

                            case 4:
                                if (deck[col, nr] == 0)
                                {
                                    deck[col, nr] = 1;
                                    mana2.carte4.color = col;
                                    mana2.carte4.number = nr;
                                }
                                else new_card(hand, card);
                                break;

                        }
                        break;
                }
            }
        }

        void change_card_image(Image card, int color, int numar)
        {
            var uriSource = new Uri("2_rosu.JPG", UriKind.Relative);
            switch(color)
            {
                case 1:
                    switch(numar)
                    {
                        case 1:
                            uriSource = new Uri(@"2_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 2:
                             uriSource = new Uri(@"3_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 3:
                              uriSource = new Uri(@"4_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 4:
                              uriSource = new Uri(@"9_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 5:
                              uriSource = new Uri(@"10_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 6:
                              uriSource = new Uri(@"11_rosu.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                    }
                    break;

                case 2:
                    switch (numar)
                    {
                        case 1:
                              uriSource = new Uri(@"2_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 2:
                             uriSource = new Uri(@"3_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 3:
                             uriSource = new Uri(@"4_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 4:
                             uriSource = new Uri(@"9_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 5:
                             uriSource = new Uri(@"10_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 6:
                             uriSource = new Uri(@"11_bata.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                    }
                    break;
                case 3:
                    switch (numar)
                    {
                        case 1:
                             uriSource = new Uri(@"2_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 2:
                            uriSource = new Uri(@"3_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 3:
                            uriSource = new Uri(@"4_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 4:
                            uriSource = new Uri(@"9_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 5:
                            uriSource = new Uri(@"10_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 6:
                            uriSource = new Uri(@"11_verde.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                    }
                    break;
                case 4:
                    switch (numar)
                    {
                        case 1: 
                            uriSource = new Uri(@"2_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 2:
                            uriSource = new Uri(@"3_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 3:
                            uriSource = new Uri(@"4_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 4:
                            uriSource = new Uri(@"9_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 5:
                            uriSource = new Uri(@"10_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                        case 6:
                            uriSource = new Uri(@"11_ghinda.JPG", UriKind.Relative);
                            card.Source = new BitmapImage(uriSource);
                            break;
                    }
                    break;

            }
            
        }
        void change_card_image_to_cardback(Image card)
        {
            var uriSource = new Uri("card-back.jpg", UriKind.Relative);
            card.Source = new BitmapImage(uriSource);
        }

        int check_deck()
        {
            for(int i=1;i<=4;i++)
            {
                for(int j=1;j<=6;j++)
                {
                    if (deck[i, j] == 0) return 0;
                }
       
            }
            return 1;
        }

        void check_if_cards_left()
        {
            k++;

            if (card1.Opacity == 0 && card2.Opacity == 0 && card3.Opacity == 0 && card4.Opacity == 0
                &&card5.Opacity==0 && card6.Opacity==0 && card7.Opacity==0 && card8.Opacity==0)
            {
                MessageBox.Show("Ai castigat o bere, hai noroc!");
                this.Close();
            }
        }

        private void card1_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == false)
            {

                if (check_deck() == 1)
                {
                    player1_card.color = mana1.carte1.color;
                    player1_card.number = mana1.carte1.number;

                    deck_image.Opacity = 0;

                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte1.color, mana1.carte1.number);
                    card1.Opacity = 0;
                }
                else
                {
                    player1_card.color = mana1.carte1.color;
                    player1_card.number = mana1.carte1.number;

                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte1.color, mana1.carte1.number);
                    new_card(1, 1);
                    change_card_image(card1, mana1.carte1.color, mana1.carte1.number);
                }

                check_if_cards_left();
                
                turn = true;
            }      
        }
        private void card2_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == false)
            {
                if (check_deck() == 1)
                {
                    player1_card.color = mana1.carte2.color;
                    player1_card.number = mana1.carte2.number;

                    deck_image.Opacity = 0;
                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte2.color, mana1.carte2.number);
                    card2.Opacity = 0;
                }
                else
                {
                    player1_card.color = mana1.carte2.color;
                    player1_card.number = mana1.carte2.number;

                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte2.color, mana1.carte2.number);
                    new_card(1, 2);
                    change_card_image(card2, mana1.carte2.color, mana1.carte2.number);
                }


                check_if_cards_left();
                turn = true;
            }
        }
        private void card3_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == false)
            {
                if (check_deck() == 1)
                {
                    player1_card.color = mana1.carte3.color;
                    player1_card.number = mana1.carte3.number;

                    deck_image.Opacity = 0;
                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte3.color, mana1.carte3.number);
                    card3.Opacity = 0;

                }
                else
                {
                    player1_card.color = mana1.carte3.color;
                    player1_card.number = mana1.carte3.number;
                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte3.color, mana1.carte3.number);
                    new_card(1, 3);
                    change_card_image(card3, mana1.carte3.color, mana1.carte3.number);
                }

                check_if_cards_left();
                turn = true;
            }
        }
        private void card4_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == false)
            {
                if (check_deck() == 1)
                {
                    player1_card.color = mana1.carte4.color;
                    player1_card.number = mana1.carte4.number;

                    deck_image.Opacity = 0;
                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte4.color, mana1.carte4.number);
                    card4.Opacity = 0;
                }
                else
                {
                    player1_card.color = mana1.carte4.color;
                    player1_card.number = mana1.carte4.number;
                    clicked1.Opacity = 1;
                    change_card_image(clicked1, mana1.carte4.color, mana1.carte4.number);
                    new_card(1, 4);
                    change_card_image(card4, mana1.carte4.color, mana1.carte4.number);
                }

                check_if_cards_left();
                turn = true;
            }
        }
        private void card5_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == true)
            {
                if (check_deck() == 1)
                {
                    player2_card.color = mana2.carte1.color;
                    player2_card.number = mana2.carte1.number;

                    deck_image.Opacity = 0;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte1.color, mana2.carte1.number);
                    card5.Opacity = 0;
                }
                else
                {
                    player2_card.color = mana2.carte1.color;
                    player2_card.number = mana2.carte1.number;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte1.color, mana2.carte1.number);
                    new_card(2, 1);
                    change_card_image(card5, mana2.carte1.color, mana2.carte1.number);
                }

                check_if_cards_left();

                turn = false;
            }
        }
        private void card6_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == true)
            {
                if (check_deck() == 1)
                {
                    player2_card.color = mana2.carte2.color;
                    player2_card.number = mana2.carte2.number;

                    deck_image.Opacity = 0;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte2.color, mana2.carte2.number);
                    card6.Opacity = 0;
                }
                else
                {
                    player2_card.color = mana2.carte2.color;
                    player2_card.number = mana2.carte2.number;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte2.color, mana2.carte2.number);
                    new_card(2, 2);
                    change_card_image(card6, mana2.carte2.color, mana2.carte2.number);
                }

                check_if_cards_left();

                turn = false;
            }

        }
        private void card7_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == true)
            {
                if (check_deck() == 1)
                {
                    player2_card.color = mana2.carte3.color;
                    player2_card.number = mana2.carte3.number;

                    deck_image.Opacity = 0;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte3.color, mana2.carte3.number);
                    card7.Opacity = 0;
                }
                else
                {
                    player2_card.color = mana2.carte3.color;
                    player2_card.number = mana2.carte3.number;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte3.color, mana2.carte3.number);
                    new_card(2, 3);
                    change_card_image(card7, mana2.carte3.color, mana2.carte3.number);
                }

                check_if_cards_left();

                turn = false;
            }
        }
        private void card8_click(object sender, MouseButtonEventArgs e)
        {
            if (turn == true)
            {
                if (check_deck() == 1)
                {
                    player2_card.color = mana2.carte4.color;
                    player2_card.number = mana2.carte4.number;

                    deck_image.Opacity = 0;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte4.color, mana2.carte4.number);
                    card8.Opacity = 0;
                }
                else
                {
                    player2_card.color = mana2.carte4.color;
                    player2_card.number = mana2.carte4.number;
                    clicked2.Opacity = 1;
                    change_card_image(clicked2, mana2.carte4.color, mana2.carte4.number);
                    new_card(2, 4);
                    change_card_image(card8, mana2.carte4.color, mana2.carte4.number);
                }

                check_if_cards_left();

                turn = false;
            }
        }


        // rules
        
        int check_cards(int card1Color, int card1Number, int card2Color, int card2Number, int tromf)
        {
            if (card1Color == tromf)
            {
                if (card2Color == tromf)
                {
                    if (card1Number > card2Number) return 1;
                    else return 2;

                }
                else return 1;
            }
            else
            {
                if (card2Color == tromf) return 2;

                else
                {
                    if (card1Color == card2Color)
                    {
                        if (card1Number > card2Number) return 1;
                        else return 2;
                    }
                    else return 1;
                }

            }
            
        }
        
        int k = 0;
        private void timer_tick(object sender, EventArgs e)
        {
            if (turn == false)
            {
                change_card_image_to_cardback(card5);
                change_card_image_to_cardback(card6);
                change_card_image_to_cardback(card7);
                change_card_image_to_cardback(card8);

                change_card_image(card1, mana1.carte1.color, mana1.carte1.number);
                change_card_image(card2, mana1.carte2.color, mana1.carte2.number);
                change_card_image(card3, mana1.carte3.color, mana1.carte3.number);
                change_card_image(card4, mana1.carte4.color, mana1.carte4.number);
            }
            else
            {
                change_card_image_to_cardback(card1);
                change_card_image_to_cardback(card2);
                change_card_image_to_cardback(card3);
                change_card_image_to_cardback(card4);

                change_card_image(card5, mana2.carte1.color, mana2.carte1.number);
                change_card_image(card6, mana2.carte2.color, mana2.carte2.number);
                change_card_image(card7, mana2.carte3.color, mana2.carte3.number);
                change_card_image(card8, mana2.carte4.color, mana2.carte4.number);

            }

            if (k == 2)
            {
                for (double i = 1; i <= 10000000; i+=0.1) ;
                    if (check_cards(player1_card.color, player1_card.number, player2_card.color, player2_card.number, 1) == 1)
                    {
                        score1++;
                    }
                if (check_cards(player1_card.color, player1_card.number, player2_card.color, player2_card.number, 1) == 2)
                {
                    score2++;
                }
                k = 0;
                clicked1.Opacity = 0;
                clicked2.Opacity = 0;
            }

            output1.Text = "player1: " + score1.ToString();
            output2.Text = "player2: " + score2.ToString();
        }
    }
}
