namespace AffinitySherpa
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            splitContainer1 = new SplitContainer();
            exeList = new ListView();
            panel1 = new Panel();
            addExeBtn = new Button();
            groupBox1 = new GroupBox();
            processList = new ListView();
            PID = new ColumnHeader();
            Affinity = new ColumnHeader();
            panel3 = new Panel();
            createLauncherBtn = new Button();
            applyAffinityBtn = new Button();
            label1 = new Label();
            updateMaskBtn = new Button();
            selectedMask = new Label();
            selectedName = new Label();
            selectedFullPath = new Label();
            panel2 = new Panel();
            autoApply = new CheckBox();
            corePanel = new Panel();
            label2 = new Label();
            openFileDialog1 = new OpenFileDialog();
            notifyIcon1 = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            corePanel.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(exeList);
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Panel2.Controls.Add(corePanel);
            splitContainer1.Size = new Size(713, 393);
            splitContainer1.SplitterDistance = 213;
            splitContainer1.TabIndex = 0;
            // 
            // exeList
            // 
            exeList.Dock = DockStyle.Fill;
            exeList.FullRowSelect = true;
            exeList.Location = new Point(0, 42);
            exeList.MultiSelect = false;
            exeList.Name = "exeList";
            exeList.Size = new Size(213, 351);
            exeList.TabIndex = 2;
            exeList.UseCompatibleStateImageBehavior = false;
            exeList.View = View.List;
            exeList.ItemSelectionChanged += exeList_ItemSelectionChanged;
            exeList.PreviewKeyDown += exeList_PreviewKeyDown;
            // 
            // panel1
            // 
            panel1.Controls.Add(addExeBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 42);
            panel1.TabIndex = 1;
            // 
            // addExeBtn
            // 
            addExeBtn.Location = new Point(12, 8);
            addExeBtn.Name = "addExeBtn";
            addExeBtn.Size = new Size(75, 23);
            addExeBtn.TabIndex = 0;
            addExeBtn.Text = "Add EXE";
            addExeBtn.UseVisualStyleBackColor = true;
            addExeBtn.Click += addExeBtn_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(processList);
            groupBox1.Controls.Add(panel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(118, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(378, 351);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = " Selected ";
            // 
            // processList
            // 
            processList.Columns.AddRange(new ColumnHeader[] { PID, Affinity });
            processList.Dock = DockStyle.Fill;
            processList.FullRowSelect = true;
            processList.Location = new Point(3, 116);
            processList.MultiSelect = false;
            processList.Name = "processList";
            processList.Size = new Size(372, 232);
            processList.TabIndex = 1;
            processList.UseCompatibleStateImageBehavior = false;
            processList.View = View.Details;
            // 
            // PID
            // 
            PID.Text = "PID";
            // 
            // Affinity
            // 
            Affinity.Text = "Affinity";
            Affinity.Width = 80;
            // 
            // panel3
            // 
            panel3.Controls.Add(createLauncherBtn);
            panel3.Controls.Add(applyAffinityBtn);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(updateMaskBtn);
            panel3.Controls.Add(selectedMask);
            panel3.Controls.Add(selectedName);
            panel3.Controls.Add(selectedFullPath);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 19);
            panel3.Name = "panel3";
            panel3.Size = new Size(372, 97);
            panel3.TabIndex = 0;
            // 
            // createLauncherBtn
            // 
            createLauncherBtn.Enabled = false;
            createLauncherBtn.Location = new Point(217, 48);
            createLauncherBtn.Name = "createLauncherBtn";
            createLauncherBtn.Size = new Size(111, 23);
            createLauncherBtn.TabIndex = 11;
            createLauncherBtn.Text = "Create Launcher";
            createLauncherBtn.UseVisualStyleBackColor = true;
            createLauncherBtn.Click += createLauncherBtn_Click;
            // 
            // applyAffinityBtn
            // 
            applyAffinityBtn.Location = new Point(108, 48);
            applyAffinityBtn.Name = "applyAffinityBtn";
            applyAffinityBtn.Size = new Size(103, 23);
            applyAffinityBtn.TabIndex = 10;
            applyAffinityBtn.Text = "Apply Affinity";
            applyAffinityBtn.UseVisualStyleBackColor = true;
            applyAffinityBtn.Click += applyAffinityBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 74);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 9;
            label1.Text = "Running:";
            // 
            // updateMaskBtn
            // 
            updateMaskBtn.Location = new Point(3, 48);
            updateMaskBtn.Name = "updateMaskBtn";
            updateMaskBtn.Size = new Size(99, 23);
            updateMaskBtn.TabIndex = 8;
            updateMaskBtn.Text = "Update Mask";
            updateMaskBtn.UseVisualStyleBackColor = true;
            updateMaskBtn.Click += updateMaskBtn_Click;
            // 
            // selectedMask
            // 
            selectedMask.AutoSize = true;
            selectedMask.Location = new Point(3, 30);
            selectedMask.Name = "selectedMask";
            selectedMask.Size = new Size(47, 15);
            selectedMask.TabIndex = 7;
            selectedMask.Text = "Mask: 0";
            // 
            // selectedName
            // 
            selectedName.AutoSize = true;
            selectedName.Location = new Point(3, 0);
            selectedName.Name = "selectedName";
            selectedName.Size = new Size(85, 15);
            selectedName.TabIndex = 6;
            selectedName.Text = "Name: Process";
            // 
            // selectedFullPath
            // 
            selectedFullPath.AutoSize = true;
            selectedFullPath.Location = new Point(3, 15);
            selectedFullPath.Name = "selectedFullPath";
            selectedFullPath.Size = new Size(99, 15);
            selectedFullPath.TabIndex = 5;
            selectedFullPath.Text = "Full Path: C:\\Path";
            // 
            // panel2
            // 
            panel2.Controls.Add(autoApply);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(118, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(378, 42);
            panel2.TabIndex = 1;
            // 
            // autoApply
            // 
            autoApply.AutoSize = true;
            autoApply.Location = new Point(6, 8);
            autoApply.Name = "autoApply";
            autoApply.Size = new Size(181, 19);
            autoApply.TabIndex = 0;
            autoApply.Text = "Auto Apply Affinity (every 5s)";
            autoApply.UseVisualStyleBackColor = true;
            // 
            // corePanel
            // 
            corePanel.Controls.Add(label2);
            corePanel.Dock = DockStyle.Left;
            corePanel.Location = new Point(0, 0);
            corePanel.Name = "corePanel";
            corePanel.Size = new Size(118, 393);
            corePanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 8);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 0;
            label2.Text = "Cores:";
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipText = "Program is minimized. Click the tray icon to restore it.";
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Affinity Sherpa";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(713, 393);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "MainForm";
            Text = "Affinity Sherpa";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            corePanel.ResumeLayout(false);
            corePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private ListView exeList;
        private Panel panel1;
        private Button addExeBtn;
        private Panel corePanel;
        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox1;
        private Panel panel2;
        private CheckBox autoApply;
        private ListView processList;
        private Panel panel3;
        private Label label1;
        private Button updateMaskBtn;
        private Label selectedMask;
        private Label selectedName;
        private Label selectedFullPath;
        private Label label2;
        private Button applyAffinityBtn;
        private ColumnHeader PID;
        private ColumnHeader Affinity;
        private NotifyIcon notifyIcon1;
        private Button createLauncherBtn;
    }
}