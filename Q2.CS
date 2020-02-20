 /// <summary>
        /// Refactoring the following method
        /// </summary>
        public void Register(UserModel user)
        {
            bool validate = true;
            string errorMessage = "";
            if (String.IsNullOrEmpty(user.UserName))
            {
                errorMessage += "You must specify a username.";
                validate = false;
            }
            if (String.IsNullOrEmpty(user.Email))
            {
                errorMessage += Environment.NewLine +  "You must specify an email address.";
                validate = false;
            }
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            else if (!Regex.IsMatch(email, regex, RegexOptions.IgnoreCase))
            {
                errorMessage += Environment.NewLine + "You must specify a valid email address.";
                validate = false;
            }

            //bool isExisted = false;
            var existedUser = _userService.GetUserByUsername(user.Username);

            if (existedUser != null)
            {
                //isExisted = true;
                errorMessage += "User with that username and/or email already exists.";
            }
            //else
            //{
            //    isExisted = false;
            //}

            //if (isExisted)
            //{
            //    errorMessage += "User with that username and/or email already exists.";
            //}

            if (user.Password == null || user.Password.Length < 6)
            {
                errorMessage += "You must specify a password of {0} or more characters.",  6);
            }

            if (!String.Equals(user.Password, user.ConfirmPassword, StringComparison.Ordinal))
            {
                errorMessage += "The new password and confirmation password do not match.";
            }

            if (validate)
            {
                Regex RgxUrl = new Regex("[^a-z0-9]");
                var isContainsSpecialCharacters = RgxUrl.IsMatch(user.Username);
                if (isContainsSpecialCharacters)
                {
                    _userService.SaveUser(user);

                    try
                    {
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress("admin@hotmail.com");
                        mail.To.Add(user.Email);
                        mail.Subject = "Register";
                        mail.Body = "Thank you for your register";

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("admin@hotmail.com", "password");
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);

                        //MailMessage message = new MailMessage();
                        //message.To.Add(user.Email);
                        //message.Subject = "Register";
                        //message.From = new System.Net.Mail.MailAddress("admin@hotmail.com");
                        //message.Body = "Thank you for your register";
                        //SmtpClient smtp = new SmtpClient();
                        //smtp.Host = "smtp.gmail.com";
                        //smtp.Send(message);

                        _userService.Sendmail(true, "Success");
                    }
                    catch (Exception ex)
                    {
                        _userService.Sendmail(false, ex.ToString());
                    }
                }
            }


        }