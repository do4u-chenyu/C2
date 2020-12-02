using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class AddLinkCommand : Command
    {
        Topic FromTopic;
        Topic ToTopic;
        Link NewLink;

        public override string Name
        {
            get { return "Add Link"; }
        }

        public AddLinkCommand(Topic from, Topic to)
        {
            FromTopic = from;
            ToTopic = to;
        }
        public override bool Execute()
        {
            NewLink = Link.CreateNew(FromTopic, ToTopic);
            if (NewLink == null)
                return false;
            else
                NewLink.RefreshLayout();

            return true;
        }

        public override bool Rollback()
        {
            if (FromTopic == null || NewLink == null)
                return false;
            else
                FromTopic.Links.Remove(NewLink);
            return true;
        }

        public override bool Redo()
        {
            if (FromTopic == null || NewLink == null)
                return false;
            else
                FromTopic.Links.Add(NewLink);
            return true;
        }
    }
}
