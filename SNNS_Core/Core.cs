using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNNS_Core
{
    public class Core
    {
        /// <summary>
        /// 压缩部分内存空间
        /// </summary>
        static public void Trim()
        {
            NeuronBase.AllNeurons.TrimExcess();
        }
        /// <summary>
        /// 运行网络
        /// </summary>
        /// <param name="time">运行时间（ticks）</param>
        public static void Run_TPL(int time)
        {
            for (int i = 0; i < time; i++)
            {
                Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
                {
                    //更新神经元
                    n.Update();
                    //路由脉冲信息
                    //一条突触只能有一对后射-前射对应，所以不用担心数据竞争
                    if (n.IsFiring)
                    {
                        foreach (var syn in n.Axon)
                        {
                            syn.SetSpike();
                            syn.OnReceived();
                        }
                        n.IsFiring = false;
                    }
                });
                Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
                {
                    foreach (var syn in n.Afferent)
                    {
                        syn.Update();
                    }
                });
            }
        }
        /// <summary>
        /// 连接两个神经元
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="s1">突触实例</param>
        public static void Connect(NeuronBase src,NeuronBase dst,SynapseBase s1)
        {
            s1.Pre_SynapseID = src.ID;
            s1.Post_SynapseID = dst.ID;
            //添加突触
            dst.Afferent.Add(s1);
            //添加轴突
            src.Axon.Add(s1);
        }
    }
}
