namespace AdventureControlLibraryTestProject
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
            textInputControl1 = new AdventureControlLibrary.TextInputControl();
            SuspendLayout();
            // 
            // textInputControl1
            // 
            textInputControl1.BackColor = Color.Black;
            textInputControl1.Location = new Point(8, 180);
            textInputControl1.Name = "textInputControl1";
            textInputControl1.Size = new Size(1084, 256);
            textInputControl1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 728);
            Controls.Add(textInputControl1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private AdventureControlLibrary.TextInputControl textInputControl1;
    }
}
