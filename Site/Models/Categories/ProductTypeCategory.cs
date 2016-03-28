using EPiServer.DataAbstraction;
using Site.Business.Categories;

namespace Site.Models.Categories
{
    [CategoryType(
        Guid = "4ECF086E-5BC1-4C8A-8AB1-1761DE8C26D9", 
        Name = "Product type1", 
        Description = "Product type")]
    public class ProductTypeCategory : Category
    {

    }

    [CategoryType(
        Guid = "CB401650-E22B-415E-88DC-5EBE073B7432",
        Name = "Jeans",
        Description = "Jeans",
        Parent = typeof(ProductTypeCategory))]
    public class JeansCategory : Category
    {
        
    }

    [CategoryType(
        Guid = "68DB3E60-880A-491E-9155-1F7954222D34",
        Name = "Shirts",
        Description = "Shirts",
        Parent = typeof(ProductTypeCategory))]
    public class ShirtsCategory : Category
    {

    }

    [CategoryType(
        Guid = "60810B30-025F-466D-96B2-EB294777D959",
        Name = "Shirts short sleeve",
        Description = "Shirts short sleeve",
        Parent = typeof(ShirtsCategory))]
    public class ShirtsShortSleeveCategory : Category
    {

    }

    [CategoryType(
        Guid = "8BE625D5-F703-463B-996C-29DB94598945",
        Name = "Shirts long sleeve",
        Description = "Shirts long sleeve",
        Parent = typeof(ShirtsCategory))]
    public class ShirtsLongSleeveCategory : Category
    {

    }
}