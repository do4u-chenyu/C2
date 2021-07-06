﻿using C2.Business.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace C2.Model.Widgets
{
    class AttachmentWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "ATTACHMENT";

        [Browsable(false)]
        public List<string> AttachmentPaths { get; set; } = new List<string>();
        public override string Description => HelpUtil.AttachmentWidgetHelpInfo;

        public AttachmentWidget()
        {
            DisplayIndex = 4;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.附件;
        }
        public override string GetTypeID()
        {
            return TypeID;
        }
        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            XmlElement attachItems = node.OwnerDocument.CreateElement("attach_items");
            foreach (string attachPath in AttachmentPaths)
            {
                new ModelXmlWriter("attach_item", attachItems).WriteAttribute("path", attachPath);              
            }
            node.AppendChild(attachItems);

        }
        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            string path;
            var attach_items = node.SelectNodes("attach_items/attach_item");
            foreach (XmlElement attach_item in attach_items)
            {
                path = attach_item.GetAttribute("path");
                if (!string.IsNullOrEmpty(path))
                { 
                    AttachmentPaths.Add(path);
                }
                  
            }
        }

        public override void OnDoubleClick(HandledEventArgs e)
        {
            if (AttachmentPaths.Count > 0)
                DoOpenAttachment(AttachmentPaths[0]);
            base.OnDoubleClick(e);
        }

        public static void DoOpenAttachment(string ffp)
        {
            string[] ext = { ".doc", ".docx", ".xlsx", ".xls", ".pdf", ".xmind", ".txt", ".png", ".jpg" };

            if (File.Exists(ffp))
            {

                if (Array.IndexOf(ext, Path.GetExtension(ffp)) == -1)
                {
                    FileUtil.ExploreDirectory(ffp);
                }
                else
                    ProcessUtil.ProcessOpen(ffp);
            }
            else
                HelpUtil.ShowMessageBox("该文件已不存在.", "提示");
        }
    }
}
