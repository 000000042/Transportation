using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.Core.DTOs.AdminDTO
{

    public class UsersToSelectList
    {
        public int UserId { get; set; }

        public string FullName { get; set; }
    }

    public class ContractsSelectLists
    {

    }
}
