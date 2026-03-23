using Microsoft.AspNetCore.Mvc;
using WealthTrack.Models;

namespace WealthTrack.Controllers
{
    public class AssetController : Controller
    {
        private static readonly List<Asset> _assets = [
            new() { Id=1001, AssetName="Nvidia", Price=6897.76 },
            new() { Id=1002, AssetName="Apple", Price=4892.46 },
            new() { Id=1003, AssetName="Alphabet", Price=817.98 },
            new() { Id=1004, AssetName="Adobe", Price=740.78 },
            new() { Id=1005, AssetName="Bajaj", Price=10098.65 },
            new() { Id=1006, AssetName="Tesla", Price=76832.89 },
            new() { Id=1007, AssetName="Microsoft", Price=8746.54 },
            new() { Id=1008, AssetName="IBM", Price=4358.27 },
            new() { Id=1009, AssetName="Reliance", Price=765.89 },
        ];

        public static List<Asset> Assets { get => _assets; }
        // GET: AssetController
        public ActionResult Index()
        {
            return View(_assets);
        }
    }
}
