using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using tainicom.Aether.Physics2D.Dynamics;

namespace Loki2D.Core.Component
{
    public class PhysicsComponent: Component
    {


        [JsonIgnore]
        public Body PhysicsBody;

        public float Mass
        {
            get => PhysicsBody.Mass;
            set => value = PhysicsBody.Mass;
        }

        public BodyType BodyType
        {
            get => PhysicsBody.BodyType;
            set => value = PhysicsBody.BodyType;
        }



        public PhysicsComponent(Body body)
        {
            PhysicsBody = body;
            
            SceneManagement.Instance.CurrentScene.AddBody(body);
            Initialize();
        }

    }
}
