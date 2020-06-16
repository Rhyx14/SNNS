using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    /// <summary>
    /// 神经元组
    /// 方便建立网络使用，本身不存储神经元和突触的实例
    /// </summary>
    public partial class NeuronGroup<T> where T:NeuronBase
    {
        public string Name { get; set; }

        public int Count { get; }
        /// <summary>
        /// 宽度，用于设置二维情况下访问
        /// </summary>
        public int Width { get; set; } = 1;

        /// <summary>
        /// 神经元数组
        /// 注意该数组中的实例会同时被NeuronsBase中的list保存
        /// </summary>
        T[] Neurons { get; set; }

        #region CTOR
        /// <summary>
        /// 创建神经元组
        /// </summary>
        /// <param name="neurons">神经元实例列表</param>
        /// <param name="name">神经元组名称</param>
        public NeuronGroup(T[] neurons, string name = "undefined")
        {
            var count = neurons.Length;
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
        /// <param name="count"></param>
        /// <param name="name">神经元组名称</param>
        public NeuronGroup(int count,int width=1, string name = "undefined")
        {
            this.Count = count;
            this.Name = name;
            this.Neurons = new T[count];
            this.Width = width;

            for (int i = 0; i < count; i++)
            {
                var n = Activator.CreateInstance<T>();
                n.GroupID = i;//全局ID在构造函数时候就会初始化，不必要在此赋值
                this.Neurons[i] = n;
            }
        }


        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="neurons"></param>
        public delegate void InitAction(T neurons);
        /// <summary>
        /// 创建神经元组
        /// </summary>
        /// <param name="count">神经元个数</param>
        /// <param name="init">初始化操作</param>
        /// <param name="width"></param>
        /// <param name="name">神经元名称</param>
        public NeuronGroup(int count, InitAction init, int width = 1, string name = "undefined")
        {
            this.Count = count;
            this.Name = name;
            this.Neurons = new T[count];
            this.Width = width;

            //构造时候不要用TPL加速，不然会有错误
            for (int i = 0; i < count; i++)
            {
                var n = Activator.CreateInstance<T>();
                n.GroupID = i;//全局ID在构造函数时候就会初始化，不必要在此赋值
                this.Neurons[i] = n;
                init(n);
            }
        }
        #endregion

        /// <summary>
        /// 连接两个神经元（弃用）
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
