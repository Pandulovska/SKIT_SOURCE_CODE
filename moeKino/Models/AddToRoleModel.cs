using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class AddToRoleModel
    {
        public string Email { get; set; }
        public string SelectedRole { get; set; }
        public List<String> Roles { get; set; }

        public AddToRoleModel() {
            Roles = new List<string>();
        }
    }
}