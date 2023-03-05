
using Microsoft.EntityFrameworkCore;

namespace ApiQuala.Entities.Class.Dto
{
    public sealed partial class ApiDBContext : DbContext
    {
        public ApiDBContext()
            : base()
        {
        }

        public ApiDBContext(DbContextOptions<ApiDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfiguracionApiDDB.cadenaConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public class ConfiguracionApiDDB
        {
            public static string cadenaConexion { get; set; }
        }
        public sealed class LogsSettings
        {

            public static string RutaLog = "Logs";
        }


        public sealed class DBSettings
        {
            public string DefaultConnection { get; set; }
        }
    }
}
