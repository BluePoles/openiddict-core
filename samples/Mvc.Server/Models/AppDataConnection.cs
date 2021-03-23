using LinqToDB.Configuration;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;

namespace Mvc.Server.Models
{
    public class AppDataConnection : DataConnection
    {
        public AppDataConnection(LinqToDbConnectionOptions<AppDataConnection> options)
        : base(options)
        {
            
        }
    }
}
