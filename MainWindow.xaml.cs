using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace U4_SpaceInvaders
{
    // *CREDITS*
    //
    // *Songs*
    // 8-Bit Dubstep by Ross Budgen,
    // This track is licensed under a ‘Creative Commons Attribution 4.0 International License’. 
    // The track can be found at the following URL: https://www.youtube.com/watch?v=kIBwWd2_NT8
    //
    // Wolf by Jeremy L
    // "All my music is royalty free, so check out my soundcloud for free downloads! 
    // (NOTE: This song has reached the maximum download limit on soundcloud sorry :( use a website to download it off youtube.)" - YouTube Description
    // https://www.youtube.com/watch?v=Ahfu1E_BBSI&list=PLKQWNXwX2zTkYUBbLPaTwYpDBIcFnDch3&index=20
    //
    // *Sound Effects*
    // "In celebration of GDC 2018 we are giving away 30GB+ of high-quality sound effects from our catalog. 
    // Everything is royalty-free and commercially usable. No attribution is required and you can use them 
    // on an unlimited number of projects for the rest of your lifetime. - https://sonniss.com/gameaudiogdc18/
    //
    // Boi by Kyler Campbell,
    // This effect was recorded by us. Therefore we own it.





    enum GameState { MainMenu, GameOn, GameOver }


    public static class Globals
    {
        public static bool EasterEggActive = false;
        public static bool musicPlaying = false;
        public static bool beginfade = false;
        public static bool isSpacePressed = false;
        public static bool playerCreated = false;

        public static int currentRound = 1;

        public static SoundPlayer musicPlayer = new SoundPlayer();
        public static MediaPlayer effectPlayer = new MediaPlayer();

    }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GameState gameState;

        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        Spaceship player;




        public MainWindow()
        {
            InitializeComponent();
            canvas_mainmenu.Visibility = Visibility.Visible;


            //start Timer
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

            gameState = GameState.MainMenu;
            DrawMainMenu();

        }

        public void CreatePlayer()
        {
            player = new Spaceship(canvas_battleground, this);
            Rectangle spaceship = new Rectangle();
            Globals.playerCreated = true;

        }

        public void Click_EasterEggTester(object Sender, RoutedEventArgs e)
        {
            if (Globals.EasterEggActive == false)
            {
                Globals.EasterEggActive = true;
                DrawMainMenu();
            }
            else if (Globals.EasterEggActive == true)
            {
                Globals.EasterEggActive = false;
                DrawMainMenu();
            }
        }

        private void DrawMainMenu()
        {
            //sets brushes to be the same as the image specified
            ImageBrush sprite_S_MMBackground = new ImageBrush(new BitmapImage(new Uri("Space Invaders.png", UriKind.Relative)));
            ImageBrush sprite_S_AlienSP1 = new ImageBrush(new BitmapImage(new Uri("Dranino.png", UriKind.Relative)));
            ImageBrush sprite_S_AlienSP2 = new ImageBrush(new BitmapImage(new Uri("Dracadre.png", UriKind.Relative)));
            ImageBrush sprite_S_AlienSP3 = new ImageBrush(new BitmapImage(new Uri("Draconus.png", UriKind.Relative)));
            ImageBrush sprite_S_AlienSP4 = new ImageBrush(new BitmapImage(new Uri("Draxxor.png", UriKind.Relative)));
            ImageBrush sprite_S_Spaceship = new ImageBrush(new BitmapImage(new Uri("Spaceship.png", UriKind.Relative)));
            ImageBrush sprite_F_MMBackground = new ImageBrush(new BitmapImage(new Uri("Face Invaders.png", UriKind.Relative)));
            ImageBrush sprite_F_AlienSP1 = new ImageBrush(new BitmapImage(new Uri("Alien SP1.png", UriKind.Relative)));
            ImageBrush sprite_F_AlienSP2 = new ImageBrush(new BitmapImage(new Uri("Alien SP2.png", UriKind.Relative)));
            ImageBrush sprite_F_AlienSP3 = new ImageBrush(new BitmapImage(new Uri("Alien SP3.png", UriKind.Relative)));
            ImageBrush sprite_F_AlienSP4 = new ImageBrush(new BitmapImage(new Uri("Draxxor.png", UriKind.Relative)));
            ImageBrush sprite_F_Spaceship = new ImageBrush(new BitmapImage(new Uri("Faceship.png", UriKind.Relative)));

            if (Globals.EasterEggActive == false)
            {

                //fills rectangle with specified image
                canvas_mainmenu.Background = sprite_S_MMBackground;
                MMAlienSP1.Fill = sprite_S_AlienSP1;
                MMAlienSP2.Fill = sprite_S_AlienSP2;
                MMAlienSP3.Fill = sprite_S_AlienSP3;
                MMAlienSP4.Fill = sprite_S_AlienSP4;
                MMSpaceship.Fill = sprite_S_Spaceship;
            }
            else if (Globals.EasterEggActive == true)
            {
                //fills rectangle with specified image
                canvas_mainmenu.Background = sprite_F_MMBackground;
                MMAlienSP1.Fill = sprite_F_AlienSP1;
                MMAlienSP2.Fill = sprite_F_AlienSP2;
                MMAlienSP3.Fill = sprite_F_AlienSP3;
                MMAlienSP4.Fill = sprite_F_AlienSP4;
                MMSpaceship.Fill = sprite_F_Spaceship;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //plays music specific to gamestates
            MusicEvents();

            //Gamestate check and update
            Gamestates();

            FadeBeginText();
        }

        private void FadeBeginText()
        {
            if (gameState == GameState.MainMenu)
            {
                if (Globals.beginfade == false)
                {
                    if (txt_Begin.Opacity != 0 || txt_Begin.Opacity >= 1)
                    {
                        txt_Begin.Opacity = txt_Begin.Opacity - 0.025;
                    }
                    if (txt_Begin.Opacity <= 0)
                    {
                        Globals.beginfade = true;
                    }
                }
                if (Globals.beginfade == true)
                {
                    if (txt_Begin.Opacity != 1 || txt_Begin.Opacity >= 0)
                    {
                        txt_Begin.Opacity = txt_Begin.Opacity + 0.025;
                    }
                    if (txt_Begin.Opacity >= 1)
                    {
                        Globals.beginfade = false;
                    }
                }
            }
        }

        private void MusicEvents()
        {
            if (gameState == GameState.MainMenu)
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri("mainmenu.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
            else if (gameState == GameState.GameOn)
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri("playgame.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
        }

        public void Gamestates()
        {

            if (gameState == GameState.MainMenu)
            {
                canvas_mainmenu.Visibility = Visibility.Visible;
                this.Title = "Main Menu";

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    //setupGame();
                    // if (gameState == GameState.MainMenu)
                    {
                        //    gameState = GameState.GameOn;
                        //    Globals.musicPlaying = false;
                    }
                    //else if (gameState == GameState.GameOn)
                    {
                        //  gameState = GameState.MainMenu;
                        //Globals.musicPlaying = false;
                    }
                }
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (Globals.isSpacePressed == false)
                    {

                        canvas_mainmenu.Visibility = Visibility.Hidden;
                        // canvas.battleground.Visibility = Visibility.Visible;
                        gameState = GameState.GameOn;
                        Globals.musicPlaying = false;
                        Globals.isSpacePressed = true;

                        List<SP1Aliens> SP1Aliens = new List<SP1Aliens>();


                    }
                }
                else if (Keyboard.IsKeyUp(Key.Space))
                {
                    Globals.isSpacePressed = false;
                }
            }



            else if (gameState == GameState.GameOn)
            {
                this.Title = "Round: " + Globals.currentRound.ToString();

                if (Globals.playerCreated == false)
                {
                    CreatePlayer();
                }
                player.Tick();

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (Globals.isSpacePressed == false)
                    {
                        if (Globals.EasterEggActive == false)
                        {
                            Globals.effectPlayer.Open(new Uri("SpaceShoot.wav", UriKind.Relative));
                            Globals.effectPlayer.Play();
                            Globals.isSpacePressed = true;
                        }
                        else if (Globals.EasterEggActive == true)
                        {
                            Globals.effectPlayer.Open(new Uri("boiShoot.wav", UriKind.Relative));
                            Globals.effectPlayer.Play();
                            Globals.isSpacePressed = true;
                        }
                    }
                }
                else if (Keyboard.IsKeyUp(Key.Space))
                {
                    Globals.isSpacePressed = false;
                }

                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    //setupGame();
                    if (gameState == GameState.MainMenu)
                    {
                        gameState = GameState.GameOn;
                        Globals.musicPlaying = false;
                    }
                    else if (gameState == GameState.GameOn)
                    {
                        gameState = GameState.MainMenu;
                        Globals.musicPlaying = false;
                    }
                }

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Clipboard.SetText(Mouse.GetPosition(this).ToString());
                }
            }



            else if (gameState == GameState.GameOver)
            {
                this.Title = "Game Over!";
            }
        }
    }
}