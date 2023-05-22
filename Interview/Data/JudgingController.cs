using Interview.Models;
using Interview.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using static Org.BouncyCastle.Math.EC.ECCurve;

using MailKit.Net.Smtp;

namespace Interview.Data
{
    [ApiController]
    public class JudgingController : ControllerBase
    {

        /// <summary>
        /// api lấy list usser cho  judging.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getAllUserNeedToJudge")]
        public async Task<List<Users>> getUserNeedToJudge()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_getUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {
                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),

                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                               
                            });
                        }
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// api lấy list usser cho  judged.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getAllUserIsJudge")]
        public async Task<List<Users>> getUserIsJudged()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_getUserJudged", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {
                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Comment = reader["comment"] == DBNull.Value ? string.Empty : reader.GetString("comment"),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// api lấy list usser cho  judging.razor dựa theo filter.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/filterUserNeedToJudge")]
        public async Task<List<Users>> getFilterNeedToJudge([FromQuery(Name = "filterBy")] string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_userFilter", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@role",
                        DbType = DbType.String,
                        Value = role,
                        Direction = ParameterDirection.Input,
                    });
                    DateTime dt1 = new DateTime();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {

                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),


                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                            });
                        }
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// api lấy list usser cho  judged.razor dựa theo filter.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/filterUserIsJudge")]
        public async Task<List<Users>> getFilterIsJudged([FromQuery(Name = "filterBy")] string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_userJudgedFilter", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@role",
                        DbType = DbType.String,
                        Value = role,
                        Direction = ParameterDirection.Input,
                    });
                    DateTime dt1 = new DateTime();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {

                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Comment = reader["comment"] == DBNull.Value ? string.Empty : reader.GetString("comment"),
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                            });
                        }
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// api lấy list usser cho  judging.razor dựa theo order.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/orderUserNeedToJudge")]
        public async Task<List<Users>> getOrderNeedToJudge([FromQuery] int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "Judging_userOrderDESC";
                }
                else if (order == 1)
                {
                    orderStatus = "Judging_userOrderASC";
                }
                using (MySqlCommand cmd = new MySqlCommand(orderStatus, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {
                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),


                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// api lấy list usser cho  judged.razor dựa theo filter.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/orderUserIsJudge")]
        public async Task<List<Users>> getOrderIsJudged([FromQuery] int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "Judging_userJudgedOrderDESC";
                }
                else if (order == 1)
                {
                    orderStatus = "Judging_userJudgedOrderASC";
                }
                using (MySqlCommand cmd = new MySqlCommand(orderStatus, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {
                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Comment = reader["comment"] == DBNull.Value ? string.Empty : reader.GetString("comment"),
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// api lấy list usser cho  judging.razor dựa theo search.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/searchUserNeedToJudge")]
        public async Task<List<Users>> getSearchNeedToJudge([FromQuery(Name = "research")] string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_userSearch", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@search",
                        DbType = DbType.String,
                        Value = search,
                        Direction = ParameterDirection.Input,
                    });

                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {

                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// api lấy list usser cho  judged.razor dựa theo search.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/searchUserIsJudge")]
        public async Task<List<Users>> getSearchIsJudged([FromQuery(Name = "research")] string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Judging_userJudgedSearch", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@search",
                        DbType = DbType.String,
                        Value = search,
                        Direction = ParameterDirection.Input,
                    });

                    DateTime dt1 = new DateTime();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {

                                userID = reader["userID"] == DBNull.Value ? 0 : reader.GetInt32("userID"),
                                userName = reader["name"] == DBNull.Value ? string.Empty : reader.GetString("name"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Comment = reader["comment"] == DBNull.Value ? string.Empty : reader.GetString("comment"),
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// api lưu dữ liệu kết quả failed cho  judging.razor .
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/UploadResultFailed")]
        public bool UploadResultFailed(string applied,int userID,string comment,int Status)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Judging_saveResultforFailed", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userID",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@status",
                            DbType = DbType.Int32,
                            Value = Status,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@applied",
                            DbType = DbType.String,
                            Value = applied,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@comment",
                            DbType = DbType.String,
                            Value = comment,
                            Direction = ParameterDirection.Input,
                        });

                        var result = cmd.ExecuteNonQuery();
                        return result > 0 ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally { conn.Close(); }

            }



        }


        /// <summary>
        /// api lưu dữ liệu kết quả passed cho  judging.razor .
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/UploadResultPassed")]
        public bool UploadResultPassed( int userID, string comment,int Status)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Judging_saveResultforPassed", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userID",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@status",
                            DbType = DbType.Int32,
                            Value = Status,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@comment",
                            DbType = DbType.String,
                            Value = comment,
                            Direction = ParameterDirection.Input,
                        });

                        var result = cmd.ExecuteNonQuery();
                        return result > 0 ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally { conn.Close(); }

            }



        }



        /// <summary>
        /// api lấy dữ liệu cho selectbox filter.
        /// </summary>
        /// 
        /// <returns>danh sách dữ liệu được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/FilterSelectbox")]
        public async Task<List<typecs>> SelectboxFilter()
        {
            List<typecs> list = new List<typecs>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("languageGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new typecs()
                            {

                                Name = reader["typeName"] == DBNull.Value ? string.Empty : reader.GetString("typeName"),
                            });
                        }
                    }
                }
            }
            return list;
        }

    }
}



