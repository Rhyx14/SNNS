using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    public class SynapseBase
    {
        /// <summary>
        /// 前射神经元的全局ID
        /// </summary>
        public int Pre_SynapseID { get; set; } = -1;
        public int Post_SynapseID { get; set; } = -1;
        /// <summary>
        /// 全局ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 是否接收到脉冲
        /// </summary>
        public bool Received { get; set; }
        public virtual void Update() { }
        /// <summary>
        /// 第一次接受到脉冲的动作
        /// </summary>
        public virtual void OnReceived() { }
    }
}
