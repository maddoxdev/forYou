using Detego.API.Application.Stores.Notifications;
using Detego.API.Context;
using Detego.API.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Detego.API.Application.Stores.Commands.CreateStore
{
    public class CreateStoreHandler : IRequestHandler<CreateStoreCommand, Store>
    {
        private readonly IMediator _mediator;
        private readonly StoreContext _context;

        public CreateStoreHandler(IMediator mediator, StoreContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Store> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = new Store
            {
                Name = request.Name,
                CountryCode = request.CountryCode,
                ContactEmail = request.ContactEmail,
                Manager = new Manager
                {
                    FirstName = request.ManagerFirstName,
                    LastName = request.ManagerLastName,
                    ContactEmail = request.ManagerContactEmail
                },
                Characteristic = new Characteristic()
            };

            #region [Use repository pattern]

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            #endregion

            await _mediator.Publish(new StoreListUpdateEvent(), cancellationToken);

            return store;
        }
    }
}