﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.DL
{
    public class DatabaseHandlerFactory
    {
        private ConnectionStringSettings connectionStringSettings;

        public DatabaseHandlerFactory(string connectionStringName)
        {
            connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        public IDatabaseHandler CreateDatabase()
        {
            IDatabaseHandler database = null;

            switch (connectionStringSettings.ProviderName.ToLower())
            {
                case "system.data.sqlclient":
                    database = new SqlDataAccess(connectionStringSettings.ConnectionString);
                    break;
                //case "system.data.oracleclient":
                //    database = new OracleDataAccess(connectionStringSettings.ConnectionString);
                //    break;
                //case "system.data.oleDb":
                //    database = new OledbDataAccess(connectionStringSettings.ConnectionString);
                //    break;
                case "system.data.odbc":
                    database = new OdbcDataAccess(connectionStringSettings.ConnectionString);
                    break;
                default:
                    break;
            }

            return database;
        }

        public string GetProviderName()
        {
            return connectionStringSettings.ProviderName;
        }
    }
}
