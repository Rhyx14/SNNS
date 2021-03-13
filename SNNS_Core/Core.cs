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
        /// 时间戳
        /// </summary>
        static public int Time { get; set; }

        /// <summary>
        /// 运行网络
        /// </summary>
        /// <param name="time">运行时间（ticks）</param>
        public static void Run_TPL(int time)
        {
            Time = 1;
            for (int i = 0; i < time; i++)
            {
                Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
                {
                    //更新神经元
                    //神经元不能在同时刻检查突触脉冲序列和向突触发射脉冲
                    n.NeuronStateUpdate();
                });
                Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
                {
                    //路由脉冲信息,更新突触
                    foreach (var syn in n.Axon)
                    {
                        if (n.IsFiring)
                        {
                            syn.SetSpike();
                            syn.OnReceived();
                            n.IsFiring = false;
                        }
                        syn.UpdateSynapseStatus();      
                    }
                });
                Time++;
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
