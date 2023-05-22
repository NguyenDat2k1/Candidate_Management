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
    public class SendMailController : ControllerBase
    {

        /// <summary>
        /// api lấy list usser cho  sendmail.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getAllUserSendMail")]
        public async Task<List<Users>> getUserSendMail()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SendMail_userSendMailMain", conn))
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
                                typeMail = reader["typeMail"] == DBNull.Value ? 0 : reader.GetInt32("typeMail"),
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
        /// api lấy list usser cho  sendmail.razor bawngf filter.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/filterUserSendMail")]
        public async Task<List<Users>> getFilterSendMail([FromQuery(Name = "filterBy")] string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SendMail_userFilter", conn))
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
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                                typeMail = reader["typeMail"] == DBNull.Value ? 0 : reader.GetInt32("typeMail"),
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
        /// api lấy list usser cho  sendmail.razor bằng order.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/orderUserSendMail")]
        public async Task<List<Users>> getOrderSendMail([FromQuery] int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "SendMail_userOrderDESC";
                }
                else if (order == 1)
                {
                    orderStatus = "SendMail_userOrderASC";
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
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                                typeMail = reader["typeMail"] == DBNull.Value ? 0 : reader.GetInt32("typeMail"),
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
        /// api lấy list usser cho  sendmail.razor bằng search.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/searchUserSendMail")]
        public async Task<List<Users>> getSearchSendMail([FromQuery(Name = "research")] string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SendMail_userSearch", conn))
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
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                                typeMail = reader["typeMail"] == DBNull.Value ? 0 : reader.GetInt32("typeMail"),
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
        /// <summary>
        /// api lấy list mẫu mail.
        /// </summary>
        /// 
        /// <returns>danh sách dữ liệu được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/mailtemplateList")]
        public async Task<List<MailRequest>> ListTemplate()
        {
            List<MailRequest> list = new List<MailRequest>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SendMail_templateGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new MailRequest()
                            {

                                pathMail = reader["template"] == DBNull.Value ? string.Empty : reader.GetString("template"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        
        
        //tạo đối tượng mailservice
        private readonly IMailService mailService;
        public SendMailController(IMailService mailService)
        {
            this.mailService = mailService;
        }


        /// <summary>
        /// api thuwjc hien viec gui mail.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpPost("api/send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// api lưu dữ liệu sau khi gửi mail.
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/wasSent")]
        public bool SendSusscessful(int status,int userID, int typeMail)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SendMail_wasSent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@Status",
                            DbType = DbType.Int32,
                            Value = status,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userID",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@typeMail",
                            DbType = DbType.Int32,
                            Value = typeMail,
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
    }
}



