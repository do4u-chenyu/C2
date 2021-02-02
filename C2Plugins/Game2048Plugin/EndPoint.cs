using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    class EndPoint
    {
        public EndPoint(int pos,bool isEmpty)
        {
            Pos = pos;
            IsEmpty = isEmpty;
        }
        public int Pos { get; set; }   //移动终点的坐标
        public bool IsEmpty { get; set; }//是否为空
    }
}
