using Interview.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Web;

namespace Interview.Pages
{
    public partial class SendResult
    {
   



    private List<Users>? userDetails/*, usersInSchedule*/;
        private List<typecs>? typeDetails;
        string?  BodyPreview, mail, mailND, locationPV, tenUV, Reason;
        //private List<MailRequest>? templDetails;
        int sign;
        string search = "";
        string Order = "";
        string type = "";
        private List<Users> userSearchs = new();
        private List<Users> userOrders = new();
        private List<Users> userFilters = new();
        DateTime timeMeeting, tgianPV;
        //private bool isChecked1 = true;
        private bool isChecked2 = false;
        //bool add2Flg = true;
        //bool isJudge;
        bool scheduleFlg = false;
        bool filterFlg = false;
        bool orderFlg = false;
        bool searchFlg = false;
        bool cautionFlg = true;
        //bool mycheck = false;
        bool judgingFlg = true;
        bool addPV_Flg = true;
        bool previewMailFlg = true;
        bool judgedFlg = false;
        MailRequest meoriquet = new MailRequest();
        bool scheBtnFlg = false;
        bool errF_Flg = true;
        bool errS_Flg = true;
        bool errO_Flg = true;
        private Users Users = new Users();
        bool isSending = true;
        bool createMailFlg = true;
        bool createMailFailedFlg = true;
        bool transFlg = true;
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

        



        /// <summary>
        /// Phương thức nhận và lưu giá trị trường who được điền.
        /// </summary>
        /// <param args=>giá trị được lấy từ từ form mail.</param>
        ///
        /// <returns>.</returns>
        private void UpdateUserWho(ChangeEventArgs args)
        {
            Users.who = args.Value.ToString();
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
        //close err popup
        private void fclose()
        {
            
            loadedFiles.Clear();
            Users.who = "";
            valiOffer = false;
            createMailFlg = true;
            

        }
        //close err popup
        private void fMClose()
        {
            loadedFiles.Clear();
            Users.who = "";

            createMailFailedFlg = true;

        }

        //close err popup
        private void pclose()
        {

            previewMailFlg = true;

        }

        //handle trans to SendMail screen
        private void transTo()
        {
            Users.Status = 15;
            SendResultService.SendSusscessful(Users.Status, Users.userID, Users.typeMail);
            List<Users> Tempo = new List<Users>();
            if (filterFlg)
            {
                foreach (var uDetails in userFilters)
                    if (Users.userID == uDetails.userID)
                    {
                        Tempo.Add(uDetails);
                    }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userFilters.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            if (searchFlg)
            {
                foreach (var uDetails in userSearchs)
                    if (Users.userID == uDetails.userID)
                    {
                        Tempo.Add(uDetails);
                    }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            if (orderFlg)
            {
                foreach (var uDetails in userOrders)
                    if (Users.userID == uDetails.userID)
                    {
                        Tempo.Add(uDetails);
                    }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userOrders.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            else
            {
                foreach (var uDetails in userDetails)
                    if (Users.userID == uDetails.userID)
                    {
                        Tempo.Add(uDetails);
                    }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters

                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
            }
            transFlg = true;

        }

        //close" trans to "popup
        private void transClose()
        {

            transFlg = true;

        }
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

            typeDetails = await SendResultService.SelectboxFilter();
            var client = httpClient.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7142");
            userDetails = await client.GetFromJsonAsync<List<Users>>("/api/getAllUserNeedToSendResult");


        }

        
        string temP = string.Empty;

        
        //update tong
        private async Task Update()
        {
            userDetails = await SendResultService.getUserSendResult();

        }




        /// <summary>
        /// Phương thức khởi tạo form mail dựa vào đối tượng truỳen vào  .
        /// </summary>
        /// <param user=>giá trị được lấy từ đối tượng truyền vào .</param>
        ///
        /// <returns>.</returns>
        private void Judge(Users user)
        {


            Users = user;
            if (Users.Position == "Intern" && Users.Status == 20)
            {
                transFlg = false;
                return;
            }
            else if (Users.Position == "Intern" && Users.Status == 19)
            {
                mail = "Thư từ chối";
                createMailFailedFlg = false;
                return;
            }
            //@if (Users.Position == "Intern" && Users.typeMail == 1 && Users.Status == 20)
            //{
            //    mail = "Thư mời phỏng vấn vòng 2";
            //}
            if ((Users.Position == "fresher" || Users.Position == "Junior") && Users.typeMail == 1 && Users.Status == 20)
            {
                mail = "Thư gửi Offer công việc";
            }
            else
            {
                mail = "Thư từ chối";
                createMailFailedFlg = false;
                return;

            }
            createMailFlg = false;



        }


        /// <summary>
        /// Phương thức cho phép gửi mail dựa trên đối tượng chứa các thành phần được truyền.
        /// </summary>
        ///<param name="Meoriquet"></param>
        /// <returns>.</returns>
        private async Task sendMail(MailRequest Meoriquet)
        {
            Meoriquet.who = Users.who;

            if (Meoriquet.statusTrack == 20)
            {
                string body = string.Empty;
                string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "css", "templateMailOffer.html");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                    body = body.Replace("{Meoriquet.role}", Meoriquet.role);
                    body = body.Replace("{Meoriquet.Name}", Meoriquet.Name);
                  
                    body = body.Replace("{Meoriquet.who}", Meoriquet.who);
                
                }
                Meoriquet.Body = body;
                //Meoriquet.Body = $@"<!DOCTYPE html>
                //<html>
                //<head>
                //    <meta charset='UTF-8'>
                //    <title>Email Notification</title>
                //</head>
                //<body>

                //    <p>Phỏng vấn thành công cho vị trí {Meoriquet.role} </p>
                //    <h4>Thông báo từ SSV:</h4>

                //    <p>Xin chào {Meoriquet.Name},</p>
                //    <p>Chúc mừng {Meoriquet.who} đã vượt qua vòng phỏng vấn và công ty xin được gửi tới  {Meoriquet.who}  {Meoriquet.Name} các thông tin về offer công việc</p>


                //    <p>nếu có thắc mắc gì về nội dung Mail xin vui lòng gọi đến 016868888 để được hỗ trợ .</p>
                //    <p>Rất mong được nhận phản hồi sớm của {Meoriquet.who} .</p>
                //    <p>Trân trọng,</p>
                //    <p>SSV</p>
                //</body>
                //</html>";
                Meoriquet.Subject = "Thư gửi Offer công việc";
            }

            else if (Meoriquet.statusTrack == 19)
            {
                string body = string.Empty;
                string filePath = Path.Combine(Environment.ContentRootPath, "wwwroot", "css", "templateMailReject.html");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                    body = body.Replace("{Meoriquet.role}", Meoriquet.role);
                    body = body.Replace("{Meoriquet.Name}", Meoriquet.Name);

                    body = body.Replace("{Meoriquet.who}", Meoriquet.who);

                }
                Meoriquet.Body = body;
                //Meoriquet.Body = $@"<!DOCTYPE html>
                //<html>
                //<head>
                //    <meta charset='UTF-8'>
                //    <title>Email Notification</title>
                //</head>
                //<body>

                //    <p>Phỏng vấn thất bại cho vị trí  {Meoriquet.role} </p>
                //    <h4>Thông báo từ SSV:</h4>

                //    <p>Xin chào {Meoriquet.Name},</p>
                //    <p>Rất tiếc {Meoriquet.who} đã chưa vượt qua vòng phỏng vấn và công ty xin được gửi tới  {Meoriquet.who}  {Meoriquet.Name} lời cảm ơn vì đã quan tâm đến vị trí công việc và mong {Meoriquet.who} {Meoriquet.Name} sẽ tìm được công việc phù hợp khác </p>


                //    <p>nếu có thắc mắc gì về nội dung Mail xin vui lòng gọi đến 016868888 để được hỗ trợ .</p>

                //    <p>Trân trọng,</p>
                //    <p>SSV</p>
                //</body>
                //</html>";
                Meoriquet.Subject = "Thư từ chối";


            }

            Meoriquet = meoriquet;
            Meoriquet.Attachments = meoriquet.Attachments;
            Meoriquet.ToEmail = meoriquet.ToEmail;
            await SendResultService.SendMail(Meoriquet);



        }
        // handle send offer to user

        bool valiOffer= false;



        /// <summary>
        /// Phương thức cho phép gửi mail dựa trên đối tượng .
        /// </summary>
        ///
        /// <returns>.</returns>
        private async Task handleSendOffer()
        {
            if (createMailFlg == false)
            {
                if (loadedFiles.Count == 0)
                {
                    valiOffer = true;
                    return;
                }
            }
            isSending = false;
            
            
            List<Users> Tempo = new List<Users>();
            if (lookFlg)
            {
                foreach(var uDetails in looking)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        try
                        {
                            if (meoriquet.Attachments.Count != 0)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.statusTrack = uDetails.Status;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                uDetails.Status = 22;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                            else
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.who = Users.who;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                meoriquet.statusTrack = uDetails.Status;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    looking.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Filtering();
            }
            if (orderFlg)
            {
                foreach(var uDetails in userDetails)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        try
                        {
                            if (meoriquet.Attachments.Count != 0)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.statusTrack = uDetails.Status;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                            else
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.who = Users.who;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                meoriquet.statusTrack = uDetails.Status;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userOrders.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Ordering();
            }
            if (searchFlg)
            {
                foreach(var uDetails in userDetails)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        try
                        {
                            if (meoriquet.Attachments.Count != 0)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.statusTrack = uDetails.Status;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                            else
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.who = Users.who;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                meoriquet.statusTrack = uDetails.Status;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userSearchs.RemoveAll(u => u.userID == temp.userID);
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                //Searching();
            }
            else
            {
                foreach(var uDetails in userDetails)
                {
                    if (Users.userID == uDetails.userID)
                    {
                        try
                        {
                            if (meoriquet.Attachments.Count != 0)
                            {

                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.statusTrack = uDetails.Status;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                await sendMail(meoriquet);

                                uDetails.Status = 22;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                            else
                            {
                                meoriquet.Subject = uDetails.Subject;
                                meoriquet.ToEmail = uDetails.Email;
                                meoriquet.Name = uDetails.userName;
                                meoriquet.who = Users.who;
                                meoriquet.role = uDetails.Position;
                                meoriquet.flexibleBody = uDetails.Body;
                                meoriquet.statusTrack = uDetails.Status;
                                await sendMail(meoriquet);

                                uDetails.Status = 21;
                                SendResultService.SendSusscessful(uDetails.Status, uDetails.userID, uDetails.typeMail);
                                Tempo.Add(uDetails);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                foreach (var temp in Tempo)
                {
                    // Loại bỏ các phần tử giống với phần tử temp trong userFilters
                    userDetails.RemoveAll(u => u.userID == temp.userID);
                }
                
            }
            isSending = true;
            createMailFlg = true;
            createMailFailedFlg = true;
        }
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 2097152;
        private int maxAllowedFiles = 5;
        private bool isLoading;





        /// <summary>
        /// Phương thức cho phép upload file .
        /// </summary>
        ///
        /// <returns>.</returns>
        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();
          
            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                try
                {
                    loadedFiles.Add(file);


                }
                catch (Exception ex)
                {
                    Logger.LogError("File: {Filename} Error: {Error}",
                        file.Name, ex.Message);
                }
            }
            meoriquet.Attachments = loadedFiles.Cast<IBrowserFile>().ToList();
            isLoading = false;
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
                        if((ConvertTimeFormat(uDetails.TimeMeeting).Contains(search, StringComparison.OrdinalIgnoreCase)))
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
                    looking = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserNeedToSendResult?research={transText}");
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
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserNeedToSendResult?filterBy={transText}");

            }
            else if (search == "" && type == "" && Order != "")
            {
                int order = int.Parse(Order);
                var client = httpClient.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7142");
                looking = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserNeedToSendResult?order={order}");

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
            userSearchs = await client.GetFromJsonAsync<List<Users>>($"/api/searchUserNeedToSendResult?research={transText}");
            userDetails = await SendResultService.getUserSendResult();
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
                userFilters = await client.GetFromJsonAsync<List<Users>>($"/api/filterUserNeedToSendResult?filterBy={transText}");
                userDetails = await SendResultService.getUserSendResult();
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
                userOrders = await client.GetFromJsonAsync<List<Users>>($"/api/orderUserNeedToSendResult?order={order}");
                userDetails = await SendResultService.getUserSendResult();
                //return userOrders;
            }

            return userOrders;

        }
    }
}
