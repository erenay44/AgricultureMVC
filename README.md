# AgricultureUI

AgricultureUI, bir tarım/ziraat işletmesi için geliştirilmiş; kurumsal tanıtım sayfaları ile yönetim (admin) panelini bir arada barındıran bir **ASP.NET Core MVC** web uygulamasıdır. Proje, **N-katmanlı mimari** (katmanlı/layered architecture) prensiplerine göre tasarlanmıştır ve Entity Framework Core üzerinden SQL Server veritabanı ile çalışır.

## İçindekiler

- [Mimari](#mimari)
- [Kullanılan Teknolojiler ve Kütüphaneler](#kullanılan-teknolojiler-ve-kütüphaneler)
- [Proje Yapısı](#proje-yapısı)
- [Özellikler](#özellikler)
- [Kurulum](#kurulum)
- [Veritabanı Migration'ları](#veritabanı-migrationları)
- [Kimlik Doğrulama](#kimlik-doğrulama)

## Mimari

Proje 4 ayrı katmandan oluşur ve her katman kendi `.csproj` dosyasına sahip bağımsız bir class library (ya da web) projesidir:

```
AgricultureUI (Sunum Katmanı - ASP.NET Core MVC)
    ↓
BussinessLayer (İş Mantığı Katmanı)
    ↓
DataLayer (Veri Erişim Katmanı - EF Core)
    ↓
EntityLayer (Domain / Entity Modelleri)
```

Her katman bir alt katmana proje referansı (`ProjectReference`) ile bağlıdır. İş mantığı katmanında **Abstract/Concrete** ayrımı (arayüz + implementasyon) ve **Repository Pattern** kullanılmıştır; bağımlılıklar `BussinessLayer/Container/Extensions.cs` içinde `IServiceCollection` üzerinden **Dependency Injection** ile kayıt edilir.

## Kullanılan Teknolojiler ve Kütüphaneler

**Genel**
- **.NET 8.0** — tüm katmanlar `net8.0` hedefliyor
- **ASP.NET Core MVC** (`Microsoft.NET.Sdk.Web`) — sunum katmanı

**Veritabanı / ORM**
- **Entity Framework Core 8.0** (`Microsoft.EntityFrameworkCore`)
- **EF Core SQL Server sağlayıcısı** (`Microsoft.EntityFrameworkCore.SqlServer`)
- **EF Core Design & Tools** (`Microsoft.EntityFrameworkCore.Design`, `Microsoft.EntityFrameworkCore.Tools`) — migration üretimi için
- **SQL Server** (LocalDB / SQL Express) — hedef veritabanı motoru

**Kimlik Doğrulama & Yetkilendirme**
- **ASP.NET Core Identity** (`Microsoft.AspNetCore.Identity`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`)
- **Cookie tabanlı authentication** (`CookieAuthenticationDefaults`)
- Global yetkilendirme politikası: tüm action'lar varsayılan olarak `RequireAuthenticatedUser()` ile korunuyor

**Doğrulama (Validation)**
- **FluentValidation.AspNetCore** — form/model doğrulama kuralları (`ValidationRules` klasöründe `AddressValidator`, `EmployeeValidator`, `ImageValidator`)

**Raporlama / Excel**
- **ClosedXML** — Excel (.xlsx) rapor dosyaları oluşturmak için (`ReportController` içinde ürün, mesaj ve duyuru raporları)
- **EPPlus** (`OfficeOpenXml`) — Excel işlemleri için ek kütüphane

**Kod Üretimi**
- **Microsoft.VisualStudio.Web.CodeGeneration.Design** — scaffolding araçları

**Frontend**
- **Bootstrap** — responsive UI framework
- **jQuery**, **jQuery Validation**, **jQuery Validation Unobtrusive** — istemci taraflı doğrulama ve DOM işlemleri
- **MajesticAdmin** (Bootstrap admin template) — admin panel arayüz teması
- Özel `css/`, `js/` dosyaları ve bir login form starter şablonu

## Proje Yapısı

### 1. `EntityLayer` — Domain Modelleri
Veritabanı varlıklarını (entity) temsil eden POCO sınıfları içerir:
- `About`, `Address`, `Admin`, `Announcement`, `Contact`, `Employee`, `Image`, `Service`, `SocialMedia`

### 2. `DataLayer` — Veri Erişim Katmanı
- **`Contexts/AgricultureContext.cs`** — `IdentityDbContext`'ten türeyen EF Core context'i; her entity için `DbSet` tanımları içerir
- **`Abstract/`** — her entity için generic olmayan DAL arayüzleri (`IAboutDal`, `IAddressDal`, vb.) ve ortak `IGenericDal`
- **`Concrete/EntityFramework/`** — arayüzlerin EF Core implementasyonları (`EfAboutDal`, `EfAddressDal`, vb.)
- **`Concrete/Repository/GenericRepository.cs`** — generic repository implementasyonu
- **`Migrations/`** — EF Core migration geçmişi (veritabanı şeması, Identity tabloları dahil)

### 3. `BussinessLayer` — İş Mantığı Katmanı
- **`Abstract/`** — servis arayüzleri (`IAboutService`, `IAddressService`, vb.)
- **`Concrete/`** — servis implementasyonları / manager sınıfları (`AboutManager`, `AddressManager`, vb.)
- **`ValidationRules/`** — FluentValidation doğrulama kuralları
- **`Container/Extensions.cs`** — tüm servis ve DAL bağımlılıklarının DI konteynerine kaydı

### 4. `AgricultureUI` — Sunum Katmanı (ASP.NET Core MVC)
- **`Controllers/`** — `AddressController`, `AdminController`, `AnnouncementController`, `ChartController`, `ContactController`, `DashboardController`, `DefaultController`, `EmployeeController`, `HomeController`, `ImageController`, `LoginController`, `ProfileController`, `ReportController`, `ServiceController`
- **`Models/`** — view modelleri (`LoginViewModel`, `RegisterViewModel`, `ServiceAddViewModel`, `UserEditViewModel`, `ContactModel`, `AnnouncementModel`, `ProductClass`, `ErrorViewModel`)
- **`ViewComponents/`** — sayfa parçalarını (partial) besleyen bileşenler (Navbar, Dashboard, Gallery, Map, Social Media vb.)
- **`Views/`** — Razor görünümleri (her controller için ayrı klasör + `Shared`, `AdminPartials`)
- **`wwwroot/`** — statik dosyalar (CSS, JS, kütüphaneler, admin şablonu)
- **`Program.cs`** — uygulama başlangıç/konfigürasyon dosyası (middleware pipeline, DI kayıtları, routing)

## Özellikler

- **Kurumsal tanıtım sitesi**: Hakkımızda, hizmetler, çalışanlar, duyurular, iletişim, sosyal medya, adres/harita bölümleri
- **Admin paneli / Dashboard**: İçerik yönetimi (hizmet, duyuru, çalışan, görsel ekleme-düzenleme) ve genel bakış ekranları
- **Kullanıcı kimlik doğrulama**: ASP.NET Core Identity ile giriş/kayıt, cookie tabanlı oturum yönetimi
- **Grafik/İstatistik**: `ChartController` üzerinden ürün verilerinin JSON olarak sunulması (dashboard grafiklerinde kullanılmak üzere)
- **Excel Raporlama**: `ReportController` üzerinden ürün stok raporu, iletişim mesajları raporu ve duyuru raporu `.xlsx` formatında indirilebilir
- **Form doğrulama**: Hem sunucu tarafında (FluentValidation) hem istemci tarafında (jQuery Validation) doğrulama

## Kurulum

### Gereksinimler
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, SQL Express veya tam sürüm)
- Visual Studio 2022 / VS Code (opsiyonel ama önerilir)

### Adımlar

1. Depoyu klonlayın:
   ```bash
   git clone <repo-url>
   cd AgricultureUI
   ```

2. Veritabanı bağlantısını yapılandırın.
   > **Not:** Şu an bağlantı dizesi `DataLayer/Contexts/AgricultureContext.cs` içinde doğrudan (hardcoded) tanımlıdır:
   > ```csharp
   > optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS;database=AgricultureDb;integrated security=true;TrustServerCertificate=True;");
   > ```
   > Kendi ortamınıza göre bu satırı güncelleyin veya (önerilen) bağlantı dizesini `appsettings.json`'a taşıyıp `builder.Configuration` üzerinden okuyacak şekilde düzenleyin.

3. Bağımlılıkları geri yükleyin:
   ```bash
   dotnet restore
   ```

4. Migration'ları veritabanına uygulayın (aşağıdaki bölüme bakın).

5. Uygulamayı çalıştırın:
   ```bash
   cd AgricultureUI
   dotnet run
   ```

6. Tarayıcıda konsolda belirtilen adresi açın (varsayılan olarak `https://localhost:xxxx`).

## Veritabanı Migration'ları

Proje, EF Core Code-First yaklaşımını kullanır. Mevcut migration geçmişi `DataLayer/Migrations/` altında bulunur (veritabanı şeması, sosyal medya, about, admin entity'leri ve Identity tabloları dahil kademeli migration'lar).

Veritabanını oluşturmak/güncellemek için `DataLayer` projesinin bulunduğu dizinde veya çözüm kök dizininde:

```bash
dotnet ef database update --project DataLayer --startup-project AgricultureUI
```

Yeni bir entity/alan değişikliği sonrası migration eklemek için:

```bash
dotnet ef migrations add <MigrationAdi> --project DataLayer --startup-project AgricultureUI
```

## Kimlik Doğrulama

- Uygulama genelinde `AuthorizationPolicyBuilder().RequireAuthenticatedUser()` ile **varsayılan olarak tüm sayfalar korumalıdır**.
- Giriş yapılmamış kullanıcılar `/Login/Index/` adresine yönlendirilir.
- Kimlik doğrulama **cookie tabanlıdır** ve kullanıcı/rol yönetimi **ASP.NET Core Identity** (`IdentityUser`, `IdentityRole`) ile `AgricultureContext` (Identity tablolarını da içeren `IdentityDbContext`) üzerinden sağlanır.

---

> Bu README, proje kaynak kodu incelenerek oluşturulmuştur. Ortam değişkenleri, deployment adımları veya lisans bilgisi gibi ek detayları kendi ihtiyaçlarınıza göre güncelleyebilirsiniz.
