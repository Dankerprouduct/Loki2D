using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Attributes;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using tainicom.Aether.Physics2D.Dynamics;

namespace Loki2D.Core.Component
{
    [EditorInspectable("Physics Component")]
    public class PhysicsComponent: Component
    {

        /// <summary>
        /// The Physics body
        /// </summary>
        [JsonIgnore]
        public Body PhysicsBody;

        private float _mass;

        /// <summary>
        /// The mass associated with the physics body
        /// </summary>
        [EditorInspectable]
        public float Mass
        {
            get => PhysicsBody.Mass;
            set => value = _mass;
        }

        private BodyType _bodyType;

        /// <summary>
        /// The body type associated with the physics body
        /// </summary>
        [EditorInspectable]
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
            //Initialize();
        }

        /// <summary>
        /// Component used for entity physics
        /// </summary>
        /// <param name="body"></param>
        public PhysicsComponent(Body body)
        {
            PhysicsBody = body;
            
            Initialize();
        }

        /// <summary>
        /// Called when component is deserialized
        /// </summary>
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
