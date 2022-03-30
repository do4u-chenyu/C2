﻿using C2.Business.CastleBravo.Binary;
using C2.Business.CastleBravo.Intruder;
using C2.Business.CastleBravo.PwdGenerator;
using C2.Business.CastleBravo.RobotsScan;
using C2.Business.CastleBravo.VPN;
using C2.Business.CastleBravo.WebScan;
using C2.Business.CastleBravo.WebShellTool;
using C2.Business.Cracker.Dialogs;
using C2.Core;
using C2.Globalization;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    class PluginButton : BaseLeftInnerButton
    {
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem JumpMindMapMenuItem;

        private readonly Dictionary<string, string> JST;

        private readonly string pluginType;
        public PluginButton(string name)
        {
            pluginType = name;
            InitButtonMenu();
            InitButtonType();
            InitButtonDoubleClick();
            JST = new Dictionary<string, string>()
            {
                {"涉赌专项", "涉赌模型" },
                {"涉枪专项", "涉枪模型" },
                {"涉黄专项", "涉黄模型" },
                {"盗洞专项", "盗洞模型" },
                {"肉鸡黑吃黑", "肉鸡黑吃黑模型" },
                {"境外网产专项", "购置境外网络资产模型" },
            };
        }

        private void InitButtonType()
        {
            ButtonText = Lang._(this.pluginType);
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            switch (this.pluginType)
            {
                case "Cracker":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.cracker;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.CrackerFormHelpInfo);
                    break;
                case "PwdGenerator":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.dictGenerator;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.PwdGeneratorHelpInfo);
                    break;
                case "WebScan":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.WebScan;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.WebScanHelpInfo);
                    break;
                case "RobotsScan":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Robots;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.RobotsScanHelpInfo);
                    break;
                case "WebShell":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.webshell;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.WebShellHelpInfo);
                    break;
                case "Binary":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.二进制;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.BinaryHelpInfo);
                    break;
                case "Intruder":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.intruder;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.IntruderHelpInfo);
                    break;
                case "VPN":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.v2rayN.ToBitmap();
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.VPNHelpoInfo);
                    break;
                case "涉赌专项":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.db;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DBFormHelpInfo);
                    break;
                case "涉枪专项":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.sq;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.SQFormHelpInfo);
                    break;
                case "涉黄专项":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.sh;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.SHFormHelpInfo);
                    break;
                case "盗洞专项":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.dd;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DDFormHelpInfo);
                    break;
                case "肉鸡黑吃黑":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.HM;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.XiseBackdoorHelpInfo);
                    break;
                case "境外网产专项":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.WC;
                    this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.NetworkAssetsHelpInfo);
                    break;
            }
        }
        private void InitButtonMenu()
        {
            contextMenuStrip.Opening += ContextMenuStrip_Opening;

            OpenToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "OpenToolStripMenuItem",
                Text = "打开"
            };
            OpenToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);

            JumpMindMapMenuItem = new ToolStripMenuItem
            {
                Name = "OpenMindMapMenuItem",
                Text = "跳转战术手册",
            };
            JumpMindMapMenuItem.Click += new EventHandler(OpenMindMapMenuItem_Click);

            contextMenuStrip.Items.Add(OpenToolStripMenuItem);
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuStrip.Items.Remove(JumpMindMapMenuItem);
            bool exs = JST.ContainsKey(pluginType) && Exists(JST[pluginType]);
            if (exs)
                contextMenuStrip.Items.Add(JumpMindMapMenuItem);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPluginForm();
        }

        private void OpenMindMapMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetManualControl().TryOpen(JST[pluginType]);
        }

        private bool Exists(string modelTitle)
        {
            return Global.GetManualControl().ContainModel(modelTitle);
        }

        private void InitButtonDoubleClick()
        {
            this.noFocusButton.MouseDown += new MouseEventHandler(this.NoFocusButton_MouseDown);
        }
        private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                OpenPluginForm();
        }

        private void OpenPluginForm()
        {
            switch (pluginType)
            {
                case "Cracker":
                    new CrackerForm().ShowDialog();
                    break;
                case "PwdGenerator":
                    new PwdGeneratorForm().ShowDialog();
                    break;
                case "WebScan":
                    new WebScanForm().ShowDialog();
                    break;
                case "RobotsScan":
                    new RobotsScan().ShowDialog();
                    break;
                case "WebShell":
                    new WebShellManageForm().ShowDialog();
                    break;
                case "Binary":
                    new BinaryMainForm().ShowDialog();
                    break;
                case "Intruder":
                    new IntruderForm().ShowDialog();
                    break;
                case "VPN":
                    new VPNMainForm().ShowDialog();
                    break;
                case "涉赌专项":
                case "涉枪专项":
                case "涉黄专项":
                case "盗洞专项":
                case "肉鸡黑吃黑":
                case "境外网产专项":
                    Global.GetMainForm().OpenJSTab(pluginType);
                    break;
                default:
                    break;
                   
            }
        }
    }
}
