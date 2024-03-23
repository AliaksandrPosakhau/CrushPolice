using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;


class Program
{
    static RenderWindow window;

    const int APPLICATION_WINDOW_WIDTH = 1400;
    const int APPLICATION_WINDOW_HEIGHT = 1200;
    const string gameTitle = "Police Mess 1.0";

    static Texture ballTexture;
    static Texture bulldozerTexture; // stickTexture
    static Texture policeCarTexture; // blockTexture

    static Sprite steelBallSprite;
    static Sprite bulldozer;
    static Sprite[] policeCarsArray;
    static Ball steelBall;
    static int initialBallSpeed = 10;

    public static void SetStartPosition()
    {
        int index = 0;
        for (int y = 0; y < 10; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {
                policeCarsArray[index].Position = new Vector2f(x * (policeCarsArray[index].TextureRect.Width + 25) + 100, y * (policeCarsArray[index].TextureRect.Height + 15) + 75);
                index++;
            }
        }
        bulldozer.Position = new Vector2f((APPLICATION_WINDOW_WIDTH / 2) - 100, APPLICATION_WINDOW_HEIGHT - 200);
        steelBall.sprite.Position = new Vector2f((APPLICATION_WINDOW_WIDTH / 2) - 50, APPLICATION_WINDOW_HEIGHT - 230);
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
        policeCarsArray = new Sprite[100];

        for (int i = 0; i < policeCarsArray.Length; i++)
        {
            policeCarsArray[i] = new Sprite(policeCarTexture);
        }

        SetStartPosition();


        while (window.IsOpen)
        {
            window.Clear();
            window.DispatchEvents();

            if(Mouse.IsButtonPressed(Mouse.Button.Left)==true) {
                steelBall.Release(initialBallSpeed, new Vector2f(0, -1));
            }

            steelBall.Move(new Vector2i(0,0),new Vector2i(APPLICATION_WINDOW_WIDTH,APPLICATION_WINDOW_HEIGHT));
            steelBall.CollisionCheck(bulldozer, "bulldozer");
            
            for (int i = 0;i < policeCarsArray.Length;++i)
            {
               if(steelBall.CollisionCheck(policeCarsArray[i],"police car")==true)
                {
                    policeCarsArray[i].Position = new Vector2f(APPLICATION_WINDOW_WIDTH + 300, APPLICATION_WINDOW_HEIGHT+300);
                    break;
                }
            }
                        
            bulldozer.Position = new Vector2f(Mouse.GetPosition(window).X - bulldozer.TextureRect.Width * 0.5f, bulldozer.Position.Y);

            window.Draw(steelBall.sprite);
            window.Draw(bulldozer);
            for (int i = 0; i < policeCarsArray.Length; i++)
            {
                window.Draw(policeCarsArray[i]);
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


