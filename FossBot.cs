using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Lab11
{
    internal class FossBot
    {
        #region Fields
        Texture2D sprite;
        Rectangle drawRectangle;
        int windowWidthBounds;
        int windowHeightBounds;
        Random randomNumberGenerator = new Random(Guid.NewGuid().GetHashCode());
        Vector2 movementVelocityVector = Vector2.Zero;
        bool isActive = true;
        #endregion
        #region Properties
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
        #endregion
        #region Constructor
        public FossBot(ContentManager contentManager, string spriteSheetFileName, int xPosition, int yPosition, int windowWidthBounds, int windowHeightBounds)
        {
            this.windowWidthBounds = windowWidthBounds;
            this.windowHeightBounds = windowHeightBounds;
            LoadContent(contentManager, spriteSheetFileName, xPosition, yPosition);
            // Generate Random velocity
            int speed = 0;
            double angle = 2 * Math.PI * randomNumberGenerator.NextDouble();
            movementVelocityVector.X = (float)Math.Cos(angle) * speed;
            movementVelocityVector.Y = -1 * (float)Math.Sin(angle) * speed;
        }
        #endregion
        #region Public Methods
        public void Update()
        {
            drawRectangle.X += (int)movementVelocityVector.X;
            drawRectangle.Y += (int)movementVelocityVector.Y;
            Bounce();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }
        /// <summary>
        ///  Returns the current X Position of the FossBot's drawRectange.X
        /// </summary>
        /// <returns>int</returns>
        public Rectangle GetCurrentPositionRectangle()
        {
            return drawRectangle;
        }
        #endregion
        #region Private Methods
        private void LoadContent(ContentManager contentManager, string spriteSheetFileName, int xPosition, int yPosition)
        {
            sprite = contentManager.Load<Texture2D>(spriteSheetFileName);
            drawRectangle = new Rectangle(xPosition - sprite.Width / 2, yPosition - sprite.Height / 2, sprite.Width, sprite.Height);
        }
        private void Bounce()
        {
            // Bounce top and bottom of screen
            if (drawRectangle.Y < 0)
            {
                drawRectangle.Y = 0;
                movementVelocityVector.Y *= -1;
            }
            else if (drawRectangle.Y + drawRectangle.Height > windowHeightBounds)
            {
                drawRectangle.Y = windowHeightBounds - drawRectangle.Height;
                movementVelocityVector.Y *= -1;
            }
            // Bounce Left and right of the screen
            if (drawRectangle.X < 0)
            {
                drawRectangle.X = 0;
                movementVelocityVector.X *= -1;
            }
            else if (drawRectangle.X + drawRectangle.Width > windowWidthBounds)
            {
                drawRectangle.X = windowWidthBounds - drawRectangle.Width;
                movementVelocityVector.X *= -1;
            }
        }
        #endregion
    }
}
