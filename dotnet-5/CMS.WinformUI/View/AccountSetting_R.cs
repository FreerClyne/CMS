﻿using CMS.DAL.Models;
using CMS.BL.Enums;
using CMS.BL.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CMS
{
    public partial class AccountSetting_R : Form
    {
        private readonly BindingList<Keyword> keywords = new BindingList<Keyword>();
        private readonly List<Keyword> removedKeywords = new List<Keyword>();
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IKeywordService _keywordService;
        private readonly IConferenceService _conferenceService;
        private readonly IApplicationStrategy _applicationStrategy;

        public AccountSetting_R(IUserService userService,
            IRoleService roleService,
            IKeywordService keywordService,
            IConferenceService conferenceService,
            IApplicationStrategy applicationStrategy)
        {
            _userService = userService;
            _roleService = roleService;
            _keywordService = keywordService;
            _conferenceService = conferenceService;
            _applicationStrategy = applicationStrategy;

            InitializeComponent();
            Init();
        }

        public void Init()
        {
            InitForm();
            KeywordDisplay();
            SelectedKwDisplay();
        }

        private void KeywordDisplay()
        {
            var keywords = _keywordService.GetKeyWords();

            dataGridView1.DataSource = keywords.ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void SelectedKwDisplay()
        {
            var expertises = _keywordService.GetExpertiseByUser(_applicationStrategy.GetLoggedInUserInfo().User.Id);

            foreach (var expertise in expertises)
            {
                this.keywords.Add(new Keyword { Id = expertise.KeywordId, Name = expertise.Keyword.Name });
            }

            listBox1.DataSource = this.keywords;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "KeywordId";
        }

        private void InitForm()
        {
            var currentUser = _applicationStrategy.GetLoggedInUserInfo();

            textBox_name.Text = currentUser.User.Name;
            textBox_email.Text = currentUser.User.Email;
            textBox_cont.Text = currentUser.User.Contact;
            comboBox_role.Text = _roleService.GetRoleById(currentUser.User.RoleId).Type;

            if (_applicationStrategy.GetLoggedInUserInfo().User.RoleId == (int)RoleTypesEnum.Reviewer
                || _applicationStrategy.GetLoggedInUserInfo().User.RoleId == (int)RoleTypesEnum.Author)
                comboBox_conf.Text = _conferenceService.GetConferenceById(currentUser.ConferenceId.Value).Title;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index >= 0)
            {
                bool find = false;
                foreach (Keyword k in keywords)
                    if (k.Id == (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["KeywordId"].Value)
                        find = true;
                if (!find)
                {
                    keywords.Add(new Keyword
                    {
                        Id = (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["KeywordId"].Value,
                        Name = (string)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Name"].Value
                    });
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                removedKeywords.Add(new Keyword { Id = (int)listBox1.SelectedValue });
                keywords.Remove((Keyword)listBox1.SelectedItem);
            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            await _userService.UpdateUser(textBox_name.Text, textBox_email.Text, textBox_cont.Text, textBox_oPass.Text, textBox_nPass.Text);
            await _keywordService.UpdateExpertise(_applicationStrategy.GetLoggedInUserInfo().User.Id, removedKeywords, keywords.ToList());
            MessageBox.Show("Update completed");
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
