using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Core.Shaders
{
    public class PointLight : Light
    {
        public PointLight()
            : base(LightType.Point)
        {

        }

        public override Light DeepCopy()
        {
            var newLight = new PointLight();
            CopyBaseFields(newLight);

            return newLight;
        }
    }
}
