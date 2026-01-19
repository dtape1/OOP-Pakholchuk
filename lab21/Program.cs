using lab21.Factory;
using lab21.Services;

Console.WriteLine("Введіть тип обміну (standard, card, crypto, vip):");
string type = Console.ReadLine();

Console.WriteLine("Введіть суму обміну:");
decimal amount = decimal.Parse(Console.ReadLine());

var strategy = ExchangeStrategyFactory.CreateStrategy(type);
var service = new ExchangeService();

decimal result = service.CalculateFinalAmount(amount, strategy);

Console.WriteLine($"Сума після комісії: {result} грн");
