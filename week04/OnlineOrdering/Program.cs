using System;

class Program
{
    static void Main(string[] args)
    {
        // Create the first customer's address and customer.
        Address address1 = new Address(
            "123 Main Street",
            "Dallas",
            "Texas",
            "USA");

        Customer customer1 = new Customer(
            "John Smith",
            address1);

        // Create the first order.
        Order order1 = new Order(customer1);

        order1.AddProduct(new Product(
            "Laptop",
            "P001",
            850.00,
            1));

        order1.AddProduct(new Product(
            "Wireless Mouse",
            "P002",
            25.00,
            2));

        order1.AddProduct(new Product(
            "Keyboard",
            "P003",
            45.00,
            1));

        // Create the second customer's address and customer.
        Address address2 = new Address(
            "15 Adeola Odeku Street",
            "Lagos",
            "Lagos",
            "Nigeria");

        Customer customer2 = new Customer(
            "David Johnson",
            address2);

        // Create the second order.
        Order order2 = new Order(customer2);

        order2.AddProduct(new Product(
            "Monitor",
            "P004",
            300.00,
            2));

        order2.AddProduct(new Product(
            "USB-C Hub",
            "P005",
            40.00,
            1));

        order2.AddProduct(new Product(
            "Webcam",
            "P006",
            75.00,
            1));

        // Display the first order.
        Console.WriteLine("ORDER 1");
        Console.WriteLine();
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}");

        Console.WriteLine();
        Console.WriteLine(new string('-', 40));
        Console.WriteLine();

        // Display the second order.
        Console.WriteLine("ORDER 2");
        Console.WriteLine();
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}