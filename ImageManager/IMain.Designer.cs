using System.Windows.Forms;

namespace ImageManager
{
    partial class IMain
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
            this.folderTextBox = new TextBox();
            this.labelControl1 = new Label();
            this.SubFoldersCheckEdit = new CheckBox();
            this.labelControl2 = new Label();
            this.ConvertButton = new Button();
            this.CreateLibrariesButton = new Button();
            this.progressBarControl1 = new ProgressBar();
            this.labelControl3 = new Label();
            this.ProgressLabel = new Label();
            this.simpleButton1 = new Button();
            this.SuspendLayout();
            // 
            // folderTextBox
            // 
            this.folderTextBox.Text = "C:\\Zircon Server\\Data Works\\Test";
            this.folderTextBox.Location = new System.Drawing.Point(113, 12);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(184, 20);
            this.folderTextBox.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Select Folder:";
            // 
            // SubFoldersCheckEdit
            // 
            this.SubFoldersCheckEdit.Location = new System.Drawing.Point(113, 38);
            this.SubFoldersCheckEdit.Name = "SubFoldersCheckEdit";
            this.SubFoldersCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.SubFoldersCheckEdit.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(2, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(105, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Include SubFolders:";
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(113, 63);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(184, 23);
            this.ConvertButton.TabIndex = 5;
            this.ConvertButton.Text = "Convert Libraries";
            this.ConvertButton.Click += new System.EventHandler(this.ConvertLibrariesButton_Click);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(12, 152);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(297, 18);
            this.progressBarControl1.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 133);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Progress:";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Location = new System.Drawing.Point(64, 133);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(51, 13);
            this.ProgressLabel.TabIndex = 8;
            this.ProgressLabel.Text = "<None>";
            // 
            // CreateLibrariesButton
            // 
            this.CreateLibrariesButton.Location = new System.Drawing.Point(113, 92);
            this.CreateLibrariesButton.Name = "CreateLibrariesButton";
            this.CreateLibrariesButton.Size = new System.Drawing.Size(184, 23);
            this.CreateLibrariesButton.TabIndex = 0;
            this.CreateLibrariesButton.Text = "Create Libraries";
            this.CreateLibrariesButton.Click += new System.EventHandler(this.CreateLibrariesButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(32, 92);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // IMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 182);
            // this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.SubFoldersCheckEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.CreateLibrariesButton);
            this.Name = "IMain";
            this.Text = "Image Manager";
            this.Load += new System.EventHandler(this.IMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox folderTextBox;
        private Label labelControl1;
        private CheckBox SubFoldersCheckEdit;
        private Label labelControl2;
        private Button ConvertButton;
        private Button simpleButton1;
        private Button CreateLibrariesButton;
        private Label labelControl3;
        private Label ProgressLabel;
        private ProgressBar progressBarControl1;
    }
}

