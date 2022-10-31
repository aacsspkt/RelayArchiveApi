

using RelayArchive.Api.Models;

namespace RelayArchive.Api;

public static class Converter
{
    public static RelayInfoDto AsRelayInfoDto(this RelayInfo relayInfo)
    {
        var signatures = relayInfo.Signatures.Select(s => s.Value).ToList();

        return new RelayInfoDto
        {
            Id = relayInfo.Id,
            Chain = relayInfo.Chain,
            EmitterAddressHex = relayInfo.EmitterAddressHex,
            PayloadHex = relayInfo.PayloadHex,
            Sequence = relayInfo.Sequence,
            Signatures = signatures,
            Status = relayInfo.Status,
            StreamEscrow = relayInfo.StreamEscrow,
            CreatedAt = relayInfo.CreatedAt
        };
    }
}