﻿using CMS.BL.Services.Interface;
using System.Windows.Forms;

namespace CMS
{
    public partial class ConferenceInfo : Form
    {
        private readonly IPaperService _paperService;
        private readonly IConferenceService _conferenceService;

        public ConferenceInfo(IPaperService paperService, IConferenceService conferenceService)
        {
            _paperService = paperService;
            _conferenceService = conferenceService;
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            dataGridView1.DataSource = _conferenceService.GetReviewersByConference();

            dataGridView2.DataSource = _paperService.GetPapersWithConference();
        }
    }
}
