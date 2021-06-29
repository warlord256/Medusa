namespace Medusa
{
    partial class FormLogin
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
            this.textUserId = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.Hoo = new System.Windows.Forms.Button();
            this.hook_on = new System.Windows.Forms.Button();
            this.serializedText = new System.Windows.Forms.TextBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textUserId
            // 
            this.textUserId.Location = new System.Drawing.Point(320, 127);
            this.textUserId.Name = "textUserId";
            this.textUserId.Size = new System.Drawing.Size(205, 20);
            this.textUserId.TabIndex = 0;
            this.textUserId.Text = "warlord";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(320, 194);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(205, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "123";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "E&XIT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdLogin
            // 
            this.cmdLogin.Location = new System.Drawing.Point(166, 306);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(75, 31);
            this.cmdLogin.TabIndex = 3;
            this.cmdLogin.Text = "LOGIN";
            this.cmdLogin.UseVisualStyleBackColor = true;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "USERNAME: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PASSWORD: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "LOGIN";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(320, 306);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 31);
            this.button4.TabIndex = 8;
            this.button4.Text = "Register";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Hoo
            // 
            this.Hoo.Location = new System.Drawing.Point(612, 321);
            this.Hoo.Name = "Hoo";
            this.Hoo.Size = new System.Drawing.Size(75, 23);
            this.Hoo.TabIndex = 9;
            this.Hoo.Text = "button3";
            this.Hoo.UseVisualStyleBackColor = true;
            // 
            // hook_on
            // 
            this.hook_on.Location = new System.Drawing.Point(593, 306);
            this.hook_on.Name = "hook_on";
            this.hook_on.Size = new System.Drawing.Size(73, 38);
            this.hook_on.TabIndex = 9;
            this.hook_on.Text = "Hook On";
            this.hook_on.UseVisualStyleBackColor = true;
            this.hook_on.Click += new System.EventHandler(this.hook_click);
            // 
            // serializedText
            // 
            this.serializedText.Location = new System.Drawing.Point(318, 256);
            this.serializedText.Name = "serializedText";
            this.serializedText.Size = new System.Drawing.Size(207, 20);
            this.serializedText.TabIndex = 10;
            this.serializedText.TextChanged += new System.EventHandler(this.serializedText_TextChanged);
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(166, 0);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(500, 95);
            this.lstFiles.TabIndex = 11;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.serializedText);
            this.Controls.Add(this.hook_on);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdLogin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textUserId);
            this.Name = "FormLogin";
            this.Text = "LOGIN WINDOW";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textUserId;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Hoo;
        private System.Windows.Forms.Button hook_on;
        private System.Windows.Forms.TextBox serializedText;
        private System.Windows.Forms.ListBox lstFiles;
    }
}

