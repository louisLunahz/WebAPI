﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LouigisSP.DL;
using LouigisSP.BO;
using LouigisSP.SL;
using System.Data;
using LouigisSP.SL.Exceptions;

//namespace shoppingPortal
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            bool showMenu = true;

//            while (showMenu)
//            {
//                showMenu = MainMenu();
//            }

//        }

//        private static bool MainMenu()
//        {
//            Console.Clear();
//            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
//            Console.WriteLine("++++++++++++++++WELCOME");
//            Console.WriteLine("1.-Login as user");
//            Console.WriteLine("2.-Login as Employee");
//            Console.WriteLine("3.-signup to buy from the shop");
//            Console.WriteLine("4.-exit");

//            switch (Console.ReadLine())
//            {
//                case "1":
//                    Person obj_customer = SignIn(new Customer());
//                    if (obj_customer is null)
//                    {
//                        Console.WriteLine("Could not found that customer");
//                    }
//                    else
//                    {
//                        Menu(obj_customer);

//                    }
//                    Console.ReadKey();
//                    return true;


//                case "2":
//                    Person obj_Employee = SignIn(new Employee());

//                    if (obj_Employee is null)
//                    {
//                        Console.WriteLine("Could not found that Employee");
//                    }
//                    else
//                    {
//                        Menu(obj_Employee);
//                    }
//                    Console.ReadKey();
//                    return true;
//                case "3":
//                    try
//                    {
//                        SignUP();
//                        Console.WriteLine("Customer registered succesfully");
//                    }
//                    catch (NullParameterException nullParameterEx)
//                    {
//                        Console.WriteLine("Email can´t be empty");
//                    }
//                    catch (DatabaseInsertionException e)
//                    {
//                        Console.WriteLine("Error in the registration");
//                    }


//                    Console.ReadKey();
//                    return true;

//                case "4":
//                    return false;

//                default:

//                    return true;

//            }


//        }



//        private static Person SignIn(Person person)
//        {


//            Console.Clear();
//            Console.WriteLine("Enter email");
//            string email = Console.ReadLine();
//            Console.WriteLine("Enter password");
//            string pass = Console.ReadLine();
//            Authenticator auth = new Authenticator();
//            if (person is Customer)
//            {
//                try
//                {
//                    Customer cus = auth.GetCustomer(Tuple.Create(email, pass));
//                    return cus;
//                }
//                catch (InvalidCredentialsException e)
//                {
//                    return null;
//                }

//            }
//            else if (person is Employee)
//            {
//                try
//                {
//                    Employee obj_Employee = auth.GetEmployee(Tuple.Create(email, pass));
//                    return obj_Employee;
//                }
//                catch (InvalidCredentialsException invalidCredentialsException)
//                {

//                    return null;
//                }
//                catch (UserNotFoundException userNotFoundException)
//                {

//                    return null;
//                }

//            }
//            else return null;



//        }

//        private static void SignUP()
//        {

//            Console.Clear();
//            Console.WriteLine("Enter email");
//            string email = Console.ReadLine();

//            Authenticator obj_Authenticator = new Authenticator();
//            try
//            {
//                Person obj_person = obj_Authenticator.SearchCustomerExistence(email);

//            }
//            catch (NullParameterException nullParameterEx)
//            {
//                throw nullParameterEx;
//            }
//            catch (UserNotFoundException userNotFoundException)
//            {
//                string[] values = new string[8];
//                values[0] = email;
//                Console.WriteLine("Continue registration");
//                Console.WriteLine("-----------------------");
//                Console.WriteLine("Enter your new password");
//                string pass = Console.ReadLine();
//                Console.WriteLine("Enter your first name");
//                string fName = Console.ReadLine();
//                Console.WriteLine("Enter your last name");
//                string lastName = Console.ReadLine();
//                Console.WriteLine("Enter your phone number");
//                string phone = Console.ReadLine();
//                Console.WriteLine("Enter your Date of birth");
//                string dob = Console.ReadLine();
//                Console.WriteLine("Enter your shipping Address");
//                string shippingA = Console.ReadLine();
//                Console.WriteLine("Enter your billing Address");
//                string billingA = Console.ReadLine();
//                values[0] = fName;
//                values[1] = lastName;
//                values[2] = phone;
//                values[3] = email;
//                values[4] = pass;
//                values[5] = dob;
//                values[6] = shippingA;
//                values[7] = billingA;
//                try
//                {
//                    obj_Authenticator.InsertCustomer(values);
//                }
//                catch (DatabaseInsertionException e)
//                {
//                    throw e;
//                }


//            }

//        }

//        private static void Menu(Person obj_person)
//        {
//            //show menu for customers

//            if (obj_person is Customer)
//            {
//                Customer obj_customer = (Customer)obj_person;
//                ShoppingCart obj_shoppingCart = new ShoppingCart(obj_customer);

//                string option = "";
//                //menu for customers
//                do
//                {
//                    //Console.Clear();
//                    Console.WriteLine("1.-Show all the products");
//                    Console.WriteLine("2.-See cart");
//                    Console.WriteLine("3.-show user information ");
//                    Console.WriteLine("4.-end session");
//                    switch (option = Console.ReadLine())
//                    {
//                        case "1":
//                            buy(ref obj_shoppingCart);
//                            break;

//                        case "2":
//                            if (obj_shoppingCart.ProductList.Count() >= 1)
//                            {
//                                Console.Clear();
//                                showShoppingCart(ref obj_shoppingCart);
//                                ShoppingCartOptions(ref obj_shoppingCart);
//                            }
//                            else
//                            {
//                                Console.WriteLine("There are no items in the cart yet");
//                            }
//                            break;

//                        case "3":
//                            Console.WriteLine(obj_customer.ToString());
//                            Console.ReadKey();


//                            break;

//                    }

//                } while (option != "4");


//            }
//            else if (obj_person is Employee)
//            {
//                //menu for employees 
//                string option = "";
//                do
//                {
//                    Console.Clear();
//                    Console.WriteLine("employee menu");
//                    Console.WriteLine("1.-Insert new product");
//                    Console.WriteLine("2-exit");
//                    switch (option = Console.ReadLine())
//                    {
//                        case "1":
//                            string[] values = new string[7];
//                            Console.WriteLine("insert the name for the new product");
//                            values[0] = Console.ReadLine();
//                            Console.WriteLine("Insert the brand");
//                            values[1] = Console.ReadLine();
//                            Console.WriteLine("Insert the model");
//                            values[2] = Console.ReadLine();
//                            Console.WriteLine("Insert the color");
//                            values[3] = Console.ReadLine();
//                            Console.WriteLine("Insert the price");
//                            values[4] = Console.ReadLine();
//                            Console.WriteLine("Insert the existing stock");
//                            values[5] = Console.ReadLine();
//                            Console.WriteLine("Insert extra info if necessary");
//                            values[6] = Console.ReadLine();
//                            Authenticator obj_authenticator = new Authenticator();
//                            try
//                            {
//                                obj_authenticator.insertProduct(values);
//                                Console.WriteLine("Product added succcesfully");
//                            }
//                            catch (NoValuesToInsertException noValuesToInsert)
//                            {
//                                Console.WriteLine("Product could not be added");
//                            }
//                            catch (Exception e)
//                            {
//                                Console.WriteLine("Product could not be added");
//                            }
//                            finally
//                            {
//                                Console.ReadKey();
//                            }


//                            break;


//                    }


//                } while (option != "2");



//            }





//        }


//        public static void ShowItems(List<Product> products)
//        {
//            //convert the list of objects into a list of the type given 
//            Console.Clear();
//            foreach (var p in products)
//            {
//                Console.WriteLine(p.ToString());
//            }
//        }

//        public static bool buy(ref ShoppingCart shoppingCart)
//        {

//            Authenticator obj_authenticator = new Authenticator();
//            List<Product> listProducts = obj_authenticator.GetAllProducts();
//            ShowItems(listProducts);
//            Console.WriteLine("To buy a product, insert the id ");
//            string id = Console.ReadLine();
//            Console.WriteLine("Insert the quantity");
//            string quantity = Console.ReadLine();
//            try
//            {
//                Product obj_product = obj_authenticator.GetProduct(int.Parse(id));
//                bool blnStockEnough = Utilities.CheckQuantity(obj_product, int.Parse(quantity));
//                if (blnStockEnough)
//                {
//                    shoppingCart.ProductList.Add(Tuple.Create(obj_product, int.Parse(quantity)));
//                }
//                else
//                {
//                    Console.Write("Not enough stock to fullfill the order");
//                }
//            }
//            catch (ProductNotFoundException e)
//            {
//                Console.WriteLine("Could not found that product, make sure to write the id correctly");
//            }

//            return true;

//        }

//        public static void showShoppingCart(ref ShoppingCart obj_shoppingCart)
//        {
//            float total = 0;
//            foreach (var item in obj_shoppingCart.ProductList)
//            {
//                total += item.Item1.Price * item.Item2;

//                item.Item1.showDetails();
//                Console.WriteLine(item.Item1.Price * item.Item2);
//                Console.WriteLine("quantity: " + item.Item2);
//            }
//            Console.WriteLine("Total: " + total);
//        }

//        public static void ShoppingCartOptions(ref ShoppingCart obj_shoppingCart)
//        {
//            string option;
//            string[] fields = new String[] { "idCustomer", "idProduct", "quantity", "orderDate" };
//            do
//            {
//                Console.WriteLine("1.-Make Payment ");
//                Console.WriteLine("2.-Remove item from shopping cart");
//                Console.WriteLine("3.-return");
//                option = Console.ReadLine();
//                switch (option)
//                {
//                    case "1":
//                        if (obj_shoppingCart.ProductList.Count >= 1)
//                        {

//                            ShoppingCartOperations obj_shoppingCartOperations = new ShoppingCartOperations();
//                            if (MakePayment(obj_shoppingCart))
//                            {
//                                int idCustomer = obj_shoppingCart.Cus.Id;
//                                List<Tuple<int, int>> products = new List<Tuple<int, int>>();//tuple <idProduct, quantity>
//                                for (int i = 0; i < obj_shoppingCart.ProductList.Count; i++)
//                                {
//                                    products.Add(Tuple.Create(obj_shoppingCart.ProductList.ElementAt(i).Item1.Id, obj_shoppingCart.ProductList.ElementAt(i).Item2));
//                                }
//                                Console.WriteLine("Payment done");
//                                //after the payment is aproved, The order is stored and the products are added to the sells table

//                                bool wasSettled = obj_shoppingCartOperations.setOrder(idCustomer, products);
//                                if (wasSettled)
//                                {
//                                    obj_shoppingCart.ProductList.Clear();
//                                    Console.WriteLine("Order was settled and will be send between 2 days");
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Error seting the order, the payment will be return as soon as possible");
//                                }
//                            }
//                            else
//                            {
//                                Console.WriteLine("Failed to process the transaction");
//                            }
//                        }
//                        else
//                        {
//                            Console.WriteLine("There are no products in the shopping cart");
//                        }


//                        break;
//                    case "2":
//                        Console.Clear();

//                        if (obj_shoppingCart.ProductList.Count() >= 1)
//                        {
//                            showShoppingCart(ref obj_shoppingCart);
//                            Console.WriteLine("----------------------------------------------------");
//                            Console.WriteLine("Insert the id of the product to be deleted from the cart");
//                            string id = Console.ReadLine();
//                            bool wasRemoved = obj_shoppingCart.DeleteProductFromList(id);
//                            if (wasRemoved)
//                            {
//                                Console.WriteLine("Item removed succesfully from the shopping cart");
//                            }
//                            else
//                            {
//                                Console.WriteLine("Could not find the item");
//                            }

//                        }
//                        else
//                        {
//                            Console.WriteLine("There are no items in the shopping cart");
//                        }


//                        break;
//                    default:
//                        break;
//                }

//            } while (option != "3");

//        }

//        public static bool MakePayment(ShoppingCart obj_shoppingCart)
//        {
//            float total = 0;
//            for (int i = 0; i < obj_shoppingCart.ProductList.Count; i++)
//            {
//                total = total + obj_shoppingCart.ProductList.ElementAt(i).Item1.Price * obj_shoppingCart.ProductList.ElementAt(i).Item2;
//            }


//            Console.WriteLine("the third party payment gateway is called ");

//            return true;

//        }

//    }


//}
