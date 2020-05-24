﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SNNS_Core
{
    public class NeuronBase
    {
        static public List<NeuronBase> AllNeurons { get; } = new List<NeuronBase>();

        public NeuronBase()
        {
            ID = AllNeurons.Count;
            AllNeurons.Add(this);
        }


        public List<SynapseBase> Afferent { get; set; } = new List<SynapseBase>();
        public List<SynapseBase> Axon { get; set; } = new List<SynapseBase>();
        public int ID { get; set; }
        public int GroupID { get; set; }
        public bool IsFiring { get; set; }
        /// <summary>
        /// 更新神经元状态
        /// </summary>
        public virtual void NeuronStateUpdate() { }

        /// <summary>
        /// 更新神经元信息
        /// </summary>
        public void Update()
        {
            // Debug下Foreach比for快一点？
            // Release下相反
#if DEBUG
            foreach (var syn in Afferent)
            {
                syn.Update();
            }
#else
            for (int i = 0; i < Afferent.Count; i++)
            {
                Afferent[i].Update();
            }
#endif
            NeuronStateUpdate();
        }

    }
}
