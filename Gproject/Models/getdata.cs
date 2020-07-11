using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Gproject.Models
{
    public class getdata
    {
        GprojectEntities db = new GprojectEntities();
        public string course { get; set; }
        public string DayName { get; set; }
        public string LabName { get; set; }
        public string floor { get; set; }
     
         
        

    }
}