namespace ProjectOne
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
            connectMainButton = new Button();
            nintendoDirectoryView = new ListView();
            downloadSaveFileButton = new Button();
            backButton = new Button();
            timePenaltyButton = new Button();
            loadSaveButton = new Button();
            saveConnectionButton = new Button();
            rootDisplayButton = new Button();
            loadConnectionButton = new Button();
            SuspendLayout();
            // 
            // connectMainButton
            // 
            resources.ApplyResources(connectMainButton, "connectMainButton");
            connectMainButton.Name = "connectMainButton";
            connectMainButton.UseVisualStyleBackColor = true;
            // 
            // nintendoDirectoryView
            // 
            resources.ApplyResources(nintendoDirectoryView, "nintendoDirectoryView");
            nintendoDirectoryView.Name = "nintendoDirectoryView";
            nintendoDirectoryView.Sorting = SortOrder.Ascending;
            nintendoDirectoryView.UseCompatibleStateImageBehavior = false;
            // 
            // downloadSaveFileButton
            // 
            resources.ApplyResources(downloadSaveFileButton, "downloadSaveFileButton");
            downloadSaveFileButton.Name = "downloadSaveFileButton";
            // 
            // backButton
            // 
            resources.ApplyResources(backButton, "backButton");
            backButton.Name = "backButton";
            // 
            // timePenaltyButton
            // 
            resources.ApplyResources(timePenaltyButton, "timePenaltyButton");
            timePenaltyButton.Name = "timePenaltyButton";
            timePenaltyButton.UseVisualStyleBackColor = true;
            // 
            // loadSaveButton
            // 
            resources.ApplyResources(loadSaveButton, "loadSaveButton");
            loadSaveButton.Name = "loadSaveButton";
            loadSaveButton.UseVisualStyleBackColor = true;
            // 
            // saveConnectionButton
            // 
            resources.ApplyResources(saveConnectionButton, "saveConnectionButton");
            saveConnectionButton.Name = "saveConnectionButton";
            saveConnectionButton.UseVisualStyleBackColor = true;
            // 
            // rootDisplayButton
            // 
            resources.ApplyResources(rootDisplayButton, "rootDisplayButton");
            rootDisplayButton.Name = "rootDisplayButton";
            // 
            // loadConnectionButton
            // 
            resources.ApplyResources(loadConnectionButton, "loadConnectionButton");
            loadConnectionButton.Name = "loadConnectionButton";
            loadConnectionButton.UseVisualStyleBackColor = true;
            // 
            // TomodachiLife
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(loadConnectionButton);
            Controls.Add(rootDisplayButton);
            Controls.Add(saveConnectionButton);
            Controls.Add(loadSaveButton);
            Controls.Add(timePenaltyButton);
            Controls.Add(backButton);
            Controls.Add(downloadSaveFileButton);
            Controls.Add(nintendoDirectoryView);
            Controls.Add(connectMainButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TomodachiLife";
            Load += TomodachiLife_Load;
            ResumeLayout(false);
        }


        #endregion

        private Button connectMainButton;
        private ListView nintendoDirectoryView;
        private Button downloadSaveFileButton;
        private Button backButton;
        private Button timePenaltyButton;
        private Button loadSaveButton;
        private Button saveConnectionButton;
        private Button rootDisplayButton;
        private Button loadConnectionButton;
    }
}