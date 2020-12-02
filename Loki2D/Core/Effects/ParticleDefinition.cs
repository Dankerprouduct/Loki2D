using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Core.Effects
{
    using Microsoft.Xna.Framework;
    public struct ParticleDefinition
    {
        /// <summary>
        /// Texture of the particle
        /// </summary>
        public string Texture { get; set; }
        
        /// <summary>
        /// Color of the particle
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// The maximum lifetime of the particle before its removed.
        /// Measured in milliseconds
        /// </summary>
        public int Lifetime { get; set; }

        /// <summary>
        /// Initial size of the particle
        /// </summary>
        public float Size { get; set; }
        
        /// <summary>
        /// How much variance is added to the initial size
        /// Size += Random(MinSizeVariance, MaxSizeVariance) / 100
        /// </summary>
        public int MinSizeVariance { get; set; }

        /// <summary>
        /// How much variance is added to the initial size
        /// Size += Random(MinSizeVariance, MaxSizeVariance) / 100
        /// </summary>
        public int MaxSizeVariance { get; set; }

        /// <summary>
        /// The minimum size of the particle before its delted
        /// </summary>
        public float MinSize { get; set; }
        
        /// <summary>
        /// How much the Particle changes over time
        /// Size *= SizeDelta
        /// </summary>
        public float SizeDelta { get; set; }

        /// <summary>
        /// Initial Alpha of the particle's texture
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// The rate of change of the texture's alpha
        /// Alpha *= AlphaDelta
        /// </summary>
        public float AlphaDelta { get; set; }

        /// <summary>
        /// The initial rotation of the particle
        ///
        /// Measured In Radians
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// How much variance is added to the initial rotation
        /// Rotation += Random(MinAngleVariance, MaxAngleVariance)
        ///
        /// Measured In Degrees
        /// </summary>
        public int MinAngleVariance { get; set; }

        /// <summary>
        /// How much variance is added to the initial rotation
        /// Rotation += Random(MinAngleVariance, MaxAngleVariance)
        ///
        /// Measured In Degrees
        /// </summary>
        public int MaxAngleVariance { get; set; }


        /// <summary>
        /// How much the rotation is changed overtime
        /// Rotation += RotationDelta
        ///
        /// Measured In Radians
        /// </summary>
        public float RotationDelta { get; set; }

        /// <summary>
        /// The Initial Rotation of the texture
        ///
        /// Measured In Radians
        /// </summary>
        public float TextureRotation { get; set; }

        /// <summary>
        /// How much the texture rotates over time
        /// TextureRotation += TextureRotationDelta
        ///
        /// Measured In Radians
        /// </summary>
        public float TextureRotationDelta { get; set; }
        
        /// <summary>
        /// The origin of the texture
        /// </summary>
        public Vector2 TextureOrigin { get; set; }

        /// <summary>
        /// the minimum speed of the particle before its deleted
        /// </summary>
        public float MinSpeed { get; set; }

        /// <summary>
        /// The Mass of the particle
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// The amount of force being added to this particle
        /// </summary>
        public float Force { get; set; }

        /// <summary>
        /// How much the Velocity is being Dampened
        /// </summary>
        public float Dampening { get; set; }

        /// <summary>
        /// How much variance is added to the initial dampening
        /// Dampening += Random(MaxDampeningVariance, MaxDampeningVariance) / 100
        /// Velocity *= Dampening
        /// </summary>
        public int MinDampeningVariance { get; set; }

        /// <summary>
        /// How much variance is added to the initial dampening
        /// Dampening += Random(MaxDampeningVariance, MaxDampeningVariance) / 100
        /// Velocity *= Dampening
        /// </summary>
        public int MaxDampeningVariance { get; set; }
        

        /// <summary>
        /// Default particle
        /// </summary>
        public static ParticleDefinition DefaultParticle =>
            new ParticleDefinition()
            {
                Color = Color.White,
                Lifetime = 5000,
                Size = 1,
                MinSizeVariance = 0,
                MaxSizeVariance = 0,
                MinSize = 0.001f,
                SizeDelta = 1,
                Alpha = 1,
                AlphaDelta = 1, 
                Rotation = 1,
                MinAngleVariance = 0,
                MaxAngleVariance = 0,
                RotationDelta = 0,
                TextureRotation = 0,
                TextureRotationDelta = 0,
                TextureOrigin = Vector2.Zero,
                MinSpeed = .0001f,
                Mass = 1,
                Force = 1,
                Dampening = .95f,
                MinDampeningVariance = 1,
                MaxDampeningVariance = 100,
            };
    }
}
