namespace PerformanceReport.ImageCreator
{
    partial class Form1
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
            pbCreatedImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbCreatedImage).BeginInit();
            SuspendLayout();
            // 
            // pbCreatedImage
            // 
            pbCreatedImage.BackColor = SystemColors.GradientInactiveCaption;
            pbCreatedImage.Location = new Point(12, 50);
            pbCreatedImage.Name = "pbCreatedImage";
            pbCreatedImage.Size = new Size(776, 278);
            pbCreatedImage.TabIndex = 0;
            pbCreatedImage.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pbCreatedImage);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbCreatedImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbCreatedImage;
    }
}
