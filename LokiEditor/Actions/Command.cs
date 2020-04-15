using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokiEditor.Actions
{
    public class Command
    {
        public virtual void Execute() { }
        public virtual void Undo() { }
        
        public virtual void Enter() { }

        public virtual bool Update()
        {
            return true;

        }
        public virtual void Exit() { }
    }
}
