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
        List<Food> goodFood, badFood;
        List<Rectangle> barriers;
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
            goodFood = new List<Food>();
            goodFood.Add(new Food(foodTexture, 40, 50, Color.Green));
            goodFood.Add(new Food(foodTexture, 200, 50, Color.Green));
            goodFood.Add(new Food(foodTexture, 34, 75, Color.Green));
            badFood = new List<Food>();
            badFood.Add(new Food(foodTexture, 100, 20, Color.Red));
            badFood.Add(new Food(foodTexture, 250, 100, Color.Red));
            badFood.Add(new Food(foodTexture, 20, 85, Color.Red));
            amoeba.HorizontalSpeed = 0;
            amoeba.VerticalSpeed = 0;
            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));
            barriers.Add(new Rectangle(-1, -1, 1, _graphics.PreferredBackBufferHeight + 1));
            barriers.Add(new Rectangle(-1, -1, _graphics.PreferredBackBufferWidth, 1));
            barriers.Add(new Rectangle(-1, _graphics.PreferredBackBufferHeight + 1, _graphics.PreferredBackBufferWidth, 1));
            barriers.Add(new Rectangle(_graphics.PreferredBackBufferWidth + 1, -1, 1, _graphics.PreferredBackBufferHeight));
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
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                amoeba.VerticalSpeed += speed;

            //Objects
            amoeba.Move();
            foreach (Rectangle barrier in barriers)
                if (amoeba.Collide(barrier))
                    amoeba.UndoMove();
            for (int i = 0; i < goodFood.Count; i++)
            {
                goodFood[i].Move(barriers);
                goodFood[i].Bounce(_graphics);
                if (amoeba.Collide(goodFood[i]))
                {
                    goodFood.RemoveAt(i);
                    amoeba.Grow();
                    i--;
                }
            }
            for (int i = 0; i < badFood.Count; i++)
            {
                badFood[i].Move(barriers);
                badFood[i].Bounce(_graphics);
                if (amoeba.Collide(badFood[i]))
                    Exit();
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            amoeba.Draw(_spriteBatch);
            foreach (Food item in goodFood)
                item.Draw(_spriteBatch);
            foreach (Food item in badFood)
                item.Draw(_spriteBatch);
            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(wallTexture, barrier, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}