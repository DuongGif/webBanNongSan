using TestTemplate.Respository;
using Microsoft.AspNetCore.Mvc;

namespace TestTemplate.ViewComponents
{
    public class LoaiNongSanMenuViewComponent : ViewComponent
    {
        private readonly ILoaiNongSan _IloaiNongSan;

        public LoaiNongSanMenuViewComponent(ILoaiNongSan IloaiNongSan)
        {
            _IloaiNongSan = IloaiNongSan;
        }
        public IViewComponentResult Invoke()
        {
            var loaiNongSan = _IloaiNongSan.GetAll().OrderBy(x => x.TenLoai);
            return View(loaiNongSan);
        }
    }
}
