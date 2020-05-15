using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Shaders
{
    public class DeferredLightEffect: Effect
    {
        private EffectPass _clearGBufferPass;
        private EffectPass _pointLightPass;
        private EffectPass _finalCombinePass;

        // matrices
        private EffectParameter _clearColorParam;
        private EffectParameter _worldToViewParam;
        private EffectParameter _projectionParam;
        private EffectParameter _screenToWorldParam;

        private EffectParameter _normalMapParam;
        private EffectParameter _lightPositionParam;
        private EffectParameter _colorParam;
        private EffectParameter _lightRadiusParam;
        private EffectParameter _lightIntensityParam;


        protected DeferredLightEffect(Effect cloneSource) : base(cloneSource)
        {
        }

        public DeferredLightEffect(GraphicsDevice graphicsDevice, byte[] effectCode) : base(graphicsDevice, effectCode)
        {
        }

        public DeferredLightEffect(GraphicsDevice graphicsDevice, byte[] effectCode, int index, int count) : base(graphicsDevice, effectCode, index, count)
        {
        }
    }
}
