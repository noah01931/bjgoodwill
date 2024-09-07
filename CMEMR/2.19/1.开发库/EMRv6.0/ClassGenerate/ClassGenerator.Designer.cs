namespace ClassGenerate
{
    partial class ClassGenerator
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lblMsg = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.tabGP = new System.Windows.Forms.TabPage();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.lvTables = new System.Windows.Forms.ListView();
            this.colSelect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTableName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTableDecription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbOpition = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHOSPITAL_NO = new System.Windows.Forms.TextBox();
            this.ckIndex = new System.Windows.Forms.CheckBox();
            this.ckOracleSQL = new System.Windows.Forms.CheckBox();
            this.ckFullManage = new System.Windows.Forms.CheckBox();
            this.btnOnOperation = new System.Windows.Forms.Button();
            this.ckProc = new System.Windows.Forms.CheckBox();
            this.ckFullCache = new System.Windows.Forms.CheckBox();
            this.ckCache = new System.Windows.Forms.CheckBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.ckSelect = new System.Windows.Forms.CheckBox();
            this.ckDocument = new System.Windows.Forms.CheckBox();
            this.ckDataAccess = new System.Windows.Forms.CheckBox();
            this.ckModel = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox3.SuspendLayout();
            this.tabGP.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.gbOpition.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.treeView1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 456);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(12, 20);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 430);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(449, 9);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 19;
            // 
            // fbd
            // 
            this.fbd.SelectedPath = "r";
            // 
            // tabGP
            // 
            this.tabGP.Controls.Add(this.txtPreview);
            this.tabGP.Location = new System.Drawing.Point(4, 21);
            this.tabGP.Name = "tabGP";
            this.tabGP.Padding = new System.Windows.Forms.Padding(3);
            this.tabGP.Size = new System.Drawing.Size(673, 431);
            this.tabGP.TabIndex = 1;
            this.tabGP.Text = "生成预览";
            this.tabGP.UseVisualStyleBackColor = true;
            // 
            // txtPreview
            // 
            this.txtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreview.Location = new System.Drawing.Point(3, 3);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(664, 421);
            this.txtPreview.TabIndex = 1;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.lvTables);
            this.tabSetting.Controls.Add(this.gbOpition);
            this.tabSetting.Location = new System.Drawing.Point(4, 21);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(673, 431);
            this.tabSetting.TabIndex = 0;
            this.tabSetting.Text = "生成设置";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // lvTables
            // 
            this.lvTables.CheckBoxes = true;
            this.lvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSelect,
            this.colTableName,
            this.colTableDecription});
            this.lvTables.FullRowSelect = true;
            this.lvTables.GridLines = true;
            this.lvTables.Location = new System.Drawing.Point(55, 134);
            this.lvTables.Name = "lvTables";
            this.lvTables.Size = new System.Drawing.Size(569, 281);
            this.lvTables.TabIndex = 22;
            this.lvTables.UseCompatibleStateImageBehavior = false;
            this.lvTables.View = System.Windows.Forms.View.Details;
            // 
            // colSelect
            // 
            this.colSelect.Text = "选择";
            this.colSelect.Width = 37;
            // 
            // colTableName
            // 
            this.colTableName.Text = "表名";
            this.colTableName.Width = 247;
            // 
            // colTableDecription
            // 
            this.colTableDecription.Text = "表描述";
            this.colTableDecription.Width = 273;
            // 
            // gbOpition
            // 
            this.gbOpition.Controls.Add(this.label1);
            this.gbOpition.Controls.Add(this.txtHOSPITAL_NO);
            this.gbOpition.Controls.Add(this.ckIndex);
            this.gbOpition.Controls.Add(this.ckOracleSQL);
            this.gbOpition.Controls.Add(this.ckFullManage);
            this.gbOpition.Controls.Add(this.btnOnOperation);
            this.gbOpition.Controls.Add(this.ckProc);
            this.gbOpition.Controls.Add(this.ckFullCache);
            this.gbOpition.Controls.Add(this.ckCache);
            this.gbOpition.Controls.Add(this.btnFilter);
            this.gbOpition.Controls.Add(this.ckSelect);
            this.gbOpition.Controls.Add(this.ckDocument);
            this.gbOpition.Controls.Add(this.ckDataAccess);
            this.gbOpition.Controls.Add(this.ckModel);
            this.gbOpition.Controls.Add(this.btnGenerate);
            this.gbOpition.Controls.Add(this.btnPreview);
            this.gbOpition.Controls.Add(this.txtNamespace);
            this.gbOpition.Controls.Add(this.label3);
            this.gbOpition.Controls.Add(this.txtPrefix);
            this.gbOpition.Controls.Add(this.label4);
            this.gbOpition.Location = new System.Drawing.Point(55, 9);
            this.gbOpition.Name = "gbOpition";
            this.gbOpition.Size = new System.Drawing.Size(569, 119);
            this.gbOpition.TabIndex = 21;
            this.gbOpition.TabStop = false;
            this.gbOpition.Text = "生成选项";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "HOSPITAL_NO";
            // 
            // txtHOSPITAL_NO
            // 
            this.txtHOSPITAL_NO.Location = new System.Drawing.Point(450, 4);
            this.txtHOSPITAL_NO.Name = "txtHOSPITAL_NO";
            this.txtHOSPITAL_NO.Size = new System.Drawing.Size(85, 21);
            this.txtHOSPITAL_NO.TabIndex = 28;
            this.txtHOSPITAL_NO.Text = "01110002";
            // 
            // ckIndex
            // 
            this.ckIndex.AutoSize = true;
            this.ckIndex.Location = new System.Drawing.Point(450, 31);
            this.ckIndex.Name = "ckIndex";
            this.ckIndex.Size = new System.Drawing.Size(72, 16);
            this.ckIndex.TabIndex = 27;
            this.ckIndex.Text = "修改主键";
            this.ckIndex.UseVisualStyleBackColor = true;
            // 
            // ckOracleSQL
            // 
            this.ckOracleSQL.AutoSize = true;
            this.ckOracleSQL.Location = new System.Drawing.Point(189, 88);
            this.ckOracleSQL.Name = "ckOracleSQL";
            this.ckOracleSQL.Size = new System.Drawing.Size(78, 16);
            this.ckOracleSQL.TabIndex = 26;
            this.ckOracleSQL.Text = "OracleSQL";
            this.ckOracleSQL.UseVisualStyleBackColor = true;
            // 
            // ckFullManage
            // 
            this.ckFullManage.AutoSize = true;
            this.ckFullManage.Location = new System.Drawing.Point(297, 58);
            this.ckFullManage.Name = "ckFullManage";
            this.ckFullManage.Size = new System.Drawing.Size(84, 16);
            this.ckFullManage.TabIndex = 25;
            this.ckFullManage.Text = "FullManage";
            this.ckFullManage.UseVisualStyleBackColor = true;
            // 
            // btnOnOperation
            // 
            this.btnOnOperation.Location = new System.Drawing.Point(102, 84);
            this.btnOnOperation.Name = "btnOnOperation";
            this.btnOnOperation.Size = new System.Drawing.Size(75, 23);
            this.btnOnOperation.TabIndex = 24;
            this.btnOnOperation.Text = "术中";
            this.btnOnOperation.UseVisualStyleBackColor = true;
            this.btnOnOperation.Click += new System.EventHandler(this.btnOnOperation_Click);
            // 
            // ckProc
            // 
            this.ckProc.AutoSize = true;
            this.ckProc.Location = new System.Drawing.Point(140, 58);
            this.ckProc.Name = "ckProc";
            this.ckProc.Size = new System.Drawing.Size(72, 16);
            this.ckProc.TabIndex = 23;
            this.ckProc.Text = "存储过程";
            this.ckProc.UseVisualStyleBackColor = true;
            // 
            // ckFullCache
            // 
            this.ckFullCache.AutoSize = true;
            this.ckFullCache.Location = new System.Drawing.Point(382, 59);
            this.ckFullCache.Name = "ckFullCache";
            this.ckFullCache.Size = new System.Drawing.Size(78, 16);
            this.ckFullCache.TabIndex = 22;
            this.ckFullCache.Text = "FullCache";
            this.ckFullCache.UseVisualStyleBackColor = true;
            // 
            // ckCache
            // 
            this.ckCache.AutoSize = true;
            this.ckCache.Location = new System.Drawing.Point(466, 56);
            this.ckCache.Name = "ckCache";
            this.ckCache.Size = new System.Drawing.Size(54, 16);
            this.ckCache.TabIndex = 21;
            this.ckCache.Text = "Cache";
            this.ckCache.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(16, 84);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 20;
            this.btnFilter.Text = "筛选";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // ckSelect
            // 
            this.ckSelect.AutoSize = true;
            this.ckSelect.Location = new System.Drawing.Point(16, 58);
            this.ckSelect.Name = "ckSelect";
            this.ckSelect.Size = new System.Drawing.Size(60, 16);
            this.ckSelect.TabIndex = 19;
            this.ckSelect.Text = "所有表";
            this.ckSelect.UseVisualStyleBackColor = true;
            this.ckSelect.CheckedChanged += new System.EventHandler(this.ckSelect_CheckedChanged);
            // 
            // ckDocument
            // 
            this.ckDocument.AutoSize = true;
            this.ckDocument.Location = new System.Drawing.Point(479, 88);
            this.ckDocument.Name = "ckDocument";
            this.ckDocument.Size = new System.Drawing.Size(84, 16);
            this.ckDocument.TabIndex = 0;
            this.ckDocument.Text = "数据库文档";
            this.ckDocument.UseVisualStyleBackColor = true;
            // 
            // ckDataAccess
            // 
            this.ckDataAccess.AutoSize = true;
            this.ckDataAccess.Location = new System.Drawing.Point(218, 58);
            this.ckDataAccess.Name = "ckDataAccess";
            this.ckDataAccess.Size = new System.Drawing.Size(84, 16);
            this.ckDataAccess.TabIndex = 0;
            this.ckDataAccess.Text = "数据访问层";
            this.ckDataAccess.UseVisualStyleBackColor = true;
            // 
            // ckModel
            // 
            this.ckModel.AutoSize = true;
            this.ckModel.Checked = true;
            this.ckModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckModel.Location = new System.Drawing.Point(78, 58);
            this.ckModel.Name = "ckModel";
            this.ckModel.Size = new System.Drawing.Size(60, 16);
            this.ckModel.TabIndex = 0;
            this.ckModel.Text = "实体类";
            this.ckModel.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(395, 84);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 17;
            this.btnGenerate.Text = "生成文件";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.Location = new System.Drawing.Point(291, 84);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 18;
            this.btnPreview.Text = "预览效果";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(99, 25);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(194, 21);
            this.txtNamespace.TabIndex = 14;
            this.txtNamespace.Text = "JHEMR.JHCommonLib.Entity.TableObject";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "名称空间";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(99, -1);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(194, 21);
            this.txtPrefix.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "前缀";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabGP);
            this.tabControl1.Location = new System.Drawing.Point(252, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 456);
            this.tabControl1.TabIndex = 13;
            // 
            // ClassGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 480);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Name = "ClassGenerator";
            this.Text = "代码生成器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClassGenerator_FormClosed);
            this.Load += new System.EventHandler(this.ClassGenerator_Load);
            this.groupBox3.ResumeLayout(false);
            this.tabGP.ResumeLayout(false);
            this.tabGP.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.gbOpition.ResumeLayout(false);
            this.gbOpition.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TabPage tabGP;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.ListView lvTables;
        private System.Windows.Forms.ColumnHeader colSelect;
        private System.Windows.Forms.ColumnHeader colTableName;
        private System.Windows.Forms.ColumnHeader colTableDecription;
        private System.Windows.Forms.GroupBox gbOpition;
        private System.Windows.Forms.CheckBox ckDataAccess;
        private System.Windows.Forms.CheckBox ckModel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox ckSelect;
        private System.Windows.Forms.CheckBox ckDocument;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.CheckBox ckCache;
        private System.Windows.Forms.CheckBox ckFullCache;
        private System.Windows.Forms.CheckBox ckProc;
        private System.Windows.Forms.Button btnOnOperation;
        private System.Windows.Forms.CheckBox ckFullManage;
        private System.Windows.Forms.CheckBox ckOracleSQL;
        private System.Windows.Forms.CheckBox ckIndex;
        private System.Windows.Forms.TextBox txtHOSPITAL_NO;
        private System.Windows.Forms.Label label1;
    }
}