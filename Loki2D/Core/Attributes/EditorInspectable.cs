using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorInspectable: Attribute
    {
        public string DisplayName { get; set; }

        public EditorInspectable()
        {

        }

        public EditorInspectable(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
