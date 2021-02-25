using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SNNS_Core
{
    /// <summary>
    /// 神经元基类
    /// </summary>
    public class NeuronBase
    {
        /// <summary>
        /// 所有的神经元的列表
        /// </summary>
        static public List<NeuronBase> AllNeurons { get; } = new List<NeuronBase>();

        /// <summary>
        /// ctor
        /// </summary>
        public NeuronBase()
        {
            ID = AllNeurons.Count;
            AllNeurons.Add(this);
        }

        /// <summary>
        /// 传入突触的引用列表
        /// </summary>
        public List<SynapseBase> Afferent { get; set; } = new List<SynapseBase>();
        /// <summary>
        /// 传出突触的引用列表
        /// </summary>
        public List<SynapseBase> Axon { get; set; } = new List<SynapseBase>();
        /// <summary>
        /// 神经元组内的ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 全局的ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 是否发射脉冲
        /// </summary>
        public bool IsFiring { get; set; }
        /// <summary>
        /// 更新神经元状态
        /// </summary>
        public virtual void NeuronStateUpdate() { }

        /// <summary>
        /// Debug阶段操作
        /// </summary>
        public virtual void Debug() { }

    }
}
