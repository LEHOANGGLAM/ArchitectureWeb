using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using BlueDataWeb.Models.Entites;
using System.Security.Policy;

namespace BlueDataWeb.Models.Service
{
    public class EmailService
    {
        public BlueDataWebEntities db = new BlueDataWebEntities();


        public bool SendEmail(List<string> Listmailto, string Subject, string emalBCC, string messageBody, string emailGoogle, string emailGooglePass)
        {
            SendEmail sendemail = db.SendEmails.Where(m => m.Name == "EmailThongBao").FirstOrDefault();

            bool result = true;

            #region Send mail

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            foreach (var item in Listmailto)
            {
                message.To.Add(new MailAddress(item)); //recipient
            }
             
            message.Bcc.Add(new MailAddress("lequocviet4990@gmail.com"));
            message.CC.Add(new MailAddress(emalBCC));
        


            message.BodyEncoding = Encoding.UTF8;
            message.Subject = Subject;
            message.From = new System.Net.Mail.MailAddress(emailGoogle); //from email

            message.Body = messageBody;

            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");// you need an smtp server address to send emails
            smtp.Credentials = new System.Net.NetworkCredential(emailGoogle, emailGooglePass); //missing line from ur code

             
            smtp.Send(message);

            #endregion Send mail

            return result;
        }


        //send email đơn giản
        public void SendEmail1(string Emailto, string Emailfrom, string subject, string body, string passmail, string servermail)
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(Emailto); //recipient
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress(Emailfrom); //from email

            message.Body = "<html><body><font face='Tahoma,Arial,sans-serif' size='-1'>";
            message.Body += body;
            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(servermail);// you need an smtp server address to send emails
            smtp.Credentials = new System.Net.NetworkCredential(Emailfrom, passmail); //missing line from ur code
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }

        //send email có BCC, CC
        public void SendEmail2(string Emailto, string Emailfrom, string subject, string body, string passmail, string servermail)
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(Emailto); //recipient
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress(Emailfrom); //from email

            message.Body = "<html><body><font face='Tahoma,Arial,sans-serif' size='-1'>";
            message.Body += body;
            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(servermail);// you need an smtp server address to send emails
            smtp.Credentials = new System.Net.NetworkCredential(Emailfrom, passmail); //missing line from ur code
            smtp.Send(message);
        }
        public bool SendMail_By_Gmail(string to, string strSubject, string strContent)
        {
            SendEmail sendemail = db.SendEmails.Where(m => m.Name == "PassRestore").FirstOrDefault();
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            //mail.Bcc.Add(new MailAddress("aaaaaa@gmail.com"));
            mail.Subject = strSubject;
            mail.IsBodyHtml = true;
            mail.Body = strContent;
            mail.From = new MailAddress(sendemail.Emailfrom);
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = sendemail.Servermail;
            client.Port = int.Parse(sendemail.Port.ToString());
            // khai báo tài khoản và mat khẩu gừi mail
            NetworkCredential credentials = new NetworkCredential(sendemail.Emailfrom, sendemail.Passmail);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            client.Send(mail); // thuc hien gui mail
            return true;


        }
        public bool SendMail_By_Gmail2(string to, string strSubject, string strContent)
        {
            SendEmail sendemail = db.SendEmails.Where(m => m.Name == "Newsletter").FirstOrDefault();
            SubEmail sub = db.SubEmails.FirstOrDefault();
            MailMessage mail = new MailMessage();
            mail.To.Add(sub.Email);
            // mail.Bcc.Add(new MailAddress(to));
            mail.Subject = strSubject;
            mail.IsBodyHtml = true;
            mail.Body = strContent;
            mail.From = new MailAddress(sendemail.Emailfrom);
            mail.Bcc.Add(to);
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = sendemail.Servermail;
            client.Port = int.Parse(sendemail.Port.ToString());
            // khai báo tài khoản và mat khẩu gừi mail
            NetworkCredential credentials = new NetworkCredential(sendemail.Emailfrom, sendemail.Passmail);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            client.EnableSsl = true;
            client.Send(mail); // thuc hien gui mail
            return true;


        }
        public void SendEmail(SendEmail sendemail, string emailto)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            var lstproducts = db.Products.ToList();

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(emailto); //recipient
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = sendemail.Subject;
            message.From = new System.Net.Mail.MailAddress(sendemail.Emailfrom); //from email

            //message.Body = "<html><body><font face='Tahoma,Arial,sans-serif' size='-1'>";
            message.Body = "";
            string body2 = "<div style='float:left;width:311px;min-height:330px;padding-top:5px;background:#fff;margin-bottom:10px'>";
            body2 += "<table style='margin-bottom:15px' border='0' cellpadding='0' cellspacing='0' width='100%'>";
            body2 += "<tbody>";
            body2 += " <tr>";
            body2 += "<td style='padding:5px 5px 0px 10px;height:40px'>";
            body2 += "<a title='{2}' href='http://dealtop1.com/{3}' style='text-decoration:none;font-size:14px;color:#0a84c1;font-weight:bold' target='_blank'>{2} <span style='color:#5baa00'>";
            body2 += " </span></a>";
            body2 += "</td>";
            body2 += "</tr>";
            body2 += " <tr>";
            body2 += "<td style='padding:5px 0px 5px 10px'>";
            body2 += "<div style='float:left'>";
            body2 += "<a title='{2}' href='http://dealtop1.com/{3}' style='text-decoration:none' target='_blank'>";
            //body2 += " <img title='{2}' style='display:block;height:235px;' alt='{2}' src='http://dealtop1.com/Images/Products/Đèn Pin Tự vệ Siêu Sáng ULTRAFIRE635188308225817183.jpg'";
            body2 += " <img title='{2}' style='display:block;height:235px;' alt='{2}' src=";

            body2 += "'http://dealtop1.com/Images/Products/{0}'";
            body2 += " height='200px' width='291'>";
            body2 += "</a>";
            body2 += "</div>";
            body2 += " </td>";
            body2 += "</tr>";
            body2 += "<tr>";
            body2 += "<td style='padding:5px 10px 5px 10px' align='right'>";
            body2 += "<span style='font-size:15px'>Giá:</span> <span style='color:#968909;text-align:right;font-size:18px;font-weight:bold'>{1} VND</span>";
            body2 += "<div style='padding-top:10px'>";
            body2 += "<a title'{2}' href='http://dealtop1.com/{3}' style='text-decoration:none;text-align:center;color:#fff;border-radius:4px 4px 4px 4px;width:70px;background-color:#6EBC0E;padding:5px' target='_blank'>";
            body2 += "<span>Xem chi tiết &gt;</span> </a>";
            body2 += "</div>";
            body2 += "</td>";
            body2 += "</tr>";
            body2 += "</tbody>";
            body2 += "</table>";
            body2 += "</div>";
            //body2 += "{4}";
            //message.Body += body2;
            string body3 = "";
            string url = "";
            string unsub = "";
            //string test = "http://dealtop1.com/Images/Products/test chơi thôi mà";
            var cate1 = db.Categories.Where(m => m.ParentCategoryID == 0).ToList();
            foreach (var c in cate1)
            {
                var protest = GetProdcut(c.CategoryID);
                foreach (var pro in protest)
                {
                    string productImage = pro.ImagePath;
                    url = BlueDataWeb.Utility.HelperSEO.ToSeoUrl(pro.Category.CategoryName) + "/" + BlueDataWeb.Utility.HelperSEO.ToSeoUrl(pro.ProductName) + "-" + pro.ProductID;
                    body3 += string.Format(body2, productImage, pro.UnitPriceNew, pro.ProductName.Replace("+"," "), url);
                }
            }
            SubEmail subtest = db.SubEmails.Where(m => m.Email == emailto).FirstOrDefault();
            User usertest = db.Users.Where(m => m.Email == emailto).FirstOrDefault();
            if (subtest != null)
            {
                unsub = subtest.CodeActive;

                message.Body += string.Format(sendemail.Body, body3, emailto, unsub);

            }
            else
            {
                unsub = usertest.CodeActive;

                message.Body += string.Format(sendemail.Body, body3, emailto, unsub);


            }
            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(sendemail.Servermail);// you need an smtp server address to send emails
            smtp.Credentials = new System.Net.NetworkCredential(sendemail.Emailfrom, sendemail.Passmail); //missing line from ur code
            smtp.Send(message);
        }
        public void SendEmailPass(SendEmail sendemail, string emailto, string Body)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            var lstproducts = db.Products.ToList();
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(emailto); //recipient
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = sendemail.Subject;
            message.From = new System.Net.Mail.MailAddress(sendemail.Emailfrom); //from email

            message.Body = Body;

            //message.Body += string.Format(sendemail.Body, product.ImagePath, product.ProductID, product.ProductName, product.UnitPriceNew, product.UnitPrice);//, product.ProductID, product.UnitPriceNew, product.UnitPrice, product.ProductName);
            message.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(sendemail.Servermail);// you need an smtp server address to send emails
            smtp.Credentials = new System.Net.NetworkCredential(sendemail.Emailfrom, sendemail.Passmail); //missing line from ur code
            smtp.Send(message);
        }
        public List<Product> GetProdcut(int id)
        {
            BlueDataWebEntities db = new BlueDataWebEntities();
            List<Category> catelist = new List<Category>();
            List<Product> productlist = new List<Product>();
            //var catelevel1 = db.Categories.Where(m => m.ParentCategoryID == 0).ToList();        
            var catelevel1 = db.Categories.Where(m => m.CategoryID == id).FirstOrDefault();
            catelist.Add(catelevel1);
            var sub2 = db.Categories.Where(m => m.ParentCategoryID == catelevel1.CategoryID).ToList();
                if (sub2.Count != 0)
                {
                    foreach (var s2 in sub2)
                    {
                        catelist.Add(s2);
                        var s3 = db.Categories.Where(m => m.ParentCategoryID == s2.CategoryID).ToList();
                        if (s3.Count != 0)
                        {
                            foreach (var sub3 in s3)
                            {
                                catelist.Add(sub3);
                            }
                        }
                    }


                }
            

                //var catelevel1 = db.Categories.Where(m => m.CategoryID == id).ToList();
                //foreach (var cate1 in catelevel1)
                //{
                //    catelist.Add(cate1);
                //}
                //foreach (var s2 in sub2)
                //{
                //    var s3 = db.Categories.Where(m => m.ParentCategoryID == s2.CategoryID).ToList();
                //    catelist.Add(s2);
                //    foreach (var sub3 in s3)
                //    {
                //        catelist.Add(sub3);
                //    }

                //}
                
                foreach (var cate in catelist)
                {
                    var product = db.Products.Where(m => m.CategoryID == cate.CategoryID).ToList();
                    foreach (var pro in product)
                    {
                        productlist.Add(pro);
                    }
                }
            
            var protest = productlist.OrderByDescending(m => m.DateCreated).Take(2).ToList();
            return protest;
        }
        public string bodyorderdetail(int Orderid, BlueDataWebEntities db, decimal? total, decimal? ship)
        {
            decimal sumorder = total.Value + ship.Value;
            var lstorderdetail = db.OrderDetails.Where(m => m.OrderId == Orderid).ToList();
            var lstclor = db.Colors.Include(m=>m.ProductColor);
            string body = "<table width='650' cellspacing='0' cellpadding='0' border='0' style='border: 1px solid #eaeaea'>";
            body += "<thead>";
            body += "<tr>";
            body += "<th bgcolor='#CE4646' align='left' style='font-size: 13px; color:#fff; padding: 3px 9px'>Mặt hàng</th>";
            body += " <th bgcolor='#CE4646' align='left' style='font-size:13px;padding:3px 9px'></th>";
            body += "<th bgcolor='#CE4646' align='center' style='font-size: 13px; color:#fff; padding: 3px 9px'>Số lượng</th>";
            body += " <th bgcolor='#CE4646' align='right' style='font-size: 13px; color:#fff; padding: 3px 9px'>Tổng tiền</th>";
            body += "</tr>";
            body += "</thead>";
            foreach (var item in lstorderdetail)
            {
                Color color = lstclor.Where(m => m.Id == item.ColorId).FirstOrDefault();
                body += "<tr>";
                body += " <td valign='top' align='left' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>";
                body += "<strong style='font-size: 11px'>" + item.Product.ProductName + "</strong>";
                body += "<dl style='margin: 0; padding: 0'>";
                body += "<dt><strong><em>Màu sắc hoặc kiểu dáng</em></strong></dt>";
                if (item.ColorId != null)
                {
                    body += "<dd style='margin: 0; padding: 0 0 0 9px'>" + color.ProductColor.Name + "</dd>";
                }
                body += "</dl>";
                body += "</td>";
                body += "<td valign='top' align='left' style='font-size:11px;padding:3px 9px;border-bottom:1px dotted #cccccc'></td>";
                body += "<td valign='top' align='center' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>" + item.Quantity + "</td>";
                body += "<td valign='top' align='right' style='font-size: 11px; padding: 3px 9px; border-bottom: 1px dotted #cccccc'>";
                body += "<span>" + string.Format("{0:0,0 đ}", item.UnitPrice) + "</span>";
                body += "</td>";
                body += "</tr>";
            }
            body += "<tr>";
            body += "<td align='right' style='padding: 3px 9px' colspan='3'>Tổng tiền</td>";
            body += "<td align='right' style='padding: 3px 9px'>";
            body += "<span>" + string.Format("{0:0,0 đ}", total) + "</span></td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td align='right' style='padding: 3px 9px' colspan='3'>Phí vận chuyển</td>";
            body += "<td align='right' style='padding: 3px 9px'>";
            body += "<span>" + string.Format("{0:0,0 đ}", ship) + "</span>                    </td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td align='right' style='padding: 3px 9px' colspan='3'>";
            body += "<strong>Tổng cộng</strong>";
            body += "</td>";
            body += "<td align='right' style='padding: 3px 9px'>";
            body += "<strong><span>" + string.Format("{0:0,0 đ}", sumorder) + "</span></strong>";
            body += "</td>";
            body += "</tr>";
            body += " </table>";

            return body;
        }
    }
}