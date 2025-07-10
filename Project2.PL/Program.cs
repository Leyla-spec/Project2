using System.ComponentModel.Design;
using Project2.BL.Services.Concretes;
using Project2.Core.Enumns;
using Project2.Core.Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;

MenuItemService menuService = new();
OrderService orderService = new();

while (true)
{
    Console.WriteLine("\n=== Əsas Menu ===");
    Console.WriteLine("1. Menu üzərində əməliyyat aparmaq");
    Console.WriteLine("2. Sifarişlər üzərində əməliyyat aparmaq");
    Console.WriteLine("0. Çıxış");
    Console.Write("Seçim edin: ");
    string? secim = Console.ReadLine();

    if (secim == "1")
    {
        MenuOperations();
    }
    else if (secim == "2")
    {
        OrderOperations();
    }
    else if (secim == "0")
    {
        break;
    }
}

void MenuOperations()
{
    while (true)
    {
        Console.WriteLine("\n--- Menu Əməliyyatları ---");
        Console.WriteLine("1. Yeni item əlavə et");
        Console.WriteLine("2. Item üzərində düzəliş et");
        Console.WriteLine("3. Item sil");
        Console.WriteLine("4. Bütün item-ları göstər");
        Console.WriteLine("0. Əvvəlki menyuya qayıt");
        Console.Write("Seçim edin: ");
        string? secim = Console.ReadLine();

        if (secim == "1")
        {
            Console.Write("Ad: ");
            string? name = Console.ReadLine();
            Console.Write("Qiymət: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Kateqoriya: ");
            string? categoryInput = Console.ReadLine();

            if (Enum.TryParse<Categories>(categoryInput, true, out Categories category))
            {
                menuService.AddMenuItem(new MenuItem { Name = name, Price = price, Category = category });
                Console.WriteLine("Əlavə edildi.");
            }
            else
            {
                Console.WriteLine("Düzgün kateqoriya daxil edilməyib.");
            }
        }
        else if (secim == "2")
        {
            Console.Write("Item ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Yeni ad: ");
            string? name = Console.ReadLine();
            Console.Write("Yeni qiymət: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var menu = menuService.GetMenuById(id);
            menu.Name = name;
            menu.Price = price;

            menuService.UpdateMenuItem(menu);
            Console.WriteLine("Düzəliş edildi.");
        }
        else if (secim == "3")
        {
            Console.Write("Silinəcək item ID: ");
            int id = int.Parse(Console.ReadLine());
            menuService.DeleteMenuItem(id);
            Console.WriteLine("Silindi.");
        }
        else if (secim == "4")
        {
            foreach (var item in menuService.GetAll())
                Console.WriteLine($"{item.Id} - {item.Name} - {item.Category} - {item.Price} AZN");
        }
        else if (secim == "0")
        {
            break;
        }
    }
}

void OrderOperations()
{
    while (true)
    {
        Console.WriteLine("\n--- Sifariş Əməliyyatları ---");
        Console.WriteLine("1. Yeni sifariş əlavə et");
        Console.WriteLine("2. Sifarişi sil");
        Console.WriteLine("3. Bütün sifarişləri göstər");
        Console.WriteLine("4. Tarix intervalına görə sifarişlər");
        Console.WriteLine("5. Məblağ intervalına görə sifarişlər");
        Console.WriteLine("6. Müəyyən tarixdə olan sifarişlər");
        Console.WriteLine("7. Nömrəyə görə sifariş");
        Console.WriteLine("0. Əvvəlki menyuya qayıt");
        Console.Write("Seçim edin: ");
        string? secim = Console.ReadLine();

        if (secim == "1")
        {
            List<OrderItem> items = new();
            while (true)
            {
                Console.Write("Menu ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Say: ");
                int count = int.Parse(Console.ReadLine());
                var menu = menuService.GetMenuById(id);
                items.Add(new OrderItem { MenuItem = menu, Count = count });

                Console.Write("Başqa məhsul əlavə etmək istəyirsiniz? (h/y): ");
                if (Console.ReadLine().ToLower() != "h") break;
            }

            var total = items.Sum(i => i.Count * i.MenuItem.Price);
            Orders order = new() { OrderItem = items, OrderDate = DateTime.Now, TotalPrice = total };
            orderService.AddOrder(order);
            Console.WriteLine("Sifariş əlavə edildi.");
        }
        else if (secim == "2")
        {
            Console.Write("Silinəcək sifariş ID: ");
            int id = int.Parse(Console.ReadLine());
            orderService.DeleteOrder(id);
            Console.WriteLine("Sifariş silindi.");
        }
        else if (secim == "3")
        {
            foreach (var o in orderService.GetAllOrders())
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
        }
        else if (secim == "4")
        {
            Console.Write("Başlanğıc tarix (yyyy-MM-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.Write("Son tarix (yyyy-MM-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine());
            foreach (var o in orderService.GetOrdersByDatesInterval(start, end))
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
        }
        else if (secim == "5")
        {
            Console.Write("Minimum məbləğ: ");
            decimal min = decimal.Parse(Console.ReadLine());
            Console.Write("Maksimum məbləğ: ");
            decimal max = decimal.Parse(Console.ReadLine());
            foreach (var o in orderService.GetOrdersByPriceInterval(min, max))
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
        }
        else if (secim == "6")
        {
            Console.Write("Tarix (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            foreach (var o in orderService.GetOrderByDate(date))
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
        }
        else if (secim == "7")
        {
            Console.Write("Sifariş ID: ");
            int id = int.Parse(Console.ReadLine());
            var o = orderService.GetOrderById(id);
            Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
            foreach (var i in o.OrderItem)
                Console.WriteLine($"\t{i.MenuItem.Name} - {i.Count} ədəd");
        }
        else if (secim == "0")
        {
            break;
        }
    }
}
