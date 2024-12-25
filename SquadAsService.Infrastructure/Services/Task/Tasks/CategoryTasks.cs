using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Domains;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Infrastructure.Services.Job.Tasks
{
    public class CategoryTasks:ICategoryTasks
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public CategoryTasks(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task SendNewArea(string name)
        {
            var subscribers = await _unitOfWork.Repository<Subscriber>().Entities
                            .Select(x => x.ContactEmail)
                            .ToListAsync(default);

            await _emailSender.SendNewAreaEmail(name, subscribers);
            

        }
        
        public async Task SendNewMarket(string name)
        {
            var subscribers = await _unitOfWork.Repository<Subscriber>().Entities
                            .Select(x => x.ContactEmail)
                            .ToListAsync(default);

            await _emailSender.SendNewMarketEmail(name, subscribers);
            

        }
        public async Task SendNewTechnology(string name)
        {
            var subscribers = await _unitOfWork.Repository<Subscriber>().Entities
                            .Select(x => x.ContactEmail)
                            .ToListAsync(default);

            await _emailSender.SendNewTechnologyEmail(name, subscribers);


        }
        public async Task SendNewProfile(string name)
        {
            var subscribers = await _unitOfWork.Repository<Subscriber>().Entities
                            .Select(x => x.ContactEmail)
                            .ToListAsync(default);

            await _emailSender.SendNewProfileEmail(name, subscribers);
        }
    }
}
