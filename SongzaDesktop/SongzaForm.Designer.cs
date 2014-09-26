namespace SongzaDesktop
{
    partial class SongzaForm
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
            this._mainList = new System.Windows.Forms.ListBox();
            this._back = new System.Windows.Forms.Button();
            this._trackTitle = new System.Windows.Forms.Label();
            this._next = new System.Windows.Forms.Button();
            this._artist = new System.Windows.Forms.Label();
            this._album = new System.Windows.Forms.Label();
            this._station = new System.Windows.Forms.Label();
            this._albumArt = new System.Windows.Forms.PictureBox();
            this._progress = new System.Windows.Forms.ProgressBar();
            this._currentTime = new System.Windows.Forms.Label();
            this._duration = new System.Windows.Forms.Label();
            this._playPause = new System.Windows.Forms.Button();
            this._stop = new System.Windows.Forms.Button();
            this._login = new System.Windows.Forms.Button();
            this._download = new System.Windows.Forms.Button();
            this._downloadSongDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._albumArt)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainList
            // 
            this._mainList.FormattingEnabled = true;
            this._mainList.Location = new System.Drawing.Point(96, 106);
            this._mainList.Name = "_mainList";
            this._mainList.Size = new System.Drawing.Size(225, 264);
            this._mainList.TabIndex = 0;
            this._mainList.SelectedIndexChanged += new System.EventHandler(this._mainList_SelectedIndexChanged);
            // 
            // _back
            // 
            this._back.Location = new System.Drawing.Point(15, 168);
            this._back.Name = "_back";
            this._back.Size = new System.Drawing.Size(75, 23);
            this._back.TabIndex = 1;
            this._back.Text = "Back";
            this._back.UseVisualStyleBackColor = true;
            this._back.Click += new System.EventHandler(this._back_Click);
            // 
            // _trackTitle
            // 
            this._trackTitle.AutoSize = true;
            this._trackTitle.Location = new System.Drawing.Point(327, 108);
            this._trackTitle.Name = "_trackTitle";
            this._trackTitle.Size = new System.Drawing.Size(0, 13);
            this._trackTitle.TabIndex = 3;
            this._trackTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _next
            // 
            this._next.Location = new System.Drawing.Point(441, 356);
            this._next.Name = "_next";
            this._next.Size = new System.Drawing.Size(75, 23);
            this._next.TabIndex = 4;
            this._next.Text = "Next Track";
            this._next.UseVisualStyleBackColor = true;
            this._next.Click += new System.EventHandler(this._next_Click);
            // 
            // _artist
            // 
            this._artist.AutoSize = true;
            this._artist.Location = new System.Drawing.Point(327, 121);
            this._artist.Name = "_artist";
            this._artist.Size = new System.Drawing.Size(0, 13);
            this._artist.TabIndex = 5;
            this._artist.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _album
            // 
            this._album.AutoSize = true;
            this._album.Location = new System.Drawing.Point(327, 134);
            this._album.Name = "_album";
            this._album.Size = new System.Drawing.Size(0, 13);
            this._album.TabIndex = 6;
            this._album.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _station
            // 
            this._station.Location = new System.Drawing.Point(15, 9);
            this._station.Name = "_station";
            this._station.Size = new System.Drawing.Size(612, 25);
            this._station.TabIndex = 7;
            this._station.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _albumArt
            // 
            this._albumArt.Location = new System.Drawing.Point(330, 150);
            this._albumArt.Name = "_albumArt";
            this._albumArt.Size = new System.Drawing.Size(200, 200);
            this._albumArt.TabIndex = 8;
            this._albumArt.TabStop = false;
            // 
            // _progress
            // 
            this._progress.Location = new System.Drawing.Point(15, 50);
            this._progress.MarqueeAnimationSpeed = 0;
            this._progress.Maximum = 100000;
            this._progress.Name = "_progress";
            this._progress.Size = new System.Drawing.Size(612, 23);
            this._progress.TabIndex = 9;
            // 
            // _currentTime
            // 
            this._currentTime.Location = new System.Drawing.Point(12, 80);
            this._currentTime.Name = "_currentTime";
            this._currentTime.Size = new System.Drawing.Size(100, 23);
            this._currentTime.TabIndex = 10;
            // 
            // _duration
            // 
            this._duration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._duration.Location = new System.Drawing.Point(298, 80);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(100, 23);
            this._duration.TabIndex = 11;
            this._duration.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _playPause
            // 
            this._playPause.Enabled = false;
            this._playPause.Location = new System.Drawing.Point(360, 356);
            this._playPause.Name = "_playPause";
            this._playPause.Size = new System.Drawing.Size(75, 23);
            this._playPause.TabIndex = 12;
            this._playPause.Text = "Play";
            this._playPause.UseVisualStyleBackColor = true;
            this._playPause.Click += new System.EventHandler(this._playPause_Click);
            // 
            // _stop
            // 
            this._stop.Location = new System.Drawing.Point(523, 355);
            this._stop.Name = "_stop";
            this._stop.Size = new System.Drawing.Size(75, 23);
            this._stop.TabIndex = 13;
            this._stop.Text = "Stop";
            this._stop.UseVisualStyleBackColor = true;
            this._stop.Click += new System.EventHandler(this._stop_Click);
            // 
            // _login
            // 
            this._login.Location = new System.Drawing.Point(15, 139);
            this._login.Name = "_login";
            this._login.Size = new System.Drawing.Size(75, 23);
            this._login.TabIndex = 14;
            this._login.Text = "Login...";
            this._login.UseVisualStyleBackColor = true;
            this._login.Click += new System.EventHandler(this._login_Click);
            // 
            // _download
            // 
            this._download.Location = new System.Drawing.Point(15, 110);
            this._download.Name = "_download";
            this._download.Size = new System.Drawing.Size(75, 23);
            this._download.TabIndex = 15;
            this._download.Text = "Download...";
            this._download.UseVisualStyleBackColor = true;
            this._download.Click += new System.EventHandler(this._download_Click);
            // 
            // SongzaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 391);
            this.Controls.Add(this._download);
            this.Controls.Add(this._login);
            this.Controls.Add(this._stop);
            this.Controls.Add(this._playPause);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._currentTime);
            this.Controls.Add(this._progress);
            this.Controls.Add(this._albumArt);
            this.Controls.Add(this._station);
            this.Controls.Add(this._album);
            this.Controls.Add(this._artist);
            this.Controls.Add(this._next);
            this.Controls.Add(this._trackTitle);
            this.Controls.Add(this._back);
            this.Controls.Add(this._mainList);
            this.Name = "SongzaForm";
            this.Text = "Songza";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._form_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._albumArt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _mainList;
        private System.Windows.Forms.Button _back;
        private System.Windows.Forms.Label _trackTitle;
        private System.Windows.Forms.Button _next;
        private System.Windows.Forms.Label _artist;
        private System.Windows.Forms.Label _album;
        private System.Windows.Forms.Label _station;
        private System.Windows.Forms.PictureBox _albumArt;
        private System.Windows.Forms.ProgressBar _progress;
        private System.Windows.Forms.Label _currentTime;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Button _playPause;
        private System.Windows.Forms.Button _stop;
        private System.Windows.Forms.Button _login;
        private System.Windows.Forms.Button _download;
        private System.Windows.Forms.SaveFileDialog _downloadSongDialog;
    }
}

