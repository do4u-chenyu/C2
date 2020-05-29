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
        private MoveRsControl NewMoveRsControl(MoveOpControl moc, string resultFilePath, List<string> columns) 
        {
            //创建MoveRsControl控件
            string path;
            char separator = '\t';
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8;
            Point location = WorldBoundControl(moc);
            int id = Global.GetCurrentDocument().ElementCount;
            string createTime = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            int sizeL = Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel;
            
            if (resultFilePath == string.Empty)
                path = String.Format("L{0}_{1}.bcp", id, createTime);
            else
                path = resultFilePath;
            string name = Path.GetFileNameWithoutExtension(path);

            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                                name, sizeL,location, separator, encoding);

            //创建MoveRsControl的Xml文件
            if (resultFilePath == string.Empty)
                path = BCPBuffer.GetInstance().CreateNewBCPFile(path, columns);

            mrc.FullFilePath = path;
            return mrc;
        }
        private Point WorldBoundControl(MoveOpControl moc)
        {
            /*
             * 结果算子位置不超过地图右边界、下边界
             */
            
            int rightBorder = 2000 - 2 * moc.Width;
            int lowerBorder = 980 - moc.Height;
            int interval = moc.Height + 5;

            Point Pm = new Point(moc.Location.X + moc.Width + 25, moc.Location.Y);
            Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(Pm, true);

            if (Pw.X > rightBorder)
            {
                Pm.X = moc.Location.X;
                Pm.Y = moc.Location.Y + interval;
            }
            if (Pw.Y > lowerBorder)
            {
                Pm.Y = moc.Location.Y - interval;
            }
            return Pm;
        }
        private void NewLineOpControlToRsControl(MoveOpControl moc,MoveRsControl mrc) 
        {
            int startX = moc.RectOut.Location.X + moc.Location.X;
            int startY = moc.RectOut.Location.Y + moc.Location.Y;
            int endX = mrc.Location.X + mrc.RectIn.Location.X;
            int endY = mrc.Location.Y + mrc.RectIn.Location.Y;
            PointF startPoint = new PointF(startX, startY);
            PointF endPoint = new PointF(endX, endY);

            Bezier line = new Bezier(startPoint, endPoint);
            Global.GetCanvasPanel().RepaintObject(line);
            ModelRelation newModelRelation = new ModelRelation(
                                moc.ID, mrc.ID,
                                startPoint,
                                endPoint,
                                0);
            Global.GetCurrentDocument().AddModelRelation(newModelRelation);
            moc.OutPinInit("lineExit");
            mrc.rectInAdd(1);
        }
        public void CreateNewMoveRsControl(MoveOpControl moc, List<string> columnName)
        {

            /*
             * 创建新结果算子
             */
            MoveRsControl mrc= NewMoveRsControl(moc, string.Empty, columnName);

            /*
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */

            NewLineOpControlToRsControl(moc, mrc);

        }
        public void CreateNewMoveRsControl(MoveOpControl moc, string path)
        {

            MoveRsControl mrc = NewMoveRsControl(moc, path, null);
            NewLineOpControlToRsControl(moc, mrc);
        }
    }
}
