using Interview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Interview.Data
{
    [ApiController]
    public class userDetailSerivce : ControllerBase
    {
        userConnection objUsers = new userConnection();


        /// <summary>
        /// api lấy dư liệu cho selectbox language của  import.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/languages")]
        public async Task<List<typecs>> getLanguageDetails()
        {
            List<typecs> usersObjs;
            usersObjs = objUsers.GetLanguageDetails();
            return usersObjs;
        }

       


        /// <summary>
        /// api lấy dư liệu cho selectbox role của  import.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/role")]
        public async Task<List<typecs>> getRoleDetails()
        {
             List<typecs> usersObjs;
            usersObjs = objUsers.GetRolePlayDetails();
            return usersObjs;
        }

        /// <summary>
        /// api lấy dư liệu cho selectbox supporter của  import.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/supporter")]
        public async Task<List<typecs>> getSugDetails()
        {
            List<typecs> usersObjs;
            usersObjs = objUsers.GetBigBoosterDetails();
            return usersObjs;
        }

        /// <summary>
        /// api lưu dư liệu cho  import.razor.
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("/api/save")]
        public bool SaveUserDetails(string name, DateTime birthday, string email, string phone, string address, string role, string postion, string source,int status,string pathCV)
        {

            var result = objUsers.saveData(name, birthday, email, phone,address, role,postion,source,status,pathCV);
            return result;
        }

        /// <summary>
        /// api lưu dư liệu cho người cũ của import.razor.
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("/api/Update")]
        public bool UpdateUserDetails(int userid,string name, DateTime birthday, string email, string phone, string address, string role, string postion, string source, int status, string pathCV)
        {

            var result = objUsers.updateData(userid,name, birthday, email, phone, address, role, postion, source, status, pathCV);
            return result;
        }


        //========Màn lọc CV==========
        /// <summary>
        /// api lấy list dư liệu cho  filter.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào.</returns>
        [HttpGet("/api/getAllUserNeedLocCV")]
        public async Task<List<Users>> getUserDetails()
        {
            List<Users> usersObjs;
            usersObjs = objUsers.GetUserDetails();
            return usersObjs;
        }

        /// <summary>
        /// api lưu kết quả failedcv filter.razor.
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpPost("/api/updateFailedCV")]
        public bool failedCVDetails(int userID,string applied)
        {

            var result = objUsers.cvFailed(userID,applied);
            return result;
        }

        /// <summary>
        /// api lưu kết quả passcv filter.razor.
        /// </summary>
        /// 
        /// <returns>.</returns>
        [HttpGet("/api/updatePassedCV")]
        public bool passedCVDetails(int userID)
        {

            var result = objUsers.cvPassed(userID);
            return result;
        }


        /// <summary>
        /// api lấy list dư liệu bằng filter cho  index.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào.</returns>
        [HttpGet("/api/filterUser")]
        public async Task<List<Users>> getUserByFilter([FromQuery(Name = "filterBy")]string role)
        {
            List<Users> usersObjs;
            usersObjs = objUsers.GetUserByFilter( role);
            return usersObjs;
        }
        /// <summary>
        /// api lấy list dư liệu bằng order cho  index.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào.</returns>
        [HttpGet("/api/orderUser")]
        public async Task<List<Users>> getUserByOrder([FromQuery] int order)
        {
            List<Users> usersObjs;
            usersObjs = objUsers.GetUserByOrder(order);
            return usersObjs;
        }
        /// <summary>
        /// api lấy list dư liệu bằng search cho  index.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào.</returns>
        [HttpGet("/api/searchUser")]
        public async Task<List<Users>> getUserBySearch([FromQuery(Name = "research")] string search)
        {
            List<Users> usersObjs;
            usersObjs = objUsers.GetUserBySearch(search);
            return usersObjs;
        }
        /// <summary>
        /// api lấy dư liệu cho selectbox language của  import.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        [HttpGet("/api/filterScreen")]
        public async Task<List<typecs>> FilterCVScreen()
        {
            List<typecs> usersObjs;
            usersObjs = objUsers.filterCVScreen();
            return usersObjs;
        }



        //=============================MÀN index (unleash) ==========================================
        /// <summary>
        /// api lấy list dư liệu cho  index.razor.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào.</returns>
        [HttpGet("/api/getAllUserOfWeb")]
        public async Task<List<Users>> getUserAll()
        {
            List<Users> usersObjs;
            usersObjs = objUsers.getUserAll();
            return usersObjs;
        }









        //[HttpGet("/api/getAllUserContact")]
        //public async Task<List<Users>> getUserDContacts()
        //{
        //    List<Users> usersObjs;
        //    usersObjs = objUsers.GetUserContacts();
        //    return usersObjs;
        //}



        //[HttpGet("/api/filterUserContact")]
        //public async Task<List<Users>> getFilterContact([FromQuery(Name = "filterBy")] string role)
        //{
        //    List<Users> usersObjs;
        //    usersObjs = objUsers.GetFilterContact(role);
        //    return usersObjs;
        //}

        //[HttpGet("/api/orderUserContact")]
        //public async Task<List<Users>> getOrderContact([FromQuery] int order)
        //{
        //    List<Users> usersObjs;
        //    usersObjs = objUsers.GetOrderContact(order);
        //    return usersObjs;
        //}

        //[HttpGet("/api/searchUserContact")]
        //public async Task<List<Users>> getSearchContact([FromQuery(Name = "research")] string search)
        //{
        //    List<Users> usersObjs;
        //    usersObjs = objUsers.GetSearchContact(search);
        //    return usersObjs;
        //}
        //[HttpGet("/api/ContactCVScreen")]
        //public async Task<List<typecs>> contactCVScreen()
        //{
        //    List<typecs> usersObjs;
        //    usersObjs = objUsers.ContactCVScreen();
        //    return usersObjs;
        //}
        //[HttpPost("/api/saveContact")]
        //public bool SaveUserContact(string Place, DateTime timeMeeting, int userID,int status)
        //{

        //    var result = objUsers.saveDataContact(Place,timeMeeting,userID,status);
        //    return result;
        //}
    }
}