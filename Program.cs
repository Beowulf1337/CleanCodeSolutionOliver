using System;

public class Order
{
    required public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }


    // sets constant values for when the discounts should be applied and how much these discounts saves on the purchase
    private const int HighQuantityThreshold = 100;
    private const int MediumQuantityThreshold = 50;
    private const double HighQuantityDiscountRate = 0.10;
    private const double MediumQuantityDiscountRate = 0.05;



    public void ProcessOrder()
    {
        if (!IsValidOrder())
        {
            Console.WriteLine("Invalid order! Please ensure product name is not empty and quantity is greater than zero.");
            return;
        }

        double discount = CalculateDiscount();
        double totalPrice = CalculateTotalPrice(discount);

        PrintOrderSummary(discount, totalPrice);
    }

    private bool IsValidOrder()
    {
        return Quantity > 0 && !string.IsNullOrWhiteSpace(ProductName);
    }

    private double CalculateDiscount()
    {
        if (Quantity > HighQuantityThreshold)
            return UnitPrice * Quantity * HighQuantityDiscountRate;
        
        if (Quantity > MediumQuantityThreshold)   
            return UnitPrice * Quantity * MediumQuantityDiscountRate;
     
        return 0;
    }

    private double CalculateTotalPrice(double discount)
    {
        return (UnitPrice * Quantity) - discount;
    }

    // using F2 to make sure it only display up to 2 decimals
    private void PrintOrderSummary(double discount, double totalPrice)
    {
        Console.WriteLine($"Processing order for {Quantity} {ProductName}(s) at ${UnitPrice:F2} each.");
        if (discount > 0)
        {
            Console.WriteLine($"Discount applied: ${discount:F2}");
        }
        Console.WriteLine($"Total price: ${totalPrice:F2}");
    }
}

// made its own class in case the need for more shipping logic comes up in the future
public class ShippingService
{
    public void ShipOrder()
    {
        Console.WriteLine("Order has been shipped!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Order order = new Order
            {
                ProductName = "Laptop",
                Quantity = 120,
                UnitPrice = 899.99
            };

            order.ProcessOrder();

            ShippingService shippingService = new ShippingService();
            shippingService.ShipOrder();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
