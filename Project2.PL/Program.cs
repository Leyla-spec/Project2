using System.ComponentModel.Design;
using Project2.BL.Services.Concretes;
using Project2.Core.Enumns;
using Project2.Core.Entities;
using Project2.DAL.Contexts;
class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var context = new MenuAndOrderDbContext();
        var menuService = new MenuItemService(context);
        var orderService = new OrderService(context);

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("\n=== Əsas Menu ===");
            Console.WriteLine("1. Menu üzərində əməliyyat aparmaq");
            Console.WriteLine("2. Sifarişlər üzərində əməliyyat aparmaq");
            Console.WriteLine("0. Çıxış");
            Console.Write("Seçim edin: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await MenuOperations(menuService);
                    break;
                case "2":
                    await OrderOperations(orderService, menuService);
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

        static async Task MenuOperations(MenuItemService menuService)
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
                        await AddItem();
                        break;
                    case "2":
                        await EditItem();
                        break;
                    case "3":
                        await DeleteItem();
                        break;
                    case "4":
                        await ListItems();
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
            }

            async Task AddItem()
            {
                Console.Clear();

                Console.Write("Ad: ");
                string? name = Console.ReadLine();

                Console.Write("Qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Qiymət düzgün deyil.");
                    Console.ReadKey();
                    return;
                }

                foreach (var cat in Enum.GetValues(typeof(Categories)))
                {
                    Console.WriteLine($"- {cat}");
                }
                Console.Write("Kateqoriya: ");
                string? categoryInput = Console.ReadLine();

                if (Enum.TryParse<Categories>(categoryInput, true, out Categories category))
                {
                    await menuService.AddMenuItemAsync(new MenuItem { Name = name, Price = price, Category = category });
                    Console.WriteLine("Əlavə edildi.");
                }
                else
                {
                    Console.WriteLine("Düzgün kateqoriya daxil edilməyib.");
                }

                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }

            async Task EditItem()
            {
                Console.Clear();
                Console.Write("Item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Düzgün ID daxil edin.");
                    Console.ReadKey();
                    return;
                }

                var menu = await menuService.GetByIdAsync(id);
                if (menu == null)
                {
                    Console.WriteLine("Item tapılmadı.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Yeni ad: ");
                string? name = Console.ReadLine();
                Console.Write("Yeni qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Qiymət düzgün deyil.");
                    Console.ReadKey();
                    return;
                }
                menu.Name = name;
                menu.Price = price;

                await menuService.UpdateMenuItemAsync(menu);
                await menuService.SaveChangesAsync();

                Console.WriteLine("Düzəliş edildi.");
                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }


            async Task DeleteItem()
            {
                Console.Clear();
                Console.Write("Silinəcək item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID düzgün deyil.");
                    Console.ReadKey();
                    return;
                }

                await menuService.DeleteAsync(id);
                await menuService.SaveChangesAsync();

                Console.WriteLine("Silindi.");
                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }

            async Task ListItems()
            {
                Console.Clear();
                var items = await menuService.GetAllAsync();
                foreach (var item in items)
                    Console.WriteLine($"{item.Id} - {item.Name} - {item.Category} - {item.Price} AZN");

                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }
        }

        static async Task OrderOperations(OrderService orderService, MenuItemService menuService)
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
                        await AddOrder();
                        break;
                    case "2":
                        await DeleteOrder();
                        break;
                    case "3":
                        await ListAllOrders();
                        break;
                    case "4":
                        await FilterByDateRange();
                        break;
                    case "5":
                        await FilterByPriceRange();
                        break;
                    case "6":
                        await FilterByExactDate();
                        break;
                    case "7":
                        await GetOrderByNumber();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim.");
                        Thread.Sleep(1000);
                        break;
                }
            }

            async Task AddOrder()
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

                    var menu = await menuService.GetByIdAsync(menuId);
                    if (menu == null)
                    {
                        Console.WriteLine("Belə bir menyu elementi tapılmadı.");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("Say: ");
                    if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                    {
                        Console.WriteLine("Düzgün say daxil edin.");
                        Console.ReadKey();
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
                    Console.ReadKey();
                    return;
                }

                decimal total = 0;
                foreach (var i in items)
                {
                    var menu = await menuService.GetByIdAsync(i.MenuItemId);
                    total += i.Count * menu.Price;
                }

                Orders order = new()
                {
                    OrderDate = DateTime.Now,
                    TotalPrice = total,
                    OrderItem = items
                };

                await orderService.AddAsync(order);
                await orderService.SaveChangesAsync();
                Console.WriteLine("Sifariş əlavə olundu.");
                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }

            async Task DeleteOrder()
            {
                Console.Clear();
                Console.Write("Silinəcək sifariş ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    try
                    {
                        await orderService.DeleteAsync(id);
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
                await orderService.SaveChangesAsync();
                Console.ReadKey();
            }

            async Task ListAllOrders()
            {
                Console.Clear();
                var orders = await orderService.GetAllAsync();
                if (orders.Count == 0)
                {
                    Console.WriteLine("Heç bir sifariş tapılmadı.");
                    Console.ReadKey();
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

            async Task FilterByDateRange()
            {
                Console.Clear();
                Console.Write("Başlanğıc tarix (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
                {
                    Console.WriteLine("Düzgün başlanğıc tarixi daxil edin.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Son tarix (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
                {
                    Console.WriteLine("Düzgün son tarixi daxil edin.");
                    Console.ReadKey();
                    return;
                }

                try
                {
                    var orders = await orderService.GetOrdersByDatesIntervalAsync(start, end);
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

            async Task FilterByPriceRange()
            {
                Console.Clear();
                Console.Write("Minimum məbləğ: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal min))
                {
                    Console.WriteLine("Düzgün məbləğ daxil edin.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Maksimum məbləğ: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal max))
                {
                    Console.WriteLine("Düzgün məbləğ daxil edin.");
                    Console.ReadKey();
                    return;
                }

                try
                {
                    var filteredOrders = await orderService.GetByPriceIntervalAsync(min, max);
                    foreach (var o in filteredOrders)
                    {
                        Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }


            async Task FilterByExactDate()
            {
                Console.Clear();
                Console.Write("Tarix (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Düzgün tarix daxil edin.");
                    Console.ReadKey();
                    return;
                }

                var filteredOrders = await orderService.GetOrderByDateAsync(date);
                if (filteredOrders.Count == 0)
                {
                    Console.WriteLine("Bu tarixdə sifariş tapılmadı.");
                    Console.ReadKey();
                    return;
                }

                foreach (var o in filteredOrders)
                {
                    Console.WriteLine($"{o.Id} - {o.TotalPrice} AZN - {o.OrderItem.Sum(i => i.Count)} məhsul - {o.OrderDate}");
                }
                Console.WriteLine("← Davam etmək üçün istənilən düyməni bas.");
                Console.ReadKey();
            }

            async Task GetOrderByNumber()
            {
                Console.Clear();
                Console.Write("Sifariş ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    try
                    {
                        var o = await orderService.GetByIdAsync(id);
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
}

