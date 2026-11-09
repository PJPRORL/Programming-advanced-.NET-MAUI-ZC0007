using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;

public class KlankkastContext : DbContext
{
    public KlankkastContext(DbContextOptions<KlankkastContext> options)
        : base(options) { }


}
