using MediatR;
using RYB.Business;
using RYB.Cryptography;
using RYB.MediatR.Notifications;
using RYB.MediatR.Requests;
using RYB.Model.ViewModel;

namespace RYB.MediatR.Handlers;

public class UserHandler :
    IRequestHandler<GetUserByEmailQuery, IEnumerable<UserProfile>>,
    IRequestHandler<GetUserByTokenQuery, UserProfile>,
    IRequestHandler<GetUsersQuery, IEnumerable<UserProfile>>,
    IRequestHandler<GetUserTokenQuery, string>
{
    private readonly IUserRepo _userRepo;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMediator _mediator;

    public UserHandler(IUserRepo userRepo, IJwtUtils jwtUtils, IMediator mediator)
    {
        _userRepo = userRepo;
        _jwtUtils = jwtUtils;
        _mediator = mediator;
    }
    public async Task<IEnumerable<UserProfile>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepo.GetUserByEmail(request.userEmail);
    }

    public async Task<UserProfile> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        // TODO: return fake data untill data ready
        return await Task.FromResult(new UserProfile
        {
            Email = request.userId,
            UserId = Guid.NewGuid(),
            Name = "Test Name"
        });
    }

    public async Task<IEnumerable<UserProfile>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserCreated("test user ID"));

        return await _userRepo.GetUsers();
    }

    public async Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_jwtUtils.GenerateJwtToken(new UserToken { Id = 123456, FirstName = "Test" }));
    }
}

