using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    /// <summary>
    /// 神经元组
    /// 方便建立网络使用，本身不存储神经元和突触的实例
    /// </summary>
    public class NeuronGroup
    {
        public string Name { get; set; }
        /// <summary>
        /// 该group中的神经元在总神经元列表中的位置
        /// </summary>
        int Offset { get; set; }
        /// <summary>
        /// 该group神经元个数
        /// </summary>
        int Count { get; set; }

        public NeuronGroup(NeuronBase[] neurons, string name="undefined")
        {
            var count = neurons.Length;
            this.Offset = Core.GetIndex(count);
            this.Count = count;
            this.Name = name;

            var neuronsArray = Core.GetNeurons();
            for (int i = 0; i < count; i++)
            {
                var n = neurons[i];
                n.ID = Offset + i;
                n.GroupID = i;
                neuronsArray.Add(n);
            }
        }

        /// <summary>
        /// 返回该group的第index号神经元
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public NeuronBase this[int index]
        {
            get
            {
                var neurons = Core.GetNeurons();
                return neurons[index + Offset];
            }
        }

        /// <summary>
        /// 连接两个神经元
        /// </summary>
        /// <param name="g1">前射的神经元组</param>
        /// <param name="g2">后射的神经元组</param>
        /// <param name="i">前射神经元的GroupID</param>
        /// <param name="j">后射神经元的GroupID</param>
        /// <param name="synapse">突触的实例</param>
        public static void Connect(NeuronGroup g1, NeuronGroup g2, int i, int j, SynapseBase synapse)
        {
            var n1 = g1[i];
            var n2 = g2[j];
            synapse.Pre_SynapseID = n1.ID;
            synapse.Post_SynapseID = n2.ID;
            //添加突触
            n2.Afferent.Add(synapse.ID);
            //添加轴突
            n1.Axon.Add(synapse.ID);
        }
    }
}
