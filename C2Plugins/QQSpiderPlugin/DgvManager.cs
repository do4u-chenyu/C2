using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    internal class DgvManager
    {
        const int imgSize = 50;
        private DataGridView dataGridView;

        public DgvManager(DataGridView dataGridView1)
        {
            this.dataGridView = dataGridView1;
            Init();
        }
        public void Init()
        {
            DgvUtil.CleanDgv(this.dataGridView);
            DgvUtil.ResetColumnsWidth(this.dataGridView, imgSize, false);
            DgvUtil.DisableOrder(this.dataGridView);
            this.dataGridView.RowTemplate.Height = imgSize;
            this.dataGridView.AllowUserToAddRows = false;
        }

        public void AppendLine(string line)
        {
            String[] fileds = line.Split('\t');
            int index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = GetImage(fileds[fileds.Length - 1]);

            for (int i = 1; i < this.dataGridView.Columns.Count; i++)
            {
                this.dataGridView.Rows[index].Cells[i].Value = fileds[i - 1];
                this.dataGridView.Update();
            }
        }

        private Image GetImage(string url)
        {
            byte[] data = new byte[0];
            Image img;
            try
            {
                using (WebClient client = new WebClient())
                {
                    data = client.DownloadData(url);
                    using (MemoryStream mem = new MemoryStream(data))
                        img = Image.FromStream(mem);
                }
            }
            catch(Exception)
            {
                img = (Image)Properties.Resources.not_found;
            }
            return Util.ResizeImage(img, new Size(imgSize, imgSize));
        }
    }
}