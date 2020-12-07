using C2.Core;
using C2.Forms;
using C2.WorkSpace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2.Controls
{
    public class DocumentManageForm : BaseForm
    {
        TaskBar _TaskBar;
        BaseForm _SelectedForm;
        MdiWorkSpace _MdiClient;

        public DocumentManageForm()
        {
            Forms = new List<BaseForm>();
        }

        public TaskBar TaskBar
        {
            get { return _TaskBar; }
            set
            {
                if (_TaskBar != value)
                {
                    _TaskBar = value;
                    OnTaskBarChanged();
                }
            }
        }

        public MdiWorkSpace MdiClient
        {
            get { return _MdiClient; }
            set 
            {
                if (_MdiClient != value)
                {
                    _MdiClient = value;
                    OnMdiClientChanged();
                }
            }
        }

        public BaseForm SelectedForm
        {
            get { return _SelectedForm; }
            private set 
            {
                if (_SelectedForm != value)
                {
                    var old = _SelectedForm;
                    _SelectedForm = value;
                    OnSelectedFormChanged(old);
                }
            }
        }

        public BaseDocumentForm SelectedDocumentForm
        {
            get
            {
                return SelectedForm as BaseDocumentForm;
            }
        }

        protected List<BaseForm> Forms { get; private set; }

        public IEnumerable<T> GetForms<T>()
            where T : BaseForm
        {
            return from f in Forms
                   where f is T
                   select (T)f;
        }

        protected string[] GetOpendDocuments()
        {
            var tabs = from f in GetForms<BaseDocumentForm>()
                       let fn = f.GetFileName()
                       where !string.IsNullOrEmpty(fn)
                       select fn;

            return tabs.ToArray();
        }
        public List<string> OpendDocuments()
        {
            char[] Chars = new char[] { ' ','*' };
            var tabs = from f in GetForms<BaseDocumentForm>()
                       let fn = f.Text
                       select fn.TrimEnd(Chars);

            return tabs.ToList();
        }
        private void OnTaskBarChanged()
        {
            if (TaskBar != null)
            {
                TaskBar.SelectedItemChanged += new EventHandler(TaskBar_SelectedItemChanged);
                TaskBar.ItemClose += new TabItemEventHandler(TaskBar_ItemClose);
            }
        }

        private void OnMdiClientChanged()
        {
            if (MdiClient != null)
            {
                IsMdiContainer = false;

                MdiClient.MdiFormActived += new EventHandler(MdiClient_MdiFormActived);
                MdiClient.MdiFormClosed += new EventHandler(MdiClient_MdiFormClosed);
            }
            else
            {
                IsMdiContainer = true;
            }
        }

        protected void ShowForm(BaseForm form)
        {
            ShowForm(form, true, true);
        }

        protected void SelectForm(BaseForm form)
        {
            if (form != null && Forms.Contains(form))
            {
                SelectedForm = form;
            }
        }
        public List<BaseDocumentForm> SearchDocument(string name)
        {
            try
            {
                var tabs = from f in GetForms<BaseDocumentForm>()
                           where string.Equals(Path.GetFileNameWithoutExtension(f.GetFileName()), name)
                           select f;
                return tabs.ToList();
            }
            catch
            {
                return new List<BaseDocumentForm>();
            }

        }

        public CanvasForm SearchCanvasForm(string path)
        {
            var cf = from f in GetForms<CanvasForm>()
                        where f.Document.SavePath == path
                        select f;
            List<CanvasForm> forms = cf.ToList();
            if (forms != null && forms.Count() > 0)
                return forms.First();
            else
                return null;
        }
        protected virtual void ShowForm(BaseForm form, bool showTab, bool canClose)
        {
            if (form == null)
                throw new ArgumentNullException();
            Global.GetWorkSpacePanel().SuspendLayout();
            if (showTab && TaskBar != null)
            {
                var ti = new TabItem();
                ti.Text = form.Text;
                ti.CanClose = canClose;
                ti.Tag = form;
                ti.ToolTipText = form.FormNameToolTip;
                if (form is BaseForm)
                    ti.Icon = form.IconImage;
                else
                    ti.Icon = PaintHelper.IconToImage(form.Icon);

                if (form is CanvasForm && !string.IsNullOrEmpty(form.FormNameToolTip))
                    TaskBar.Items.Insert(TaskBar.Items.IndexOf(TaskBar.SelectedItem) + 1, ti);
                else if (form is CanvasForm)
                    //模型市场的模型默认在最前面打开
                    TaskBar.Items.Insert(1, ti);
                else
                    TaskBar.Items.Add(ti);

                TaskBar.SelectedItem = ti;

            }

            form.TextChanged += new EventHandler(Form_TextChanged);
            form.Activated += new EventHandler(Form_Activated);
            form.FormClosed += new FormClosedEventHandler(Form_FormClosed);

            if (MdiClient != null)
            {
                MdiClient.ShowMdiForm(form);
            }
            else if (IsMdiContainer)
            {
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                //form.FormBorderStyle = FormBorderStyle.None;
                form.ControlBox = false;
                form.Show();
            }

            Global.GetWorkSpacePanel().ResumeLayout();
            if (!Forms.Contains(form))
            {
                Forms.Add(form);
            }
            
        }

        protected void ComfirmSaveDocuments(ref bool cancel)
        {
            var forms = GetForms<BaseDocumentForm>().ToArray();
            for (int i = forms.Length - 1; i >= 0; i--)
            {
                var form = forms[i];
                form.AskSave(ref cancel);
                if (cancel)
                    return;
            }
        }

        void TaskBar_SelectedItemChanged(object sender, EventArgs e)
        {
            if (TaskBar.SelectedItem != null)
            {
                Form form = TaskBar.SelectedItem.Tag as Form;
                if (form != null)
                {
                    if (MdiClient != null)
                        MdiClient.ActiveMdiForm(form);
                    else
                        form.Activate();
                }
            }
        }

        void TaskBar_ItemClose(object sender, TabItemEventArgs e)
        {
            if (e.Item != null && e.Item.Tag is Form)
            {
                if( MdiClient == null)
                    ((Form)e.Item.Tag).Close();
                else if(e.Item.Tag is DocumentForm)
                {
                    //不仅仅关闭当前form，关联form也要关掉
                    TabItem[] relateItem = TaskBar.Items.Where(ti=> ti.Tag is CanvasForm).Where(ti => ti.ToolTipText != null && ti.ToolTipText.StartsWith(e.Item.ToolTipText+"-")).ToArray();
                    foreach(TabItem ti in relateItem)
                    {
                        MdiClient.CloseMdiForm((Form)ti.Tag);
                    }
                    if(TaskBar.Items.Where(ti => ti.Tag is CanvasForm).Where(ti => ti.ToolTipText!=null && ti.ToolTipText.StartsWith(e.Item.ToolTipText+"-")).ToArray().Count() == 0)
                        MdiClient.CloseMdiForm((Form)e.Item.Tag);
                }
                else
                    MdiClient.CloseMdiForm((Form)e.Item.Tag);                    
            }
        }

        void MdiClient_MdiFormClosed(object sender, EventArgs e)
        {
            Form_FormClosed(sender, new FormClosedEventArgs(CloseReason.MdiFormClosing));
        }

        void MdiClient_MdiFormActived(object sender, EventArgs e)
        {
            SelectedForm = MdiClient.ActivedMdiForm as BaseForm;

            Form_Activated(sender, e);
        }

        void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is BaseForm)
            {
                var form = (BaseForm)sender;
                if (TaskBar != null)
                {
                    TabItem ti = TaskBar.GetItemByTag(form);
                    if (ti != null)
                    {
                        TaskBar.Items.Remove(ti);
                    }
                }

                if (form == SelectedForm)
                {
                    SelectedForm = null;
                }

                if (Forms.Contains(form))
                {
                    Forms.Remove(form);
                }
            }
        }

        void Form_Activated(object sender, EventArgs e)
        {
            if (sender is Form && TaskBar != null)
            {
                Form form = (Form)sender;
                TaskBar.SelectByTag(form);
                if (sender is C2.Forms.CanvasForm)
                    (sender as C2.Forms.CanvasForm).BlankButtonFocus();
            }
        }

        void Form_TextChanged(object sender, EventArgs e)
        {
            if (sender is Form && TaskBar != null)
            {
                Form form = (Form)sender;
                TabItem item = TaskBar.GetItemByTag(form);
                if (item != null)
                {
                    item.Text = form.Text;
                }
            }
        }

        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);

            SelectedForm = ActiveMdiChild as BaseForm;
        }

        protected virtual void OnSelectedFormChanged(BaseForm old)
        {
            if (MdiClient != null)
            {
               
                MdiClient.ActiveMdiForm(SelectedForm);
                Global.GetBottomViewPanel().Visible = !(Global.GetMainForm().MdiClient.ActivedMdiForm is StartForm);
            }
        }
    }
}
