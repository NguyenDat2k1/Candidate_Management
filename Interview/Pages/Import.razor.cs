using Interview.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;

namespace Interview.Pages
{
    public partial class Import
    {
        string? userName, Email, Address, language, role, source, pathCV;
        bool nameFlg = true;
        bool emailFlg = true;
        bool addressFlg = true;
        bool phone0Flg = true;
        bool phoneLength_Flg = true;
        bool phoneChar_Flg = true;
        int status;
        bool roleFlg = true;
        bool souFlg = true;
        bool langFlg = true;
        bool fileFlg = true;
        public DateTime Birthday = DateTime.Today;
        bool emai1lFlg = true;
        bool phoneFlg = true;
        string Phone = "";
        bool isUpdate = false;
        private List<Users> userDetails = new();
        private List<typecs>? langDetails, roleDetails, sugDetails;
        //handle validate email
        public static bool IsValidEmail(string email)
        {
            // Định dạng email phù hợp
            // email phải có ít nhất một ký tự trước dấu @, và sau đó là một tên miền phù hợp
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Kiểm tra định dạng email
            if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailPattern))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// phương thức xử lý import thông tin ứng viên
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        private void imported()
        {
            nameFlg = true; phone0Flg = true; emailFlg = true; addressFlg = true; roleFlg = true; souFlg = true; langFlg = true; fileFlg = true; phoneChar_Flg = true; phoneLength_Flg = true; phoneFlg = true; emai1lFlg = true;
            if (language == null || language == string.Empty)
            {
                langFlg = false;
            }

            if (role == null || role == string.Empty)
            {
                roleFlg = false;
            }

            if (loadedFile == null)
            {
                fileFlg = false;
            }
            if (source == null || source == string.Empty)
            {
                souFlg = false;
            }

            if (userName == null || userName == string.Empty)
            {
                nameFlg = false;
            }
            if (phone0Flg && phoneChar_Flg && phoneLength_Flg && phoneFlg)
            {
                if (Phone == null || Phone == string.Empty)
                {
                    phoneFlg = false;
                }
                else if (!Phone.All(char.IsDigit))
                {
                    phoneChar_Flg = false;
                }
                else if (Phone[0] != '0' /*&& Phone[0] != '1'*/)
                {
                    phone0Flg = false;
                }
                else if (Phone.Length != 10)
                {
                    phoneLength_Flg = false;
                }

            }
            if (emailFlg)
            {
                if (Email == null || Email == string.Empty)
                {
                    emailFlg = false;
                }
                else if (!IsValidEmail(Email))
                {
                    emai1lFlg = false;
                }
            }

            if (Address == null || Address == string.Empty)
            {
                addressFlg = false;
            }
            else
            {
                foreach (var uDetails in userDetails)
                {
                    if (Email == uDetails.Email && Phone == uDetails.Phone)
                    {
                        if (language == null || language == string.Empty)
                        {
                            langFlg = false;
                        }

                        if (role == null || role == string.Empty)
                        {
                            roleFlg = false;
                        }

                        if (loadedFile == null)
                        {
                            fileFlg = false;
                        }
                        if (source == null || source == string.Empty)
                        {
                            souFlg = false;
                        }

                        if (userName == null || userName == string.Empty)
                        {
                            nameFlg = false;
                        }
                        if (phone0Flg && phoneChar_Flg && phoneLength_Flg && phoneFlg)
                        {
                            if (Phone == null || Phone == string.Empty)
                            {
                                phoneFlg = false;
                            }
                            else if (!Phone.All(char.IsDigit))
                            {
                                phoneChar_Flg = false;
                            }
                            else if (Phone[0] != '0' /*&& Phone[0] != '1'*/)
                            {
                                phone0Flg = false;
                            }
                            else if (Phone.Length != 10)
                            {
                                phoneLength_Flg = false;
                            }

                        }
                        if (emailFlg)
                        {
                            if (Email == null || Email == string.Empty)
                            {
                                emailFlg = false;
                            }
                            else if (!IsValidEmail(Email))
                            {
                                emai1lFlg = false;
                            }
                        }

                        if (Address == null || Address == string.Empty)
                        {
                            addressFlg = false;
                        }
                        if ((language != null && language != string.Empty) && (role != null && role != string.Empty) && (loadedFile != null) && (source != null && source != string.Empty) && (userName != null && userName != string.Empty) && (Address != null && Address != string.Empty) && (emailFlg != false))
                        {
                            ImportService.UpdateUserDetails(uDetails.userID, userName, Birthday, Email, Phone, Address, language, role, source, status, pathCV);
                            isUpdate = true;
                        }
                        else
                        {
                            return;
                        }
                    }

                }

                if (isUpdate == false)
                {
                    if (language == null || language == string.Empty)
                    {
                        langFlg = false;
                    }

                    if (role == null || role == string.Empty)
                    {
                        roleFlg = false;
                    }

                    if (loadedFile == null)
                    {
                        fileFlg = false;
                    }
                    if (source == null || source == string.Empty)
                    {
                        souFlg = false;
                    }

                    if (userName == null || userName == string.Empty)
                    {
                        nameFlg = false;
                    }
                    if (phone0Flg && phoneChar_Flg && phoneLength_Flg && phoneFlg)
                    {
                        if (Phone == null || Phone == string.Empty)
                        {
                            phoneFlg = false;
                        }
                        else if (!Phone.All(char.IsDigit))
                        {
                            phoneChar_Flg = false;
                        }
                        else if (Phone[0] != '0' /*&& Phone[0] != '1'*/)
                        {
                            phone0Flg = false;
                        }
                        else if (Phone.Length != 10)
                        {
                            phoneLength_Flg = false;
                        }

                    }
                    if (emailFlg)
                    {
                        if (Email == null || Email == string.Empty)
                        {
                            emailFlg = false;
                        }
                        else if (!IsValidEmail(Email))
                        {
                            emai1lFlg = false;
                        }
                    }

                    if (Address == null || Address == string.Empty)
                    {
                        addressFlg = false;
                    }
                    if ((language != null && language != string.Empty) && (role != null && role != string.Empty) && (loadedFile != null) && (source != null && source != string.Empty) && (userName != null && userName != string.Empty) && (Address != null && Address != string.Empty) && (emailFlg != false))
                    { 
                        ImportService.SaveUserDetails(userName, Birthday, Email, Phone, Address, language, role, source, status, pathCV);
                    }
                    else
                    {
                        return;
                    }
                }



                NavigationManager.NavigateTo("Filter");
            }
        }


        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        /// <param></param>
        /// <returns>.</returns>
        protected override async Task OnInitializedAsync()
        {
            langDetails = await ImportService.getLanguageDetails();
            roleDetails = await ImportService.getRoleDetails();
            sugDetails = await ImportService.getSugDetails();
            userDetails = await ImportService.getUserAll();

        }
        private Users users = new Users();


        private void HandleValidSubmit()
        {

            //ImportService.SaveUserDetails(userName, Birthday, Email, Phone, Address, language, role, position);
        }
        private IBrowserFile loadedFile;
        private long maxFileSize = 2097152;
        private bool isLoading;
        //handle take path'CV
        private void appendPath(string a)
        {
            pathCV = a;
        }

        /// <summary>
        /// phương thức xử lý upload file .
        /// </summary>
        /// <param name="e"
        /// <returns>.</returns>
        private async Task LoadFile(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFile = e.File;

            try
            {
                //var trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(Environment.ContentRootPath, "wwwroot", "CV",loadedFile.Name);

                await using FileStream fs = new(path, FileMode.Create);
                await loadedFile.OpenReadStream(maxFileSize).CopyToAsync(fs);
                appendPath(loadedFile.Name);

            }
            catch (Exception ex)
            {
                Logger.LogError("File: {Filename} Error: {Error}",
                    loadedFile.Name, ex.Message);

            }


            isLoading = false;

        }



    }
}
