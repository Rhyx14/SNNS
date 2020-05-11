using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNNS_Core
{
    class Core
    {
        static int Index { get; set; } = 0;
        static List<NeuronBase> Neurons { get; set; } = new List<NeuronBase>();
        static void Run(int time)
        {
            for (int i = 0; i < time; i++)
            {
                Parallel.ForEach(Neurons, (n) =>
                {
                    n.Update();
                });
            }
        }
    }
}
