using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SNNS_Models
{
    class LoopRecorder<T>
    {
        T[] Array { get; set; }
        public LoopRecorder(int length)
        {
            this.Array = new T[length];
            this.length = length;
        }

        int Start { get; set; } = 0;
        public int Length { get { return Array.Length; } }
        int length;

        public T this[int index]
        {
            get {return  Array[(index + Start) % length]; }
            set { Array[(index + Start) % length] = value; }
        }

    }
}
