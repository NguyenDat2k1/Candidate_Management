using Interview.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Web;

namespace Interview.Pages
{
    public partial class FilterCV
    {

        string alertInput = "Cần viết lý do loại để lưu!";
        private List<Users>? userDetails;
        private List<typecs>? typeDetails;
        string text;
        string Order = "";
        string type = "";
        string search = "";
        string cv = System.String.Empty;
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        bool add1Flg = true;
        bool add2Flg = true;
        bool failedFlg = true;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
        bool mainFlg = true;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        bool haveNotFillYet = true;
        string dateString ="";
        bool filterScreen = false;
        private void close()
        {
            add1Flg = true;

        }
        private void fclose()
        {
            failedFlg = true;
            haveNotFillYet = true;
        }
        private Users Users = new Users();
        private ElementReference buttonRef;
       

        /// <summary>
        /// Phương thức mở popup applied.
        /// </summary>
        /// 
        /// <returns></returns>
        private void appliedDetail(Users user)
        {
            add1Flg = false;

            Users = user;
            if (Users.Applied == string.Empty)
            {
                Users.Applied = "Ứng viên này mới ứng tuyển lần đầu nên chưa có lý do loại cho lần ứng tuyển trước!";
            }
        }
        private void close2()
        {
            add2Flg = true;

        }
        private void Fclose()
        {
            errF_Flg = true;

        }
        private void Oclose()
        {
            errO_Flg = true;

        }
        private void Sclose()
        {
            errS_Flg = true;

        }
        private void failedCV(Users user)
        {
            failedFlg = false;
            Users = user;


        }




        public static string ConvertTimeFormat(DateTime time)
        {
            string newTimeStr = time.ToString("dd/MM/yyyy ", CultureInfo.InvariantCulture);
            return newTimeStr;
        }


        /// <summary>
        /// Phương thức xử lý cv .
        /// </summary>
        /// 
        /// <returns></returns>
        private void CVDetail(Users user)
        {
            add2Flg = false;

            Users = user;
            text = Users.pathCV.ToString();
            cv = text;


        }

        /// <summary>
        /// Phương thức loại user chưa đủ điều kiện.
        /// </summary>
        /// 
        /// <returns></returns> 
        private void handleFailedCV()
        {


            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                if (Users.failedValue == null || Users.failedValue == "")
                {
                    haveNotFillYet = false;
                    return;
                }
                else
                {
                    foreach (var uDetails in looking)
                    {
                        if (uDetails.userID == Users.userID)
                        {
                            
                            uDetails.failedValue = Users.failedValue;
                            FilterService.failedCVDetails(uDetails.userID, uDetails.failedValue);
                            Tempo.Add(uDetails);
                        }
                    }
                    foreach (var temp in Tempo)
                    {
                        // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                        looking.RemoveAll(u => u.userID == temp.userID);
                        userDetails.RemoveAll(u => u.userID == temp.userID);
                    }
                }


            }
            else if (orderFlg)
            {
                if (Users.failedValue == null || Users.failedValue == "")
                {
                    haveNotFillYet = false;
                    return;
                }
                else
                {
                    foreach (var uDetails in userOrders)
                    {
                        if (uDetails.userID == Users.userID)
                        {
                            uDetails.failedValue = Users.failedValue;
                            FilterService.failedCVDetails(uDetails.userID, uDetails.failedValue);
                            Tempo.Add(uDetails);
                        }
                    }
                    foreach (var temp in Tempo)
                    {
                        // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                        userOrders.RemoveAll(u => u.userID == temp.userID);
                        userDetails.RemoveAll(u => u.userID == temp.userID);
                    }
                }
            }
            else if (searchFlg)
            {

                if (Users.failedValue == null || Users.failedValue == "")
                {
                    haveNotFillYet = false;
                    return;
                }
                else
                {
                    foreach (var uDetails in userSearchs)
                    {
                        if (uDetails.userID == Users.userID)
                        {
                            uDetails.failedValue = Users.failedValue;
                            FilterService.failedCVDetails(uDetails.userID, uDetails.failedValue);
                            Tempo.Add(uDetails);
                        }
                    }
                    foreach (var temp in Tempo)
                    {
                        // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                        userSearchs.RemoveAll(u => u.userID == temp.userID);
                        userDetails.RemoveAll(u => u.userID == temp.userID);
                    }
                }

            }
            else
            {
                if (Users.failedValue == null || Users.failedValue == "")
                {
                    haveNotFillYet = false;
                    return;
                }
                else
                {
                    foreach (var uDetails in userDetails)
                    {
                        
                        if (uDetails.userID == Users.userID)
                        {
                            
                            uDetails.failedValue = Users.failedValue;
                            FilterService.failedCVDetails(uDetails.userID, uDetails.failedValue);
                            Tempo.Add(uDetails);
                        }
                    }
                    foreach (var temp in Tempo)
                    {
                        // Loại bỏ các phần tử giống với phần tử temp trong userFilters

                        userDetails.RemoveAll(u => u.userID == temp.userID);
                    }

                }



            }
            failedFlg = true;


        }


        /// <summary>
        /// Phương thức duyệt user đủ điều kiện.
        /// </summary>
        /// 
        /// <returns></returns>
        private async Task<List<Users>> passCV(Users user)
        {
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                foreach (var uDetails in looking)
                {
                    if (uDetails.userID == user.userID)
                    {
                        FilterService.passedCVDetails(user.userID);
                        buttonRef.FocusAsync();
                        //userDetails = await FilterService.getUserDetails();
                        Tempo.Add(uDetails);
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    looking.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }

                return looking;
            }
            else if (orderFlg)
            {
                foreach (var uDetails in userOrders)
                {
                    if (uDetails.userID == user.userID)
                    {
                        FilterService.passedCVDetails(user.userID);
                        //Filtering();
                        //userDetails = await FilterService.getUserDetails();
                        Tempo.Add(uDetails);
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userOrders.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }

                return userOrders;
            }
            if (searchFlg)
            {
                foreach (var uDetails in userSearchs)
                {
                    if (uDetails.userID == user.userID)
                    {
                        FilterService.passedCVDetails(user.userID);
                        //Filtering();
                        //userDetails = await FilterService.getUserDetails();
                        Tempo.Add(uDetails);
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }

                return userSearchs;
            }
            else
            {
                foreach (var uDetails in userDetails)
                {
                    if (uDetails.userID == user.userID)
                    {
                        FilterService.passedCVDetails(user.userID);
                        buttonRef.FocusAsync();
                        //userDetails = await FilterService.getUserDetails();
                        Tempo.Add(uDetails);
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters

                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }

                return userDetails;
            }
        }




        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {
            typeDetails = await FilterService.FilterCVScreen();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserNeedLocCV");


        }
        string temP = string.Empty;
        //string tempSearch = "";
        string tempsearch1 = "";
        string tempsearch2 = "";
        List<Users> myTempFilter = new List<Users>();
        List<Users> myTempOrder = new List<Users>();
        List<Users> myTempSearch = new List<Users>();
        bool onSearch = false;
        bool onOrder = false;
        bool onFilter = false;
        //searchlist
        //ORDER
        List<Users> myOrder = new List<Users>();
        List<Users> myOrder1 = new List<Users>();
     
        string temp1 = "";
        //string temp2 = "";


        /// <summary>
        /// Phương thức Searching,Ordering,Filtering,Schedule không dùng đến.
        /// </summary>
        ///
        /// <returns>.</returns>
        private async Task<List<Users>> Searching()
        {
            List<Users> Tempo = new List<Users>();
            mainFlg = false;
            onSearch = true;
            if ((search == null || search == "") && (type == null || type == "") && (Order == null || Order == ""))
            {
                mainFlg = true;
                userOrders.Clear();
                userSearchs.Clear();
                userFilters.Clear();
                return userDetails;
            }
            //if (search == "")
            //{
            //    if (filterFlg)
            //    {
            //        return userFilters;

            //    }
            //    else if (orderFlg)
            //    {
            //        return userOrders;

            //    }

            //}
            //if (tempsearch1!=type)
            //{
            //    foreach (var uDetails in userSearchs)
            //    {

            //        if ((uDetails.Role.Contains(type, StringComparison.OrdinalIgnoreCase)) )
            //        {
            //            Tempo.Add(uDetails);

            //        }

            //    }
            //    userFilters.Clear();
            //    userFilters.AddRange(Tempo);
            //    return userFilters;
            //}

            if (tempsearch2 == search && type == "" && Order == "")
            {
                userFilters.Clear();
                userOrders.Clear();
                return userSearchs;
            }
            if (tempsearch2 != search)
            {
                myTempSearch.Clear();
            }
            if (onFilter)
            {
                if (userFilters.Count != 0)
                {
                    userSearchs.Clear();

                    foreach (var uDetails in userFilters)
                    {
                        string SearchID = Convert.ToString(uDetails.userID);
                        if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                        {
                            Tempo.Add(uDetails);

                        }
                    }


                    myTempFilter.AddRange(userFilters);
                    userSearchs.AddRange(Tempo);
                    searchFlg = true;
                    tempsearch1 = type;
                    tempsearch2 = search;
                    userFilters.Clear();
                    return userSearchs;
                }
                else if (userFilters.Count == 0)
                {
                    userSearchs.Clear();


                    foreach (var uDetails in userOrders)
                    {
                        string SearchID = Convert.ToString(uDetails.userID);
                        if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                        {
                            Tempo.Add(uDetails);

                        }
                    }


                    myTempOrder.AddRange(userOrders);
                    userSearchs.AddRange(Tempo);
                    searchFlg = true;
                    tempsearch2 = search;
                    userOrders.Clear();
                    myTempSearch.AddRange(userSearchs);
                    myOrder1.AddRange(userSearchs);
                    return userSearchs;
                }
            }

            if /*(filterFlg == true || orderFlg == true)*/(onSearch)
            {

                if (filterFlg || myTempFilter.Count != 0)
                {
                    userSearchs.Clear();

                    if ((search != null || search != "") && userFilters.Count != 0)
                    {
                        foreach (var uDetails in userFilters)
                        {
                            string SearchID = Convert.ToString(uDetails.userID);
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }
                        }
                    }
                    else
                    {
                        foreach (var uDetails in myTempFilter)
                        {
                            string SearchID = Convert.ToString(uDetails.userID);
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }
                        }
                    }
                    myTempFilter.AddRange(userFilters);
                    userSearchs.AddRange(Tempo);
                    searchFlg = true;
                    tempsearch1 = type;
                    tempsearch2 = search;
                    userFilters.Clear();
                    myTempSearch.AddRange(userSearchs);
                    return userSearchs;
                }
                else if (orderFlg || myTempOrder.Count != 0)
                {
                    userSearchs.Clear();

                    if ((search != null || search != "") && userOrders.Count != 0)
                    {
                        foreach (var uDetails in userOrders)
                        {
                            string SearchID = Convert.ToString(uDetails.userID);
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }
                        }
                    }
                    else
                    {
                        foreach (var uDetails in myTempOrder)
                        {
                            string SearchID = Convert.ToString(uDetails.userID);
                            if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
                            {
                                Tempo.Add(uDetails);

                            }
                        }
                    }
                    myTempOrder.AddRange(userOrders);
                    userSearchs.AddRange(Tempo);
                    searchFlg = true;
                    tempsearch1 = type;
                    tempsearch2 = search;
                    userOrders.Clear();
                    myTempSearch.AddRange(userSearchs);
                    return userSearchs;
                }

            }
            else if (tempsearch2 != search && type == "" && Order == "")
            {
                string transText = search;
                if (search == "C#" || search == "c#")
                {
                    transText = HttpUtility.UrlEncode(search);


                }
                onSearch = true;
                searchFlg = true;
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUser?research={transText}");
                userDetails = await FilterService.getUserDetails();
                tempsearch2 = search;
                myTempSearch.AddRange(userSearchs);
            }
            return userSearchs;
        }
        //Filter
        private async Task<List<Users>> Filtering()
        {
            mainFlg = false;
            filterScreen = true;

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

            if (temP != type)
            {
                userOrders.Clear();
                //userSearchs.Clear();
            }
            if (tempsearch2 != search)
            {
                myTempSearch.Clear();
            }

            userFilters.Clear();
            filterFlg = true;
            if (userOrders.Count != 0 && Order != "")
            { /*userFilters.Clear();*/
                orderFlg = false;
                if (Order == "1")
                {
                    switch (type)
                    {
                        case "C#":
                            userFilters = userOrders.Where(u => u.Role == "C#").OrderBy(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;

                        case "Java":

                            userFilters = userOrders.Where(u => u.Role == "Java").OrderBy(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;
                        case "Python":

                            userFilters = userOrders.Where(u => u.Role == "Python").OrderBy(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;
                        case "Nodejs":

                            userFilters = userOrders.Where(u => u.Role == "Nodejs").OrderBy(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
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
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;

                        case "Java":

                            userFilters = userOrders.Where(u => u.Role == "Java").OrderByDescending(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;
                        case "Python":

                            userFilters = userOrders.Where(u => u.Role == "Python").OrderByDescending(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;
                        case "Nodejs":

                            userFilters = userOrders.Where(u => u.Role == "Nodejs").OrderByDescending(u => u.userID).ToList();
                            myOrder.AddRange(userFilters);
                            userOrders.Clear();
                            break;
                    }

                }


            }
            else if (userSearchs.Count != 0 && search != "")
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
                if (tempsearch2 == search)
                {
                    userSearchs.AddRange(myTempSearch);
                }
            }

            else if ((temP == type || temP != type) && search == "" && Order == "")
            {
                onFilter = true;
                string transText = type;
                if (type == "C#")
                {
                    transText = HttpUtility.UrlEncode(type);
                    //type = transText;

                }
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUser?filterBy={transText}");
                userDetails = await FilterService.getUserDetails();
            }
            temP = type;
            return userFilters;


        }

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
            if (tempsearch2 != search)
            {
                myTempSearch.Clear();
            }
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
            if (onFilter)
            {
                if (temp1 == type)
                {
                    userFilters.Clear();
                    userFilters.AddRange(myOrder);
                }
                if (userFilters.Count != 0 && userSearchs.Count != 0)
                {
                    userFilters.Clear();
                }
                if (userFilters.Count != 0)
                {
                    //if (temp1 != type)
                    //{
                    //    myOrder.Clear();
                    //}
                    filterFlg = false;

                    if (order == 1)
                    {
                        userFilters.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                        userOrders.Clear();
                        userOrders.AddRange(userFilters);
                        myOrder.Clear();
                        myOrder.AddRange(userOrders);
                        userFilters.Clear();
                    }
                    else if (order == 0)
                    {
                        userFilters.Sort((p1, p2) => p2.userID.CompareTo(p1.userID));
                        userOrders.Clear();
                        userOrders.AddRange(userFilters);
                        myOrder.Clear();
                        myOrder.AddRange(userOrders);
                        userFilters.Clear();
                    }


                }
                else if (userFilters.Count == 0)
                {

                    if (order == 1)
                    {
                        userOrders.Clear();
                        myOrder1.Sort((p1, p2) => p1.userID.CompareTo(p2.userID));
                        userOrders.AddRange(myOrder1);

                    }
                    else if (order == 0)
                    {
                        userOrders.Clear();
                        myOrder1.Sort((p1, p2) => p2.userID.CompareTo(p1.userID));
                        userOrders.AddRange(myOrder1);

                    }

                }
                temp1 = type;
            }

            //else
            else if ((temP == type || temP != type) && search == "" && Order != "")
            {
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUser?order={order}");
                userDetails = await FilterService.getUserDetails();
                //return userOrders;
            }

            return userOrders;

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
               
                string syntax = "/";
               
                if (search.Contains(syntax, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var uDetails in userDetails)
                    {
                        if ((ConvertTimeFormat(uDetails.Birthday).Contains(search, StringComparison.OrdinalIgnoreCase)))
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
                    looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUser?research={transText}");
                }


            }
            else if (search == "" && type != "" && Order == "")
            {
                onFilter = true;
                string transText = type;
                if (type == "C#")
                {
                    transText = HttpUtility.UrlEncode(type);
                    //type = transText;

                }
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUser?filterBy={transText}");
                
            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUser?order={order}");

                //return userOrders;
            }
            else if (search == "" && type == "" && Order == "")
            {
                lookFlg = false;
                return userDetails;

            }
            else {
                
                    foreach (var uDetails in userDetails)
                    {
                        string SearchID = Convert.ToString(uDetails.userID);
                        if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Source.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase))|| (ConvertTimeFormat(uDetails.Birthday).Contains(search, StringComparison.OrdinalIgnoreCase)))
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
