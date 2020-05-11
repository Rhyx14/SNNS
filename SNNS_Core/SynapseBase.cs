using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    class SynapseBase
    {
        int Pre_SynapseID { get; set; }
        bool Received { get; set; }
        public virtual void Update() { }
    }
}
