﻿using System;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class InvertLinkCommand : Command
    {
        Link Link;
        Topic From;
        Topic Target;

        public InvertLinkCommand(Link link)
        {
            if (link == null)
                throw new ArgumentNullException();

            Link = link;
            From = link.From;
            Target = link.Target;
        }

        public override string Name
        {
            get { return "Invert Link"; }
        }

        public override bool Rollback()
        {
            SetLink(Link, From, Target);

            return true;
        }
        public override bool Redo()
        {
            return Execute();
        }
        public override bool Execute()
        {
            SetLink(Link, Target, From);

            return true;
        }

        private void SetLink(Link link, Topic from, Topic target)
        {
            if (!from.Links.Contains(link))
            {
                if (link.From != null)
                    link.From.Links.Remove(link);
                from.Links.Add(link);
            }
            link.Target = target;
            link.RefreshLayout();
            link.SetChanged();
        }
    }
}
