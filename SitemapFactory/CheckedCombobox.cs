using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SitemapFactory
{
    public class CheckedCombobox : System.Windows.Forms.Panel
    {
        private DragSelectComponent _dsc;

        public System.Windows.Forms.Panel panel;
        System.Windows.Forms.Panel _parent;
        public System.Windows.Forms.Button btnDropDown;
        public System.Windows.Forms.TextBox txtDropDownInput;
        Form1 _form;

        public SubsidiaryEntry[] _subsidiaryEntry = null;
        private Dictionary<String, List<String>> _navigation = null;

        public Dictionary<String, List<String>> Navigation
        {
            get
            {
                return this._navigation;
            }
        }


        public CheckedCombobox(System.Windows.Forms.Panel parent, Form1 form, SubsidiaryEntry[] subsidiaryEntry)
        {
            comboboxArrowWhite = getImage("http://www.snnmo.com/images/icon/arrow_sans_down-35.png");
            comboboxArrowGray = getImage("http://www.snnmo.com/images/icon/arrow_sans_down-35-gray.png");

            this._form = form;
            this._parent = parent;
            this._navigation = new Dictionary<string, List<string>>();
            this._subsidiaryEntry = subsidiaryEntry;

            parent.BackColor = System.Drawing.Color.Transparent;

            this.BackColor = System.Drawing.Color.Transparent;
            this.Width = parent.Width;
            this.Height = parent.Height;

            this.Location = parent.Location;
            parent.Visible = false;

            form.Controls.Add(this);

            txtDropDownInput = new System.Windows.Forms.TextBox();
            txtDropDownInput.ReadOnly = true;
            txtDropDownInput.BackColor = System.Drawing.Color.White;
            txtDropDownInput.Width = parent.Width;
            this.txtDropDownInput.Height = 24;



            btnDropDown = new System.Windows.Forms.Button();
            btnDropDown.Width = 16;
            btnDropDown.Height = 16;
            this.setBtnDropDownLocation();
            btnDropDown.BackColor = System.Drawing.Color.White;
            btnDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDropDown.FlatAppearance.BorderSize = 0;
            btnDropDown.BackColor = System.Drawing.Color.Transparent;
            btnDropDown.ForeColor = System.Drawing.Color.Black;

            this.setBackgroundImage(btnDropDown, this.comboboxArrowWhite);

            txtDropDownInput.Click += btnDropDown_Click;
            btnDropDown.Click += btnDropDown_Click;
            btnDropDown.MouseHover += btnDropDown_MouseHover;
            btnDropDown.MouseLeave += btnDropDown_MouseLeave;

            this.Controls.Add(btnDropDown);
            this.Controls.Add(txtDropDownInput);

            panel = new System.Windows.Forms.Panel();
            panel.Paint += panel_Paint;

            panel.BackColor = System.Drawing.Color.White;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.Location = new System.Drawing.Point(0, 21);
            panel.Name = "panel1";
            panel.Size = new System.Drawing.Size(this.Width, parent.Height - 21);
            panel.Visible = false;

            this.panel.MouseDown += panel_MouseDown;
            this.panel.MouseUp += panel_MouseUp;
            this.panel.MouseMove += panel_MouseMove;

            this.Controls.Add(panel);


            _dsc = new DragSelectComponent(this);
        }

        void panel_MouseMove(object sender, MouseEventArgs e)
        {
            var btnGo = this._form.Controls.Find("btnGo", true)[0] as Button;
            if (!btnGo.Enabled)
                return;

            if (this._dsc.isDown)
            {
                this._dsc.BX = e.X;
                this._dsc.BY = e.Y;

                this._dsc.doDrag();
                this._dsc.doCheck();
            }
        }

        void panel_MouseUp(object sender, MouseEventArgs e)
        {
            var btnGo = this._form.Controls.Find("btnGo", true)[0] as Button;
            if (!btnGo.Enabled)
                return;

            this.panel.Refresh();
            this._dsc.isDown = false;
        }

        void panel_MouseDown(object sender, MouseEventArgs e)
        {
            var btnGo = this._form.Controls.Find("btnGo", true)[0] as Button;
            if (!btnGo.Enabled)
                return;

            this._dsc.AX = e.X;
            this._dsc.AY = e.Y;

            //Panel p = new Panel();
            //panel.BackColor = Color.Red;
            //p.Width = this.panel.Width;
            //p.Height = this.panel.Height;
            //p.BringToFront();

            //this._form.Controls.Add(p);

            this._dsc.isDown = true;
        }

        public void addCheckBox()
        {
            this.panel.Controls.Clear();

            var checkedSubs = this.txtDropDownInput.Text.TrimEnd(',').Split(',');

            var xOffset = 135;
            var hOffset = 17;

            var itemCount = _subsidiaryEntry.Length;

            int columnCount = this.Width / xOffset;
            int rowCount = itemCount < columnCount ? 1 : itemCount / columnCount;

            if (itemCount % columnCount != 0) rowCount = rowCount + 1;

            int panelHeight = rowCount * hOffset + 8;

            this.panel.Height = panelHeight;

            for (int i = 0; i < rowCount; i++)
            {
                var y = i * hOffset + 4;

                for (int j = 0; j < columnCount; j++)
                {
                    var x = j * xOffset + 4;

                    var index = i * columnCount + j;

                    if (index + 1 > itemCount)
                    {
                        break;
                    }

                    System.Windows.Forms.CheckBox cbx = new System.Windows.Forms.CheckBox();

                    cbx.AutoSize = true;
                    cbx.Location = new System.Drawing.Point(x, y);
                    cbx.Name = _subsidiaryEntry[index].Name;
                    cbx.Size = new System.Drawing.Size(xOffset, hOffset);
                    cbx.TabIndex = 0;

                    var lastModifyDate = _subsidiaryEntry[index].SitemapLastModifyDate.ToString("yy.MM.dd");
                    if (lastModifyDate == "70.01.01")
                        lastModifyDate = "(n/a)";

                    cbx.Text = _subsidiaryEntry[index].Name + "-" + lastModifyDate;

                    if (checkedSubs.Contains(_subsidiaryEntry[index].Name))
                    {
                        cbx.Checked = true;
                    }

                    cbx.UseVisualStyleBackColor = true;
                    cbx.Click += c1_Click;
                    cbx.BackColor = System.Drawing.Color.Transparent;

                    this.panel.Controls.Add(cbx);
                }
            }
        }

        void panel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // (sender as Panel).BorderStyle = BorderStyle.None;
            // e.Graphics.DrawRectangle(new Pen(Color.Gray, 1), (sender as Panel).ClientRectangle);
        }

        private System.Drawing.Image getImage(String imageurl)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(imageurl);
            return System.Drawing.Image.FromStream(new MemoryStream(bytes));
        }

        private void setBackgroundImage(System.Windows.Forms.Button button, System.Drawing.Image image)
        {
            var newImage = new Bitmap(this.btnDropDown.Width, this.btnDropDown.Height);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, this.btnDropDown.Width, this.btnDropDown.Height);
            button.BackgroundImage = newImage;
        }

        private System.Drawing.Image comboboxArrowWhite;

        public System.Drawing.Image ComboboxArrowWhite
        {
            get
            {
                return comboboxArrowWhite;
            }
        }

        private System.Drawing.Image comboboxArrowGray;

        public System.Drawing.Image ComboboxArrowGray
        {
            get
            {
                return comboboxArrowGray;
            }
        }

        void btnDropDown_MouseLeave(object sender, EventArgs e)
        {
            this.setBackgroundImage(this.btnDropDown, this.comboboxArrowWhite);
            this.btnDropDown.Refresh();
        }

        void btnDropDown_MouseHover(object sender, EventArgs e)
        {
            this.setBackgroundImage(this.btnDropDown, this.ComboboxArrowGray);
            this.btnDropDown.Refresh();
        }

        public void btnDropDown_Click(object sender, EventArgs e)
        {
            this.addCheckBox();

            panel.Visible = !panel.Visible;

            this.adjustPostion();



        }

        public void adjustPostion()
        {
            this.addCheckBox();

            var ht = this.panel.Height;

            // adjuest position for log panel and grid 
            var richTextBox1 = this._form.Controls.Find("richTextBox1", true);
            var panel_Grid = this._form.Controls.Find("panel_Grid", true);

            var btnLog = this._form.Controls.Find("btnLog", true)[0] as Button;
            var btnResult = this._form.Controls.Find("btnResult", true)[0] as Button;
            var cbbResultList = this._form.Controls.Find("cbbResultList", true)[0] as ComboBox;
            var btnLoadStatus = this._form.Controls.Find("btnLoadStatus", true)[0] as Button;

            RichTextBox rtb = richTextBox1[0] as RichTextBox;
            Panel currPanel = panel_Grid[0] as Panel;

            if (this.panel.Visible)
            {
                rtb.Location = new Point(rtb.Location.X, rtb.Location.Y + ht);
                rtb.Height = rtb.Height - ht;

                currPanel.Location = new Point(currPanel.Location.X, currPanel.Location.Y + ht);
                currPanel.Height = currPanel.Height - ht;

                btnLog.Location = new Point(btnLog.Location.X, btnLog.Location.Y + ht);
                btnResult.Location = new Point(btnResult.Location.X, btnResult.Location.Y + ht);
                cbbResultList.Location = new Point(cbbResultList.Location.X, cbbResultList.Location.Y + ht);
                btnLoadStatus.Location = new Point(btnLoadStatus.Location.X, btnLoadStatus.Location.Y + ht);
            }
            else
            {
                //  if (rtb.Location.Y - ht < 0) return;

                rtb.Location = new Point(rtb.Location.X, rtb.Location.Y - ht);
                rtb.Height = rtb.Height + ht;

                currPanel.Location = new Point(currPanel.Location.X, currPanel.Location.Y - ht);
                currPanel.Height = currPanel.Height + ht;

                btnLog.Location = new Point(btnLog.Location.X, btnLog.Location.Y - ht);
                btnResult.Location = new Point(btnResult.Location.X, btnResult.Location.Y - ht);
                cbbResultList.Location = new Point(cbbResultList.Location.X, cbbResultList.Location.Y - ht);
                btnLoadStatus.Location = new Point(btnLoadStatus.Location.X, btnLoadStatus.Location.Y - ht);
            }


            //HelperClass.adjustGridWidth()

            foreach (var item in currPanel.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                {
                    HelperClass.adjustGridWidth(grid, currPanel);
                }
            }
        }

        void c1_Click(object sender, EventArgs e)
        {
            var cbx = sender as System.Windows.Forms.CheckBox;
            this.doClick(cbx);
        }

        public void doClick(CheckBox cbx)
        {
            var txt = cbx.Text.Substring(0, cbx.Text.LastIndexOf("-"));

            var btnGo = this._form.Controls.Find("btnGo", true)[0] as Button;
            if (!btnGo.Enabled)
            {
                cbx.Checked = !cbx.Checked;
                return;
            }

            if (!cbx.Checked)
            {
                this.txtDropDownInput.Text = this.txtDropDownInput.Text.Replace(txt + ",", "");
                return;
            }

            if (cbx.Checked && this.txtDropDownInput.Text.Contains(txt))
            {
                return;
            }

            // check subs if avaliable
            var currSiteName = cbx.Name;
            var path = string.Empty;

            if (currSiteName == "en-gb")
            {
                path = string.Format("{0}{1}\\enterprise\\sitepages", this._form.CSPAuthoringRoot(), currSiteName);
            }
            else
            {
                path = string.Format("{0}enterprise\\{1}\\sitepages", this._form.CSPAuthoringRoot(), currSiteName);
            }

            // MethodInvoker action = delegate
            //  {

            if (!System.IO.Directory.Exists(path))
            {
                cbx.ForeColor = System.Drawing.Color.Red;
                cbx.Text = cbx.Text.Substring(0, cbx.Text.LastIndexOf('-')) + "-Not Available!";
                cbx.Checked = false;
                return;
            }

            //var count = 0;
            // check count, the max count <= 30
            //foreach (var checkbox in this.panel.Controls)
            //{
            //    if (checkbox as CheckBox != null) {
            //        if ((checkbox as CheckBox).Checked) {
            //            count++;
            //        }
            //    }
            //}

            //if (count > 30) {
            //    cbx.Checked = false;
            //    return;
            //}

            if (cbx.Checked)
            {
                if (!this._navigation.ContainsKey(txt))
                    this._navigation.Add(txt, null);

                this._navigation[txt] = new List<string>();
                this._form.LoadNavigation(this._navigation[txt], txt);

                this.txtDropDownInput.Text += txt + ",";
            }

            // };

            //this.Invoke(action);

        }



        internal void doResize(int changedWidth, int changedHeight)
        {
            this.Width = this.txtDropDownInput.Width + changedWidth;
            this.panel.Width = this.Width;
            this._parent.Width = this.panel.Width;
            this.txtDropDownInput.Width = this.panel.Width;

            this.setBtnDropDownLocation();

            this.addCheckBox();
        }

        private void setBtnDropDownLocation()
        {
            btnDropDown.Location = new System.Drawing.Point(this._parent.Width - btnDropDown.Width - 2, 2);
        }
    }

    public class DragSelectComponent
    {

        private bool isInclude = true;
        public bool isDown { get; set; }

        private CheckedCombobox _ccb;
        public int AX { get; set; }
        public int AY { get; set; }
        public int BX { get; set; }
        public int BY { get; set; }

        public DragSelectComponent(CheckedCombobox ccb)
        {
            _ccb = ccb;
        }


        public void doDrag()
        {
            if (this.AX == this.BX && this.AY == this.BY)
            {
                // just click instead of drag
                //_ccb.txtDropDownInput.Text = "just click";
                return;
            }

            if (this.AX < this.BX)
            {
                // left to right
                //_ccb.txtDropDownInput.Text = "left to right";
                isInclude = true;
            }

            if (this.AX > this.BX)
            {
                // right to left
                //_ccb.txtDropDownInput.Text = "right to left";
                isInclude = false;
            }

            drawingRectangle();
        }

        private void drawingRectangle()
        {
            this._ccb.panel.Refresh();


            Pen drwaPen = new Pen(isInclude ? Color.Navy : Color.Green, 2);
            drwaPen.DashStyle = isInclude ? System.Drawing.Drawing2D.DashStyle.Solid : System.Drawing.Drawing2D.DashStyle.Dash;
            Rectangle rect = new Rectangle(Math.Min(this.AX, this.BX),
                               Math.Min(this.AY, this.BY),
                               Math.Abs(this.AX - this.BX),
                               Math.Abs(this.AY - this.BY));
            System.Drawing.Graphics formGraphics = this._ccb.panel.CreateGraphics(); ;

            formGraphics.DrawRectangle(drwaPen, rect);
        }

        internal void doCheck()
        {
            foreach (var item in this._ccb.panel.Controls)
            {
                var cbx = item as CheckBox;
                if (cbx != null)
                {
                    var location = cbx.Location;

                    if ((location.X >= Math.Min(this.AX, this.BX) &&
                        location.X <= Math.Max(this.AX, this.BX))
                        && (location.Y >= Math.Min(this.AY, this.BY) &&
                        location.Y <= Math.Max(this.AY, this.BY)))
                    {
                        //_ccb.txtDropDownInput.Text = "checked";
                        cbx.Checked = isInclude;
                    }
                    else
                    {
                        // cbx.Checked = false;
                    }

                    this._ccb.doClick(cbx);
                }
            }
        }
    }


}
