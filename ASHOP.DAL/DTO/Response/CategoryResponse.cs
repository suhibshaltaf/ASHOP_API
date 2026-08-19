using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string User{ get; set; }
        public string Name { get; set; }
    }
}
