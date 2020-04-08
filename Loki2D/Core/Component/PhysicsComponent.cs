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

        private float _mass;
        public float Mass
        {
            get => PhysicsBody.Mass;
            set => value = _mass;
        }

        private BodyType _bodyType;
        public BodyType BodyType
        {
            get => PhysicsBody.BodyType;
            set
            {
                value = _bodyType;
                if (PhysicsBody != null)
                    PhysicsBody.BodyType = _bodyType;
            }
        }


        public PhysicsComponent()
        {

        }

        public PhysicsComponent(Body body)
        {
            PhysicsBody = body;
            
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            if (PhysicsBody == null)
            {
                PhysicsBody = new Body();
                PhysicsBody.Mass = _mass;
                PhysicsBody.BodyType = _bodyType;
            }
            SceneManagement.Instance.CurrentScene.AddBody(PhysicsBody);
        }
    }
}
