namespace Ika_SPT_Launcher_ConfigEditor
{
    partial class ConfigForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            form_save_btn = new Button();
            form_savelaunch_btn = new Button();
            se_wait_lbl = new Label();
            se_wait_txt = new TextBox();
            statusStrip1 = new StatusStrip();
            status_lbl = new ToolStripStatusLabel();
            revision_title_lbl = new ToolStripStatusLabel();
            revision_data_lbl = new ToolStripStatusLabel();
            se_wait_info_lbl = new Label();
            deb_pause_info_lbl = new Label();
            deb_pause_chk = new CheckBox();
            se_islocal_chk = new CheckBox();
            se_local_info_lbl = new Label();
            getFile_dia = new OpenFileDialog();
            label9 = new Label();
            le_path_info_lbl = new Label();
            le_path_txt = new TextBox();
            le_browse_btn = new Button();
            groupBox1 = new GroupBox();
            se_erase_lbl = new Label();
            se_waitsec_lbl = new Label();
            se_path_txt = new TextBox();
            se_browse_btn = new Button();
            se_path_info_lbl = new Label();
            se_path_lbl = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            le_erase_lbl = new Label();
            form_quit_btn = new Button();
            groupBox4 = new GroupBox();
            ea_apps_lv = new ListView();
            ea_application_col = new ColumnHeader();
            ea_minimized_col = new ColumnHeader();
            ea_id_col = new ColumnHeader();
            imgList = new ImageList(components);
            ea_selectall_btn = new Button();
            ea_unselectall_btn = new Button();
            ea_remove_btn = new Button();
            ea_add_btn = new Button();
            ea_section_info_lbl = new Label();
            form_reset_btn = new Button();
            se_erase_lbl_tt = new ToolTip(components);
            le_erase_lbl_tt = new ToolTip(components);
            le_path_info_lbl_tt = new ToolTip(components);
            se_local_info_lbl_tt = new ToolTip(components);
            se_path_info_lbl_tt = new ToolTip(components);
            se_wait_info_lbl_tt = new ToolTip(components);
            deb_pause_info_lbl_tt = new ToolTip(components);
            ea_section_info_lbl_tt = new ToolTip(components);
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // form_save_btn
            // 
            form_save_btn.Location = new Point(482, 680);
            form_save_btn.Name = "form_save_btn";
            form_save_btn.Size = new Size(108, 23);
            form_save_btn.TabIndex = 0;
            form_save_btn.Text = "Save";
            form_save_btn.UseVisualStyleBackColor = true;
            form_save_btn.Click += Form_save_btn_Click;
            // 
            // form_savelaunch_btn
            // 
            form_savelaunch_btn.Location = new Point(596, 680);
            form_savelaunch_btn.Name = "form_savelaunch_btn";
            form_savelaunch_btn.Size = new Size(106, 23);
            form_savelaunch_btn.TabIndex = 1;
            form_savelaunch_btn.Text = "Save and Launch";
            form_savelaunch_btn.UseVisualStyleBackColor = true;
            form_savelaunch_btn.Click += Form_savelaunch_btn_Click;
            // 
            // se_wait_lbl
            // 
            se_wait_lbl.Location = new Point(20, 87);
            se_wait_lbl.Name = "se_wait_lbl";
            se_wait_lbl.Size = new Size(125, 15);
            se_wait_lbl.TabIndex = 4;
            se_wait_lbl.Text = "Time To Wait (in sec.)";
            // 
            // se_wait_txt
            // 
            se_wait_txt.Location = new Point(160, 82);
            se_wait_txt.Name = "se_wait_txt";
            se_wait_txt.Size = new Size(65, 23);
            se_wait_txt.TabIndex = 5;
            se_wait_txt.TextChanged += Se_wait_txt_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { status_lbl, revision_title_lbl, revision_data_lbl });
            statusStrip1.Location = new Point(0, 707);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // status_lbl
            // 
            status_lbl.Margin = new Padding(5, 3, 0, 2);
            status_lbl.Name = "status_lbl";
            status_lbl.Size = new Size(0, 17);
            // 
            // revision_title_lbl
            // 
            revision_title_lbl.ImageScaling = ToolStripItemImageScaling.None;
            revision_title_lbl.Name = "revision_title_lbl";
            revision_title_lbl.Size = new Size(780, 17);
            revision_title_lbl.Spring = true;
            revision_title_lbl.Text = "Configuration Revision: ";
            revision_title_lbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // revision_data_lbl
            // 
            revision_data_lbl.Name = "revision_data_lbl";
            revision_data_lbl.Size = new Size(0, 17);
            // 
            // se_wait_info_lbl
            // 
            se_wait_info_lbl.Cursor = Cursors.Help;
            se_wait_info_lbl.Image = (Image)resources.GetObject("se_wait_info_lbl.Image");
            se_wait_info_lbl.Location = new Point(741, 86);
            se_wait_info_lbl.Name = "se_wait_info_lbl";
            se_wait_info_lbl.Size = new Size(16, 16);
            se_wait_info_lbl.TabIndex = 8;
            se_wait_info_lbl.Text = " ";
            se_wait_info_lbl_tt.SetToolTip(se_wait_info_lbl, "Time to wait for the server to be initialized in seconds before launching the rest of the applications.\nThis will help for the SPT launcher to not open without a connection error.\n\nDEFAULT: 10");
            // 
            // deb_pause_info_lbl
            // 
            deb_pause_info_lbl.Cursor = Cursors.Help;
            deb_pause_info_lbl.Image = (Image)resources.GetObject("deb_pause_info_lbl.Image");
            deb_pause_info_lbl.Location = new Point(741, 39);
            deb_pause_info_lbl.Name = "deb_pause_info_lbl";
            deb_pause_info_lbl.Size = new Size(16, 16);
            deb_pause_info_lbl.TabIndex = 10;
            deb_pause_info_lbl.Text = " ";
            deb_pause_info_lbl_tt.SetToolTip(deb_pause_info_lbl, resources.GetString("deb_pause_info_lbl.ToolTip"));
            // 
            // deb_pause_chk
            // 
            deb_pause_chk.AutoSize = true;
            deb_pause_chk.Location = new Point(24, 40);
            deb_pause_chk.Name = "deb_pause_chk";
            deb_pause_chk.Size = new Size(308, 19);
            deb_pause_chk.TabIndex = 11;
            deb_pause_chk.Text = "Pause Launcher whenever applications are not found.";
            deb_pause_chk.UseVisualStyleBackColor = true;
            deb_pause_chk.CheckedChanged += Deb_pause_chk_CheckedChanged;
            // 
            // se_islocal_chk
            // 
            se_islocal_chk.AutoSize = true;
            se_islocal_chk.Location = new Point(160, 27);
            se_islocal_chk.Name = "se_islocal_chk";
            se_islocal_chk.Size = new Size(211, 19);
            se_islocal_chk.TabIndex = 14;
            se_islocal_chk.Text = "Server is located on local computer";
            se_islocal_chk.UseVisualStyleBackColor = true;
            se_islocal_chk.CheckedChanged += Se_islocal_chk_CheckedChanged;
            // 
            // se_local_info_lbl
            // 
            se_local_info_lbl.Cursor = Cursors.Help;
            se_local_info_lbl.Image = (Image)resources.GetObject("se_local_info_lbl.Image");
            se_local_info_lbl.Location = new Point(741, 28);
            se_local_info_lbl.Name = "se_local_info_lbl";
            se_local_info_lbl.Size = new Size(16, 16);
            se_local_info_lbl.TabIndex = 13;
            se_local_info_lbl.Text = " ";
            se_local_info_lbl_tt.SetToolTip(se_local_info_lbl, "Check if the server required to play is on this computer.\nIf you are connecting to a remote server, leave it unchecked.\n\nDEFAULT: Yes/Checked");
            // 
            // label9
            // 
            label9.Location = new Point(20, 40);
            label9.Name = "label9";
            label9.Size = new Size(125, 15);
            label9.TabIndex = 22;
            label9.Text = "Launcher Path";
            // 
            // le_path_info_lbl
            // 
            le_path_info_lbl.Cursor = Cursors.Help;
            le_path_info_lbl.Image = (Image)resources.GetObject("le_path_info_lbl.Image");
            le_path_info_lbl.Location = new Point(741, 39);
            le_path_info_lbl.Name = "le_path_info_lbl";
            le_path_info_lbl.Size = new Size(16, 16);
            le_path_info_lbl.TabIndex = 21;
            le_path_info_lbl.Text = " ";
            le_path_info_lbl_tt.SetToolTip(le_path_info_lbl, "Path of the SPT Launcher.\nDefault is in the same directory as Ika SPT Launcher.\n\nDEFAULT: Aki.Launcher.exe");
            // 
            // le_path_txt
            // 
            le_path_txt.Location = new Point(160, 35);
            le_path_txt.Name = "le_path_txt";
            le_path_txt.ReadOnly = true;
            le_path_txt.Size = new Size(480, 23);
            le_path_txt.TabIndex = 20;
            // 
            // le_browse_btn
            // 
            le_browse_btn.Location = new Point(646, 35);
            le_browse_btn.Name = "le_browse_btn";
            le_browse_btn.Size = new Size(75, 23);
            le_browse_btn.TabIndex = 19;
            le_browse_btn.Text = "Browse";
            le_browse_btn.UseVisualStyleBackColor = true;
            le_browse_btn.Click += Le_browse_btn_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(se_erase_lbl);
            groupBox1.Controls.Add(se_waitsec_lbl);
            groupBox1.Controls.Add(se_path_txt);
            groupBox1.Controls.Add(se_browse_btn);
            groupBox1.Controls.Add(se_path_info_lbl);
            groupBox1.Controls.Add(se_path_lbl);
            groupBox1.Controls.Add(se_wait_lbl);
            groupBox1.Controls.Add(se_wait_txt);
            groupBox1.Controls.Add(se_wait_info_lbl);
            groupBox1.Controls.Add(se_local_info_lbl);
            groupBox1.Controls.Add(se_islocal_chk);
            groupBox1.Location = new Point(17, 105);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(766, 133);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server";
            // 
            // se_erase_lbl
            // 
            se_erase_lbl.Cursor = Cursors.Hand;
            se_erase_lbl.Image = (Image)resources.GetObject("se_erase_lbl.Image");
            se_erase_lbl.Location = new Point(141, 57);
            se_erase_lbl.Name = "se_erase_lbl";
            se_erase_lbl.Size = new Size(16, 16);
            se_erase_lbl.TabIndex = 24;
            se_erase_lbl_tt.SetToolTip(se_erase_lbl, "Erase Server Path");
            se_erase_lbl.Click += Se_erase_lbl_Click;
            // 
            // se_waitsec_lbl
            // 
            se_waitsec_lbl.AutoSize = true;
            se_waitsec_lbl.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            se_waitsec_lbl.ForeColor = SystemColors.ControlDarkDark;
            se_waitsec_lbl.Location = new Point(231, 87);
            se_waitsec_lbl.Name = "se_waitsec_lbl";
            se_waitsec_lbl.Size = new Size(50, 15);
            se_waitsec_lbl.TabIndex = 27;
            se_waitsec_lbl.Text = "(0 - 600)";
            // 
            // se_path_txt
            // 
            se_path_txt.Location = new Point(160, 53);
            se_path_txt.Name = "se_path_txt";
            se_path_txt.ReadOnly = true;
            se_path_txt.Size = new Size(480, 23);
            se_path_txt.TabIndex = 24;
            // 
            // se_browse_btn
            // 
            se_browse_btn.Location = new Point(646, 53);
            se_browse_btn.Name = "se_browse_btn";
            se_browse_btn.Size = new Size(75, 23);
            se_browse_btn.TabIndex = 23;
            se_browse_btn.Text = "Browse";
            se_browse_btn.UseVisualStyleBackColor = true;
            se_browse_btn.Click += Se_browse_btn_Click;
            // 
            // se_path_info_lbl
            // 
            se_path_info_lbl.Cursor = Cursors.Help;
            se_path_info_lbl.Image = (Image)resources.GetObject("se_path_info_lbl.Image");
            se_path_info_lbl.Location = new Point(741, 57);
            se_path_info_lbl.Name = "se_path_info_lbl";
            se_path_info_lbl.Size = new Size(16, 16);
            se_path_info_lbl.TabIndex = 25;
            se_path_info_lbl.Text = " ";
            se_path_info_lbl_tt.SetToolTip(se_path_info_lbl, "Path of the SPT Server.\nDefault is in the same directory as Ika SPT Launcher.\n\nDEFAULT: Aki.Server.exe");
            // 
            // se_path_lbl
            // 
            se_path_lbl.Location = new Point(20, 58);
            se_path_lbl.Name = "se_path_lbl";
            se_path_lbl.Size = new Size(125, 15);
            se_path_lbl.TabIndex = 26;
            se_path_lbl.Text = "Server Path";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(deb_pause_chk);
            groupBox2.Controls.Add(deb_pause_info_lbl);
            groupBox2.Location = new Point(17, 244);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(766, 87);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "Debug";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(le_erase_lbl);
            groupBox3.Controls.Add(le_path_txt);
            groupBox3.Controls.Add(le_browse_btn);
            groupBox3.Controls.Add(le_path_info_lbl);
            groupBox3.Controls.Add(label9);
            groupBox3.Location = new Point(17, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(766, 87);
            groupBox3.TabIndex = 25;
            groupBox3.TabStop = false;
            groupBox3.Text = "Launcher";
            // 
            // le_erase_lbl
            // 
            le_erase_lbl.Cursor = Cursors.Hand;
            le_erase_lbl.Image = (Image)resources.GetObject("le_erase_lbl.Image");
            le_erase_lbl.Location = new Point(141, 39);
            le_erase_lbl.Name = "le_erase_lbl";
            le_erase_lbl.Size = new Size(16, 16);
            le_erase_lbl.TabIndex = 23;
            le_erase_lbl_tt.SetToolTip(le_erase_lbl, "Erase Launcher Path");
            le_erase_lbl.Click += Le_erase_lbl_Click;
            // 
            // form_quit_btn
            // 
            form_quit_btn.Location = new Point(708, 680);
            form_quit_btn.Name = "form_quit_btn";
            form_quit_btn.Size = new Size(75, 23);
            form_quit_btn.TabIndex = 26;
            form_quit_btn.Text = "Quit";
            form_quit_btn.UseVisualStyleBackColor = true;
            form_quit_btn.Click += Form_quit_btn_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ea_apps_lv);
            groupBox4.Controls.Add(ea_selectall_btn);
            groupBox4.Controls.Add(ea_unselectall_btn);
            groupBox4.Controls.Add(ea_remove_btn);
            groupBox4.Controls.Add(ea_add_btn);
            groupBox4.Controls.Add(ea_section_info_lbl);
            groupBox4.Location = new Point(17, 337);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(766, 321);
            groupBox4.TabIndex = 27;
            groupBox4.TabStop = false;
            groupBox4.Text = "External Applications";
            // 
            // ea_apps_lv
            // 
            ea_apps_lv.CheckBoxes = true;
            ea_apps_lv.Columns.AddRange(new ColumnHeader[] { ea_application_col, ea_minimized_col, ea_id_col });
            ea_apps_lv.FullRowSelect = true;
            ea_apps_lv.GridLines = true;
            ea_apps_lv.HideSelection = true;
            ea_apps_lv.LabelWrap = false;
            ea_apps_lv.Location = new Point(20, 32);
            ea_apps_lv.MultiSelect = false;
            ea_apps_lv.Name = "ea_apps_lv";
            ea_apps_lv.Scrollable = false;
            ea_apps_lv.Size = new Size(536, 278);
            ea_apps_lv.SmallImageList = imgList;
            ea_apps_lv.TabIndex = 30;
            ea_apps_lv.UseCompatibleStateImageBehavior = false;
            ea_apps_lv.View = View.Details;
            // 
            // ea_application_col
            // 
            ea_application_col.Text = "Application";
            ea_application_col.Width = 462;
            // 
            // ea_minimized_col
            // 
            ea_minimized_col.Text = "Minimized";
            ea_minimized_col.Width = 70;
            // 
            // ea_id_col
            // 
            ea_id_col.Text = "ID";
            // 
            // imgList
            // 
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            imgList.ImageSize = new Size(1, 24);
            imgList.TransparentColor = Color.Transparent;
            // 
            // ea_selectall_btn
            // 
            ea_selectall_btn.Location = new Point(562, 145);
            ea_selectall_btn.Name = "ea_selectall_btn";
            ea_selectall_btn.Size = new Size(171, 23);
            ea_selectall_btn.TabIndex = 29;
            ea_selectall_btn.Text = "Select All";
            ea_selectall_btn.UseVisualStyleBackColor = true;
            ea_selectall_btn.Click += Ea_selectall_btn_Click;
            // 
            // ea_unselectall_btn
            // 
            ea_unselectall_btn.Location = new Point(562, 171);
            ea_unselectall_btn.Name = "ea_unselectall_btn";
            ea_unselectall_btn.Size = new Size(171, 23);
            ea_unselectall_btn.TabIndex = 28;
            ea_unselectall_btn.Text = "Unselect All";
            ea_unselectall_btn.UseVisualStyleBackColor = true;
            ea_unselectall_btn.Click += Ea_unselectall_btn_Click;
            // 
            // ea_remove_btn
            // 
            ea_remove_btn.Location = new Point(562, 287);
            ea_remove_btn.Name = "ea_remove_btn";
            ea_remove_btn.Size = new Size(171, 23);
            ea_remove_btn.TabIndex = 25;
            ea_remove_btn.Text = "Remove Selected";
            ea_remove_btn.UseVisualStyleBackColor = true;
            ea_remove_btn.Click += Ea_remove_btn_Click;
            // 
            // ea_add_btn
            // 
            ea_add_btn.Location = new Point(562, 32);
            ea_add_btn.Name = "ea_add_btn";
            ea_add_btn.Size = new Size(171, 23);
            ea_add_btn.TabIndex = 24;
            ea_add_btn.Text = "Add Application";
            ea_add_btn.UseVisualStyleBackColor = true;
            ea_add_btn.Click += Ea_add_btn_Click;
            // 
            // ea_section_info_lbl
            // 
            ea_section_info_lbl.Cursor = Cursors.Help;
            ea_section_info_lbl.Image = (Image)resources.GetObject("ea_section_info_lbl.Image");
            ea_section_info_lbl.Location = new Point(741, 17);
            ea_section_info_lbl.Name = "ea_section_info_lbl";
            ea_section_info_lbl.Size = new Size(16, 16);
            ea_section_info_lbl.TabIndex = 22;
            ea_section_info_lbl.Text = " ";
            ea_section_info_lbl_tt.SetToolTip(ea_section_info_lbl, resources.GetString("ea_section_info_lbl.ToolTip"));
            // 
            // form_reset_btn
            // 
            form_reset_btn.Location = new Point(17, 680);
            form_reset_btn.Name = "form_reset_btn";
            form_reset_btn.Size = new Size(108, 23);
            form_reset_btn.TabIndex = 28;
            form_reset_btn.Text = "Reset Default";
            form_reset_btn.UseVisualStyleBackColor = true;
            form_reset_btn.Click += Form_reset_btn_Click;
            // 
            // se_erase_lbl_tt
            // 
            se_erase_lbl_tt.AutoPopDelay = 5000;
            se_erase_lbl_tt.InitialDelay = 100;
            se_erase_lbl_tt.ReshowDelay = 100;
            // 
            // le_erase_lbl_tt
            // 
            le_erase_lbl_tt.AutoPopDelay = 5000;
            le_erase_lbl_tt.InitialDelay = 100;
            le_erase_lbl_tt.ReshowDelay = 100;
            // 
            // le_path_info_lbl_tt
            // 
            le_path_info_lbl_tt.AutoPopDelay = 5000;
            le_path_info_lbl_tt.InitialDelay = 100;
            le_path_info_lbl_tt.ReshowDelay = 100;
            // 
            // se_local_info_lbl_tt
            // 
            se_local_info_lbl_tt.AutoPopDelay = 5000;
            se_local_info_lbl_tt.InitialDelay = 100;
            se_local_info_lbl_tt.ReshowDelay = 100;
            // 
            // se_path_info_lbl_tt
            // 
            se_path_info_lbl_tt.AutoPopDelay = 5000;
            se_path_info_lbl_tt.InitialDelay = 100;
            se_path_info_lbl_tt.ReshowDelay = 100;
            // 
            // se_wait_info_lbl_tt
            // 
            se_wait_info_lbl_tt.AutoPopDelay = 5000;
            se_wait_info_lbl_tt.InitialDelay = 100;
            se_wait_info_lbl_tt.ReshowDelay = 100;
            // 
            // deb_pause_info_lbl_tt
            // 
            deb_pause_info_lbl_tt.AutoPopDelay = 5000;
            deb_pause_info_lbl_tt.InitialDelay = 100;
            deb_pause_info_lbl_tt.ReshowDelay = 100;
            // 
            // ea_section_info_lbl_tt
            // 
            ea_section_info_lbl_tt.AutoPopDelay = 5000;
            ea_section_info_lbl_tt.InitialDelay = 100;
            ea_section_info_lbl_tt.ReshowDelay = 100;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 729);
            Controls.Add(form_reset_btn);
            Controls.Add(groupBox4);
            Controls.Add(form_quit_btn);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Controls.Add(form_savelaunch_btn);
            Controls.Add(form_save_btn);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ConfigForm";
            Text = "Ika SPT Launcher - Configuration Editor 1.1.0";
            FormClosing += ConfigForm_FormClosing;
            Load += ConfigForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button form_save_btn;
        private Button form_savelaunch_btn;
        private Label se_wait_lbl;
        private TextBox se_wait_txt;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel status_lbl;
        private ToolStripStatusLabel revision_title_lbl;
        private ToolStripStatusLabel revision_data_lbl;
        private Label se_wait_info_lbl;
        private Label deb_pause_info_lbl;
        private CheckBox deb_pause_chk;
        private CheckBox se_islocal_chk;
        private Label se_local_info_lbl;
        private OpenFileDialog getFile_dia;
        private Label label9;
        private Label le_path_info_lbl;
        private TextBox le_path_txt;
        private Button le_browse_btn;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button form_quit_btn;
        private GroupBox groupBox4;
        private Button form_reset_btn;
        private TextBox se_path_txt;
        private Button se_browse_btn;
        private Label se_path_info_lbl;
        private Label se_path_lbl;
        private Label ea_section_info_lbl;
        private Button ea_add_btn;
        private Button ea_remove_btn;
        private Button ea_selectall_btn;
        private Button ea_unselectall_btn;
        private ListView ea_apps_lv;
        private ColumnHeader ea_application_col;
        private ColumnHeader ea_minimized_col;
        private ImageList imgList;
        private ColumnHeader ea_id_col;
        private Label se_waitsec_lbl;
        private Label le_erase_lbl;
        private Label se_erase_lbl;
        private ToolTip se_erase_lbl_tt;
        private ToolTip le_erase_lbl_tt;
        private ToolTip le_path_info_lbl_tt;
        private ToolTip se_local_info_lbl_tt;
        private ToolTip se_path_info_lbl_tt;
        private ToolTip se_wait_info_lbl_tt;
        private ToolTip deb_pause_info_lbl_tt;
        private ToolTip ea_section_info_lbl_tt;
    }
}
