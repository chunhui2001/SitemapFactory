namespace SitemapFactory
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
            this.btnGo = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnLoadStatus = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_Grid = new System.Windows.Forms.Panel();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.cbbResultList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(935, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 24);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 76);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(909, 422);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // btnLoadStatus
            // 
            this.btnLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadStatus.Location = new System.Drawing.Point(935, 76);
            this.btnLoadStatus.Name = "btnLoadStatus";
            this.btnLoadStatus.Size = new System.Drawing.Size(75, 47);
            this.btnLoadStatus.TabIndex = 5;
            this.btnLoadStatus.Text = "Load Status";
            this.btnLoadStatus.UseVisualStyleBackColor = true;
            this.btnLoadStatus.Click += new System.EventHandler(this.btnLoadStatus_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(15, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(909, 486);
            this.panel2.TabIndex = 8;
            // 
            // panel_Grid
            // 
            this.panel_Grid.BackColor = System.Drawing.SystemColors.Window;
            this.panel_Grid.Location = new System.Drawing.Point(15, 76);
            this.panel_Grid.Name = "panel_Grid";
            this.panel_Grid.Size = new System.Drawing.Size(909, 422);
            this.panel_Grid.TabIndex = 9;
            this.panel_Grid.Visible = false;
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(14, 51);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 10;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(91, 51);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(75, 23);
            this.btnResult.TabIndex = 11;
            this.btnResult.Text = "Results";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // cbbResultList
            // 
            this.cbbResultList.FormattingEnabled = true;
            this.cbbResultList.Location = new System.Drawing.Point(91, 52);
            this.cbbResultList.Name = "cbbResultList";
            this.cbbResultList.Size = new System.Drawing.Size(75, 21);
            this.cbbResultList.TabIndex = 0;
            this.cbbResultList.SelectedIndexChanged += new System.EventHandler(this.cbbResultList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 513);
            this.Controls.Add(this.cbbResultList);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.panel_Grid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnLoadStatus);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnGo);
            this.Name = "Form1";
            this.Text = "Sitemap Factory";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnLoadStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_Grid;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.ComboBox cbbResultList;
    }
}

