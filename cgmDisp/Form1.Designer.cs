namespace cgmDisp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelBackGround = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDelta = new System.Windows.Forms.Label();
            this.labelGlucose = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBackGround
            // 
            this.panelBackGround.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBackGround.Controls.Add(this.labelTime);
            this.panelBackGround.Controls.Add(this.labelDelta);
            this.panelBackGround.Controls.Add(this.labelGlucose);
            this.panelBackGround.Controls.Add(this.buttonClose);
            this.panelBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackGround.ForeColor = System.Drawing.Color.Black;
            this.panelBackGround.Location = new System.Drawing.Point(0, 0);
            this.panelBackGround.Name = "panelBackGround";
            this.panelBackGround.Size = new System.Drawing.Size(211, 106);
            this.panelBackGround.TabIndex = 0;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.labelTime.Location = new System.Drawing.Point(24, 65);
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
            this.labelDelta.Location = new System.Drawing.Point(138, 14);
            this.labelDelta.Name = "labelDelta";
            this.labelDelta.Size = new System.Drawing.Size(45, 32);
            this.labelDelta.TabIndex = 5;
            this.labelDelta.Text = "+0";
            // 
            // labelGlucose
            // 
            this.labelGlucose.AutoSize = true;
            this.labelGlucose.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlucose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.labelGlucose.Location = new System.Drawing.Point(21, 9);
            this.labelGlucose.Name = "labelGlucose";
            this.labelGlucose.Size = new System.Drawing.Size(113, 41);
            this.labelGlucose.TabIndex = 4;
            this.labelGlucose.Text = "--- →";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(63)))), ((int)(((byte)(52)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(151)))), ((int)(((byte)(31)))));
            this.buttonClose.Location = new System.Drawing.Point(121, 56);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(77, 37);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(211, 106);
            this.Controls.Add(this.panelBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CGM Display";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelBackGround.ResumeLayout(false);
            this.panelBackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBackGround;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelGlucose;
        private System.Windows.Forms.Label labelDelta;
        private System.Windows.Forms.Label labelTime;
    }
}

