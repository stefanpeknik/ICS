﻿
namespace Actie.App.Messages;
public record UserEditMessage
{
    public required Guid UserId { get; init; }
}
