using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Loki2D.Core.Utilities;
using Loki2D.Systems;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LokiEditor.Actions
{
    public class TransformCommand: Command
    {
        private Entity _selectedEntity;
        private bool _selected;

        public override void Enter()
        {
            var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));

            var entity = SceneManagement.Instance.CurrentScene.GetEntity(position);


            if (entity != null)
            {
                PropertyControl.Instance.SetInspector(entity);
                Debug.Log($"Selected: {entity.Name} - {DateTime.Now}");
                _selected = true;
                _selectedEntity = entity;
            }
            else
            {
                _selected = false; 
            }
            
        }

        public override bool Update()
        {
            if (!_selected) return false;

            if (InputManager.LeftMouseDown() && _selected)
            {
                var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                    Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));

                var textureName = _selectedEntity.GetComponent<RenderComponent>().TextureName;
                var width = TextureManager.Instance.GetTexture(textureName).Width;
                var height = TextureManager.Instance.GetTexture(textureName).Height;

                if (InputManager.KeyDown(Keys.LeftShift))
                {
                    position.X = ((float)Math.Round(position.X / width) * width);
                    position.Y = ((float)Math.Round(position.Y / height) * height);
                }
                
                _selectedEntity.GetComponent<TransformComponent>().Position = position;
            }

            if (InputManager.LeftMouseUp() && _selected)
            {
                _selected = false;
                PropertyControl.Instance.SetInspector(_selectedEntity);
                return false;
            }

            return true;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
