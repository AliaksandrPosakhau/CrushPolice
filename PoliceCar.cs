using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class PoliceCar
    {

    public Sprite sprite;

    SoundBuffer crashPoliceCarSoundBuffer = new SoundBuffer(("resources/sound/crashCar.wav"));
    Sound crashPoliceCarSound = new Sound();

    SoundBuffer alarmSoundBuffer = new SoundBuffer(("resources/sound/carAlarm.wav"));
    Sound alarmSound = new Sound();


    const uint APPLICATION_WINDOW_WIDTH = 1400;
    const uint APPLICATION_WINDOW_HEIGHT = 1200;

    private int resistance;

    public int  getResistance() { return resistance; }
    public void setResistance(int resistance) {         
        this.resistance = resistance; 
    }
    public PoliceCar(Texture texture)
    {
        Random resistanceRandom = new Random();        
        sprite = new Sprite(texture);
        this.setResistance(resistanceRandom.Next(0, 2));
        Console.WriteLine("resistance : "+getResistance());
    }
 
    public void  playCrashSound()
    {
        this.crashPoliceCarSound.SoundBuffer = crashPoliceCarSoundBuffer;
        this.crashPoliceCarSound.Play();
    }

    public void playCarAlarmSound()
    {
        this.alarmSound.SoundBuffer = alarmSoundBuffer;
        this.alarmSound.Play();
    }

    public void MovePoliceCarOff() {
        int currentResistance;
        if (this.getResistance()==0) {
            this.sprite.Position = new Vector2f(APPLICATION_WINDOW_WIDTH + 300, APPLICATION_WINDOW_HEIGHT + 300);
        } else
        {
            this.playCarAlarmSound();
            currentResistance = this.getResistance();
            this.setResistance(currentResistance--);
        }
    }
}
 
