using System.Drawing;

namespace MD5Plugin
{
    partial class CommonPlugin
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
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.inputTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.inputTextBox.Location = new System.Drawing.Point(17, 20);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(460, 450);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.inputTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputTextBox_MouseDown);
            inputTextBox.ForeColor = Color.DarkGray;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.outputTextBox.Location = new System.Drawing.Point(564, 20);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(460, 450);
            this.outputTextBox.TabIndex = 1;
            this.outputTextBox.Text = "加密后的结果";
            outputTextBox.ForeColor = Color.DarkGray;
            this.outputTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OutputTextBox_MouseDown);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEncode.Location = new System.Drawing.Point(483, 193);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(75, 30);
            this.buttonEncode.TabIndex = 2;
            this.buttonEncode.Text = "编码 =>";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // MD5128Plugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Name = "MD5128Plugin";
            this.Size = new System.Drawing.Size(1046, 549);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox inputTextBox;
        public System.Windows.Forms.TextBox outputTextBox;
        public System.Windows.Forms.Button buttonEncode;
    }
}
