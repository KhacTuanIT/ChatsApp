using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatsApp
{
    public partial class frmLogin : Form
    {
        private ServerChatsApp.Model.ChatDbContext context = null;
        private Login login = null;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string user = "user";
        private frmLogin _frmLogin = null;
        public frmLogin()
        {
            InitializeComponent();
            _frmLogin = this;
            context = new ServerChatsApp.Model.ChatDbContext();
            generateId();
            login = new Login(_frmLogin, user);
            login1.Visible = false;
            login.Anchor = login1.Anchor;
            login.Location = login1.Location;
            login.Size = login1.Size;
            login.Visible = true;
            panel2.Controls.Clear();
            panel2.Controls.Add(login);
        }

        public void generateId()
        {
            Guid guid = Guid.NewGuid();
            this.user += guid.ToString();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (login != null)
                {
                    login.Login_Click(sender, e);
                }
            }
        }
    }
}
