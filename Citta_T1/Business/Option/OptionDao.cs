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
            ElementSubType[] doubleInputs = new ElementSubType[] {
                                                ElementSubType.CollideOperator,
                                                ElementSubType.UnionOperator,
                                                ElementSubType.DifferOperator };
            List<ModelRelation> relations = Global.GetCurrentDocument().SearchRelationByID(mr.EndID,false);
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == mr.EndID && !doubleInputs.Contains(me.SubType))
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = true;
                    break;
                }
                else if (me.ID == mr.EndID && relations.Count == 2)
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = true;
                    break;
                }
            }

        }
        public void CreateResultControl(MoveOpControl moveOpControl, List<string> columnName)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            string tmpName = "L" + moveOpControl.ID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(0, tmpName, new Point(x, y));
            /*
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */

            PointF startPoint = new PointF(
                   moveOpControl.RectOut.Location.X + moveOpControl.Location.X,
                   moveOpControl.RectOut.Location.Y + moveOpControl.Location.Y
                   );
            PointF endPoint = new PointF(mrc.Location.X + mrc.RectIn.Location.X, mrc.Location.Y + mrc.RectIn.Location.Y);
            Bezier line = new Bezier(startPoint, endPoint);
            CanvasPanel canvas = Global.GetCanvasPanel();
            CanvasWrapper canvasWrp = new CanvasWrapper(canvas, canvas.CreateGraphics(), new Rectangle());
            canvas.RepaintObject(line);
            ModelRelation newModelRelation = new ModelRelation(
                                moveOpControl.ID, mrc.ID,
                                new Point(moveOpControl.RectOut.Location.X + moveOpControl.Location.X, moveOpControl.RectOut.Location.Y + moveOpControl.Location.Y),
                                new Point(mrc.RectIn.Location.X + mrc.Location.X, mrc.RectIn.Location.Y + mrc.Location.Y),
                                0);
            Global.GetCurrentDocument().AddModelRelation(newModelRelation);
            Global.GetCurrentDocument().BindRelationToControl(newModelRelation, moveOpControl, mrc);
            moveOpControl.OutPinInit("lineExit");
            mrc.rectInAdd(1);
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpName, columnName);
            mrc.Path = path;
        }



        //删除relation

        //修改配置输出
        public void IsModifyOut(List<string> oldColumns, List<string> currentcolumns, int ID)  
        {
           
            string path = Global.GetCurrentDocument().SearchResultOperator(ID).GetPath();
            List<string> columns = new List<string>();
            foreach (string cn in oldColumns)
            {
                //新输出字段中不包含旧字段
                if (!currentcolumns.Contains(cn))
                {                               
                    BCPBuffer.GetInstance().ReWriteBCPFile(path, currentcolumns);
                    Global.GetCurrentDocument().StateChange(ID);
                    return;

                }
            }
            //旧字段真包含于新字段
            foreach (string name in currentcolumns)
            {
                if (!oldColumns.Contains(name))
                    columns.Add(name);
            }
            List<string> outColumns = oldColumns.Concat(columns).ToList<string>();
            BCPBuffer.GetInstance().ReWriteBCPFile(path, outColumns);
        }
        //配置初始化
        public Dictionary<string, string> GetDataSourceInfo(int ID, bool singelOperation = true)
        {

            Dictionary<string, string> dataInfo=new Dictionary<string, string>();
            Dictionary<int, int> startControls = new Dictionary<int,int>();
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.EndID == ID && singelOperation)
                {
                    startControls[mr.EndPin] = mr.StartID;
                    break;
                }
                else if (mr.EndID == ID && !singelOperation)
                    startControls[mr.EndPin] = mr.StartID;

            }
            if(startControls.Count == 0)
                return dataInfo;
            foreach (KeyValuePair<int,int> kvp in startControls)
            {
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(kvp.Value);
                dataInfo["dataPath" + kvp.Key.ToString()] = me.GetPath();
                dataInfo["encoding" + kvp.Key.ToString()] = me.Encoding.ToString();
            }
            return dataInfo;
        }
       
    }
}
