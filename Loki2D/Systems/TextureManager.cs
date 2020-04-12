using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Systems
{
    public class TextureManager: SystemManager<TextureManager>
    {
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        private GraphicsDevice _graphicsDevice;
        public TextureManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public Texture2D GetTexture(string key)
        {
            return Textures[key];
        }

        public void LoadTexture(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var key = Path.GetFileNameWithoutExtension(filePath);
                var texture = Texture2D.FromStream(_graphicsDevice,stream);

                Textures.Add(key, texture);
            }
        }

        public void LoadFolder(string folderPath)
        {

            var filePaths = Directory.EnumerateFiles(folderPath, "*.png*", SearchOption.AllDirectories).ToArray();
            foreach (var file in filePaths)
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var texture = Texture2D.FromStream(_graphicsDevice, stream);
                    var key = Path.GetFileNameWithoutExtension(file);
                    Textures.Add(key, texture);

                    stream.Dispose();
                }
            }
            
        }
    }
}
