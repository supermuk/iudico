using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebEditor.Models.Auth
{
    public class OpenIDMembershipProvider : SqlMembershipProvider
    {
        protected string SqlConnectionString;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            SqlConnectionString = ConnectionStringSettings.ConnectionString;

            base.Initialize(name, config);
        }

        public void LinkUserWithOpenID(object UserID, string OpenID)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);
            
            SqlCommand cmd = new SqlCommand("OpenID_LinkUserWithOpenID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OpenID", SqlDbType.NVarChar).Value = OpenID;
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID.ToString();
            
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new ProviderException(e.Message, e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UnlinkUserWithOpenID(object UserID)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);

            SqlCommand cmd = new SqlCommand("OpenID_UnlinkUserWithOpenID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OpenID", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID.ToString();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new ProviderException(e.Message, e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UnlinkUserWithOpenID(string OpenID)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);

            SqlCommand cmd = new SqlCommand("OpenID_UnlinkUserWithOpenID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OpenID", SqlDbType.NVarChar).Value = OpenID;
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
            
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new ProviderException(e.Message, e);
            }
            finally
            {
                conn.Close();
            }
        }

        public string GetOpenIDByUserID(object UserID)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);

            SqlCommand cmd = new SqlCommand("OpenID_GetOpenIDByUserID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;

            string OpenID;

            try
            {
                conn.Open();
                OpenID = cmd.ExecuteScalar().ToString();
            }
            catch (SqlException e)
            {
                throw new ProviderException(e.Message, e);
            }
            finally
            {
                conn.Close();
            }

            return OpenID;
        }

        public MembershipUser GetUser(string openID, bool userIsOnline)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);
            
            SqlCommand cmd = new SqlCommand("OpenID_GetUserIDByOpenID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OpenID", SqlDbType.NVarChar).Value = openID;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    
                    Guid userId = reader.GetGuid(0);

                    return GetUser(userId, userIsOnline);
                }

            }
            catch (SqlException e)
            {
                
                throw new ProviderException(e.Message, e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                conn.Close();
            }

            return null;
        }

        public MembershipUser CreateUser(string username, string password, string email, string openID, out MembershipCreateStatus status)
        {
            MembershipUser user = GetUser(openID, false);

            if (user == null)
            {
                user = CreateUser(username, password, email, null, null, false, null, out status);
                
                LinkUserWithOpenID(user.ProviderUserKey, openID);

                return user;
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public void UpdateUser(OpenIDMembershipUser user)
        {
            base.UpdateUser((MembershipUser)user);

            UnlinkUserWithOpenID(user.ProviderUserKey);

            if (user.OpenID.Length > 0)
            {
                LinkUserWithOpenID(user.ProviderUserKey, user.OpenID);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            MembershipUser user = base.GetUser(providerUserKey, userIsOnline);
            string OpenID = GetOpenIDByUserID(user.ProviderUserKey);

            return new OpenIDMembershipUser(user, OpenID);
        }
    }
}