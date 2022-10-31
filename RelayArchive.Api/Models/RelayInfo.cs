using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelayArchive.Api.Models;

[Table("relay_infos")]
[Index(nameof(Chain), nameof(EmitterAddressHex), nameof(Sequence), IsUnique = true)]
public class RelayInfo
{
    [Key, Required, Column("id")]
    public Guid Id { get; init; }

    [Required, Column("chain")]
    public string Chain { get; init; } = String.Empty;

    [Required, Column("emitter_address_hex")]
    public string EmitterAddressHex { get; init; } = String.Empty;

    [Required, Column("sequence")]
    public ulong Sequence { get; init; }

    [Required, Column("payload_hex")]
    public string PayloadHex { get; set; } = String.Empty;

    [Column("stream_escrow")]
    public string? StreamEscrow { get; set; }

    [Required, Column("status")]
    public string Status { get; set; } = String.Empty;

    [Required, Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    public List<Signature> Signatures { get; set; } = new List<Signature>();
}

[Table("signatures")]
public class Signature
{
    [Key, Column("value")]
    public string Value { get; set; } = String.Empty;

    [Column("relay_info_id")]
    public Guid RelayInfoId { get; init; }
}