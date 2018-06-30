using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hearn.MonoGame.Lightning
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private MouseState _mouseState;
        private MouseState _lastMouseState;

        private KeyboardState _keyboardState;

        private LightningStorm _storm;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            
            var source = new Vector2(10, 10);
            var dest = new Vector2(GraphicsDevice.Viewport.Width - 10, GraphicsDevice.Viewport.Height - 10);

            var electricBlue = new Color(44, 117, 255);

            const int StormDuration = 2000;
            const int BoltDuration = 500;
            
            _storm = new LightningStorm(source, dest, StormDuration, BoltDuration, electricBlue);
            _storm.NumberOfBolts = 4;
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LightningSegment = Content.Load<Texture2D>("line");
            Art.HalfCircle = Content.Load<Texture2D>("halfcircle");            
        }

        private bool MouseWasClicked()
        {
            return _mouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released;
        }

        protected override void Update(GameTime gameTime)
        {

            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            var keys = _keyboardState.GetPressedKeys();
            if (keys.Length !=0 && keys[0] == Keys.F12)
            {                
                _graphics.ToggleFullScreen();
            }

            _lastMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            if (MouseWasClicked())
            {
                _storm.Source = _storm.Dest;
                _storm.Dest = _mouseState.Position.ToVector2();
                _storm.Restart();
            }

            if (!_storm.IsComplete)
            {
                _storm.Update(gameTime);
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);

            _storm.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
