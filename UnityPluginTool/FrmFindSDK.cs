using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASRuntimeUnityPluginTool
{
	public partial class FrmFindSDK : Form
	{
		public FrmFindSDK()
		{
			InitializeComponent();
			this.DialogResult = DialogResult.No;
		}

		public string SDKPath;

		private void btnFind_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				string p = folderBrowserDialog1.SelectedPath;
				if (checksdk(p))
				{
					textBox1.Text = p;
				}
				else
				{
					textBox1.Text = string.Empty;
				}
			}
			else
			{
				textBox1.Text = string.Empty;
			}
		}

		private bool checksdk(string path)
		{
			if (System.IO.Directory.Exists(path))
			{
				if (System.IO.File.Exists(path + "/air-sdk-description.xml")
					&&
					System.IO.File.Exists(path + "/bin/CMXMLCCLI.exe")
					&&
					System.IO.File.Exists(path + "/unity/ASRuntimeUnityPluginTool.exe")
					)
				{
					label2.Visible = false;
					return true;
				}
			}
			label2.Visible = true;
			return false;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			checksdk(textBox1.Text);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (checksdk(textBox1.Text))
			{
				SDKPath = textBox1.Text;
				this.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				MessageBox.Show("选择有效的ASRuntime SDK目录。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
