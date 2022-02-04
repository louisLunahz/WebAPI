using LouigisSP.BO;
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
    public class ProductOperations
    {
        public IEnumerable<Product> GetAllProducts()
        {
           IEnumerable<Product> products = new List<Product>();
            DataTable dataTable = DBManager.GetDataTable("select * from products ", CommandType.Text);
           return  products = dataTable.AsEnumerable().Select(
                row => new Product { 
                id= Convert.ToInt32(row["id"]),
                name= Convert.ToString(row["Name"]),
                brand=Convert.ToString(row["brand"]),
                model=Convert.ToString(row["model"]),
                color=Convert.ToString(row["color"]),
                price=Convert.ToInt32(row["price"]),
                stock=Convert.ToInt32(row["stock"]),
                extra_info=Convert.ToString(row["extra_info"]),
                barcode=Convert.ToString(row["barcode"]),
                src_image=Convert.ToString(row["src_image"])
                });

            

        }

        public Product GetProductById(int id) {
            Product obj_product = null;
            var idP = DBManager.CreateParameter("@id", id, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = idP;
            DataTable dataTable = DBManager.GetDataTable("select * from products where id=@id", CommandType.Text, parameters);
            if (dataTable.Rows.Count < 1) throw new ProductNotFoundException();
            else
            {

                Console.WriteLine("it was retrieved succesfully");
                DataRow row = dataTable.Rows[0];
                obj_product = new Product();
                ProcessProductInformation(row, obj_product);
                return obj_product;
            }


        }

        public void InsertProduct(Product product)
        {
            if (product is null)    
                throw new NullParameterException();                
                    try
                    {
                        IDbDataParameter[] parameters = new IDbDataParameter[8];
                        parameters[0] = DBManager.CreateParameter("@name", product.name, DbType.String);
                        parameters[1] = DBManager.CreateParameter("@brand", product.brand, DbType.String);
                        parameters[2] = DBManager.CreateParameter("@model", product.model, DbType.String);
                        parameters[3] = DBManager.CreateParameter("@color", product.color, DbType.String);
                        parameters[4] = DBManager.CreateParameter("@price", product.price, DbType.Currency);
                        parameters[5] = DBManager.CreateParameter("@stock", product.stock, DbType.Int32);
                        parameters[6] = DBManager.CreateParameter("@extra_info", product.extra_info, DbType.String);
                        parameters[7] = DBManager.CreateParameter("@barcode", product.barcode, DbType.String);

                        DBManager.Insert("insert into products(name, brand, model, color, price, stock, extra_info, barcode) values (@name,@brand, @model, @color, @price, @stock, @extra_info, @barcode)", commandType: CommandType.Text, parameters);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
        }

        public Product EditProduct(int id, Product product)
        {
            if (product == null)
                throw new NullParameterException();
            if (product.id != id)
            {
                throw new InvalidProductException("Id's don´t match");
            }
            try
            {
                IDbDataParameter[] parameters = new IDbDataParameter[9];
                parameters[0] = DBManager.CreateParameter("@name", product.name, DbType.String);
                parameters[1] = DBManager.CreateParameter("@brand", product.brand, DbType.String);
                parameters[2] = DBManager.CreateParameter("@model", product.model, DbType.String);
                parameters[3] = DBManager.CreateParameter("@color", product.color, DbType.String);
                parameters[4] = DBManager.CreateParameter("@price", product.price, DbType.Currency);
                parameters[5] = DBManager.CreateParameter("@stock", product.stock, DbType.Int32);
                parameters[6] = DBManager.CreateParameter("@extra_info", product.extra_info, DbType.String);
                parameters[7] = DBManager.CreateParameter("@barcode", product.barcode, DbType.String);
                parameters[8] = DBManager.CreateParameter("@src_image", product.src_image, DbType.String);


                DBManager.Update("update products set name=@name, brand=@brand, model=@model, color=@color, price=@price, stock=@stock, extra_info=@extra_info, barcode=@barcode, src_image=@src_image", commandType: CommandType.Text, parameters);

                return product;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ProcessProductInformation(DataRow row, Product product)
        {
            product.id = Convert.ToInt32(row["id"]);
            product.name = Convert.ToString(row["Name"]);
            product.brand = Convert.ToString(row["brand"]);
            product.model = Convert.ToString(row["model"]);
            product.color = Convert.ToString(row["color"]);
            product.price = Decimal.ToSingle((decimal)row["price"]);
            product.stock = Convert.ToInt32(row["stock"]);
            product.extra_info = Convert.ToString(row["extra_info"]);
            product.barcode = Convert.ToString(row["barcode"]);
            product.src_image = Convert.ToString(row["src_image"]);


        }
    }
}
