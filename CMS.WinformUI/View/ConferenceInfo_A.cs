﻿using CMS.Library.Global;
using CMS.Library.Service;
using System.Windows.Forms;

namespace CMS
{
    public partial class ConferenceInfo_A : Form
    {
        private IUserService _userService;
        private IPaperService _paperService;
        private IConferenceService _conferenceService;

        public ConferenceInfo_A(IUserService userService, IPaperService paperService, IConferenceService conferenceService, int type)
        {
            _userService = userService;
            _paperService = paperService;
            _conferenceService = conferenceService;
            InitializeComponent();
            Init(type);
        }

        public void Init(int type)
        {
            switch (type)
            {
                case (int)ConferenceViewTypes.ConferenceMembers:
                    dataGridView1.DataSource = _conferenceService.GetConferencesUser();
                    break;
                case (int)ConferenceViewTypes.UserInfo:
                    dataGridView1.DataSource = _userService.GetUserRole();
                    break;
                case (int)ConferenceViewTypes.Papers:
                    dataGridView1.DataSource = _paperService.GetPaperUser();
                    break;
                default:
                    break;
            }
        }
    }
}
