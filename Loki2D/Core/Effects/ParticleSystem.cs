using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Effects
{
    public class ParticleSystem: SystemManager<ParticleSystem>
    {
        private int _poolSize;
        private static Particle[] _particles; 
        private static List<int> _deadParticles;

        private List<ParticleEmitter> _particleEmitters = new List<ParticleEmitter>();

        public static bool Enable { get; set; }

        public ParticleSystem(int poolSize)
        {
            Init(poolSize);
        }

        public void Init(int poolSize)
        {
            _poolSize = poolSize;
            _particles = new Particle[_poolSize];
            _deadParticles = new List<int>();

            for (int i = 0; i < _poolSize; i++)
            {
                _particles[i] = new Particle(i);
                _deadParticles.Add(i);
            }
        }

        public void AddParticle(Vector2 position, ParticleDefinition particleDefinition)
        {
            if (_deadParticles.Count <= 0) return;

            for (int i = 0; i < _deadParticles.Count; i++)
            {
                if (!_particles[_deadParticles[i]].Alive)
                {
                    _particles[_deadParticles[i]].CreateParticle(position, particleDefinition);
                    _deadParticles.RemoveAt(i);
                    return;
                }
            }
        }

        public void AddToOpenList(int i)
        {
            _deadParticles.Add(i);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(!Enable)
                return;

            for (int i = 0; i < _particles.Length; i++)
            {
                if (_particles[i].Alive)
                {
                    _particles[i].Update(gameTime);
                }
            }

            
            foreach (var emitter in _particleEmitters)
            {
                if (emitter.Enabled)
                {
                    emitter.Update(gameTime);
                }
            }
        }

        public void AddEmitter(ParticleEmitter emitter)
        {
            _particleEmitters.Add(emitter);
        }

        public void RemoveEmitter(ParticleEmitter emitter)
        {
            _particleEmitters.Remove(emitter); 
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!Enable)
                return;

            foreach (var particle in _particles)
            {
                if(particle.Alive)
                    particle.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        public override void OnDestroy()
        {
            
        }
    }
}
