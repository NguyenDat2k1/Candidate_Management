﻿using Interview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Interview.Data
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/api/getAllOfUser")]
        public async Task<List<Users>> getUserAll()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Home_getUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


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
                            });
                        }
                    }
                }
            }
            return list;
        }
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


        [HttpGet("/api/filterUserHome")]
        public async Task<List<Users>> getFilterContact([FromQuery(Name = "filterBy")] string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Contact_userFilter", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@role",
                        DbType = DbType.String,
                        Value = role,
                        Direction = ParameterDirection.Input,
                    });


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

                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),

                            });
                        }
                    }
                }
            }
            return list;
        }

        [HttpGet("/api/orderUserHome")]
        public async Task<List<Users>> getOrderContact([FromQuery] int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "Contact_userOrderDESC";
                }
                else if (order == 1)
                {
                    orderStatus = "Contact_userOrderASC";
                }
                using (MySqlCommand cmd = new MySqlCommand(orderStatus, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


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

                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),
                            });
                        }
                    }
                }
            }
            return list;
        }

        [HttpGet("/api/searchUserHome")]
        public async Task<List<Users>> getSearchContact([FromQuery(Name = "research")] string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("Contact_userSearch", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@search",
                        DbType = DbType.String,
                        Value = search,
                        Direction = ParameterDirection.Input,
                    });

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

                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),

                            });
                        }
                    }
                }
            }
            return list;
        }
       
        [HttpPost("/api/saveContact")]
        public bool SaveUserContact(string Place, DateTime timeMeeting, int userID, int status)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Contact_userSaveContact", conn))
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
                            ParameterName = "@status",
                            DbType = DbType.Int32,
                            Value = status,
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
    }



}

