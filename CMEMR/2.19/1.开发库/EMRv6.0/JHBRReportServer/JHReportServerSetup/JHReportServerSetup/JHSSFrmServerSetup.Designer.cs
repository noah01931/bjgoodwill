namespace JHEMR.JHReportServerSetup
{
    partial class JHSSFrmServerSetup
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JHSSFrmServerSetup));
            this.wc_setup = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.chk_treaty = new DevExpress.XtraEditors.CheckEdit();
            this.FinishWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.txt_way = new DevExpress.XtraEditors.TextEdit();
            this.btn_browser = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.wizardPage3 = new DevExpress.XtraWizard.WizardPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.chk_web = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.wc_setup)).BeginInit();
            this.wc_setup.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_treaty.Properties)).BeginInit();
            this.FinishWizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_way.Properties)).BeginInit();
            this.wizardPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_web.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wc_setup
            // 
            this.wc_setup.CancelText = "取消";
            this.wc_setup.Controls.Add(this.welcomeWizardPage1);
            this.wc_setup.Controls.Add(this.wizardPage1);
            this.wc_setup.Controls.Add(this.FinishWizardPage1);
            this.wc_setup.Controls.Add(this.wizardPage2);
            this.wc_setup.Controls.Add(this.wizardPage3);
            this.wc_setup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wc_setup.FinishText = "&完成";
            this.wc_setup.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wc_setup.HeaderImage")));
            this.wc_setup.Image = ((System.Drawing.Image)(resources.GetObject("wc_setup.Image")));
            this.wc_setup.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.wc_setup.Location = new System.Drawing.Point(0, 0);
            this.wc_setup.Name = "wc_setup";
            this.wc_setup.NextText = "&下一步 >";
            this.wc_setup.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.FinishWizardPage1});
            this.wc_setup.PreviousText = "< &上一步";
            this.wc_setup.Size = new System.Drawing.Size(710, 459);
            this.wc_setup.Text = "呃我认为";
            this.wc_setup.TitleImage = ((System.Drawing.Image)(resources.GetObject("wc_setup.TitleImage")));
            this.wc_setup.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wc_setup_SelectedPageChanging);
            this.wc_setup.CancelClick += new System.ComponentModel.CancelEventHandler(this.wc_setup_CancelClick);
            this.wc_setup.FinishClick += new System.ComponentModel.CancelEventHandler(this.wc_setup_FinishClick);
            this.wc_setup.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wc_setup_NextClick);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.IntroductionText = "服务安装向导将会引导您一步一步安装嘉和报表服务JDK，使原本繁杂的服务安装变得如此简单。\r\n";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "点击下一步以继续";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(493, 326);
            this.welcomeWizardPage1.Text = "嘉和统计报表服务安装";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.memoEdit1);
            this.wizardPage1.Controls.Add(this.chk_treaty);
            this.wizardPage1.DescriptionText = "嘉和报表服务安装法律条文";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(678, 314);
            this.wizardPage1.Text = "嘉和统计报表服务安装协议";
            // 
            // memoEdit1
            // 
            this.memoEdit1.EditValue = resources.GetString("memoEdit1.EditValue");
            this.memoEdit1.Location = new System.Drawing.Point(2, 3);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(675, 272);
            this.memoEdit1.TabIndex = 0;
            // 
            // chk_treaty
            // 
            this.chk_treaty.Location = new System.Drawing.Point(3, 295);
            this.chk_treaty.Name = "chk_treaty";
            this.chk_treaty.Properties.Caption = "我同意以上协议";
            this.chk_treaty.Size = new System.Drawing.Size(111, 19);
            this.chk_treaty.TabIndex = 11;
            // 
            // FinishWizardPage1
            // 
            this.FinishWizardPage1.Controls.Add(this.chk_web);
            this.FinishWizardPage1.FinishText = resources.GetString("FinishWizardPage1.FinishText");
            this.FinishWizardPage1.Name = "FinishWizardPage1";
            this.FinishWizardPage1.ProceedText = "要关闭请点击\"完成\"按钮";
            this.FinishWizardPage1.Size = new System.Drawing.Size(493, 326);
            this.FinishWizardPage1.Text = "嘉和报表服务安装向导结束";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.txt_way);
            this.wizardPage2.Controls.Add(this.btn_browser);
            this.wizardPage2.Controls.Add(this.labelControl1);
            this.wizardPage2.DescriptionText = "选择要将嘉和报表服务安装的位置";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(678, 314);
            this.wizardPage2.Text = "选择安装路径";
            // 
            // txt_way
            // 
            this.txt_way.EditValue = "";
            this.txt_way.Location = new System.Drawing.Point(166, 126);
            this.txt_way.Name = "txt_way";
            this.txt_way.Properties.ReadOnly = true;
            this.txt_way.Size = new System.Drawing.Size(382, 21);
            this.txt_way.TabIndex = 3;
            this.txt_way.TextChanged += new System.EventHandler(this.txt_way_TextChanged);
            // 
            // btn_browser
            // 
            this.btn_browser.Location = new System.Drawing.Point(554, 125);
            this.btn_browser.Name = "btn_browser";
            this.btn_browser.Size = new System.Drawing.Size(75, 23);
            this.btn_browser.TabIndex = 2;
            this.btn_browser.Text = "浏览···";
            this.btn_browser.Click += new System.EventHandler(this.btn_browser_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(72, 129);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "请选择安装路径";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.groupBox1);
            this.wizardPage3.DescriptionText = "显示报表服务JDK安装的进度情况";
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(678, 314);
            this.wizardPage3.Text = "安装进度";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.progressBarControl1);
            this.groupBox1.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 308);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "安装进度";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Location = new System.Drawing.Point(506, 266);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(132, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "正在注册组件，请稍候···";
            this.labelControl3.Visible = false;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(1, 284);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressBarControl1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.progressBarControl1.Properties.Maximum = 300;
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Size = new System.Drawing.Size(669, 23);
            this.progressBarControl1.TabIndex = 0;
            this.progressBarControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Warning;
            this.progressBarControl1.ToolTipTitle = "etrwertert";
            this.progressBarControl1.UseWaitCursor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(694, 355);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "2008101512013223871.jpg");
            this.imageCollection1.Images.SetKeyName(1, "3320946_045308899836_2.jpg");
            this.imageCollection1.Images.SetKeyName(2, "crm05a.jpg");
            // 
            // timer2
            // 
            this.timer2.Interval = 2500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // chk_web
            // 
            this.chk_web.Location = new System.Drawing.Point(191, 146);
            this.chk_web.Name = "chk_web";
            this.chk_web.Properties.Caption = "打开演示中心";
            this.chk_web.Size = new System.Drawing.Size(117, 19);
            this.chk_web.TabIndex = 0;
            // 
            // JHSSFrmServerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 459);
            this.Controls.Add(this.wc_setup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "JHSSFrmServerSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "嘉和统计报表JDK安装";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JHSSFrmServerSetup_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.wc_setup)).EndInit();
            this.wc_setup.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_treaty.Properties)).EndInit();
            this.FinishWizardPage1.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_way.Properties)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_web.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wc_setup;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage FinishWizardPage1;
        private DevExpress.XtraEditors.CheckEdit chk_treaty;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraWizard.WizardPage wizardPage3;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txt_way;
        private DevExpress.XtraEditors.SimpleButton btn_browser;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.Timer timer2;
        private DevExpress.XtraEditors.CheckEdit chk_web;
    }
}

