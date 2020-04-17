using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.Scene;
using Loki2D.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Systems
{
    public class RenderManager: SystemManager<RenderManager>
    {
        public List<RenderManager> Components = new List<RenderManager>();
        public RenderManager()
        {

        }
        
        public void RegisterComponent(RenderComponent renderComponent)
        {

        }

        public void UnRegisterComponent(RenderComponent renderComponent)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);


            for (int y = 0; y < SceneManagement.Instance.CurrentScene.CellSpacePartition.Width; y++)
            {
                for (int x = 0; x < SceneManagement.Instance.CurrentScene.CellSpacePartition.Height; x++)
                {
                    var entities = SceneManagement.Instance.CurrentScene.CellSpacePartition.RetrieveCell(x, y).Entities;
                    if(entities== null)
                        continue;
                    
                    foreach (var entity in entities)
                    {
                        var texture =
                            TextureManager.Instance.GetTexture(entity.GetComponent<RenderComponent>().TextureName);
                        var position = entity.GetComponent<TransformComponent>().Position; 
                        
                        spriteBatch.Draw(texture, position,
                            null, Color.White, MathHelper.ToRadians(entity.GetComponent<RenderComponent>().Rotation), 
                            new Vector2(texture.Width /2, texture.Height /2), 1, 
                            SpriteEffects.None, entity.GetComponent<RenderComponent>().RenderLayer);

                    }
                }
            }

            if (SceneManagement.Instance.DrawDebug)
            {
                Debug.DrawDebug(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
