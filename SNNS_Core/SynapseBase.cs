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

        bool Received_A { get; set; } = false;
        bool Received_B { get; set; } = false;
        public virtual void Update() { }
        /// <summary>
        /// 第一次接受到脉冲的动作
        /// </summary>
        public virtual void OnReceived() { }


        /// <summary>
        /// 是否接收到脉冲,由神经元调用
        /// </summary>
        public bool HasReceived()
        {
            if (Core.IsRoundB)
            {
                return Received_A;
            }
            return Received_B;
        }
        public void Receive()
        {
            if (Core.IsRoundB)
            {
                Received_A=true;
            }
            else
            {
                Received_B = true;
            }
        }      
        /// <summary>
        /// 该突触将收到的脉冲传递出去（收到脉冲flag设为false）
        /// </summary>
        public void Pass()
        {
            if (Core.IsRoundB)
            {
                Received_A = false;
            }
            else
            {
                Received_B = false;
            }
        }

        /// <summary>
        /// 设置Receive flag
        /// 此函数仅供Core调用
        /// 对于Core来说，A轮写入Received_A,B轮写B
        /// 对于神经元说，Received_B在A轮才是上一次脉冲结果
        /// </summary>
        public void SetReceive()
        {
            if (Core.IsRoundB)
            {
                Received_B = true;
            }
            else
            {
                Received_A = true;
            }
        }
    }
}
