using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNNS_Core
{
    class WorkerGroup
    {
        Barrier WorkBarrier { get; set; }
        int ThreadingCount { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="count">线程数</param>
        public WorkerGroup(int count=12)
        {
            ThreadingCount = count;
            WorkBarrier = new Barrier(ThreadingCount);
        }

        public delegate void Works(int start,int end, Barrier b);

        public void Run(int start,int length,Works w)
        {
            //保存线程
            List<Task>tasks = new List<Task>();
            //计算每个线程分配数量
            //区间左开右闭
            var dl = (int)Math.Ceiling((double)length / ThreadingCount);
            for (int i = 0; i < ThreadingCount; i++)
            {
                if ((i+1)*dl > length)
                {
                    //这个时候已经将任务分配完了
                    //不需要再开线程了
                    var b = i * dl;
                    var n = length;
                    tasks.Add(Task.Run(() => w(i*dl, length, WorkBarrier)));
                    break;
                }
                else
                {
                    var b = i * dl;
                    var n = i * dl + dl;
                    tasks.Add(Task.Run(() => w(b, n, WorkBarrier)));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }
    }

    class Worker
    {
        public int Begin { get; set; }
        public int End { get; set; }
    }
}
