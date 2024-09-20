namespace FoodStore
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        void UpdateOrderQuantity(int productId, int quantityChange);
    }
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new List<Order>();

        public List<Order> GetAllOrders() => _orders;

        public void AddOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.ProductId == order.ProductId);
            if (existingOrder != null)
            {
                existingOrder.Quantity += order.Quantity;
            }
            else
            {
                _orders.Add(order);
            }
        }

        public void RemoveOrder(Order order)
        {
            _orders.Remove(order);
        }

        public void UpdateOrderQuantity(int productId, int quantityChange)
        {
            var order = _orders.FirstOrDefault(o => o.ProductId == productId);
            if (order != null)
            {
                order.Quantity += quantityChange;
                if (order.Quantity <= 0)
                {
                    _orders.Remove(order);
                }
            }
        }
    }
}
