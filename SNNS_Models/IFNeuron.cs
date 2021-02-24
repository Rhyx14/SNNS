using SNNS_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Models
{
    /// <summary>
    /// IF神经元，采用WeightSynapse突触
    /// </summary>
    public class IFNeuron:NeuronBase
    {
        public int Refractory { get; set; } = 0;
        public double MembranePotential { get; set; } = 0;
        public double MinPotential { get; set; } = -100;
        public double MaxPotential { get; set; } = 100;
        public double Threshold { get; set; } = 1;
        public double Current { get; set; } = 0;
        int ActualRefractory { get; set; } = 0;
        /// <summary>
        /// 记录有多少次脉冲
        /// </summary>
        public long SpikeCounts { get; set; } = 0;

        public override void NeuronStateUpdate()
        {
            MembranePotential += Current;
            if (ActualRefractory > 0)
            {
                ActualRefractory--;
                return;
            }
            //Integrate
            MembranePotential += Integrate();

            //Fire
            if (MembranePotential >= Threshold)
            {
                this.IsFiring = true;
                MembranePotential -= Threshold;
                MembranePotential = MembranePotential >= MinPotential ? MembranePotential : MinPotential;
                ActualRefractory = Refractory;
                SpikeCounts += 1;
            }
        }

        double Integrate()
        {
            double res = 0.0;
            foreach (var syn in this.Afferent)
            {
                var s = syn as WeightSynapse;
                //该突触的接收到脉冲的标志
                var f = syn.GetSpikes();
                res += f * s.Weight;
            }
            return res;
        }
    }
}
