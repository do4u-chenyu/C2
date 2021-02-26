namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectMapDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.datasource = new System.Windows.Forms.Label();
            this.mapType = new System.Windows.Forms.Label();
            this.lat = new System.Windows.Forms.Label();
            this.lon = new System.Windows.Forms.Label();
            this.datasourceComboBox = new System.Windows.Forms.ComboBox();
            this.mapTypeComboBox = new System.Windows.Forms.ComboBox();
            this.latComboBox = new System.Windows.Forms.ComboBox();
            this.lonComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.countComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // datasource
            // 
            this.datasource.AutoSize = true;
            this.datasource.Location = new System.Drawing.Point(80, 20);
            this.datasource.Name = "datasource";
            this.datasource.Size = new System.Drawing.Size(53, 12);
            this.datasource.TabIndex = 10003;
            this.datasource.Text = "数据源：";
            // 
            // mapType
            // 
            this.mapType.AutoSize = true;
            this.mapType.Location = new System.Drawing.Point(68, 65);
            this.mapType.Name = "mapType";
            this.mapType.Size = new System.Drawing.Size(65, 12);
            this.mapType.TabIndex = 10004;
            this.mapType.Text = "地图类型：";
            // 
            // lat
            // 
            this.lat.AutoSize = true;
            this.lat.Location = new System.Drawing.Point(92, 110);
            this.lat.Name = "lat";
            this.lat.Size = new System.Drawing.Size(41, 12);
            this.lat.TabIndex = 10005;
            this.lat.Text = "经度：";
            // 
            // lon
            // 
            this.lon.AutoSize = true;
            this.lon.Location = new System.Drawing.Point(92, 155);
            this.lon.Name = "lon";
            this.lon.Size = new System.Drawing.Size(41, 12);
            this.lon.TabIndex = 10006;
            this.lon.Text = "纬度：";
            // 
            // datasourceComboBox
            // 
            this.datasourceComboBox.FormattingEnabled = true;
            this.datasourceComboBox.Location = new System.Drawing.Point(151, 17);
            this.datasourceComboBox.Name = "datasourceComboBox";
            this.datasourceComboBox.Size = new System.Drawing.Size(189, 20);
            this.datasourceComboBox.TabIndex = 10007;
            this.datasourceComboBox.SelectedIndexChanged += new System.EventHandler(this.datasourceComboBox_SelectedIndexChanged);
            // 
            // mapTypeComboBox
            // 
            this.mapTypeComboBox.FormattingEnabled = true;
            this.mapTypeComboBox.Items.AddRange(new object[] {
            "标注图",
            "轨迹图",
            "区域图",
            "热力图"});
            this.mapTypeComboBox.Location = new System.Drawing.Point(151, 62);
            this.mapTypeComboBox.Name = "mapTypeComboBox";
            this.mapTypeComboBox.Size = new System.Drawing.Size(189, 20);
            this.mapTypeComboBox.TabIndex = 10008;
            this.mapTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.mapTypeComboBox_SelectedIndexChanged);
            // 
            // latComboBox
            // 
            this.latComboBox.FormattingEnabled = true;
            this.latComboBox.Location = new System.Drawing.Point(151, 102);
            this.latComboBox.Name = "latComboBox";
            this.latComboBox.Size = new System.Drawing.Size(189, 20);
            this.latComboBox.TabIndex = 10009;
            // 
            // lonComboBox
            // 
            this.lonComboBox.FormattingEnabled = true;
            this.lonComboBox.Location = new System.Drawing.Point(151, 147);
            this.lonComboBox.Name = "lonComboBox";
            this.lonComboBox.Size = new System.Drawing.Size(189, 20);
            this.lonComboBox.TabIndex = 10010;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10011;
            this.label1.Text = "权重：";
            // 
            // countComboBox
            // 
            this.countComboBox.Enabled = false;
            this.countComboBox.FormattingEnabled = true;
            this.countComboBox.Location = new System.Drawing.Point(151, 192);
            this.countComboBox.Name = "countComboBox";
            this.countComboBox.Size = new System.Drawing.Size(189, 20);
            this.countComboBox.TabIndex = 10012;
            // 
            // SelectMapDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 270);
            this.Controls.Add(this.countComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lonComboBox);
            this.Controls.Add(this.latComboBox);
            this.Controls.Add(this.mapTypeComboBox);
            this.Controls.Add(this.datasourceComboBox);
            this.Controls.Add(this.lon);
            this.Controls.Add(this.lat);
            this.Controls.Add(this.mapType);
            this.Controls.Add(this.datasource);
            this.Name = "SelectMapDialog";
            this.Text = "SelectMapDialog";
            this.Controls.SetChildIndex(this.datasource, 0);
            this.Controls.SetChildIndex(this.mapType, 0);
            this.Controls.SetChildIndex(this.lat, 0);
            this.Controls.SetChildIndex(this.lon, 0);
            this.Controls.SetChildIndex(this.datasourceComboBox, 0);
            this.Controls.SetChildIndex(this.mapTypeComboBox, 0);
            this.Controls.SetChildIndex(this.latComboBox, 0);
            this.Controls.SetChildIndex(this.lonComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.countComboBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label datasource;
        private System.Windows.Forms.Label mapType;
        private System.Windows.Forms.Label lat;
        private System.Windows.Forms.Label lon;
        private System.Windows.Forms.ComboBox datasourceComboBox;
        private System.Windows.Forms.ComboBox mapTypeComboBox;
        private System.Windows.Forms.ComboBox latComboBox;
        private System.Windows.Forms.ComboBox lonComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox countComboBox;
    }
}