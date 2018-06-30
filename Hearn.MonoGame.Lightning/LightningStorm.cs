using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearn.MonoGame.Lightning
{
    class LightningStorm
    {

        private List<LightningBolt> _bolts;
        private int _elapsed;
        private int _interval;

        private int _duration;
        private Color _tint;

        public LightningStorm(Vector2 source, Vector2 dest, int duration, Color tint)
        {
            _bolts = new List<LightningBolt>();

            Source = source;
            Dest = dest;
            _duration = duration;
            _tint = tint;

            NumberOfBolts = 3;
        }

        private Vector2 Source { get; set; }
        private Vector2 Dest { get; set; }
        public int NumberOfBolts { get; set; }

        public void Update(GameTime gameTime)
        {
            _bolts.RemoveAll(b => b.IsComplete);

            _interval = (int)(_duration / NumberOfBolts);
            _elapsed += gameTime.ElapsedGameTime.Milliseconds;            
            if (_elapsed >= _interval)
            {
                _elapsed -= _interval;
                if (_bolts.Count <= NumberOfBolts)
                {
                    var bolt = new LightningBolt(Source, Dest, _duration, _tint);
                    _bolts.Add(bolt);
                }
            }

            foreach (var bolt in _bolts)
            {
                bolt.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bolt in _bolts)
            {
                bolt.Draw(spriteBatch);
            }
        }

    }
}
