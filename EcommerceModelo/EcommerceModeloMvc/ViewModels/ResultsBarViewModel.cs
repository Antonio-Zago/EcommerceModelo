namespace EcommerceModeloMvc.ViewModels
{
    public class ResultsBarViewModel
    {
        public int Shown { get; set; } = 30;
        public int Total { get; set; } = 1000;

        public List<SortOptionViewModel> SortOptions { get; set; } = new()
        {
            new() { Value = "",  Label = "Relevância",              Selected = true },
            new() { Value = "1", Label = "Preço: maior para menor" },
            new() { Value = "2", Label = "Preço: menor para maior" },
            new() { Value = "3", Label = "Mais vendidos"            },
            new() { Value = "4", Label = "Mais recentes"            },
            new() { Value = "5", Label = "Nome A–Z"                 },
            new() { Value = "6", Label = "Nome Z–A"                 },
        };
    }

    public class SortOptionViewModel
    {
        public string Value   { get; set; } = "";
        public string Label   { get; set; } = "";
        public bool   Selected { get; set; } = false;
    }
}
