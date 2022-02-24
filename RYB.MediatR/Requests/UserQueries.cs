using MediatR;
using RYB.Model.ViewModel;

namespace RYB.MediatR.Requests;

public record GetUsersQuery : IRequest<IEnumerable<UserProfile>>;
public record GetUserByEmailQuery(string userEmail) : IRequest<IEnumerable<UserProfile>>;
public record GetUserByTokenQuery(string userId) : IRequest<UserProfile>;
public record GetUserTokenQuery(string userId) : IRequest<string>;

