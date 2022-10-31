using System.ComponentModel.DataAnnotations;

public class RelayInfoDto
{
    public Guid Id { get; init; }

    public string Chain { get; init; } = String.Empty;

    public string EmitterAddressHex { get; init; } = String.Empty;

    public ulong Sequence { get; init; }

    public string PayloadHex { get; init; } = String.Empty;

    public string? StreamEscrow { get; init; }

    public string Status { get; init; } = String.Empty;

    public DateTimeOffset CreatedAt { get; init; }

    public List<string> Signatures { get; init; } = new List<string>();
}



