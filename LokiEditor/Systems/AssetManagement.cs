using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities;
using Loki2D.Systems;
using LokiEditor.Game;
using LokiEditor.LokiControls;
using Newtonsoft.Json;

namespace LokiEditor.Systems
{
    public class AssetManagement
    {
        public static AssetManagement Instance;
        public static LokiData LokiData { get; set; }
        public string LokiFilePath { get; set; }


        public AssetManagement()
        {
            Instance = this;
            MainWindow.NewProjectEvent += NewProject;
        }

        public AssetManagement(string lokiPath)
        {
            Instance = this;
            LokiFilePath = lokiPath;

        }



        private void NewProject(object sender, LoadLokiProjectEvent e)
        {
            LokiData data;
            LokiFilePath = e.FilePath;
            using (StreamReader reader = new StreamReader(e.FilePath))
            {
                var json = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<LokiData>(json);
                LokiData = data;
            }
            
            AssetControl.Instance.LoadAssets(LokiData);

        }

    }

    public class LoadLokiProjectEvent : EventArgs
    {
        /// <summary>
        /// the path of the .loki file
        /// </summary>
        public string FilePath { get; set; }
    }
}
