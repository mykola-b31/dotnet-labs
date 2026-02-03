namespace ua.cn.stu;

public class Car
{
    public string LicensePlate { get; set; }
    private string Brand { get; set; }
    private string Model { get; set; }
    private int Year { get; set; }
    private double EngineVolume { get; set; }
    private bool IsAutomatic { get; set; }

    public Car(string licensePlate, string brand, string model, int year, double engineVolume, bool isAutomatic)
    {
        LicensePlate = licensePlate;
        Brand = brand;
        Model = model;
        Year = year;
        EngineVolume = engineVolume;
        IsAutomatic = isAutomatic;
    }

    public override string ToString()
    {
        return $"[{LicensePlate}] {Brand} {Model} ({Year}), {EngineVolume}L, Auto: {IsAutomatic}";
    }
}