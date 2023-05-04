using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection : DataConnection
    {
        
        public Connection() : base("PDHN1"){
        
        }

        
        public ITable<Student> _Student { get { return  this.GetTable<Student>(); } }//This is use in order to make INSERT,UPDATE,SELECT,DELETE en SQL SERVER

       
    }
}
