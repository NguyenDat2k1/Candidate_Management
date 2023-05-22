using Interview.Models;
using System.Globalization;

namespace Interview.Pages
{
    public partial class Schedule
    {
        //string location, name, room, optional;
        bool isClose_Flg = true;
        private List<Users>? usersInSchedule;
        private List<Users> pvERs = new();
        bool roomFlg = true;
        bool placeFlg = true;
        //bool addPV_Flg = true;
        bool isSending = true;
        string tempRoom = "";
        string tempPlace = "";
        MailRequest meoriquet = new MailRequest();
        bool detail_Flg = true;
        private List<string> Optioners = new List<string>();
        private Users Users = new Users();

        /// <summary>
        /// Phương thức ép kiểu dữ liệu datetime sang string theo format d/m/y.
        /// </summary>
        /// <param time=>giá trị datetime truyền vào</param>
        ///
        /// <returns>giá trị datetime đã được format lại theo kiểu string</returns>
        public static string ConvertTimeFormat(DateTime time)
        {
            string newTimeStr = time.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);

            if (time.Hour < 12)
            {
                newTimeStr = newTimeStr.Replace("AM", "Sáng");
            }
            else
            {
                newTimeStr = newTimeStr.Replace("PM", "Chiều");
            }

            return newTimeStr;
        }

        
        //phương thức đóng popup lỗi room
        private void Rclose()
        {

            roomFlg = true;

        }

        //phương thức đóng popup lỗi place
        private void Pclose()
        {

            placeFlg = true;

        }


        //phương thức đóng popup chi tiết lịch phỏng vấn
        private bool fclose()
        {
            if (Users.Room != tempRoom || Users.Place != tempPlace)
            {
                Users.Room = tempRoom;
                Users.Place = tempPlace;
                isClose_Flg = false;
                return detail_Flg = false;
            }


            return detail_Flg = true;


        }
        
        
        //phương thức đóng popup thông báo còn lịch đang sửa
        private void IC_close()
        {
            isClose_Flg = true;

        }

        //phương thức đóng popup chi tiết lịch đang sửa
        private void YsC_close()
        {


            isClose_Flg = true;
            detail_Flg = true;
            //ReloadPage();



        }
        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {


            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            usersInSchedule = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserInSchedule");


        }




        /// <summary>
        /// phương thức update lichj phong van
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>

        private async Task Update_Schedule()
        {
            isSending = false;
            foreach (var PVer in pvERs)
            {
                if (Users.Room == "")
                {
                    roomFlg = false;
                    return;
                }
                if (Users.Place == "")
                {
                    placeFlg = false;
                    return;
                }
                meoriquet.Subject = "Cập nhật thông tin phỏng vấn ứng viên " + Users.userName;
                meoriquet.ToEmail = PVer.PvEr_Mail;
                meoriquet.Body += "Địa điểm : " + Users.Place + "   Phòng họp : " + Users.Room + "   vào lúc  " + Users.TimeMeeting;
                await AddPVService.SendMail(meoriquet);
                AddPVService.SendToUpdate(Users.Place, Users.TimeMeeting, Users.userID, Users.Room);
                meoriquet.Body = string.Empty;
            }
            isSending = true;
            detail_Flg = true;


        }



        /// <summary>
        /// phương thức xoá lịch phỏng vấn
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        private async Task Delete_Schedule()
        {
            isSending = false;
            List<Users> Tempo = new List<Users>();
            detail_Flg = true;
            foreach (var PVer in pvERs)
            {
                meoriquet.Subject = "Hủy Lịch phỏng vấn ứng viên " + Users.userName + "    vào" + Users.TimeMeeting;
                meoriquet.ToEmail = PVer.PvEr_Mail;
                meoriquet.Body += "Địa điểm : " + Users.Place + "   Phòng họp : " + Users.Room + "   vào lúc  " + Users.TimeMeeting;
                await AddPVService.SendMail(meoriquet);
                AddPVService.Delete_Interview(Users.Place, Users.TimeMeeting, Users.userID, Users.Room);
                meoriquet.Body = string.Empty;
                Tempo.Add(PVer);
            }
            foreach (var temp in Tempo)
            {
                usersInSchedule.RemoveAll(u => u.userID == Users.userID);
            }
            isSending = true;

        }

        /// <summary>
        /// phương thức lấy list người phỏng vấn
        /// </summary>
        /// <param></param>
        /// <returns>danh sách người phỏng vấn đc lấy theo điều kiện.</returns>
        private async Task<List<Users>> getOut(Users user)
        {
            Users = user;
            detail_Flg = false;
            tempPlace = Users.Place;
            tempRoom = Users.Room;
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            pvERs = await client.GetFromJsonAsync<List<Users>>($"/api/getPvEr?filterBy={user.userID}");

            return pvERs;


        }

    }
}
