using Interview.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using System.Globalization;
using System.Web;

namespace Interview.Pages
{
    public partial class SendMail
    {
        private List<Users>? userDetails;
        private List<typecs>? typeDetails;
        string? /*text,*/  statusContact, Place, BodyPreview, mailND;
        private List<MailRequest>? templDetails;
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        string search = "";
        string Order = "";
        string type = "";
        string who = "";
        DateTime timeMeeting;
        string lblMail = "";
        bool errM_Flg = true;
        string mail = "";
        //bool add2Flg = true;
        bool isCreateMail;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
        bool cautionFlg = true;
        bool mycheck = false;
        bool isCheckAll = false;
        bool createMailFlg = true;
        bool previewMailFlg = true;
        bool errTitle_Flg = true;
        bool missingWho_Flg = true;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        //bool add2Flg = true;
        MailRequest meoriquet = new MailRequest();
        //string new_time = "";
        int uHint;
        string uHint2;
        bool mainFlg = true;
        bool isSending = true;
        


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
        //close err popup
        private void ErrMail()
        {
            errM_Flg = true;
        }
        //close err popup
        private void ErrTitle()
        {
            errTitle_Flg = true;
        }

        //close err popup
        private void Eclose()
        {
            errM_Flg = true;
        }
        //close err popup title
        private void Tclose()
        {
            errTitle_Flg = true;
        }

        //close err popup who
        private void missingWho()
        {
            missingWho_Flg = true;
        }

        //close err popup filer
        private void Fclose()
        {
            errF_Flg = true;

        }//close err popup order
        private void Oclose()
        {
            errO_Flg = true;

        }//close err popup search
        private void Sclose()
        {
            errS_Flg = true;

        }



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



        private Users Users = new Users();

        //close err popup
        //private void close2()
        //{
        //    add2Flg = true;

        //}
        //close err popup
        private void close()
        {

            cautionFlg = true;

        }//close err popup
        private void fclose()
        {

            createMailFlg = true;

        }
        //close err popup
        private void pclose()
        {

            previewMailFlg = true;

        }



        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {
            templDetails = await SendMailService.ListTemplate();
            typeDetails = await SendMailService.SelectboxFilter();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserSendMail");


        }

        //searchlist
        string temP = string.Empty;


        /// <summary>
        /// Phương thức Searching,Ordering,Filtering không dùng đến.
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
            userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserSendMail?research={transText}");
            userDetails = await SendMailService.getUserSendMail();
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
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserSendMail?filterBy={transText}");
                userDetails = await SendMailService.getUserSendMail();
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
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserSendMail?order={order}");
                userDetails = await SendMailService.getUserSendMail();
                //return userOrders;
            }

            return userOrders;

        }
        //update tong



        /// <summary>
        /// Phương thức check hoặc uncheck tất cả các checkbox.
        /// </summary>
        ///
        /// <returns>n.</returns>
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



        /// <summary>
        /// Phương thức tạo form cho email.
        /// </summary>
        ////<param name="user"></param>
        /// <returns>.</returns>
        private void createMail(Users user)
        {
            Users = user;
            if (Users.typeMail == 0)
            {
                lblMail = "Mẫu mail phỏng vấn vòng 1";

            }
            else { lblMail = "Mẫu mail phỏng vấn vòng 2"; }
            switch (user.Position)
            {
                case "Intern":
                    foreach (var mailList in templDetails)
                    {
                        string fileData = "";
                        if (mailList.pathMail == "intern.txt")
                        {
                            string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "CV",/* trustedFileNameForFileStorage + */ mailList.pathMail);
                            //string filePath = "/CV/" + mailList.pathMail;
                            try
                            {
                                // Pass the file path and file name to the StreamReader constructor
                                StreamReader sr = new StreamReader(filePath);
                                // Read the entire file contents into the fileData variable
                                fileData = sr.ReadToEnd();
                                // Close the file
                                sr.Close();
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                            finally
                            {
                                Console.WriteLine("Executing finally block.");
                            }

                        }
                        mailND = fileData;

                        if (mailND != null && mailND != string.Empty)
                        {
                            break;
                        }
                    }
                    break;
                case "fresher":
                    foreach (var mailList in templDetails)
                    {
                        string fileData = "";
                        if (mailList.pathMail == "fresher.txt")
                        {
                            string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "CV",/* trustedFileNameForFileStorage + */ mailList.pathMail);
                            //string filePath = "/CV/" + mailList.pathMail;
                            try
                            {
                                // Pass the file path and file name to the StreamReader constructor
                                StreamReader sr = new StreamReader(filePath);
                                // Read the entire file contents into the fileData variable
                                fileData = sr.ReadToEnd();
                                // Close the file
                                sr.Close();
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                            finally
                            {
                                Console.WriteLine("Executing finally block.");
                            }
                        }
                        mailND = fileData;

                        if (mailND != null && mailND != string.Empty)
                        {
                            break;
                        }
                    }

                    break;
                case "Junior":
                    foreach (var mailList in templDetails)
                    {
                        string fileData = "";
                        if (mailList.pathMail == "junior.txt")
                        {
                            string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "CV",/* trustedFileNameForFileStorage + */ mailList.pathMail);
                            //string filePath = "/CV/" + mailList.pathMail;
                            try
                            {
                                // Pass the file path and file name to the StreamReader constructor
                                StreamReader sr = new StreamReader(filePath);
                                // Read the entire file contents into the fileData variable
                                fileData = sr.ReadToEnd();
                                // Close the file
                                sr.Close();
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                            finally
                            {
                                Console.WriteLine("Executing finally block.");
                            }
                        }
                        mailND = fileData;

                        if (mailND != null && mailND != string.Empty)
                        {
                            break;
                        }
                    }
                    break;

                default:

                    break;
            }
            createMailFlg = false;

            Users = user;

            //api/SendEmailToTest
        }


        /// <summary>
        /// Phương thức cho phép xem preview mail vừa tạo và gửi mail.
        /// </summary>
        ///<param name="user"></param>
        /// <returns>.</returns>
        private void previewMail(Users user)
        {
            who = user.who;
            Users = user;
            previewMailFlg = false;
            Users.Body = user.Body;
            Users.Subject = user.Subject;

        }




        /// <summary>
        /// Phương thức cho phép gửi đồng loạt nhiều mail dựa trên những checkbox được tích.
        /// </summary>
        ///
        /// <returns>.</returns>
        private async Task SubmitAll()
        {
            isSending = false;
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                foreach (var uDetails in looking)
                {
                    if (uDetails.isChecked && uDetails.isCreateMail)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.typeMail == 0)
                                { uDetails.typeMail = 1; }
                                else if (uDetails.typeMail == 1)
                                { uDetails.typeMail = 2; }
                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                        {
                            uHint = uDetails.userID;
                            uHint2 += uHint.ToString() + ",";
                            errM_Flg = false;
                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    looking.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }

            }
            if (orderFlg)
            {
                foreach (var uDetails in userOrders)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.Subject == "Mail mời phỏng vấn vòng 1")
                                { uDetails.typeMail = 1; }
                                else if (uDetails.Subject == "Mail mời phỏng vấn vòng 2")
                                { uDetails.typeMail = 2; }
                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                        {
                            uHint = uDetails.userID;
                            uHint2 += uHint.ToString() + ",";
                            errM_Flg = false;
                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userOrders.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Ordering();
            }
            if (searchFlg)
            {
                foreach (var uDetails in userSearchs)
                {
                    if (uDetails.isChecked)
                    {
                        mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.Subject == "Mail mời phỏng vấn vòng 1")
                                { uDetails.typeMail = 1; }
                                else if (uDetails.Subject == "Mail mời phỏng vấn vòng 2")
                                { uDetails.typeMail = 2; }
                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                        {
                            uHint = uDetails.userID;
                            uHint2 += uHint.ToString() + ",";
                            errM_Flg = false;
                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userSearchs.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Searching();
            }
            else
            {
                foreach (var uDetails in userDetails)
                {
                    if (uDetails.isChecked && uDetails.isCreateMail)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.typeMail == 0)
                                { uDetails.typeMail = 1; }
                                else if (uDetails.typeMail == 1)
                                { uDetails.typeMail = 2; }
                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                        {
                            uHint = uDetails.userID;
                            uHint2 += uHint.ToString() + ",";
                            errM_Flg = false;
                        }

                    }

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userDetails.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                }
                //OnInitializedAsync();
            }

            isSending = true;



        }


        /// <summary>
        /// Phương thức cho phép gửi mail dựa trên đối tượng chứa các thành phần được truyền.
        /// </summary>
        ///<param name="Meoriquet"></param>
        /// <returns>.</returns>
        private async Task sendMail(MailRequest Meoriquet)
        {

            string body = string.Empty;
            string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "css", "index.html");
            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
                body = body.Replace("{Meoriquet.role}", Meoriquet.role);
                body = body.Replace("{Meoriquet.Name}", Meoriquet.Name);
                body = body.Replace("{Meoriquet.flexibleBody}", Meoriquet.flexibleBody);
                body = body.Replace("{Meoriquet.flexibleBody}", Meoriquet.flexibleBody);
                body = body.Replace(" {Meoriquet.location}", Meoriquet.location);
                body = body.Replace("{Meoriquet.who}", Meoriquet.who);
                body = body.Replace("{Meoriquet.timeMeeting}", ConvertTimeFormat(Meoriquet.timeMeeting));
            }
            Meoriquet.Body = body;
            //Meoriquet.Body = $@"<!DOCTYPE html>
            //    <html>
            //    <head>
            //        <meta charset='UTF-8'>
            //        <title>Email Notification</title>
            //    </head>
            //    <body>
            //        <h2>Thông báo từ SSV:</h2>
            //        <p> Mời phỏng vấn cho vị trí {Meoriquet.role}</p>
            //        <p>Xin chào {Meoriquet.Name},</p>
            //        <p>{Meoriquet.flexibleBody}</p>
            //        <p>Thời gian: {Meoriquet.timeMeeting} Địa điểm: {Meoriquet.location} </p>
            //        <p>nếu có thắc mắc gì về nội dung Mail xin vui lòng gọi đến 016868888 để được hỗ trợ .</p>
            //        <p>Rất mong được gặp {Meoriquet.who} tại buổi phỏng vấn.</p>
            //        <p>Trân trọng,</p>
            //        <p>SSV</p>
            //    </body>
            //    </html>";

            Meoriquet = meoriquet;
            Meoriquet.ToEmail = meoriquet.ToEmail;
            await SendMailService.SendMail(Meoriquet);



        }


        /// <summary>
        /// Phương thức tạo bản mail hoàn chỉnh có các dữ liệu từ form mail được tạo ban đầu.
        /// </summary>
        ///<param name="user"></param>
        /// <returns>.</returns>
        private void handleCreateMail(Users user)
        {
            //who = user.who;
            //if (mail == "")
            //{
            //    errTitle_Flg = false;
            //    return;
            //}
            if (Users.who == null)
            {
                missingWho_Flg = false;
                return;
            }
            Users = user;


            createMailFlg = true;
            user.isCreateMail = true;
            Users.Body = mailND;
            
            Users.Subject = lblMail;
            mail = "";

        }




        /// <summary>
        /// Phương thức cho phép gửi mail sau khi xem preview mail.
        /// </summary>
        ///<param name="user"></param>
        /// <returns>.</returns>
        private async Task handleUpdateMail(Users user)
        {
            isSending = false;
            user.who = who;
            Users.Body = user.Body;
            
            Users = user;
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                foreach (var uDetails in looking)
                {
                    if (Users.userID == uDetails.userID && uDetails.isCreateMail)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.typeMail == 0)
                                { uDetails.typeMail = 1; }
                                else if (uDetails.typeMail == 1)
                                { uDetails.typeMail = 2; }

                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    //else
                    //{
                    //    if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                    //    {
                    //        uHint = uDetails.userID;
                    //        uHint2 += uHint.ToString() + ",";
                    //        errM_Flg = false;
                    //    }

                    //}

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    looking.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Filtering();
            }
            if (orderFlg)
            {
                foreach (var uDetails in userOrders)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.Subject == "Mail mời phỏng vấn vòng 1")
                                { uDetails.typeMail = 1; }
                                else if (uDetails.Subject == "Mail mời phỏng vấn vòng 2")
                                { uDetails.typeMail = 2; }

                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    //else
                    //{
                    //    if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                    //    {
                    //        uHint = uDetails.userID;
                    //        uHint2 += uHint.ToString() + ",";
                    //        errM_Flg = false;
                    //    }

                    //}

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userOrders.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Ordering();
            }
            if (searchFlg)
            {
                foreach (var uDetails in userSearchs)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.Subject == "Mail mời phỏng vấn vòng 1")
                                { uDetails.typeMail = 1; }
                                else if (uDetails.Subject == "Mail mời phỏng vấn vòng 2")
                                { uDetails.typeMail = 2; }

                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    //else
                    //{
                    //    if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                    //    {
                    //        uHint = uDetails.userID;
                    //        uHint2 += uHint.ToString() + ",";
                    //        errM_Flg = false;
                    //    }

                    //}

                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userSearchs.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Searching();
            }
            else
            {
                foreach (var uDetails in userDetails)
                {
                    if (Users.userID == uDetails.userID && uDetails.isCreateMail)
                    {
                        //mycheck = uDetails.isChecked;
                        try
                        {
                            if (uDetails.Body != null)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.location = uDetails.Place;
                                meoriquet.timeMeeting = uDetails.TimeMeeting;
                                meoriquet.role = uDetails.Role;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                if (uDetails.typeMail == 0)
                                { uDetails.typeMail = 1; }
                                else if (uDetails.typeMail == 1)
                                { uDetails.typeMail = 2; }

                                uDetails.Status = 5;
                                SendMailService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    //else
                    //{
                    //    if (uDetails.who != null /*|| uDetails.who != string.Empty*/)
                    //    {
                    //        uHint = uDetails.userID;
                    //        uHint2 += uHint.ToString() + ",";
                    //        errM_Flg = false;
                    //    }

                    //}


                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userDetails.RemoveAll(u => u.Body == temp.Body && u.isChecked == temp.isChecked);
                }
                //OnInitializedAsync();
            }
            isSending = true;
            previewMailFlg = true;
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
                    looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserSendMail?research={transText}");
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
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserSendMail?filterBy={transText}");

            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserSendMail?order={order}");

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
                    if ((uDetails.Position.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Email.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Role.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.userName.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Place.Contains(search, StringComparison.OrdinalIgnoreCase)) || (SearchID.Contains(search, StringComparison.OrdinalIgnoreCase)) || (uDetails.Phone.Contains(search, StringComparison.OrdinalIgnoreCase))|| (ConvertTimeFormat(uDetails.TimeMeeting).Contains(search, StringComparison.OrdinalIgnoreCase)))
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
