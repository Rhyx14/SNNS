using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNNS_Core
{
    public class Core
    {
        /// <summary>
        /// A轮还是B轮标志
        /// </summary>
        static public bool IsRoundB { get; set; } = false;

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
                //更新神经元
                Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
                {
                    n.Update();
                    //路由脉冲信息
                    //一条突触只能有一对后射-前射对应，所以不用担心数据竞争
                    if (n.IsFiring)
                    {
                        foreach (var syn in n.Axon)
                        {
                            syn.SetReceive();
                            syn.OnReceived();
                        }
                        n.IsFiring = false;
                    }
                });
                IsRoundB = !IsRoundB;
            }
        }
        //static void A_Run_TPL(int time)
        //{
        //    //更新神经元
        //    Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
        //        n.Update()
        //    );
        //    //路由脉冲信息
        //    //一条突触只能有一对后射-前射对应，所以不用担心数据竞争
        //    Parallel.ForEach(NeuronBase.AllNeurons, (n) =>
        //    {
        //        if (n.IsFiring)
        //        {
        //            foreach (var syn in n.Axon)
        //            {
        //                syn.Received = true;
        //                syn.OnReceived();
        //            }
        //            n.IsFiring = false;
        //        }
        //    });
        //    IsRoundB = !IsRoundB;
        //}

    }
}
