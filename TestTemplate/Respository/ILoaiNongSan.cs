using TestTemplate.Models;

namespace TestTemplate.Respository
{
    public interface ILoaiNongSan
    {
        LoaiNongSan Add(LoaiNongSan loai);

        LoaiNongSan Update(LoaiNongSan loai);

        LoaiNongSan Detele(string maloai);

        LoaiNongSan GetLoaiNongSan(string maloai);

        IEnumerable<LoaiNongSan> GetAll();
    }
}
