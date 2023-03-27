# BookProject

Bu proje, .NET Core 5 ile yazılmış mikroservis mimarisi temelli bir web uygulamasıdır. API Gateway, Redis, RabbitMQ gibi teknolojileri kullanarak, kitap verilerinin saklanması ve yönetilmesini sağlar.

# Başlarken

Bu proje, .NET Core 5 ve PostgreSQL veritabanı kullanılarak geliştirilmiştir. Aşağıdaki adımları izleyerek projeyi yerel bilgisayarınızda çalıştırabilirsiniz:

1. Bu depoyu klonlayın.
2. PostgreSQL veritabanı kurulumu yapın.
3. appsettings.json dosyasında veritabanı bağlantı ayarlarınızı yapın.
4. dotnet ef database update komutunu kullanarak veritabanınızı oluşturun.
5. Projenizi çalıştırın.

# Kullanılan Teknolojiler

Bu proje, aşağıdaki teknolojileri kullanarak geliştirilmiştir:

* .NET Core 5
* API Gateway (Ocelot)
* Redis
* RabbitMQ
* xUnit
* Dependency Injection
* Generic Repository Pattern
* Code First Migrations
* Entity Framework
* Jwt Token

# Projenin Yapısı

Bu projede, farklı mikroservisler bir araya getirilerek oluşturulmuştur. BookService mikroservisi, kitap verilerini saklamakla sorumludur. UserService mikroservisi, kullanıcı verilerini saklamakla sorumludur. ApiGateway mikroservisi, istekleri yönlendirerek ilgili servislere gönderir. Ayrıca, veri doğrulama işlemleri de burada yapılmaktadır.

Proje, Generic Repository Pattern kullanarak veri işlemlerini gerçekleştirir. Ayrıca, Dependency Injection kullanarak bağımlılıkları yönetir.


# Testler

Bu proje, xUnit test kütüphanesi kullanılarak test edilmiştir. Testler, veri işlemlerinin doğru çalıştığından emin olmak için yazılmıştır.
