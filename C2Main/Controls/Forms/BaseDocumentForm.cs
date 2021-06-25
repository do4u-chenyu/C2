using C2.Core;
using C2.Model.Documents;
using System;
using System.ComponentModel;
using System.Drawing.Printing;

namespace C2.Controls
{
    public class BaseDocumentForm : BaseForm
    {
        string _Filename;
        bool _ReadOnly;

        public event EventHandler FilenameChanged;

        public BaseDocumentForm()
        {
        }

        [Browsable(false)]
        public string Filename
        {
            get { return _Filename; }
            set
            {
                if (_Filename != value)
                {
                    _Filename = value;
                    OnFilenameChanged();
                }
            }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                if (_ReadOnly != value)
                {
                    _ReadOnly = value;
                    OnReadOnlyChanged();
                }
            }
        }

        [Browsable(false)]
        public bool CanSave
        {
            // when not modified still can save
            get { return !ReadOnly && Document != null; }// && Document.Modified; }
        }

        [Browsable(false)]
        public virtual Document Document
        {
            get { return null; }
            set { }
        }

        public virtual DocumentTypeGroup[] GetExportDocumentTypes()
        {
            return new DocumentTypeGroup[0];
        }

        //public virtual void ExportDocument(string filename, string typeMime)
        //{
        //}

        public virtual bool Save()
        {
            return true;
        }

        public virtual bool SaveAs()
        {
            return true;
        }

        public bool Print()
        {
            return Print(null);
        }

        public virtual bool Print(PageSettings pageSettings)
        {
            return true;
        }

        public virtual bool PrintPreview()
        {
            return true;
        }

        protected virtual void OnFilenameChanged()
        {
            FilenameChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnReadOnlyChanged()
        {
        }

        //protected virtual void OnModifiedChanged()
        //{
        //    if (ModifiedChanged != null)
        //    {
        //        ModifiedChanged(this, EventArgs.Empty);
        //    }
        //}

        public virtual void AskSave(ref bool cancel)
        {
        }

        public virtual string GetFileName()
        {
            return null;
        }
    }
}
