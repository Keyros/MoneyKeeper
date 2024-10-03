namespace MoneyKeeper.Web.Services.Dashboard;

public interface IDashBoardService
{
    Task<DashboardData> GetDashboardDataAsync();
}

internal sealed class DashboardService : IDashBoardService
{
    public Task<DashboardData> GetDashboardDataAsync() => Task.FromResult(new DashboardData());
}

public sealed record DashboardData
{
    private static readonly Dictionary<string, string> Currencies = new()
    {
        { "USD", "$" },
        { "GBP", "\u00a3" },
        { "EUR", "\u20ac" },
        { "JPY", "\u00a5" },
        { "CNY", "\uffe5" }
    };

    public string Currency => Accounts.First(x => x.IsMainAccount).CurrencySymbol;
    public decimal CurrentBalance => Accounts.Sum(x => x.Balance);

    public List<Account> Accounts { get; set; } = Enumerable.Range(0, 10)
        .Select(x =>
        {
            var isMainAccount = x == 0;
            var index = isMainAccount ? 0 : Random.Shared.Next(0, Currencies.Count);
            var (name, symbol) = Currencies.ElementAt(index);
            return new Account
            {
                Name = $"Account {x}",
                Balance = Random.Shared.Next(0, 1000),
                CurrencySymbol = symbol,
                CurrencyName = name,
                IsMainAccount = isMainAccount
            };
        })
        .OrderByDescending(x => x.IsMainAccount)
        .ThenBy(x => x.Name)
        .ThenBy(x => x.Balance)
        .ToList();
}

public sealed record Account
{
    public required string Name { get; init; }
    public required decimal Balance { get; init; }
    public required string CurrencySymbol { get; init; }
    public required string CurrencyName { get; init; }
    public required bool IsMainAccount { get; init; }
}