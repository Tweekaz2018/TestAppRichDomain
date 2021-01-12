namespace TestAppRichDomain.Core.Entries
{
    public class Item : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public bool IsAviable { get; private set; }

        private Item()
        {
            //Ef
        }

        public Item(string title, string description, string image, decimal price, bool isAviable)
        {
            Title = title;
            Description = description;
            Image = image;
            Price = price;
            IsAviable = isAviable;
        }

        public void SetAviable()
        {
            IsAviable = true;
        }

        public void SetUbAviable()
        {
            IsAviable = false;
        }
    }
}