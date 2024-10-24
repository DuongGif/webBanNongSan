using TestTemplate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitaplon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NongSanAPIController : ControllerBase
    {
        QlnongSanNewContext db = new QlnongSanNewContext();
        [HttpGet]
        public IEnumerable<NongSan> GetAllSP()
        {
            return db.NongSans.ToList();
        }

        [HttpGet("{maLoai}")]
        public IEnumerable<NongSan> GetAllSPByList(string maLoai)
        {
            return db.NongSans.Where(x => x.MaLoai == maLoai).ToList();
        }


    }
}
