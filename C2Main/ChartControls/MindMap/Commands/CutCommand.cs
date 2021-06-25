using System;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class CutCommand : Command
    {
        ChartObject[] ChartObjects;
        DeleteCommand DeleteCommand;

        public CutCommand(ChartObject[] chartObjects)
        {
            ChartObjects = chartObjects;

            if (ChartObjects == null)
                throw new ArgumentNullException();
        }

        public override string Name
        {
            get { return "Cut"; }
        }

        public override bool Rollback()
        {
            if (this.DeleteCommand != null)
                return this.DeleteCommand.Rollback();
            else
                return false;
        }
        public override bool Redo()
        {
            return Execute();
        }
        public override bool Execute()
        {
            var copyCommand = new CopyCommand(ChartObjects, true);
            copyCommand.Execute();

            DeleteCommand = new DeleteCommand(ChartObjects);
            DeleteCommand.Execute();

            return true;
        }
    }
}
