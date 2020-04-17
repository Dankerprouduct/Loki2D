using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokiEditor.Actions;

namespace LokiEditor.Systems
{
    public class CommandManager
    {
        public static CommandManager Instance;

        public List<Command> Commands;
        public CommandManager()
        {
            Instance = this;
            Commands = new List<Command>();
        }


        public void Execute(Command command)
        {
            Commands.Add(command);
        }

        public void Undo()
        {

        }
        
        public void Redo()
        {

        }


    }
}
