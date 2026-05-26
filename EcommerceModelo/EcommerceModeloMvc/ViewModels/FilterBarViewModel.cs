namespace EcommerceModeloMvc.ViewModels
{
    public enum FilterType { Checkbox, SizeGrid, Range }

    public class FilterGroupViewModel
    {
        public string Label { get; set; } = "";
        public FilterType Type { get; set; } = FilterType.Checkbox;
        public List<FilterOptionViewModel> Options { get; set; } = new();
        public RangeFilterViewModel? Range { get; set; }
    }

    public class FilterOptionViewModel
    {
        public string Id { get; set; } = "";
        public string Label { get; set; } = "";
    }

    public class RangeFilterViewModel
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 1000;
        public int Step { get; set; } = 10;
        public int Value { get; set; } = 500;
        public string InputId { get; set; } = "faixa-preco";
        public string OutputId { get; set; } = "faixa-preco-value";
    }
}
