using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace xCarpaccio.client
{
    using Nancy;
    using System;
    using Nancy.ModelBinding;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "It works !!! You need to register your server on main server.";

            Post["/order"] = _ =>
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    Console.WriteLine("Order received: {0}", reader.ReadToEnd());
                }

                var order = this.Bind<Order>();

                Bill bill = new Bill();
                //return null;
                bill.total = 0;
                decimal tax = Convert.ToDecimal(1);
                decimal reduc = Convert.ToDecimal(1);

                if (order.Country == "FI")
                {
                    tax = Convert.ToDecimal(1.17);
                }
                else if (order.Country == "SK")
                {
                    tax = Convert.ToDecimal(1.18);
                }
                else if (order.Country == "ES" || order.Country == "CZ")
                {
                    tax = Convert.ToDecimal(1.19);
                }
                else if (order.Country == "DE" || order.Country == "FR" || order.Country == "NL" || order.Country == "EL" ||
                         order.Country == "LV" || order.Country == "MT")
                {
                    tax = Convert.ToDecimal(1.20);
                }
                else if (order.Country == "UK" || order.Country == "PL" || order.Country == "BG" ||
                         order.Country == "DK" || order.Country == "IE" || order.Country == "CY")
                {
                    tax = Convert.ToDecimal(1.21);
                }
                else if (order.Country == "AT" || order.Country == "EE")
                {
                    tax = Convert.ToDecimal(1.22);
                }
                else if (order.Country == "SE" || order.Country == "HR" || order.Country == "LT")
                {
                    tax = Convert.ToDecimal(1.23);
                }
                else if (order.Country == "BE" || order.Country == "SI")
                {
                    tax = Convert.ToDecimal(1.24);
                }
                else if (order.Country == "IT" || order.Country == "LU")
                {
                    tax = Convert.ToDecimal(1.25);
                }
                else if (order.Country == "HU")
                {
                    tax = Convert.ToDecimal(1.27);
                }

                int cpt = order.Prices.Length;
                for (int i = 0; i <= cpt-1; i++)
                {
                    bill.total = bill.total + (order.Prices[i] * order.Quantities[i]);
                }

                bill.total = bill.total* tax;

                if (order.Reduction != "PAY THE PRICE")
                {
                    if (bill.total >= 50000)
                    {
                        reduc = Convert.ToDecimal(0.15);
                    }
                    else if (bill.total >= 10000)
                    {
                        reduc = Convert.ToDecimal(0.10);
                    }
                    else if (bill.total >= 7000)
                    {
                        reduc = Convert.ToDecimal(0.07);
                    }
                    else if (bill.total >= 5000)
                    {
                        reduc = Convert.ToDecimal(0.05);
                    }
                    else if (bill.total >= 1000)
                    {
                        reduc = Convert.ToDecimal(0.03);
                    }

                    bill.total = bill.total*(1 - reduc);
                }
                //Bill bill = null;
                //TODO: do something with order and return a bill if possible
                // If you manage to get the result, return a Bill object (JSON serialization is done automagically)
                // Else return a HTTP 404 error : return Negotiate.WithStatusCode(HttpStatusCode.NotFound);

                if (bill.total == 0)
                {
                    bill = null;
                    return bill;
                }
                else
                {
                     return bill;
                }
            };

            Post["/feedback"] = _ =>
            {
                var feedback = this.Bind<Feedback>();
                Console.Write("Type: {0}: ", feedback.Type);
                Console.WriteLine(feedback.Content);
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}