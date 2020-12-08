using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class ChangeLinkCommand : Command
    {
        Topic NewFromTopic;
        Topic NewToTopic;
        Topic OldFromTopic;
        Topic OldToTopic;
        Link ChangeLink;

        public override string Name
        {
            get { return "Change Link"; }
        }

        public ChangeLinkCommand(Link link, Topic from, Topic to)
        {
            ChangeLink = link;
            OldFromTopic = link.From;
            OldToTopic = link.Target;
            NewFromTopic = from;
            NewToTopic = to;
        }

        public override bool Execute()
        {
            if (ChangeLink == null || (NewFromTopic == null && NewToTopic == null) || (OldFromTopic == null && OldToTopic == null))
                return false;
            if (NewFromTopic != null)
                ChangeLink.From = NewFromTopic;
            else if (NewToTopic != null)
                ChangeLink.Target = NewToTopic;
            ChangeLink.RefreshLayout();

            return true;
        }

        public override bool Rollback()
        {
            if (ChangeLink == null || (NewFromTopic == null && NewToTopic == null) || (OldFromTopic == null && OldToTopic == null))
                return false;
            if (NewFromTopic != null)
                ChangeLink.From = OldFromTopic;
            else if (NewToTopic != null)
                ChangeLink.Target = OldToTopic;
            ChangeLink.RefreshLayout();
            return true;
        }

        public override bool Redo()
        {
            return Execute();
        }
    }
}
