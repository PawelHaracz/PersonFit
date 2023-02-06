namespace PersonFit.Frontend.WebAssembly.Pages.Excerices;

public partial class Excerices
{
    protected EntityServerTableContext<ProductDto, Guid, ProductViewModel> Context { get; set; } = default!;

    private EntityTable<ProductDto, Guid, ProductViewModel> _table = default!;
}