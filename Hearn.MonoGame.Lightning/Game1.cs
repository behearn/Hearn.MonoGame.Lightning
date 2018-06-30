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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private MouseState mouseState;
        private MouseState lastMouseState;

        private LightningBolt bolt;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;

        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LightningSegment = Content.Load<Texture2D>("line");
            Art.HalfCircle = Content.Load<Texture2D>("halfcircle");
            
        }

        protected override void UnloadContent()
        {
        } 

        protected override void Update(GameTime gameTime)
        {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            var screenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);

            if (MouseWasClicked())
                bolt = new LightningBolt(screenSize / 2, mousePosition, new Color(44, 117, 255));

            if (bolt != null)
                bolt.Update();
        }

        private bool MouseWasClicked()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);

            if (bolt != null)
                bolt.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
