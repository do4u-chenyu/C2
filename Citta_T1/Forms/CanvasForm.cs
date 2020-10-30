using C2.Business.Model;
using C2.Business.Option;
using C2.Business.Schedule;
using C2.Controls;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Move;
using C2.Controls.Move.Dt;
using C2.Controls.Move.Op;
using C2.Controls.Top;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace C2.Forms
{
    public partial class CanvasForm : BaseForm
    {
        public ModelDocument Document { get { return this.document; } }
        public CanvasPanel CanvasPanel{ get { return this.canvasPanel; }}
        public RemarkControl RemarkControl { get { return this.remarkControl; } }
        public FlowControl FlowControl { get { return this.flowControl; } }
        public OperatorControl OperatorControl { get { return this.operatorControl; } }
        public OptionDao OptionDao { get { return this.optionDao; } }
        public ModelDocumentDao ModelDocumentDao { get { return this.modelDocumentDao; } }
        public NaviViewControl NaviViewControl { get { return this.naviViewControl; } }
        private OptionDao optionDao;
        private ModelDocument document;
        private ModelDocumentDao modelDocumentDao;
        private readonly string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        private string userName;
        #region 运行委托
        delegate void AsynUpdateLog(string logContent);
        delegate void AsynUpdateGif();
        delegate void TaskCallBack();
        delegate void AsynUpdateProgressBar();
        delegate void AsynUpdateMask();
        delegate void AsynUpdateOpErrorMessage();
        #endregion
        private static LogUtil log = LogUtil.GetInstance("CanvasForm-i"); // 获取日志模块
        public CanvasForm(ModelDocument modelDoc)
        {
            InitializeComponent();
            this.document = modelDoc;
            this.canvasPanel.Document = modelDoc;
            this.modelDocumentDao = new ModelDocumentDao(modelDoc);
            this.optionDao = new OptionDao();
            this.userName = Global.GetMainForm().UserName;
            InitializeMainFormEventHandler();
        }

        public TopToolBarControl TopToolBarControl { get { return this.topToolBarControl; } }
        #region C1文档切换
        private void InitializeMainFormEventHandler()
        {
            // 新增文档事件
            //this.modelTitlePanel.NewModelDocument += ModelTitlePanel_NewModelDocument;
            //this.modelTitlePanel.ModelDocumentSwitch += ModelTitlePanel_DocumentSwitch;
            this.canvasPanel.NewElementEvent += NewDocumentOperator;
            //this.remarkControl.RemarkChangeEvent += RemarkChange;
        }
        private void NewDocumentOperator(MoveBaseControl ct)
        {
            ModelElement me = this.modelDocumentDao.AddDocumentOperator(ct);
            SetDocumentDirty();
            if (ct is MoveDtControl || ct is MoveOpControl)
            {
                BaseCommand cmd = new ElementAddCommand(me);
                UndoRedoManager.GetInstance().PushCommand(this.modelDocumentDao.CurrentDocument, cmd);
            }
        }
        public void SetDocumentDirty()
        {
            // 已经为dirty了，就不需要再操作了，以提高性能
            // TODO 采用Blumind的Dirty逻辑
            //if (this.modelDocumentDao.CurrentDocument.Dirty)
            //    return;
            //this.modelDocumentDao.CurrentDocument.Dirty = true;
            //string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelTitle;
            //this.canvasPanel.ModelTitlePanel.ResetDirtyPictureBox(currentModelTitle, true);
        }
        #endregion


        private void CanvasForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeControlsLocation();
        }
        public void InitializeControlsLocation()
        {
            int x = this.canvasPanel.Width - 10 - this.naviViewControl.Width;
            int y = this.canvasPanel.Height - 5 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.resetButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location = new Point(x, y + 50);

            //运行状态动图、进度条定位
            this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.progressBar.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            this.flowControl.Location = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 35);
            this.operatorControl.Location = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 90);
            this.remarkControl.Location = new Point(this.canvasPanel.Width - 205 - this.flowControl.Width, this.flowControl.Height - 15);
            this.rightShowButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width, 35);
            this.rightHideButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width, 35 + this.rightHideButton.Width);

            //// 右上用户名，头像
            //int count = System.Text.RegularExpressions.Regex.Matches(userName, "[a-z0-9]").Count;
            //int rightMargin = (this.userName.Length - (count / 3) - 3) * 14;
            //this.usernamelabel.Text = this.userName;
            //Point userNameLocation = new Point(185, 10);
            //this.usernamelabel.Location = new Point(userNameLocation.X + 65 - rightMargin, userNameLocation.Y + 2);
            //this.helpPictureBox.Location = new Point(userNameLocation.X - rightMargin, userNameLocation.Y + 1);
            //this.portraitpictureBox.Location = new Point(userNameLocation.X + 30 - rightMargin, userNameLocation.Y + 1);
        }
        #region 运行相关部分
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //重置前打断框选、选中线
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;

            //在模型运行完成，及终止的情况下，可以重置
            //Console.WriteLine(currentManager.ModelStatus.ToString());
            if (currentManager.ModelStatus != ModelStatus.GifDone && currentManager.ModelStatus != ModelStatus.Pause && currentManager.ModelStatus != ModelStatus.Running)
            {
                currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument(), "all");
                currentManager.Reset();
                //SetDocumentDirty();//需不需要dirty
                MessageBox.Show("当前模型的运算结果已重置，点击‘运行’可以重新运算了", "已重置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void StopButton_Click(object sender, EventArgs e)
        {
            //终止前打断框选、选中线
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            if (this.runButton.Name == "pauseButton" || this.runButton.Name == "continueButton")
            {
                Global.GetCurrentDocument().TaskManager.Stop();
                UpdateRunbuttonImageInfo();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            //运算前打断框选、选中线
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
            BindUiManagerFunc();

            if (this.runButton.Name == "runButton")
            {
                if (Global.GetCurrentDocument().Dirty)
                {
                    MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument(), "all");
                //int notReadyNum = currentManager.CountOpStatus(ElementStatus.Null);
                int notReadyNum = currentManager.CountOpNullAndNoRelation();
                if (notReadyNum > 0)
                {
                    MessageBox.Show("有" + notReadyNum + "个未配置的算子，请配置后再运行模型", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (currentManager.IsAllOperatorDone())
                {
                    MessageBox.Show("当前模型的算子均已运算完毕，重新运算需要先点击‘重置’按钮。", "运算完毕", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.Start();
                int taskNum = currentManager.CountOpStatus(ElementStatus.Ready);
                this.progressBar.Step = taskNum > 0 ? 100 / taskNum : 100;

                this.progressBar.Value = 0;
                this.progressBarLabel.Text = "0%";
            }
            else if (this.runButton.Name == "pauseButton")
            {
                currentManager.Pause();
            }
            else if (this.runButton.Name == "continueButton")
            {
                currentManager.Continue();
            }

            UpdateRunbuttonImageInfo();
        }

        public void BindUiManagerFunc()
        {
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
            //初次运行时，绑定线程与ui交互的委托
            if (currentManager.ModelStatus == ModelStatus.Null)
            {
                currentManager.UpdateLogDelegate = UpdateLogStatus;
                currentManager.TaskCallBack = Accomplish;
                currentManager.UpdateGifDelegate = UpdateRunningGif;
                currentManager.UpdateBarDelegate = UpdateProgressBar;
                currentManager.UpdateOpErrorDelegate = UpdateOpErrorMessage;
                currentManager.UpdateMaskDelegate = EnableRunningControl;
            }
        }

        //更新op算子错误信息
        private void UpdateOpErrorMessage(TaskManager manager, int id, string error)
        {
            this.Invoke(new AsynUpdateOpErrorMessage(delegate ()
            {
                ModelDocument model = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
                MoveOpControl op = model.SearchElementByID(id).InnerControl as MoveOpControl;
                op.SetStatusBoxErrorContent(error);
            }));
        }

        //更新进度条
        private void UpdateProgressBar(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel != Global.GetCurrentDocument())
                return;


            if (manager.ModelStatus == ModelStatus.Running)
                this.Invoke(new AsynUpdateProgressBar(delegate ()
                {
                    this.progressBar.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleListGen.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar.Value.ToString() + "%";
                }));
        }


        //更新log
        private void UpdateLogStatus(string logContent)
        {
            this.Invoke(new AsynUpdateLog(delegate (string tlog)
            {
                log.Info(tlog);
            }), logContent);
        }


        private void UpdateRunningGif(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel != Global.GetCurrentDocument())
                return;

            if (manager.ModelStatus == ModelStatus.GifDone)
                this.Invoke(new AsynUpdateGif(delegate ()
                {
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Show();
                }));
            else if (manager.ModelStatus == ModelStatus.Done)
                this.Invoke(new AsynUpdateGif(delegate ()
                {
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    this.currentModelFinLab.Hide();
                }));

        }

        //完成任务时需要调用
        private void Accomplish(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            doneModel.Save();
            if (doneModel == Global.GetCurrentDocument())
            {
                this.Invoke(new TaskCallBack(delegate ()
                {
                    UpdateRunbuttonImageInfo();
                }));

            }
        }

        //更新状态的节点：1、当前模型开始、终止、运行完成；2、切换文档
        public void UpdateRunbuttonImageInfo()
        {
            TaskManager manager = Global.GetCurrentDocument().TaskManager;
            switch (manager.ModelStatus)
            {
                //点击暂停按钮，均隐藏
                case ModelStatus.Pause:
                    this.runButton.Name = "continueButton";
                    this.runButton.Image = global::C2.Properties.Resources.continual;
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    EnableRunningRsControl();
                    break;
                //点击运行按钮
                case ModelStatus.Running:
                    this.runButton.Name = "pauseButton";
                    //this.runButton.Image = global::C2.Properties.Resources.pause;
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = false;//暂时隐去暂停功能
                    this.currentModelRunBackLab.Show();
                    this.currentModelRunLab.Show();
                    this.progressBar.Show();
                    this.progressBarLabel.Show();
                    this.progressBar.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleListGen.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar.Value.ToString() + "%";
                    UnEnableRunningControl();
                    break;
                case ModelStatus.GifDone:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = true;//暂时隐去暂停功能
                    break;
                default:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = true;//暂时隐去暂停功能
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Hide();
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    EnableRunningControl();
                    break;
            }
        }

        private void UnEnableRunningControl()
        {
            //禁止的控件
            /*
             * 1、当前模型的element
             * 2、右上方菜单栏
             * 3、左侧菜单栏
             */
            Global.GetCurrentDocument().UnEnable();
            EnableCommonControl(false);
        }
        private void EnableRunningControl()
        {
            Global.GetCurrentDocument().Enable();
            EnableCommonControl(true);
        }
        private void EnableRunningRsControl()
        {
            Global.GetCurrentDocument().EnableRs();
            EnableCommonControl(false);
        }
        private void EnableRunningControl(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            this.Invoke(new AsynUpdateMask(delegate ()
            {
                doneModel.Enable();
                EnableCommonControl(true);
            }));
        }

        private void EnableCommonControl(bool status)
        {
            this.topToolBarControl.Enabled = status;
            this.operatorControl.Enabled = status;
            Global.GetLeftToolBoxPanel().Enabled = status;
            this.flowControl.Enabled = status;
        }
        #endregion

        #region C1文档关闭
        private void CanvasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.modelDocumentDao.SaveEndDocuments(this.userName);
        }

        private void CanvasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ModelDocument md in this.modelDocumentDao.ModelDocuments)
            {
                if (!md.Dirty)
                    continue;
                DialogResult result = MessageBox.Show("有尚未保存的模型，是否保存后关闭？",
                                               "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                // 取消操作
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                // 保存文件
                if (result == DialogResult.Yes)
                {
                    foreach (ModelDocument modelDocument in this.modelDocumentDao.ModelDocuments)
                        modelDocument.Save();
                    return;
                }
                // 不保存关闭文件
                if (result == DialogResult.No)
                    return;
            }
        }
        #endregion

        #region UndoRedo
        private void UpdateUndoRedoButton()
        {
            //topToolBarControl.SetUndoButtonEnable(!UndoRedoManager.GetInstance().IsUndoStackEmpty(modelDocumentDao.CurrentDocument));
            //topToolBarControl.SetRedoButtonEnable(!UndoRedoManager.GetInstance().IsRedoStackEmpty(modelDocumentDao.CurrentDocument));
        }
        #endregion

        #region 文档加载
        public void LoadDocument(string modelTitle)
        {
            // TODO 就不该有多个doc
            this.modelDocumentDao.CurrentDocument = new ModelDocument(modelTitle, userName);
            this.modelDocumentDao.CurrentDocument.Load();
            this.Show();
            //this.modelTitlePanel.AddModel(modelTitle);
            //this.modelDocumentDao.CurrentDocument.Load();
            //this.modelDocumentDao.CurrentDocument.ReCountDocumentMaxElementID();
            //this.modelDocumentDao.CurrentDocument.Show();
            //this.modelDocumentDao.CurrentDocument.Dirty = false;
            //CanvasAddElement(this.modelDocumentDao.CurrentDocument);
            //// 加载文档时，需要暂时关闭remark的TextChange事件
            //this.remarkControl.RemarkChangeEvent -= RemarkChange;
            //this.remarkControl.RemarkDescription = this.modelDocumentDao.RemarkDescription;
            //this.remarkControl.RemarkChangeEvent += RemarkChange;
        }
        #endregion
        #region 被干掉的方法
        //private void RemarkChange(RemarkControl rc)
        //{
        //    SetDocumentDirty();
        //    this.modelDocumentDao.UpdateRemark(rc);
        //}
        #endregion
    }
}
