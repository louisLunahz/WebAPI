using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;
using LouigisSP.DL;

namespace LouigisSP.SL
{
    public class ShoppingCartOperations
    {

  

        public IEnumerable<Cart> GetAllCarts()
        {
            IEnumerable<Cart> products = new List<Cart>();
            DataTable dataTable = DBManager.GetDataTable("select * from shopping_carts ", CommandType.Text);
            return products = dataTable.AsEnumerable().Select(
                 row => new Cart
                 {
                     Id = Convert.ToInt32(row["id"]),
                     IdPerson = Convert.ToInt32(row["id_person"]),
                    
                 });
        }

      

        public IEnumerable<Cart> GetAllCartsRelatedToOnePerson(int id)
        {
            IEnumerable<Cart> products = new List<Cart>();
            var id_person = DBManager.CreateParameter("@id_person", id, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = id_person;
            DataTable dataTable = DBManager.GetDataTable("select * from shopping_carts where id_person=@id_person", CommandType.Text, parameters);
            return products = dataTable.AsEnumerable().Select(
                 row => new Cart
                 {
                     Id = Convert.ToInt32(row["id"]),
                     IdPerson = Convert.ToInt32(row["id_person"]),

                 });
        }

        public IEnumerable<Cart> GetAllCartsRelatedToOnePersonNotInTheOrdersTable(int id) {
            IEnumerable<Cart> products = new List<Cart>();
            var id_person = DBManager.CreateParameter("@id_person", id, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = id_person;
            DataTable dataTable = DBManager.GetDataTable("SELECT * FROM shopping_carts WHERE id_person = @id_person and id NOT IN (SELECT id_cart FROM Orders)", CommandType.Text, parameters);
            return products = dataTable.AsEnumerable().Select(
                 row => new Cart
                 {
                     Id = Convert.ToInt32(row["id"]),
                     IdPerson = Convert.ToInt32(row["id_person"]),

                 });
        }

        public IEnumerable<Cart> GetAllCartsRelatedToOnePersonNotInTheOrdersTableWithAtLeastOneItem(int id)
        {
            IEnumerable<Cart> carts = new List<Cart>();
            var id_person = DBManager.CreateParameter("@id_person", id, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = id_person;
            DataTable dataTable = DBManager.GetDataTable("SELECT * FROM shopping_carts WHERE id_person = @id_person and id NOT IN (SELECT id_cart FROM Orders) and exists(select id_shoppingCart from Items) ", CommandType.Text, parameters);
            return carts = dataTable.AsEnumerable().Select(
                 row => new Cart
                 {
                     Id = Convert.ToInt32(row["id"]),
                     IdPerson = Convert.ToInt32(row["id_person"]),

                 });
        }


        public void CreateCartForPerson(int id_person)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = DBManager.CreateParameter("@id_person", id_person, DbType.Int32);
            DBManager.Insert("insert into shopping_carts (id_person) values (@id_person)", commandType: CommandType.Text, parameters);
        }

        public void InsertItem(Product product, int id_cart, int quantity)
        {
            if (product is null)
                throw new ArgumentNullException();
            
                IDbDataParameter[] parameters = new IDbDataParameter[4];
                parameters[0] = DBManager.CreateParameter("@quantity", quantity, DbType.Int32);
                parameters[1] = DBManager.CreateParameter("@id_product", product.id, DbType.Int32);
                parameters[2] = DBManager.CreateParameter("@id_shoppingCart", id_cart, DbType.Int32);
                parameters[3] = DBManager.CreateParameter("@individual_total", product.price*quantity, DbType.Currency);
                DBManager.Insert("insert into items(quantity, id_product, id_shoppingCart, individual_total) values (@quantity,@id_product, @id_shoppingCart, @individual_total)", commandType: CommandType.Text, parameters);
        }

        public void DeleteItemFromCart(int id_item, int id_cart)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[2];
            var product = DBManager.CreateParameter("@id_item", id_item, DbType.Int32);
            var cart = DBManager.CreateParameter("@id_cart", id_cart, DbType.Int32);
            parameters[0]=product;
            parameters[1]=cart;
            try { 
                 DBManager.Delete("delete from items where id_item=@id_item and id_shoppingCart=@id_cart", CommandType.Text, parameters);
            }catch (Exception e)
            {
                throw e;
            }



        }

        public IEnumerable<Item> GetAllItemsInCart(int id_cart)
        {
            var id_shoppingCartP = DBManager.CreateParameter("@id_shoppingCart", id_cart, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = id_shoppingCartP;
            IEnumerable<Item> listItems = new List<Item>();
            DataTable dataTable = DBManager.GetDataTable("select * from items where id_shoppingCart=@id_shoppingCart ", CommandType.Text, parameters);
            return listItems = dataTable.AsEnumerable().Select(
                 row => new Item
                 {
                     id= Convert.ToInt32(row["id_item"]),
                     quantity = Convert.ToInt32(row["quantity"]),
                     id_product = Convert.ToInt32(row["id_product"]),
                     id_shoppingCart = Convert.ToInt32(row["id_shoppingCart"]), 
                     individual_total= (float)Convert.ToDouble(row["individual_total"]),
                 });

        }

    }
}
