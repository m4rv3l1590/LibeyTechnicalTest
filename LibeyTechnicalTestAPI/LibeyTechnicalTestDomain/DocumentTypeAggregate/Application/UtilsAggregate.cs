using LibeyTechnicalTestDomain.DocumentTypeAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.DocumentTypeAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.DocumentTypeAggregate.Application
{
    public  class UtilsAggregate : IUtilsAggregate
    {
        private readonly IUtilsRepository _repository;
        public UtilsAggregate(IUtilsRepository repository)
        {
            _repository = repository;
        }

        public List<DocumentType> GetAllDocumentType()
        {
            return _repository.GetAllDocumentType();
        }

        public List<Region> GetAllRegion()
        {
            return _repository.GetAllRegion();
        }

        public List<Province> GetProvince(string regionCode)
        {
            return _repository.GetProvince(regionCode);
        }

        public List<Ubigeo> GetUbigeo(string regionCode, string provinceCode)
        { 
            return _repository.GetUbigeo(regionCode, provinceCode);
        }
    }
}
