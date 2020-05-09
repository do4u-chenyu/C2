using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Controls.Interface
{
    // 缩放接口
    public interface IScalable
    {
        void ChangeSize(int sizeL);
    }
    // 拖拽接口
    public interface IDragable
    {
        void ChangeLoc(float dx, float dy);
    }
    // 画布上的所有可移动控件接口
    interface IMoveControl
    {
        void UpdateLineWhenMoving();
        void SaveStartLines(int line_index);
        void SaveEndLines(int line_index);
        // 修正坐标
        PointF RevisePointLoc(PointF p);
        int GetID();
        PointF GetStartPinLoc(int pinIndex);
        PointF GetEndPinLoc(int pinIndex);

        void OutPinInit(String status);
        void rectInAdd(int pinIndex);
        void ControlSelect();
        void ControlNoSelect();

    }
}
