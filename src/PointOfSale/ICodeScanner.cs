using PointOfSale.Model;

namespace PointOfSale
{
    public interface ICodeScanner
    {
        StockItem ScanCode(string code);
    }
}