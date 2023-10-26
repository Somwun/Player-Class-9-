using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player_Class__9_
{
    internal class Food
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        private Color _color;
        public Food(Texture2D texture, int x, int y, Color color)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 10, 10);
            _speed = new Vector2(2);
            _color = color;
        }
        public void Move(List<Rectangle> items)
        {
            _location.X += (int)_speed.X;
            foreach(Rectangle item in items)
                if (_location.Intersects(item))
                {
                    _speed.X *= -1;
                    _location.X += (int)_speed.X;
                }
            _location.Y += (int)_speed.Y;
            foreach (Rectangle item in items)
                if (_location.Intersects(item))
                {
                    _speed.Y *= -1;
                    _location.Y += (int)_speed.Y;
                }
        }
        public void Bounce(GraphicsDeviceManager graphics)
        {
            if (_location.X >= graphics.PreferredBackBufferWidth)
                _speed.X *= -1;
            else if (_location.X <= 0)
                _speed.X *= -1;
            else if (_location.Y >= graphics.PreferredBackBufferHeight)
                _speed.Y *= -1;
            else if (_location.Y <= 0)
                _speed.Y *= -1;
        }
        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }
        public float HorizontalSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VerticalSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        public Rectangle Rectangle
        {
            get { return _location; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, _color);
        }
    }
}
