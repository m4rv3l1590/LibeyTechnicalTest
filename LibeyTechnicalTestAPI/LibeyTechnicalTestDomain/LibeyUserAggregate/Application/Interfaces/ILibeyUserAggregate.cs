using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse FindResponse(string documentNumber);
        List<LibeyUserResponse> FindAll();
        void Create(UserUpdateorCreateCommand command);
        void Delete(string documentNumber);
    }
}