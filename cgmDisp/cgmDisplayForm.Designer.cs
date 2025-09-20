namespace cgmDisp
{
    partial class cgmDisplayForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cgmDisplayForm));
            this.panelBackGround = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDelta = new System.Windows.Forms.Label();
            this.labelGlucose = new System.Windows.Forms.Label();
            this.bgGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bgGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBackGround
            // 
            this.panelBackGround.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBackGround.Controls.Add(this.bgGraph);
            this.panelBackGround.Controls.Add(this.labelTime);
            this.panelBackGround.Controls.Add(this.labelDelta);
            this.panelBackGround.Controls.Add(this.labelGlucose);
            this.panelBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackGround.ForeColor = System.Drawing.Color.Black;
            this.panelBackGround.Location = new System.Drawing.Point(0, 0);
            this.panelBackGround.Name = "panelBackGround";
            this.panelBackGround.Size = new System.Drawing.Size(317, 182);
            this.panelBackGround.TabIndex = 0;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.labelTime.Location = new System.Drawing.Point(11, 23);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(72, 19);
            this.labelTime.TabIndex = 6;
            this.labelTime.Text = "12:00AM";
            this.labelTime.Click += new System.EventHandler(this.labelTime_Click);
            // 
            // labelDelta
            // 
            this.labelDelta.AutoSize = true;
            this.labelDelta.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.labelDelta.Location = new System.Drawing.Point(227, 15);
            this.labelDelta.Name = "labelDelta";
            this.labelDelta.Size = new System.Drawing.Size(44, 32);
            this.labelDelta.TabIndex = 5;
            this.labelDelta.Text = "+0";
            // 
            // labelGlucose
            // 
            this.labelGlucose.AutoSize = true;
            this.labelGlucose.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlucose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.labelGlucose.Location = new System.Drawing.Point(110, 11);
            this.labelGlucose.Name = "labelGlucose";
            this.labelGlucose.Size = new System.Drawing.Size(113, 41);
            this.labelGlucose.TabIndex = 4;
            this.labelGlucose.Text = "--- →";
            // 
            // bgGraph
            // 
            this.bgGraph.BackColor = System.Drawing.Color.Transparent;
            this.bgGraph.BorderlineColor = System.Drawing.Color.Transparent;
            this.bgGraph.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LabelStyle.TruncatedLabels = true;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Gray;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.bgGraph.ChartAreas.Add(chartArea1);
            this.bgGraph.Location = new System.Drawing.Point(-7, 53);
            this.bgGraph.Name = "bgGraph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            series1.Name = "bgPoints";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.bgGraph.Series.Add(series1);
            this.bgGraph.Size = new System.Drawing.Size(323, 128);
            this.bgGraph.TabIndex = 7;
            this.bgGraph.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(317, 182);
            this.Controls.Add(this.panelBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CGM Display";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelBackGround.ResumeLayout(false);
            this.panelBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bgGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBackGround;
        private System.Windows.Forms.Label labelGlucose;
        private System.Windows.Forms.Label labelDelta;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart bgGraph;
    }
}

