using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace ChatsApp
{
    public partial class frmConfigSettings : Form
    {
        private static NameValueCollection settings = null;
        private string key = "";
        private string value = "";
        NameValueCollection ReadAllSettings()
        {
            NameValueCollection appSettings = null;
            try
            {
                appSettings = ConfigurationManager.AppSettings;
            }
            catch (ConfigurationErrorsException)
            {
                lblNotification.Text = "Lỗi không thể đọc cài đặt của ứng dụng";
            }
            return appSettings;
        }

        private void addUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                lblNotification.Text = "Lỗi ghi cài đặt của ứng dụng";
            }
        }

        private void setUpComboBox()
        {
            if (settings != null)
            {
                cbbKey.Items.Clear();
                foreach (var key in settings)
                {
                    cbbKey.Items.Add(key);
                }
            }
            else
            {
                lblNotification.Text = "Không có cài đặt nào trong hệ thống";
            }
        }

        public frmConfigSettings()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedItem = cbbKey.SelectedItem;
            key = selectedItem.ToString();

            if (key != "" || key != null)
            {
                value = settings[key];
                txtValue.Text = value;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.value = "";
            this.key = "";
            this.cbbKey.Items.Clear();
            this.lblNotification.Text = "";
            this.txtValue.Text = "";
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (key != "" && value != "")
            {
                addUpdateAppSettings(key, value);
                MessageBox.Show("Update Success!", "Update Configuration");
            }
        }


        private void frmConfigSettings_Load(object sender, EventArgs e)
        {
            ReadAllSettings();
        }
    }
}
