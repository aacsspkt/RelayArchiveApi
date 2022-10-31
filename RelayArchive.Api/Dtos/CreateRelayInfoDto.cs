using System.ComponentModel.DataAnnotations;

public class CreateRelayInfoDto
{

    [Required]
    public string Chain { get; init; } = String.Empty;

    [Required]
    public string EmitterAddressHex { get; init; } = String.Empty;

    [Required]
    public ulong Sequence { get; init; }

    [Required]
    public string PayloadHex { get; init; } = String.Empty;

    public string? StreamEscrow { get; init; }

    [Required]
    public string Status { get; init; } = String.Empty;

    [Required]
    public List<string> Signatures { get; init; } = new List<string>();
}
