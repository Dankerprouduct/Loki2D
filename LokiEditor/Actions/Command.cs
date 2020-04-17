using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokiEditor.Actions
{
    public class Command
    {
        
        public virtual void Enter() { }

        public virtual bool Update()
        {
            return false;
        }

        public virtual void Exit() { }


        public virtual void Undo() { }
    }
}
