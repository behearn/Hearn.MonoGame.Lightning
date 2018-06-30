using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearn.MonoGame.Lightning
{
    class Line
    {

        private Vector2 _a;
        private Vector2 _b;
        private float _thickness;

        public Line(Vector2 a, Vector2 b, float thickness = 1)
        {
            _a = a;
            _b = b;
            _thickness = thickness;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            var tangent = _b - _a;
            var rotation = (float)Math.Atan2(tangent.Y, tangent.X);

            const float ImageThickness = 8;
            var thicknessScale = _thickness / ImageThickness;

            var capOrigin = new Vector2(Art.HalfCircle.Width, Art.HalfCircle.Height / 2f);
            var middleOrigin = new Vector2(0, Art.LightningSegment.Height / 2f);
            var middleScale = new Vector2(tangent.Length(), thicknessScale);

            spriteBatch.Draw(Art.LightningSegment, _a, null, color, rotation, middleOrigin, middleScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Art.HalfCircle, _a, null, color, rotation, capOrigin, thicknessScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Art.HalfCircle, _b, null, color, rotation + MathHelper.Pi, capOrigin, thicknessScale, SpriteEffects.None, 0f);
        }

    }
}
