using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressBD.Models.Service;

namespace ExpressBD.Models
{
    public partial class ShoppingCart
    {
        ProductService productservice = new ProductService();
        ExpressBDEntities db = new ExpressBDEntities();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product, int? colorid, bool? flag)
        {
            var lstcartItem = db.CartItems;
            CartItem cartItem=null;
            if (colorid != null && flag==true)
            {
                cartItem = lstcartItem.Where(c => c.CartId == ShoppingCartId 
                            && c.ProductId == product.ProductID && c.ColorId == colorid).FirstOrDefault();
            }
            else
            {
                cartItem = lstcartItem.Where(c => c.CartId == ShoppingCartId && c.ProductId == product.ProductID).FirstOrDefault();
                colorid = null;
            }

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = product.ProductID,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                    ColorId = colorid
                };

                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            db.SaveChanges();
        }
        public int UpdateCartCount(int id, int cartCount)
        {
            var cartItem = db.CartItems.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Quantity = cartCount;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return itemCount;
        }
        public int RemoveFromCart(int id)
        {

            var cartItem = db.CartItems.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                //if (cartItem.Count > 1)
                //{
                //    cartItem.Count--;
                //    itemCount = cartItem.Count;
                //}
                //else
                //{
                db.CartItems.Remove(cartItem);
                //}
                // Save changes 
                db.SaveChanges();
            }
            return itemCount;
        }


        public void EmptyCart()
        {
            var cartItems = db.CartItems.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }

            // Save changes
            db.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return db.CartItems.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in db.CartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();

            return count ?? 0;
        }
        public int GetSumcart()
        {
            int? count = (from cartItems in db.CartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            var total = (from cartItems in db.CartItems.AsEnumerable()
                         where cartItems.CartId == ShoppingCartId
                         select (decimal?)(cartItems.Quantity * cartItems.Product.UnitPriceNew)).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            double? orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.UnitPriceNew,
                    Quantity = item.Quantity,
                    ColorId = item.ColorId

                };
                if (item.ColorId == null)
                {
                    productservice.UpdateQuantity(db, item.Product, item.Quantity);
                }
                else
                {
                    productservice.UpdateQuantityColor(db, item.ColorId, item.Quantity);
                }

                // Set the order total of the shopping cart
                orderTotal += (item.Quantity * item.Product.UnitPriceNew);

                db.OrderDetails.Add(orderDetail);

            }

            order.Total = (decimal)orderTotal;

            // Save the order
            db.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            //khi giỏ hàng được khởi tạo thì nó sẽ tạo một sesion cho gio hàng đo, sesion tồn tại đến khi brower bị tắt
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.CartItems.Where(c => c.CartId == ShoppingCartId);

            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }
    }


}