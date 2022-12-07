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
    class Working
    {

        #region Ticket
        public static WorkingInfo getWorkingInfo(int intid, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            WorkingInfo obj = new WorkingInfo();
            String strSQL = "SELECT *\r\n" +
                            "FROM TicketTracking\r\n" +
                            "WHERE id=@id\r\n";
            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            sqlcom.Transaction = sqlTran;
            sqlcom.Parameters.Add("@id", SqlDbType.Int).Value = intid;
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
                    CallingErrorForm.GetErrorForm("Can not getting record from tblWorking.", ex);
                }
            }
            return obj;
        }

        public static WorkingInfo ReadData(DataRow objDr)
        {
            WorkingInfo obj = new WorkingInfo();
            try
            {
                obj.id = (int)objDr["id"];               
                obj.title = objDr["title"].ToString();
                obj.description = objDr["description"].ToString();
                obj.create_date = (DateTime)objDr["create_date"];
                obj.create_by = objDr["create_by"].ToString();
                obj.severity_id = (int)objDr["severity_id"];
                obj.status_id = (int)objDr["status_id"];
                obj.is_active = (bool)objDr["is_active"];
                obj.priority_id = (int)objDr["priority_id"];
         
                if (objDr["is_edit"] != DBNull.Value)
                {
                    obj.is_edit = (bool)objDr["is_edit"];
                }
                if (objDr["edit_time"] != DBNull.Value)
                {
                    obj.edit_time = (int)objDr["edit_time"];
                }
                if (objDr["edit_description"] != DBNull.Value)
                {
                    obj.edit_description = objDr["edit_description"].ToString();
                }
                if (objDr["edit_by"] != DBNull.Value)
                {
                    obj.edit_by = objDr["edit_by"].ToString();
                }
                if (objDr["edit_date"] != DBNull.Value)
                {
                    obj.edit_date = (DateTime)objDr["edit_date"];
                }
            }
            catch { }
            return obj;
        }

        public static bool Insert(WorkingInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "INSERT INTO tblTicketTracking(\n\r" +
            "           ,title\r\n" +
            "           ,description\r\n" +
            "           ,create_date\r\n" +
            "           ,create_by\r\n" +
            "           ,severity_id\r\n" +
            "           ,status_id\r\n" +
            "           ,is_active\r\n" +
            "           ,priority_id\r\n" +
            "           ,is_edit\r\n" +
            "           ,edit_time\r\n" +
            "           ,edit_description\r\n" +
            "           ,edit_by\r\n" +
            "           ,edit_date\r\n" +
            ")\r\n" +
            "VALUES(\r\n" +
            "           ,@title\r\n" +
            "           ,@description\r\n" +
            "           ,@create_date\r\n" +
            "           ,@create_by\r\n" +
            "           ,@severity_id\r\n" +
            "           ,@status_id\r\n" +
            "           ,@is_active\r\n" +
            "           ,@priority_id\r\n" +
            "           ,@is_edit\r\n" +
            "           ,@edit_time\r\n" +
            "           ,@edit_description\r\n" +
            "           ,@edit_by\r\n" +
            "           ,@edit_date\r\n" +
            "); select @@IDENTITY ";
            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                CommandHelperInsert(sqlcom, objInfo);
                object obj = sqlcom.ExecuteScalar();
                if (obj != null)
                {
                    objInfo.id = int.Parse(obj.ToString());
                }
                //AuditTrial.Insert("Insert New Working", objInfo.MakeDescription(), cEnums.AuditTrialGroup., sqlTran);
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
                    CallingErrorForm.GetErrorForm("Can not insert record to tblWorking.", ex);
                }
            }
            return blnResult;
        }

        public static bool Update(WorkingInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "UPDATE tblTicketTracking SET\n\r" +
    "           ,title=@title\r\n" +
    "           ,description=@description\r\n" +
    "           ,create_date=@create_date\r\n" +
    "           ,create_by=@create_by\r\n" +
    "           ,severity_id=@severity_id\r\n" +
    "           ,status_id=@status_id\r\n" +
    "           ,is_active=@is_active\r\n" +
    "           ,deadline_date=@deadline_date\r\n" +
    "           ,complete_date=@complete_date\r\n" +
    "           ,person_do=@person_do\r\n" +
    "           ,priority_id=@priority_id\r\n" +
    "           ,is_edit=@is_edit\r\n" +
    "           ,edit_time=@edit_time\r\n" +
    "           ,edit_description=@edit_description\r\n" +
    "           ,edit_by=@edit_by\r\n" +
    "           ,edit_date=@edit_date\r\n" +
                "WHERE id=@id\r\n";
            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                CommandHelperEdit(sqlcom, objInfo);
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
                    CallingErrorForm.GetErrorForm("Can not insert record to tblWorking.", ex);
                }
            }
            return blnResult;
        }

        public static bool UpdateResolved(WorkingInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "UPDATE tblTicketTracking SET \n\r" +
    "           status_id=@status_id\r\n" +
    "           ,is_edit=@is_edit\r\n" +
    "           ,edit_time=@edit_time\r\n" +
    "           ,edit_description=@edit_description\r\n" +
    "           ,edit_by=@edit_by\r\n" +
    "           ,edit_date=@edit_date\r\n" +
                "WHERE id=@id\r\n";
            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                sqlcom.Parameters.Add("@id", SqlDbType.Int).Value = objInfo.id;
                sqlcom.Parameters.Add("@status_id", SqlDbType.Int).Value = objInfo.status_id;
                sqlcom.Parameters.Add("@edit_time", SqlDbType.Int).Value = objInfo.edit_time;
                sqlcom.Parameters.Add("@is_edit", SqlDbType.Bit).Value = objInfo.is_edit;
                sqlcom.Parameters.Add("@edit_description", SqlDbType.NVarChar).Value = objInfo.edit_description;
                sqlcom.Parameters.Add("@edit_by", SqlDbType.NVarChar).Value = objInfo.edit_by;
                sqlcom.Parameters.Add("@edit_date", SqlDbType.DateTime).Value = objInfo.edit_date;
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
                    CallingErrorForm.GetErrorForm("Can not insert record to tblWorking.", ex);
                }
            }
            return blnResult;
        }

        public static bool Delete(WorkingInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "UPDATE tblTicketTracking SET\n\r" +
    "           is_active=@is_active\r\n" +
    "           WHERE id=@id\r\n";
            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                sqlcom.Parameters.Add("@is_active", SqlDbType.Int).Value = objInfo.is_active;
                sqlcom.Parameters.Add("@id", SqlDbType.Int).Value = objInfo.id;
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
                    CallingErrorForm.GetErrorForm("Can not insert record to tblWorking.", ex);
                }
            }
            return blnResult;
        }

        private static void CommandHelperInsert(SqlCommand command, WorkingInfo objInfo)
        {
            try
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = objInfo.id;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = objInfo.title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = objInfo.description;
                command.Parameters.Add("@create_date", SqlDbType.DateTime).Value = objInfo.create_date;
                command.Parameters.Add("@create_by", SqlDbType.NVarChar).Value = objInfo.create_by;
                command.Parameters.Add("@severity_id", SqlDbType.Int).Value = objInfo.severity_id;
                command.Parameters.Add("@status_id", SqlDbType.Int).Value = objInfo.status_id;
                command.Parameters.Add("@is_active", SqlDbType.Bit).Value = objInfo.is_active;
                command.Parameters.Add("@person_do", SqlDbType.NVarChar).Value = objInfo.person_do;
                command.Parameters.Add("@edit_time", SqlDbType.Int).Value = objInfo.edit_time;
                command.Parameters.AddWithValue("@priority_id", SqlDbType.Int).Value = objInfo.priority_id;

                command.Parameters.AddWithValue("@deadline_date", DBNull.Value);
                command.Parameters.AddWithValue("@complete_date", DBNull.Value);                
                command.Parameters.AddWithValue("@location_name", DBNull.Value);
                command.Parameters.AddWithValue("@working_amount", DBNull.Value);
                command.Parameters.AddWithValue("@is_edit", DBNull.Value);
                command.Parameters.AddWithValue("@edit_description", DBNull.Value);
                command.Parameters.AddWithValue("@edit_by", DBNull.Value);
                command.Parameters.AddWithValue("@edit_date", DBNull.Value);
            }
            catch { }
        }

        private static void CommandHelperEdit(SqlCommand command, WorkingInfo objInfo)
        {
            try
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = objInfo.id;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = objInfo.title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = objInfo.description;
                command.Parameters.Add("@create_date", SqlDbType.DateTime).Value = objInfo.create_date;
                command.Parameters.Add("@create_by", SqlDbType.NVarChar).Value = objInfo.create_by;
                command.Parameters.Add("@severity_id", SqlDbType.Int).Value = objInfo.severity_id;
                command.Parameters.Add("@status_id", SqlDbType.Int).Value = objInfo.status_id;
                command.Parameters.Add("@is_active", SqlDbType.Bit).Value = objInfo.is_active;
                command.Parameters.Add("@priority_id", SqlDbType.Int).Value = objInfo.priority_id;
                command.Parameters.Add("@edit_time", SqlDbType.Int).Value = objInfo.edit_time;
                command.Parameters.AddWithValue("@is_edit", DBNull.Value);
                command.Parameters.AddWithValue("@edit_description", DBNull.Value);
                command.Parameters.AddWithValue("@edit_by", DBNull.Value);
                command.Parameters.AddWithValue("@edit_date", DBNull.Value);

            }
            catch { }
        }


        public static DataTable LoadWorkingList(string pTypeName, SqlConnection sqlConn)
        {
            String strSQL =
"select wk.id\r\n" +
",'' as [View]\r\n" +
",st.name as [Status]\r\n" +
",wk.Title\r\n" +
",wk.[Description]\r\n" +
",wk.Create_By\r\n" +
",CONVERT(varchar,wk.Create_Date,103) +' '+ LTRIM(RIGHT(CONVERT(VARCHAR(20), wk.Create_Date, 100), 7))  as Create_Date\r\n" +
",sr.[name] as Severity\r\n" +
",pt.[name] as Priority\r\n" +
"\r\n" +
"from tblWorking wk\r\n" +
"inner join tblStatus st on wk.status_id=st.id\r\n" +
"inner join tblSeverity sr on wk.frequency_id=sr.id\r\n" +
"inner join tblPriority pt on wk.location_id=pt.id\r\n" +
"left join tblTicketType stn on wk.type_id=stn.id\r\n" +
"where wk.is_active=1\r\n" +
"and stn.[name]=@type_name\r\n" +
"\r\n" +
"order by wk.create_date desc\r\n";

            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            sqlcom.Parameters.Add("@type_name", SqlDbType.NVarChar).Value = pTypeName;
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

        public static DataTable LoadWorkingListSolve(string pTypeName, SqlConnection sqlConn)
        {
            String strSQL =
"select wk.id\r\n" +
",'' as [View]\r\n" +
",st.name as [Status]\r\n" +
",wk.Title\r\n" +
",wk.[Description]\r\n" +
",wk.Create_By\r\n" +
",CONVERT(varchar,wk.Create_Date,103) +' '+ LTRIM(RIGHT(CONVERT(VARCHAR(20), wk.Create_Date, 100), 7))  as Create_Date\r\n" +
",sr.[name] as Severity\r\n" +
",pt.[name] as Priority\r\n" +
"\r\n" +
",isnull(wk.edit_by,'') as Resolved_By\r\n" +
",isnull((CONVERT(varchar,wk.edit_date,103)),'')  as Resolved_Date\r\n" +
",isnull(wk.edit_description,'') as Resolved_Description\r\n" +
" \r\n" +
"from tblTicketTracking wk\r\n" +
"inner join tblStatus st on wk.status_id=st.id\r\n" +
"inner join tblSeverity sr on wk.frequency_id=sr.id\r\n" +
"inner join tblPriority pt on wk.location_id=pt.id\r\n" +
"left join tblTicketType stn on wk.type_id=stn.id\r\n" +
"where wk.is_active=1\r\n" +
"and stn.[name]=@type_name\r\n" +
"\r\n" +
"order by wk.create_date desc\r\n";

            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            sqlcom.Parameters.Add("@type_name", SqlDbType.NVarChar).Value = pTypeName;
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

        #endregion

       
    }
}