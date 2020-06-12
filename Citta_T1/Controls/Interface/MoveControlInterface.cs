using System;
using System.Drawing;

namespace Citta_T1.Controls.Interface
{

    // 画布上的所有可移动控件接口
    interface IMoveControl
    {
        // 修正坐标
        void OutPinInit(String status);
        void RectInAdd(int pinIndex);
        void InPinInit(int pinIndex);
        void ControlSelect();
        void ControlNoSelect();
        void DeleteMenuItem_Click(object sender, EventArgs e);
    }
}
