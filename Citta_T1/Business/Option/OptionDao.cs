using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Business.Model;
using Citta_T1.Utils;
using Citta_T1.Controls.Move;

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
        public void CreateResultControl()
        {
            
        }



        //删除relation

        //修改配置
        //配置初始化,
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
