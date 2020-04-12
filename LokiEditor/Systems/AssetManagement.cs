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
        }

        public AssetManagement(MainWindow window)
        {
            Instance = this;
            window.NewProjectEvent += NewProject;
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

            LokiGame.Instance.LoadScene("C:\\Users\\danke\\source\\repos\\Game4\\Game4\\bin\\Windows\\x86\\Debug\\testScene.json",
                AssetControl.Instance.LoadedAssembly);
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
