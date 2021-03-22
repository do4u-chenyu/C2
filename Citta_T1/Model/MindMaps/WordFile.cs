using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    class WordFile
    {
    }

    class WordFileSaver 
    {
        XWPFDocument MyDoc = new XWPFDocument();

        private void WriteToWord() 
        { 

        }

        private void WriteImgToWord() 
        { 
        }

        private void WriteTableToWord() 
        { 
        }

        private string GetTopicInfo(Topic topic) 
        {
            string title = topic.XmlElementName;
            
            return null;
        }

        private void SaveAsWord(MindMap mindMap, string filename) 
        {
            Topic topic = mindMap.Root;

        }
    }
}
