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
            ModelElement modelElement = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
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
                    //TODO单输入 SingleInputCompare
                    break;
                }
                else if (me.ID == mr.EndID && relations.Count == 2)
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = true;
                    //TODO双输入 DoubleInputCompare
                    break;
                }
            }
        }
        private void SingleInputCompare(ModelElement modelElement, List<string> oldColumnName) 
        {
            string dataSourcePath = modelElement.GetPath();
            DSUtil.Encoding encoding = modelElement.Encoding;
            int ID = modelElement.ID;
            if (oldColumnName.Count == 0)
                return;
            //获取当前连接的数据源的表头字段
            BcpInfo bcpInfo = new BcpInfo(dataSourcePath, "", ElementType.Null, encoding);
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            //新数据源表头不包含旧数据源
            foreach (string name in oldColumnName)
            {              
                if (!columnName.Contains(name))
                    Global.GetCurrentDocument().StateChange(ID);
            }
            //新数据源表头与旧数据源表头顺序是否不一致
            if (oldColumnName.Count() > columnName.Count())
                return;
            for (int i = 0; i < oldColumnName.Count(); i++)
            {
                if (oldColumnName[i] != columnName[i])
                {
                    Global.GetCurrentDocument().StateChange(ID);
                    return;
                }                  
            }
            //回复控件上次的状态
            if((modelElement.GetControl as MoveOpControl).Option != null)
                (modelElement.GetControl as MoveOpControl).Status = ElementStatus.Ready;
            else
                (modelElement.GetControl as MoveOpControl).Status = ElementStatus.Null;



        }
        private void DoubleInputCompare(List<ModelRelation> relations, List<string> oldColumnName0, List<string> oldColumnName1,int ID) 
        {
            ModelElement modelElement0 = null;
            ModelElement modelElement1 = null;
            foreach (ModelRelation me in relations)
            {
                if(me.EndPin == 0)
                    modelElement0 = Global.GetCurrentDocument().SearchElementByID(me.StartID);
                else if (me.EndPin == 1)
                    modelElement1 = Global.GetCurrentDocument().SearchElementByID(me.StartID);
            }
            
            string dataSourcePath0 = modelElement0.GetPath();
            DSUtil.Encoding encoding0 = modelElement0.Encoding;
            int ID0 = modelElement0.ID;

            string dataSourcePath1 = modelElement1.GetPath();
            DSUtil.Encoding encoding1 = modelElement1.Encoding;
            int ID1 = modelElement1.ID;

            if (oldColumnName0.Count == 0 || oldColumnName1.Count == 0)
                return;
            //获取当前连接的数据源的表头字段
            BcpInfo bcpInfo0 = new BcpInfo(dataSourcePath0, "", ElementType.Null, encoding0);
            string column0 = bcpInfo0.columnLine;
            string[] columnName0 = column0.Split('\t');

            BcpInfo bcpInfo1 = new BcpInfo(dataSourcePath1, "", ElementType.Null, encoding1);
            string column1 = bcpInfo1.columnLine;
            string[] columnName1 = column1.Split('\t');
            //新数据源表头不包含旧数据源
            foreach (string name in oldColumnName0)
            {
                if (!columnName0.Contains(name))
                    Global.GetCurrentDocument().StateChange(ID);
            }

            foreach (string name in oldColumnName1)
            {
                if (!columnName1.Contains(name))
                    Global.GetCurrentDocument().StateChange(ID);
            }
            //新数据源表头与旧数据源表头顺序是否不一致
            if (oldColumnName0.Count() > columnName0.Count())
                return;
            for (int i = 0; i < oldColumnName0.Count(); i++)
            {
                if (oldColumnName0[i] != columnName0[i])
                {
                    Global.GetCurrentDocument().StateChange(ID);
                    return;
                }
            }

            if (oldColumnName1.Count() > columnName1.Count())
                return;
            for (int i = 0; i < oldColumnName1.Count(); i++)
            {
                if (oldColumnName1[i] != columnName1[i])
                {
                    Global.GetCurrentDocument().StateChange(ID);
                    return;
                }
            }


        }
        public void CreateResultControl(MoveOpControl moveOpControl, List<string> columnName)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            string tmpName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
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

            canvas.RepaintObject(line);
            ModelRelation newModelRelation = new ModelRelation(
                                moveOpControl.ID, mrc.ID,
                                new Point(moveOpControl.RectOut.Location.X + moveOpControl.Location.X, moveOpControl.RectOut.Location.Y + moveOpControl.Location.Y),
                                new Point(mrc.RectIn.Location.X + mrc.Location.X, mrc.RectIn.Location.Y + mrc.Location.Y),
                                0);
            Global.GetCurrentDocument().AddModelRelation(newModelRelation);

            moveOpControl.OutPinInit("lineExit");
            mrc.rectInAdd(1);
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpName, columnName);
            mrc.Path = path;
        }



        //删除数据源和算子之间的线
        public void DisableControlOption(ModelRelation mr)
        {        
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == mr.EndID && me.Type == ElementType.Operator)
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = false;
                    break;
                }
            }
        }


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
