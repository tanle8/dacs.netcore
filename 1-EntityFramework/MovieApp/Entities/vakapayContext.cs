using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieApp.Entities
{
    public partial class vakapayContext : DbContext
    {
        public vakapayContext()
        {
        }

        public vakapayContext(DbContextOptions<vakapayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitcoinaddress> BitcoinAddresses { get; set; }
        public virtual DbSet<Ethereumwithdrawtransaction> EthereumWithdrawTransactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        // Unable to generate entity type for table 'bitcoinwithdrawtransaction'. Please see the warning messages.
        // Unable to generate entity type for table 'ethereumaddress'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;userid=root;pwd=admin;port=3306;database=vakapay;sslmode=none;");
            }
        }

        /** The method OnModelCreating(...), this is where all the
          * relationships and validation are implemented
          */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is an entity call for each one of our entities
            modelBuilder.Entity<Bitcoinaddress>(entity =>
            {
                entity.ToTable("bitcoinaddress");

                entity.Property(e => e.Id).HasColumnType("varchar(200)");

                entity.Property(e => e.Address).HasColumnType("varchar(45)");

                entity.Property(e => e.CreatedAt).HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("varchar(45)");

                entity.Property(e => e.UpdatedAt).HasColumnType("int(11)");

                entity.Property(e => e.WalletId).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Ethereumwithdrawtransaction>(entity =>
            {
                entity.ToTable("ethereumwithdrawtransaction");

                entity.Property(e => e.Id).HasColumnType("varchar(200)");

                entity.Property(e => e.Amount).HasColumnType("decimal(20,8)");

                entity.Property(e => e.BlockNumber).HasColumnType("varchar(200)");

                entity.Property(e => e.CreatedAt).HasColumnType("int(11)");

                entity.Property(e => e.Fee).IsRequired().HasColumnType("varchar(200)");

                entity.Property(e => e.FromAddress).IsRequired().HasColumnType("varchar(200)");

                entity.Property(e => e.Hash).HasColumnType("varchar(200)");

                entity.Property(e => e.InProcess).HasColumnType("int(11)");

                entity.Property(e => e.NetworkName).HasColumnType("varchar(200)");

                entity.Property(e => e.Status).HasColumnType("varchar(200)");

                entity.Property(e => e.ToAddress).IsRequired().HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedAt).HasColumnType("int(11)");

                entity.Property(e => e.Version).HasColumnType("int(11)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnType("varchar(45)");

                entity.Property(e => e.Email).HasColumnType("varchar(45)");

                entity.Property(e => e.IpWhiteList).HasColumnType("varchar(45)");

                entity.Property(e => e.SecondPassword).HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallet");

                entity.Property(e => e.Id).HasColumnType("varchar(200)");

                entity.Property(e => e.Address).HasColumnType("varchar(45)");

                entity.Property(e => e.Balance).HasColumnType("decimal(16,8)");

                entity.Property(e => e.CreatedAt).HasColumnType("int(11)");

                entity.Property(e => e.NetworkName).HasColumnType("varchar(45)");

                entity.Property(e => e.UpdatedAt).HasColumnType("int(11)");

                entity.Property(e => e.UserId).IsRequired().HasColumnType("varchar(200)");

                entity.Property(e => e.Version).HasColumnType("int(11)");
            });
        }
    }
}
