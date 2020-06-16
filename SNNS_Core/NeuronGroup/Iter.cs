using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNNS_Core
{
    /// <summary>
    /// 神经元组
    /// 方便建立网络使用，本身不存储神经元和突触的实例
    /// iter.cs 索引与访问的方法
    /// </summary>
    public partial class NeuronGroup<T> where T : NeuronBase
    {

        #region 索引器
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
        /// 二维索引
        /// </summary>
        /// <param name="h">纵坐标</param>
        /// <param name="w">横坐标</param>
        /// <returns></returns>
        public T this[int h, int w]
        {
            get
            {
                return Neurons[h * Width + w];
            }
        }
        #endregion

        #region 循环访问
        /// <summary>
        /// 并行访问
        /// </summary>
        /// <param name="action">执行动作</param>
        public void ParallelForeach(InitAction action)
        {
            Parallel.For(0, this.Count, (i) => action(this.Neurons[i]));
        }
        /// <summary>
        /// 循环
        /// </summary>
        /// <param name="action">执行动作</param>
        public void Foreach(InitAction action)
        {
            for (int i = 0; i < this.Count; i++)
            {
                action(this.Neurons[i]);
            }
        }
        #endregion

    }

}
