using C2.Business.Model;
using C2.Controls.Move.Op;
using C2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace C2.Business.Option
{
    class OptionDao
    {
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
            string[] oldColumns0 = moveOpControl.FirstDataSourceColumns;
            string[] oldColumns1 = moveOpControl.SecondDataSourceColumns;
            List<string> newColumns0 = new List<string>() { };
            List<string> newColumns1 = new List<string>() { };

            // 任何情况下,第一个入度的数据源表头不能为空
            if (oldColumns0.Length == 0)
                return;

            // 二元算子时,第二个入度的数据源表头不能为空
            if (moveOpControl.IsBinaryDimension())
                if (oldColumns1.Length == 0)
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

            bool factor0 = newColumns0.Count >= oldColumns0.Length && oldColumns0.SequenceEqual(newColumns0.Take(oldColumns0.Length));
            bool factor1 = newColumns1.Count >= oldColumns1.Length && oldColumns1.SequenceEqual(newColumns1.Take(oldColumns1.Length));

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
            MoveOpControl moveOpControl = me.InnerControl as MoveOpControl;
            Dictionary<string, string> optionDict = moveOpControl.Option.OptionDict;

            if (optionDict == null)
                return ElementStatus.Null;

            string[] keys = new string[] { "otherSeparator", "browseChosen", "endRow", "fixSecond", "randomBegin", "randomEnd", "resultColumns", "pyParam" };
            List<string> keyList = new List<string>();
            foreach (RegisterInfo register in moveOpControl.Option.AllRegisterInfo[me.SubType])
            {
                if (register.Prefix != "factorH")
                    keyList.Add(register.Prefix);
            }
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                //python算子、IA多源算子中的其他分隔符字段允许为空,输入其他参数\指定结果文件也可能为空题 ，  
                //sort的结束行数也能为空。。。，直接判断为空会出问
                if (keys._Contains(kvp.Key))
                    continue;

                if (String.IsNullOrWhiteSpace(kvp.Value))
                    return ElementStatus.Null;
            }
            foreach (string key in keyList)
            {
                if (!optionDict.Keys.Contains(key) || string.IsNullOrWhiteSpace(optionDict[key]))
                    return ElementStatus.Null;
            }
            return ElementStatus.Ready;
        }
        // 判断是否清空单输入算子的配置
        public bool IsCleanSingleOperatorOption(MoveOpControl moc, string[] newColumns)
        {
            return IsCleanOption(moc, newColumns, "columnname0");
        }
        // 判断是否清空双输入算子的配置
        public bool IsCleanBinaryOperatorOption(MoveOpControl moc, string[] newColumns0, string[] newColumns1)
        {
            bool emptyOption0 = IsCleanOption(moc, newColumns0, "columnname0");
            bool emptyOption1 = IsCleanOption(moc, newColumns1, "columnname1");
            return emptyOption0 || emptyOption1;
        }
        public bool IsCleanOption(MoveOpControl moc, string[] newColumns, string field)
        {
            bool emptyOption = false;
            /*
             * 判断配置中是field否为空，为空则认为整个配置字典为空
             */
            string optionValues = moc.Option.GetOption(field);
            if (string.IsNullOrEmpty(optionValues))
                return !emptyOption;
            string[] oldColumns0 = optionValues.Split('\t');
            /*
             * 新旧数据源不一致，清空算子用户配置内容
             */
            bool factor0 = newColumns.Length >= oldColumns0.Length && oldColumns0.SequenceEqual(newColumns.Take(oldColumns0.Length));
            if (!factor0)
            {
                emptyOption = true;
                foreach (string key in moc.Option.Keys)
                    moc.Option[key] = String.Empty;
            } 
            return emptyOption;
        }

        //配置窗口输出的改变，引起后续子图状态改变逻辑


        //
        // 情况1：新输出和旧输出字段数目一致，且顺序一致
        //       后续子图Null状态
        // 情况2：新输出>旧输出字段数目一致，且顺序一致
        //       重写连接的结果Xml文件表头
        // 情况3：其他情况
        //       后续子图Null状态
        //

        public void DoOutputCompare(List<string> oldColumns, List<string> nowColumns, int ID)
        {
            int oldCount = oldColumns.Count;
            int nowCount = nowColumns.Count;
            if (nowCount == oldCount && oldColumns.SequenceEqual(nowColumns))
                return;
            bool isContain = nowCount > oldCount && oldCount > 0;
            string path = Global.GetCurrentDocument().SearchResultElementByOpID(ID).FullFilePath;
            if (isContain && oldColumns.SequenceEqual(nowColumns.Take(oldCount)))
            {            
                BCPBuffer.GetInstance().ReWriteBCPFile(path, nowColumns);
                return;
            }
            IsNewOut(nowColumns, ID, path);
        }

        public void IsNewOut(List<string> nowColumns, int ID, string fullFilePath)
        { 
            BCPBuffer.GetInstance().ReWriteBCPFile(fullFilePath, nowColumns);
            Global.GetCurrentDocument().SetChildrenStatusNull(ID);
        }


        //配置初始化，获取数据源表头信息
        public Dictionary<string, string> GetDataSourceInfoDict(int ID)
        {
            Dictionary<string, string> dataSourceInfoDict = new Dictionary<string, string>();
            List<ModelRelation> relations = Global.GetCurrentDocument().ModelRelations.FindAll(mr => mr.EndID == ID);
            foreach (ModelRelation mr in relations)
            {
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
                dataSourceInfoDict["dataPath" + mr.EndPin] = me.FullFilePath;
                dataSourceInfoDict["encoding" + mr.EndPin] = me.Encoding.ToString();
                dataSourceInfoDict["separator" + mr.EndPin] = me.Separator.ToString();
                dataSourceInfoDict["extType" + mr.EndPin] = me.ExtType.ToString();
                dataSourceInfoDict["description" + mr.EndPin] = me.Description;
            }
            return dataSourceInfoDict;
        }
        public List<ModelElement> GetDataSources(int ID)
        {
            Dictionary<string, string> dataSourceInfoDict = new Dictionary<string, string>();
            List<ModelElement> dataSources = new List<ModelElement>();
            List<ModelRelation> relations = Global.GetCurrentDocument().ModelRelations.FindAll(mr => mr.EndID == ID);
            foreach (ModelRelation mr in relations)
            {
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
                dataSources.Add(me);
            }
            return dataSources;
        }
    }
}
