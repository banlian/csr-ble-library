namespace CsrBleLibrary
{
    partial class CsrBleUserMultiControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxDeviceConnect = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRssiDbm = new System.Windows.Forms.ComboBox();
            this.listBoxDeviceSearch = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.buttonEnum = new System.Windows.Forms.Button();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonUserStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainerMain.Panel1.Controls.Add(this.splitter1);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainerMain.Size = new System.Drawing.Size(609, 424);
            this.splitContainerMain.SplitterDistance = 349;
            this.splitContainerMain.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listBoxDeviceConnect);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(346, 424);
            this.splitContainer2.SplitterDistance = 130;
            this.splitContainer2.TabIndex = 1;
            // 
            // listBoxDeviceConnect
            // 
            this.listBoxDeviceConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDeviceConnect.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxDeviceConnect.FormattingEnabled = true;
            this.listBoxDeviceConnect.ItemHeight = 28;
            this.listBoxDeviceConnect.Location = new System.Drawing.Point(0, 0);
            this.listBoxDeviceConnect.Name = "listBoxDeviceConnect";
            this.listBoxDeviceConnect.Size = new System.Drawing.Size(346, 130);
            this.listBoxDeviceConnect.TabIndex = 1;
            this.listBoxDeviceConnect.SelectedIndexChanged += new System.EventHandler(this.listBoxDeviceConnect_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxRssiDbm);
            this.groupBox1.Controls.Add(this.listBoxDeviceSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 290);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "信号强度";
            // 
            // comboBoxRssiDbm
            // 
            this.comboBoxRssiDbm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxRssiDbm.FormattingEnabled = true;
            this.comboBoxRssiDbm.Location = new System.Drawing.Point(66, 267);
            this.comboBoxRssiDbm.Name = "comboBoxRssiDbm";
            this.comboBoxRssiDbm.Size = new System.Drawing.Size(121, 20);
            this.comboBoxRssiDbm.TabIndex = 1;
            this.comboBoxRssiDbm.SelectedIndexChanged += new System.EventHandler(this.comboBoxRssiDbm_SelectedIndexChanged);
            // 
            // listBoxDeviceSearch
            // 
            this.listBoxDeviceSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDeviceSearch.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxDeviceSearch.FormattingEnabled = true;
            this.listBoxDeviceSearch.ItemHeight = 28;
            this.listBoxDeviceSearch.Location = new System.Drawing.Point(3, 17);
            this.listBoxDeviceSearch.Name = "listBoxDeviceSearch";
            this.listBoxDeviceSearch.Size = new System.Drawing.Size(340, 270);
            this.listBoxDeviceSearch.TabIndex = 0;
            this.listBoxDeviceSearch.SelectedIndexChanged += new System.EventHandler(this.listBoxDeviceSearch_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 424);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.buttonEnum);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(256, 424);
            this.splitContainer3.SplitterDistance = 89;
            this.splitContainer3.TabIndex = 0;
            // 
            // buttonEnum
            // 
            this.buttonEnum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEnum.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEnum.Location = new System.Drawing.Point(0, 0);
            this.buttonEnum.Name = "buttonEnum";
            this.buttonEnum.Size = new System.Drawing.Size(256, 89);
            this.buttonEnum.TabIndex = 0;
            this.buttonEnum.Text = "查找";
            this.buttonEnum.UseVisualStyleBackColor = true;
            this.buttonEnum.Click += new System.EventHandler(this.buttonEnum_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.buttonConnect);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(256, 331);
            this.splitContainer4.SplitterDistance = 89;
            this.splitContainer4.TabIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConnect.Location = new System.Drawing.Point(0, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(256, 89);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.buttonDisconnect);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.buttonUserStart);
            this.splitContainer5.Size = new System.Drawing.Size(256, 238);
            this.splitContainer5.SplitterDistance = 89;
            this.splitContainer5.TabIndex = 0;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDisconnect.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDisconnect.Location = new System.Drawing.Point(0, 0);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(256, 89);
            this.buttonDisconnect.TabIndex = 0;
            this.buttonDisconnect.Text = "断开";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonUserStart
            // 
            this.buttonUserStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUserStart.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUserStart.Location = new System.Drawing.Point(0, 0);
            this.buttonUserStart.Name = "buttonUserStart";
            this.buttonUserStart.Size = new System.Drawing.Size(256, 145);
            this.buttonUserStart.TabIndex = 1;
            this.buttonUserStart.Text = "开始";
            this.buttonUserStart.UseVisualStyleBackColor = true;
            this.buttonUserStart.Click += new System.EventHandler(this.buttonUserStart_Click);
            // 
            // CsrBleUserMultiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "CsrBleUserMultiControl";
            this.Size = new System.Drawing.Size(609, 424);
            this.Load += new System.EventHandler(this.CsrBleUserMultiControl_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxDeviceConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxDeviceSearch;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        public System.Windows.Forms.Button buttonEnum;
        public System.Windows.Forms.Button buttonConnect;
        public System.Windows.Forms.Button buttonDisconnect;
        public System.Windows.Forms.Button buttonUserStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRssiDbm;
    }
}
