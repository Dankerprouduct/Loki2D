using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Loki2D.Core.Component
{
    public class Component
    {
        public Type ComponentType { get; set; }

        public virtual void Initialize()
        {
            ComponentType = this.GetType();
        }
    }
}
