using System;
using Citta_T1.Core;
using Citta_T1.Model;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Controls.MapViews
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
