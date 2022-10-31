using Microsoft.AspNetCore.Mvc;
using RelayArchive.Api.Models;
using RelayArchive.Api.AppData;
using Microsoft.EntityFrameworkCore;

namespace RelayArchive.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class RelayInfosController : ControllerBase
{

    private readonly ILogger<RelayInfosController> logger;
    private readonly DataContext context;


    public RelayInfosController(DataContext context, ILogger<RelayInfosController> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RelayInfoDto>>> GetRelayInfos(
        [FromQuery] string? chain,
        [FromQuery] string? emitterAddressHex,
        [FromQuery] ulong? sequence,
        [FromQuery] int limit = 10,
        [FromQuery] int offset = 1
        )
    {
        if (context.RelayInfos == null || context.Signatures == null)
        {
            logger.LogError("No context found for RelayInfo or Transactions.");
            return NotFound();
        }

        List<RelayInfo> relayInfos = new();

        offset = offset > 1 ? offset : 1;
        limit = limit > 100 ? 100 : limit;

        var skipCount = (offset - 1) * limit;

        logger.LogInformation("Getting relay infos");
        if (chain != null || emitterAddressHex != null || sequence != null)
        {
            if (chain != null && emitterAddressHex != null)
            {
                if (sequence != null)
                {
                    relayInfos = await context.RelayInfos
                        .Include(r => r.Signatures)
                        .Where(r => r.Chain == chain && r.EmitterAddressHex == emitterAddressHex && r.Sequence == sequence)
                        .Skip(skipCount)
                        .Take(limit)
                        .ToListAsync();
                }
                else
                {
                    relayInfos = await context.RelayInfos
                        .Include(r => r.Signatures)
                        .Where(r => r.Chain == chain && r.EmitterAddressHex == emitterAddressHex)
                        .Skip(skipCount)
                        .Take(limit)
                        .ToListAsync();
                }
            }
            else if (chain != null)
            {
                relayInfos = await context.RelayInfos
                        .Include(r => r.Signatures)
                        .Where(r => r.Chain == chain)
                        .Skip(skipCount)
                        .Take(limit)
                        .ToListAsync();
            }
            else if (emitterAddressHex != null)
            {
                relayInfos = await context.RelayInfos
                        .Include(r => r.Signatures)
                        .Where(r => r.EmitterAddressHex == emitterAddressHex)
                        .Skip(skipCount)
                        .Take(limit)
                        .ToListAsync();
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            relayInfos = await context.RelayInfos.OrderByDescending(r => r.CreatedAt)
                .Include(r => r.Signatures)
                .Skip(skipCount)
                .Take(limit)
                .ToListAsync();

        }

        var dtos = relayInfos.Select(r => r.AsRelayInfoDto()).ToList();

        return dtos;
    }

    [HttpGet("{chain}/{emitterAddressHex}/{sequence}")]
    public async Task<ActionResult<RelayInfoDto>> GetRelayInfo(string chain, string emitterAddressHex, ulong sequence)
    {
        logger.LogInformation("Getting relay infos for chain: {chain}, emitterAddressHex: {emitterAddressHex}, sequence: {sequence}", chain, emitterAddressHex, sequence);
        if (context.RelayInfos == null || context.Signatures == null)
        {
            logger.LogError("No context found for RelayInfo or Transactions.");
            return NotFound();
        }

        var relayInfo = await context.RelayInfos.Include(r => r.Signatures).SingleOrDefaultAsync(
            r => r.Chain == chain &&
            r.EmitterAddressHex == emitterAddressHex &&
            r.Sequence == sequence
            );

        if (relayInfo == null)
        {
            logger.LogInformation("No data found");
            return NotFound();
        }

        logger.LogDebug("RelayInfo: {}", relayInfo);

        var dto = relayInfo.AsRelayInfoDto();

        return dto;
    }

    [HttpPost(Name = "StoreRelayInfo")]
    public async Task<ActionResult<RelayInfoDto>> StoreRelayInfo(CreateRelayInfoDto relayInfoDto)
    {
        if (context.RelayInfos == null || context.Signatures == null)
        {
            logger.LogError("No context found for RelayInfo or Transactions");

            return NotFound();
        }

        var relayId = Guid.NewGuid();
        var signatures = relayInfoDto.Signatures.Select<string, Signature>(value =>
            new()
            {
                Value = value,
                RelayInfoId = relayId
            }
            ).ToList();

        RelayInfo relayInfo = new()
        {
            Id = relayId,
            Chain = relayInfoDto.Chain,
            EmitterAddressHex = relayInfoDto.EmitterAddressHex,
            Sequence = relayInfoDto.Sequence,
            PayloadHex = relayInfoDto.PayloadHex,
            Status = relayInfoDto.Status,
            Signatures = signatures,
            StreamEscrow = relayInfoDto.StreamEscrow,
            CreatedAt = DateTimeOffset.UtcNow
        };

        try
        {
            await context.RelayInfos.AddAsync(relayInfo);

            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }


        return CreatedAtAction(
            nameof(GetRelayInfo),
            new
            {
                chain = relayInfo.Chain,
                emitterAddressHex = relayInfo.EmitterAddressHex,
                sequence = relayInfo.Sequence
            },
            relayInfo.AsRelayInfoDto()
        );
    }

    [HttpDelete("{chain}/{emitterAddressHex}/{sequence}", Name = "DeleteRelayInfo")]
    public async Task<ActionResult> DeleteRelayInfo([FromRoute] string chain, [FromRoute] string emitterAddressHex, [FromRoute] ulong sequence)
    {
        if (context.RelayInfos == null || context.Signatures == null)
        {
            return NotFound();
        }

        var relayInfo = await context.RelayInfos.SingleOrDefaultAsync(r =>
            r.Chain.Equals(chain) &&
            r.EmitterAddressHex.Equals(emitterAddressHex) &&
            r.Sequence == sequence
        );

        if (relayInfo == null)
        {
            return NotFound();
        }

        // var transactions = await context.Signatures.Where(t => t.RelayInfoId == relayInfo.Id).ToListAsync();
        // context.Signatures.RemoveRange(transactions);

        context.RelayInfos.Remove(relayInfo);

        await context.SaveChangesAsync();

        return NoContent();
    }
}

