# E-Commercial-ASP.NET-MVC-ONION-ARCHITECT
E-Commercial-ASP.NET-MVC-ONION-ARCHITECT

# Project Information
- Language : ` C# `
- .Net Version :  ` .Net Framework 4.6.1 `
- Architect:  ` ASP.NET MVC / Onion Architect `
- Database : `  MSSQL `
#
Merhabalar, proje yapılırken fonksiyonel süreçleri için trendyol alış veriş sitesi baz alınmıştır, çok katmanlı(**[n-tier]()**) mimari olan "**[Onion Architect]()**" kullanılmıştır.Veri tabanı olarak MSSQL kullanılmış ve Code-First yaklaşımı ile bağlanmıştır

# Onion Mimari İçeriği ;

- Core Katmanında database crud operations interface ve genel yapılandırma ayarları oluşturulmuştur,
- Model Katmanında proje kapsamında kullanılacak sınıflar ve onlara ait map'ler oluşturulmuş ve Core katmanı referans edilmiştir.
- Service Katmanında Core ve Model katmanları referans edildikten sonra proje kapsamında veri tabanı ile crud operation işlemleri için Core katmanında set edilen interface, sınıflara kazandırılmıştır.
- WebUI katmanında ücretsiz bir tema sayfalara düzenlenerek giydirilmiş ve kullanıcı-admin yönetimi birbirlerinden ayrılmıştır.

# HIZLI BAŞLANGIÇ
- Projeyi build ediniz.
- Veritabanı ayarlarını aşağıdaki gibi yapılandırınız.
> [DatabaseTrendYol.sql] ile veri tabanı kurulumunu SQL Server Management ile yapınız.

> Model katmanında => Model.Context yolundaki ConnectionString kendi veritabanınıza göre ayarladıkdan sonra Package Manager Console üzerinde 'update-database' komutunu çalıştırın.

- Projede WebUI katmanını start up project olarak ayarladıktan sonra çalıştırabilirsiniz.
