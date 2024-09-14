using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        public LibeyUserAggregate(ILibeyUserRepository repository)
        {
            _repository = repository;
        }
        public void Create(UserUpdateorCreateCommand command)
        {
            var userEntity = new LibeyUser(command.DocumentNumber,command.DocumentTypeId,command.Name,command.FathersLastName,command.MothersLastName,command.Address,command.UbigeoCode,command.Phone,command.Email,command.Password, true);
            _repository.Create(userEntity);
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        public List<LibeyUserResponse> FindAll()
        {
            var row = _repository.FindAll();
            return row;
        }

        public void Delete(string documentNumber)
        {
            _repository.Delete(documentNumber);
        }

    }
}