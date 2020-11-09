using C2.Controls.MapViews;
using C2.Model.MindMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Controls.MapViews
{
    class DataTreeNode : TopicTreeNode
    {
        private String _text;
        public DataTreeNode(Topic topic) : base(topic)
        {
        }

        public DataTreeNode(String text)
        {
            Text1 = text;
        }

        public string Text1 { get => _text; set => _text = value; }
    }
}
