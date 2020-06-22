using SNNS_Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Models
{
    /// <summary>
    /// 监测收到的脉冲个数
    /// </summary>
    class SpikeMonitor:NeuronBase
    {
        public int RecLength;
        public byte[] Records;
        int Index;

        public override void NeuronStateUpdate()
        {
            foreach (var syn in this.Afferent)
            {
                //该突触的接收到脉冲的标志
                var f = syn.GetReceiptFlag();
                if (f.Value)
                {
                    Records[Index] += 1;
                    f.Value = false;
                }
            }
            Index += 1;
            Index %= RecLength;
        }
    }
}
