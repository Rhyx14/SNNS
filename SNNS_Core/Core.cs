using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNNS_Core
{
    class Core
    {
        static int index=0;
        static public int GetIndex(int count)
        {
            var tmp = index;
            index += count;
            return tmp;
        }

        static public List<NeuronBase> GetNeurons()
        {
            return Neurons;
        }
        static List<NeuronBase> Neurons { get; set; } = new List<NeuronBase>();

        static void Run(int time)
        {
            for (int i = 0; i < time; i++)
            {
                //更新神经元
                Parallel.ForEach(Neurons, (n) =>
                {
                    n.Update();
                });
                // 路由脉冲信息
                // 一条突触只能有一个后射-前射对应，所以不用担心数据竞争
                Parallel.ForEach(Neurons, (n) =>
                {
                    if (n.IsFiring)
                    {
                        foreach (var s_id in n.Axon)
                        {
                            SynapseBase.Synapses[s_id].Received = true;
                        }
                    }
                });
            }
        }
    }
}
