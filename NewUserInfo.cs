using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Structure
{
    public class NewUserInfo
    {
        //Created By: 
        //Created Date: 22-Nov-2022
        //Purpose:  User


        #region Private Field
        private int _intuser_id;
        private String _strfirst_name;
        private String _strlast_name;
        private String _strsex;
        private String _struser_name;
        private String _strpassword1;
        private String _strpassword2;
        private bool _blnis_active;
        private int _intgroup_id;

        #endregion Private Field

        #region Public Property
        public int user_id
        {
            get { return _intuser_id; }
            set { _intuser_id = value; }
        }
        public String first_name
        {
            get { return _strfirst_name; }
            set { _strfirst_name = value; }
        }
        public String last_name
        {
            get { return _strlast_name; }
            set { _strlast_name = value; }
        }
        public String sex
        {
            get { return _strsex; }
            set { _strsex = value; }
        }

        public String user_name
        {
            get { return _struser_name; }
            set { _struser_name = value; }
        }
        public String password1
        {
            get { return _strpassword1; }
            set { _strpassword1 = value; }
        }
        public String password2
        {
            get { return _strpassword2; }
            set { _strpassword2 = value; }
        }
        public bool is_active
        {
            get { return _blnis_active; }
            set { _blnis_active = value; }
        }
        
        public int group_id
        {
            get { return _intgroup_id; }
            set { _intgroup_id = value; }
        }

        #endregion Public Property

    }
}