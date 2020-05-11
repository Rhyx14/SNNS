using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    class NeuronBase
    {
        List<SynapseBase> Synapses { get; set; }
        List<int> Axon { get; set; }
        int ID { get; set; }
        int GroupID { get; set; }
        bool IsFiring { get; set; }
        public virtual void Update() { }

        /// <summary>
        /// 
        /// </summary>
        public void Receive()
        {
            // TODO TPL
        }
    }
}
