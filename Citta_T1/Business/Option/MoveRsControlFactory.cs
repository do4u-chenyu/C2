using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;
using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Core;
using Citta_T1.Utils;
using System.Drawing;
using System.IO;

namespace Citta_T1.Business.Option
{
    class MoveRsControlFactory
    {
        private static MoveRsControlFactory MoveRsControlFactoryInstance;

        public static MoveRsControlFactory GetInstance()
        {
            if (MoveRsControlFactoryInstance == null)
            {
                MoveRsControlFactoryInstance = new MoveRsControlFactory();
            }
            return MoveRsControlFactoryInstance;
        }
        private MoveRsControl NewMoveRsControl(MoveOpControl moc, List<string> columnName)
        {
            //创建MoveRsControl控件
            char seperator = '\t';
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8;
            Point location = WorldBoundControl(moc);
            int id = Global.GetCurrentDocument().ElementCount;
            string createTime = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            string name = String.Format("L{0}_{1}",id, createTime);
            int sizeL = Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel;
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult( name, sizeL,
                                                                      location, seperator, encoding);
            //创建MoveRsControl的Xml文件
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(name + ".bcp", columnName);
            mrc.FullFilePath = path;
            return mrc;
        }
        private Point WorldBoundControl(MoveOpControl moc)
        {
            /*
             * 结果算子位置不超过地图大小
             */
            int x = moc.Location.X + moc.Width + 15;
            int y = moc.Location.Y;
            Point Pm = new Point(x,y);

            Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(Pm, true);

            if (Pw.X < 20)
            {
                Pm.X = 20;
            }
            if (Pw.Y < 70)
            {
                Pm.Y = 70;
            }
            if (Pw.X > 2000 - moc.Width)
            {
                Pm.X = moc.Location.X;
                Pm.Y = moc.Location.Y + moc.Height + 5;
            }
            if (Pw.Y > 980 - moc.Height)
            {
                Pm.Y = moc.Location.Y - moc.Height - 5;
            }
            return Pm;
        }
        public void CreateNewMoveRsControl(MoveOpControl moc, List<string> columnName)
        {

            /*
             * 创建新结果算子
             */
            MoveRsControl mrc= NewMoveRsControl(moc, columnName);

            /*
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */

            PointF startPoint = new PointF(
                   moc.RectOut.Location.X + moc.Location.X,
                   moc.RectOut.Location.Y + moc.Location.Y
                   );
            PointF endPoint = new PointF(mrc.Location.X + mrc.RectIn.Location.X, mrc.Location.Y + mrc.RectIn.Location.Y);
            Bezier line = new Bezier(startPoint, endPoint);
            CanvasPanel canvas = Global.GetCanvasPanel();

            canvas.RepaintObject(line);
            ModelRelation newModelRelation = new ModelRelation(
                                moc.ID, mrc.ID,
                                new Point(moc.RectOut.Location.X + moc.Location.X, moc.RectOut.Location.Y + moc.Location.Y),
                                new Point(mrc.RectIn.Location.X + mrc.Location.X, mrc.RectIn.Location.Y + mrc.Location.Y),
                                0);
            Global.GetCurrentDocument().AddModelRelation(newModelRelation);

            moc.OutPinInit("lineExit");
            mrc.rectInAdd(1);
           
        }
        public void CreateNewMoveRsControl(MoveOpControl moveOpControl, string path, char separator = '\t', DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {

            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            //string tmpBcpFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                System.IO.Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path)), Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel,
                new Point(x, y), separator, encoding);
            /*
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */

            PointF startPoint = new PointF(
                   moveOpControl.RectOut.Location.X + moveOpControl.Location.X,
                   moveOpControl.RectOut.Location.Y + moveOpControl.Location.Y
                   );
            PointF endPoint = new PointF(mrc.Location.X + mrc.RectIn.Location.X, mrc.Location.Y + mrc.RectIn.Location.Y);
            Bezier line = new Bezier(startPoint, endPoint);
            CanvasPanel canvas = Global.GetCanvasPanel();

            canvas.RepaintObject(line);
            ModelRelation newModelRelation = new ModelRelation(
                                moveOpControl.ID, mrc.ID,
                                new Point(moveOpControl.RectOut.Location.X + moveOpControl.Location.X, moveOpControl.RectOut.Location.Y + moveOpControl.Location.Y),
                                new Point(mrc.RectIn.Location.X + mrc.Location.X, mrc.RectIn.Location.Y + mrc.Location.Y),
                                0);
            Global.GetCurrentDocument().AddModelRelation(newModelRelation);

            moveOpControl.OutPinInit("lineExit");
            mrc.rectInAdd(1);
            mrc.FullFilePath = path;
        }
    }
}
