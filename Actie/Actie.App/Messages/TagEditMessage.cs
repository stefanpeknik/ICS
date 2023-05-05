

namespace Actie.App.Messages;
public record TagEditMessage
{
    public required Guid TagId { get; init; }
}
