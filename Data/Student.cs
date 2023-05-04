using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public  class Student
    {
        [PrimaryKey,Identity]
        public int id { set; get; }
        public string idNumber { set; get; }
        public string name { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }

        public byte[] image { set; get; }
    }
}
