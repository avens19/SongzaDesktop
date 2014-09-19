using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SongzaClasses;

namespace SongzaDesktop
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void _login_Click(object sender, EventArgs e)
        {
            _failMessage.Visible = false;

            try
            {
                User u = await API.Login(_username.Text, _password.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                _failMessage.Visible = true;
            }
        }

        private void _cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
    }
}
