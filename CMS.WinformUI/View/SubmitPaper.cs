﻿using CMS.DAL.Models;
using CMS.Service.Enums;
using CMS.Service.Global;
using CMS.Service.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace CMS
{
    public partial class SubmitPaper : Form
    {
        private readonly BindingList<Keyword> keywords = new BindingList<Keyword>();

        // paperid is used to know the paperid before a paper entity is created,
        // in which way the two seperated but conneted table entity can be created
        // in "the same time"
        int paperid = 0;
        byte[] content;
        string fileext = "";
        string filename = "";
        bool paperuploaded = false;
        private readonly IKeywordService _keywordService;
        private readonly IPaperService _paperService;
        private readonly IConferenceService _conferenceService;

        public SubmitPaper(IKeywordService keywordService,
            IPaperService paperService,
            IConferenceService conferenceService)
        {
            _keywordService = keywordService;
            _paperService = paperService;
            _conferenceService = conferenceService;
            InitializeComponent();
            Init();
        }

        private void btn_uploadPaper_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileext = Path.GetExtension(openFileDialog1.FileName);
                filename = Path.GetFileName(openFileDialog1.FileName);
                textBox_filePath.Text = openFileDialog1.FileName;
                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    content = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                }
                paperuploaded = true;
            }
        }

        public void Init()
        {
            fileext = "";
            filename = "";
            paperuploaded = false;
            paperid = _paperService.GetMaxPaperId() + 1;
            DisplayKeywords();
            DisplaySelectedKeyword();
        }

        private void DisplayKeywords()
        {
            // ### add orderby
            dataGridView1.DataSource = _keywordService.GetKeyWords();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void DisplaySelectedKeyword()
        {
            keywords.Clear();
            listBox_keyword.DataSource = keywords;
            listBox_keyword.DisplayMember = "Name";
        }

        private void btn_keyAdd_Click(object sender, EventArgs e)
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
                    listBox_keyword.SelectedIndex = listBox_keyword.Items.Count - 1;
                }
            }
        }

        private void btn_keyRmv_Click(object sender, EventArgs e)
        {
            Keyword k = (Keyword)listBox_keyword.SelectedItem;
            keywords.Remove(k);
        }

        private string PaperValidation()
        {
            var deadline = _conferenceService.GetConferenceById(GlobalVariable.UserConference).PaperDeadline;

            if (DateTime.Compare(DateTime.Today, (DateTime)deadline) >= 0)
                return "Paper submition has finished";
            //if (textBox_paperTitle.Text.Trim().Equals(""))
            //    return error = "Paper Title cannot be empty";
            if (textBox_author.Text.Trim().Equals(""))
                return "Paper Author cannot be empty";
            if (comboBox_paperLength.SelectedItem == null)
                return "Paper Length cannot be empty";
            if (!paperuploaded)
                return "Paper has to be uploaded";
            if (keywords.Count == 0)
                return "Paper topic cannot be empty";
            return "";
        }

        private async void btn_savePaper_Click(object sender, EventArgs e)
        {
            string error = PaperValidation();
            if (!error.Equals(""))
            {
                MessageBox.Show(error);
                return;
            }

            var paper = new Paper
            {
                Id = paperid,
                Title = textBox_paperTitle.Text,
                Author = textBox_author.Text,
                Length = (string)comboBox_paperLength.SelectedItem,
                ConferenceId = GlobalVariable.UserConference,
                AuthorId = GlobalVariable.CurrentUser.Id,
                Format = fileext,
                FileName = filename,
                Status = PaperStatusEnum.Submitted.ToString(),
                Content = content,
                SubmissionDate = DateTime.Today
            };

            var topics = new List<PaperTopic>();
            foreach (Keyword k in keywords)
            {
                topics.Add(new PaperTopic
                {
                    PaperId = paperid,
                    KeywordId = k.Id
                });
            }

            await _paperService.AddPaper(paper, topics);

            MessageBox.Show("Save successful!");
            this.Controls.Clear();
            Init();
        }
    }
}
