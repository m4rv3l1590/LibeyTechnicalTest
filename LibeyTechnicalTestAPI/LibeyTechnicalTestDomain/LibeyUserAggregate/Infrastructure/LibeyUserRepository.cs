using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly Context _context;
        public LibeyUserRepository(Context context)
        {
            _context = context;
        }
        public void Create(LibeyUser libeyUser)
        {
            var existingUser = _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == libeyUser.DocumentNumber);
            if (existingUser == null)
            {
                _context.LibeyUsers.Add(libeyUser);
            }
            else 
            {
                UpdateUser(existingUser, libeyUser);
            }
            
            _context.SaveChanges();
        }

        public void Delete(string documentNumber)
        {
            var existingUser = _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == documentNumber);
            if (existingUser != null)
            {
                var userEntity = new LibeyUser(existingUser.DocumentNumber, existingUser.DocumentTypeId, existingUser.Name, existingUser.FathersLastName, existingUser.MothersLastName, existingUser.Address, existingUser.UbigeoCode, existingUser.Phone, existingUser.Email, existingUser.Password, false);
                UpdateUser(existingUser, userEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
        private void UpdateUser(LibeyUser existingUser, LibeyUser newUser)
        {
            var entry = _context.Entry(existingUser);
            entry.CurrentValues.SetValues(newUser);
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var q = from libeyUser in _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber))
            select new LibeyUserResponse()
            {
                DocumentNumber = libeyUser.DocumentNumber,
                Active = libeyUser.Active,
                Address = libeyUser.Address,
                DocumentTypeId = libeyUser.DocumentTypeId,
                Email = libeyUser.Email,
                FathersLastName = libeyUser.FathersLastName,
                MothersLastName = libeyUser.MothersLastName,
                Name = libeyUser.Name,
                Password = libeyUser.Password,
                Phone = libeyUser.Phone,
                UbigeoCode = libeyUser.UbigeoCode
            };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }
        public List<LibeyUserResponse> FindAll()
        {
            var q = from libeyUser in _context.LibeyUsers.Where(x => x.Active == true)
            select new LibeyUserResponse()
            {
                DocumentNumber = libeyUser.DocumentNumber,
                Active = libeyUser.Active,
                Address = libeyUser.Address,
                        DocumentTypeId = libeyUser.DocumentTypeId,
                        Email = libeyUser.Email,
                        FathersLastName = libeyUser.FathersLastName,
                        MothersLastName = libeyUser.MothersLastName,
                        Name = libeyUser.Name,
                        Password = libeyUser.Password,
                        Phone = libeyUser.Phone
                    };
            var list = q.ToList();
            return list;
        }

    }
}