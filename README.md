BookProject
Bu proje, .NET Core 5 ile yazılmış mikroservis mimarisi temelli bir web uygulamasıdır. API Gateway, Redis, RabbitMQ gibi teknolojileri kullanarak, kitap verilerinin saklanması ve yönetilmesini sağlar.

Başlarken
Bu proje, .NET Core 5 ve PostgreSQL veritabanı kullanılarak geliştirilmiştir. Aşağıdaki adımları izleyerek projeyi yerel bilgisayarınızda çalıştırabilirsiniz:

Bu depoyu klonlayın.
PostgreSQL veritabanı kurulumu yapın.
appsettings.json dosyasında veritabanı bağlantı ayarlarınızı yapın.
dotnet ef database update komutunu kullanarak veritabanınızı oluşturun.
Projenizi çalıştırın.
Kullanılan Teknolojiler
Bu proje, aşağıdaki teknolojileri kullanarak geliştirilmiştir:

.NET Core 5
API Gateway
Redis
RabbitMQ
xUnit
Dependency Injection
Generic Repository Pattern
Code First Migrations
Entity Framework
Projenin Yapısı
Bu projede, farklı mikroservisler bir araya getirilerek oluşturulmuştur. BookService mikroservisi, kitap verilerini saklamakla sorumludur. UserService mikroservisi, kullanıcı verilerini saklamakla sorumludur. ApiGateway mikroservisi, istekleri yönlendirerek ilgili servislere gönderir. Ayrıca, veri doğrulama işlemleri de burada yapılmaktadır.

Proje, Generic Repository Pattern kullanarak veri işlemlerini gerçekleştirir. Ayrıca, Dependency Injection kullanarak bağımlılıkları yönetir.

Testler
Bu proje, xUnit test kütüphanesi kullanılarak test edilmiştir. Testler, veri işlemlerinin doğru çalıştığından emin olmak için yazılmıştır.

