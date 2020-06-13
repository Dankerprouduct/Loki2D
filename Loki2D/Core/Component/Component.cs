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
        /// <summary>
        ///  The type of this component 
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// The name of the component
        /// </summary>
        [JsonIgnore]
        public string Name { get; set; }

        /// <summary>
        /// Called when the component is deserialized
        /// </summary>
        public virtual void Initialize()
        {
            ComponentType = this.GetType().FullName;
            Name = this.GetType().Name;
        }

        /// <summary>
        /// Returns the name of the component
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var name = this.GetType().Name;
            var a = System.Text.RegularExpressions.Regex
                .Replace(name, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
            return a;
        }
    }
}
