using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerChatsApp.Model;

namespace ChatsApp
{
    public partial class Register : UserControl
    {
        private string user = "user";
        private frmLogin _frmLogin = null;

        public Register(frmLogin _frmLogin, string user)
        {
            InitializeComponent();
            this.user = user;
            this._frmLogin = _frmLogin;
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login(this._frmLogin, this.user);
            login.Location = this.Location;
            login.Width = this.Width;
            login.Height = this.Height;
            login.Anchor = this.Anchor;
            panel1.Controls.Clear();
            panel1.Controls.Add(login);
        }

        public void Register_Click(object sender, EventArgs e)
        {
            this.btnRegister_Click(sender, e);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if ("".Equals(fullname) || "".Equals(password) || "".Equals(password))
            {
                lblWarning.ForeColor = Color.Red;
                lblWarning.Text = "Vui lòng điền đầy đủ các trường";
            }
            else
            {
                if (checkExistAccount(username))
                {
                    lblWarning.ForeColor = Color.Red;
                    lblWarning.Text = username + " đã tồn tại, thử lại";
                }
                else
                {
                    addAccount(username, password, fullname);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtFullname.Text = "";
                    lblWarning.ForeColor = Color.Green;
                    lblWarning.Text = "Đăng ký thành công";
                }
            }
        }

        private void addAccount(string username, string password, string fullname)
        {
            User user = new User()
            {
                Fullname = fullname,
                Username = username,
                Password = password,
                Time = DateTime.Now
            };
        }

        private bool checkExistAccount(string username)
        {
            return false;
        }

        private void txtFullname_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(sender, e);
            }
        }
    }
}
