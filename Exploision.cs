using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
namespace Lab11
{
    internal class Exploision
    {
        #region Fields
        Texture2D sprite;
        Rectangle drawRectangle;
        bool isPlaying = false;
        int frameWidth = 32;
        int frameHeight = 32;
        // fields used to track and draw animations
        Rectangle sourceRectangle;
        int currentFrame;
        const int TotalFrameMilliseconds = 10;
        int elapsedFrameMilliseconds = 0;
        const int numFrames = 56;
        const int framesPerRow = 7;
        #endregion
        #region Constructor
        public Exploision(ContentManager contentManager, string spriteSheetFileName)
        {
            LoadContent(contentManager, spriteSheetFileName);
        }
        #endregion
        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }
        public void LoadContent(ContentManager contentManager, string spriteSheetFileName)
        {
            sprite = contentManager.Load<Texture2D>(spriteSheetFileName);
            drawRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }
        public void Update(GameTime gameTime)
        {
            if (isPlaying)
            {
                elapsedFrameMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameMilliseconds > TotalFrameMilliseconds)
                {
                    elapsedFrameMilliseconds = 0;
                    if (currentFrame < numFrames - 1)
                    {
                        currentFrame++;
                        SetSourceRectangleLocation(currentFrame);
                    }
                    else
                    {
                        isPlaying = false;
                    }
                }
            }
        }
        public void Play(int xOrigin, int yOrigin)
        {
            isPlaying = true;
            elapsedFrameMilliseconds = 0;
            currentFrame = 0;
            // set draw location and source rectangle
            drawRectangle.X = xOrigin - drawRectangle.Width / 2;
            drawRectangle.Y = yOrigin - drawRectangle.Height / 2;
            SetSourceRectangleLocation(currentFrame);
        }
        #endregion 
        #region Private Methods
        private void SetSourceRectangleLocation(int frameNumber)
        {
            // calculate X and Y based on frame number
            sourceRectangle.X = (frameNumber % framesPerRow) * frameWidth;
            sourceRectangle.Y = (frameNumber / framesPerRow) * frameHeight;
        }
        #endregion
    }
}
