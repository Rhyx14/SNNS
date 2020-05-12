using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    public class SynapseBase
    {
        static int Count { get; set; } = 0;

        /// <summary>
        /// 突触列表
        /// </summary>
        static public List<SynapseBase> Synapses;

        public SynapseBase()
        {
            this.ID = Count;
            Count++;
            SynapseBase.Synapses.Add(this);
        }
        /// <summary>
        /// 前射神经元的ID
        /// </summary>
        public int Pre_SynapseID { get; set; } = -1;
        public int Post_SynapseID { get; set; } = -1;
        /// <summary>
        /// 全局ID
        /// </summary>
        public int ID { get; set; }

        public bool Received { get; set; }
        public virtual void Update() { }
    }
}
