using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Structure
{
    class WorkingInfo
    {

        #region Private Field
        String _Description = "";
        private int _intid;
        private String _strtitle;
        private String _strdescription;
        private DateTime _datcreate_date;
        private String _strcreate_by;
        private int _intseverity_id;
        private int _intstatus_id;
        private bool _blnis_active;
        private int _intpriority_id;
        private bool _blnis_edit;
        private int _intedit_time;
        private String _stredit_description;
        private String _stredit_by;
        private DateTime _datedit_date;
        #endregion Private Field

        #region Public Property
        public int id
        {
            get { return _intid; }
            set { _intid = value; }
        }
        public String title
        {
            get { return _strtitle; }
            set { _strtitle = value; }
        }
        public String description
        {
            get { return _strdescription; }
            set { _strdescription = value; }
        }
        public DateTime create_date
        {
            get { return _datcreate_date; }
            set { _datcreate_date = value; }
        }
        public String create_by
        {
            get { return _strcreate_by; }
            set { _strcreate_by = value; }
        }
        public int severity_id
        {
            get { return _intseverity_id; }
            set { _intseverity_id = value; }
        }
        public int status_id
        {
            get { return _intstatus_id; }
            set { _intstatus_id = value; }
        }
        public bool is_active
        {
            get { return _blnis_active; }
            set { _blnis_active = value; }
        }
        public int priority_id
        {
            get { return _intpriority_id; }
            set { _intpriority_id = value; }
        }

        public bool is_edit
        {
            get { return _blnis_edit; }
            set { _blnis_edit = value; }
        }
        public int edit_time
        {
            get { return _intedit_time; }
            set { _intedit_time = value; }
        }
        public String edit_description
        {
            get { return _stredit_description; }
            set { _stredit_description = value; }
        }
        public String edit_by
        {
            get { return _stredit_by; }
            set { _stredit_by = value; }
        }
        public DateTime edit_date
        {
            get { return _datedit_date; }
            set { _datedit_date = value; }
        }
        #endregion Public Property

    }
}