using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearn.MonoGame.Lightning
{
    class LightningBolt
    {

        private int _duration;
        private int _elapsed;
        private float _alpha;
        private Color _tint;
        private List<Line> _segments = new List<Line>();
        
        public LightningBolt(Vector2 source, Vector2 dest, int duration) : this(source, dest, 300, new Color(0.9f, 0.8f, 1f)) { }

        public LightningBolt(Vector2 source, Vector2 dest, int duration, Color color)
        {
            _segments = CreateBolt(source, dest, 2);
            _duration = duration;
            _tint = color;
            _alpha = 1f;
        }

        public bool IsComplete { get => _elapsed >= _duration; }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var segment in _segments)
            {
                segment.Draw(spriteBatch, _tint * _alpha);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime.Milliseconds;

            _alpha = 1f - ((float)_elapsed / _duration);
            if (_alpha < 0)
            {
                _alpha = 0;
            }          
        }

        protected static List<Line> CreateBolt(Vector2 source, Vector2 dest, float thickness)
        {
            var results = new List<Line>();
            var tangent = dest - source;
            var normal = Vector2.Normalize(new Vector2(tangent.Y, -tangent.X));
            var length = tangent.Length();

            var positions = new List<float>();
            positions.Add(0);

            var rnd = new Random();

            for (int i = 0; i < length / 4; i++)
            {
                positions.Add((float)rnd.NextDouble());
            }

            positions.Sort();

            const int Sway = 8000;
            const float Jaggedness = 1f / (Sway / 100f);

            Vector2 prevPoint = source;
            float prevDisplacement = 0;
            for (int i = 1; i < positions.Count; i++)
            {
                var pos = positions[i];

                // used to prevent sharp angles by ensuring very close positions also have small perpendicular variation.
                var scale = (length * Jaggedness) * (pos - positions[i - 1]);

                // defines an envelope. Points near the middle of the bolt can be further from the central line.
                var envelope = pos > 0.95f ? 20 * (1 - pos) : 1;

                var displacement = rnd.Next(-Sway, Sway) / 100f ;
                displacement -= (displacement - prevDisplacement) * (1 - scale);
                displacement *= envelope;

                var point = source + pos * tangent + displacement * normal;
                results.Add(new Line(prevPoint, point, thickness));
                prevPoint = point;
                prevDisplacement = displacement;
            }

            results.Add(new Line(prevPoint, dest, thickness));

            return results;
        }
        
    }
}
