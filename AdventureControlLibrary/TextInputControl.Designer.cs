namespace AdventureControlLibrary
{
    partial class TextInputControl
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
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 700;
            timer1.Tick += timer1_Tick;
            // 
            // TextInputControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            DoubleBuffered = true;
            Name = "TextInputControl";
            Size = new Size(800, 450);
            Load += TextInputControl_Load;
            Paint += TextInputControl_Paint;
            Resize += TextInputControl_Resize;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
    }
}
