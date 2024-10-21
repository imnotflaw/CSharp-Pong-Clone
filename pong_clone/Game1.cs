using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong_clone;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private int player1Score = 0;
    private int player2Score = 0;
    private Paddle _leftPaddle;
    private Paddle _rightPaddle;
    private Ball _ball;
    private Texture2D whitePixel;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        whitePixel = new Texture2D(GraphicsDevice, 1, 1);
        whitePixel.SetData(new[] {Color.White});

        _leftPaddle = new Paddle(new Vector2(20, GraphicsDevice.Viewport.Height / 2 - 50));
        _rightPaddle = new Paddle(new Vector2(GraphicsDevice.Viewport.Width - 30, GraphicsDevice.Viewport.Height / 2 - 50));

        _ball = new Ball(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Vector2(3, 3));
    }

    protected override void Update(GameTime gameTime)
    {
        Window.Title = $"C# Pong Clone | Score:     Player1: { player1Score }      Player2: { player2Score }";
        KeyboardState keyboardState = Keyboard.GetState();

        _leftPaddle.Update(keyboardState, Keys.W, Keys.S, GraphicsDevice.Viewport.Height);
        _rightPaddle.Update(keyboardState, Keys.Up, Keys.Down, GraphicsDevice.Viewport.Height);
        _ball.Update(_leftPaddle, _rightPaddle, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        if (_ball.pos.Y < 0 || _ball.pos.Y > GraphicsDevice.Viewport.Height - 10)
            _ball.Velocity *= -1;
        
        if (_ball.pos.X < 0)
        {
            _ball.pos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _ball.Velocity = new Vector2(3, 3);
            player2Score++;
            
        }
        else if (_ball.pos.X > GraphicsDevice.Viewport.Width)
        {
            _ball.pos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _ball.Velocity = new Vector2(3, 3);
            player1Score++;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _leftPaddle.Draw(_spriteBatch, whitePixel);
        _rightPaddle.Draw(_spriteBatch, whitePixel);
        _ball.Draw(_spriteBatch, whitePixel);

        base.Draw(gameTime);
        _spriteBatch.End();
    }
}