namespace MoneyKeeper.Web.Services.Dashboard;

public interface IDashBoardService
{
    Task<DashboardData> GetDashboardDataAsync();
}

internal sealed class DashboardService : IDashBoardService
{
    public Task<DashboardData> GetDashboardDataAsync()
    {
        return Task.FromResult(new DashboardData());
    }
}

public sealed record DashboardData
{
    public string Currency { get; set; } = "\u00a3";
    public decimal CurrentBalance { get; set; } = 213;
    public decimal TotalIncome { get; set; } = 234;
    public decimal TotalExpense { get; set; } = 345;

    public DateOnly From { get; set; } = new(DateTime.Now.Year, DateTime.Now.Month, 1);

    public DateOnly To { get; set; } = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
}