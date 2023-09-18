﻿namespace ProjectOne
{
    partial class TomodachiLife
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TomodachiLife));
            ConnectMain = new Button();
            nintendoDirectory = new ListView();
            nintendoRetrieve = new Button();
            loadSaveFile = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            backButton = new Button();
            SuspendLayout();
            // 
            // ConnectMain
            // 
            resources.ApplyResources(ConnectMain, "ConnectMain");
            ConnectMain.Name = "ConnectMain";
            ConnectMain.UseVisualStyleBackColor = true;
            ConnectMain.Click += ConnectMain_Click;
            // 
            // nintendoDirectory
            // 
            resources.ApplyResources(nintendoDirectory, "nintendoDirectory");
            nintendoDirectory.Name = "nintendoDirectory";
            nintendoDirectory.UseCompatibleStateImageBehavior = false;
            // 
            // nintendoRetrieve
            // 
            resources.ApplyResources(nintendoRetrieve, "nintendoRetrieve");
            nintendoRetrieve.Name = "nintendoRetrieve";
            // 
            // loadSaveFile
            // 
            resources.ApplyResources(loadSaveFile, "loadSaveFile");
            loadSaveFile.Name = "loadSaveFile";
            loadSaveFile.Click += loadSaveFile_Click;
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // backButton
            // 
            resources.ApplyResources(backButton, "backButton");
            backButton.Name = "backButton";
            backButton.Click += backButton_Click;
            // 
            // TomodachiLife
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(backButton);
            Controls.Add(loadSaveFile);
            Controls.Add(nintendoRetrieve);
            Controls.Add(nintendoDirectory);
            Controls.Add(ConnectMain);
            Name = "TomodachiLife";
            Load += TomodachiLife_Load;
            ResumeLayout(false);
        }


        #endregion

        private Button ConnectMain;
        private ListView nintendoDirectory;
        private Button nintendoRetrieve;
        private Button loadSaveFile;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button backButton;
    }
}