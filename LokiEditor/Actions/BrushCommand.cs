using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Loki2D.Systems;
using LokiEditor.Game;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LokiEditor.Actions
{
    public class BrushCommand : Command
    {
        private Entity _selectedEntity;
        private bool _selected;
        private static bool _orthographic;

        public override void Enter()
        {
            // spawn entity
            var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));

            if (AssetControl.Instance.SelectedAsset != null)
            {
                var assembly = AssetControl.Instance.LoadedAssembly;
                var type = assembly.GetType(AssetControl.Instance.SelectedAsset.FullType, true);
                var entity = Activator.CreateInstance(type) as Entity;

                entity.GetComponent<TransformComponent>().Position = position;
                _selectedEntity = entity;
                _selected = true;
                SceneManagement.Instance.CurrentScene.AddEntity(entity);
                var addedEntityArgs = new AddedEntityArgs()
                {
                    AddedEntity = entity
                };
                LokiGame.AddedEntityHandler?.Invoke(this, addedEntityArgs);
            }

            base.Enter();
        }

        public override bool Update()
        {
            // transform
            if (InputManager.LeftMouseDown() && _selected)
            {
                var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                    Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));

                var textureName = _selectedEntity.GetComponent<RenderComponent>().TextureName;
                var width = TextureManager.Instance.GetTexture(textureName).Width;
                var height = TextureManager.Instance.GetTexture(textureName).Height;

                if (InputManager.KeyDown(Keys.LeftShift))
                {
                    if (_orthographic)
                    {
                        position.X = ((float)Math.Round(position.X / width) * width);
                        position.Y = ((float)Math.Round(position.Y / height) * height);
                    }
                    else
                    {
                        var mapX = ((float)Math.Round(position.X / width));
                        var mapY = ((float)Math.Round(position.Y / height));
                        position.X = (mapX - mapY) * (width / 2);
                        position.Y = (mapX + mapY * (height / 2));
                    }
                }

                _selectedEntity.GetComponent<TransformComponent>().Position = position;
            }

            if (InputManager.LeftMouseUp() && _selected)
            {
                _selected = false;
                PropertyControl.Instance.SetInspector(_selectedEntity);
                return false;
            }

            if (_selectedEntity == null)
                return false;

            return true;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
