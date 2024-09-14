using LibeyTechnicalTestDomain.DocumentTypeAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.DocumentTypeAggregate.Application.Interfaces
{
    public interface IUtilsRepository
    {
        List<DocumentType> GetAllDocumentType();
        List<Region> GetAllRegion();
        List<Province> GetProvince(string regionCode);
        List<Ubigeo> GetUbigeo(string regionCode, string provinceCode);
    }
}
