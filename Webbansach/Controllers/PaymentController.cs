using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;
using PayPal.Api;
namespace Webbansach.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("ProcessOrderPaypal", "SanPhams");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ProcessOrderPaypal", "SanPhams");
            }
            //on successful payment, show success page to user.  
            return RedirectToAction("ProcessOrderPaypal", "SanPhams");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            var a = payment.Execute(apiContext, paymentExecution);
            return a;
        }
        public Payment CreatePayment(APIContext apiContext, string redirectUrl)

        {
            List<Product> cart = (List<Product>)Session["cart"];
            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };
            int sub = 0;
            //Adding Item Details like name, currency, price etc
            foreach(var item in cart)
            {
                itemList.items.Add(new Item()
                {
                    name = item.SanPham.TenSP.ToString(),
                    currency = "USD",
                    price = item.SanPham.GiaKM.ToString(),
                    quantity = item.SoLuong.ToString(),
                    sku = "sku"
                });
                sub = Convert.ToInt32(item.SanPham.GiaKM * item.SoLuong);
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            int ship = 15000;
            int taxx = 10000;
            int discount = 5000;
            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = taxx.ToString(),
                shipping = ship.ToString(),
                subtotal = sub.ToString(),
                shipping_discount = discount.ToString()
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (taxx + ship + sub - discount).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
    }
}