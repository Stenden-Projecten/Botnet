namespace Ananke {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnActivateModule = new System.Windows.Forms.Button();
            this.lstModules = new System.Windows.Forms.ListView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtConsole.Location = new System.Drawing.Point(0, 411);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(784, 150);
            this.txtConsole.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstClients);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 246);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clients";
            // 
            // lstClients
            // 
            this.lstClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClients.FormattingEnabled = true;
            this.lstClients.Location = new System.Drawing.Point(3, 16);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(294, 227);
            this.lstClients.TabIndex = 0;
            this.lstClients.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstClients_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSettings);
            this.groupBox2.Controls.Add(this.btnActivateModule);
            this.groupBox2.Controls.Add(this.lstModules);
            this.groupBox2.Location = new System.Drawing.Point(536, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 226);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modules";
            // 
            // btnActivateModule
            // 
            this.btnActivateModule.Enabled = false;
            this.btnActivateModule.Location = new System.Drawing.Point(6, 197);
            this.btnActivateModule.Name = "btnActivateModule";
            this.btnActivateModule.Size = new System.Drawing.Size(75, 23);
            this.btnActivateModule.TabIndex = 4;
            this.btnActivateModule.Text = "Activate";
            this.btnActivateModule.UseVisualStyleBackColor = true;
            this.btnActivateModule.Click += new System.EventHandler(this.btnActivateModule_Click);
            // 
            // lstModules
            // 
            this.lstModules.CheckBoxes = true;
            this.lstModules.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstModules.HideSelection = false;
            this.lstModules.Location = new System.Drawing.Point(3, 16);
            this.lstModules.MultiSelect = false;
            this.lstModules.Name = "lstModules";
            this.lstModules.Size = new System.Drawing.Size(230, 175);
            this.lstModules.TabIndex = 3;
            this.lstModules.UseCompatibleStateImageBehavior = false;
            this.lstModules.View = System.Windows.Forms.View.List;
            this.lstModules.SelectedIndexChanged += new System.EventHandler(this.lstModules_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(101, 326);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(467, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // btnSettings
            // 
            this.btnSettings.Enabled = false;
            this.btnSettings.Location = new System.Drawing.Point(87, 197);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtConsole);
            this.Name = "MainForm";
            this.Text = "Ananke";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstModules;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnActivateModule;
        private System.Windows.Forms.Button btnSettings;
    }
}

