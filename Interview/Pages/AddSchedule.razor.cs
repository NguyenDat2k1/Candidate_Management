using Interview.Models;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Web;

namespace Interview.Pages
{
    public partial class AddSchedule
    {
       
        private List<Users>? userDetails, usersInSchedule;
        private List<typecs> typeDetails= new();
        string?  locationPV, tenUV;
        //private List<MailRequest>? templDetails;
        int sign;
        string search = "";
        string Order = "";
        string type = "";
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        DateTime /*timeMeeting*/ tgianPV;
        string room = "";
        bool isSending = true;
        bool scheduleFlg = false;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
       
        bool addPV_Flg = true;
        bool previewMailFlg = true;
        bool roomFlg = true;
        bool selectFlg = true;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        bool isClose_Flg = true;
        bool mainFlg = true;
        MailRequest meoriquet = new MailRequest();
        //bool scheBtnFlg = false;
        int idUV, idClone;

        //format datetime 
        //public static string ConvertTimeFormat(DateTime time)
        //{
        //    // Định dạng lại thời gian theo định dạng "dd/MM/yyyy hh:mm tt"
        //    string newTimeStr = time.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);


        //    return newTimeStr;
        //}




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

  


        /// <summary>
        /// Phương thức nhận và lưu giá trị selectbox đã được chọn .
        /// </summary>
        /// <param e=>giá trị được lấy từ selectbox.</param>
        ///
        /// <returns>.</returns>
        private List<string> Optioners = new List<string>();

        private void HandleSelectChange(ChangeEventArgs e)
        {
            Optioners.Clear();
            var options = (IEnumerable<string>)e.Value;
            foreach (var option in options)
            {
                Optioners.Add(option);
            }



        }
        // close of filter error 
        private void Fclose()
        {
            errF_Flg = true;

        }
        // close of order error 
        private void Oclose()
        {
            errO_Flg = true;

        }
        // close the popup haven't done
        private void IC_close()
        {
            isClose_Flg = true;

        }
        //close  all of popup 
        private void YsC_close()
        {
            if (filterFlg)
            {
                Optioners.Clear();
                isClose_Flg = true;
                addPV_Flg = true;


            }
            else if (orderFlg)
            {
                Optioners.Clear();
                isClose_Flg = true;
                addPV_Flg = true;

            }
            else if (searchFlg)
            {
                Optioners.Clear();
                isClose_Flg = true;
                addPV_Flg = true;


            }
            else
            {
                Optioners.Clear();
                isClose_Flg = true;
                addPV_Flg = true;
                //ReloadPage();
            }


        }
        
        //close searching error popup
        private void Lclose()
        {
            errS_Flg = true;

        }

        private Users Users = new Users();


        //close error input has lost
        private bool fclose()
        {
            if (room != "" || Optioners.Count != 0)
            {
                isClose_Flg = false;
                return addPV_Flg = false;
            }


            return addPV_Flg = true;

        }
        //close error room input
        private void Rclose()
        {

            roomFlg = true;

        }
        //close error selectbox 
        private void Sclose()
        {

            selectFlg = true;

        }
        //close schedule 
        private void unSchedule()
        {
            scheduleFlg = false;


        }
        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {

            typeDetails = await AddPVService.SelectboxFilter();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserAddPV");


        }

        //searchlist
        string temP = string.Empty;

        /// <summary>
        /// Phương thức Searching,Ordering,Filtering,Schedule không dùng đến.
        /// </summary>
        ///
        /// <returns>.</returns>
        private async Task<List<Users>> Searching()
        {

            List<Users> Tempo = new List<Users>();
            mainFlg = false;
            if ((search == null || search == "") && (type == null || type == "") && (Order == null || Order == ""))
            {
                mainFlg = true;
                userOrders.Clear();
                userSearchs.Clear();
                userFilters.Clear();
                return userDetails;
            }
            if (search == "")
            {
                if (filterFlg)
                {
                    return userFilters;

                }
                else if (orderFlg)
                {
                    return userOrders;

                }

            }
            if (filterFlg == true || orderFlg == true)
            {

                if (filterFlg)
                {
                    userSearchs.Clear();
                    if ((search != null || search != "") && userFilters.Count != 0)
                    {
                        foreach (var uDetails in userFilters)
                        {
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }

                        }
                        userSearchs.AddRange(Tempo);
                        searchFlg = true;

                        userFilters.Clear();
                        return userSearchs;
                    }
                }
                else if (orderFlg)
                {
                    userSearchs.Clear();
                    if ((search != null || search != "") && userOrders.Count != 0)
                    {
                        foreach (var uDetails in userOrders)
                        {
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }

                        }
                        userSearchs.AddRange(Tempo);
                        searchFlg = true;
                        userOrders.Clear();
                        return userSearchs;
                    }
                }

            }

            string transText = search;
            if (search == "C#" || search == "c#")
            {
                transText = HttpUtility.UrlEncode(search);


            }
            searchFlg = true;
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserAddPV?research={transText}");
            userDetails = await AddPVService.getUserAddPV();
            return userSearchs;
        }
        //Filter
        private async Task<List<Users>> Filtering()
        {

            mainFlg = false;
            if (type == "")
            {
                if (searchFlg)
                {
                    return userSearchs;

                }
                else if (orderFlg)
                {
                    return userOrders;

                }

            }
            if (type == "" && search == "" && Order == "")
            {
                mainFlg = true;
                //errF_Flg = false;
                userOrders.Clear();
                userSearchs.Clear();
                userFilters.Clear();
                return userDetails;
            }
            //search = "";
            if (temP != type)
            {
                userOrders.Clear();
                //userSearchs.Clear();
            }
            temP = type;
            userFilters.Clear();
            filterFlg = true;
            if (userOrders.Count != 0)
            { /*userFilters.Clear();*/
                orderFlg = false;
                if (Order == "1")
                {
                    switch (type)
                    {
                        case "C#":
                            userFilters = userOrders.Where(u => u.Role == "C#").OrderBy(u => u.userID).ToList();
                            userOrders.Clear();
                            break;

                        case "Java":

                            userFilters = userOrders.Where(u => u.Role == "Java").OrderBy(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                        case "Python":

                            userFilters = userOrders.Where(u => u.Role == "Python").OrderBy(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                        case "Nodejs":

                            userFilters = userOrders.Where(u => u.Role == "Nodejs").OrderBy(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                    }

                }
                else if (Order == "0")
                {
                    switch (type)
                    {
                        case "C#":
                            userFilters = userOrders.Where(u => u.Role == "C#").OrderByDescending(u => u.userID).ToList();
                            userOrders.Clear();
                            break;

                        case "Java":

                            userFilters = userOrders.Where(u => u.Role == "Java").OrderByDescending(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                        case "Python":

                            userFilters = userOrders.Where(u => u.Role == "Python").OrderByDescending(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                        case "Nodejs":

                            userFilters = userOrders.Where(u => u.Role == "Nodejs").OrderByDescending(u => u.userID).ToList();
                            userOrders.Clear();
                            break;
                    }

                }


            }
            else if (userSearchs.Count != 0)
            {

                switch (type)
                {
                    case "C#":
                        userFilters = userSearchs.Where(u => u.Role == "C#").OrderByDescending(u => u.userID).ToList();
                        userSearchs.Clear();
                        break;

                    case "Java":

                        userFilters = userSearchs.Where(u => u.Role == "Java").OrderByDescending(u => u.userID).ToList();
                        userSearchs.Clear();
                        break;
                    case "Python":

                        userFilters = userSearchs.Where(u => u.Role == "Python").OrderByDescending(u => u.userID).ToList();
                        userSearchs.Clear();
                        break;
                    case "Nodejs":

                        userFilters = userSearchs.Where(u => u.Role == "Nodejs").OrderByDescending(u => u.userID).ToList();
                        userSearchs.Clear();
                        break;

                }
            }

            else
            {
                string transText = type;
                if (type == "C#")
                {
                    transText = HttpUtility.UrlEncode(type);
                    //type = transText;

                }
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserAddPV?filterBy={transText}");
                userDetails = await AddPVService.getUserAddPV();
            }
            return userFilters;


        }

        //ORDER
        private async Task<List<Users>> Ordering()
        {
            mainFlg = false;
            if (type == "" && search == "" && Order == "")
            {
                //errO_Flg = false;
                userOrders.Clear();
                userSearchs.Clear();
                userFilters.Clear();
                return userDetails;
            }
            //search = "";
            //userOrders.Clear();
            if (Order == "")
            {

                orderFlg = true;
                if (filterFlg)
                {


                    userFilters.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                    userOrders.AddRange(userFilters);
                    //return userOrders;
                    userFilters.Clear();

                }
                else if (searchFlg)
                {

                    userSearchs.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                    userOrders.AddRange(userSearchs);
                    //return userOrders;
                    userSearchs.Clear();

                }
                return userOrders;
            }
            orderFlg = true;
            int order = int.Parse(Order);
            if (userFilters.Count != 0)
            { /*userFilters.Clear();*/
                filterFlg = false;
                if (order == 1)
                {
                    userFilters.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                    userOrders.AddRange(userFilters);
                    //return userOrders;
                    userFilters.Clear();
                }
                else if (order == 0)
                {
                    userFilters.Sort((p1, p2) => p2.userID.CompareTo(p1.userID));
                    userOrders.AddRange(userFilters);
                    userFilters.Clear();
                }

            }
            else if (userSearchs.Count != 0)
            { /*userSearchs.Clear();*/
                searchFlg = false;
                if (order == 1)
                {
                    userSearchs.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                    userOrders.AddRange(userSearchs);
                    //return userOrders;
                    userSearchs.Clear();
                }
                else if (order == 0)
                {
                    userSearchs.Sort((p1, p2) => p2.userID.CompareTo(p1.userID));
                    userOrders.AddRange(userSearchs);
                    //return userOrders;
                    userSearchs.Clear();
                }
            }
            else
            {
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserAddPV?order={order}");
                userDetails = await AddPVService.getUserAddPV();
                //return userOrders;
            }

            return userOrders;

        }
        private async Task<List<Users>> Schedule()
        {
            if (userFilters != null)
            { userFilters.Clear(); }
            else if (userSearchs != null)
            { userSearchs.Clear(); }

            if (userOrders != null)
            { userOrders.Clear(); }
            scheduleFlg = true;

            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            usersInSchedule = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserInSchedule");
            return usersInSchedule;
        }





        /// <summary>
        /// Phương thức lấy giá trị của đối tượng user và mở popup thêm lịch.
        /// </summary>
        /// <param User>người dùng được truyền vào </param>
        /// 
        /// <returns></returns>
        private void AddInfor(Users user)
        {
            Optioners.Clear();
            Users = user;
            addPV_Flg = false;
            tenUV = Users.userName;
            idUV = Users.userID;
            idClone = Users.userID;
            tgianPV = Users.TimeMeeting;
            locationPV = Users.Place;
            sign = Users.userID;
            room = "";
        }

        //get list schedule

        /// <summary>
        /// Phương thức xử lý thêm lịch phỏng vấn sau khi ấn lưu.
        /// </summary>
        /// 
        /// <returns></returns>
        private async Task handleAdd_Schedule()
        {
            isSending = false;
            List<Users> Tempo = new List<Users>();
            Users.SelectedOption = Optioners;
            if (lookFlg)
            {
                foreach (var curentUV in looking)
                {
                    if (sign == curentUV.userID)
                    {

                        if (Users.SelectedOption.Count == 0)
                        {
                            selectFlg = false;
                            isSending = true;
                            return;
                        }
                        foreach (var PVer in curentUV.SelectedOption = Users.SelectedOption)
                        {
                            if (room == "")
                            {
                                isSending = true;
                                roomFlg = false;
                                return;

                            }
                            curentUV.Place = locationPV;
                            curentUV.Room = room;
                            curentUV.TimeMeeting = tgianPV;

                            switch (PVer)
                            {
                                case "ntdat170401@gmail.com":
                                    curentUV.PvEr_Name = "Hai";
                                    break;
                                case "barryallen1742001@gmail.com":
                                    curentUV.PvEr_Name = "Chinh";
                                    break;
                                case "speedforceim@gmail.com":
                                    curentUV.PvEr_Name = "Xuat";
                                    break;
                            }
                            meoriquet.Subject = "đi phỏng vấn ứng viên " + curentUV.userName;
                            meoriquet.ToEmail = PVer;
                            meoriquet.Body += "Địa chỉ " + curentUV.Place + "   ,Phòng họp : " + curentUV.Room + "   ,vào lúc  " + curentUV.TimeMeeting;
                            await AddPVService.SendMail(meoriquet);
                            AddPVService.AddFor_Interviewer(PVer, curentUV.userID, curentUV.PvEr_Name);
                            meoriquet.Body = string.Empty;
                        }
                        AddPVService.SendSusscessful(curentUV.Place, curentUV.TimeMeeting, curentUV.userID, curentUV.Room);
                        Tempo.Add(curentUV);
                    }


                }

                foreach (var temp in Tempo)
                {
                    looking.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }



            }
            else if (searchFlg)
            {
                foreach (var curentUV in userSearchs)
                {
                    if (sign == curentUV.userID)
                    {

                        if (Users.SelectedOption.Count == 0)
                        {
                            selectFlg = false;
                            return;
                        }
                        foreach (var PVer in curentUV.SelectedOption = Users.SelectedOption)
                        {
                            if (room == "")
                            {
                                roomFlg = false;
                                return;

                            }
                            curentUV.Place = locationPV;
                            curentUV.Room = room;
                            curentUV.TimeMeeting = tgianPV;

                            switch (PVer)
                            {
                                case "ntdat170401@gmail.com":
                                    curentUV.PvEr_Name = "Hai";
                                    break;
                                case "barryallen1742001@gmail.com":
                                    curentUV.PvEr_Name = "Chinh";
                                    break;
                                case "speedforceim@gmail.com":
                                    curentUV.PvEr_Name = "Xuat";
                                    break;
                            }
                            meoriquet.Subject = "đi phỏng vấn ứng viên " + curentUV.userName;
                            meoriquet.ToEmail = PVer;
                            meoriquet.Body += "Địa chỉ " + curentUV.Place + "   ,Phòng họp : " + curentUV.Room + "   ,vào lúc  " + curentUV.TimeMeeting;
                            await AddPVService.SendMail(meoriquet);
                            AddPVService.AddFor_Interviewer(PVer, curentUV.userID, curentUV.PvEr_Name);
                            meoriquet.Body = string.Empty;
                        }
                        AddPVService.SendSusscessful(curentUV.Place, curentUV.TimeMeeting, curentUV.userID, curentUV.Room);
                        Tempo.Add(curentUV);
                    }


                }

                foreach (var temp in Tempo)
                {

                    userSearchs.RemoveAll(u => u.userID == temp.userID);
                    //userOrders.RemoveAll(u => u.userID == temp.userID);
                    //userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }



            }
            else if (orderFlg)
            {
                foreach (var curentUV in userOrders)
                {
                    if (sign == curentUV.userID)
                    {

                        if (Users.SelectedOption.Count == 0)
                        {
                            selectFlg = false;
                            return;
                        }
                        foreach (var PVer in curentUV.SelectedOption = Users.SelectedOption)
                        {
                            if (room == "")
                            {
                                roomFlg = false;
                                return;

                            }
                            curentUV.Place = locationPV;
                            curentUV.Room = room;
                            curentUV.TimeMeeting = tgianPV;

                            switch (PVer)
                            {
                                case "ntdat170401@gmail.com":
                                    curentUV.PvEr_Name = "Hai";
                                    break;
                                case "barryallen1742001@gmail.com":
                                    curentUV.PvEr_Name = "Chinh";
                                    break;
                                case "speedforceim@gmail.com":
                                    curentUV.PvEr_Name = "Xuat";
                                    break;
                            }
                            meoriquet.Subject = "đi phỏng vấn ứng viên " + curentUV.userName;
                            meoriquet.ToEmail = PVer;
                            meoriquet.Body += "Địa chỉ " + curentUV.Place + "   ,Phòng họp : " + curentUV.Room + "   ,vào lúc  " + curentUV.TimeMeeting;
                            await AddPVService.SendMail(meoriquet);
                            AddPVService.AddFor_Interviewer(PVer, curentUV.userID, curentUV.PvEr_Name);
                            meoriquet.Body = string.Empty;
                        }
                        AddPVService.SendSusscessful(curentUV.Place, curentUV.TimeMeeting, curentUV.userID, curentUV.Room);
                        Tempo.Add(curentUV);
                    }


                }

                foreach (var temp in Tempo)
                {


                    userOrders.RemoveAll(u => u.userID == temp.userID);

                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }



            }
            else
            {
                foreach (var curentUV in userDetails)
                {
                    if (sign == curentUV.userID)
                    {
                        if (Users.SelectedOption.Count == 0)
                        {
                            selectFlg = false;
                            isSending = true;
                            return;
                        }
                        foreach (var PVer in curentUV.SelectedOption = Users.SelectedOption)
                        {
                            if (room == "")
                            {
                                isSending = true;
                                roomFlg = false;
                                return;

                            }


                            curentUV.Place = locationPV;
                            curentUV.Room = room;
                            curentUV.TimeMeeting = tgianPV;

                            switch (PVer)
                            {
                                case "ntdat170401@gmail.com":
                                    curentUV.PvEr_Name = "Hai";
                                    break;
                                case "barryallen1742001@gmail.com":
                                    curentUV.PvEr_Name = "Chinh";
                                    break;
                                case "speedforceim@gmail.com":
                                    curentUV.PvEr_Name = "Xuat";
                                    break;
                            }
                            meoriquet.Subject = "đi phỏng vấn ứng viên " + curentUV.userName;
                            meoriquet.ToEmail = PVer;
                            meoriquet.Body += "Địa chỉ " + curentUV.Place + "   ,Phòng họp : " + curentUV.Room + "   ,vào lúc  " + curentUV.TimeMeeting;
                            await AddPVService.SendMail(meoriquet);
                            AddPVService.AddFor_Interviewer(PVer, curentUV.userID, curentUV.PvEr_Name);
                            meoriquet.Body = string.Empty;
                        }
                        AddPVService.SendSusscessful(curentUV.Place, curentUV.TimeMeeting, curentUV.userID, curentUV.Room);
                        Tempo.Add(curentUV);
                    }


                }

                foreach (var temp in Tempo)
                {

                    //userFilters.RemoveAll(u => u.userID == temp.userID);
                    //userOrders.RemoveAll(u => u.userID == temp.userID);
                    //userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }


            }
            addPV_Flg = true;
            isSending = true;


        }
        List<Users> looking = new List<Users>();
        bool lookFlg = false;



        /// <summary>
        /// Phương thức tìm kiếm đa điều kiện.
        /// </summary>
        /// 
        /// <returns>danh sách user được tìm theo điều kiện đưa vào .</returns>
        private async Task<List<Users>> Looking()
        {
            List<Users> Tempo = new List<Users>();
            lookFlg = true;
            looking.Clear();

            if (search != "" && type == "" && Order == "")
            {
                string sa = "Sáng";
                string ch = "Chiều";
                string syntax = "/";
                string syntax1 = ":";
                if ((sa.Contains(search, StringComparison.OrdinalIgnoreCase)) || (ch.Contains(search, StringComparison.OrdinalIgnoreCase)) || (search.Contains(syntax, StringComparison.OrdinalIgnoreCase)) || (search.Contains(syntax1, StringComparison.OrdinalIgnoreCase)))
                {
                    foreach (var uDetails in userDetails)
                    {
                        if ((ConvertTimeFormat(uDetails.TimeMeeting).Contains(search, StringComparison.OrdinalIgnoreCase)))
                        {
                            Tempo.Add(uDetails);

                        }
                    }
                    looking.AddRange(Tempo);
                }
                else
                {
                    string transText = search;
                    if (search == "C#" || search == "c#")
                    {
                        transText = HttpUtility.UrlEncode(search);


                    }

                    var client = httpClient.CreateClient();
                    client.BaseAddress = new Uri("https://localhost:7142");
                    looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserAddPV?research={transText}");
                }


            }
            else if (search == "" && type != "" && Order == "")
            {
                
                string transText = type;
                if (type == "C#")
                {
                    transText = HttpUtility.UrlEncode(type);
                    //type = transText;

                }
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserAddPV?filterBy={transText}");

            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserAddPV?order={order}");

                //return userOrders;
            }
            else if (search == "" && type == "" && Order == "")
            {
                lookFlg = false;
                return userDetails;

            }
            else
            {

                foreach (var uDetails in userDetails)
                {
                    string SearchID = Convert.ToString(uDetails.userID);
                    if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Place.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase))|| (ConvertTimeFormat(uDetails.TimeMeeting).Contains(search, StringComparison.OrdinalIgnoreCase)))
                    {
                        Tempo.Add(uDetails);

                    }
                }
                looking.AddRange(Tempo);


                if (type != "")
                {
                    
                        switch (type)
                        {
                            case "C#":
                                looking = looking.Where(u => u.Role == "C#").OrderBy(u => u.userID).ToList();

                                break;

                            case "Java":

                                looking = looking.Where(u => u.Role == "Java").OrderBy(u => u.userID).ToList();

                                break;
                            case "Python":

                                looking = looking.Where(u => u.Role == "Python").OrderBy(u => u.userID).ToList();

                                break;
                            case "Nodejs":

                                looking = looking.Where(u => u.Role == "Nodejs").OrderBy(u => u.userID).ToList();

                                break;
                        }

                }

                if (Order == "1")
                {

                    looking.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));


                }
                else if (Order == "0")
                {

                    looking.Sort((p1, p2) => p2.userID.CompareTo(p1.userID));


                }



            }

            return looking;
        }
    }
}
