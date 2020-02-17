using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Core.Scene
{
    public class SceneManagement
    {
        public static SceneManagement Instance;
        public Scene CurrentScene { get; set; }

        public SceneManagement()
        {
            Instance = this; 
        }

        public void LoadScene(Scene scene)
        {
            CurrentScene = scene; 
        }
    }
}
