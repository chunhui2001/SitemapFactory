using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SitemapFactory
{
    public partial class Form1 : Form
    {
        private SubsidiaryEntry[] _subsidiaryList;
        private String[] _selectedSubsidiaryList;
        private String _subsidiaryName = "";

        public String _cspauthoringRoot = @"X:\";


        private CheckedCombobox ccb = null;

        private void enableBtnResult()
        {
            this.btnResult.BackColor = System.Drawing.Color.Green;
            this.btnResult.ForeColor = System.Drawing.Color.White;

            this.btnLog.BackColor = System.Drawing.Color.Gray;
            this.btnLog.ForeColor = System.Drawing.Color.White;
        }
        private void enableBtnLog()
        {
            this.btnLog.BackColor = System.Drawing.Color.Green;
            this.btnLog.ForeColor = System.Drawing.Color.White;

            this.btnResult.BackColor = System.Drawing.Color.Gray;
            this.btnResult.ForeColor = System.Drawing.Color.White;
        }

        public Form1()
        {
            InitializeComponent();

            this.Click += Form1_Click;

            this.btnResult.BackColor = System.Drawing.Color.Gray;
            this.btnResult.ForeColor = System.Drawing.Color.White;

            this.btnLog.BackColor = System.Drawing.Color.Gray;
            this.btnLog.ForeColor = System.Drawing.Color.White;

            //this.btnResult.Margin = 0;
            this.btnLog.Padding = new Padding(0, 0, 0, 0);
            this.btnLog.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);

            //this.btnLog.BackColor = System.Drawing.Color.Blue;
            //this.btnLog.FlatStyle = FlatStyle.Flat;
            //this.btnResult.FlatStyle = FlatStyle.Flat;
            //this.btnLog.FlatAppearance.BorderColor = Color.Red;
            //this.btnLog.FlatAppearance.BorderSize = 0;
            //this.btnResult.FlatAppearance.BorderSize = 0;
            this.cbbResultList.Visible = false;



            ccb = new CheckedCombobox(this.panel2, this, this._cspauthoringRoot);
            this.panel2.Visible = false;
            this.richTextBox1.Visible = true;
            this.btnResult.Visible = false;
            this.richTextBox1.BorderStyle = BorderStyle.FixedSingle;


            this.MaximizeBox = false;
            this.btnLoadStatus.Visible = false;

            // var gridName = "gridName1";
            // var grid = this.getDataGridView(gridName);

            // this.panel_Grid.Controls.Add( grid);


            //  doInit();
        }

        void Form1_Click(object sender, EventArgs e)
        {
            //this.ccb.btnDropDown.Click();
        }

        private DataGridView createDataGridView(string gridName)
        {
            var grid = new DataGridView();

            grid.Name = gridName;
            grid.AutoGenerateColumns = false;
            grid.BorderStyle = BorderStyle.None;


            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "ID";
            idColumn.DataPropertyName = "ID";
            idColumn.HeaderText = "ID";

            DataGridViewTextBoxColumn fileStatusColumn = new DataGridViewTextBoxColumn();
            fileStatusColumn.Name = "FileStatus";
            fileStatusColumn.DataPropertyName = "FileStatus";
            fileStatusColumn.HeaderText = "Status";

            DataGridViewTextBoxColumn changefreqColumn = new DataGridViewTextBoxColumn();
            changefreqColumn.Name = "Changefreq";
            changefreqColumn.DataPropertyName = "Changefreq";
            changefreqColumn.HeaderText = "Changefreq";

            DataGridViewTextBoxColumn priorityColumn = new DataGridViewTextBoxColumn();
            priorityColumn.Name = "Priority";
            priorityColumn.DataPropertyName = "Priority";
            priorityColumn.HeaderText = "Priority";

            DataGridViewTextBoxColumn lastmodColumn = new DataGridViewTextBoxColumn();
            lastmodColumn.Name = "Lastmod";
            lastmodColumn.DataPropertyName = "Lastmod";
            lastmodColumn.HeaderText = "Lastmod";

            DataGridViewTextBoxColumn locColumn = new DataGridViewTextBoxColumn();
            locColumn.Name = "Loc";
            locColumn.DataPropertyName = "Loc";
            locColumn.HeaderText = "Loc";

            grid.Columns.Add(idColumn);
            grid.Columns.Add(fileStatusColumn);
            grid.Columns.Add(changefreqColumn);
            grid.Columns.Add(priorityColumn);
            grid.Columns.Add(lastmodColumn);
            grid.Columns.Add(locColumn);

            grid.Columns["ID"].Width = 30;
            grid.Columns["FileStatus"].Width = 60;
            grid.Columns["Changefreq"].Width = 70;
            grid.Columns["Priority"].Width = 50;
            grid.Columns["Lastmod"].Width = 65;

            HelperClass.adjustGridWidth(grid, this.panel_Grid);

            if (grid.Columns["Loc"].Width < 200)
            {
                grid.Columns["Loc"].Width = 200;
            }

            grid.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.Columns["FileStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.Columns["Changefreq"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.Columns["Priority"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.Columns["Lastmod"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.Columns["Loc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;


            grid.Columns["Changefreq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns["Priority"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grid.RowHeadersVisible = false;

            return grid;
        }


        private void doInit()
        {
            // http://www.microsoft.com/enterprise/en-us/default.aspx

            _selectedSubsidiaryList = this.ccb.txtDropDownInput.Text.TrimEnd(',').Split(',');

            this._subsidiaryList = this.ccb.subsidiaryEntry;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InputFormDialog ifd = new InputFormDialog(this);

            if (ifd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
            else
            {
                this.Close();
                this.Dispose();
                return;
            }
        }


        private void showMsgBox(String errorMsg)
        {
            MessageBox.Show(errorMsg, "ERROR!");
        }

        private void clearGrid()
        {
            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                {
                    this.panel_Grid.Controls.Remove(grid);
                }
            }
        }

        private DataGridView getGrid(String subsidiaryName)
        {
            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null && grid.Name == subsidiaryName)
                    return grid;
            }

            return null;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.doInit();

            this.clearGrid();
            this.cbbResultList.Items.Clear();
            this.cbbResultList.SelectedText = string.Empty;
            this.cbbResultList.Visible = false;

            this.ccb.txtDropDownInput.Enabled = false;
            this.ccb.btnDropDown.Enabled = false;
            this.btnLoadStatus.Visible = false;
            this.richTextBox1.Visible = true;
            this.panel_Grid.Visible = false;
            this.btnResult.Visible = false;
            this.enableBtnLog();

            this.btnGo.Enabled = false;
            richTextBox1.Text = String.Empty;

            var panel1 = this.ccb.Controls.Find("panel1", true);
            if (panel1.Length > 0)
            {
                if (panel1[0].Visible)
                {
                    panel1[0].Visible = false;
                    ccb.adjustPostion();
                }
            }


            foreach (var siteName in this._selectedSubsidiaryList)
            {
                //this.cbbResultList.Items.Add(siteName);
                //this.cbbResultList.SelectedIndex = 0;
                this.loadSitePages(siteName);
            }
        }

        private IEnumerable<URLEntry> loadSitePages(String siteName)
        {

            RecursionFolder r = new RecursionFolder();
            r.NotifyParentEvent += updateLogPanel;
            r.subsidiaryName = siteName;

            var path = "";

            if (siteName == "en-gb")
            {

                path = string.Format("{0}{1}\\enterprise\\sitepages", _cspauthoringRoot, siteName);
            }
            else
            {

                path = string.Format("{0}enterprise\\{1}\\sitepages", _cspauthoringRoot, siteName);
            }


            DelRecursionFolder drf = new DelRecursionFolder(r.Recursion);
            drf.BeginInvoke(path, new AsyncCallback(loadSitePagesComplete), r);

            //  r.Recursion(path);
            //this.loadSitePagesComplete(r.files, r.subsidiaryName);

            return r.files;
        }

        //private void loadSitePagesComplete(List<URLEntry> urlEntryList, String subsidiaryName)
        //{
        //    MethodInvoker action = null;
        //    if (urlEntryList == null) return;

        //    // create grid
        //    var grid = createDataGridView(subsidiaryName);

        //    action = delegate
        //    {
        //        this.cbbResultList.Items.Add(subsidiaryName);
        //        var isVisible = false;

        //        if (this.cbbResultList.Items.Count == 1)
        //        {
        //            this.cbbResultList.SelectedIndex = 0;
        //            isVisible = true;
        //        }

        //        if (this.cbbResultList.Items.Count == _selectedSubsidiaryList.Length)
        //        {
        //            MethodInvoker action3 = delegate
        //            {
        //                this.ccb.txtDropDownInput.Enabled = true;
        //            };
        //            this.ccb.txtDropDownInput.Invoke(action3);


        //            MethodInvoker action4 = delegate
        //            {
        //                this.ccb.btnDropDown.Enabled = true;
        //            };
        //            this.ccb.btnDropDown.Invoke(action4);
        //        }

        //        MethodInvoker action2 = delegate
        //        {
        //            this.panel_Grid.Visible = true;
        //            this.panel_Grid.Controls.Add(grid);
        //            grid.Visible = isVisible;
        //            this.enableBtnResult();
        //        };


        //        this.panel_Grid.Invoke(action2);
        //    };

        //    this.cbbResultList.Invoke(action);

        //    this.bindData(urlEntryList, subsidiaryName);
        //}

        private void loadSitePagesComplete(IAsyncResult itfAR)
        {
            MethodInvoker action = delegate
            { this.btnGo.Enabled = true; };
            this.btnGo.BeginInvoke(action);

            action = delegate
            { this.btnResult.Visible = false; };
            this.btnResult.Invoke(action);

            action = delegate
            { this.cbbResultList.Visible = true; };
            this.cbbResultList.Invoke(action);

            var v = itfAR.AsyncState as RecursionFolder;
            if (v != null)
            {
                var urlEntryList = v.files;


                if (urlEntryList != null)
                {
                    // create grid
                    var grid = createDataGridView(v.subsidiaryName);

                    action = delegate
                    {
                        this.cbbResultList.Items.Add(v.subsidiaryName);
                        var isVisible = false;

                        if (this.cbbResultList.Items.Count == 1)
                        {
                            this.cbbResultList.SelectedIndex = 0;
                            isVisible = true;
                        }

                        if (this.cbbResultList.Items.Count == _selectedSubsidiaryList.Length)
                        {
                            MethodInvoker action3 = delegate
                            {
                                this.ccb.txtDropDownInput.Enabled = true;
                            };
                            this.ccb.txtDropDownInput.Invoke(action3);


                            MethodInvoker action4 = delegate
                            {
                                this.ccb.btnDropDown.Enabled = true;
                            };
                            this.ccb.btnDropDown.Invoke(action4);
                        }

                        MethodInvoker action2 = delegate
                        {
                            this.panel_Grid.Visible = true;
                            this.panel_Grid.Controls.Add(grid);
                            grid.Visible = isVisible;
                            this.enableBtnResult();
                        };


                        this.panel_Grid.Invoke(action2);
                    };

                    this.cbbResultList.Invoke(action);

                    this.bindData(urlEntryList, v.subsidiaryName);
                }
            }
        }

        private void bindData(List<URLEntry> urlEntryList, String subsidiaryName)
        {

            var currGrid = this.getGrid(subsidiaryName);
            if (currGrid == null) return;

            MethodInvoker action = delegate
            {
                currGrid.DataSource = urlEntryList;
            };

            currGrid.Invoke(action);


            if (urlEntryList.Count > 0)
            {

                action = delegate
               {
                   this.richTextBox1.Visible = false;
               };

                richTextBox1.BeginInvoke(action);

                action = delegate
                {
                    this.btnLoadStatus.Visible = true;
                    this.btnLoadStatus.Enabled = true;
                };

                this.btnLoadStatus.Invoke(action);

                // action = delegate
                // {
                //TODOcurrGrid.Visible = true;
                // };

                //this.dataGridView1.BeginInvoke(action);



            }
        }

        private void updateLogPanel(String msg)
        {
            MethodInvoker action = delegate
            { richTextBox1.Text += (msg + Environment.NewLine); };
            richTextBox1.BeginInvoke(action);

            action = delegate
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            };
            richTextBox1.BeginInvoke(action);

            //richTextBox1.Text += (msg + Environment.NewLine);

            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
        }

        private void UpdateRichTextBox(String text)
        {
            this.richTextBox1.AppendText(this.richTextBox1.Text + text + Environment.NewLine);
        }

        private void btnLoadStatus_Click(object sender, EventArgs e)
        {
            this.btnGo.Enabled = false;

            MethodInvoker action = delegate
            { this.btnLoadStatus.Enabled = false; };

            this.btnLoadStatus.Invoke(action);

            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                {
                    LoadFileStatus lfs = new LoadFileStatus();
                    LoadFileDelegate lfd = new LoadFileDelegate(lfs.LoadFile);
                    lfd.BeginInvoke(grid, new AsyncCallback(loadStatusComplete), lfs);
                }
            }
        }

        [STAThread]
        private void loadStatusComplete(IAsyncResult itfAR)
        {
            var v = itfAR.AsyncState as LoadFileStatus;


            MethodInvoker action = delegate
            { this.btnLoadStatus.Enabled = false; };

            this.btnLoadStatus.Invoke(action);

            action = delegate
            { this.btnGo.Enabled = true; };
            this.btnGo.Invoke(action);

            if (v.SiteMapXmlDocument != null)
            {
                Invoke((Action)(() =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.CreatePrompt = true;
                    saveFileDialog.FileName = "sitemap" + ((String.IsNullOrEmpty(this._subsidiaryName) ? "" : "_" + this._subsidiaryName) + ".xml")
                                                            .Replace("\\enterprise", "").Replace("enterprise\\", "");
                    saveFileDialog.Title = "Save path of the Sitemap to be exported";


                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        v.SiteMapXmlDocument.Save(saveFileDialog.FileName);
                }));

            }
        }

        private int _formWidth = 0;
        private int _formHeight = 0;
        private int _ccbPanelHeight = 0;

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this._formWidth = this.Width;
            this._formHeight = this.Height;

            _ccbPanelHeight = this.ccb.panel.Height;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width < 450) this.Width = 650;
            if (this.Height < 450) this.Height = 450;

            var changedWidth = this.Width - this._formWidth;
            var changedHeight = this.Height - this._formHeight;



            this.btnGo.Left = this.btnGo.Left + changedWidth;
            this.btnLoadStatus.Left = this.btnLoadStatus.Left + changedWidth;
            this.panel2.Width = this.panel2.Width + changedWidth;


            this.panel_Grid.Width = this.panel2.Width;
            this.richTextBox1.Width = this.panel2.Width;
            this.panel_Grid.Height = this.panel_Grid.Height + changedHeight;
            this.richTextBox1.Height = this.richTextBox1.Height + changedHeight;


            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                {
                    HelperClass.adjustGridWidth(grid, this.panel_Grid);
                }
            }


            ccb.doResize(changedWidth, changedHeight);


            //_ccbPanelHeight = this.ccb.panel.Height;

            //ccb.adjustPostion();
            if (ccb.panel.Visible)
                this.adjustAgain(this.ccb.panel.Height - _ccbPanelHeight);
        }

        private void adjustAgain(int height)
        {
            var btnLog = this.Controls.Find("btnLog", true)[0] as Button;
            var btnResult = this.Controls.Find("btnResult", true)[0] as Button;
            var cbbResultList = this.Controls.Find("cbbResultList", true)[0] as ComboBox;
            var btnLoadStatus = this.Controls.Find("btnLoadStatus", true)[0] as Button;

            btnLog.Location = new Point(btnLog.Location.X, btnLog.Location.Y + height);
            btnResult.Location = new Point(btnResult.Location.X, btnResult.Location.Y + height);
            cbbResultList.Location = new Point(cbbResultList.Location.X, cbbResultList.Location.Y + height);
            btnLoadStatus.Location = new Point(btnLoadStatus.Location.X, btnLoadStatus.Location.Y + height);

            this.richTextBox1.Location = new Point(this.richTextBox1.Location.X, this.richTextBox1.Location.Y + height);
            this.panel_Grid.Location = new Point(this.panel_Grid.Location.X, this.panel_Grid.Location.Y + height);



            var hh = this.Height - this.btnLog.Location.Y - this.btnLog.Height - 55;
            this.richTextBox1.Height = hh;
            this.panel_Grid.Height = hh;

            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                    HelperClass.adjustGridWidth(grid, this.panel_Grid);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            this.enableBtnLog();

            this.richTextBox1.Visible = true;
            this.panel_Grid.Visible = false;

            if (this.cbbResultList.Visible)
                this.btnResult.Visible = true;

            this.cbbResultList.Visible = false;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            this.enableBtnResult();

            this.richTextBox1.Visible = false;
            this.panel_Grid.Visible = true;
            this.btnResult.Visible = false;
            this.cbbResultList.Visible = true;
        }

        private void cbbResultList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currCBB = sender as ComboBox;
            if (currCBB == null) return;

            var currSubName = currCBB.SelectedItem == null ? "" : currCBB.SelectedItem.ToString();

            if (String.IsNullOrEmpty(currSubName)) return;

            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null && grid.Name.ToLower() == currSubName.ToLower())
                {
                    grid.Visible = true;
                }
                else if (grid != null)
                {
                    grid.Visible = false;
                }
            }
        }
    }


    public static class HelperClass
    {
        public static void adjustGridWidth(DataGridView grid, Panel panel)
        {
            grid.Width = panel.Width;
            grid.Height = panel.Height;

            grid.Columns["Loc"].Width = grid.Width
                                        - grid.Columns["ID"].Width
                                        - grid.Columns["FileStatus"].Width
                                        - grid.Columns["Changefreq"].Width
                                        - grid.Columns["Priority"].Width
                                        - grid.Columns["Lastmod"].Width - 18;
        }
    }

    public delegate void LoadFileDelegate(DataGridView dataGridView);
    public delegate void DelRecursionFolder(String path);
    public delegate void NotifyParentDelegate(String msg);

    public class RecursionFolder
    {
        public List<URLEntry> files = null;
        public String subsidiaryName = null;

        public event NotifyParentDelegate NotifyParentEvent;


        public RecursionFolder()
        {

            files = new List<URLEntry>();

        }


        public bool isSkip(String path)
        {
            String[] skips = new String[] { 
                                "aRFM",
                                "epgUserContactList",
                                "Testing",
                                "atest",
                                "v2.xml",
                                "v3.xml",
                                "backup.xml",
                                @"\archive\",
                                @"\V1 Archive\",
                                ".docx",
                                ".aspx",
                                "Regsys",
                                "mysitemap",
                                @"\test\",
                                "epgWTAutoTag",
                                "test.xml", "default2.xml",
                                ".png",
                                ".jpg",
                                "v1.xml",
                                "_back_",
                                "-old.xml"};


            foreach (var item in skips)
            {
                if (path.ToLower().IndexOf(item.ToLower()) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public void RecursionFolders(String[] path)
        {
            foreach (var item in path)
                this.Recursion(item);
        }

        public void Recursion(String path)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

            if (di.Exists)
            {
                var filesList = di.GetFiles();

                for (int i = 0; i < filesList.Length; i++)
                {
                    var item = filesList[i];

                    if (NotifyParentEvent != null)
                    {

                        if (!isSkip(item.FullName))
                        {
                            files.Add(new URLEntry() { File = item, FileStatus = FileStatus.None, ID = files.Count + 1 });

                            NotifyParentEvent(item.FullName);
                        }
                        else
                        {
                            NotifyParentEvent(item.FullName + " [SKIP]");
                        }
                    }
                }

                foreach (var item in di.GetDirectories())
                {
                    Recursion(item.FullName);
                }
            }
        }


    }

    public class LoadFileStatus
    {
        private DataGridView _dataGridView;
        private XDocument _siteMapDoc;

        public XDocument SiteMapXmlDocument
        {
            get
            {
                return this._siteMapDoc;
            }
        }

        public LoadFileStatus()
        {
            // xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9""
            _siteMapDoc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset>
   
</urlset>");
        }

        public void LoadFile(DataGridView dataGridView)
        {
            this._dataGridView = dataGridView;

            if (this._dataGridView.Rows.Count == 0) return;

            foreach (var item in this._dataGridView.Rows)
            {
                DataGridViewRow row = item as DataGridViewRow;

                row.Cells["FileStatus"].Value = FileStatus.Loading;
                row.Cells["FileStatus"].Style.ForeColor = Color.Green;

                // get page by url 
                String currUrl = row.Cells["Loc"].Value.ToString();

                WebClient wc = new WebClient();

                //wc.DownloadDataCompleted += wc_DownloadDataCompleted;
                //wc.DownloadDataAsync(new Uri(currUrl), row.Cells["FileStatus"]);

                try
                {
                    /*
                    HttpWebRequest request = WebRequest.Create(currUrl) as HttpWebRequest;
                    request.Method = "HEAD";
                    request.Timeout = 120000;

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    HttpStatusCode status = response.StatusCode;
                    */


                    byte[] pageContent = wc.DownloadData(currUrl);


                    if (pageContent != null && pageContent.Length > 0) //status == HttpStatusCode.OK)
                    {
                        row.Cells["FileStatus"].Value = FileStatus.Approved;
                        row.Cells["FileStatus"].Style.ForeColor = Color.Blue;

                        XElement ele = XElement.Parse(string.Format(@"<url>
      <loc>{0}</loc>
      <lastmod>{1}</lastmod>
      <changefreq>{2}</changefreq>
      <priority>{3}</priority>
   </url>", currUrl, row.Cells["Lastmod"].Value.ToString(), row.Cells["Changefreq"].Value.ToString(), row.Cells["Priority"].Value.ToString()));


                        this._siteMapDoc.Root.AddFirst(ele);
                    }
                }
                catch (WebException we)
                {
                    if (we.Message.Contains("404"))
                    {
                        row.Cells["FileStatus"].Value = FileStatus._404.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Red;
                    }
                    else if (we.Message.ToLower().Contains("timed out"))
                    {
                        row.Cells["FileStatus"].Value = FileStatus.Timeout.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Aqua;
                    }
                    else
                    {

                        row.Cells["FileStatus"].Value = FileStatus.Error.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Red;
                    }
                }
                catch (Exception ee)
                {
                    row.Cells["FileStatus"].Value = FileStatus.Error.ToString();
                    row.Cells["FileStatus"].Style.ForeColor = Color.Red;
                }



                MethodInvoker action = delegate
                {
                    this._dataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                };

                this._dataGridView.BeginInvoke(action);


            }
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var cell = e.UserState as DataGridViewTextBoxCell;

            try
            {
                if (e.Result != null && e.Result.Length > 0)
                {
                    if (cell != null)
                    {
                        cell.Value = FileStatus.Approved;
                        cell.Style.ForeColor = Color.Blue;
                    }
                }
            }
            catch (Exception ee)
            {
                if (cell != null)
                {
                    cell.Value = FileStatus.Error.ToString();
                    cell.Style.ForeColor = Color.Red;
                }
            }
        }
    }


    public class URLEntry
    {
        public int ID { get; set; }
        public FileStatus FileStatus { get; set; }

        public System.IO.FileInfo File { get; set; }
        public String Path
        {
            get
            {
                return "http://cspauthoring/enterprise" + this.File.FullName.Substring(this.File.FullName.IndexOf(":") + 1).Replace("\\", "/");
            }
        }
        public String Loc
        {
            get
            {
                String s = this.File.FullName.Substring(this.File.FullName.IndexOf(":") + 1);
                return "http://www.microsoft.com" + s.ToLower().Replace("\\sitepages", "").Replace("\\", "/").Replace(".xml", ".aspx");
            }
        }
        public String Lastmod
        {
            get
            {
                return this.File.LastWriteTime.ToString("yyyy-MM-dd");
            }
        }
        public String Changefreq { get { return "monthly"; } }
        public String Priority { get { return "0.8"; } }


        public override string ToString()
        {
            String s = string.Format(@"<url>
      <loc>{0}</loc>
      <lastmod>{1}</lastmod>
      <changefreq>{2}</changefreq>
      <priority>{3}</priority>
   </url>", this.Loc, this.Lastmod, this.Changefreq, this.Priority);
            return s;
        }
    }

    public class SubsidiaryEntry
    {
        public String Name { get; set; }
        public DateTime SitemapLastModifyDate { get; set; }
    }
    public enum FileStatus
    {
        None = 0, Draft = 1, Pending = 2, Approved = 3, Error = 4, Timeout = 5, _404 = 6, Loading = 7
    }
}

