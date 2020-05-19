using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    /// <summary>
    /// 神经元组
    /// 方便建立网络使用，本身不存储神经元和突触的实例
    /// </summary>
    public class NeuronGroup<T> where T:NeuronBase
    {
        public string Name { get; set; }
        /// <summary>
        /// 该group中的神经元在总神经元列表中的起始位置
        /// </summary>
        int Offset { get; set; }
        public int Count { get; }

        /// <summary>
        /// 神经元数组
        /// 注意该数组中的实例会同时被Core中的list保存
        /// </summary>
        T[] Neurons { get; set; }

        /// <summary>
        /// 创建神经元组
        /// </summary>
        /// <param name="neurons">神经元实例列表</param>
        /// <param name="name">神经元组名称</param>
        public NeuronGroup(T[] neurons, string name = "undefined")
        {
            var count = neurons.Length;
            this.Offset = Core.GetIndex(count);
            this.Count = count;
            this.Name = name;
            this.Neurons = neurons;

            for (int i = 0; i < count; i++)
            {
                neurons[i].GroupID = i;
            }
        }

        /// <summary>
        /// 创建神经元组
        /// </summary>
        /// <param name="length">神经元个数</param>
        /// <param name="neuronType">神经元类型</param>
        /// <param name="name">神经元名称</param>
        public NeuronGroup(int count, string name = "undefined")
        {
            this.Offset = Core.GetIndex(count);
            this.Count = count;
            this.Name = name;
            this.Neurons = new T[count];

            for (int i = 0; i < count; i++)
            {
                var n = Activator.CreateInstance<T>();
                n.GroupID = i;
                this.Neurons[i] = n;
            }
        }

        /// <summary>
        /// 返回该group的第index号神经元
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return Neurons[index];
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
        public static void Connect(NeuronGroup<T> g1, NeuronGroup<T> g2, int i, int j, SynapseBase synapse)
        {
            var n1 = g1[i];
            var n2 = g2[j];
            synapse.Pre_SynapseID = n1.ID;
            synapse.Post_SynapseID = n2.ID;
            //添加突触
            n2.Afferent.Add(synapse);
            //添加轴突
            n1.Axon.Add(synapse);
        }
    }

}
