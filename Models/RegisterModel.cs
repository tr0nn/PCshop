using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PCshop_.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }


        public string Repeat_Password { get; set; }
    }
}