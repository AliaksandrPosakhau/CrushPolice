using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class Ball
{
    public Sprite sprite;
    float speed;
    Vector2f direction;

    const uint APPLICATION_WINDOW_WIDTH = 1400;
    const uint APPLICATION_WINDOW_HEIGHT = 1200;

    public Ball(Texture texture)
    {
        sprite = new Sprite(texture);
    }

    public void Release(float speed, Vector2f direction)
    {
        if (this.speed!=0)
        {
            return;
        }        
            this.speed = speed;
            this.direction = direction;       
    }

    public void Move(Vector2i boundsPosition, Vector2i boundsSize) {
        sprite.Position += direction * speed;

        if (sprite.Position.X > boundsSize.X - sprite.Texture.Size.X || sprite.Position.X<boundsPosition.X)
        {
            direction.X *= -1;
        }

        if (sprite.Position.Y <boundsPosition.Y)
        {
            direction.Y *= -1;
        } 

    }


    public bool CollisionCheck(Sprite sprite, string tag)
    {
        if (this.sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds())==true) {

            if (tag == "bulldozer")
            {
                direction.Y = -1;
                float f = ((this.sprite.Position.X+this.sprite.Texture.Size.X*0.5f)-(sprite.Position.X+sprite.Texture.Size.X*0.5f))/sprite.Texture.Size.X;
                direction.X = f*2;
            }

            if (tag == "police car")
            {
                direction.Y *= -1;
            }  
                return true;            
        }

        return false;
    }
}

