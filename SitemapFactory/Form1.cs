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
        private String _errorMsg;
        private String _siteName;
        private String _subsidiaryName = "";

        public String _cspauthoringRoot = @"X:\";

        public Form1()
        {
            InitializeComponent();


            

           // this.textBox1.BorderStyle = BorderStyle.None;
            this.txtSiteName.BorderStyle = BorderStyle.None;

            this.MaximizeBox = false;

            this.btnLoadStatus.Visible = false;
            this.dataGridView1.AutoGenerateColumns = false;


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

            this.dataGridView1.Columns.Add(idColumn);
            this.dataGridView1.Columns.Add(fileStatusColumn);
            this.dataGridView1.Columns.Add(changefreqColumn);
            this.dataGridView1.Columns.Add(priorityColumn);
            this.dataGridView1.Columns.Add(lastmodColumn);
            this.dataGridView1.Columns.Add(locColumn);

            this.dataGridView1.Columns["ID"].Width = 30;
            this.dataGridView1.Columns["FileStatus"].Width = 70;
            this.dataGridView1.Columns["Changefreq"].Width = 70;
            this.dataGridView1.Columns["Priority"].Width = 50;
            this.dataGridView1.Columns["Lastmod"].Width = 70;

            this.adjustGridWidth();

            if (this.dataGridView1.Columns["Loc"].Width < 200) {
                this.dataGridView1.Columns["Loc"].Width = 200;
            }

            this.dataGridView1.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["FileStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["Changefreq"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["Priority"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["Lastmod"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["Loc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;


            this.dataGridView1.Columns["Changefreq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns["Priority"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1.RowHeadersVisible = false;

          //  doInit();
        }

        private void adjustGridWidth()
        {

            this.dataGridView1.Columns["Loc"].Width =
                                                this.dataGridView1.Width
                                                - this.dataGridView1.Columns["ID"].Width
                                                - this.dataGridView1.Columns["FileStatus"].Width
                                                - this.dataGridView1.Columns["Changefreq"].Width
                                                - this.dataGridView1.Columns["Priority"].Width
                                                - this.dataGridView1.Columns["Lastmod"].Width - 18;
        }

        private void doInit()
        {
            // http://www.microsoft.com/enterprise/en-us/default.aspx

            _siteName = this.txtSiteName.Text.Trim().ToLower();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^www.microsoft.com(/[a-zA-Z]{2}-[a-zA-Z]{2})?/enterprise(/[a-zA-Z]{2}-[a-zA-Z]{2})?/default.aspx$");
            var match = regex.Match(_siteName.Replace("http://", ""));

            if (match.Success)
            {
                _siteName = match.Result("$1").TrimStart('/');
                if (String.IsNullOrEmpty(_siteName))
                {
                    this._siteName = match.Result("$2").TrimStart('/');
                    this._siteName = "enterprise\\" + this._siteName;
                }
                else {
                    // en-gb
                    this._siteName = this._siteName + "\\enterprise";
                }


                if (!string.IsNullOrEmpty(this._siteName))
                {
                    this._subsidiaryName = this._siteName;
                    this._siteName = string.Format("{0}{1}\\SitePages\\", _cspauthoringRoot, this._siteName);
                }
                else
                {

                    this._subsidiaryName = "/enterprise";
                    this._siteName = string.Format("{0}SitePages\\", _cspauthoringRoot);
                }

                _errorMsg = string.Empty;
            }
            else
            {
                _errorMsg = "Please input a correct site name!";
            }
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


            if (!string.IsNullOrEmpty(this._errorMsg))
            {
                this.showMsgBox(this._errorMsg);
                return;
            }
        }

        private IEnumerable<URLEntry> loadSitePages(String path)
        {

            RecursionFolder r = new RecursionFolder();
            r.NotifyParentEvent += updateLogPanel;


            DelRecursionFolder drf = new DelRecursionFolder(r.Recursion);
            drf.BeginInvoke(path, new AsyncCallback(loadSitePagesComplete), r);

            return r.files;
        }

        private void showMsgBox(String errorMsg)
        {
            MessageBox.Show(errorMsg, "ERROR!");
            this.txtSiteName.Focus();
            this.txtSiteName.SelectAll();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.doInit();
            if (!string.IsNullOrEmpty(this._errorMsg))
            {
                this.showMsgBox(this._errorMsg);
                return;
            }

            this.dataGridView1.DataSource = null;

            this.dataGridView1.Visible = false;
            this.btnLoadStatus.Visible = false;
            this.richTextBox1.Visible = true;

            this.btnGo.Enabled = false;
            richTextBox1.Text = String.Empty;
            this.loadSitePages(this._siteName);
        }

        private void loadSitePagesComplete(IAsyncResult itfAR)
        {
            MethodInvoker action = delegate
            { this.btnGo.Enabled = true; };
            this.btnGo.BeginInvoke(action);

            var v = itfAR.AsyncState as RecursionFolder;
            if (v != null)
            {
                var urlEntryList = v.files;

                if (urlEntryList != null)
                {
                    // dispaly grid
                    this.bindData(urlEntryList);
                }
            }
        }

        private void bindData(List<URLEntry> urlEntryList)
        {
            MethodInvoker action = delegate
            {
                this.dataGridView1.DataSource = urlEntryList;
            };

            this.dataGridView1.Invoke(action);


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

                action = delegate
                {
                    this.dataGridView1.Visible = true;
                };

                this.dataGridView1.BeginInvoke(action);
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

            LoadFileStatus lfs = new LoadFileStatus();
            
            LoadFileDelegate lfd = new LoadFileDelegate(lfs.LoadFile);
            lfd.BeginInvoke(this.dataGridView1, new AsyncCallback(loadStatusComplete), lfs);
        }

        [STAThread]
        private void loadStatusComplete(IAsyncResult itfAR) {
            var v = itfAR.AsyncState as LoadFileStatus;


            MethodInvoker action = delegate
            { this.btnLoadStatus.Enabled = false; };

            this.btnLoadStatus.Invoke(action);

            action = delegate
            { this.btnGo.Enabled = true; };
            this.btnGo.Invoke(action);

            if (v.SiteMapXmlDocument != null)
            {
                Invoke((Action)(() => {
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

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this._formWidth = this.Width;
            this._formHeight = this.Height;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width < 450) this.Width = 650;
            if (this.Height < 450) this.Height = 450;

            var changedWidth = this.Width - this._formWidth;
            var changedHeight = this.Height - this._formHeight;

            this.btnGo.Left = this.btnGo.Left + changedWidth;
            this.btnLoadStatus.Left = this.btnLoadStatus.Left + changedWidth;
            this.panel1.Width = this.panel1.Width + changedWidth;

            this.txtSiteName.Width = this.panel1.Width - 55;

            this.dataGridView1.Width = this.panel1.Width;
            this.richTextBox1.Width = this.panel1.Width;
            this.dataGridView1.Height = this.dataGridView1.Height + changedHeight;
            this.richTextBox1.Height = this.richTextBox1.Height + changedHeight;

            this.adjustGridWidth();            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }
    }

    public delegate void LoadFileDelegate(DataGridView dataGridView);
    public delegate void DelRecursionFolder(String path);
    public delegate void NotifyParentDelegate(String msg);

    public class RecursionFolder
    {
        public List<URLEntry> files = null;

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

        public XDocument SiteMapXmlDocument {
            get {
                return this._siteMapDoc;
            }
        }

        public LoadFileStatus() {
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
                    if (we.Message.Contains("404")) {
                        row.Cells["FileStatus"].Value = FileStatus._404.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Red;
                    }
                    else if (we.Message.ToLower().Contains("timed out"))
                    {
                        row.Cells["FileStatus"].Value = FileStatus.Timeout.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Aqua;
                    }
                    else {

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

    public enum FileStatus
    {
        None = 0, Draft = 1, Pending = 2, Approved = 3, Error = 4, Timeout = 5, _404 = 6, Loading = 7
    }
}

