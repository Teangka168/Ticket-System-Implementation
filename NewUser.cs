using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using WebApp.BLL;
using WebApp.Structure;
using BRC.Utilities;


namespace WebApp.BLL
{
    public class NewUser
    {

        #region raw method

        public NewUserInfo getUserInfo(int intuser_id, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            NewUserInfo obj = new NewUserInfo();
            String strSQL = "SELECT *\r\n" +
                            "FROM tblUser\r\n" +
                            "WHERE user_id=@user_id\r\n";
            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            sqlcom.Transaction = sqlTran;
            sqlcom.Parameters.Add("@user_id", SqlDbType.Int).Value = intuser_id;
            try
            {
                DataTable objData = Common.ExecuteTable(sqlcom);
                if (objData.Rows.Count > 0)
                {
                    DataRow objDr = objData.Rows[0];
                    obj = ReadData(objDr);
                }

            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    throw ex;
                }
                else
                {
                    CallingErrorForm.GetErrorForm("Can not getting record from tblUser.", ex);
                }
            }
            return obj;
        }

        public NewUserInfo ReadData(DataRow objDr)
        {
            NewUserInfo obj = new NewUserInfo();
            obj.user_id = (int)objDr["user_id"];
            obj.first_name = objDr["first_name"].ToString();
            obj.last_name = objDr["last_name"].ToString();
            obj.sex = objDr["sex"].ToString();
            obj.user_name = objDr["user_name"].ToString();
            obj.password1 = objDr["password1"].ToString();
            obj.password2 = objDr["password2"].ToString();
            obj.is_active = (bool)objDr["is_active"];
            obj.group_id = (int)objDr["group_id"];

            return obj;
        }

        public static bool Insert(NewUserInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "INSERT INTO tblUser(\n\r" +
            "           first_name\r\n" +
            "           ,last_name\r\n" +
            "           ,sex\r\n" +
            "           ,user_name\r\n" +
            "           ,password1\r\n" +
            "           ,password2\r\n" +
            "           ,is_active\r\n" +
            "           ,group_id\r\n" +
            ")\r\n" +
            "VALUES(\r\n" +
            "           @first_name\r\n" +
            "           ,@last_name\r\n" +
            "           ,@sex\r\n" +
            "           ,@user_name\r\n" +
            "           ,@password1\r\n" +
            "           ,@password2\r\n" +
            "           ,@is_active\r\n" +
            "           ,@group_id\r\n" +
            "); " +
            "select @@Identity; \r\n";

            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                CommandHelper(sqlcom, objInfo);
                sqlcom.ExecuteNonQuery();

                blnResult = true;
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    throw ex;
                }
                else
                {
                    CallingErrorForm.GetErrorForm("Can not insert record to tblUser.", ex);
                }
            }
            return blnResult;
        }

        private static void CommandHelper(SqlCommand command, NewUserInfo objInfo)
        {
            command.Parameters.Add("@user_id", SqlDbType.Int).Value = objInfo.user_id;
            command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = objInfo.first_name;
            command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = objInfo.last_name;
            command.Parameters.Add("@sex", SqlDbType.NVarChar).Value = objInfo.sex;
            command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = objInfo.user_name;
            command.Parameters.Add("@password1", SqlDbType.NVarChar).Value = objInfo.password1;
            command.Parameters.Add("@password2", SqlDbType.NVarChar).Value = objInfo.password2;
            command.Parameters.Add("@is_active", SqlDbType.Bit).Value = objInfo.is_active;
            command.Parameters.Add("@group_id", SqlDbType.Int).Value = objInfo.group_id;
        }

        public static DataTable LoadUserList( int intUserTypeID ,SqlConnection sqlConn)
        {
            String strSQL = "";

            if (intUserTypeID != 0)
            {
                strSQL =
                "select first_name as [First Name]\r\n" +
                ",last_name as [Last Name]\r\n" +
                ",sex as [Sex]\r\n" +
                ",[user_name] as [User Name]\r\n" +
                ",ug.group_name as [User Type]\r\n" +
                "from tblUser u\r\n" +
                "inner join tblUserGroup ug on u.group_id=ug.group_id\r\n" +
                "where u.is_active=1\r\n" +
                "and u.group_id= " + intUserTypeID;
            }
            else
            {
                strSQL = "select first_name as [First Name]\r\n" +
                 ",last_name as [Last Name]\r\n" +
                 ",sex as [Sex]\r\n" +
                 ",[user_name] as [User Name]\r\n" +
                 ",ug.group_name as [User Type]\r\n" +
                 "from tblUser u\r\n" +
                 "inner join tblUserGroup ug on u.group_id=ug.group_id\r\n" +
                 "where u.is_active=1\r\n";
            }
            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            DataTable objData = new DataTable();
            try
            {
                objData = Common.ExecuteTable(sqlcom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objData;
        }

        #endregion make list

    }
}