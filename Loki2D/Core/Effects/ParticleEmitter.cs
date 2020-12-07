﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.X3DAudio;

namespace Loki2D.Core.Effects
{
    public class ParticleEmitter
    {

        /// <summary>
        /// The speed of this emitter in miliseconds
        /// </summary>
        public float EmissionSpeed { get; set; }
        
        /// <summary>
        /// The amount of particles to emit 
        /// </summary>
        public int BurstAmount { get; set; }

        public ParticleEmitter()
        {
            
        }

        public void Emit()
        {

        }
    }
}
