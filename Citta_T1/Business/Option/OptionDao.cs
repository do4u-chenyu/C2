using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.Business.Option
{
    class OptionDao
    {
        private static LogUtil log = LogUtil.GetInstance("OptionDao");

        // 情况1
        // LEFT_ME ----- StartID.MR.EndID ----- RIGHT_ME
        // RIGHT_ME.EnableOption开启设置菜单
        // 更新子图的配置状态
        //
        // 情况2
        // LEFT_ME0 ----- StartID.Brother1.EndID ----- RIGHT_ME(双元素算子)
        //                StartID.Brother2.EndID -----|
        // RIGHT_ME.EnableOption开启设置菜单
        // 更新子图的配置状态
        public void EnableOpOptionView(ModelRelation mr)
        {      
            ModelElement rightMe = Global.GetCurrentDocument().SearchElementByID(mr.EndID);
            // 手工划线时关系的ENDID必须为OPControl
            if (rightMe == ModelElement.Empty || rightMe.Type != ElementType.Operator)
                return;

            MoveOpControl moveOpControl = rightMe.InnerControl as MoveOpControl;
            // 情况1   
            if (moveOpControl.IsSingleDimension()) 
            {
                moveOpControl.EnableOption = true;
                DoInputComare(rightMe, mr, ModelRelation.Empty);   
            }
            // 情况2
            else
            {
                List<ModelRelation> brothers = Global.GetCurrentDocument().SearchBrotherRelations(mr);
                if (brothers.Count != 2) return;
                moveOpControl.EnableOption = true;
                DoInputComare(rightMe, brothers[0], brothers[1]);
            }                
        }

        //  情况1: 旧表是新表的子集，且顺序一致
        //         算子恢复配置
        //  情况2: 否则
        //         后续子节点配置状态全为Null

        private void DoInputComare(ModelElement me, ModelRelation mr0, ModelRelation mr1)
        {
            /*
             * 获取单，双输入新旧数据源旧表头
             */
            MoveOpControl moveOpControl = me.InnerControl as MoveOpControl;
            List<string> oldColumns0 = moveOpControl.FirstDataSourceColumns; 
            List<string> oldColumns1 = moveOpControl.SecondDataSourceColumns;
            List<string> newColumns0 = new List<string>() { };
            List<string> newColumns1 = new List<string>() { };

            // 任何情况下,第一个入度的数据源表头不能为空
            if (oldColumns0.Count == 0)
                return;

            // 二元算子时,第二个入度的数据源表头不能为空
            if (moveOpControl.IsBinaryDimension())
                if(oldColumns1.Count == 0)
                    return;

            ModelElement startElement0 = Global.GetCurrentDocument().SearchElementByID(mr0.StartID);
            LoadColumns(startElement0, newColumns0);
    
            if (moveOpControl.IsBinaryDimension())// 二元算子
            {
                ModelElement startElement1 = Global.GetCurrentDocument().SearchElementByID(mr1.StartID);
                LoadColumns(startElement1, newColumns1);
                // newColumns0对应第一个入度的表头
                // newColumns1对应第二个入度的表头
                // 默认线0落在入度0,线1落在入度1
                // 线0落在入度1,线1落在入度0, 交换表头
                if (mr0.EndPin == 1 && mr1.EndPin == 0)
                    Swap(ref newColumns0, ref newColumns1);  
            }

            bool factor0 = newColumns0.Count >= oldColumns0.Count && oldColumns0.SequenceEqual(newColumns0.Take(oldColumns0.Count));
            bool factor1 = newColumns1.Count >= oldColumns1.Count && oldColumns1.SequenceEqual(newColumns1.Take(oldColumns1.Count));

            if (factor0 && factor1)
            {
                me.Status = RestoreOptionStatus(me);
                return;
            } 
            Global.GetCurrentDocument().SetChildrenStatusNull(me.ID);
        }

        private void Swap(ref List<string> A, ref List<string> B)
        {
            List<string> C = A;
            A = B;
            B = C;
        }

        private void LoadColumns(ModelElement me, List<string> columns)
        {
            columns.AddRange(new BcpInfo(me).ColumnArray);
        }




        //获取算子上次配置状态
        private ElementStatus RestoreOptionStatus(ModelElement me)
        { 
            Dictionary<string, string> optionDict = (me.InnerControl as MoveOpControl).Option.OptionDict;

            if (optionDict == null)
                return ElementStatus.Null;

            string[] keys = new string[] { "otherSeparator", "browseChosen", "endRow" };

            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                //python算子、IA多源算子中的其他分隔符字段允许为空,输入其他参数\指定结果文件也可能为空，sort的结束行数也能为空。。。，直接判断为空会出问题       
                if (keys.Contains(kvp.Key))
                    continue;

                if (String.IsNullOrWhiteSpace(optionDict[kvp.Key]))
                   return ElementStatus.Null;
            }
            return ElementStatus.Ready;
        }
       

        //新数据源修改输出

        public bool IsDataSourceNotEqual(string[] oldColumnList, string[] columnName, int[] outIndex) 
        {
            int maxIndex = outIndex.Max();
            if (maxIndex > columnName.Length - 1)
                return false;
            foreach(int index in outIndex)
            {
                if (oldColumnList[index] != columnName[index])
                    return false;
            }
            return true;
  
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
                    if (!IsDataSourceNotEqual(oldColumnList, columnName, outIndex))
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
                    if (field == "outfield1" && !IsDataSourceNotEqual(oldColumnList1, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                    if (field != "outfield1" && !IsDataSourceNotEqual(oldColumnList0, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
            return true;
        }
        //配置窗口输出的改变，引起后续子图状态改变逻辑

      
        public void DoOutputCompare(List<string> oldColumns, List<string> currentcolumns, int ID)  
        {

            /*
            * 情况1：新输出和旧输出字段数目一致，且顺序一致
            *       后续子图Null状态
            * 情况2：新输出>旧输出字段数目一致，且顺序一致
            *       重写连接的结果Xml文件表头
            * 情况3：其他情况
            *      后续子图Null状态
            */
           

            int oldCount = oldColumns.Count;
            int nowCount = currentcolumns.Count;
            if (nowCount == oldCount && oldColumns.SequenceEqual(currentcolumns))
                return;
            else if (nowCount > oldCount && oldColumns.SequenceEqual(currentcolumns.Take(oldCount)))
            {
                string path = Global.GetCurrentDocument().SearchResultElementByOpID(ID).FullFilePath;
                BCPBuffer.GetInstance().ReWriteBCPFile(path, currentcolumns);
            }    
            else
                IsNewOut(currentcolumns, ID);

        }
        public void IsModifyDoubleOut(List<string> oldColumns0, List<string> currentcolumns0, List<string> oldColumns1, List<string> currentcolumns1, int ID)
        {
            List<string> columns = new List<string>();
            string path = Global.GetCurrentDocument().SearchResultElementByOpID(ID).FullFilePath;
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
            string fullFilePath = Global.GetCurrentDocument().SearchResultElementByOpID(ID).FullFilePath;
            BCPBuffer.GetInstance().ReWriteBCPFile(fullFilePath, currentColumns);
            Global.GetCurrentDocument().SetChildrenStatusNull(ID);
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
            List<ModelRelation> relations = Global.GetCurrentDocument().ModelRelations.FindAll(mr => mr.EndID == ID);
            foreach (ModelRelation mr in relations)
            {
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
                dataInfo["dataPath" + mr.EndPin] = me.FullFilePath;
                dataInfo["encoding" + mr.EndPin] = me.Encoding.ToString();
                dataInfo["separator" + mr.EndPin] = me.Separator.ToString();
            }
            return dataInfo;
        }
     
        public void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }
   
    }
}
