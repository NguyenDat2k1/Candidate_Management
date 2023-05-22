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
using System.Collections.Generic;

namespace Interview.Data
{
    [ApiController]
    public class AddPVController : ControllerBase
    {

        /// <summary>
        /// api get list màn add phỏng vấn .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getAllUserAddPV")]
        public async Task<List<Users>> getUserAddPV()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddPV_userAddInforMain", conn))
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
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                            });
                        }
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// api get list màn lịch phỏng vấn .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getAllUserInSchedule")]
        public async Task<List<Users>> getUserInSchedule()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddPV_userInScheduleMain", conn))
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
                                

                               
                                TimeMeeting = reader["timeMeeting"] == DBNull.Value ? dt1 : reader.GetDateTime("timeMeeting"),
                                Place = reader["location"] == DBNull.Value ? string.Empty : reader.GetString("location"),
                                Room = reader["room"] == DBNull.Value ? string.Empty : reader.GetString("room"),
                            });
                        }
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// api get list user = filter .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/filterUserAddPV")]
        public async Task<List<Users>> getFilterAddPV([FromQuery(Name = "filterBy")] string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddPV_userFilter", conn))
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
        /// api get list user = order .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/orderUserAddPV")]
        public async Task<List<Users>> getOrderAddPV([FromQuery] int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "AddPV_userOrderDESC";
                }
                else if (order == 1)
                {
                    orderStatus = "AddPV_userOrderASC";
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
        /// api get list user = search .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/searchUserAddPV")]
        public async Task<List<Users>> getSearchAddPV([FromQuery(Name = "research")] string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddPV_userSearch", conn))
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
        /// api get list option  cho chức năng filter .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
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
        /// api get list option  cho chức năng filter .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        //[HttpGet("/api/FilterSelectbox")]
        //public async Task<List<MailRequest>> ListTemplate()
        //{
        //    List<MailRequest> list = new List<MailRequest>();
        //    using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
        //    {
        //        conn.Open();
        //        using (MySqlCommand cmd = new MySqlCommand("SendMail_templateGet", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(new MailRequest()
        //                    {

        //                        pathMail = reader["template"] == DBNull.Value ? string.Empty : reader.GetString("template"),
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return list;
        //}


        /// <summary>
        /// tạo controller để xử lý gửi mail .
        /// </summary>
        /// 
        /// <returns>.</returns>
        private readonly IMailService mailService;
        public AddPVController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        /// <summary>
        /// api xử lý gửi mail .
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/sendToPVer")]
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
        /// api lưu dư liệu .
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/wasSentToUpdate")]
        public bool SendSusscessful(string Place, DateTime timeMeeting, int userID, string room)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("AddPV_updateMeeting", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@uvID",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@room",
                            DbType = DbType.String,
                            Value = room,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@location",
                            DbType = DbType.String,
                            Value = Place,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@timeMeeting",
                            DbType = DbType.DateTime,
                            Value = timeMeeting,
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
        /// api cập nhật dữ liệu.
        /// </summary>
        /// 
        /// <returns>.</returns>

        [HttpPost("api/wasSendingToUpdate")]
        public bool SendToUpdate(string Place, DateTime timeMeeting, int userID, string room)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("AddPV_updateIn4_meet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@uvID",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@room",
                            DbType = DbType.String,
                            Value = room,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@location",
                            DbType = DbType.String,
                            Value = Place,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@timeMeeting",
                            DbType = DbType.DateTime,
                            Value = timeMeeting,
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
        /// api xoá dữ liệu dư liệu .
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("api/wasSentToDelete")]
        public bool Delete_Interview(string Place, DateTime timeMeeting, int userID, string room)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("AddPV_Delete_InterView", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@uv_code",
                            DbType = DbType.Int32,
                            Value = userID,
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
        /// api lưu dư liệu cho người phognr vấn.
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost("api/wasSentToUpdatePvEr")]
        public bool AddFor_Interviewer(string email_InterViewer, int userID,string name_InterViewer)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("AddPV_Interviewer_Save", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@mailPvEr",
                            DbType = DbType.String,
                            Value = email_InterViewer,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@uv_code",
                            DbType = DbType.Int32,
                            Value = userID,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@namePvEr",
                            DbType = DbType.String,
                            Value = name_InterViewer,
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
        /// api lưu dư liệu của người pv .
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/getPvEr")]
        public async Task<List<Users>> GetPvErbyID([FromQuery(Name = "filterBy")] int uv_code)
       
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddPV_getInfor_PvEr", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@uv_code",
                        DbType = DbType.Int32,
                        Value = uv_code,
                        Direction = ParameterDirection.Input,
                    });
                    //DateTime dt1 = new DateTime();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Users()
                            {
                                PvEr_Name = reader["PvName"] == DBNull.Value ? string.Empty : reader.GetString("PvName"),
                                
                                 PvEr_Mail = reader["mailPvEr"] == DBNull.Value ? string.Empty : reader.GetString("mailPvEr"),

                            });
                        }
                    }
                }
            }
            return list;
        }
       
    }
}




