using System;

namespace C2.Model.MindMaps
{
    public delegate void LinkEventHandler(object sender, LinkEventArgs e);

    public class LinkEventArgs : EventArgs
    {
        private Link _Link;

        public LinkEventArgs(Link link)
        {
            Link = link;
        }

        public Link Link
        {
            get { return _Link; }
            private set { _Link = value; }
        }
    }
}
