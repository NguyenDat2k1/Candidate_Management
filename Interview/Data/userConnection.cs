
using Interview.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Net;
using System.Xml.Linq;


namespace Interview.Data
{
   
    
    
    public class userConnection 
    {
//===================================================================MÀN LỌC CV==========================
        public List<Users> GetUserDetails()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("locCV_userNeedLocCVGet", conn))
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
                                Birthday = reader["birthday"] == DBNull.Value ? dt1 : reader.GetDateTime("birthday"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Source = reader["source"] == DBNull.Value ? string.Empty : reader.GetString("source"),
                                Address = reader["address"] == DBNull.Value ? string.Empty : reader.GetString("address"),
                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        //chức năng order
        public List<Users> GetUserByOrder(int order)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                string orderStatus = System.String.Empty;
                if (order == 0)
                {
                    orderStatus = "locCV_userOrderDESC";
                }
                else if (order == 1) {
                    orderStatus = "locCV_userOrderASC";
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
                                Birthday = reader["birthday"] == DBNull.Value ? dt1 : reader.GetDateTime("birthday"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Source = reader["source"] == DBNull.Value ? string.Empty : reader.GetString("source"),
                                Address = reader["address"] == DBNull.Value ? string.Empty : reader.GetString("address"),
                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        //====== chức năng filter=======
        public List<Users> GetUserByFilter(string role)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("locCV_userFilter", conn))
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
                                Birthday = reader["birthday"] == DBNull.Value ? dt1 : reader.GetDateTime("birthday"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Source = reader["source"] == DBNull.Value ? string.Empty : reader.GetString("source"),
                                Address = reader["address"] == DBNull.Value ? string.Empty : reader.GetString("address"),
                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),

                            });
                        }
                    }
                }
            }
            return list;
        }
        //======== chức năng search========
        public List<Users> GetUserBySearch(string search)
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("locCV_userSearch", conn))
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
                                Birthday = reader["birthday"] == DBNull.Value ? dt1 : reader.GetDateTime("birthday"),
                                Applied = reader["applied"] == DBNull.Value ? string.Empty : reader.GetString("applied"),
                                Source = reader["source"] == DBNull.Value ? string.Empty : reader.GetString("source"),
                                Address = reader["address"] == DBNull.Value ? string.Empty : reader.GetString("address"),
                                pathCV = reader["cv"] == DBNull.Value ? string.Empty : reader.GetString("cv"),

                            });
                        }
                    }
                }
            }
            return list;
        }

        //filterCVScreen(xem màn hình cv)
        public List<typecs> filterCVScreen()
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

        public bool cvFailed(int userID, string applied)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("locCV_userFailedLocCV", conn))
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
                            ParameterName = "@applied",
                            DbType = DbType.String,
                            Value = applied,
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

        //====chức năng duyệt cv =====
        public bool cvPassed(int userID)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("locCV_userPassedLocCV", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userID",
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



        //==============================================================================================================


       
        public List<Users> getUserAll()
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
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader.GetString("email"),
                                Phone = reader["phone"] == DBNull.Value ? string.Empty : reader.GetString("phone"),
                                Role = reader["role"] == DBNull.Value ? string.Empty : reader.GetString("role"),
                                Position = reader["position"] == DBNull.Value ? string.Empty : reader.GetString("position"),
                                
                                Status = reader["status"] == DBNull.Value ? 0 : reader.GetInt32("status"),
                            });
                        }
                    }
                }
            }
            return list;
        }



        public List<typecs> GetLanguageDetails()
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
                                Type = reader["kind"] == DBNull.Value ? string.Empty : reader.GetString("kind"),
                            });
                        }
                    }
                }
            }
            return list;
        }


        //get list chuc danh
        public  List<typecs> GetRolePlayDetails()
        {
            List<typecs> list = new List<typecs>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("roleGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new typecs()
                            {
                                Name = reader["typeName"] == DBNull.Value ? string.Empty : reader.GetString("typeName"),
                                Type = reader["kind"] == DBNull.Value ? string.Empty : reader.GetString("kind"),
                            });
                        }
                    }
                }
            }
            return list;
        }

        //get list nguoi gioi thieu
        public  List<typecs> GetBigBoosterDetails()
        {
            List<typecs> list = new List<typecs>();
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("sourceGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new typecs()
                            {
                                Name = reader["typeName"] == DBNull.Value ? string.Empty : reader.GetString("typeName"),
                                Type = reader["kind"] == DBNull.Value ? string.Empty : reader.GetString("kind"),
                            });
                        }
                    }
                }
            }
            return list;
        }
        public bool saveData(string name, DateTime birthday, string email,string phone, string address,string role,string position,string source,int status,string pathCV)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("userSave", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@name",
                            DbType = DbType.String,
                            Value = name,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@birthday",
                            DbType = DbType.DateTime,
                            Value = birthday,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@email",
                            DbType = DbType.String,
                            Value = email,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@phone",
                            DbType = DbType.String,
                            Value = phone,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@address",
                            DbType = DbType.String,
                            Value = address,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@role",
                            DbType = DbType.String,
                            Value = role,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@position",
                            DbType = DbType.String,
                            Value = position,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@source",
                            DbType = DbType.String,
                            Value = source,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@status",
                            DbType = DbType.String,
                            Value = 0,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@cv",
                            DbType = DbType.String,
                            Value = pathCV,
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
        public bool updateData(int userid,string name, DateTime birthday, string email, string phone, string address, string role, string position, string source, int status, string pathCV)
        {
            using (MySqlConnection conn = mySQLSqlHelper.GetConnection())
            {

                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("updateUserSave", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userid",
                            DbType = DbType.Int32,
                            Value = userid,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@name",
                            DbType = DbType.String,
                            Value = name,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@birthday",
                            DbType = DbType.DateTime,
                            Value = birthday,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@email",
                            DbType = DbType.String,
                            Value = email,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@phone",
                            DbType = DbType.String,
                            Value = phone,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@address",
                            DbType = DbType.String,
                            Value = address,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@role",
                            DbType = DbType.String,
                            Value = role,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@position",
                            DbType = DbType.String,
                            Value = position,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@source",
                            DbType = DbType.String,
                            Value = source,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@status",
                            DbType = DbType.String,
                            Value = 0,
                            Direction = ParameterDirection.Input,
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@cv",
                            DbType = DbType.String,
                            Value = pathCV,
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
