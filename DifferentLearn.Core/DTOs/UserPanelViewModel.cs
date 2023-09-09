using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }
    }
    public class SideBarPanelViewModel
    {
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageName { get; set; }
    }
}
