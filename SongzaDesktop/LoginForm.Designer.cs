namespace SongzaDesktop
{
    partial class LoginForm
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
            this._username = new System.Windows.Forms.TextBox();
            this._password = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._login = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._failMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _username
            // 
            this._username.Location = new System.Drawing.Point(18, 25);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(118, 20);
            this._username.TabIndex = 0;
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(18, 80);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(118, 20);
            this._password.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Password";
            // 
            // _login
            // 
            this._login.Location = new System.Drawing.Point(40, 106);
            this._login.Name = "_login";
            this._login.Size = new System.Drawing.Size(75, 23);
            this._login.TabIndex = 2;
            this._login.Text = "Login";
            this._login.UseVisualStyleBackColor = true;
            this._login.Click += new System.EventHandler(this._login_Click);
            // 
            // _cancel
            // 
            this._cancel.Location = new System.Drawing.Point(40, 135);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 3;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // _failMessage
            // 
            this._failMessage.AutoSize = true;
            this._failMessage.ForeColor = System.Drawing.Color.Red;
            this._failMessage.Location = new System.Drawing.Point(59, 165);
            this._failMessage.Name = "_failMessage";
            this._failMessage.Size = new System.Drawing.Size(35, 13);
            this._failMessage.TabIndex = 6;
            this._failMessage.Text = "Failed";
            this._failMessage.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(157, 188);
            this.Controls.Add(this._failMessage);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._password);
            this.Controls.Add(this._username);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _username;
        private System.Windows.Forms.MaskedTextBox _password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _login;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Label _failMessage;
    }
}