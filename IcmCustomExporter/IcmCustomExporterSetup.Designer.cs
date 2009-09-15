namespace IcmCustomExporter
{
    partial class IcmCustomExporterSetup
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
            this.label_Destination = new System.Windows.Forms.Label();
            this.label_FileType = new System.Windows.Forms.Label();
            this.input_Destination = new System.Windows.Forms.TextBox();
            this.button_BrowseDestination = new System.Windows.Forms.Button();
            this.combo_FileType = new System.Windows.Forms.ComboBox();
            this.button_SetupFileType = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.destinationBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.option_Single = new System.Windows.Forms.RadioButton();
            this.option_Multi = new System.Windows.Forms.RadioButton();
            this.delete_first_page_checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_Destination
            // 
            this.label_Destination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Destination.AutoSize = true;
            this.label_Destination.Location = new System.Drawing.Point(12, 9);
            this.label_Destination.Name = "label_Destination";
            this.label_Destination.Size = new System.Drawing.Size(60, 13);
            this.label_Destination.TabIndex = 0;
            this.label_Destination.Text = "Destination";
            // 
            // label_FileType
            // 
            this.label_FileType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.label_FileType.AutoSize = true;
            this.label_FileType.Location = new System.Drawing.Point(12, 48);
            this.label_FileType.Name = "label_FileType";
            this.label_FileType.Size = new System.Drawing.Size(46, 13);
            this.label_FileType.TabIndex = 3;
            this.label_FileType.Text = "File type";
            // 
            // input_Destination
            // 
            this.input_Destination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.input_Destination.Location = new System.Drawing.Point(15, 25);
            this.input_Destination.Name = "input_Destination";
            this.input_Destination.ReadOnly = true;
            this.input_Destination.Size = new System.Drawing.Size(292, 20);
            this.input_Destination.TabIndex = 1;
            // 
            // button_BrowseDestination
            // 
            this.button_BrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_BrowseDestination.Location = new System.Drawing.Point(313, 25);
            this.button_BrowseDestination.Name = "button_BrowseDestination";
            this.button_BrowseDestination.Size = new System.Drawing.Size(70, 20);
            this.button_BrowseDestination.TabIndex = 2;
            this.button_BrowseDestination.Text = "&Browse";
            this.button_BrowseDestination.UseVisualStyleBackColor = true;
            this.button_BrowseDestination.Click += new System.EventHandler(this.button_BrowseDestination_Click);
            // 
            // combo_FileType
            // 
            this.combo_FileType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_FileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_FileType.FormattingEnabled = true;
            this.combo_FileType.Location = new System.Drawing.Point(15, 64);
            this.combo_FileType.Name = "combo_FileType";
            this.combo_FileType.Size = new System.Drawing.Size(292, 21);
            this.combo_FileType.TabIndex = 4;
            this.combo_FileType.SelectedIndexChanged += new System.EventHandler(this.combo_FileType_SelectedIndexChanged);
            // 
            // button_SetupFileType
            // 
            this.button_SetupFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SetupFileType.Enabled = false;
            this.button_SetupFileType.Location = new System.Drawing.Point(313, 64);
            this.button_SetupFileType.Name = "button_SetupFileType";
            this.button_SetupFileType.Size = new System.Drawing.Size(70, 20);
            this.button_SetupFileType.TabIndex = 5;
            this.button_SetupFileType.Text = "&Setup";
            this.button_SetupFileType.UseVisualStyleBackColor = true;
            this.button_SetupFileType.Click += new System.EventHandler(this.button_SetupFileType_Click);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(237, 112);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(70, 23);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(313, 112);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(70, 23);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // option_Single
            // 
            this.option_Single.AutoSize = true;
            this.option_Single.Location = new System.Drawing.Point(15, 92);
            this.option_Single.Name = "option_Single";
            this.option_Single.Size = new System.Drawing.Size(81, 17);
            this.option_Single.TabIndex = 8;
            this.option_Single.TabStop = true;
            this.option_Single.Text = "Single page";
            this.option_Single.UseVisualStyleBackColor = true;
            this.option_Single.CheckedChanged += new System.EventHandler(this.option_SingleMulti_CheckedChanged);
            // 
            // option_Multi
            // 
            this.option_Multi.AutoSize = true;
            this.option_Multi.Location = new System.Drawing.Point(107, 92);
            this.option_Multi.Name = "option_Multi";
            this.option_Multi.Size = new System.Drawing.Size(74, 17);
            this.option_Multi.TabIndex = 9;
            this.option_Multi.TabStop = true;
            this.option_Multi.Text = "Multi page";
            this.option_Multi.UseVisualStyleBackColor = true;
            this.option_Multi.CheckedChanged += new System.EventHandler(this.option_SingleMulti_CheckedChanged);
            // 
            // delete_first_page_checkbox
            // 
            this.delete_first_page_checkbox.AutoSize = true;
            this.delete_first_page_checkbox.Location = new System.Drawing.Point(16, 116);
            this.delete_first_page_checkbox.Name = "delete_first_page_checkbox";
            this.delete_first_page_checkbox.Size = new System.Drawing.Size(103, 17);
            this.delete_first_page_checkbox.TabIndex = 10;
            this.delete_first_page_checkbox.Text = "Delete first page";
            this.delete_first_page_checkbox.UseVisualStyleBackColor = true;
            this.delete_first_page_checkbox.CheckedChanged += new System.EventHandler(this.delete_first_page_checkbox_CheckedChanged);
            // 
            // CustomReleaseSetup
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(395, 149);
            this.Controls.Add(this.delete_first_page_checkbox);
            this.Controls.Add(this.option_Multi);
            this.Controls.Add(this.option_Single);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_SetupFileType);
            this.Controls.Add(this.combo_FileType);
            this.Controls.Add(this.button_BrowseDestination);
            this.Controls.Add(this.input_Destination);
            this.Controls.Add(this.label_FileType);
            this.Controls.Add(this.label_Destination);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CustomReleaseSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Release Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Destination;
        private System.Windows.Forms.Label label_FileType;
        private System.Windows.Forms.TextBox input_Destination;
        private System.Windows.Forms.Button button_BrowseDestination;
        private System.Windows.Forms.ComboBox combo_FileType;
        private System.Windows.Forms.Button button_SetupFileType;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.FolderBrowserDialog destinationBrowserDialog;
        private System.Windows.Forms.RadioButton option_Single;
        private System.Windows.Forms.RadioButton option_Multi;
        private System.Windows.Forms.CheckBox delete_first_page_checkbox;
    }
}