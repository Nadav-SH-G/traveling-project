using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace project.Model
{
    public class Helper
    {
        private string conString = "connection string";

        public Helper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            conString = configuration.GetConnectionString("UsersDB");

        }
        public int ExecuteCommandWithResult(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public void ExecuteCommand(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable RetrieveTable(string SQLStr, string table)
        // Gets A table from the data base acording to the SELECT Command in SQLStr;
        // Returns DataTable with the Table.
        {
            // connect to DataBase
            SqlConnection con = new SqlConnection(conString);

            // Build SQL Query
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // Build DataAdapter
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            // Build DataSet to store the data
            DataSet ds = new DataSet();

            // Get Data form DataBase into the DataSet
            ad.Fill(ds, table);



            return ds.Tables[table];
        }

        public int Insert(User user, string table)
        {
            using SqlConnection con = new SqlConnection(conString);

            // בדיקת אם המשתמש כבר קיים (עם פרמטר)
            string checkSql = $"SELECT COUNT(*) FROM {table} WHERE Username = @username";
            using SqlCommand checkCmd = new SqlCommand(checkSql, con);
            checkCmd.Parameters.AddWithValue("@username", user.Username);

            con.Open();
            int count = (int)checkCmd.ExecuteScalar();
            if (count > 0)
            {
                return -1; // משתמש כבר קיים
            }
            con.Close();

            // קבלת סכימה ריקה מהטבלה
            string selectSql = $"SELECT * FROM {table} WHERE 1=0";
            SqlDataAdapter adapter = new SqlDataAdapter(selectSql, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, table);

            DataRow dr = ds.Tables[table].NewRow();
            dr["Firstname"] = user.FirstName;
            dr["Lastname"] = user.LastName;
            dr["Username"] = user.Username;
            dr["Password"] = user.Password;
            dr["Email"] = user.Email;
            dr["Phone"] = user.Phone;
            dr["Birthday"] = user.Birthday;  // תאריך DateTime
            dr["Admin"] = false;

            ds.Tables[table].Rows.Add(dr);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            con.Open();
            int rowsInserted = adapter.Update(ds, table);
            con.Close();

            return rowsInserted;
        }



        public int ExecuteNonQuery(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);

            // ביצוע השאילתא
            con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();

            // return the number of rows affected
            return n;
        }
        public int Delete(int id, string table)
        {
            if (id == 0)
            {
                return -1;
            }
            string SQL = $"DELETE FROM {table} WHERE ID = {id}";
            int n = ExecuteNonQuery(SQL);
            return n;
        }

        public int Update(User user, string table)
        {
            string SQL = $"UPDATE {table} " +
                         $"SET Username = @Username, " +
                         $"Password = @Password, " +
                         $"FirstName = @FirstName, " +
                         $"LastName = @LastName, " +
                         $"Email = @Email, " +
                         $"Phone = @Phone, " +
                         $"Admin = @Admin, " +
                         $"Birthday = @Birthday " +
                         $"WHERE Id = @ID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@FirstName", user.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", user.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Email", user.Email ?? (object)DBNull.Value),
                new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Admin", user.Admin),
                new SqlParameter("@Birthday", user.Birthday),
                new SqlParameter("@ID", user.ID)
            };

            return ExecuteCommandWithResult(SQL, parameters);
        }

        public int Update_disconnected(User user, string table)
        {

            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM {table} WHERE Id = {user.ID}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת סכימת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count == 0)
            {
                return -1;
            }

            // קבלת מצביע לשורה בטבלה
            DataRow dr = ds.Tables[table].Rows[0]; //Get the only row available

            // עדכון השורה
            dr["Firstname"] = user.FirstName;
            dr["Lastname"] = user.LastName;
            dr["Username"] = user.Username;
            dr["Password"] = user.Password;
            dr["Email"] = user.Email;
            dr["Phone"] = user.Phone;
            dr["Birthday"] = user.Birthday.ToString();
            dr["Admin"] = user.Admin;

            // עדכון הדאטה סט בבסיס הנתונים
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        public int Delete_disconnected(int id, string table)
        {
            // The Method recieve an Id and delete it from the user table.
            // The method return the number of rows affected (1) if it succeded.
            // if the id of the user is not in the databse it will return -1

            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM {table} WHERE Id = {id}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת סכימת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            //if (ds.Tables[table].Rows.Count == 0)
            //{
            //    return -1;
            //}

            //// מחיקת השורה
            //for (int i= 0; i < ds.Tables[table].Rows.Count; i++)
            //{
            //    ds.Tables[table].Rows[i].Delete();
            //}
            //foreach (DataRow dr in ds.Tables[table].Rows)
            //{
            //    dr.Delete();
            //}
            ds.Tables[table].Rows[0].Delete();

            // עדכון הדאטה סט בבסיס הנתונים
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        public object GetScalar(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);

            // ביצוע השאילתא
            con.Open();
            object scalar = cmd.ExecuteScalar();
            con.Close();

            return scalar;
        }

        public SqlDataReader GetDataReader(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);

            con.Open();
            // Command behavior insure closing the reader will close the connection
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public DataTable RetrieveTable(string SQLStr, string table, SqlParameter[] parameters = null)
        {
            using SqlConnection con = new SqlConnection(conString);
            using SqlCommand cmd = new SqlCommand(SQLStr, con);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, table);
            return ds.Tables[table];
        }



        public object GetScalar(string SQL, SqlParameter[] parameters = null)
        {
            using SqlConnection con = new SqlConnection(conString);
            using SqlCommand cmd = new SqlCommand(SQL, con);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            return result;
        }


    }
}