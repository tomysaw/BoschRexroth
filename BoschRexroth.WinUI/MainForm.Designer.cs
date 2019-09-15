namespace BoschRexroth.WinUI
{
    partial class MainForm
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
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.clbOperations = new System.Windows.Forms.CheckedListBox();
            this.btnBase = new System.Windows.Forms.Button();
            this.btnTurn = new System.Windows.Forms.Button();
            this.btnSpeed = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.tbTurn = new System.Windows.Forms.TextBox();
            this.tbPause = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(394, 12);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(394, 36);
            this.btnCalibrate.TabIndex = 0;
            this.btnCalibrate.Text = "CALIBRATE";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // clbOperations
            // 
            this.clbOperations.FormattingEnabled = true;
            this.clbOperations.Location = new System.Drawing.Point(12, 12);
            this.clbOperations.Name = "clbOperations";
            this.clbOperations.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.clbOperations.Size = new System.Drawing.Size(365, 199);
            this.clbOperations.TabIndex = 1;
            // 
            // btnBase
            // 
            this.btnBase.Location = new System.Drawing.Point(394, 222);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(394, 36);
            this.btnBase.TabIndex = 2;
            this.btnBase.Text = "BASE";
            this.btnBase.UseVisualStyleBackColor = true;
            this.btnBase.Click += new System.EventHandler(this.btnBase_Click);
            // 
            // btnTurn
            // 
            this.btnTurn.Location = new System.Drawing.Point(394, 54);
            this.btnTurn.Name = "btnTurn";
            this.btnTurn.Size = new System.Drawing.Size(257, 36);
            this.btnTurn.TabIndex = 3;
            this.btnTurn.Text = "TURN";
            this.btnTurn.UseVisualStyleBackColor = true;
            this.btnTurn.Click += new System.EventHandler(this.btnTurn_Click);
            // 
            // btnSpeed
            // 
            this.btnSpeed.Location = new System.Drawing.Point(394, 138);
            this.btnSpeed.Name = "btnSpeed";
            this.btnSpeed.Size = new System.Drawing.Size(257, 36);
            this.btnSpeed.TabIndex = 4;
            this.btnSpeed.Text = "SPEED";
            this.btnSpeed.UseVisualStyleBackColor = true;
            this.btnSpeed.Click += new System.EventHandler(this.btnSpeed_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(394, 96);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(394, 36);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(394, 180);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(257, 36);
            this.btnPause.TabIndex = 6;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // tbSpeed
            // 
            this.tbSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSpeed.Location = new System.Drawing.Point(657, 138);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(82, 35);
            this.tbSpeed.TabIndex = 8;
            this.tbSpeed.Text = "1";
            // 
            // tbTurn
            // 
            this.tbTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTurn.Location = new System.Drawing.Point(657, 55);
            this.tbTurn.Name = "tbTurn";
            this.tbTurn.Size = new System.Drawing.Size(82, 35);
            this.tbTurn.TabIndex = 9;
            this.tbTurn.Text = "90";
            // 
            // tbPause
            // 
            this.tbPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPause.Location = new System.Drawing.Point(657, 179);
            this.tbPause.Name = "tbPause";
            this.tbPause.Size = new System.Drawing.Size(82, 35);
            this.tbPause.TabIndex = 10;
            this.tbPause.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(743, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Deg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(743, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "Hz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(745, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Ms";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 222);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(173, 36);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(204, 222);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(173, 36);
            this.btnExecute.TabIndex = 15;
            this.btnExecute.Text = "EXECUTE";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbLog.Location = new System.Drawing.Point(0, 319);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(800, 84);
            this.tbLog.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 403);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPause);
            this.Controls.Add(this.tbTurn);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSpeed);
            this.Controls.Add(this.btnTurn);
            this.Controls.Add(this.btnBase);
            this.Controls.Add(this.clbOperations);
            this.Controls.Add(this.btnCalibrate);
            this.Name = "MainForm";
            this.Text = "Bosch Rexroth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.CheckedListBox clbOperations;
        private System.Windows.Forms.Button btnBase;
        private System.Windows.Forms.Button btnTurn;
        private System.Windows.Forms.Button btnSpeed;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.TextBox tbTurn;
        private System.Windows.Forms.TextBox tbPause;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbLog;
    }
}

