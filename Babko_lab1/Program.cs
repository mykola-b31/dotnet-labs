namespace ua.cn.stu;

class Program
{
    private static IDictionary<string, Car> _carDictionary = new Dictionary<string, Car>();
    private static void Main(string[] args)
    {
        for (;;)
        {
            Console.WriteLine("Please enter command add, remove, find, list or exit: ");
            var command = Console.ReadLine();
            switch (command)
            {
                case "add":
                {
                    Console.WriteLine("Enter license plate:");
                    var licensePlate = Console.ReadLine() ?? "";
                    if (_carDictionary.ContainsKey(licensePlate) || licensePlate == "")
                    {
                        Console.WriteLine("Error: Car with this license plate already exists!");
                        break;
                    }
                    Console.WriteLine("Enter brand:");
                    var brand = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter model: ");
                    var model = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter year:");
                    int.TryParse(Console.ReadLine(), out var year);
                    Console.WriteLine("Enter engine volume:");
                    double.TryParse(Console.ReadLine(), out var engineVolume);
                    Console.WriteLine("Is automatic transmission? (true/false):");
                    bool.TryParse(Console.ReadLine(), out var isAutomatic);
                    
                    var car = new Car(licensePlate, brand, model, year, engineVolume, isAutomatic);
                    _carDictionary.Add(car.LicensePlate, car);
                    Console.WriteLine("Car was successfully added to collection");
                    break;
                }
                case "remove":
                {
                    Console.WriteLine("Enter license plate:");
                    var licensePlate = Console.ReadLine();
                    if (_carDictionary.Remove(licensePlate!))
                    {
                        Console.WriteLine("Car was removed from collection");
                    }
                    else 
                    {
                        Console.WriteLine("Error: Car not found.");
                    }
                    break;
                }
                case "list":
                {
                    foreach (var car in _carDictionary.Values)
                    {
                        Console.WriteLine(car.ToString());
                    }

                    break;
                }
                case "find":
                {
                    Console.WriteLine("Enter license plate to search:");
                    var searchKey = Console.ReadLine() ?? "";
                    if (_carDictionary.TryGetValue(searchKey, out var foundCar)) 
                    {
                        Console.WriteLine(foundCar.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Car not found.");
                    }
                    break;
                }
                case "exit":
                    Console.WriteLine("Exiting the program.");
                    return;
            } 
        }
    }
}