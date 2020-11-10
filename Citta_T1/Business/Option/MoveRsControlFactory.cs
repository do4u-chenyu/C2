using C2.Business.Model;
using C2.Controls.Move.Op;
using C2.Controls.Move.Rs;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace C2.Business.Option
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
            char separator = OpUtil.DefaultSeparator;
            OpUtil.Encoding encoding = OpUtil.Encoding.UTF8;
            Point location = Global.GetCurrentModelDocument().WorldMap.WorldBoundRSControl(moc);
            int id = Global.GetCurrentModelDocument().ElementCount;
            int sizeL = Global.GetCurrentModelDocument().WorldMap.SizeLevel;

            if (resultFilePath == string.Empty)
                path = String.Format("L{0}_{1}.bcp", id, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            else
                path = resultFilePath;
            string name = Path.GetFileNameWithoutExtension(path);

            MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(
                                name, sizeL, location, separator, encoding);

            //创建MoveRsControl的结果BCP文件
            if (resultFilePath == string.Empty)
                path = BCPBuffer.GetInstance().CreateNewBCPFile(path, columns);

            mrc.FullFilePath = path;
            return mrc;
        }
        private void NewLineOpControlToRsControl(MoveOpControl moc, MoveRsControl mrc)
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
            Global.GetCurrentModelDocument().AddModelRelation(newModelRelation);
            Global.GetMainForm().SetDocumentDirty();
            moc.OutPinInit("lineExit");
            mrc.RectInAdd(1);
        }
        public void CreateNewMoveRsControl(MoveOpControl moc, List<string> columnName)
        {

            /*
             * 创建新结果算子
             */
            MoveRsControl mrc = NewMoveRsControl(moc, string.Empty, columnName);

            /*
             * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
             * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
             */

            NewLineOpControlToRsControl(moc, mrc);

            // UndoRedo
            List<Tuple<int, int, int>> relations = new List<Tuple<int, int, int>>();
            ModelDocument doc = Global.GetCurrentModelDocument();
            ModelElement me = Global.GetCurrentModelDocument().SearchElementByID(mrc.ID);
            ModelElement opEle = null;
            foreach (ModelRelation mr in doc.ModelRelations.Where(t => t.EndID == me.ID))
            {
                relations.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                opEle = doc.SearchElementByID(mr.StartID);
            }
            BaseCommand cmd = new ElementAddCommand(me, relations, opEle);
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentModelDocument(), cmd);
        }
        public void CreateNewMoveRsControl(MoveOpControl moc, string path)
        {
            MoveRsControl mrc = NewMoveRsControl(moc, path, new List<string>());
            NewLineOpControlToRsControl(moc, mrc);

            // UndoRedo
            List<Tuple<int, int, int>> relations = new List<Tuple<int, int, int>>();
            ModelDocument doc = Global.GetCurrentModelDocument();
            ModelElement me = Global.GetCurrentModelDocument().SearchElementByID(mrc.ID);
            ModelElement opEle = null;
            foreach (ModelRelation mr in doc.ModelRelations.Where(t => t.EndID == me.ID))
            {
                relations.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                opEle = doc.SearchElementByID(mr.StartID);
            }
            BaseCommand cmd = new ElementAddCommand(me, relations, opEle);
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentModelDocument(), cmd);
        }
    }
}
