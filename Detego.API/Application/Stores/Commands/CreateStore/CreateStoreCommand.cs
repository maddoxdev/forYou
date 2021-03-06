using Detego.API.Entity;
using MediatR;

namespace Detego.API.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<Store>
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string ContactEmail { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerContactEmail { get; set; }

    }
}