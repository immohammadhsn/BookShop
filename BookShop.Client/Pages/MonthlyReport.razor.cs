using BookShop.Shared.Entities;

namespace BookShop.Client.Pages;

public partial class MonthlyReport
{
    private double TotalPrifit;
    private List<SoldBook>? SoldBooks = new();
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender)
        {
            SoldBooks = await _bookService.GetSoldBooks();
            CalculateTotalProfit();
            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private void CalculateTotalProfit()
    {
        if (SoldBooks is null) return;
        TotalPrifit = SoldBooks.Sum(b => b.Profit);
    }
}

