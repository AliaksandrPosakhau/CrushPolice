using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System; 
using SFML.Audio;



class Program
{
    static RenderWindow window;

    const int APPLICATION_WINDOW_WIDTH = 1400;
    const int APPLICATION_WINDOW_HEIGHT = 1200;
    const string gameTitle = "Crush Police 1.0";
    const int POLICE_CARS_INITIAL_AMOUNT = 100;

    static Texture ballTexture;
    static Texture bulldozerTexture; // stickTexture
    static Texture policeCarTexture; // blockTexture

    static Sprite steelBallSprite;
    static Sprite bulldozer;
    static PoliceCar[] policeCarsArray;
    static Ball steelBall;
    static int initialBallSpeed = 10;
     
    static Boolean gameOverFlag = false;
    public static void SetStartPosition()
    {
        int index = 0;
        for (int y = 0; y < 10; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {
                policeCarsArray[index].sprite.Position = new Vector2f(x * (policeCarsArray[index].sprite.TextureRect.Width + 25) + 100, y * (policeCarsArray[index].sprite.TextureRect.Height + 15) + 75);
                index++;
            }
        }
        bulldozer.Position = new Vector2f((APPLICATION_WINDOW_WIDTH / 2) - 100, APPLICATION_WINDOW_HEIGHT - 200);
        steelBall.sprite.Position = new Vector2f((APPLICATION_WINDOW_WIDTH / 2) - 50, APPLICATION_WINDOW_HEIGHT - 230);
    }

    static Music backgroundMusic = new Music("resources/sound/background.ogg");
   
    public static void PlayBackground()
    {
        backgroundMusic.Play();       
    }

    public static void StopBackground()
    {
        backgroundMusic.Stop();
    }

    public static void GameOver()
    {
        steelBall.Freeze();
        steelBall.SetBallGaveOverState(true);

        for (int i = 0;i<policeCarsArray.Length;i++) {
            policeCarsArray[i].Hide();
        }
        
    }

    static void Main(string[] args)
    {
        window = new RenderWindow(new VideoMode(APPLICATION_WINDOW_WIDTH, APPLICATION_WINDOW_HEIGHT), gameTitle);
        window.SetFramerateLimit(60);
        window.Closed += Window_Closed;


        ballTexture = new Texture("resources/spikedBall.png");
        bulldozerTexture = new Texture("resources/bld.png");
        policeCarTexture = new Texture("resources/policeCar100.png");

        steelBall = new Ball(ballTexture);
        bulldozer = new Sprite(bulldozerTexture);
        policeCarsArray = new PoliceCar[POLICE_CARS_INITIAL_AMOUNT];

        for (int i = 0; i < policeCarsArray.Length; i++)
        {
            policeCarsArray[i] = new PoliceCar(policeCarTexture);
        }

        SetStartPosition();


        while (window.IsOpen)
        {
            window.Clear();
            window.DispatchEvents();

            if(Mouse.IsButtonPressed(Mouse.Button.Left)==true) { 
                if(gameOverFlag==false)
                {
                    steelBall.Release(initialBallSpeed, new Vector2f(0, -1));
                    // PlayBackground();
                }
            }

            steelBall.Move(new Vector2i(0,0),new Vector2i(APPLICATION_WINDOW_WIDTH,APPLICATION_WINDOW_HEIGHT));
            steelBall.CollisionCheck(bulldozer, "bulldozer");
            
            for (int i = 0;i < policeCarsArray.Length;++i)
            {
               if(steelBall.CollisionCheck(policeCarsArray[i].sprite,"police car")==true)
                {                    
                    policeCarsArray[i].MovePoliceCarOff();
                    policeCarsArray[i].playCrashSound();
                    break;
                }
            }
            
            if (gameOverFlag==false) {
                bulldozer.Position = new Vector2f(Mouse.GetPosition(window).X - bulldozer.TextureRect.Width * 0.5f, bulldozer.Position.Y);
            }
            

            window.Draw(steelBall.sprite);
            window.Draw(bulldozer);
            for (int i = 0; i < policeCarsArray.Length; i++)
            {
                window.Draw(policeCarsArray[i].sprite);
            }

            if (steelBall.BallLooseCheck()==true)
            {
                //Console.WriteLine("The ball is lost. Game over.");
                GameOver();
                gameOverFlag = true;
            }

            window.Display();
        }
    }

    private static void Window_Closed(object sender, EventArgs e)
    {
        Console.WriteLine("Window closed.");
        window.Close();
    }
}


