using System;
using C2.Core;
using C2.Model;

namespace C2.Model.MindMaps
{
    public delegate void TopicEventHandler(object sender, TopicEventArgs e);

    public class TopicEventArgs : EventArgs
    {
        Topic _Topic;
        ChangeTypes _Changes;

        public TopicEventArgs(Topic topic)
        {
            Topic = topic;
        }

        public TopicEventArgs(Topic topic, ChangeTypes changes)
        {
            Topic = topic;
            Changes = changes;
        }

        public Topic Topic
        {
            get { return _Topic; }
            private set { _Topic = value; }
        }

        public ChangeTypes Changes
        {
            get { return _Changes; }
            private set { _Changes = value; }
        }
    }
}
