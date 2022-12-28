namespace IETT_GUI
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
            this.routesContainer = new System.Windows.Forms.SplitContainer();
            this.stopsContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.routesContainer)).BeginInit();
            this.routesContainer.Panel2.SuspendLayout();
            this.routesContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stopsContainer)).BeginInit();
            this.stopsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // routesContainer
            // 
            this.routesContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routesContainer.Location = new System.Drawing.Point(0, 0);
            this.routesContainer.Name = "routesContainer";
            this.routesContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // routesContainer.Panel2
            // 
            this.routesContainer.Panel2.Controls.Add(this.stopsContainer);
            this.routesContainer.Size = new System.Drawing.Size(800, 450);
            this.routesContainer.SplitterDistance = 195;
            this.routesContainer.TabIndex = 1;
            // 
            // stopsContainer
            // 
            this.stopsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopsContainer.Location = new System.Drawing.Point(0, 0);
            this.stopsContainer.Name = "stopsContainer";
            this.stopsContainer.Size = new System.Drawing.Size(800, 251);
            this.stopsContainer.SplitterDistance = 266;
            this.stopsContainer.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.routesContainer);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.routesContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.routesContainer)).EndInit();
            this.routesContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stopsContainer)).EndInit();
            this.stopsContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer routesContainer;
        private System.Windows.Forms.SplitContainer stopsContainer;
    }
}

