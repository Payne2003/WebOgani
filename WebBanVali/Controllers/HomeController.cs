using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanVali.Models;
using WebBanVali.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebBanVali.Controllers
{
	public class HomeController : Controller
	{
		QlbanVaLiContext db = new QlbanVaLiContext();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index(int? page)
		{

			int pageSize = 8;
			int pageNumber=page==null||page<0?1:page.Value;
			var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x=>x.TenSp);
			IPagedList<TDanhMucSp> lst = lstsanpham.ToPagedList(pageNumber,pageSize);
			return View(lst);
		}
		public IActionResult SanPhamTheoLoai(String maLoai,int? page)
		{

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().Where(x=>x.MaLoai == maLoai).OrderBy(x => x.TenSp);
            IPagedList<TDanhMucSp> lst = lstsanpham.ToPagedList(pageNumber, pageSize);
			ViewBag.maLoai = maLoai;
            return View(lst);
		}
		public IActionResult ChiTietSanPham(String MaSp)
		{
			var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSp);
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == MaSp).ToList();
			ViewBag.anhSanPham = anhSanPham;
			return View(sanPham);
		}

		public IActionResult ProductDetail(String maSp)
		{
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel { danhMucSp = sanPham, anhSps = anhSanPham };
            return View(homeProductDetailViewModel);
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
