using Interview.Models;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace Interview.Pages
{
    public partial class Contact
    {
        string notContactyet = "chưa liên hệ ";
        private List<Users>? userDetails;
        private List<typecs>? typeDetails;
        string?  statusContact,  Place;
        string cv = System.String.Empty;
        string errMsg = System.String.Empty;
        string failedValue = System.String.Empty;
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        string search = "";
        string Order = "";
        string type = "";
        //DateTime timeMeeting;
        //public DateTime to_day = DateTime.Today;
        bool add2Flg = true;
        bool failedFlg = true;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
        bool cautionFlg = true;
        bool mycheck = false;
        bool isCheckAll = false;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        bool mainFlg = true;
        private ElementReference buttonRef;



        /// <summary>
        /// Phương thức tích checkbox cho đối tượng.
        /// </summary>
        /// <param time=>giá trị checkbox truyền vào</param>
        ///<param user=>giá trị đối tượng truyền vào</param>
        /// <returns></returns>
        private void checking(ChangeEventArgs e, Users user)
        {
            if (filterFlg)
            {
                foreach (var ucheck in userFilters)
                {
                    if (ucheck.userID == user.userID)
                    {
                        user.isChecked = (bool)e.Value;
                        ucheck.isChecked = user.isChecked;


                    }
                }

            }
            else if (orderFlg)
            {
                foreach (var ucheck in userOrders)
                {
                    if (ucheck.userID == user.userID)
                    {
                        user.isChecked = (bool)e.Value;
                        ucheck.isChecked = user.isChecked;


                    }
                }
            }
            else if (searchFlg)
            {
                foreach (var ucheck in userSearchs)
                {
                    if (ucheck.userID == user.userID)
                    {
                        user.isChecked = (bool)e.Value;
                        ucheck.isChecked = user.isChecked;


                    }
                }
            }
            else
            {
                foreach (var ucheck in userDetails)
                {
                    if (ucheck.userID == user.userID)
                    {
                        user.isChecked = (bool)e.Value;
                        ucheck.isChecked = user.isChecked;


                    }
                }
            }

        }


       //close popup err filter
        private void Fclose()
        {
            errF_Flg = true;

        }
        //close popup err order
        private void Oclose()
        {
            errO_Flg = true;

        }
        //close popup err searching
        private void Sclose()
        {
            errS_Flg = true;

        }

        //private void fclose()
        //{
        //    failedFlg = true;

        //}
        private Users Users = new Users();

        //private void close2()
        //{
        //    add2Flg = true;

        //}
        
        
        
        //close caution popup
        private void close()
        {

            cautionFlg = true;

        }




        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {
            typeDetails = await ContactService.contactCVScreen();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserContact");


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
            userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserContact?research={transText}");
            userDetails = await ContactService.getUserDContacts();
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
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserContact?filterBy={transText}");

                userDetails = await ContactService.getUserDContacts();
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
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserContact?order={order}");
                userDetails = await ContactService.getUserDContacts();
                //return userOrders;
            }

            return userOrders;

        }
        //update COntact

        /// <summary>
        /// Phương thức update thông tin sau khi liên hệ .
        /// </summary>
        ///
        /// <returns>.</returns>
        private void updateContact(Users user)
        {
            buttonRef.FocusAsync();
            Users = user;
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                foreach (var uFor in looking)
                {

                    if (uFor.userID == Users.userID)
                    {
                        if (uFor.statusContact == "16" )
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserNotContact(uFor.userID, StatusContact);
                            Tempo.Add(uFor);
                        }
                        else if (uFor.statusContact == "15" && (uFor.Place == null || uFor.Place == ""))
                        {
                            errMsg = "Chưa có địa điểm phỏng vấn !";
                            cautionFlg = false;

                        }
                        else if (uFor.statusContact == "14" || uFor.statusContact == null)
                        {
                            errMsg = "Chưa cập nhật trạng thái liên hệ !";
                            cautionFlg = false;
                        }
                        //else if (uFor.isChecked == false )
                        //{
                        //    errMsg = "Chưa tích chọn người dùng cần cập nhật";
                        //    cautionFlg = false;
                        //}
                        else if (uFor.statusContact == "15" && uFor.Place != null && uFor.Place != "")
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(Users.Place, Users.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);

                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    looking.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            else if (orderFlg)
            {
                foreach (var uFor in userOrders)
                {


                    if (uFor.userID == Users.userID)
                    {
                        if (uFor.statusContact == "16" && uFor.Place == null)
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(uFor.Place, uFor.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);
                        }
                        else if (uFor.statusContact == "15" && uFor.Place == null)
                        {
                            errMsg = "Chưa có địa điểm phỏng vấn !";
                            cautionFlg = false;

                        }
                        else if (uFor.statusContact == "14" || uFor.statusContact == null)
                        {
                            errMsg = "Chưa cập nhật trạng thái liên hệ !";
                            cautionFlg = false;
                        }
                        //else if (uFor.isChecked == false)
                        //{
                        //    errMsg = "Chưa tích chọn người dùng cần cập nhật";
                        //    cautionFlg = false;
                        //}
                        else if (uFor.statusContact == "15" && uFor.Place != null)
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(Users.Place, Users.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);

                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    userOrders.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            else if (searchFlg)
            {
                foreach (var uFor in userSearchs)
                {

                    if (uFor.userID == Users.userID)
                    {
                        if (uFor.statusContact == "16" && uFor.Place == null)
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(uFor.Place, uFor.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);
                        }
                        else if (uFor.statusContact == "15" && uFor.Place == null)
                        {
                            errMsg = "Chưa có địa điểm phỏng vấn !";
                            cautionFlg = false;

                        }
                        else if (uFor.statusContact == "14" || uFor.statusContact == null)
                        {
                            errMsg = "Chưa cập nhật trạng thái liên hệ !";
                            cautionFlg = false;
                        }
                        //else if (uFor.isChecked == false)
                        //{
                        //    errMsg = "Chưa tích chọn người dùng cần cập nhật";
                        //    cautionFlg = false;
                        //}
                        else if (uFor.statusContact == "15" && uFor.Place != null)
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(Users.Place, Users.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);

                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            else
            {
                foreach (var uFor in userDetails)
                {


                    if (uFor.userID == Users.userID)
                    {
                        if (uFor.statusContact == "16")
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserNotContact(uFor.userID, StatusContact);
                            Tempo.Add(uFor);
                        }
                        else if (uFor.statusContact == "15" &&( uFor.Place == null||uFor.Place==""))
                        {
                            errMsg = "Chưa có địa điểm phỏng vấn !";
                            cautionFlg = false;

                        }
                        else if (uFor.statusContact == "14" || uFor.statusContact == null)
                        {
                            errMsg = "Chưa cập nhật trạng thái liên hệ !";
                            cautionFlg = false;
                        }
                        //else if (uFor.isChecked == false)
                        //{
                        //    errMsg = "Chưa tích chọn người dùng cần cập nhật";
                        //    cautionFlg = false;
                        //}
                        else if (uFor.statusContact == "15" && uFor.Place != null && uFor.Place!="")
                        {
                            int StatusContact;
                            int.TryParse(uFor.statusContact, out StatusContact);
                            ContactService.SaveUserContact(Users.Place, Users.TimeMeeting, uFor.userID, StatusContact);
                            Tempo.Add(uFor);

                        }
                        
                    }

                }
                foreach (var temp in Tempo)
                {

                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            
        }




        /// <summary>
        /// Phương thức update tất cả các user được tích.
        /// </summary>
        ///
        /// <returns>.</returns>
        public void updateAll()
        {
           
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                Tempo.AddRange(looking);
                foreach(var uDetails in Tempo)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        updateContact(uDetails);
                    }
                }
            }

            if (orderFlg)
            {
                Tempo.AddRange(userOrders);
                foreach(var uDetails in Tempo)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        updateContact(uDetails);
                    }
                }
            }
            if (searchFlg)
            {
                Tempo.AddRange(userSearchs);
                foreach(var uDetails in Tempo)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        updateContact(uDetails);
                    }
                }

            }
            else
            {
                Tempo.AddRange(userDetails);
                foreach(var uDetails in Tempo)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        updateContact(uDetails);

                    }

                }

            }
           
        }





        /// <summary>
        /// Phương thức check hoặc uncheck tất cả checkbox
        /// </summary>
        ///
        /// <returns>.</returns>
        public void checkAll(List<Users> userList)
        {
            if (userList == userDetails)
            {
                if (isCheckAll)
                {
                    foreach (var uDetails in userDetails)
                    {

                        uDetails.isChecked = false;

                    }
                    isCheckAll = false;
                }
                else
                {
                    foreach (var uDetails in userDetails)
                    {

                        uDetails.isChecked = true;

                    }
                    isCheckAll = true;

                }
            }
            else if (userList == userOrders)
            {
                if (isCheckAll)
                {
                    foreach (var uDetails in userOrders)
                    {

                        uDetails.isChecked = false;

                    }
                    isCheckAll = false;
                }
                else
                {
                    foreach (var uDetails in userOrders)
                    {

                        uDetails.isChecked = true;

                    }
                    isCheckAll = true;

                }
            }
            else if (userList == userSearchs)
            {
                if (isCheckAll)
                {
                    foreach (var uDetails in userSearchs)
                    {

                        uDetails.isChecked = false;

                    }
                    isCheckAll = false;
                }
                else
                {
                    foreach (var uDetails in userSearchs)
                    {

                        uDetails.isChecked = true;

                    }
                    isCheckAll = true;

                }
            }
            else if (userList == looking)
            {
                if (isCheckAll)
                {
                    foreach (var uDetails in looking)
                    {

                        uDetails.isChecked = false;

                    }
                    isCheckAll = false;
                }
                else
                {
                    foreach (var uDetails in looking)
                    {

                        uDetails.isChecked = true;

                    }
                    isCheckAll = true;

                }
            }

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
                string transText = search;
                if (search == "C#" || search == "c#")
                {
                    transText = HttpUtility.UrlEncode(search);


                }

                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserContact?research={transText}");

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
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserContact?filterBy={transText}");

            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserContact?order={order}");

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
                    if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)))
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
