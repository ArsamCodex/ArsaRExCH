﻿// <auto-generated />
using System;
using ArsaRExCH.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArsaRExCH.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArsaRExCH.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f2bfac30-d800-4bb6-961c-1e7ce1419ce3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3a984523-b727-475d-97cb-d4bd4b2cff49",
                            Email = "ARMINTTWAT@GMAIL.COM",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ARMINTTWAT@GMAIL.COM",
                            NormalizedUserName = "ARMINTTWAT@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e0ead1f6-21af-4d07-99e5-c65754fe4d9b",
                            TwoFactorEnabled = false,
                            UserName = "arminttwat@gmail.com"
                        });
                });

            modelBuilder.Entity("ArsaRExCH.Model.AdminSetupInit", b =>
                {
                    b.Property<int>("AdminSetupInitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminSetupInitId"));

                    b.Property<bool>("ShowAdminSetupPopUp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("AdminSetupInitId");

                    b.ToTable("adminSetupInits");
                });

            modelBuilder.Entity("ArsaRExCH.Model.AdminWarningMessage", b =>
                {
                    b.Property<int>("AdminWarningMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminWarningMessageId"));

                    b.Property<string>("AdminUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.HasKey("AdminWarningMessageId");

                    b.ToTable("adminWarningMessages");
                });

            modelBuilder.Entity("ArsaRExCH.Model.AirDrop", b =>
                {
                    b.Property<int>("AirDropID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirDropID"));

                    b.Property<int>("AirDropBalance")
                        .HasColumnType("int");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DailyClickCount")
                        .HasColumnType("int");

                    b.Property<int>("HowMannyClickInTottal")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastClickDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeClick")
                        .HasColumnType("datetime2");

                    b.HasKey("AirDropID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("AirDrops");
                });

            modelBuilder.Entity("ArsaRExCH.Model.AirDropFaq", b =>
                {
                    b.Property<int>("AirDropFaqId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirDropFaqId"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.HasKey("AirDropFaqId");

                    b.ToTable("airDropFaqs");
                });

            modelBuilder.Entity("ArsaRExCH.Model.BanedCountries", b =>
                {
                    b.Property<int>("BanedCountriesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BanedCountriesId"));

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAdressToBann")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BanedCountriesId");

                    b.ToTable("BanedCountris");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Bet", b =>
                {
                    b.Property<int>("BetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BetId"));

                    b.Property<double>("BetAmountBtc")
                        .HasColumnType("float");

                    b.Property<double>("BetAmountETH")
                        .HasColumnType("float");

                    b.Property<string>("BetSignutare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BtcPriceExpireBet")
                        .HasColumnType("float");

                    b.Property<double>("BtcPriceNow")
                        .HasColumnType("float");

                    b.Property<DateTime>("CompletedTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("EthPriceExpireBet")
                        .HasColumnType("float");

                    b.Property<double>("EthPriceNow")
                        .HasColumnType("float");

                    b.Property<DateTime>("HitDateBTC")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HitDateETH")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ISDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBetActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OpendBetAtricle")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserIdSec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("WiningAmount")
                        .HasColumnType("float");

                    b.Property<double?>("WiningAmountEth")
                        .HasColumnType("float");

                    b.HasKey("BetId");

                    b.ToTable("Bet");
                });

            modelBuilder.Entity("ArsaRExCH.Model.BitcoinPool", b =>
                {
                    b.Property<int>("BitcoinPoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BitcoinPoolId"));

                    b.Property<string>("AdminUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Daterefilled")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPoolActive")
                        .HasColumnType("bit");

                    b.Property<double>("PoolCurrentBalance")
                        .HasColumnType("float");

                    b.Property<string>("PoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PoolOpenedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SuspendDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BitcoinPoolId");

                    b.ToTable("BitcoinPools");
                });

            modelBuilder.Entity("ArsaRExCH.Model.BitcoinPoolTransactions", b =>
                {
                    b.Property<int>("BitcoinPoolTransactionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BitcoinPoolTransactionsId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TxHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("receiverAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BitcoinPoolTransactionsId");

                    b.ToTable("poolTransactions");
                });

            modelBuilder.Entity("ArsaRExCH.Model.LiveChat", b =>
                {
                    b.Property<int>("LiveChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LiveChatId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeAndDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LiveChatId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("lifeChat");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<string>("PairName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Pair", b =>
                {
                    b.Property<int>("PairID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PairID"));

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("ListedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NetworkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaiName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PairID");

                    b.ToTable("Pair");

                    b.HasData(
                        new
                        {
                            PairID = 1,
                            ListPrice = 100.0,
                            ListedDate = new DateTime(2024, 11, 3, 14, 37, 30, 75, DateTimeKind.Local).AddTicks(7154),
                            NetworkName = "BTC",
                            PaiName = "BTC"
                        },
                        new
                        {
                            PairID = 2,
                            ListPrice = 200.0,
                            ListedDate = new DateTime(2024, 11, 3, 14, 37, 30, 75, DateTimeKind.Local).AddTicks(7204),
                            NetworkName = "BNB",
                            PaiName = "BNB"
                        },
                        new
                        {
                            PairID = 3,
                            ListPrice = 300.0,
                            ListedDate = new DateTime(2024, 11, 3, 14, 37, 30, 75, DateTimeKind.Local).AddTicks(7208),
                            NetworkName = "ETH",
                            PaiName = "ETH"
                        });
                });

            modelBuilder.Entity("ArsaRExCH.Model.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Reply", b =>
                {
                    b.Property<int>("ReplyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReplyID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RepliedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserIdRelied")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReplyID");

                    b.HasIndex("PostId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Trade", b =>
                {
                    b.Property<string>("TradeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BitcoinPoolId")
                        .HasColumnType("int");

                    b.Property<string>("BitcoinPriceAtTheTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMarketBuy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTradeDone")
                        .HasColumnType("bit");

                    b.Property<double?>("RequestPriceFOrOrderBuy")
                        .HasColumnType("float");

                    b.Property<double>("SymbolII")
                        .HasColumnType("float");

                    b.Property<double>("TradeFee")
                        .HasColumnType("float");

                    b.Property<int>("TradePair")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<double>("symbolI")
                        .HasColumnType("float");

                    b.HasKey("TradeId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("BitcoinPoolId");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("ArsaRExCH.Model.TradeFee", b =>
                {
                    b.Property<string>("TradeFeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BitcoinWalletExchange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FeeInBtc")
                        .HasColumnType("float");

                    b.Property<string>("SetByAdminId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TradeFeeId");

                    b.ToTable("tradeFees");
                });

            modelBuilder.Entity("ArsaRExCH.Model.UserDatesRecord", b =>
                {
                    b.Property<int>("UserDatesRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserDatesRecordId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserIpAdressPublic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIpAdressX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserLoggedInDates")
                        .HasColumnType("datetime2");

                    b.HasKey("UserDatesRecordId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserDatesRecords");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Wallet", b =>
                {
                    b.Property<int>("WalletID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletID"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PairName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeedPhrase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIDSec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WalletID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "dfda0d02-4513-4e5c-90cd-1047f84ec069",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "f2bfac30-d800-4bb6-961c-1e7ce1419ce3",
                            RoleId = "dfda0d02-4513-4e5c-90cd-1047f84ec069"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ArsaRExCH.Model.AirDrop", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("UserAirdops")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ArsaRExCH.Model.LiveChat", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "user")
                        .WithMany("liveChats")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Post", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "Admin")
                        .WithMany("Posts")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Reply", b =>
                {
                    b.HasOne("ArsaRExCH.Model.Post", "Post")
                        .WithMany("Replies")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Trade", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "User")
                        .WithMany("Trade")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArsaRExCH.Model.BitcoinPool", "BitcoinPool")
                        .WithMany("Trades")
                        .HasForeignKey("BitcoinPoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BitcoinPool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArsaRExCH.Model.UserDatesRecord", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("UserLoginDates")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Wallet", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArsaRExCH.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ArsaRExCH.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArsaRExCH.Data.ApplicationUser", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Trade");

                    b.Navigation("UserAirdops");

                    b.Navigation("UserLoginDates");

                    b.Navigation("Wallets");

                    b.Navigation("liveChats");
                });

            modelBuilder.Entity("ArsaRExCH.Model.BitcoinPool", b =>
                {
                    b.Navigation("Trades");
                });

            modelBuilder.Entity("ArsaRExCH.Model.Post", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
