namespace NetflixChecker
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.adder = new System.Windows.Forms.Button();
            this.scanner = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(339, 118);
            this.textBox1.TabIndex = 0;
            // 
            // adder
            // 
            this.adder.Location = new System.Drawing.Point(357, 12);
            this.adder.Name = "adder";
            this.adder.Size = new System.Drawing.Size(103, 46);
            this.adder.TabIndex = 1;
            this.adder.Text = "Add accounts";
            this.adder.UseVisualStyleBackColor = true;
            this.adder.Click += new System.EventHandler(this.adder_Click);
            // 
            // scanner
            // 
            this.scanner.Location = new System.Drawing.Point(357, 84);
            this.scanner.Name = "scanner";
            this.scanner.Size = new System.Drawing.Size(103, 46);
            this.scanner.TabIndex = 2;
            this.scanner.Text = "Start scanner";
            this.scanner.UseVisualStyleBackColor = true;
            this.scanner.Click += new System.EventHandler(this.scanner_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(223, 136);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(237, 122);
            this.webBrowser1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(12, 136);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(192, 120);
            this.textBox2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 268);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.scanner);
            this.Controls.Add(this.adder);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Netflix Account Scanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button adder;
        private System.Windows.Forms.Button scanner;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

