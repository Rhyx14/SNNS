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
        public double MinPotential { get; set; } = 0;
        public double MaxPotential { get; set; } = 10;
        public double Threshold { get; set; } = 1;
        public double Current { get; set; } = 0;
        double ActualRefractory { get; set; } = 0;

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
                ActualRefractory = Refractory;
                SpikeCounts += 1;
            }
        }

        double Integrate()
        {
            double res = 0.0;
            foreach (var s_id in this.Afferent)
            {
                var s = SynapseBase.Synapses[s_id] as WeightSynapse;
                if (s.Received)
                {
                    res += s.Weight;
                    s.Received = false;
                }
            }
            return res;
        }
    }
}
