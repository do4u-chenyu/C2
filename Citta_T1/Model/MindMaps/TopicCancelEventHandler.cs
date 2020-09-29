using System.ComponentModel;

namespace Citta_T1.Model.MindMaps
{
    public delegate void TopicCancelEventHandler(object sender, TopicCancelEventArgs e);

    public class TopicCancelEventArgs : CancelEventArgs
    {
        private Topic _Topic;

        public TopicCancelEventArgs(Topic topic)
        {
            Topic = topic;
        }

        public Topic Topic
        {
            get { return _Topic; }
            private set { _Topic = value; }
        }
    }
}
