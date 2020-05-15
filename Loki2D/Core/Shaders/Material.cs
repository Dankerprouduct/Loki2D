using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Systems;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Core.Shaders
{
    public class Material
    {
        public string Diffuse { get; set; }
        public string Normal { get; set; }


        public Material(string diffuseTexture, string normalTexture)
        {
            Diffuse = diffuseTexture;
            Normal = normalTexture;
        }

        public Texture2D GetDiffuse()
        {
            return TextureManager.Instance.GetTexture(Diffuse);
        }

        public Texture2D GetNormal()
        {
            return TextureManager.Instance.GetTexture(Normal);
        }
    }
}
