using System;
using C2.Core;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class AddTopicCommand : Command
    {
        Topic ParentTopic;
        Topic[] SubTopics;
        int Index;

        public AddTopicCommand(Topic parentTopic, Topic subTopic, int index)
        {
            if (parentTopic == null || subTopic == null)
            {
                throw new ArgumentNullException();
            }

            ParentTopic = parentTopic;
            SubTopics = new Topic[] { subTopic };
            Index = index;
        }

        public AddTopicCommand(Topic parentTopic, Topic[] subTopics, int index)
        {
            if (parentTopic == null || subTopics.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }

            ParentTopic = parentTopic;
            SubTopics = subTopics;
            Index = index;
        }

        public override string Name
        {
            get { return "Add"; }
        }

        public override bool Rollback()
        {
            foreach (var st in SubTopics)
            {
                if (ParentTopic.Children.Contains(st))
                {
                    ParentTopic.Children.Remove(st);
                }
            }

            return true;
        }
        public override bool Redo()
        {
            return Execute();
        }

        public override bool Execute()
        {
            foreach (var st in SubTopics)
            {
                if (ParentTopic == st || st.IsDescent(ParentTopic))
                    return false;
            }

            if (Index >= 0 && Index < ParentTopic.Children.Count)
            {
                var index = Index;
                foreach (var st in SubTopics)
                {
                    ParentTopic.Children.Insert(index, st);
                    index++;
                }
            }
            else
            {
                foreach (var st in SubTopics)
                {
                    ParentTopic.Children.Add(st);
                }
            }

            return true;
        }
    }
}
