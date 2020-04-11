using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;


namespace LokiEditor.Game
{
    public class Asset
    {
        public string FullType { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; } = "Content/MarauderLogo.png";

        [JsonIgnore]
        public ImageBrush Image
        {
            get
            {
                var i = new BitmapImage();
                i.BeginInit();
                i.CacheOption = BitmapCacheOption.OnLoad;
                i.UriSource = new Uri(ImagePath, UriKind.Relative);
                i.EndInit();

                var imageBrush = new ImageBrush(i);
                return imageBrush;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
