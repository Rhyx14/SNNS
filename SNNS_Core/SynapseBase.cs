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

        Queue<Spike> Spikes { get; set; }
        public int Delay { get; set; }

        public SynapseBase()
        {
            this.Spikes = new Queue<Spike>();
        }

        /// <summary>
        /// 当前时刻有多少Spike
        /// </summary>
        /// <returns></returns>
        public int GetSpikes()
        {
            int res = 0;
            if (Spikes.Count == 0) return res;
            while (true)
            {
                var t = Spikes.Peek();
                if (t.Count == 1)
                {
                    res++;
                    Spikes.Dequeue();
                }
                else
                {
                    break;
                }
            }
            foreach (var item in Spikes)
            {
                item.Count--;
            }
            return res;
        }
        
        /// <summary>
        /// 设置Spike
        /// </summary>
        internal void SetSpike()
        {
            this.Spikes.Enqueue(new Spike { Count = Delay });
        }

        public virtual void Update() { }
        /// <summary>
        /// 接受到脉冲时候进行的动作
        /// </summary>
        public virtual void OnReceived() { }
        /// <summary>
        /// Debug阶段操作
        /// </summary>
        public virtual void Debug() { }
    
    }
}
