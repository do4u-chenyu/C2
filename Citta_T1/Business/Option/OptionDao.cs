using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.Business.Option
{
    class OptionDao
    {
        private static LogUtil log = LogUtil.GetInstance("OptionDao");
        //添加relation
        public void EnableControlOption(ModelRelation mr)
        {
            ModelElement modelElement = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
            ElementSubType[] doubleInputs = new ElementSubType[] {
                                                ElementSubType.CollideOperator,
                                                ElementSubType.UnionOperator,
                                                ElementSubType.RelateOperator,
                                                ElementSubType.DifferOperator,
                                                ElementSubType.CustomOperator2};
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
                    DoubleInputCompare(relations, moveOpControl.DoubleDataSourceColumns, moveOpControl.ID);
                    break;
                }
            }
        }
        private void SingleInputCompare(ModelRelation modelRelation, string oldColumnName) 
        {
           if (oldColumnName == null) return;
            char separator = '\t';
            ModelElement startElement = Global.GetCurrentDocument().SearchElementByID(modelRelation.StartID);
            ModelElement endElement = Global.GetCurrentDocument().SearchElementByID(modelRelation.EndID);
            string dataSourcePath = startElement.GetFullFilePath();
            DSUtil.Encoding encoding = startElement.Encoding;
            int ID = endElement.ID;
            //获取当前连接的数据源的表头字段
            BcpInfo bcpInfo = new BcpInfo(dataSourcePath, "", ElementType.Null, encoding);
            string column = bcpInfo.columnLine;
            if (startElement.GetControl is MoveDtControl)
                separator = (startElement.GetControl as MoveDtControl).Separator;
            string[] columnName = column.Split(separator);
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
            Dictionary<string, string> optionDict = (endElement.GetControl as MoveOpControl).Option.OptionDict;
            if (optionDict == null)  return;
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                if (kvp.Key == "otherSeparator" || kvp.Key == "pyParam" || kvp.Key == "browseChosen" || kvp.Key == "endRow") continue;//python算子、IA多源算子中的其他分隔符字段允许为空,输入其他参数\指定结果文件也可能为空，sort的结束行数也能为空。。。，直接判断为空会出问题
                if (optionDict[kvp.Key] == "") return;
            }
            (endElement.GetControl as MoveOpControl).Status = ElementStatus.Ready;




        }
        private void DoubleInputCompare(List<ModelRelation> relations, Dictionary<string, List<string>> doubleDataSource,int ID) 
        {

            if (!doubleDataSource.ContainsKey("0") || !doubleDataSource.ContainsKey("1"))
                return;
            char separator0 = '\t';
            char separator1 = '\t';
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
            
            string dataSourcePath0 = modelElement0.GetFullFilePath();
            DSUtil.Encoding encoding0 = modelElement0.Encoding;
            int ID0 = modelElement0.ID;

            string dataSourcePath1 = modelElement1.GetFullFilePath();
            DSUtil.Encoding encoding1 = modelElement1.Encoding;
            int ID1 = modelElement1.ID;

            
            //获取当前连接的数据源的表头字段
            BcpInfo bcpInfo0 = new BcpInfo(dataSourcePath0, "", ElementType.Null, encoding0);
            string column0 = bcpInfo0.columnLine;
            if (modelElement0.GetControl is MoveDtControl)
                separator0 = (modelElement0.GetControl as MoveDtControl).Separator;
            string[] columnName0 = column0.Split(separator0);

            BcpInfo bcpInfo1 = new BcpInfo(dataSourcePath1, "", ElementType.Null, encoding1);
            string column1 = bcpInfo1.columnLine;
            if (modelElement1.GetControl is MoveDtControl)
                separator1 = (modelElement1.GetControl as MoveDtControl).Separator;
            string[] columnName1 = column1.Split(separator1);
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
            
            Dictionary<string, string> optionDict = (modelElement.GetControl as MoveOpControl).Option.OptionDict;
            if (optionDict == null) return;
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                if (kvp.Key == "otherSeparator") continue;//python算子、IA多源算子中的其他分隔符字段允许为空，直接判断为空会出问题
                if (optionDict[kvp.Key] == "") return;
            }
            (modelElement.GetControl as MoveOpControl).Status = ElementStatus.Ready;

        }
        public void CreateResultControl(MoveOpControl moveOpControl, List<string> columnName,char seperator = '\t', DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            string tmpBcpFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                System.IO.Path.GetFileNameWithoutExtension(tmpBcpFileName), Global.GetCurrentDocument().SizeL,
            new Point(x, y), seperator, encoding);;
          
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
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpBcpFileName, columnName);
            mrc.FullFilePath = path;
        }

        public void CreateResultControlCustom(MoveOpControl moveOpControl, string path, char separator= '\t', DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            //string tmpBcpFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                System.IO.Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path)), Global.GetCurrentDocument().SizeL,
                new Point(x, y), separator, encoding);
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
            mrc.FullFilePath = path;
        }

        //新数据源修改输出

        public bool IsDataSourceEqual(string[] oldColumnList, string[] columnName, int[] outIndex) 
        {
            int maxIndex = outIndex.Max();
            if (maxIndex > columnName.Length - 1)
                return true;
            return (!Enumerable.SequenceEqual(oldColumnList, columnName));
  
        }
        public bool IsSingleDataSourceChange(MoveOpControl opControl, string[] columnName,string field, List<int> fieldList = null)
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否清空进行判断

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
                            opControl.Option.OptionDict[field] = "";
                            return false;
                        }
                    }
                }
                else if (field.Contains("outfield") && opControl.Option.GetOption(field) != "")
                {

                    string[] checkIndexs = opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (IsDataSourceEqual(oldColumnList, columnName, outIndex))
                    {
                        opControl.Option.OptionDict["outfield"] = "";
                        return false;
                    }

                }
                else if(opControl.Option.GetOption(field) != "")
                {
                    //单选框配置的判断
                    int index = Convert.ToInt32(opControl.Option.GetOption(field));
                    if (index > columnName.Length - 1 || oldColumnList[index] != columnName[index])
                        opControl.Option.OptionDict[field] = "";
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return true;
        }
        public bool IsDoubleDataSourceChange(MoveOpControl opControl, string[] columnName0, string[] columnName1, string field, List<int> fieldList = null)
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (opControl.Option.GetOption("columnname0") == "" || opControl.Option.GetOption("columnname1") == "") return true;
            string[] oldColumnList0 = opControl.Option.GetOption("columnname0").Split('\t');
            string[] oldColumnList1 = opControl.Option.GetOption("columnname1").Split('\t');

            try
            { 
                if (field.Contains("factor") && opControl.Option.GetOption(field) != "")
                {
                    bool IsEqual0 = fieldList[0] > columnName0.Length - 1 || oldColumnList0[fieldList[0]] != columnName0[fieldList[0]];
                    bool IsEqual1 = fieldList[1] > columnName1.Length - 1 || oldColumnList1[fieldList[1]] != columnName1[fieldList[1]];
                    if (IsEqual0 || IsEqual1)
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                }
                else if (field.Contains("outfield"))
                {

                    string[] checkIndexs = opControl.Option.GetOption(field).Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (field == "outfield1" && IsDataSourceEqual(oldColumnList1, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                    if (field != "outfield1" && IsDataSourceEqual(oldColumnList0, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
            return true;
        }
        //修改配置输出
        public void IsModifyOut(List<string> oldColumns, List<string> currentcolumns, int ID)  
        {
           
            string path = Global.GetCurrentDocument().SearchResultOperator(ID).GetFullFilePath();
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
            //新输出字段包含就字段，但是新输出字段数目少于旧字段数目，如并集的重复选择
            if (oldColumns.Count > currentcolumns.Count)
            {
                IsNewOut(currentcolumns, ID);
                return;
            }
            //判断输出顺序是否一致，如排序算子

            if (oldColumns.Count > 0)
            {
                for (int i = 0; i < oldColumns.Count(); i++)
                {
                    if (oldColumns[i] != currentcolumns[i])
                    {
                        IsNewOut(currentcolumns, ID);
                        return;
                    }
                }
                if (currentcolumns.Skip(oldColumns.Count()).Count() != 0)
                {
                    List<string> outColumns = oldColumns.Concat(currentcolumns.Skip(oldColumns.Count())).ToList<string>();
                    BCPBuffer.GetInstance().ReWriteBCPFile(path, outColumns);
                }

            }
            else if (oldColumns.Count == 0)
            { IsNewOut(currentcolumns, ID); }
                   
        }
        public void IsModifyDoubleOut(List<string> oldColumns0, List<string> currentcolumns0, List<string> oldColumns1, List<string> currentcolumns1, int ID)
        {
            List<string> columns = new List<string>();
            string path = Global.GetCurrentDocument().SearchResultOperator(ID).GetFullFilePath();
            //左侧数据源判断
            if (oldColumns0.Count() != currentcolumns0.Count()|| !oldColumns0.SequenceEqual(currentcolumns0))
            {
                IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                return;
            }
            //右侧数据源判断,新输出字段中不包含旧字段
            foreach (string cn in oldColumns1)
            {
                if (!currentcolumns1.Contains(cn))
                {
                    IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                    return;
                }
            }

            //判断输出顺序是否一致，如排序算子

            if (oldColumns1.Count > 0)
            {
                for (int i = 0; i < oldColumns1.Count(); i++)
                {
                    if (oldColumns1[i] != currentcolumns1[i])
                    {
                        IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                        return;
                    }
                }
                if (currentcolumns1.Skip(oldColumns1.Count()).Count() != 0)
                {
                    List<string> outColumns = oldColumns1.Concat(currentcolumns1.Skip(oldColumns1.Count())).ToList<string>();
                    BCPBuffer.GetInstance().ReWriteBCPFile(path, currentcolumns0.Concat(outColumns).ToList());
                }
            }
            else if(oldColumns1.Count == 0)
            { IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID); }
        }

        public void IsNewOut( List<string> currentColumns, int ID)
        {
            string fullFilePath = Global.GetCurrentDocument().SearchResultOperator(ID).GetFullFilePath();
            BCPBuffer.GetInstance().ReWriteBCPFile(fullFilePath, currentColumns);
            Global.GetCurrentDocument().StateChangeByOut(ID);
        }
        //更新输出列表选定项的索引
        public void UpdateOutputCheckIndexs(List<int> checkIndexs, List<int> outIndexs)
        {
            foreach (int index in checkIndexs)
            {
                if (!outIndexs.Contains(index))
                    outIndexs.Add(index);
            }
            foreach (int index in outIndexs)
            {
                if (!checkIndexs.Contains(index))
                {
                    outIndexs.Clear();
                    outIndexs.AddRange(checkIndexs);
                    break;
                }
            }
 
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
                char separator = '\t';
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(kvp.Value);
                dataInfo["dataPath" + kvp.Key.ToString()] = me.GetFullFilePath();
                dataInfo["encoding" + kvp.Key.ToString()] = me.Encoding.ToString();
                if (me.GetControl is MoveDtControl)
                    separator = (me.GetControl as MoveDtControl).Separator;
                dataInfo["separator" + kvp.Key.ToString()] = separator.ToString();
            }
            return dataInfo;
        }
        #region 配置窗口不合法字符判断
        public void NonNumeric_ControlText(Control control) 
        {
            if (control.Text == "") return;
            Regex rg = new Regex("^[0-9]*[1-9][0-9]*$");
            if (!rg.IsMatch(control.Text))
            {
                control.Text = "";
                MessageBox.Show("请输入数字");
            }
        }
        public void IsIllegalInputName(Control control,String[] columnName,String name) 
        {
            if (columnName.Count() == 0 || name == "") return;
            if (!columnName.Contains(name))
            {
                control.Text = "";
                MessageBox.Show("未输入正确列名，请从下拉列表中选择正确列名");
            }
        }
        public void Control_Leave(object sender, EventArgs e)
        {
            List<string> columnName = new List<string>();
            foreach ( var item in (sender as ComboBox).Items)
            {
                columnName.Add(item.ToString());
            }
            IsIllegalInputName((sender as ComboBox), columnName.ToArray(), (sender as ComboBox).Text);
        }
        public void Control_KeyUp(object sender, KeyEventArgs e)
        {
            List<string> columnName = new List<string>();
            foreach (var item in (sender as ComboBox).Items)
            {
                columnName.Add(item.ToString());
            }
            if (e.KeyCode == Keys.Enter)
               IsIllegalInputName((sender as ComboBox), columnName.ToArray(), (sender as ComboBox).Text);
        }
        public void IsIllegalCharacter(object sender, EventArgs e)
        {

            if ((sender as TextBox).Text.Contains(",") || (sender as TextBox).Text.Contains("，"))
            {
                (sender as TextBox).Text = ""; 
                MessageBox.Show("输入非法字','，请重新输入过滤条件");
            }
        }
        #endregion
        #region 配置窗口获取用户某些选定数据事件
        public void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }
        #endregion
    }
}
