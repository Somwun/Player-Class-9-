using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Player_Class__9_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        Texture2D amoebaTexture, wallTexture, foodTexture;
        Player amoeba;
        Rectangle amoebaRect;
        List<Rectangle> barriers, food;
        int speed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            amoeba = new Player(amoebaTexture, 10, 10);
            amoeba.HorizontalSpeed = 0;
            amoeba.VerticalSpeed = 0;
            amoebaRect = new Rectangle(10, 10, 100, 100);
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));
            food = new List<Rectangle>();
            food.Add(new Rectangle(50, 50, 10, 10));
            food.Add(new Rectangle(600, 100, 10, 10));
            food.Add(new Rectangle(50, 200, 10, 10));
            speed = 4;
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            wallTexture = Content.Load<Texture2D>("rectangle");
            foodTexture = Content.Load<Texture2D>("circle");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            amoeba.HorizontalSpeed = 0;
            amoeba.VerticalSpeed = 0;
            keyboardState = Keyboard.GetState();
            //Movement
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                amoeba.HorizontalSpeed += speed;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                amoeba.HorizontalSpeed -= speed;
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                amoeba.VerticalSpeed -= speed;
            else if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                amoeba.VerticalSpeed += speed;          
            //Objects
            for (int i = 0; i < food.Count; i++)
                if (amoeba.Collide(food[i]))
                {
                    food.RemoveAt(i);
                    i--;
                    amoeba.Grow();
                }
            amoeba.Move();
            foreach (Rectangle barrier in barriers)
                if (amoeba.Collide(barrier))
                    amoeba.UndoMove();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            amoeba.Draw(_spriteBatch);
            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(wallTexture, barrier, Color.White);
            foreach (Rectangle bit in food)
                _spriteBatch.Draw(foodTexture, bit, Color.Green);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}