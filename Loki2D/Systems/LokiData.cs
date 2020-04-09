using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Systems
{
    public class LokiData
    {
        /// <summary>
        /// The .exe of the project
        /// </summary>
        public string Project { get; set; } 

        /// <summary>
        /// The Scenes Folder
        /// </summary>
        public string Scenes { get; set; }
        
        /// <summary>
        /// The Entity Folder
        /// </summary>
        public string Entity { get; set; }
        
        /// <summary>
        /// The Scripts Folder
        /// </summary>
        public string Scripts { get; set; }

        /// <summary>
        /// The Content Folder
        /// </summary>
        public string Content { get; set; }
    }
}
