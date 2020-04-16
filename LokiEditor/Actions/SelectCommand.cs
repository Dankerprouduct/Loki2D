using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Scene;
using Loki2D.Core.Utilities;
using Loki2D.Systems;
using LokiEditor.LokiControls;
using Microsoft.Xna.Framework;

namespace LokiEditor.Actions
{
    public class SelectCommand: Command
    {
        public override void Enter()
        {
            var position = Vector2.Transform(InputManager.MouseState.Position.ToVector2(),
                Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));
            var entity = SceneManagement.Instance.CurrentScene.GetEntity(position);

            if (entity != null)
            {
                PropertyControl.Instance.SetInspector(entity);
            }
        }
    }
}
