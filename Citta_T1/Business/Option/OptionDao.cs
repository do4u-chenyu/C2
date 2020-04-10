using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Business.Model;
using Citta_T1.Utils;
using Citta_T1.Controls.Move;
using System.Drawing;
using Citta_T1.Controls;

namespace Citta_T1.Business.Option
{
    class OptionDao
    {

        //添加relation
        public void EnableControlOption(ModelRelation mr)
        { 
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == mr.EndID && me.Type == ElementType.Operator)
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = true;
                    break;
                }
            }

        }
        public void CreateResultControl(MoveOpControl moveOpControl, string[] columnName)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            string tmpName = "Result" + DateTime.Now.ToString("yyyyMMdd") + moveOpControl.ID.ToString();
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(0, tmpName, new Point(x, y));
            /*
             * TODO [DK] 添加线
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */
            Bezier line = new Bezier(
                new PointF(


                    moveOpControl.rectOut.Location.X + moveOpControl.Location.X,
                    moveOpControl.rectOut.Location.Y + moveOpControl.Location.Y
                    ),
                new PointF(mrc.Location.X + mrc.rectIn.Location.X, mrc.Location.Y + mrc.rectIn.Location.Y)
            );

            CanvasPanel canvas = Global.GetCanvasPanel();
            CanvasWrapper canvasWrp = new CanvasWrapper(canvas, canvas.CreateGraphics(), new Rectangle());
            canvas.RepaintObject(line);
            canvas.lines.Add(line);

            Global.GetModelDocumentDao().AddDocumentRelation(moveOpControl.ID, mrc.ID, moveOpControl.Location, mrc.Location, 0);
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpName, columnName);
            mrc.Path = path;
        }



        //删除relation

        //修改配置

        //配置初始化
        public Dictionary<string, string> GetInputDataInfo(int ID)
        {
            int startID = -1;
            Dictionary<string, string> dataInfo=new Dictionary<string, string>();
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.EndID == ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == startID)
                {
                    dataInfo["dataPath"] = me.GetPath();
                    dataInfo["encoding"] = me.Encoding.ToString();
                    break;
                }
            }
            return dataInfo;
        }
       
    }
}
