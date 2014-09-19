namespace SongzaDesktop
{
    partial class SearchForm
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
            this._query = new System.Windows.Forms.TextBox();
            this._search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _query
            // 
            this._query.Location = new System.Drawing.Point(12, 12);
            this._query.Name = "_query";
            this._query.Size = new System.Drawing.Size(306, 20);
            this._query.TabIndex = 0;
            // 
            // _search
            // 
            this._search.Location = new System.Drawing.Point(325, 11);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(75, 23);
            this._search.TabIndex = 1;
            this._search.Text = "Search";
            this._search.UseVisualStyleBackColor = true;
            this._search.Click += new System.EventHandler(this._search_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 43);
            this.Controls.Add(this._search);
            this.Controls.Add(this._query);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _query;
        private System.Windows.Forms.Button _search;
    }
}