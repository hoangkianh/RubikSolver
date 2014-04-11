namespace RubikSolver
{
    partial class RubikForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.csgL12Control1 = new CSGL12.CSGL12Control();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblMoveCount = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lbxMoveList = new System.Windows.Forms.ListBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.trbSpeed = new System.Windows.Forms.TrackBar();
            this.chkAnimate = new System.Windows.Forms.CheckBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnOrange = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            this.lblYellow = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblOrange = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblWhite = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // csgL12Control1
            // 
            this.csgL12Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.csgL12Control1.Location = new System.Drawing.Point(0, 0);
            this.csgL12Control1.Name = "csgL12Control1";
            this.csgL12Control1.Size = new System.Drawing.Size(500, 522);
            this.csgL12Control1.TabIndex = 0;
            this.csgL12Control1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.csgL12Control1_MouseDown);
            this.csgL12Control1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.csgL12Control1_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.csgL12Control1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnReset);
            this.splitContainer1.Panel2.Controls.Add(this.lblMoveCount);
            this.splitContainer1.Panel2.Controls.Add(this.btnNext);
            this.splitContainer1.Panel2.Controls.Add(this.btnPrev);
            this.splitContainer1.Panel2.Controls.Add(this.lbxMoveList);
            this.splitContainer1.Panel2.Controls.Add(this.btnSolve);
            this.splitContainer1.Panel2.Controls.Add(this.btnShuffle);
            this.splitContainer1.Panel2.Controls.Add(this.trbSpeed);
            this.splitContainer1.Panel2.Controls.Add(this.chkAnimate);
            this.splitContainer1.Panel2.Controls.Add(this.lblSpeed);
            this.splitContainer1.Panel2.Controls.Add(this.trbZoom);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.lblZoom);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(905, 522);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(7, 359);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblMoveCount
            // 
            this.lblMoveCount.AutoSize = true;
            this.lblMoveCount.Location = new System.Drawing.Point(337, 37);
            this.lblMoveCount.Name = "lblMoveCount";
            this.lblMoveCount.Size = new System.Drawing.Size(39, 13);
            this.lblMoveCount.TabIndex = 15;
            this.lblMoveCount.Text = "Moves";
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(305, 466);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(83, 23);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Location = new System.Drawing.Point(196, 466);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(86, 23);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // lbxMoveList
            // 
            this.lbxMoveList.FormattingEnabled = true;
            this.lbxMoveList.Location = new System.Drawing.Point(196, 53);
            this.lbxMoveList.Name = "lbxMoveList";
            this.lbxMoveList.Size = new System.Drawing.Size(192, 407);
            this.lbxMoveList.TabIndex = 12;
            this.lbxMoveList.SelectedIndexChanged += new System.EventHandler(this.lbxMoveList_SelectedIndexChanged);
            // 
            // btnSolve
            // 
            this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolve.Location = new System.Drawing.Point(99, 321);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 10;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.Location = new System.Drawing.Point(7, 321);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(75, 23);
            this.btnShuffle.TabIndex = 0;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // trbSpeed
            // 
            this.trbSpeed.LargeChange = 10;
            this.trbSpeed.Location = new System.Drawing.Point(101, 42);
            this.trbSpeed.Maximum = 2000;
            this.trbSpeed.Minimum = 500;
            this.trbSpeed.Name = "trbSpeed";
            this.trbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbSpeed.Size = new System.Drawing.Size(45, 234);
            this.trbSpeed.SmallChange = 10;
            this.trbSpeed.TabIndex = 6;
            this.trbSpeed.TickFrequency = 100;
            this.trbSpeed.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbSpeed.Value = 1000;
            this.trbSpeed.Scroll += new System.EventHandler(this.trbSpeed_Scroll);
            // 
            // chkAnimate
            // 
            this.chkAnimate.AutoSize = true;
            this.chkAnimate.Checked = true;
            this.chkAnimate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnimate.Location = new System.Drawing.Point(12, 296);
            this.chkAnimate.Name = "chkAnimate";
            this.chkAnimate.Size = new System.Drawing.Size(64, 17);
            this.chkAnimate.TabIndex = 9;
            this.chkAnimate.Text = "Animate";
            this.chkAnimate.UseVisualStyleBackColor = true;
            this.chkAnimate.CheckedChanged += new System.EventHandler(this.chkAnimate_CheckedChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(96, 276);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "Speed:";
            // 
            // trbZoom
            // 
            this.trbZoom.LargeChange = 1;
            this.trbZoom.Location = new System.Drawing.Point(18, 42);
            this.trbZoom.Maximum = 14;
            this.trbZoom.Minimum = 6;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbZoom.Size = new System.Drawing.Size(45, 234);
            this.trbZoom.TabIndex = 1;
            this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbZoom.Value = 9;
            this.trbZoom.Scroll += new System.EventHandler(this.trbZoom_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "1";
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(21, 276);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(34, 13);
            this.lblZoom.TabIndex = 2;
            this.lblZoom.Text = "Zoom";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(10, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "+";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cubeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(905, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cubeToolStripMenuItem
            // 
            this.cubeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solverToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            this.cubeToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.cubeToolStripMenuItem.Text = "&Rubik";
            // 
            // solverToolStripMenuItem
            // 
            this.solverToolStripMenuItem.Name = "solverToolStripMenuItem";
            this.solverToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.solverToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.solverToolStripMenuItem.Text = "&Solve";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "&Quit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(905, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(500, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblWhite);
            this.groupBox1.Controls.Add(this.lblGreen);
            this.groupBox1.Controls.Add(this.lblOrange);
            this.groupBox1.Controls.Add(this.lblRed);
            this.groupBox1.Controls.Add(this.lblBlue);
            this.groupBox1.Controls.Add(this.lblYellow);
            this.groupBox1.Controls.Add(this.btnWhite);
            this.groupBox1.Controls.Add(this.btnGreen);
            this.groupBox1.Controls.Add(this.btnOrange);
            this.groupBox1.Controls.Add(this.btnRed);
            this.groupBox1.Controls.Add(this.btnBlue);
            this.groupBox1.Controls.Add(this.btnYellow);
            this.groupBox1.Location = new System.Drawing.Point(7, 388);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 109);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors for solution";
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnYellow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnYellow.Location = new System.Drawing.Point(16, 19);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(23, 23);
            this.btnYellow.TabIndex = 18;
            this.btnYellow.Text = "T";
            this.btnYellow.UseVisualStyleBackColor = false;
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnBlue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBlue.ForeColor = System.Drawing.Color.White;
            this.btnBlue.Location = new System.Drawing.Point(76, 19);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(23, 23);
            this.btnBlue.TabIndex = 19;
            this.btnBlue.Text = "F";
            this.btnBlue.UseVisualStyleBackColor = false;
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.Red;
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnRed.ForeColor = System.Drawing.Color.White;
            this.btnRed.Location = new System.Drawing.Point(135, 19);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(23, 23);
            this.btnRed.TabIndex = 20;
            this.btnRed.Text = "R";
            this.btnRed.UseVisualStyleBackColor = false;
            // 
            // btnOrange
            // 
            this.btnOrange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOrange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOrange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOrange.ForeColor = System.Drawing.Color.White;
            this.btnOrange.Location = new System.Drawing.Point(16, 63);
            this.btnOrange.Name = "btnOrange";
            this.btnOrange.Size = new System.Drawing.Size(23, 23);
            this.btnOrange.TabIndex = 21;
            this.btnOrange.Text = "L";
            this.btnOrange.UseVisualStyleBackColor = false;
            // 
            // btnGreen
            // 
            this.btnGreen.BackColor = System.Drawing.Color.Green;
            this.btnGreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnGreen.ForeColor = System.Drawing.Color.White;
            this.btnGreen.Location = new System.Drawing.Point(76, 63);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(23, 23);
            this.btnGreen.TabIndex = 22;
            this.btnGreen.Text = "B";
            this.btnGreen.UseVisualStyleBackColor = false;
            // 
            // btnWhite
            // 
            this.btnWhite.BackColor = System.Drawing.Color.White;
            this.btnWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnWhite.Location = new System.Drawing.Point(135, 63);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(23, 23);
            this.btnWhite.TabIndex = 23;
            this.btnWhite.Text = "D";
            this.btnWhite.UseVisualStyleBackColor = false;
            // 
            // lblYellow
            // 
            this.lblYellow.AutoSize = true;
            this.lblYellow.Location = new System.Drawing.Point(15, 45);
            this.lblYellow.Name = "lblYellow";
            this.lblYellow.Size = new System.Drawing.Size(26, 13);
            this.lblYellow.TabIndex = 24;
            this.lblYellow.Text = "Top";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(73, 45);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(31, 13);
            this.lblBlue.TabIndex = 25;
            this.lblBlue.Text = "Front";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(132, 45);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(32, 13);
            this.lblRed.TabIndex = 26;
            this.lblRed.Text = "Right";
            // 
            // lblOrange
            // 
            this.lblOrange.AutoSize = true;
            this.lblOrange.Location = new System.Drawing.Point(16, 92);
            this.lblOrange.Name = "lblOrange";
            this.lblOrange.Size = new System.Drawing.Size(25, 13);
            this.lblOrange.TabIndex = 27;
            this.lblOrange.Text = "Left";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(73, 92);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(32, 13);
            this.lblGreen.TabIndex = 28;
            this.lblGreen.Text = "Back";
            // 
            // lblWhite
            // 
            this.lblWhite.AutoSize = true;
            this.lblWhite.Location = new System.Drawing.Point(130, 90);
            this.lblWhite.Name = "lblWhite";
            this.lblWhite.Size = new System.Drawing.Size(35, 13);
            this.lblWhite.TabIndex = 29;
            this.lblWhite.Text = "Down";
            // 
            // RubikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 522);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RubikForm";
            this.Text = "SimpleApplication";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private CSGL12.CSGL12Control csgL12Control1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.TrackBar trbZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TrackBar trbSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkAnimate;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.ListBox lbxMoveList;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblMoveCount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWhite;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnOrange;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Label lblYellow;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblOrange;
        private System.Windows.Forms.Label lblWhite;
        private System.Windows.Forms.Label lblGreen;
    }
}
