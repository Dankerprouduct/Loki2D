using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Shaders
{
    public class SpotLight : Light
    {
        private float _spotRotation;

        /// <summary>
        /// Gets or sets the spot angle. Specified how large the cone is.
        /// </summary>
        /// <value>The spot angle.</value>
        public float SpotAngle { get; set; }

        /// <summary>
        /// Gets or sets the spot decay exponent. Measures how the light intensity
        /// decreases from the center of the cone, towards the walls.
        /// </summary>
        /// <value>The spot decay exponent.</value>
        public float SpotDecayExponent { get; set; }

        public float SpotRotation
        {
            get { return _spotRotation; }
            set
            {
                _spotRotation = value;
                Direction = new Vector3(
                    (float)Math.Cos(_spotRotation),
                    (float)Math.Sin(_spotRotation),
                    Direction.Z);
            }
        }

        public SpotLight()
            : base(LightType.Spot)
        {
            SpotAngle = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;

        }


        public override Light DeepCopy()
        {
            throw new NotImplementedException();
        }
    }
}
