namespace FishServer
{
    partial class transparentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // transparentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "transparentControl";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.transparentControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transparentControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.transparentControl_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
