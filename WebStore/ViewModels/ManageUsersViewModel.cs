using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.ViewModels
{
    public class ManageUsersViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}