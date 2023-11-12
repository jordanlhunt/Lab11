using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Lab11
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;
        // Player 
        FossBot player;
        // Explosion
        Exploision exploision;
        // Mouse support
        MouseState currentMouseState;
        ButtonState previousMouseButtonState = ButtonState.Released;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new FossBot(Content, "Graphics/sarabot", WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, WINDOW_WIDTH, WINDOW_HEIGHT);
            exploision = new Exploision(Content, "Graphics/32x32Boom");
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (player.IsActive)
            {
                player.Update();
            }

            currentMouseState = Mouse.GetState();
            HandleMouseState(currentMouseState);
            previousMouseButtonState = currentMouseState.LeftButton;
            exploision.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (player.IsActive)
            {
                player.Draw(_spriteBatch);
            }
            exploision.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void HandleMouseState(MouseState currentMouseState)
        {
            CheckPlayerClick(player.GetCurrentPositionRectangle(), currentMouseState);
        }
        private void CheckPlayerClick(Rectangle playerRectangle, MouseState curerntMouseState)
        {
            int mouseX = currentMouseState.Position.X;
            int mouseY = currentMouseState.Position.Y;
            ButtonState mouseLeftButton = currentMouseState.LeftButton;
            if (playerRectangle.Contains(currentMouseState.Position) && mouseLeftButton == ButtonState.Released && previousMouseButtonState == ButtonState.Pressed)
            {
                player.IsActive = false;
                exploision.Play(playerRectangle.X, playerRectangle.Y);
            }
        }
    }
}