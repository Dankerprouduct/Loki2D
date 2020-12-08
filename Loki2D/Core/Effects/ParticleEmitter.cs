using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities;
using Microsoft.Xna.Framework;
using SharpDX.X3DAudio;

namespace Loki2D.Core.Effects
{
    public class ParticleEmitter
    {

        private float _emissionSpeed; 
        /// <summary>
        /// How often this particle emits in milliseconds
        /// </summary>
        public float EmissionSpeed
        {
            get => _emissionSpeed;
            set
            {
                _emissionSpeed = value;
                _particleTimer = new Timer(_emissionSpeed);
            }
        }
        
        /// <summary>
        /// The amount of particles to emit 
        /// </summary>
        public int BurstAmount { get; set; }

        /// <summary>
        /// The position the particles emit from
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The particle to be emitted 
        /// </summary>
        public ParticleDefinition ParticleDefinition { get; set; }

        public bool Enabled { get; set; }

        private Timer _particleTimer; 
        
        public ParticleEmitter(Vector2 position, ParticleDefinition particleDefinition, int burstAmount, float emissionSpeed)
        {
            Enabled = true; 

            Position = position;
            ParticleDefinition = particleDefinition;
            EmissionSpeed = emissionSpeed;
            BurstAmount = burstAmount; 
        }

        public void Update(GameTime gameTime)
        {
            if (_particleTimer.Update(gameTime))
            {
                for (int i = 0; i < BurstAmount; i++)
                {
                    Emit();
                }
            }
        }

        public void Emit()
        {
            ParticleSystem.Instance.AddParticle(Position, ParticleDefinition);
        }
    }
}
