# Book shop system
This system contains 3 applications.
The first application - BookShop. This app is APS.NET Core application, which uses Entity Framework, Quartz and Masstansit.It stores books and can sell them, also it can make discounts. 
The second one - BookProvider. This app is APS.NET Core application too. It delivers the books it takes from External API. Application delivers book using message queue.
The third - BookBuyer. ASP.NET Core application, which can buy book from book shop.
## The scheme of integration:
![](https://github.com/makarenkode/NSU_CSharp_course/raw/master/Scheme.png)

All of application deployed to Azure:
- [BookShop](https://bookshopweb.azurewebsites.net/books/)
- [BookProvider](https://bookprovider.azurewebsites.net/api/books/3)
- [BookBuyer](https://bookbuyer.azurewebsites.net/books)

Information about the technologies used:
- [MassTansit](https://masstransit-project.com/)
- [Quartz](https://www.quartz-scheduler.net/)
- [Crustal Quartz](https://github.com/guryanovev/CrystalQuartz/blob/master/readme.md)
- [Azure Service Bus](https://azure.microsoft.com/ru-ru/services/service-bus/)

How to use it:
You can go to [BookShop](https://bookshopweb.azurewebsites.net/books/) app and sell book by request:
https://bookshopweb.azurewebsites.net/books/sell/{id} or sell all books by request: https://bookshopweb.azurewebsites.net/books/sell/all
also you can make discount by request: https://bookshopweb.azurewebsites.net/books/discount/{genre}&{discount}.
Others application work automatically. If quantity of books in shop less then 10 percent, application will send need book message to books provider.
Bookbuyer every 30 seconds buys random book from shop. To see when book will be buyed you should go to: https://bookbuyer.azurewebsites.net/Quartz
