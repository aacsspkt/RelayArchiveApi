using Microsoft.EntityFrameworkCore;
using RelayArchive.Api.Models;

namespace RelayArchive.Api.AppData;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<RelayInfo>? RelayInfos { get; set; }
    public DbSet<Signature>? Signatures { get; set; }
}