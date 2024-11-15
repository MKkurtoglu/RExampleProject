// See https://aka.ms/new-console-template for more information
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;

Console.WriteLine("Hello, World!");


//CarManager carManager = new CarManager(new EfCarDal());

//var carList=carManager.GetAll();
//foreach (var car in carList.Data)
//{
//    Console.WriteLine(car.BrandId);
//}

//carManager.Insert(new EntityLayer.Concrete.Car
//{
//    BrandId = 1,
//    CarId = 5,
//    ColorId = 1,
//    DailyPrice = 1000,
//    ModelYear = 1999,
//    Description = "bu arabalara bu fiyatlar çok ucuzz"
//});
//foreach (var car in carList.Data)
//{
//    Console.WriteLine($"Araba Markası :{car.BrandId} " +
//        $"Araba Rengi : {car.ColorId}" +
//        $"Araba Model Yılı : {car.ModelYear}" +
//        $"Araba Açıklaması : {car.Description}" +
//        $"Araba Günlük Fiyatı : {car.DailyPrice}");
//}
//var details = carManager.GetAllCarDetails().Data;
//var a = carManager.Get(2);
//Console.WriteLine($"Silinecek araç markası {a.Data.BrandId}");
//carManager.Delete(a.Data);
//foreach (var item in details)
//{
//    Console.WriteLine(item.CarName + " " +item.BrandName +" "+item.ColorName);
//}
