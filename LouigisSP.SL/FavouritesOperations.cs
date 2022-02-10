using LouigisSP.BO;
using LouigisSP.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL
{
    public class FavouritesOperations
    {
        public IEnumerable<Product> GetFavourites(int id_currentPerson)
        {
            IEnumerable<Product> products = new List<Product>();
            var idP = DBManager.CreateParameter("@id_person", id_currentPerson, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = idP;
            DataTable dataTable = DBManager.GetDataTable("select products.id, products.name, products.brand, products.model, products.color, products.price, products.stock, products.extra_info, products.barcode, products.src_image from products join favourites on products.id = favourites.id_product where favourites.id_person = @id_person", CommandType.Text, parameters);
            return products = dataTable.AsEnumerable().Select(
                 row => new Product
                 {
                     id = Convert.ToInt32(row["id"]),
                     name = Convert.ToString(row["Name"]),
                     brand = Convert.ToString(row["brand"]),
                     model = Convert.ToString(row["model"]),
                     color = Convert.ToString(row["color"]),
                     price = Convert.ToInt32(row["price"]),
                     stock = Convert.ToInt32(row["stock"]),
                     extra_info = Convert.ToString(row["extra_info"]),
                     barcode = Convert.ToString(row["barcode"]),
                     src_image = Convert.ToString(row["src_image"])
                 });


        }

        public void addFavourite(int id_currentPerson, int id_product)
        {
            IEnumerable<Product> products = new List<Product>();
            var idPerson = DBManager.CreateParameter("@id_person", id_currentPerson, DbType.Int32);
            var idProduct = DBManager.CreateParameter("@id_product", id_product, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[2];
            parameters[0] = idProduct;
            parameters[1] = idPerson;


            DBManager.Insert("insert into favourites (id_product, id_person) values (@id_product, @id_person)", CommandType.Text, parameters);
        }


        public void delFavourite(int id_currentPerson, int id_product)
        {
            var idPerson = DBManager.CreateParameter("@id_person", id_currentPerson, DbType.Int32);
            var idProduct = DBManager.CreateParameter("@id_product", id_product, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[2];
            parameters[0] = idProduct;
            parameters[1] = idPerson;
            try
            {
                DBManager.Delete("delete from favourites where id_person=@id_person and id_product=@id_product", CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
