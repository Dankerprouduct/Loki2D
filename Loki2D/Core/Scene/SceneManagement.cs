using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Loki2D.Core.Scene
{
    public class SceneManagement
    {
        public static SceneManagement Instance;
        public GraphicsDevice GraphicsDevice;
        public ContentManager Content;
        public Scene CurrentScene { get; private set; }
        public bool DrawDebug { get; set; } = true;


        private SceneManagement()
        {
            Instance = this; 
        }

        public SceneManagement(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Instance = this;

            GraphicsDevice = graphicsDevice;
            Content = content;
        }

        public SceneManagement(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Loads a scene and sets the current scene
        /// Also initializes the scene
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(Scene scene)
        {
            CurrentScene = scene; 
            CurrentScene.Initialize(GraphicsDevice);
        }

        public void SaveScene(Scene scene = null, string fileName = null)
        {
            if (scene == null)
            {
                var sceneData = JsonConvert.SerializeObject(SceneManagement.Instance.CurrentScene, Formatting.Indented);

                if (fileName == null)
                {
                    fileName = CurrentScene.Name;
                }
                using (StreamWriter streamWriter = new StreamWriter(fileName + ".sloki"))
                {
                    streamWriter.Write(sceneData);
                }
            }
            else
            {
                var sceneData = JsonConvert.SerializeObject(scene, Formatting.Indented);

                if (fileName == null)
                {
                    fileName = CurrentScene.Name;
                }
                using (StreamWriter streamWriter = new StreamWriter(fileName ))
                {
                    streamWriter.Write(sceneData);
                }
            }
        }

        public void LoadScene(string path, Assembly assembly = null)
        {
            Scene scene = null;
            string json = "";
            using (StreamReader reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
                scene = JsonConvert.DeserializeObject<Scene>(json);
            }
            
            if (scene == null)
            {
                Console.WriteLine("Could not deserialize scene from path.");
                return;
            }

            var newScene = new Scene(scene.Name, scene.Size);
            LoadScene(newScene);

            var jObject = JObject.Parse(json);
            var partition = jObject["CellSpacePartition"];
            
            var cells = JArray.Parse(partition["Cells"].ToString());

            Console.WriteLine($"partition width:  {partition["Width"]} ");
            Console.WriteLine($"partition height: {partition["Height"]} ");
            
            Console.WriteLine($"partition length: {cells.Count} ");

            foreach (var cell in cells)
            {
                var x = (int) cell["X"];
                var y = (int) cell["Y"];

                foreach (var entity in cell["Entities"])
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine(entity["Name"]);

                    var entityType = Type.GetType((string)entity["EntityType"]);
                    if (assembly != null)
                    {
                        var rawEntityType = (string) entity["EntityType"];
                        entityType = assembly.GetType(rawEntityType, true);
                    }

                    var newEntity = Activator.CreateInstance(entityType) as Entity;

                    foreach (var component in entity["Components"])
                    {

                        var componentType = Type.GetType((string)component["ComponentType"]);
                        if (assembly != null)
                            componentType = assembly.GetType((string) component["ComponentType"]);
                        if(componentType == null)
                            componentType = Type.GetType((string)component["ComponentType"]);

                        var newComponent = Activator.CreateInstance(componentType) as Component.Component;

                        Console.WriteLine("adding: " + componentType.Name);
                        Console.WriteLine($"field count: {componentType.GetProperties().Length}");
                        foreach (var field in componentType.GetProperties())
                        {
                            Console.WriteLine($"setting field: {field.Name}");
                            Console.WriteLine($"Field Type: {field.PropertyType}");
                            field.SetValue(newComponent, Convert.ChangeType(component[field.Name], field.PropertyType));

                        }

                        newComponent.Initialize();
                        newEntity.AddComponent(newComponent);
                    }
                    Console.WriteLine($"Component Count: {newEntity.Components.Length}");

                    newEntity.Init();
                    newScene.AddEntity(newEntity);
                    Console.WriteLine($"added entity @ {newEntity.GetComponent<TransformComponent>().Position}");

                    Console.WriteLine("---------------------");
                }
            }
        }


        /// <summary>
        /// Updates the current Scene;
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene?.Draw(spriteBatch);
        }
    }
}
