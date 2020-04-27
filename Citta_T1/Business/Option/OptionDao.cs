using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Citta_T1.Business.Option
{
    class OptionDao
    {
        private LogUtil log = LogUtil.GetInstance("OptionDao");
        //添加relation
        public void EnableControlOption(ModelRelation mr)
        {
            ModelElement modelElement = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
            ElementSubType[] doubleInputs = new ElementSubType[] {
                                                ElementSubType.CollideOperator,
                                                ElementSubType.UnionOperator,
                                                ElementSubType.RelateOperator,
                                                ElementSubType.DifferOperator };
            List<ModelRelation> relations = Global.GetCurrentDocument().SearchRelationByID(mr.EndID,false);
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == mr.EndID && !doubleInputs.Contains(me.SubType))
                {
                    MoveOpControl moveOpControl = me.GetControl as MoveOpControl;
                    moveOpControl.EnableOpenOption = true;
                    SingleInputCompare(mr, moveOpControl.SingleDataSourceColumns);
                    break;
                }
                else if (me.ID == mr.EndID && doubleInputs.Contains(me.SubType) && relations.Count == 2)
                {
                    MoveOpControl moveOpControl = me.GetControl as MoveOpControl;
                    moveOpControl.EnableOpenOption = true;
                    //所有双目算子配置逻辑没写完，现在用会有问题
                   // DoubleInputCompare(relations, moveOpControl.DoubleDataSourceColumns, moveOpControl.ID);
                    break;
                }
            }
        }
        private void SingleInputCompare(ModelRelation modelRelation, string oldColumnName) 
        {
            if (oldColumnName == null) return;
            ModelElement startElement = Global.GetCurrentDocument().SearchElementByID(modelRelation.StartID);
            ModelElement endElement = Global.GetCurrentDocument().SearchElementByID(modelRelation.EndID);
            string dataSourcePath = startElement.GetPath();
            DSUtil.Encoding encoding = startElement.Encoding;
            int ID = startElement.ID;
            //获取当前连接的数据源的表头字段
            BcpInfo bcpInfo = new BcpInfo(dataSourcePath, "", ElementType.Null, encoding);
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            string[] oldName = oldColumnName.Split('\t');
            //新数据源表头不包含旧数据源
            foreach (string name in oldName)
            {
                if (!columnName.Contains(name))
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }
                   
            }
            //新数据源表头与旧数据源表头顺序是否不一致
            for (int i = 0; i < oldName.Count(); i++)
            {
                if (oldName[i] != columnName[i])
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }                  
            }
            //恢复控件上次的状态,可能会有点问题TODO
            if ((endElement.GetControl as MoveOpControl).Option.OptionDict != null)
                (endElement.GetControl as MoveOpControl).Status = ElementStatus.Ready;
            else
                (endElement.GetControl as MoveOpControl).Status = ElementStatus.Null;



        }
        private void DoubleInputCompare(List<ModelRelation> relations, Dictionary<string, List<string>> doubleDataSource,int ID) 
        {

            if (!doubleDataSource.ContainsKey("0") || !doubleDataSource.ContainsKey("1"))
                return;
            List<string> oldColumnName0 = doubleDataSource["0"];
            List<string> oldColumnName1 = doubleDataSource["1"];
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
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }
                   
            }

            foreach (string name in oldColumnName1)
            {
                if (!columnName1.Contains(name))
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }
                   
            }
            //新数据源表头与旧数据源表头顺序是否不一致

            for (int i = 0; i < oldColumnName0.Count(); i++)
            {
                if (oldColumnName0[i] != columnName0[i])
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }
            }

            for (int i = 0; i < oldColumnName1.Count(); i++)
            {
                if (oldColumnName1[i] != columnName1[i])
                {
                    Global.GetCurrentDocument().StateChangeByOut(ID);
                    return;
                }
            }
            //恢复控件上次的状态,可能会有点问题TODO
            ModelElement modelElement = Global.GetCurrentDocument().SearchElementByID(ID);
            if ((modelElement.GetControl as MoveOpControl).Option.OptionDict != null)
                (modelElement.GetControl as MoveOpControl).Status = ElementStatus.Ready;
            else
                (modelElement.GetControl as MoveOpControl).Status = ElementStatus.Null;

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
        public void DisableControlOption(int ID)
        {        
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                if (me.ID == ID && me.Type == ElementType.Operator)
                {
                    (me.GetControl as MoveOpControl).EnableOpenOption = false;
                    (me.GetControl as MoveOpControl).Status = ElementStatus.Null;
                    break;
                }
                if (me.ID == ID && me.Type == ElementType.Result)
                {
                    (me.GetControl as MoveRsControl).Status = ElementStatus.Null;
                    break;
                }
            }
        }

        //新数据源修改输出

        public bool IsDataSourceEqual(string[] oldColumnList, string[] columnName, int[] outIndex) 
        {
            int maxIndex = outIndex.Max();
            if (maxIndex > columnName.Length - 1)
                return true;
            List<string> oldName = new List<string>();
            List<string> newName = new List<string>();
            foreach (int i in outIndex)
            {
                oldName.Add(oldColumnList[i]);
                newName.Add(columnName[i]);
            }
            return (!Enumerable.SequenceEqual(oldName, newName));
  
        }
        public bool IsSingleDataSourceChange(MoveOpControl opControl, string[] columnName,string field, List<int> fieldList = null)
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断

            if (opControl.Option.GetOption("columnname") == "") return true;
            string[] oldColumnList = opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (field.Contains("factor") && opControl.Option.GetOption(field) != "")
                {
                    foreach (int fl in fieldList)
                    {
                        if (fl > columnName.Length - 1 || oldColumnList[fl] != columnName[fl])
                        {
                            opControl.Option.OptionDict.Remove(field);
                            return false;
                        }
                         
                    }
                }
                else if (field.Contains("outfield"))
                {

                    string[] checkIndexs = opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (IsDataSourceEqual(oldColumnList, columnName, outIndex))
                    {
                        opControl.Option.OptionDict.Remove("outfield");
                        return false;
                    }
                       
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return true;
        }

        //修改配置输出
        public void IsModifyOut(List<string> oldColumns, List<string> currentcolumns, int ID)  
        {
           
            string path = Global.GetCurrentDocument().SearchResultOperator(ID).GetPath();
            List<string> columns = new List<string>();

            //新输出字段中不包含旧字段
            foreach (string cn in oldColumns)
            {
                if (!currentcolumns.Contains(cn))
                {
                    IsNewOut(currentcolumns, ID);
                    return;
                }
                   
            }
            //输出数目相同，顺序可能不同
            if (oldColumns.Count >0 && oldColumns.Count == currentcolumns.Count)
            {
                if (!Enumerable.SequenceEqual(oldColumns, currentcolumns))
                {
                    IsNewOut(currentcolumns, ID);
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

        public void IsNewOut( List<string> currentcolumns, int ID)
        {
            string path = Global.GetCurrentDocument().SearchResultOperator(ID).GetPath();
            BCPBuffer.GetInstance().ReWriteBCPFile(path, currentcolumns);
            Global.GetCurrentDocument().StateChangeByOut(ID);
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
