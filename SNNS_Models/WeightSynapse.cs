using System;
using System.Collections.Generic;
using System.Text;
using SNNS_Core;

namespace SNNS_Models
{
    /// <summary>
    /// 带有权重的简单突触
    /// </summary>
    public class WeightSynapse:SynapseBase
    {
        public double Weight { get; set; } = 0;
    }
}
