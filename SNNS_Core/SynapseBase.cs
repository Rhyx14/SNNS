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
        /// <summary>
        /// 后摄神经元的全局ID
        /// </summary>
        public int Post_SynapseID { get; set; } = -1;
        /// <summary>
        /// 全局ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 脉冲队列
        /// </summary>
        Queue<Spike> Spikes { get; set; }
        /// <summary>
        /// 本时刻的脉冲队列
        /// </summary>
        Queue<Spike> OutSpikes { get; set; }
        /// <summary>
        /// 突触延迟
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// 所有突触集合
        /// </summary>
        static public List<SynapseBase> Synapses { get; set; } = new List<SynapseBase>();

        /// <summary>
        /// 默认CTOR
        /// 将新突触加入到全局的突触列表中
        /// </summary>
        public SynapseBase()
        {
            this.Spikes = new Queue<Spike>();
            this.OutSpikes = new Queue<Spike>();
            this.ID = Synapses.Count;
            Synapses.Add(this);
        }

        /// <summary>
        /// 当前时刻有多少Spike
        /// </summary>
        /// <returns></returns>
        public int GetSpikes()
        {
            int res = 0;
            foreach (var sp in Spikes)
            {
                if (sp.Remain == 0)
                {
                    res++;
                }
            }
            return res;
        }
        
        /// <summary>
        /// 设置Spike 由pre访问
        /// </summary>
        internal void SetSpike()
        {
            this.OutSpikes.Enqueue(new Spike { Remain = Delay-1 });
        }

        internal void UpdateSynapseStatus()
        {           
            while (Spikes.Count != 0)
            {
                if (Spikes.Peek().Remain == 0)
                {
                    Spikes.Dequeue();
                }
                else
                {
                    break;
                }
            }
            foreach (var item in Spikes)
            {
                item.Remain--;
            }

            //本时刻发射的脉冲放入脉冲队列
            while (OutSpikes.Count!=0)
            {
                Spikes.Enqueue(OutSpikes.Peek());
                OutSpikes.Dequeue();
            }
            Update();
        }
        /// <summary>
        /// 更新突触状态
        /// </summary>
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
