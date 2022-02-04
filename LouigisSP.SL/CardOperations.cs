using LougisSP.BO;
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
    public class CardOperations
    {
        public void saveCard(Card card)
        {
            try
            {
                if (card != null)
                {

                    IDbDataParameter[] parameters = new IDbDataParameter[5];
                    parameters[0] = DBManager.CreateParameter("@number", card.Number, DbType.String);
                    parameters[1] = DBManager.CreateParameter("@name", card.OwnerName, DbType.String);
                    parameters[2] = DBManager.CreateParameter("@month", card.Month, DbType.Int32);
                    parameters[3] = DBManager.CreateParameter("@year", card.Year, DbType.Int32);
                    parameters[4] = DBManager.CreateParameter("@id_person", card.IdPerson, DbType.Int32);
                 
                    DBManager.Insert("insert into cards( number, name, month, year, id_person) values ( @number, @name, @month, @year, @id_person)", commandType: CommandType.Text, parameters);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to insert to the database");
                throw new DatabaseInsertionException();
            }
        }
    }
}
