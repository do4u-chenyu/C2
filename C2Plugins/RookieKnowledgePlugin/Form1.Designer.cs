namespace RookieKnowledgePlugin
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linuxTextBox = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linuxFilterTB = new System.Windows.Forms.TextBox();
            this.linuxTreeView = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pythonTextBox = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pythonTreeView = new System.Windows.Forms.TreeView();
            this.pythonFilterTB = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Linux";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.linuxTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(152, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 418);
            this.panel2.TabIndex = 3;
            // 
            // linuxTextBox
            // 
            this.linuxTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linuxTextBox.FoldingStrategy = "XML";
            this.linuxTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.linuxTextBox.IsReadOnly = true;
            this.linuxTextBox.Location = new System.Drawing.Point(0, 0);
            this.linuxTextBox.Name = "linuxTextBox";
            this.linuxTextBox.ShowSpaces = true;
            this.linuxTextBox.ShowTabs = true;
            this.linuxTextBox.Size = new System.Drawing.Size(637, 418);
            this.linuxTextBox.SyntaxHighlighting = "Python";
            this.linuxTextBox.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linuxFilterTB);
            this.panel1.Controls.Add(this.linuxTreeView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(149, 418);
            this.panel1.TabIndex = 2;
            // 
            // linuxFilterTB
            // 
            this.linuxFilterTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.linuxFilterTB.Location = new System.Drawing.Point(0, 0);
            this.linuxFilterTB.Name = "linuxFilterTB";
            this.linuxFilterTB.Size = new System.Drawing.Size(149, 21);
            this.linuxFilterTB.TabIndex = 3;
            this.linuxFilterTB.TextChanged += new System.EventHandler(this.LinuxFilterTB_TextChanged);
            // 
            // linuxTreeView
            // 
            this.linuxTreeView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linuxTreeView.Location = new System.Drawing.Point(0, 20);
            this.linuxTreeView.Name = "linuxTreeView";
            this.linuxTreeView.Size = new System.Drawing.Size(149, 398);
            this.linuxTreeView.TabIndex = 0;
            this.linuxTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LinuxTreeView_AfterSelect);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Python";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pythonTextBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(152, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(637, 418);
            this.panel4.TabIndex = 1;
            // 
            // pythonTextBox
            // 
            this.pythonTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pythonTextBox.FoldingStrategy = "XML";
            this.pythonTextBox.Font = new System.Drawing.Font("Courier New", 10F);
            this.pythonTextBox.Location = new System.Drawing.Point(0, 25);
            this.pythonTextBox.Name = "pythonTextBox";
            this.pythonTextBox.Size = new System.Drawing.Size(637, 393);
            this.pythonTextBox.SyntaxHighlighting = "Python";
            this.pythonTextBox.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pythonTreeView);
            this.panel3.Controls.Add(this.pythonFilterTB);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(149, 418);
            this.panel3.TabIndex = 0;
            // 
            // pythonTreeView
            // 
            this.pythonTreeView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pythonTreeView.Location = new System.Drawing.Point(0, 20);
            this.pythonTreeView.Name = "pythonTreeView";
            this.pythonTreeView.Size = new System.Drawing.Size(149, 398);
            this.pythonTreeView.TabIndex = 5;
            this.pythonTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PythonTreeView_AfterSelect);
            // 
            // pythonFilterTB
            // 
            this.pythonFilterTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pythonFilterTB.Location = new System.Drawing.Point(0, 0);
            this.pythonFilterTB.Name = "pythonFilterTB";
            this.pythonFilterTB.Size = new System.Drawing.Size(149, 21);
            this.pythonFilterTB.TabIndex = 4;
            this.pythonFilterTB.TextChanged += new System.EventHandler(this.PythonFilterTB_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新人培训";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView linuxTreeView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox linuxFilterTB;
        private ICSharpCode.TextEditor.TextEditorControlEx linuxTextBox;
        private System.Windows.Forms.Panel panel4;
        private ICSharpCode.TextEditor.TextEditorControlEx pythonTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView pythonTreeView;
        private System.Windows.Forms.TextBox pythonFilterTB;
    }
}