namespace ShopTaskBD
{
    public class CartService
    {
        public void IncreaseQuantity(Cart cartItem)
        {
            cartItem.Count++;
            UpdatePrice(cartItem);
        }

        public bool DecreaseQuantity(Cart cartItem)
        {
            if (cartItem.Count > 1)
            {
                UnUpdatePrice(cartItem);
                cartItem.Count--;
            }
            return cartItem.Count == 0;
        }

        private void UpdatePrice(Cart cartItem)
        {
            cartItem.Price += cartItem.Price / (cartItem.Count - 1);
        }

        private void UnUpdatePrice(Cart cartItem)
        {
            cartItem.Price -= cartItem.Price / cartItem.Count;
        }
    }
}
