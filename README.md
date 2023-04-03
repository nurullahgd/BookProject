# BookProject

Bu proje, .NET Core 5 kullanılarak yazılmış bir N katmanlı mimari ile oluşturulmuş projedir. Projede API Gateway, Fluent Validation, JWT Token, AutoMapper, Mock, RabbitMQ, XUnit, Dependency Injection, Generic Repository Pattern, Code First Migrations ve Entity Framework teknolojileri kullanılmıştır. Projede PostgreSQL veritabanı kullanmaktadır.

# Başlarken

Bu projeyi çalıştırmak için aşağıdaki gereksinimlere ihtiyacınız olacaktır:

* .NET Core 5 SDK
* PostgreSQL veritabanı
* RabbitMQ mesajlaşma aracı

Ayrıca, projenin çalışması için gerekli olan diğer bağımlılıklar, projenin ana dizinindeki BookProject.sln dosyasında yer alan .csproj dosyalarında listelenmiştir. Bu bağımlılıklar, ilk kez çalıştırdığınızda projenin otomatik olarak yüklenmesi için .NET Core CLI veya Visual Studio ile yapılandırılabilir.

# Kurulum

1. Bu projeyi GitHub'dan indirin veya kopyalayın.
2. appsettings.json dosyasında PostgreSQL ayarlarınızı yapın.
3. Komut satırından proje ana dizinine gidin.
4. Projenin bağımlılıklarını yüklemek için aşağıdaki komutu çalıştırın:
dotnet restore
5. Veritabanını oluşturmak için aşağıdaki komutu çalıştırın:
dotnet ef database update
6. Projeyi çalıştırın
7. Proje varsayılan olarak http://localhost:5000 adresinde çalışacaktır.

# Kullanılan Teknolojiler

Bu proje, aşağıdaki teknolojileri kullanarak geliştirilmiştir:

* .NET Core 5
* API Gateway (Ocelot)
* RabbitMQ
* xUnit
* Dependency Injection
* Generic Repository Pattern
* Code First Migrations
* Entity Framework
* Jwt Token
* Fluent Validation

# Projenin Yapısı

BookService , kitap verilerini saklamakla sorumludur. AccountService , kullanıcı verilerini saklamakla sorumludur. ApiGateway, istekleri yönlendirerek ilgili servislere gönderir. Ayrıca, veri doğrulama işlemleri de burada yapılmaktadır.

Proje, Generic Repository Pattern kullanarak veri işlemlerini gerçekleştirir. Ayrıca, Dependency Injection kullanarak bağımlılıkları yönetir.

# Testler

Bu proje, xUnit test kütüphanesi kullanılarak test edilmiştir. Testler, veri işlemlerinin doğru çalıştığından emin olmak için yazılmıştır.


# Katkıda Bulunma

Bu projeye katkıda bulunmak isterseniz, lütfen pull request gönderin veya issue oluşturun. İletişim bilgilerim aşağıdadır.

# İletişim
E-posta: nurullahgundogdu02@hotmail.com

