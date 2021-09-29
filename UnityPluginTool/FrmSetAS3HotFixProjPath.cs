using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASRuntimeUnityPluginTool
{
	public partial class FrmSetAS3HotFixProjPath : Form
	{

		private string unityprojdir;

		public FrmSetAS3HotFixProjPath(string unityprojdir)
		{
			InitializeComponent();

			this.unityprojdir = unityprojdir;

			this.openFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(unityprojdir);

			txtProjPath.Text = unityprojdir;
			updatelabel();
		}

		private void updatelabel()
		{

			toolStripStatusLabel1.Text = "Project will be created in:" + getProjFile();

		}

		private string getProjFile()
		{
			string pf = System.IO.Path.GetFullPath( txtProjPath.Text + "/" + (this.chkCreateDirectionary.Checked?txtProjName.Text+"/":"") + txtProjName.Text + ".as3proj");

			return pf;
		}

		public string AS3ProjFile;

		private void btnOK_Click(object sender, EventArgs e)
		{
			string pjfile = getProjFile();

			var pjdir = System.IO.Path.GetDirectoryName(pjfile);
			if (System.IO.Directory.Exists(pjdir) && System.IO.Directory.GetFileSystemEntries(pjdir).Length > 0)
			{
				MessageBox.Show(pjdir + "Directory is not empty. Please select a blank directory.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			DialogResult = DialogResult.OK;

			AS3ProjFile = pjfile;

			Close();
		}

		private void btnBrow_Click(object sender, EventArgs e)
		{
			
			
			this.openFileDialog1.FileName = txtProjName.Text;
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtProjPath.Text = System.IO.Path.GetDirectoryName(this.openFileDialog1.FileName);


				updatelabel();
			}

		}

		private void txtProjName_TextChanged(object sender, EventArgs e)
		{
			updatelabel();
		}

		private void txtProjPath_TextChanged(object sender, EventArgs e)
		{
			updatelabel();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.flashdevelop.org/");
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void chkCreateDirectionary_CheckedChanged(object sender, EventArgs e)
		{
			updatelabel();
		}
	}
}
