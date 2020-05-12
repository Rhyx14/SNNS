using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SNNS_Core
{
    public class NeuronBase
    {
        public List<int> Afferent { get; set; } = new List<int>();
        public List<int> Axon { get; set; } = new List<int>();
        public int ID { get; set; }
        public int GroupID { get; set; }
        public bool IsFiring { get; set; }
        /// <summary>
        /// 更新神经元状态
        /// </summary>
        public virtual void NeuronStateUpdate() { }

        /// <summary>
        /// 更新神经元信息
        /// </summary>
        public void Update()
        {
            foreach (var s_id in Afferent)
            {
                SynapseBase.Synapses[s_id].Update();
            }
            NeuronStateUpdate();
        }
    }
}
