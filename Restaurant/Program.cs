using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.DTOS;
using Restaurant.Core.Services;
using Restaurant.Services;
using Restaurant.Repositories;
using Restaurant.Core.Repositories;

var serviceCollection = new ServiceCollection();
ConfigureService(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

void ConfigureService(ServiceCollection serviceCollection)
{
    serviceCollection.AddScoped<IReservationService, ReservationService>();
    serviceCollection.AddScoped<IOrderService, OrderService>();
    serviceCollection.AddScoped<ICustomerService, CustomerService>();
    serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
    serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
    serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
}

Console.WriteLine("--------------------------------------");
Console.WriteLine("  <> RESTAURANT MANAGEMENT SYSTEM <> ");
Console.WriteLine("--------------------------------------");
Console.WriteLine("1. Manage Orders");
Console.WriteLine("2. Manage Reservations");
Console.WriteLine("3. Manage Customers");
Console.WriteLine("4. Leave");
var w = Convert.ToInt32(Console.ReadLine());
while (w != 4)
{
    if (w == 1)
    {
        Console.WriteLine("Enter the number of the following options in order management");
        Console.WriteLine("1. Add Order");
        Console.WriteLine("2. Edit Order");
        Console.WriteLine("3. Remove Order");
        Console.WriteLine("4. Get All Orders");
        Console.WriteLine("5. Exit");
        int num = Convert.ToInt32(Console.ReadLine());
        switch (num)
        {
            case 1:
                {
                    var menu = 0;
                    var prices = 0;
                    do
                    {
                        Console.WriteLine("Enter the number of the following items to order");
                        Console.WriteLine("1. Chicken Karahi, Price: Rs 600");
                        Console.WriteLine("2. Mandi Pulao Platter, Price: Rs 2000");
                        Console.WriteLine("3. Chicken Soup, Price: Rs 100");
                        Console.WriteLine("4. Kheer, Price: Rs 150");
                        Console.WriteLine("5. Exit");
                        menu = Convert.ToInt32(Console.ReadLine());
                        if (menu == 1) prices += 600;
                        else if (menu == 2) prices += 2000;
                        else if (menu == 3) prices += 100;
                        else if (menu == 4) prices += 150;

                        Console.WriteLine("Do you want to enter more items?");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        menu = Convert.ToInt32(Console.ReadLine());
                        if (menu == 2)
                        {
                            menu = 5;
                            break;
                        }
                    } while (menu < 5);

                    var order = new OrderDto();
                    var orderServices = serviceProvider.GetService<IOrderService>();
                    if (orderServices == null)
                    {
                        Console.WriteLine("IOrderService is not registered or cannot be resolved.");
                    }

                    order.Bill = prices;
                    await orderServices.AddOrderAsync(order);
                    Console.WriteLine("Your bill is : " + order.Bill);
                    break;
                }
            case 2:
                {
                    var orderServices = serviceProvider.GetService<IOrderService>();
                    Console.WriteLine("Give ID of the order to edit");
                    int n = Convert.ToInt32(Console.ReadLine());
                    var order = await orderServices.GetOrderByIdAsync(n);

                    if (order == null)
                    {
                        Console.WriteLine("Order not found");
                        break; // Add break here to exit the case if order is not found
                    }
                    else
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("<> Order Searched is <>");
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine($"Order ID: {order.Id}");
                        Console.WriteLine($"Bill: {order.Bill}");
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Enter updated data");
                        Console.WriteLine("------------------------------------");

                        var menu = 0;
                        var prices = 0;
                        do
                        {
                            Console.WriteLine("Enter the number of the following items to order");
                            Console.WriteLine("1. Chicken Karahi, Price: Rs 600");
                            Console.WriteLine("2. Mandi Pulao Platter, Price: Rs 2000");
                            Console.WriteLine("3. Chicken Soup, Price: Rs 100");
                            Console.WriteLine("4. Kheer, Price: Rs 150");
                            Console.WriteLine("5. Exit");
                            menu = Convert.ToInt32(Console.ReadLine());

                            if (menu == 1) prices += 600;
                            else if (menu == 2) prices += 2000;
                            else if (menu == 3) prices += 100;
                            else if (menu == 4) prices += 150;

                            Console.WriteLine("Do you want to enter more items?");
                            Console.WriteLine("1. Yes");
                            Console.WriteLine("2. No");
                            menu = Convert.ToInt32(Console.ReadLine());

                            if (menu == 2)
                            {
                                menu = 5;
                                break;
                            }
                        } while (menu < 5);

                        // Update the order's bill with the new calculated prices
                        order.Bill = prices;
                        await orderServices.UpdateOrderAsync(order);
                    }

                    break; // Ensure break is here to terminate the case properly
                }

            case 3:
                {
                    var orderServices = serviceProvider.GetService<IOrderService>();

                    Console.WriteLine("Give ID of the order to delete");
                    int n = Convert.ToInt32(Console.ReadLine());
                    await orderServices.DeleteOrderAsync(n);
                    break;
                }
            case 4:
                {
                    var orderServices = serviceProvider.GetService<IOrderService>();
                    await orderServices.DisplayAllOrdersAsync();
                    break;
                }
        }
    }

    if (w == 2)
    {
        Console.WriteLine("Welcome to Reservation Management");
        Console.WriteLine("Enter the number of the following options in reservation management");
        Console.WriteLine("1. Add Reservation");
        Console.WriteLine("2. Edit Reservation");
        Console.WriteLine("3. Remove Reservation");
        Console.WriteLine("4. Get All Reservations");
        Console.WriteLine("5. Exit");
        int num = Convert.ToInt32(Console.ReadLine());
        switch (num)
        {
            case 1:
                {
                    var reservationServices = serviceProvider.GetService<IReservationService>();

                    Console.WriteLine("Enter Customer ID:");
                    int customerId;
                    if (int.TryParse(Console.ReadLine(), out customerId))
                    {
                        Console.WriteLine("Enter Table Number:");
                        int tableNo;
                        if (int.TryParse(Console.ReadLine(), out tableNo))
                        {
                            DateTime dateTime = DateTime.Now;

                            await reservationServices.AddReservationAsync(customerId, tableNo, dateTime);

                            Console.WriteLine("Reservation added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Table Number");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Customer ID");
                    }

                    break;
                }


            case 2:
                {
                    var reservationServices = serviceProvider.GetService<IReservationService>();
                    Console.WriteLine("Give ID of the reservation to edit");
                    int reservationId = Convert.ToInt32(Console.ReadLine());
                    var reservation = await reservationServices.GetReservationByIdAsync(reservationId);

                    if (reservation == null)
                    {
                        Console.WriteLine("Reservation not found.");
                    }
                    else
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("<> Reservation Searched is <>");
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine($"Reservation ID: {reservation.Id}");
                        Console.WriteLine($"Customer ID: {reservation.CustomerDtoId}"); // Ensure this matches your property
                        Console.WriteLine($"Table Number: {reservation.Tableno}");
                        Console.WriteLine($"Date of Reservation: {reservation.DateOfReserve}");
                        Console.WriteLine("------------------------------------");

                        // Ask for a new table number
                        Console.WriteLine("Enter new Table Number:");
                        int newTableNo;
                        if (!int.TryParse(Console.ReadLine(), out newTableNo))
                        {
                            Console.WriteLine("Invalid Table Number. Reservation not updated.");
                            break;
                        }

                        // Ask for a new reservation date
                        Console.WriteLine("Enter new date of reservation (yyyy-mm-dd):");
                        DateTime newDateOfReserve;
                        if (!DateTime.TryParse(Console.ReadLine(), out newDateOfReserve))
                        {
                            Console.WriteLine("Invalid date format. Reservation not updated.");
                            break;
                        }

                        // Call the UpdateReservationAsync method to save the changes
                        await reservationServices.UpdateReservationAsync(reservationId, newTableNo, newDateOfReserve);

                        Console.WriteLine("Reservation updated successfully.");
                    }
                    break;
                }





            case 3:
                {
                    var reservationServices = serviceProvider.GetService<IReservationService>();
                    Console.WriteLine("Give ID of the reservation to delete");
                    int n = Convert.ToInt32(Console.ReadLine());
                    await reservationServices.RemoveReservationAsync(n);
                    break;
                }
            case 4:
                {
                    var reservationServices = serviceProvider.GetService<IReservationService>();
                    await reservationServices.DisplayAllReservations();
                    break;
                }
        }
    }

    if (w == 3) // Manage Customers
    {
        Console.WriteLine("Welcome to Customer Management");
        Console.WriteLine("Enter the number of the following options in customer management");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Exit");
        int num = Convert.ToInt32(Console.ReadLine());
        switch (num)
        {
            case 1:
                {
                    var customerService = serviceProvider.GetService<ICustomerService>();

                    Console.WriteLine("Enter Customer Name:");
                    string customerName = Console.ReadLine();

                    Console.WriteLine("Enter Customer Cell Number:");
                    int cellNumber;
                    if (int.TryParse(Console.ReadLine(), out cellNumber))
                    {
                        // Create a new customer DTO
                        var customer = new CustomerDto
                        {
                            Name = customerName,
                            Celno = cellNumber // Assigning the cell number
                        };

                        await customerService.AddCustomerAsync(customer);
                        Console.WriteLine("Customer added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Cell Number");
                    }

                    break;
                }

            case 2:
                {
                    // Exit customer management
                    break;
                }
        }
    }


    Console.WriteLine("1. Manage Orders");
    Console.WriteLine("2. Manage Reservations");
    Console.WriteLine("3. Manage Customers");
    Console.WriteLine("4. Leave");
    w = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine("Thank you for visiting...");
Console.WriteLine("Have a nice day...");
