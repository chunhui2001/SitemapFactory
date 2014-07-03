using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SitemapFactory
{
    public partial class InputFormDialog : Form
    {
        private Form1 f;

        public InputFormDialog(Form1 form)
        {
            InitializeComponent();

            this.f = form;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.f._cspauthoringRoot = this.textBox1.Text;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }
    }
}
