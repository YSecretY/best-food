using BestFood.Modules.Users.Application.Users.Commands;
using BestFood.Modules.Users.Presentation.Users.Requests.Register;
using Mapster;

namespace BestFood.Modules.Users.Presentation.Users.Mapping;

public class UsersMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserRequest, RegisterUserCommand>();
    }
}