using System.ComponentModel.Design;
using Project2.BL.Services.Concretes;
using Project2.Core.Enumns;
using Project2.Core.Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;

MenuItemService menuService = new();
OrderService orderService = new();

bool isRunning = true;
while (isRunning)
{
    Console.Clear();
    Console.WriteLine("\n=== Əsas Menu ===");
    Console.WriteLine("1. Menu üzərində əməliyyat aparmaq");
    Console.WriteLine("2. Sifarişlər üzərində əməliyyat aparmaq");
    Console.WriteLine("0. Çıxış");
    Console.Write("Seçim edin: ");

    switch (Console.ReadLine())
    {
        case "1":
            MenuOperations();                           
            break;
        case "2":
            OrderOperations();
            break;
        case "0":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Yanlış seçim!");
            Thread.Sleep(1000);
            break;
    }
}

void MenuOperations()
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        Console.WriteLine("\n--- Menu Əməliyyatları ---");
        Console.WriteLine("1. Yeni item əlavə et");
        Console.WriteLine("2. Item üzərində düzəliş et");
        Console.WriteLine("3. Item sil");
        Console.WriteLine("4. Bütün item-ları göstər");
        Console.WriteLine("0. Əvvəlki menyuya qayıt");
        Console.Write("Seçim edin: ");
        switch (Console.ReadLine())
        {
            case "1":
                AddItem();
                break;
            case "2":
                EditItem();
                break;
            case "3":
                DeleteItem();
                break;
            case "4":
                ListItems();
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Yanlış seçim!");
                Thread.Sleep(1000);
                Console.ReadKey();
                break;
        }

        void AddItem()
        {
            Console.Clear();

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

            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }
    }
    void EditItem()
    {
        Console.Clear();
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

        Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
        Console.ReadKey();
    }
    void DeleteItem()
    {
        Console.Clear();
        Console.Write("Silinəcək item ID: ");
        int id = int.Parse(Console.ReadLine());
        menuService.DeleteMenuItem(id);
        Console.WriteLine("Silindi.");

        Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
        Console.ReadKey();
    }
    void ListItems()
    {
        Console.Clear();
        foreach (var item in menuService.GetAll())
            Console.WriteLine($"{item.Id} - {item.Name} - {item.Category} - {item.Price} AZN");

        Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
        Console.ReadKey();
    }
}

void OrderOperations()
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        Console.WriteLine("\n--- Sifariş Əməliyyatları ---");
        Console.WriteLine("1. Yeni sifariş əlavə et");
        Console.WriteLine("2. Sifarişi sil");
        Console.WriteLine("3. Bütün sifarişləri göstər");
        Console.WriteLine("4. Tarix intervalına görə sifarişlər");
        Console.WriteLine("5. Məbləğ intervalına görə sifarişlər");
        Console.WriteLine("6. Müəyyən tarixdə olan sifarişlər");
        Console.WriteLine("7. Nömrəyə görə sifariş");
        Console.WriteLine("0. Əvvəlki menyuya qayıt");
        Console.Write("Seçim edin: ");

        switch (Console.ReadLine())
        {
            case "1":
                AddOrder();
                break;
            case "2":
                DeleteOrder();
                break;
            case "3":
                ListAllOrders();
                break;
            case "4":
                FilterByDateRange();
                break;
            case "5":
                FilterByPriceRange();
                break;
            case "6":
                FilterByExactDate();
                break;
            case "7":
                GetOrderByNumber();
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Yanlış seçim.");
                Thread.Sleep(1000);
                break;
        }

        void AddOrder()
        {
            Console.Clear();
            List<OrderItem> items = new();
            while (true)
            {
                Console.Clear();
                Console.Write("Menu ID: ");
                if (!int.TryParse(Console.ReadLine(), out int menuId))
                {
                    Console.WriteLine("Düzgün ID daxil edin.");
                    continue;
                }

                var menu = menuService.GetMenuById(menuId);
                if (menu == null)
                {
                    Console.WriteLine("Belə bir menyu elementi tapılmadı.");
                    continue;
                }

                Console.Write("Say: ");
                if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                {
                    Console.WriteLine("Düzgün say daxil edin.");
                    continue;
                }

                items.Add(new OrderItem
                {
                    MenuItemId = menuId,
                    Count = count
                });

                Console.Write("Başqa məhsul əlavə etmək istəyirsiniz? (h/y): ");
                string? cavab = Console.ReadLine();
                if (cavab == null || !cavab.Trim().ToLower().StartsWith("h")) break;
            }

            if (items.Count == 0)
            {
                Console.WriteLine("Sifariş boşdur, əlavə edilmədi.");
            }
            decimal total = items.Sum(i =>
            {
                var menu = menuService.GetMenuById(i.MenuItemId);
                return i.Count * menu.Price;
            });

            Orders order = new()
            {
                OrderDate = DateTime.Now,
                TotalPrice = total,
                OrderItem = items
            };

            orderService.AddOrder(order);
            Console.WriteLine("Sifariş əlavə olundu.");
            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }

        void DeleteOrder()
        {
            Console.Clear();
            Console.Write("Silinəcək sifariş ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    orderService.DeleteOrder(id);
                    Console.WriteLine("Sifariş silindi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Düzgün ID daxil edin.");
            }
        }

        void ListAllOrders()
        {
            Console.Clear();
            var orders = orderService.GetAllOrders();
            if (orders.Count == 0)
            {
                Console.WriteLine("Heç bir sifariş tapılmadı.");
                return;
            }

            foreach (var o in orders)
            {
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");

                foreach (var item in o.OrderItem)
                {
                    Console.WriteLine($"   {item.MenuItem.Name} - Say: {item.Count}");
                }
            }

            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }

        void FilterByDateRange()
        {
            Console.Clear();
            Console.Write("Başlanğıc tarix (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
            {
                Console.WriteLine("Düzgün başlanğıc tarixi daxil edin.");
                return;
            }

            Console.Write("Son tarix (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
            {
                Console.WriteLine("Düzgün son tarixi daxil edin.");
                return;
            }

            try
            {
                var orders = orderService.GetOrdersByDatesInterval(start, end);
                foreach (var o in orders)
                    Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }

        void FilterByPriceRange()
        {
            Console.Clear();
            Console.Write("Minimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal min))
            {
                Console.WriteLine("Düzgün məbləğ daxil edin.");
                return;
            }

            Console.Write("Maksimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal max))
            {
                Console.WriteLine("Düzgün məbləğ daxil edin.");
                return;
            }

            var filteredOrders = orderService.GetOrdersByPriceInterval(min, max);
            if (filteredOrders.Count == 0)
            {
                Console.WriteLine("Bu məbləğ aralığında sifariş tapılmadı.");
                return;
            }

            foreach (var o in filteredOrders)
            {
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
            }
            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }

        void FilterByExactDate()
        {
            Console.Clear();
            Console.Write("Tarix (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Düzgün tarix daxil edin.");
                return;
            }

            var filteredOrders = orderService.GetOrderByDate(date);
            if (filteredOrders.Count == 0)
            {
                Console.WriteLine("Bu tarixdə sifariş tapılmadı.");
                return;
            }

            foreach (var o in filteredOrders)
            {
                Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
            }
            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }

        void GetOrderByNumber()
        {
            Console.Clear();
            Console.Write("Sifariş ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var o = orderService.GetOrderById(id);
                    Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
                    foreach (var i in o.OrderItem)
                    {
                        Console.WriteLine($"\t{i.MenuItem.Name} - {i.Count} ədəd");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Düzgün ID daxil edin.");
            }
            Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
            Console.ReadKey();
        }
    }
}

