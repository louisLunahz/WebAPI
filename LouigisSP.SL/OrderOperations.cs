using LouigisSP.DL;
using LouigisSP.SL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL
{
    public class OrderOperations
    {
        public void insertOrder(int idCart, int idPerson )
        {
            
            try
            {
                    IDbDataParameter[] parameters = new IDbDataParameter[4];
                    parameters[0] = DBManager.CreateParameter("@order_date", DateTime.Now, DbType.DateTime);
                    parameters[1] = DBManager.CreateParameter("@status", "waiting for delivery", DbType.String);
                    parameters[2] = DBManager.CreateParameter("@id_cart", idCart, DbType.Int32);
                    parameters[3] = DBManager.CreateParameter("@id_person", idPerson, DbType.Int32);
                  
                    DBManager.Insert("insert into orders(order_date, status, id_cart, id_person) values (@order_date, @status, @id_cart, @id_person)", commandType: CommandType.Text, parameters);
            }
            catch (Exception e)
            {
                throw new DatabaseInsertionException("there was an error while trying to save the card in the database");
            }
        }
    }
}
