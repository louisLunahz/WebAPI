using APIOnlineShop.Exceptions;
using LouigisSP.BO;
using LouigisSP.SL;
using APIOnlineShop.BO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIOnlineShop.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpPost]
        [Route("api/payment/makePayment")]
        [Authorize]
        public IHttpActionResult makePayment(Card card)
        {
            int id_currentPerson = -1;
            if (card is null)
                throw new ArgumentNullException("No argument was send");
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();
            ShoppingCartOperations obj_shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> personCarts = obj_shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTableWithAtLeastOneItem(id_currentPerson);
            if (personCarts.Count() < 1)
                throw new ElementNotFoundException();


            if (!pay())
            {
                return BadRequest("could not process the payment");
            }
            card.IdPerson = id_currentPerson;
            CardOperations objCardOperations = new CardOperations();
            objCardOperations.saveCard(card);
            OrderOperations obj_OrderOperations = new OrderOperations();
            obj_OrderOperations.insertOrder(personCarts.ElementAt(0).Id, id_currentPerson);
            return Ok();
        }

        private bool pay()
        {
            return true;
        }

    }
}
