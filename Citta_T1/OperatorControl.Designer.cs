namespace Citta_T1
{
    partial class OperatorControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorControl));
            this.OperatorConnectButton = new System.Windows.Forms.Button();
            this.OperatorGetIntersect = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OperatorConnectButton
            // 
            this.OperatorConnectButton.BackColor = System.Drawing.Color.White;
            this.OperatorConnectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OperatorConnectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.OperatorConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OperatorConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("OperatorConnectButton.Image")));
            this.OperatorConnectButton.Location = new System.Drawing.Point(23, 22);
            this.OperatorConnectButton.Name = "OperatorConnectButton";
            this.OperatorConnectButton.Size = new System.Drawing.Size(87, 26);
            this.OperatorConnectButton.TabIndex = 25;
            this.OperatorConnectButton.UseVisualStyleBackColor = false;
            // 
            // OperatorGetIntersect
            // 
            this.OperatorGetIntersect.FlatAppearance.BorderSize = 0;
            this.OperatorGetIntersect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.OperatorGetIntersect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.OperatorGetIntersect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OperatorGetIntersect.Image = ((System.Drawing.Image)(resources.GetObject("OperatorGetIntersect.Image")));
            this.OperatorGetIntersect.Location = new System.Drawing.Point(23, 72);
            this.OperatorGetIntersect.Name = "OperatorGetIntersect";
            this.OperatorGetIntersect.Size = new System.Drawing.Size(72, 20);
            this.OperatorGetIntersect.TabIndex = 26;
            this.OperatorGetIntersect.UseVisualStyleBackColor = true;
            this.OperatorGetIntersect.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(23, 122);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 20);
            this.button3.TabIndex = 27;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(23, 172);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 20);
            this.button5.TabIndex = 29;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(23, 222);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(86, 21);
            this.button6.TabIndex = 30;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // OperatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.OperatorGetIntersect);
            this.Controls.Add(this.OperatorConnectButton);
            this.Name = "OperatorControl";
            this.Size = new System.Drawing.Size(140, 637);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OperatorConnectButton;
        private System.Windows.Forms.Button OperatorGetIntersect;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}
