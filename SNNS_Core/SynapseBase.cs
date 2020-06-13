using System;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Core
{
    public class SynapseBase
    {
        /// <summary>
        /// 前射神经元的全局ID
        /// </summary>
        public int Pre_SynapseID { get; set; } = -1;
        public int Post_SynapseID { get; set; } = -1;
        /// <summary>
        /// 全局ID
        /// </summary>
        public int ID { get; set; }

        ReceiptFlag RA = new ReceiptFlag();
        ReceiptFlag RB = new ReceiptFlag();

        /// <summary>
        /// 是否接收到脉冲,由神经元调用
        /// </summary>
        public ReceiptFlag GetReceiptFlag()
        {
            if (Core.IsRoundB)
            {
                return RA;
            }
            else
            {
                return RB;
            }
        }

        public virtual void Update() { }
        /// <summary>
        /// 接受到脉冲时候进行的动作
        /// </summary>
        public virtual void OnReceived() { }
        /// <summary>
        /// Debug阶段操作
        /// </summary>
        public virtual void Debug() { }

        /// <summary>
        /// 设置Receive flag
        /// 此函数仅供Core调用
        /// 对于Core来说，A轮将脉冲信息写入Received_A,B轮写B
        /// 对于神经元说，Received_B的信息在A轮才是上一次脉冲结果
        /// </summary>
        public void SetReceive()
        {
            if (Core.IsRoundB)
            {
                RB.Value = true;
            }
            else
            {
                RA.Value = true;
            }
        }
    
    }

    public class ReceiptFlag
    {
        public bool Value { get; set; } = false;
    }
}
