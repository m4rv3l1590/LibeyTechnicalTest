using LibeyTechnicalTestDomain.DocumentTypeAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.DocumentTypeAggregate.Domain;
using LibeyTechnicalTestDomain.EFCore;

namespace LibeyTechnicalTestDomain.DocumentTypeAggregate.Infraestructure
{
    public class UtilsRepository : IUtilsRepository
    {
        private readonly Context _context;
        public UtilsRepository(Context context)
        {
            _context = context;
        }

        public List<DocumentType> GetAllDocumentType()
        {
            var q = from documentType in _context.DocumentType
                    select new DocumentType()
                    {
                        DocumentTypeId = documentType.DocumentTypeId,
                        DocumentTypeDescription = documentType.DocumentTypeDescription,
                    };
            return q.ToList();
        }

        public List<Region> GetAllRegion()
        {
            var q = from documentType in _context.Region
                    select new Region()
                    {
                        RegionCode = documentType.RegionCode,
                        RegionDescription = documentType.RegionDescription
                    };
            return q.ToList();
        }

        public List<Province> GetProvince(string regionCode)
        {
            var q = from documentType in _context.Province
                    where documentType.RegionCode == regionCode
                    select new Province()
                    {
                        ProvinceCode = documentType.ProvinceCode,
                        RegionCode = documentType.RegionCode,
                        ProvinceDescription = documentType.ProvinceDescription
                    };
            return q.ToList();
        }

        public List<Ubigeo> GetUbigeo(string regionCode, string provinceCode)
        {
            var q = from documentType in _context.Ubigeo
                    where documentType.RegionCode == regionCode && documentType.ProvinceCode == provinceCode
                    select new Ubigeo()
                    {
                        UbigeoCode = documentType.UbigeoCode,
                        ProvinceCode = documentType.ProvinceCode,
                        RegionCode = documentType.RegionCode,
                        UbigeoDescription = documentType.UbigeoDescription
                    };
            return q.ToList();
        }

    }
}
