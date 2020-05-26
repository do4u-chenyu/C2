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
    class CreateMoveRsControl
    {
        public void CreateResultControl(MoveOpControl moveOpControl, List<string> columnName, char seperator = '\t', DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            string tmpBcpFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                System.IO.Path.GetFileNameWithoutExtension(tmpBcpFileName),
                new Point(x, y), seperator, encoding);

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
            string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpBcpFileName, columnName);
            mrc.FullFilePath = path;
        }
        public void CreateResultControlCustom(MoveOpControl moveOpControl, string path, char separator = '\t', DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                if (mr.StartID == moveOpControl.ID) return;
            int x = moveOpControl.Location.X + moveOpControl.Width + 15;
            int y = moveOpControl.Location.Y;
            //string tmpBcpFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                System.IO.Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path)),
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
