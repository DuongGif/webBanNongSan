using TestTemplate.Models;
namespace TestTemplate.Respository
{
    public class LoaiNongSanRepository : ILoaiNongSan
    {
        private readonly QlnongSanNewContext ql;

        public LoaiNongSanRepository(QlnongSanNewContext ql)
        {
            this.ql = ql;
        }

        public LoaiNongSan Add(LoaiNongSan loai)
        {
            ql.LoaiNongSans.Add(loai);
            ql.SaveChanges();
            return loai;
        }

        public LoaiNongSan Detele(string maloai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoaiNongSan> GetAll()
        {
            return ql.LoaiNongSans;
        }

        public LoaiNongSan GetLoaiNongSan(string maloai)
        {
            return ql.LoaiNongSans.Find(maloai);
        }

        public LoaiNongSan Update(LoaiNongSan loai)
        {
            ql.LoaiNongSans.Update(loai);
            ql.SaveChanges();
            return loai;
        }
    }
}
