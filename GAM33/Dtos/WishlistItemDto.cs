namespace GAM33.Dtos
{
    public class WishlistItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }         // from Products table
        public string PictureUrl { get; set; }   // from Products table
        public decimal Price { get; set; }       // from Products table
        public string Details { get; set; }      // from Products table - add this line
        public int Quantity { get; set; }        // from CartProducts table
    }
}
