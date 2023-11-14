using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Oracle.EntityFrameworkCore.Migrations.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Data
{
    public class EFMigrationHistory : OracleHistoryRepository
    {
        public EFMigrationHistory(HistoryRepositoryDependencies dependencies)
        : base(dependencies)
        {
        }

        protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);

            history.Property(h => h.MigrationId).HasColumnName("ID");
            history.Property(h => h.ProductVersion).HasColumnName("PRODUCT_VERSION");
        }
    }
}
