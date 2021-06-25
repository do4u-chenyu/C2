using C2.Model;
using C2.Model.Documents;

namespace C2.Controls.MapViews
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

        public abstract bool Redo();

        public override string ToString()
        {
            return Name;
        }
    }
}
