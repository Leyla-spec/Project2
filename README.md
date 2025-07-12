# Restoran Sifariş İdarəetmə Konsol Tətbiqi

Bu layihə .NET 8 ilə yazılmış dörd qatlı (layer) konsol tətbiqidir. Restoran menyusunu və sifarişlərini idarə etməyə imkan verir.  
Verilənlər Entity Framework Core vasitəsilə SQL Server LocalDB-də saxlanılır.

---

## Layihə strukturu

Project2.sln
├── Project2.Core # Entity-lər və Enum-lar
│ ├── Entities # Məlumat modelləri
│ └── Enums # Kateqoriyalar (Categories)
├── Project2.DAL # Verilənlər bazası girişi (DbContext, Migration və s.)
│ ├── Contexts
│ ├── Configurations
│ └── Migrations
├── Project2.BL # Biznes məntiq
│ ├── Exceptions
│ └── Services
│ ├── Interfaces
│ └── Concretes
└── Project2.PL # Konsol interfeysi (Program.cs)


| Layer | Təyinat | Asılılıq |
|-------|---------|-----------|
| **Core** | Əsas domen sinifləri | Heç bir |
| **DAL**  | Verilənlər bazası ilə işləmə | Core |
| **BL**   | Biznes qaydalar və istisnalar | Core, DAL |
| **PL**   | İstifadəçi interfeysi (konsol menyusu) | Core, BL |

---

## Domen modelləri

| Entity        | Əsas sahələr                        | Qeyd |
|---------------|-------------------------------------|------|
| **BaseEntity** | `Id`, `CreatedAt`                  | Digər entity-lər bunu miras alır |
| **MenuItem**  | `Name`, `Price`, `Category`         | Menyu məhsulu |
| **OrderItem** | `MenuItemId`, `Count`               | Sifarişdəki məhsul |
| **Orders**    | `OrderDate`, `TotalPrice`, `OrderItem` | Tam sifariş obyekti |

### Enum – `Categories`

Menyu məhsullarının kateqoriyalarını göstərir:

### Biznes qaydalar və istisnalar

| Qayda                                         | Tətbiq olunduğu metod                              | İstisna sinifi                 |
|-----------------------------------------------|---------------------------------------------------|-------------------------------|
| Eyni adda menyu məhsulu təkrar əlavə edilə bilməz | `MenuItemService.AddMenuItem`                      | `DuplicateMenuItemException`   |
| Tarix və ya qiymət intervallarında başlanğıc son tarixdən/məbləğdən böyük ola bilməz | `OrderService.GetOrdersByDatesInterval`<br>`OrderService.GetOrdersByPriceInterval` | `InvalidPriceRangeException`   |
| Verilən ID-yə uyğun sifariş tapılmadıqda     | `OrderService.GetOrderById`<br>`OrderService.DeleteOrder` və s. | `OrderNotFoundException`       |

---

## Proqramı işə salmaq

Layihəni işə salmaq üçün aşağıdakı addımları izləyin:

1. **Tələblər:**

   - .NET 8 SDK-nın quraşdırılması  
   - SQL Server LocalDB (və ya `MenuAndOrderDbContext` içində connection string-i uyğun şəkildə dəyişdirin)

2. **Layihəni bərpa və kompilyasiya etmək:**

   ```bash
   dotnet restore
   dotnet build

3. **Verilənlər bazasını yeniləmək (migrasiyaları tətbiq etmək):**

   ```bash
   dotnet ef database update --project Project2.DAL

4. **Tətbiqi işə salmaq:**
   ```bash
   dotnet run --project Project2.PL

---

## Əsas menyu və funksiyalar

Tətbiq işə düşəndə aşağıdakı seçimlər mövcuddur:

```text
=== Əsas Menu ===
1. Menu üzərində əməliyyat aparmaq
2. Sifarişlər üzərində əməliyyat aparmaq
0. Çıxış
```
---

### 1. Menu üzərində əməliyyat aparmaq

Burada istifadəçi aşağıdakı seçimləri edə bilər:

* **Yeni item əlavə et:**
  Ad, qiymət və kateqoriya daxil edilir.
  Kateqoriya `Categories` enum-dan olmalıdır (məsələn, Starter, MainCourse və s.).

* **Item üzərində düzəliş et:**
  Mövcud itemin ID-si ilə tapılır və adı ilə qiyməti yenilənir.

* **Item sil:**
  ID vasitəsilə item silinir.

* **Bütün item-ları göstər:**
  Menyudakı bütün məhsullar siyahı şəklində, ID, ad, kateqoriya və qiymət ilə göstərilir.

* **Əvvəlki menyuya qayıt:**
  Əsas menyuya qayıtmaq üçün seçim.

---

### 2. Sifarişlər üzərində əməliyyat aparmaq

Seçimlər:

* **Yeni sifariş əlavə et:**
  Menyudan məhsulların ID-lərini və sayını daxil etməklə sifariş yaradılır.
  Ümumi məbləğ avtomatik hesablanır.

* **Sifarişi sil:**
  ID-ə əsasən sifariş silinir.

* **Bütün sifarişləri göstər:**
  Bütün mövcud sifarişlər ümumi məlumatları ilə göstərilir.

* **Tarix intervalına görə sifarişlər:**
  İstifadəçi tərəfindən daxil edilmiş başlanğıc və son tarixlərə uyğun sifarişlər göstərilir.

* **Məbləğ intervalına görə sifarişlər:**
  Müəyyən başlanğıc və son məbləğ aralığında olan sifarişlər siyahılanır.

* **Müəyyən tarixdə olan sifarişlər:**
  Daxil edilmiş tarixə uyğun bütün sifarişlər göstərilir.

* **Nömrəyə görə sifariş:**
  Sifariş ID-sinə görə sifariş və ona aid məhsulların detalları göstərilir.

* **Əvvəlki menyuya qayıt:**
  Əsas menyuya qayıtmaq üçün seçim.

---

### İstifadəçi qarşılıqlı əlaqəsi

* İstifadəçi seçim etmək üçün uyğun nömrəni daxil edir.
* Yanlış və ya uyğunsuz məlumat daxil edilərsə, səhv mesajı göstərilir və seçim təkrar tələb olunur.
* Sifariş əlavə edilərkən məhsul ID-si və sayı düzgün daxil edilməlidir, yoxsa yenidən soruşulur.

---

Bu menyu quruluşu restoranın menyu və sifariş idarəsini asanlaşdırır.

---

## Müəllif

**Ad**: Leyla Əliyeva
**GitHub**: \(https://github.com/Leyla-spec)
**Dil**: C# (.NET 8.0)

---
