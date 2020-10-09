namespace ImageTest
{
    partial class ImageAspectFixTestWindow
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtImagesPath = new System.Windows.Forms.TextBox();
            this.lblImagesPath = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnProcess.Location = new System.Drawing.Point(167, 94);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(116, 25);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process Images";
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtImagesPath
            // 
            this.txtImagesPath.Location = new System.Drawing.Point(167, 57);
            this.txtImagesPath.Name = "txtImagesPath";
            this.txtImagesPath.Size = new System.Drawing.Size(943, 20);
            this.txtImagesPath.TabIndex = 1;
            // 
            // lblImagesPath
            // 
            this.lblImagesPath.AutoSize = true;
            this.lblImagesPath.Location = new System.Drawing.Point(24, 60);
            this.lblImagesPath.Name = "lblImagesPath";
            this.lblImagesPath.Size = new System.Drawing.Size(114, 13);
            this.lblImagesPath.TabIndex = 2;
            this.lblImagesPath.Text = "Path To Images Folder";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Info;
            this.txtLog.Location = new System.Drawing.Point(167, 152);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(943, 179);
            this.txtLog.TabIndex = 3;
            this.txtLog.Text = "";
            // 
            // ImageAspectFixTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 355);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblImagesPath);
            this.Controls.Add(this.txtImagesPath);
            this.Controls.Add(this.btnProcess);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImageAspectFixTestWindow";
            this.Text = "Image Aspect Fix Test Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtImagesPath;
        private System.Windows.Forms.Label lblImagesPath;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}

