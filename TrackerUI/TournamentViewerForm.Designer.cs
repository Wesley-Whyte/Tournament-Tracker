namespace TrackerUI
{
    partial class TournamentViewerForm
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.roundComboBox = new System.Windows.Forms.ComboBox();
            this.unplyedOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.matchupListBox = new System.Windows.Forms.ListBox();
            this.teamOneLabel = new System.Windows.Forms.Label();
            this.TeamTwoLabel = new System.Windows.Forms.Label();
            this.teamOneScoreLabel = new System.Windows.Forms.Label();
            this.teamTwoScoreLabel = new System.Windows.Forms.Label();
            this.versusLabel = new System.Windows.Forms.Label();
            this.teamOneScoreTextBox = new System.Windows.Forms.TextBox();
            this.teamTwoScoreTextBox = new System.Windows.Forms.TextBox();
            this.scoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Light", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.headerLabel.Location = new System.Drawing.Point(11, 45);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(255, 60);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Tournament:";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Segoe UI Light", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tournamentNameLabel.Location = new System.Drawing.Point(255, 45);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(187, 60);
            this.tournamentNameLabel.TabIndex = 1;
            this.tournamentNameLabel.Text = "<name>";
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.roundLabel.Location = new System.Drawing.Point(14, 120);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(95, 38);
            this.roundLabel.TabIndex = 2;
            this.roundLabel.Text = "Round";
            // 
            // roundComboBox
            // 
            this.roundComboBox.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.roundComboBox.FormattingEnabled = true;
            this.roundComboBox.Location = new System.Drawing.Point(150, 120);
            this.roundComboBox.Name = "roundComboBox";
            this.roundComboBox.Size = new System.Drawing.Size(292, 45);
            this.roundComboBox.TabIndex = 3;
            // 
            // unplyedOnlyCheckBox
            // 
            this.unplyedOnlyCheckBox.AutoSize = true;
            this.unplyedOnlyCheckBox.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unplyedOnlyCheckBox.Location = new System.Drawing.Point(150, 171);
            this.unplyedOnlyCheckBox.Name = "unplyedOnlyCheckBox";
            this.unplyedOnlyCheckBox.Size = new System.Drawing.Size(157, 32);
            this.unplyedOnlyCheckBox.TabIndex = 4;
            this.unplyedOnlyCheckBox.Text = "Unplayed Only";
            this.unplyedOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // matchupListBox
            // 
            this.matchupListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.matchupListBox.FormattingEnabled = true;
            this.matchupListBox.ItemHeight = 37;
            this.matchupListBox.Location = new System.Drawing.Point(21, 241);
            this.matchupListBox.Name = "matchupListBox";
            this.matchupListBox.Size = new System.Drawing.Size(421, 411);
            this.matchupListBox.TabIndex = 5;
            // 
            // teamOneLabel
            // 
            this.teamOneLabel.AutoSize = true;
            this.teamOneLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamOneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.teamOneLabel.Location = new System.Drawing.Point(527, 291);
            this.teamOneLabel.Name = "teamOneLabel";
            this.teamOneLabel.Size = new System.Drawing.Size(165, 38);
            this.teamOneLabel.TabIndex = 6;
            this.teamOneLabel.Text = "<team one>";
            // 
            // TeamTwoLabel
            // 
            this.TeamTwoLabel.AutoSize = true;
            this.TeamTwoLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamTwoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.TeamTwoLabel.Location = new System.Drawing.Point(527, 534);
            this.TeamTwoLabel.Name = "TeamTwoLabel";
            this.TeamTwoLabel.Size = new System.Drawing.Size(163, 38);
            this.TeamTwoLabel.TabIndex = 7;
            this.TeamTwoLabel.Text = "<team two>";
            // 
            // teamOneScoreLabel
            // 
            this.teamOneScoreLabel.AutoSize = true;
            this.teamOneScoreLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamOneScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.teamOneScoreLabel.Location = new System.Drawing.Point(527, 349);
            this.teamOneScoreLabel.Name = "teamOneScoreLabel";
            this.teamOneScoreLabel.Size = new System.Drawing.Size(81, 38);
            this.teamOneScoreLabel.TabIndex = 8;
            this.teamOneScoreLabel.Text = "Score";
            // 
            // teamTwoScoreLabel
            // 
            this.teamTwoScoreLabel.AutoSize = true;
            this.teamTwoScoreLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamTwoScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.teamTwoScoreLabel.Location = new System.Drawing.Point(527, 590);
            this.teamTwoScoreLabel.Name = "teamTwoScoreLabel";
            this.teamTwoScoreLabel.Size = new System.Drawing.Size(81, 38);
            this.teamTwoScoreLabel.TabIndex = 9;
            this.teamTwoScoreLabel.Text = "Score";
            // 
            // versusLabel
            // 
            this.versusLabel.AutoSize = true;
            this.versusLabel.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.versusLabel.Location = new System.Drawing.Point(527, 431);
            this.versusLabel.Name = "versusLabel";
            this.versusLabel.Size = new System.Drawing.Size(47, 38);
            this.versusLabel.TabIndex = 10;
            this.versusLabel.Text = "VS";
            // 
            // teamOneScoreTextBox
            // 
            this.teamOneScoreTextBox.Location = new System.Drawing.Point(614, 346);
            this.teamOneScoreTextBox.Name = "teamOneScoreTextBox";
            this.teamOneScoreTextBox.Size = new System.Drawing.Size(135, 43);
            this.teamOneScoreTextBox.TabIndex = 11;
            // 
            // teamTwoScoreTextBox
            // 
            this.teamTwoScoreTextBox.Location = new System.Drawing.Point(614, 587);
            this.teamTwoScoreTextBox.Name = "teamTwoScoreTextBox";
            this.teamTwoScoreTextBox.Size = new System.Drawing.Size(135, 43);
            this.teamTwoScoreTextBox.TabIndex = 12;
            // 
            // scoreButton
            // 
            this.scoreButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.scoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.scoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.scoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scoreButton.Location = new System.Drawing.Point(760, 427);
            this.scoreButton.Name = "scoreButton";
            this.scoreButton.Size = new System.Drawing.Size(99, 45);
            this.scoreButton.TabIndex = 13;
            this.scoreButton.Text = "Score";
            this.scoreButton.UseVisualStyleBackColor = true;
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(928, 705);
            this.Controls.Add(this.scoreButton);
            this.Controls.Add(this.teamTwoScoreTextBox);
            this.Controls.Add(this.teamOneScoreTextBox);
            this.Controls.Add(this.versusLabel);
            this.Controls.Add(this.teamTwoScoreLabel);
            this.Controls.Add(this.teamOneScoreLabel);
            this.Controls.Add(this.TeamTwoLabel);
            this.Controls.Add(this.teamOneLabel);
            this.Controls.Add(this.matchupListBox);
            this.Controls.Add(this.unplyedOnlyCheckBox);
            this.Controls.Add(this.roundComboBox);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.ComboBox roundComboBox;
        private System.Windows.Forms.CheckBox unplyedOnlyCheckBox;
        private System.Windows.Forms.ListBox matchupListBox;
        private System.Windows.Forms.Label teamOneLabel;
        private System.Windows.Forms.Label TeamTwoLabel;
        private System.Windows.Forms.Label teamOneScoreLabel;
        private System.Windows.Forms.Label teamTwoScoreLabel;
        private System.Windows.Forms.Label versusLabel;
        private System.Windows.Forms.TextBox teamOneScoreTextBox;
        private System.Windows.Forms.TextBox teamTwoScoreTextBox;
        private System.Windows.Forms.Button scoreButton;
    }
}

