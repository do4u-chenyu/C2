using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    class DocxFile
    {
    }

    class DocxFileSaver 
    {
        XWPFDocument MyDoc = new XWPFDocument();

        private void WriteToDocx() 
        { 

        }

        private void WriteImgToDocx() 
        { 
        }

        private void WriteTableToDocx() 
        { 
        }

        private string GetTopicInfo(Topic topic) 
        {
            string title = topic.XmlElementName;
            
            return null;
        }

        private void SaveAsDocx(MindMap mindMap, string filename) 
        {
            Topic topic = mindMap.Root;

        }
    }
}
