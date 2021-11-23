using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public abstract class Depot_DAL<Type_DAL> : IDepot_DAL<Type_DAL>
    {
        public string ConnectionString { get; set; }

        protected SqlConnection connection;
        protected SqlCommand command;
        public Depot_DAL()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile("appsettings.json", false, true).Build();

            ConnectionString = config.GetSection("ConnectionStrings:default").Value;
        }

        protected void CreateConnectionAndCommand()
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
        }

        protected void DestroyConnectionAndCommand()
        {
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        #region abstract methode
        public abstract void Delete(Type_DAL item);
        public abstract List<Type_DAL> GetAll();
        public abstract Type_DAL GetByID(int ID);
        public abstract Type_DAL Insert(Type_DAL item);
        public abstract Type_DAL Update(Type_DAL item);
        #endregion
    }
}
