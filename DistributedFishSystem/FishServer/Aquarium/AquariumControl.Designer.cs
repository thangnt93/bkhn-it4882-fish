﻿namespace FishServer.Controls
{
    partial class AquariumControl
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
            // AquariumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FishServer.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DoubleBuffered = true;
            this.Name = "AquariumControl";
            this.Size = new System.Drawing.Size(638, 440);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AquariumControl_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AquariumControl_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AquariumControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AquariumControl_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
