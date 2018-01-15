namespace AshesLauncher {
    partial class Benchmark {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label18 = new System.Windows.Forms.Label();
            this.tbBenchmarkSummary = new System.Windows.Forms.TextBox();
            this.chbOpenBenchmarkResults = new System.Windows.Forms.CheckBox();
            this.btnOpenBenchmarkFolder = new System.Windows.Forms.Button();
            this.btnRunCpuFocussed = new System.Windows.Forms.Button();
            this.btnRunGpuFocussed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(187, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "Results summary";
            // 
            // tbBenchmarkSummary
            // 
            this.tbBenchmarkSummary.Location = new System.Drawing.Point(190, 29);
            this.tbBenchmarkSummary.Multiline = true;
            this.tbBenchmarkSummary.Name = "tbBenchmarkSummary";
            this.tbBenchmarkSummary.ReadOnly = true;
            this.tbBenchmarkSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbBenchmarkSummary.Size = new System.Drawing.Size(320, 349);
            this.tbBenchmarkSummary.TabIndex = 13;
            // 
            // chbOpenBenchmarkResults
            // 
            this.chbOpenBenchmarkResults.AutoSize = true;
            this.chbOpenBenchmarkResults.Location = new System.Drawing.Point(9, 78);
            this.chbOpenBenchmarkResults.Name = "chbOpenBenchmarkResults";
            this.chbOpenBenchmarkResults.Size = new System.Drawing.Size(169, 17);
            this.chbOpenBenchmarkResults.TabIndex = 12;
            this.chbOpenBenchmarkResults.Text = "Open results file when finished";
            this.chbOpenBenchmarkResults.UseVisualStyleBackColor = true;
            // 
            // btnOpenBenchmarkFolder
            // 
            this.btnOpenBenchmarkFolder.Location = new System.Drawing.Point(9, 348);
            this.btnOpenBenchmarkFolder.Name = "btnOpenBenchmarkFolder";
            this.btnOpenBenchmarkFolder.Size = new System.Drawing.Size(169, 30);
            this.btnOpenBenchmarkFolder.TabIndex = 10;
            this.btnOpenBenchmarkFolder.Text = "Open Benchmark Folder";
            this.btnOpenBenchmarkFolder.UseVisualStyleBackColor = true;
            this.btnOpenBenchmarkFolder.Click += new System.EventHandler(this.btnOpenBenchmarkFolder_Click);
            // 
            // btnRunCpuFocussed
            // 
            this.btnRunCpuFocussed.Location = new System.Drawing.Point(9, 42);
            this.btnRunCpuFocussed.Name = "btnRunCpuFocussed";
            this.btnRunCpuFocussed.Size = new System.Drawing.Size(169, 30);
            this.btnRunCpuFocussed.TabIndex = 4;
            this.btnRunCpuFocussed.Text = "Run CPU Focussed Benchmark";
            this.btnRunCpuFocussed.UseVisualStyleBackColor = true;
            this.btnRunCpuFocussed.Click += new System.EventHandler(this.btnRunCpuFocussed_Click);
            // 
            // btnRunGpuFocussed
            // 
            this.btnRunGpuFocussed.Location = new System.Drawing.Point(9, 9);
            this.btnRunGpuFocussed.Margin = new System.Windows.Forms.Padding(0);
            this.btnRunGpuFocussed.Name = "btnRunGpuFocussed";
            this.btnRunGpuFocussed.Size = new System.Drawing.Size(169, 30);
            this.btnRunGpuFocussed.TabIndex = 3;
            this.btnRunGpuFocussed.Text = "Run GPU Focussed Benchmark";
            this.btnRunGpuFocussed.UseVisualStyleBackColor = true;
            this.btnRunGpuFocussed.Click += new System.EventHandler(this.btnRunGpuFocussed_Click);
            // 
            // Benchmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(520, 411);
            this.Controls.Add(this.btnRunCpuFocussed);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnRunGpuFocussed);
            this.Controls.Add(this.tbBenchmarkSummary);
            this.Controls.Add(this.chbOpenBenchmarkResults);
            this.Controls.Add(this.btnOpenBenchmarkFolder);
            this.Name = "Benchmark";
            this.Text = "Benchmark";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbBenchmarkSummary;
        private System.Windows.Forms.CheckBox chbOpenBenchmarkResults;
        private System.Windows.Forms.Button btnOpenBenchmarkFolder;
        private System.Windows.Forms.Button btnRunCpuFocussed;
        private System.Windows.Forms.Button btnRunGpuFocussed;
    }
}