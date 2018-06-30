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

            _storm = new LightningStorm(source, dest, 500, electricBlue);
            _storm.NumberOfBolts = 4;
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LightningSegment = Content.Load<Texture2D>("line");
            Art.HalfCircle = Content.Load<Texture2D>("halfcircle");            
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
            
            _storm.Update(gameTime);

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
