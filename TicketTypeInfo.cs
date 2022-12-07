using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Structure
{
    public class TicketTypeInfo
    {


        #region Private Field
        String _Description = "";
        private int _intid;
        private String _strname;
        private String _strdescription;
        private bool _blnis_active;
        private String _strcreate_by;
        private DateTime _datcreate_date;
        #endregion Private Field

        #region Public Property
        public int id
        {
            get { return _intid; }
            set { _intid = value; }
        }
        public String name
        {
            get { return _strname; }
            set { _strname = value; }
        }
        public String description
        {
            get { return _strdescription; }
            set { _strdescription = value; }
        }
        public bool is_active
        {
            get { return _blnis_active; }
            set { _blnis_active = value; }
        }
        public String create_by
        {
            get { return _strcreate_by; }
            set { _strcreate_by = value; }
        }
        public DateTime create_date
        {
            get { return _datcreate_date; }
            set { _datcreate_date = value; }
        }
        #endregion Public Property



    }
}