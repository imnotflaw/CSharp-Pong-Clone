using System.Runtime.InteropServices;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Ball
{
    private Vector2 _pos;
    public Vector2 pos
    {
        get => _pos;
        set => _pos = value;
    } 
    public Vector2 Velocity;
    public Vector2 vel
    {
        get => Velocity;
        set => Velocity = value;
    }
    public int size {get; set;} = 20;
    public Rectangle box => new Rectangle((int)pos.X, (int)pos.Y, size, size);

    public Ball(Vector2 bPos, Vector2 bVelocity)
    {
        _pos = bPos;
        Velocity = bVelocity;
    }

    public void Update(Paddle leftPaddle, Paddle rightPaddle, int sWidth, int sHeight)
    {
        _pos += Velocity;
        if (box.Intersects(leftPaddle.box))
        {
            Velocity.X *= -1.25f;
            _pos.X = leftPaddle.pos.X + leftPaddle.width;
        }
        else if (box.Intersects(rightPaddle.box))
        {
            Velocity.X *= -1.25f;
            _pos.X = rightPaddle.pos.X - size;
        }

        if (_pos.X < 0 || pos.X > sWidth)
        {
            _pos.Y = sHeight - size;
            Velocity = new Vector2(3, 3);
        }

        if (_pos.Y <= 0)
        {
            _pos.Y = 0;
            Velocity.Y *= -1;
        }
        else if (_pos.Y >= sHeight - size)
        {
            _pos.Y = sHeight - size;
            Velocity.Y *= -1;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.White);
    }
}