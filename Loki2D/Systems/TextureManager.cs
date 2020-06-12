using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Debug = Loki2D.Core.Utilities.Debug;

namespace Loki2D.Systems
{
    public class TextureManager: SystemManager<TextureManager>
    {
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        private GraphicsDevice _graphicsDevice;
        public static Texture2D Pixel;

        public TextureManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            Pixel = CreateTexture(graphicsDevice, 1, 1, pixel => Color.White);
        }

        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);

            return texture;
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

        /// <summary>
        /// returns the center of an image
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Vector2 CenterOfImage(Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /// <summary>
        /// Loads selected folder 
        /// </summary>
        /// <param name="folderPath"></param>
        public void LoadFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); 
            }
            var filePaths = Directory.EnumerateFiles(folderPath, "*.png*", SearchOption.AllDirectories).ToArray();
            foreach (var file in filePaths)
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var texture = Texture2D.FromStream(_graphicsDevice, stream);
                    var key = Path.GetFileNameWithoutExtension(file);
                    stream.Dispose();
                    try
                    {
                        Textures.Add(key, texture);
                    }
                    catch (ArgumentException exception)
                    {
                        Debug.Log($"Texture {texture} has already been added!");
                    }
                }
            }
            
        }
    }
}
