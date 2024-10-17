using baitaplon.Models;

namespace baitaplon.Respository
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
