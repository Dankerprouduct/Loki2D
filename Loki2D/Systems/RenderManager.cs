using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;

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
    }
}
