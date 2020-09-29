using Citta_T1.Model;
using Citta_T1.Model.Documents;

namespace Citta_T1.Controls.MapViews
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public virtual bool NoteHistory
        {
            get { return true; }
        }

        public ChartObject[] AfterSelection { get; set; }

        public abstract bool Rollback();

        public abstract bool Execute();

        public override string ToString()
        {
            return Name;
        }
    }
}
