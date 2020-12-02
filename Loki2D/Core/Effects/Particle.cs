using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities;
using Loki2D.Core.Utilities.MathHelper;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Effects
{
    public class Particle
    {
        private static Random _random = new Random();

        public bool Alive { get; set; }
        private int _particleIndex;

        private Vector2 _velocity { get; set; }
        public Vector2 Position { get; set; }

        private ParticleDefinition _particleDefinition;
        private Timer _particleLifeTimer; 

        public Particle(int index)
        {
            _particleIndex = index; 
            _particleDefinition = new ParticleDefinition();
            Alive = false; 
        }

        public void CreateParticle(Vector2 position, ParticleDefinition definition)
        {
            Alive = true;

            Position = position; 
            _particleDefinition = definition;
            _particleDefinition.Color *= _particleDefinition.Alpha;
            _particleDefinition.Size += (float)_random.Next(_particleDefinition.MinSizeVariance, _particleDefinition.MaxSizeVariance) / 100;
            _particleDefinition.Rotation += MathHelper.ToRadians((float)_random.Next(_particleDefinition.MinAngleVariance, _particleDefinition.MaxAngleVariance));
            _particleDefinition.Dampening += (float) _random.Next(_particleDefinition.MinDampeningVariance, _particleDefinition.MaxDampeningVariance) / 100;

            _particleLifeTimer = new Timer(_particleDefinition.Lifetime);

            // F=MA
            // A = F/M => V(t) = F/M
            var velocity = _particleDefinition.Force / _particleDefinition.Mass;
            var xComponent = (float)Math.Cos(_particleDefinition.Rotation) * velocity; 
            var yComponent = (float)Math.Sin(_particleDefinition.Rotation) * velocity;

            _velocity = new Vector2(xComponent, yComponent);

        }

        public void Update(GameTime gameTime)
        {
            if (_velocity.Length() <= _particleDefinition.MinSpeed)
            {
                Destroy();
                return;
            }

            if (_particleDefinition.Size <= _particleDefinition.MinSize)
            {
                Destroy();
                return;
            }

            if (_particleLifeTimer.Update(gameTime.ElapsedGameTime.Milliseconds))
            {
                Destroy();
                return;
            }

            _particleDefinition.Color *= _particleDefinition.AlphaDelta;
            _particleDefinition.Size *= _particleDefinition.SizeDelta;
            _particleDefinition.Rotation += _particleDefinition.RotationDelta;
            _particleDefinition.TextureRotation += _particleDefinition.RotationDelta;

            _velocity *= _particleDefinition.Dampening; 
            Position += _velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TextureManager.Instance.GetTexture(_particleDefinition.Texture), 
                Position, 
                null, 
                _particleDefinition.Color, 
                _particleDefinition.TextureRotation, 
                _particleDefinition.TextureOrigin, 
                _particleDefinition.Size, 
                SpriteEffects.None, 
                1f);
        }

        /// <summary>
        /// Marks this particle as inactive
        /// </summary>
        public void Destroy()
        {
            Alive = false;
            ParticleSystem.Instance.AddToOpenList(_particleIndex);
        }
    }
}
