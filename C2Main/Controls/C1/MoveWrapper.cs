using C2.Business.Model;
using C2.Controls.Move;
using C2.Controls.Move.Op;
using C2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls
{
    class MoveWrapper
    {
        private Point now;
        private bool on;
        private MoveBaseControl moc;
        private List<ModelRelation> startRels;
        private List<ModelRelation> endRels;
        public MoveWrapper(MoveBaseControl moc)
        {
            this.moc = moc;
        }
        public Point Now
        {
            get { return this.now; }
            set
            {
                if (value != now)
                {
                    Point old = now;
                    now = value;
                    if (on)
                        OnNowPointChange(old);
                }
            }
        }
        public void MouseDown(Point p)
        {
            this.on = true;
            this.now = p;
            this.startRels = Global.GetCurrentModelDocument().ModelRelations.Where(rel => rel.StartID == this.moc.ID).ToList();
            this.endRels = Global.GetCurrentModelDocument().ModelRelations.Where(rel => rel.EndID == this.moc.ID).ToList();
        }
        public void MouseMove(Point p)
        {
            this.Now = p;
        }
        public void MouseUp()
        {
            this.on = false;
        }
        private void OnNowPointChange(Point old)
        {
            Rectangle oldRegion = GetRegion(old);
            Rectangle newRegion = GetRegion(Now);
            oldRegion.Inflate(1, 1);
            newRegion.Inflate(1, 1);
            Invalidate(oldRegion);
            Invalidate(newRegion);
        }

        private void Invalidate(Rectangle region)
        {
            Global.GetCanvasPanel().Invalidate(region);
        }

        private Rectangle GetRegion(Point p)
        {
            int left = Math.Min(Math.Min(p.X, this.MinStartRelsLeft()), this.MinEndRelsLeft());
            int top = Math.Min(Math.Min(p.Y, this.MinStartRelsTop()), this.MinEndRelsTop()); 
            int right = Math.Max(Math.Max(p.X + this.moc.Width, this.MaxStartRelsRight()), this.MaxEndRelsRight()); 
            int bottom = Math.Max(Math.Max(p.Y + this.moc.Height, this.MaxStartRelsBottom()), this.MaxEndRelsBottom());
            return new Rectangle(left, top, right - left, bottom - top);
        }


        private int MinStartRelsLeft()
        {
            if (this.startRels.Count == 0)
                return this.moc.Location.X;
            float min = this.startRels[0].EndP.X;
            foreach (ModelRelation mr in this.startRels)
                min = Math.Min(min, mr.EndP.X);
            return (int)min;
        }
        private int MinEndRelsLeft()
        {
            if (this.endRels.Count == 0)
                return this.moc.Location.X;
            float min = this.endRels[0].StartP.X;
            foreach (ModelRelation mr in this.endRels)
                min = Math.Min(min, mr.StartP.X);
            return (int)min;
        }

        private int MinStartRelsTop()
        {
            if (this.startRels.Count == 0)
                return this.moc.Location.Y;
            float min = this.startRels[0].EndP.Y;
            foreach (ModelRelation mr in this.startRels)
                min = Math.Min(min, mr.EndP.Y);
            return (int)min;
        }
        private int MinEndRelsTop()
        {
            if (this.endRels.Count == 0)
                return this.moc.Location.Y;
            float min = this.endRels[0].StartP.Y;
            foreach (ModelRelation mr in this.endRels)
                min = Math.Min(min, mr.StartP.Y);
            return (int)min;
        }
        private int MaxStartRelsRight()
        {
            if (this.startRels.Count == 0)
                return this.moc.Location.Y + this.moc.Width;
            float max = this.startRels[0].EndP.X;
            foreach (ModelRelation mr in this.startRels)
                max = Math.Max(max, mr.EndP.X);
            return (int)max;
        }

        private int MaxEndRelsRight()
        {
            if (this.endRels.Count == 0)
                return this.moc.Location.Y + this.moc.Width;
            float max = this.endRels[0].StartP.X;
            foreach (ModelRelation mr in this.endRels)
                max = Math.Max(max, mr.StartP.X);
            return (int)max;
        }
        private int MaxStartRelsBottom()
        {
            if (this.startRels.Count == 0)
                return this.moc.Location.Y + this.moc.Height;
            float max = this.startRels[0].EndP.Y;
            foreach (ModelRelation mr in this.startRels)
                max = Math.Max(max, mr.EndP.Y);
            return (int)max;
        }

        private int MaxEndRelsBottom()
        {
            if (this.endRels.Count == 0)
                return this.moc.Location.Y + this.moc.Height;
            float max = this.endRels[0].StartP.Y;
            foreach (ModelRelation mr in this.endRels)
                max = Math.Max(max, mr.StartP.Y);
            return (int)max;
        }
    }
}
