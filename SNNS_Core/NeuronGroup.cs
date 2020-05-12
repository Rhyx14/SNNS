using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    class NeuronGroup
    {
        public string Name { get; set; }
        int Offset { get; set; }
        int Count { get; set; }

        int Added { get; set; } = 0;
        public NeuronGroup(int count,string name="undefined")
        {
            this.Offset = Core.GetIndex(count);
            this.Count = count;
            this.Name = name;
        }

        /// <summary>
        /// 添加神经元
        /// </summary>
        /// <param name="n"></param>
        public void Add(NeuronBase n)
        {
            if (Added == Count)
            {
                throw new Exception("This group has been already filled to the full.");
            }
            var neurons = Core.GetNeurons();
            n.ID = Offset + Added;
            n.GroupID = Added;
            Added++;
            neurons.Add(n);
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
            n2.Synapses.Add(synapse.ID);
            //添加轴突
            n1.Axon.Add(synapse.ID);
        }
    }
}
