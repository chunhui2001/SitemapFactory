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

            this.btnLog.Padding = new Padding(0, 0, 0, 0);
            this.btnLog.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);

            this.cbbResultList.Visible = false;
            this.cbbResultList.FlatStyle = FlatStyle.Flat;
            this.cbbResultList.BackColor = System.Drawing.Color.Green;
            this.cbbResultList.ForeColor = System.Drawing.Color.White;

            ccb = new CheckedCombobox(this.panel2, this, this.getSubsidiaryList(this.Outputdir), this._cspauthoringRoot);
            this.panel2.Visible = false;
            this.richTextBox1.Visible = true;
            this.btnResult.Visible = false;
            this.richTextBox1.BorderStyle = BorderStyle.FixedSingle;

            this.MaximizeBox = false;
            this.btnLoadStatus.Visible = false;

            //doInit();
        }

        void CSPAUTHORING_DRIVE_NAME_Click(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            txt.Text = string.Empty;
            txt.Width = 3000;
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

            this._subsidiaryList = this.ccb._subsidiaryEntry;

            this.ccb._cspauthoringRoot = this._cspauthoringRoot;
            this.ccb._subsidiaryEntry = this.getSubsidiaryList(this.Outputdir);
        }

        private SubsidiaryEntry[] getSubsidiaryList(String outputDir)
        {
            return new SubsidiaryEntry[] { 
new SubsidiaryEntry(){ Name = "ar-eg"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir}, 
new SubsidiaryEntry(){ Name = "ar-gulf"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ar-iq"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ar-ly"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ar-middleeast"		   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "cs-cz"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "da-dk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "de-at"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "DE-CH"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "de-de"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "el-gr"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "EN-AU"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-bd"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "in-in"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-br"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-ca"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-cb"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-cee"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-eg"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-esa"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-gb"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-gh"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-gm"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-gulf"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-hk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-ie"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-in"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "En-ir"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-jo"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-lb"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-lk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-lr"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "EN-MY"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-ng"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "EN-NZ"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "EN-PH"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-pk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-sa"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "EN-SG"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-sl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-xl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "en-za"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-ar"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-cl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-co"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-es"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-mx"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "es-xl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fi-fi"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-africa"			   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-be"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-ca"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-ch"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-dz"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-fr"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-ma"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-tn"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "fr-wca"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "he-il"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "hu-hu"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ID-ID"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "it-it"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ja-jp"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "KO-KR"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "nb-no"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "nl-be"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "nl-nl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pl-pl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pt-ao"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pt-br"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pt-cv"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pt-mz"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "pt-pt"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ro-ro"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ru-ru"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ru-uk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "ru-xl"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "sk-sk"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "sv-se"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "TH-TH"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "tr-tr"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "uk-ua"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "VI-VN"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "zh-cn"				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},
new SubsidiaryEntry(){ Name = "zh-tw" 				   , SitemapLastModifyDate = DateTime.Parse( "1970-01-01"), OutputDIr = outputDir},

        }.OrderBy(e => e.Name).ToArray();
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

            // this.ccb.txtDropDownInput.Enabled = false;
            // this.ccb.btnDropDown.Enabled = false;
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



            this.loadSitePages(this._selectedSubsidiaryList);
        }

        private void loadSitePages(String[] selectedSubsidiaryList)
        {
            RecursionFolder r = new RecursionFolder(selectedSubsidiaryList, this._cspauthoringRoot);
            r.PrintMessage += updateLogPanel;

            DelRecursion drf = new DelRecursion(r.Recursion);
            drf.BeginInvoke(new AsyncCallback(loadSitePagesComplete), r);
        }

        private void loadSitePagesComplete(IAsyncResult itfAR)
        {
            MethodInvoker action = null;

            action = delegate
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
                var result = v.result;

                foreach (var item in result)
                {
                    var urlEntryList = item.Value;
                    var subsidiaryName = item.Key;


                    if (urlEntryList != null)
                    {
                        // create grid
                        var grid = createDataGridView(subsidiaryName);

                        action = delegate
                        {
                            this.cbbResultList.Items.Add(subsidiaryName);
                            var isVisible = false;

                            if (this.cbbResultList.Items.Count == 1)
                            {
                                this.cbbResultList.SelectedIndex = 0;
                                isVisible = true;
                            }

                            //if (this.cbbResultList.Items.Count == _selectedSubsidiaryList.Length)
                            //{
                            //    MethodInvoker action3 = delegate
                            //    {
                            //        this.ccb.txtDropDownInput.Enabled = true;
                            //    };
                            //    this.ccb.txtDropDownInput.Invoke(action3);


                            //    MethodInvoker action4 = delegate
                            //    {
                            //        this.ccb.btnDropDown.Enabled = true;
                            //    };
                            //    this.ccb.btnDropDown.Invoke(action4);
                            //}

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

                        this.bindData(urlEntryList, subsidiaryName);
                    }
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

        private void updateLogPanel(String msg, System.Drawing.Color color, bool isNewLine)
        {
            MethodInvoker action = delegate
            {
                this.richTextBox1.AppendText(msg + (isNewLine ? Environment.NewLine : ""), color);
                //this.richTextBox1.Focus();
            };
            richTextBox1.BeginInvoke(action);


            //action = delegate
            //{
            //    richTextBox1.SelectionStart = richTextBox1.Text.Length+2;
            //    richTextBox1.ScrollToCaret();
            //};
            //richTextBox1.BeginInvoke(action);

            
        }


        private void btnLoadStatus_Click(object sender, EventArgs e)
        {
            List<DataGridView> gridList = new List<DataGridView>();
            foreach (var item in this.panel_Grid.Controls)
            {
                var grid = item as DataGridView;
                if (grid != null)
                {
                    gridList.Add(grid);
                }
            }

            if (gridList.Count > 0)
            {

                this.btnGo.Enabled = false;

                MethodInvoker action = delegate
                { this.btnLoadStatus.Enabled = false; };

                this.btnLoadStatus.Invoke(action);


                LoadFileStatus lfs = new LoadFileStatus(gridList);
                lfs.PrintMessage += updateLogPanel;
                lfs.TurnOnSubName += lfs_TurnOnSubName;

                LoadFileDelegate lfd = new LoadFileDelegate(lfs.LoadFile);
                lfd.BeginInvoke(new AsyncCallback(loadStatusComplete), lfs);
            }
        }

        void lfs_TurnOnSubName(string subsidiaryName)
        {
            var index = -1;
            for (int i = 0; i < this.cbbResultList.Items.Count; i++)
            {
                if (this.cbbResultList.Items[i].ToString() == subsidiaryName)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                MethodInvoker action = delegate
                {
                    this.cbbResultList.SelectedIndex = index;
                };

                this.cbbResultList.Invoke(action);
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

            if (v.Result != null)
            {
                // Save the result to outputdir
                foreach (var item in v.Result)
                {
                    var fileFullName = HelperClass.getOutputSitemapFileName(item.Key, this.Outputdir);

                    if (System.IO.File.Exists(fileFullName))
                    {
                        System.IO.File.Delete(fileFullName);
                    }

                    try
                    {
                        if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileFullName)))
                        {
                            // create directory
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileFullName));
                        }


                        item.Value.Save(fileFullName);

                        this.updateLogPanel("Saving " + fileFullName + " Successful..", System.Drawing.Color.Green, true);

                        // Update checkbox text
                        if (this.ccb.panel.Visible)
                        {
                            var now = DateTime.Now;
                            foreach (var checkbox in this.ccb.panel.Controls)
                            {
                                var cbx = checkbox as CheckBox;
                                if (cbx != null && cbx.Name == item.Key)
                                {
                                    cbx.Text = cbx.Name + "-" + DateTime.Now.ToString("yy.MM.dd");
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                        this.updateLogPanel("Saving " + fileFullName + " Failed: " + e.Message, System.Drawing.Color.Red, true);
                    }
                }
            }
        }

        public String Outputdir = "D:\\EPGSitemapOutputDir";
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
            this.richTextBox1.Focus();

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

        private void CSPAUTHORING_DRIVE_NAME_Click_1(object sender, EventArgs e)
        {
            var txt = sender as ToolStripTextBox;

            txt.Text = string.Empty;
        }

        private void OUTPUT_DIR_Click(object sender, EventArgs e)
        {

            var txt = sender as ToolStripTextBox;

            txt.Text = string.Empty;
        }
    }


    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
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

        public static String getOutputSitemapFileName(String subsidiaryName, String outputDir)
        {

            var fileFullName = outputDir + "\\sitemap" + ((String.IsNullOrEmpty(subsidiaryName) ? "" : "_" + subsidiaryName) + ".xml")
                                                    .Replace("\\enterprise", "").Replace("enterprise\\", "");
            return fileFullName;
        }
    }

    public delegate void LoadFileDelegate();
    public delegate void DelRecursion();
    public delegate void PrintMessage(String msg, System.Drawing.Color color, bool isNewLine);
    public delegate void TurnOnSubName(String subsidiaryName);

    public class RecursionFolder
    {
        public List<URLEntry> files = null;
        public String subsidiaryName = null;
        public Dictionary<String, List<URLEntry>> result = null;
        private String _cspauthoringRoot = null;

        public event PrintMessage PrintMessage;


        public RecursionFolder(String[] subsidiaryList, String cspauthoringRoot)
        {
            _cspauthoringRoot = cspauthoringRoot;
            files = new List<URLEntry>();

            result = new Dictionary<string, List<URLEntry>>();
            foreach (var subName in subsidiaryList)
            {
                if (!result.ContainsKey(subName))
                {
                    result.Add(subName, new List<URLEntry>());
                }
            }
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
                                "-old.xml", " - old.xml", "thumbs.db"};


            foreach (var item in skips)
            {
                if (path.ToLower().IndexOf(item.ToLower()) != -1)
                {
                    return true;
                }
            }

            return false;
        }


        public void Recursion()
        {
            foreach (var item in result)
            {
                var subsidiaryName = item.Key;
                var path = string.Empty;

                if (subsidiaryName == "en-gb")
                {
                    path = string.Format("{0}{1}\\enterprise\\sitepages", _cspauthoringRoot, subsidiaryName);
                }
                else
                {

                    path = string.Format("{0}enterprise\\{1}\\sitepages", _cspauthoringRoot, subsidiaryName);
                }

                this.Recursion(path, subsidiaryName);
            }
        }


        public void Recursion(String path, String subsidiaryName)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

            if (di.Exists)
            {
                var filesList = di.GetFiles();

                for (int i = 0; i < filesList.Length; i++)
                {
                    var item = filesList[i];

                    var skip = "";
                    if (!isSkip(item.FullName))
                        this.result[subsidiaryName].Add(new URLEntry() { File = item, FileStatus = FileStatus.None, ID = this.result[subsidiaryName].Count + 1 });
                    else
                        skip = " [SKIP]";

                    if (PrintMessage != null)
                        PrintMessage(item.FullName + skip, System.Drawing.Color.Black, true);
                }

                foreach (var item in di.GetDirectories())
                {
                    Recursion(item.FullName, subsidiaryName);
                }
            }
        }



    }

    public class LoadFileStatus
    {
        private List<DataGridView> _gridList;
        private Dictionary<string, XDocument> _result;

        public event PrintMessage PrintMessage;
        public event TurnOnSubName TurnOnSubName;

        public Dictionary<string, XDocument> Result
        {
            get
            {
                return this._result;
            }
        }

        public LoadFileStatus(List<DataGridView> gridList)
        {
            // xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9""

            this._gridList = gridList;

            _result = new Dictionary<string, XDocument>();
            foreach (var item in this._gridList)
            {
                this.Result.Add(item.Name, XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset>
   
</urlset>"));
            }
        }

        byte[] pageContent = null;
        bool downloadCompleted = false;
        HttpStatusCode downloadStatus = HttpStatusCode.NotFound;

        public void LoadFile()
        {
            foreach (var g in this._gridList)
            {
                var dataGridView = g;

                if (dataGridView.Rows.Count == 0) return;

                if (TurnOnSubName != null)
                    TurnOnSubName(dataGridView.Name);

                if (this.PrintMessage != null)
                    this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + g.Name + " Load status is being started...."
                        , System.Drawing.Color.Fuchsia, true);

                #region loading status
                foreach (var item in dataGridView.Rows)
                {
                    #region process the grid
                    DataGridViewRow row = item as DataGridViewRow;

                    row.Cells["FileStatus"].Value = FileStatus.Loading;
                    row.Cells["FileStatus"].Style.ForeColor = Color.Green;

                    // get page by url 
                    String currUrl = row.Cells["Loc"].Value.ToString();

                    WebClient wc = new WebClient();

                    wc.DownloadDataCompleted += wc_DownloadDataCompleted;

                    try
                    {
                        /*
                        HttpWebRequest request = WebRequest.Create(currUrl) as HttpWebRequest;
                        request.Method = "HEAD";
                        request.Timeout = 120000;

                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        HttpStatusCode status = response.StatusCode;
                        */
                        pageContent = null;
                        downloadCompleted = false;
                        downloadStatus = HttpStatusCode.NotFound;

                        try
                        {
                            wc.DownloadDataAsync(new Uri(currUrl), row.Cells["FileStatus"]);
                        }
                        catch
                        {
                            downloadStatus = HttpStatusCode.NotFound;
                            downloadCompleted = true;
                            row.Cells["FileStatus"].Value = FileStatus._404.ToString();
                            row.Cells["FileStatus"].Style.ForeColor = Color.Red;

                            if (this.PrintMessage != null)
                                this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + currUrl + " 404 .."
                                    , System.Drawing.Color.Red, true);
                        }

                        //pageContent = wc.DownloadData(currUrl);


                        Thread.Sleep(1200);

                        #region show download process
                        bool tag = false;
                        while (!downloadCompleted)
                        {
                            Thread.Sleep(50);
                            // print process bar
                            if (this.PrintMessage != null)
                                this.PrintMessage(">", System.Drawing.Color.Turquoise, false);

                            tag = true;
                        }
                        #endregion show download process


                        if (tag && this.PrintMessage != null)
                            this.PrintMessage("", System.Drawing.Color.White, true);


                        if (downloadStatus == HttpStatusCode.OK) //status == HttpStatusCode.OK)
                        {
                            #region approve
                            row.Cells["FileStatus"].Value = FileStatus.Approved;
                            row.Cells["FileStatus"].Style.ForeColor = Color.Blue;

                            XElement ele = XElement.Parse(string.Format(@"<url>
      <loc>{0}</loc>
      <lastmod>{1}</lastmod>
      <changefreq>{2}</changefreq>
      <priority>{3}</priority>
   </url>", currUrl, row.Cells["Lastmod"].Value.ToString(), row.Cells["Changefreq"].Value.ToString(), row.Cells["Priority"].Value.ToString()));

                            try
                            {
                                // Add to document
                                this._result[g.Name].Root.AddFirst(ele);


                                if (this.PrintMessage != null)
                                    this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + currUrl + " " + " is on live.."
                                        , System.Drawing.Color.Salmon, true);
                            }
                            catch (Exception e)
                            {
                                if (this.PrintMessage != null)
                                    this.PrintMessage(e.Message, System.Drawing.Color.Red, true);
                            }
                            #endregion approve
                        }
                        else
                        {
                            #region not approve
                            row.Cells["FileStatus"].Value = FileStatus._404.ToString();
                            row.Cells["FileStatus"].Style.ForeColor = Color.Red;


                            if (this.PrintMessage != null)
                                this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + currUrl + " 404"
                                    , System.Drawing.Color.Red, true);
                            #endregion not approve
                        }
                    }
                    catch (WebException we)
                    {
                        downloadStatus = HttpStatusCode.NotFound;
                        downloadCompleted = true;
                        #region
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

                        if (this.PrintMessage != null)
                            this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + currUrl + " " + we.Message
                                , System.Drawing.Color.Red, true);
                        #endregion
                    }
                    catch (Exception ee)
                    {
                        downloadStatus = HttpStatusCode.NotFound;
                        downloadCompleted = true;
                        #region
                        row.Cells["FileStatus"].Value = FileStatus.Error.ToString();
                        row.Cells["FileStatus"].Style.ForeColor = Color.Red;

                        if (this.PrintMessage != null)
                            this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + currUrl + " " + ee.Message
                                , System.Drawing.Color.Red, true);
                        #endregion
                    }



                    MethodInvoker action = delegate
                    {
                        dataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    };

                    dataGridView.BeginInvoke(action);

                    #endregion process the grid
                }
                #endregion loading status

                if (this.PrintMessage != null)
                    this.PrintMessage("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "] " + g.Name + " Loading status completed...."
                        , System.Drawing.Color.Fuchsia, true);
            }
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var cell = e.UserState as DataGridViewTextBoxCell;
            
            //downloadStatus = HttpStatusCode.OK;
            try
            {
                if (e.Result != null && e.Result.Length > 0)
                {
                    downloadStatus = HttpStatusCode.OK;
                    pageContent = e.Result;
                }
                else
                {
                    downloadStatus = HttpStatusCode.NotFound;
                    pageContent = null;
                }
            }
            catch (Exception)
            {
                downloadStatus = HttpStatusCode.NotFound;
                
            }
            

            downloadCompleted = true;
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
        private String _outputDir;
        private DateTime _sitemapLastModifyDate;
        public String Name { get; set; }
        public String OutputDIr
        {
            get
            {
                return this._outputDir;
            }
            set
            {
                this._outputDir = value;
            }
        }
        public DateTime SitemapLastModifyDate
        {
            get
            {
                var fileName = HelperClass.getOutputSitemapFileName(this.Name, _outputDir);

                if (System.IO.File.Exists(fileName))
                {
                    return new System.IO.FileInfo(fileName).LastWriteTime;
                }

                return this._sitemapLastModifyDate;
            }

            set
            {
                this._sitemapLastModifyDate = value;
            }
        }
    }
    public enum FileStatus
    {
        None = 0, Draft = 1, Pending = 2, Approved = 3, Error = 4, Timeout = 5, _404 = 6, Loading = 7
    }
}

