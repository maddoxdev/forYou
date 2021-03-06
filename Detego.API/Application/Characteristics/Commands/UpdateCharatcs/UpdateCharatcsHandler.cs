using Detego.API.Application.Stores.Notifications;
using Detego.API.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Detego.API.Application.Characteristics.Commands.UpdateCharatcs
{
    public class UpdateCharatcsHandler : IRequestHandler<UpdateCharatcsCommand>
    {
        private readonly StoreContext _context;
        private readonly IMediator _mediator;

        public UpdateCharatcsHandler(StoreContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(UpdateCharatcsCommand request, CancellationToken cancellationToken)
        {
            var charatcs = await _context.Characteristics
                .FindAsync(request.Id);
            
            charatcs.BackStore = request.BackStore;
            charatcs.FrontStore = request.FrontStore;
            charatcs.ShopWindow = request.ShopWindow;
            charatcs.Accuracy = request.Accuracy;
            charatcs.Availability = request.Availability;
            charatcs.MeanAge = request.MeanAge;

            _context.Update(charatcs);
            await _context.SaveChangesAsync();

            await _mediator.Publish(new StoreUpdateEvent {StoreId = request.StoreId});

            return await Unit.Task;
        }
    }
}