namespace H3DAutomatorGUI
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_devices = new System.Windows.Forms.ListView();
            this.Serial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Product = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Device = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip_adb = new System.Windows.Forms.StatusStrip();
            this.status_lable_adb = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_install = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.label_python = new System.Windows.Forms.Label();
            this.textBox_python = new System.Windows.Forms.TextBox();
            this.button_python = new System.Windows.Forms.Button();
            this.label_wt = new System.Windows.Forms.Label();
            this.textBox_wt = new System.Windows.Forms.TextBox();
            this.button_wt = new System.Windows.Forms.Button();
            this.radioButton_install = new System.Windows.Forms.RadioButton();
            this.radioButton_onlytest = new System.Windows.Forms.RadioButton();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_apk = new System.Windows.Forms.TextBox();
            this.button_apk = new System.Windows.Forms.Button();
            this.statusStrip_adb.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_devices
            // 
            this.listView_devices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Serial,
            this.State,
            this.Model,
            this.Product,
            this.Device});
            this.listView_devices.FullRowSelect = true;
            this.listView_devices.Location = new System.Drawing.Point(12, 12);
            this.listView_devices.Name = "listView_devices";
            this.listView_devices.Size = new System.Drawing.Size(441, 364);
            this.listView_devices.TabIndex = 0;
            this.listView_devices.UseCompatibleStateImageBehavior = false;
            this.listView_devices.View = System.Windows.Forms.View.Details;
            // 
            // Serial
            // 
            this.Serial.Text = "Serial";
            this.Serial.Width = 124;
            // 
            // State
            // 
            this.State.Text = "State";
            this.State.Width = 92;
            // 
            // Model
            // 
            this.Model.Text = "Model";
            this.Model.Width = 106;
            // 
            // Product
            // 
            this.Product.Text = "Product";
            this.Product.Width = 97;
            // 
            // Device
            // 
            this.Device.Text = "Device";
            this.Device.Width = 94;
            // 
            // statusStrip_adb
            // 
            this.statusStrip_adb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_lable_adb});
            this.statusStrip_adb.Location = new System.Drawing.Point(0, 390);
            this.statusStrip_adb.Name = "statusStrip_adb";
            this.statusStrip_adb.Size = new System.Drawing.Size(821, 22);
            this.statusStrip_adb.TabIndex = 2;
            this.statusStrip_adb.Text = "statusStrip1";
            // 
            // status_lable_adb
            // 
            this.status_lable_adb.Name = "status_lable_adb";
            this.status_lable_adb.Size = new System.Drawing.Size(135, 17);
            this.status_lable_adb.Text = "ADBServerStatus:Stop";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(463, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 363);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label_title);
            this.tabPage1.Controls.Add(this.radioButton_onlytest);
            this.tabPage1.Controls.Add(this.radioButton_install);
            this.tabPage1.Controls.Add(this.button_apk);
            this.tabPage1.Controls.Add(this.button_wt);
            this.tabPage1.Controls.Add(this.textBox_apk);
            this.tabPage1.Controls.Add(this.textBox_wt);
            this.tabPage1.Controls.Add(this.button_python);
            this.tabPage1.Controls.Add(this.label_wt);
            this.tabPage1.Controls.Add(this.textBox_python);
            this.tabPage1.Controls.Add(this.label_python);
            this.tabPage1.Controls.Add(this.button_install);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(340, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_install
            // 
            this.button_install.Location = new System.Drawing.Point(224, 308);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(110, 23);
            this.button_install.TabIndex = 4;
            this.button_install.Text = "Run";
            this.button_install.UseVisualStyleBackColor = true;
            this.button_install.Click += new System.EventHandler(this.button_install_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_log);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(340, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_log
            // 
            this.textBox_log.BackColor = System.Drawing.Color.Black;
            this.textBox_log.ForeColor = System.Drawing.Color.White;
            this.textBox_log.Location = new System.Drawing.Point(0, 0);
            this.textBox_log.MaxLength = 32767000;
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_log.Size = new System.Drawing.Size(340, 337);
            this.textBox_log.TabIndex = 7;
            this.textBox_log.TextChanged += new System.EventHandler(this.textBox_log_TextChanged);
            // 
            // label_python
            // 
            this.label_python.AutoSize = true;
            this.label_python.Location = new System.Drawing.Point(7, 12);
            this.label_python.Name = "label_python";
            this.label_python.Size = new System.Drawing.Size(47, 12);
            this.label_python.TabIndex = 5;
            this.label_python.Text = "Python:";
            // 
            // textBox_python
            // 
            this.textBox_python.Enabled = false;
            this.textBox_python.Location = new System.Drawing.Point(61, 7);
            this.textBox_python.Name = "textBox_python";
            this.textBox_python.Size = new System.Drawing.Size(218, 21);
            this.textBox_python.TabIndex = 6;
            this.textBox_python.Text = "E:/Python27/python.exe";
            // 
            // button_python
            // 
            this.button_python.Location = new System.Drawing.Point(289, 7);
            this.button_python.Name = "button_python";
            this.button_python.Size = new System.Drawing.Size(45, 23);
            this.button_python.TabIndex = 7;
            this.button_python.Text = "...";
            this.button_python.UseVisualStyleBackColor = true;
            this.button_python.Click += new System.EventHandler(this.button_python_Click);
            // 
            // label_wt
            // 
            this.label_wt.AutoSize = true;
            this.label_wt.Location = new System.Drawing.Point(7, 41);
            this.label_wt.Name = "label_wt";
            this.label_wt.Size = new System.Drawing.Size(41, 12);
            this.label_wt.TabIndex = 5;
            this.label_wt.Text = "WeTest";
            // 
            // textBox_wt
            // 
            this.textBox_wt.Enabled = false;
            this.textBox_wt.Location = new System.Drawing.Point(61, 36);
            this.textBox_wt.Name = "textBox_wt";
            this.textBox_wt.Size = new System.Drawing.Size(218, 21);
            this.textBox_wt.TabIndex = 6;
            this.textBox_wt.Text = "E:/GitRepos/H3DAutomator/H3DAutomator/wetest";
            // 
            // button_wt
            // 
            this.button_wt.Location = new System.Drawing.Point(289, 36);
            this.button_wt.Name = "button_wt";
            this.button_wt.Size = new System.Drawing.Size(45, 23);
            this.button_wt.TabIndex = 7;
            this.button_wt.Text = "...";
            this.button_wt.UseVisualStyleBackColor = true;
            this.button_wt.Click += new System.EventHandler(this.button_wt_Click);
            // 
            // radioButton_install
            // 
            this.radioButton_install.AutoSize = true;
            this.radioButton_install.Checked = true;
            this.radioButton_install.Location = new System.Drawing.Point(20, 73);
            this.radioButton_install.Name = "radioButton_install";
            this.radioButton_install.Size = new System.Drawing.Size(77, 16);
            this.radioButton_install.TabIndex = 8;
            this.radioButton_install.TabStop = true;
            this.radioButton_install.Text = "安装+测试";
            this.radioButton_install.UseVisualStyleBackColor = true;
            this.radioButton_install.CheckedChanged += new System.EventHandler(this.radioButton_install_CheckedChanged);
            // 
            // radioButton_onlytest
            // 
            this.radioButton_onlytest.AutoSize = true;
            this.radioButton_onlytest.Location = new System.Drawing.Point(118, 73);
            this.radioButton_onlytest.Name = "radioButton_onlytest";
            this.radioButton_onlytest.Size = new System.Drawing.Size(59, 16);
            this.radioButton_onlytest.TabIndex = 9;
            this.radioButton_onlytest.Text = "仅测试";
            this.radioButton_onlytest.UseVisualStyleBackColor = true;
            this.radioButton_onlytest.CheckedChanged += new System.EventHandler(this.radioButton_onlytest_CheckedChanged);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Location = new System.Drawing.Point(4, 104);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(23, 12);
            this.label_title.TabIndex = 10;
            this.label_title.Text = "APK";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_apk
            // 
            this.textBox_apk.Enabled = false;
            this.textBox_apk.Location = new System.Drawing.Point(61, 99);
            this.textBox_apk.Name = "textBox_apk";
            this.textBox_apk.Size = new System.Drawing.Size(218, 21);
            this.textBox_apk.TabIndex = 6;
            this.textBox_apk.TextChanged += new System.EventHandler(this.textBox_apk_TextChanged);
            // 
            // button_apk
            // 
            this.button_apk.Location = new System.Drawing.Point(289, 97);
            this.button_apk.Name = "button_apk";
            this.button_apk.Size = new System.Drawing.Size(45, 23);
            this.button_apk.TabIndex = 7;
            this.button_apk.Text = "...";
            this.button_apk.UseVisualStyleBackColor = true;
            this.button_apk.Click += new System.EventHandler(this.button_apk_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 412);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip_adb);
            this.Controls.Add(this.listView_devices);
            this.Name = "MainForm";
            this.Text = "自动化测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip_adb.ResumeLayout(false);
            this.statusStrip_adb.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_devices;
        private System.Windows.Forms.ColumnHeader Serial;
        private System.Windows.Forms.ColumnHeader State;
        private System.Windows.Forms.ColumnHeader Model;
        private System.Windows.Forms.ColumnHeader Product;
        private System.Windows.Forms.ColumnHeader Device;
        private System.Windows.Forms.StatusStrip statusStrip_adb;
        private System.Windows.Forms.ToolStripStatusLabel status_lable_adb;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_install;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Button button_python;
        private System.Windows.Forms.TextBox textBox_python;
        private System.Windows.Forms.Label label_python;
        private System.Windows.Forms.Button button_wt;
        private System.Windows.Forms.TextBox textBox_wt;
        private System.Windows.Forms.Label label_wt;
        private System.Windows.Forms.RadioButton radioButton_onlytest;
        private System.Windows.Forms.RadioButton radioButton_install;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Button button_apk;
        private System.Windows.Forms.TextBox textBox_apk;
    }
}

