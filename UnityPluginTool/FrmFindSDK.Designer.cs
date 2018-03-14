namespace ASRuntimeUnityPluginTool
{
	partial class FrmFindSDK
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
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "选择ASRuntime SDK所在目录";
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(13, 54);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(213, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// btnFind
			// 
			this.btnFind.Location = new System.Drawing.Point(232, 54);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(56, 23);
			this.btnFind.TabIndex = 2;
			this.btnFind.Text = "浏览";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(294, 54);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(14, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(167, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "没有选择有效的ASRuntime SDK";
			// 
			// FrmFindSDK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 109);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmFindSDK";
			this.Text = "查找SDK";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label2;
	}
}