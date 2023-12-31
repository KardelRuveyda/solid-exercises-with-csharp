#region Kodun Çalıştırıldığı Yer

Renault renault = new Renault();
renault.SendInfoDriver(new DriverInfo
{
    Telephone = "53051531",
    EmailAddress = "ruveyda@gmail.com"
});
CalculateTripCost();


#endregion

#region Seyahat harcanan parayı bulan fonksiyon
void CalculateTripCost()
{
    var calculator = new FuelCostClaculator();
    var cost= calculator.Calculate(new Renault());

    Console.WriteLine($"Toplam harcanan:{cost}");
}
#endregion


#region Renault 
public class Renault : BaseCar
{
    public override double GetCostPerKM()
    {
        return 1.5;
    }
}

#endregion

#region Nissan
public class Nissan : BaseCar
{
    public override double GetCostPerKM()
    {
        return 2.5;
    }
}
#endregion

#region Base İşlemleri
//Base için Base Car abstract oldu.
public abstract class BaseCar
{
    public int RoadKm { get; set; }

    public abstract double GetCostPerKM();

    public void Go()
    {
        Console.WriteLine("Araba  gidiyor.");
    }

    public void Stop()
    {
        Console.WriteLine("Araba durdu.");
    }

    public void SendMail()
    {
        Console.WriteLine("Mail gönderildi.");
    }

    public void SendSms()
    {
        Console.WriteLine("Mail gönderildi.");
    }


    //Bir metot içerisinde birden fazla işlem yapmaması gerekiyor
    //Aşağıdaki işlemde hem Mail hem Sms atılmaktadır.
    //Bu single responsibility'e uygu bir kullanım değildir. 
    public void SendInfoDriver(DriverInfo driver)
    {
        if (!string.IsNullOrEmpty(driver.EmailAddress))
        {
            SendMail();
        }

        if (!string.IsNullOrEmpty(driver.Telephone))
        {
            SendSms();
        }
    }

    public void SendInfoDriverSms(DriverInfo driver)
    {
        if (!string.IsNullOrEmpty(driver.Telephone))
        {
            SendSms();
        }
    }

    public void SendInfoDriverMail(DriverInfo driver)
    {
        if (!string.IsNullOrEmpty(driver.EmailAddress))
        {
            SendMail();
        }
    }
}

#endregion

#region //Yakıt Giderini hesaplayan Class

public class FuelCostClaculator
{
     public double Calculate(BaseCar car)
    {


        //Normalde bu şekilde ama ben her araç geldiğinde 
        //Aşağıdaki kodu değiştirmem gerekiyor.
        //Değiştirmek zorunda kalıyorsan aslında kuralı ihlal etmiş oldum.
        // En basit yol abstraction kullanmaktır.
        //return car.RoadKm * 4;
        //if (car is Renault)
        //{
        //    return car.RoadKm * 2;
        //}else if(car is Nissan)
        //{
        //    return car.RoadKm * 4;
        //}
        //else
        //{
        //    return car.RoadKm * 1;

        //}

        // Geliştirmeye açık ama değişme kapalı olmuş oldu.

        return car.RoadKm * car.GetCostPerKM();
    }
}

#endregion

#region Sürücü Bilgisini içeren Class
public class DriverInfo
{
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }
}
#endregion
