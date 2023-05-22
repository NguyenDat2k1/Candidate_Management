using Interview.Models;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Web;

namespace Interview.Pages
{
    public partial class Judged
    {

        private List<Users>? userDetails /*usersInSchedule*/;
        private List<typecs>? typeDetails;
        string? Reason;
       
        int sign;
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        DateTime timeMeeting, tgianPV;
        private bool isChecked1 = true;
        private bool isChecked2 = false;
        bool add2Flg = true;
        bool isJudge;
        bool scheduleFlg = false;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
        bool cautionFlg = true;
        bool mycheck = false;
        bool judgingFlg = true;
        bool addPV_Flg = true;
        bool previewMailFlg = true;
        bool judgedFlg = false;
        MailRequest meoriquet = new MailRequest();
        bool scheBtnFlg = false;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        int statusTemp = 0;
        string Comment = "";
        string search = "";
        string Order = "";
        string type = "";
        string isjudge = "đánh giá nào";
        bool mainFlg = true;


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
        private List<string> Optioners = new List<string>();



        /// <summary>
        /// Phương thức nhận và lưu giá trị selectbox đã được chọn .
        /// </summary>
        /// <param e=>giá trị được lấy từ selectbox.</param>
        ///
        /// <returns>.</returns>
        private void HandleSelectChange(ChangeEventArgs e)
        {
            Optioners.Clear();
            var options = (IEnumerable<string>)e.Value;
            foreach (var option in options)
            {
                Optioners.Add(option);
            }
        }


        private Users Users = new Users();
        private void Lclose()
        {
            errS_Flg = true;

        }
        private void Fclose()
        {
            errF_Flg = true;

        }
        private void Sclose()
        {
            errS_Flg = true;

        }
        private void Oclose()
        {
            errO_Flg = true;

        }
        private void judgingClose()
        {
            Reason = "";
            Comment = "";
            judgingFlg = true;

        }
        private void judgedClose()
        {

            judgedFlg = true;

        }
        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {

            typeDetails = await JudgeService.SelectboxFilter();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserIsJudge");


        }

        //searchlist
        string temP = string.Empty;




        /// <summary>
        /// Phương thức Searching,Ordering,Filtering,Schedule không dùng đến.
        /// </summary>
        ///
        /// <returns>.</returns>
        //searchlist
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
            userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserIsJudge?research={transText}");
            userDetails = await JudgeService.getUserIsJudged();
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
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserIsJudge?filterBy={transText}");
                userDetails = await JudgeService.getUserIsJudged();
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
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserIsJudge?order={order}");
                userDetails = await JudgeService.getUserIsJudged();
                //return userOrders;
            }

            return userOrders;

        }
        //update tong
        private async Task Update()
        {
            userDetails = await JudgeService.getUserNeedToJudge();

        }



        private void Judge(Users user)
        {
            isChecked1 = true; isChecked2 = false;
            judgingFlg = false;
            Users = user;
            Reason = Users.Applied;
            Comment = Users.Comment;
            statusTemp = Users.Status;


         }

            /// <summary>
            /// Phương thức mở popup xem đánh giá
            /// </summary>
            /// 
            /// <returns>d .</returns>
            private void viewJudge()
        {
            judgingFlg = true;

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
                    looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserIsJudge?research={transText}");
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
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserIsJudge?filterBy={transText}");

            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserIsJudge?order={order}");

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
                    if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Place.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)) || (ConvertTimeFormat(uDetails.TimeMeeting).Contains(search, StringComparison.OrdinalIgnoreCase)))
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
