using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using tainicom.Aether.Physics2D.Dynamics;

namespace Loki2D.Core.Component
{
    public class PhysicsComponent: Component
    {
        public Body PhysicsBody; 

        public PhysicsComponent(Body body,Entity entity) : base(entity)
        {
            PhysicsBody = body; 
            SceneManagement.Instance.CurrentScene.AddBody(body);
        }

    }
}
