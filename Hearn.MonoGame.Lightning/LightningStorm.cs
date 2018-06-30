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

        private int _intervalElapsed;
        private int _interval;

        private int _stormElapsed;
        private int _stormDuration;

        private int _boltDuration;

        private Color _tint;

        public LightningStorm(Vector2 source, Vector2 dest, int stormDuration, int boltDuration, Color tint)
        {
            _bolts = new List<LightningBolt>();

            Source = source;
            Dest = dest;

            _stormDuration = stormDuration;
            _boltDuration = boltDuration;
            _tint = tint;

            NumberOfBolts = 3;
        }

        public Vector2 Source { get; set; }
        public Vector2 Dest { get; set; }
        public int NumberOfBolts { get; set; }

        public bool IsComplete { get => _stormElapsed >= _stormDuration && !_bolts.Any(); }

        public void Update(GameTime gameTime)
        {
            _bolts.RemoveAll(b => b.IsComplete);

            _stormElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (_stormElapsed < _stormDuration)
            {
                _interval = (int)(_boltDuration / NumberOfBolts);
                _intervalElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (_intervalElapsed >= _interval)
                {
                    _intervalElapsed -= _interval;
                    if (_bolts.Count <= NumberOfBolts)
                    {
                        var bolt = new LightningBolt(Source, Dest, _boltDuration, _tint);
                        _bolts.Add(bolt);
                    }
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

        public void Restart()
        {
            _stormElapsed = 0;
        }

    }
}
