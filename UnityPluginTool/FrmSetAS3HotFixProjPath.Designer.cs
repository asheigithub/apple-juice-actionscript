namespace ASRuntimeUnityPluginTool
{
	partial class FrmSetAS3HotFixProjPath
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtProjName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtProjPath = new System.Windows.Forms.TextBox();
			this.btnBrow = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.chkCreateDirectionary = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "项目名";
			// 
			// txtProjName
			// 
			this.txtProjName.Location = new System.Drawing.Point(84, 47);
			this.txtProjName.Name = "txtProjName";
			this.txtProjName.Size = new System.Drawing.Size(193, 21);
			this.txtProjName.TabIndex = 1;
			this.txtProjName.Text = "HotFixProj";
			this.txtProjName.TextChanged += new System.EventHandler(this.txtProjName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "项目路径";
			// 
			// txtProjPath
			// 
			this.txtProjPath.Location = new System.Drawing.Point(84, 85);
			this.txtProjPath.Name = "txtProjPath";
			this.txtProjPath.Size = new System.Drawing.Size(367, 21);
			this.txtProjPath.TabIndex = 3;
			this.txtProjPath.TextChanged += new System.EventHandler(this.txtProjPath_TextChanged);
			// 
			// btnBrow
			// 
			this.btnBrow.Location = new System.Drawing.Point(457, 85);
			this.btnBrow.Name = "btnBrow";
			this.btnBrow.Size = new System.Drawing.Size(75, 23);
			this.btnBrow.TabIndex = 4;
			this.btnBrow.Text = "浏览";
			this.btnBrow.UseVisualStyleBackColor = true;
			this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(457, 139);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(539, 139);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 206);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(637, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.AddExtension = false;
			this.openFileDialog1.CheckFileExists = false;
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// chkCreateDirectionary
			// 
			this.chkCreateDirectionary.AutoSize = true;
			this.chkCreateDirectionary.Checked = true;
			this.chkCreateDirectionary.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCreateDirectionary.Location = new System.Drawing.Point(15, 170);
			this.chkCreateDirectionary.Name = "chkCreateDirectionary";
			this.chkCreateDirectionary.Size = new System.Drawing.Size(96, 16);
			this.chkCreateDirectionary.TabIndex = 8;
			this.chkCreateDirectionary.Text = "创建项目目录";
			this.chkCreateDirectionary.UseVisualStyleBackColor = true;
			this.chkCreateDirectionary.CheckedChanged += new System.EventHandler(this.chkCreateDirectionary_CheckedChanged);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(455, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(125, 12);
			this.linkLabel1.TabIndex = 9;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "前往下载flashDevelop";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(233, 12);
			this.label4.TabIndex = 11;
			this.label4.Text = "将创建FlashDevelop工程作为热更新工程。";
			// 
			// FrmSetAS3HotFixProjPath
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 228);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.chkCreateDirectionary);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnBrow);
			this.Controls.Add(this.txtProjPath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtProjName);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSetAS3HotFixProjPath";
			this.Text = "创建AS3热更项目";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtProjName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtProjPath;
		private System.Windows.Forms.Button btnBrow;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.CheckBox chkCreateDirectionary;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
	}
}