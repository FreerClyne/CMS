﻿using System;
using System.Linq;
using System.Windows.Forms;
using CMSLibrary.Global;
using CMSLibrary.Model;

namespace CMS
{
    public partial class AccountSetting : Form
    {
        public AccountSetting()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            InitForm();
        }

        private void InitForm()
        {
            User user = GlobalVariable.CurrentUser;

            textBox_name.Text = user.userName;
            textBox_email.Text = user.userEmail;
            textBox_cont.Text = user.userContact;
            comboBox_role.Text = DataProcessor.GetRoles().FirstOrDefault(r => r.roleId == user.roleId).roleType;
            
            if (GlobalVariable.CurrentUser.roleId == (int)RoleTypes.Reviewer
                || GlobalVariable.CurrentUser.roleId == (int)RoleTypes.Author)
                comboBox_conf.Text = DataProcessor.GetConferences().FirstOrDefault(c => c.confId == GlobalVariable.UserConference).confTitle;
        }

        // TODO: extract validation method

        private string UserEditValidation()
        {
            if (textBox_name.Text.Trim().Equals(""))
                return "User Name cannot be empty";
            if (textBox_email.Text.Trim().Equals(""))
                return "User Email cannot be empty";

            return "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string error = UserEditValidation();
            if (error.Equals(""))
            {
                DataProcessor.UpdateUser(textBox_name.Text, textBox_email.Text, textBox_cont.Text, textBox_oPass.Text, textBox_nPass.Text);
                MessageBox.Show("Update completed");
            }
            else
                MessageBox.Show(error);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
