using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMusicXML
{
    public class Data
    {
        string _nodeName;
        string _nodeValue;

        // Mon constructeur
        public Data()
            : this( string.Empty, string.Empty )
        {
        }

        public Data( string nodeName, string nodeValue )
        {
            if (nodeName == null) throw new ArgumentNullException("NodeName cannot be null!");
            if (nodeValue == null) throw new ArgumentNullException("NodeValue cannot be null!");

        }

        /// <summary>
        /// Gets or sets the node name.
        /// </summary>
        public virtual string NodeName
        {
            get { return _nodeName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("NodeName must be not null.");
                }
                _nodeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the node value.
        /// </summary>
        public virtual string NodeValue
        {
            get { return _nodeValue; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("NodeValue must be not null.");
                }
                _nodeValue = value;
            }
        }
    }
}
