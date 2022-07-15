using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Service.Interface;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CIneplex.Web.Controllers
{
    public class OrderController : Controller
    {

       
        public readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this.orderService.getOrderInfo(userId));
        }
        public IActionResult generate(Guid id)
        {
            
            var model = new
            {
                Id = id
            };
            BaseEntity be = new BaseEntity();
            be.id = id;
            var order = orderService.getOrderDetails(be);
         
            var template = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var doc = DocumentModel.Load(template);

            doc.Content.Replace("{{OrderNumber}}", order.id.ToString());
            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach(var item in order.ticketsInOrder)
            {
                sb.Append(item.Ticket.Movie.Title.ToString() +" "+ item.Ticket.Price.ToString()+ " " + item.Quantity.ToString());
                total += item.Quantity * item.Ticket.Price;
            }

            doc.Content.Replace("{{Tickets}}", sb.ToString());

            var stream = new MemoryStream();

            doc.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(),new PdfSaveOptions().ContentType,"ExportInvoicePDF.pdf");
        }

        [HttpGet]
        public IActionResult InvoiceAll()
        {
            string fileName = "Orders.xlsx";
            string contentType = new XmlSaveOptions().ContentType;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheed = workbook.Worksheets.Add("All Orders");

                worksheed.Cell(1, 1).Value = "Order ID";
                worksheed.Cell(1, 2).Value = "User ID";

                List<Order> userOrders = orderService.getOrderList(userId);

                for(var i =1; i <= userOrders.Count ; i++)
                {
                    var item = userOrders[i];

                    worksheed.Cell(i+1, 1).Value = item.id.ToString();
                    worksheed.Cell(i + 1, 2).Value = item.User.Email.ToString();

                    var p = 0;
                    foreach(var ticket in item.ticketsInOrder)
                    {
                        worksheed.Cell(1, p+3).Value = ticket.Ticket.Movie.Ticket.ToString();
                        worksheed.Cell(i + 1, 2).Value = ticket.Ticket.Movie.Ticket.ToString();
                        p++;
                    }
                }

                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }

            
        }
    }
}
