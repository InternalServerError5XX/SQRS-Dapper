using MediatR;
using Relations.Application.Services.ProfileService;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.ProfileRepository;

namespace Relations.Application.Queries
{
    public record GetProfilesQuery() : IRequest<IEnumerable<UserProfile>>;
    public record GetProfileQuery(long Id) : IRequest<UserProfile>;

    public class GetProfilesQueryHandler(IProfileService profileService)
        : IRequestHandler<GetProfilesQuery, IEnumerable<UserProfile>>
    {
        public async Task<IEnumerable<UserProfile>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            return await profileService.GetAllDapper();
        }
    }

    public class GetProfileQueryHandler(IProfileService profileService)
        : IRequestHandler<GetProfileQuery, UserProfile>
    {
        public async Task<UserProfile> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return await profileService.GetByIdDapper(request.Id);
        }
    }
}
