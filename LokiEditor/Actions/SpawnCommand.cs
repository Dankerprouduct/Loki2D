using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;

namespace LokiEditor.Actions
{
    public class SpawnCommand : Command
    {
        private Vector2 _position;
        private Entity _entity;

        public SpawnCommand(Vector2 position)
        {
            _position = position;
        }

        public override void Execute()
        {
            base.Execute();

            var assembly = AssetControl.Instance.LoadedAssembly;
            var fullType = AssetControl.Instance.SelectedAsset.FullType;
            var type = assembly.GetType(fullType, true);
            _entity = Activator.CreateInstance(type) as Entity;

            _entity.GetComponent<TransformComponent>().Position = _position;
            SceneManagement.Instance.CurrentScene.AddEntity(_entity);
        }

        public override void Undo()
        {
            base.Undo();
        }
    }
}
