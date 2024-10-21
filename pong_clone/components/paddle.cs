using System;
using System.Reflection.PortableExecutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Paddle 
{
    private Vector2 _pos;
    public Vector2 pos
    {
        get => _pos;
        set => _pos = value;
    } 
    public int width {get; set;} = 10;
    public int height {get; set;} = 100;
    public int speed {get; set;} = 10;
    public Rectangle box => new Rectangle((int)pos.X, (int)pos.Y, width, height);

    public Paddle(Vector2 pPos)
    {
        _pos = pPos;
    }

    public void Update(KeyboardState keyboardState, Keys upKey, Keys downKey, int screenHeight)
    {
        if (keyboardState.IsKeyDown(upKey))
            _pos.Y -= speed;
        if (keyboardState.IsKeyDown(downKey))
            _pos.Y += speed;

        _pos.Y = Math.Clamp(pos.Y, 0, screenHeight - height);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
    }
}