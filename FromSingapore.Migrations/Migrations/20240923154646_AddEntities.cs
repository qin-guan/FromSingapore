using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FromSingapore.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkExpirations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkExpirations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkPasswords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkPasswords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkVisitLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Max = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkVisitLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BuiltIn = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreeDomains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeDomains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeDomains_Domains_Id",
                        column: x => x.Id,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ShortCode = table.Column<string>(type: "text", nullable: false),
                    OriginalUri = table.Column<string>(type: "text", nullable: false),
                    IsBanned = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LinkVisitLimitId = table.Column<Guid>(type: "uuid", nullable: true),
                    LinkExpirationId = table.Column<Guid>(type: "uuid", nullable: true),
                    LinkPasswordId = table.Column<Guid>(type: "uuid", nullable: true),
                    DomainId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_LinkExpirations_LinkExpirationId",
                        column: x => x.LinkExpirationId,
                        principalTable: "LinkExpirations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Links_LinkPasswords_LinkPasswordId",
                        column: x => x.LinkPasswordId,
                        principalTable: "LinkPasswords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Links_LinkVisitLimits_LinkVisitLimitId",
                        column: x => x.LinkVisitLimitId,
                        principalTable: "LinkVisitLimits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DomainSubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainSubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainSubscriptionPlans_SubscriptionPlans_Id",
                        column: x => x.Id,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkSubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkSubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkSubscriptionPlans_SubscriptionPlans_Id",
                        column: x => x.Id,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LinkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkVisits_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DomainSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DomainSubscriptionPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainSubscriptions_DomainSubscriptionPlans_DomainSubscript~",
                        column: x => x.DomainSubscriptionPlanId,
                        principalTable: "DomainSubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DomainSubscriptions_Subscriptions_Id",
                        column: x => x.Id,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LinkSubscriptionPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkSubscriptions_LinkSubscriptionPlans_LinkSubscriptionPla~",
                        column: x => x.LinkSubscriptionPlanId,
                        principalTable: "LinkSubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkSubscriptions_Subscriptions_Id",
                        column: x => x.Id,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaidDomains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DomainSubscriptionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidDomains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidDomains_DomainSubscriptions_DomainSubscriptionId",
                        column: x => x.DomainSubscriptionId,
                        principalTable: "DomainSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaidDomains_Domains_Id",
                        column: x => x.Id,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainSubscriptions_DomainSubscriptionPlanId",
                table: "DomainSubscriptions",
                column: "DomainSubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_DomainId_ShortCode",
                table: "Links",
                columns: new[] { "DomainId", "ShortCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkExpirationId",
                table: "Links",
                column: "LinkExpirationId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkPasswordId",
                table: "Links",
                column: "LinkPasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkVisitLimitId",
                table: "Links",
                column: "LinkVisitLimitId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkSubscriptions_LinkSubscriptionPlanId",
                table: "LinkSubscriptions",
                column: "LinkSubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkVisits_LinkId",
                table: "LinkVisits",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidDomains_DomainSubscriptionId",
                table: "PaidDomains",
                column: "DomainSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AppUserId",
                table: "Subscriptions",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreeDomains");

            migrationBuilder.DropTable(
                name: "LinkSubscriptions");

            migrationBuilder.DropTable(
                name: "LinkVisits");

            migrationBuilder.DropTable(
                name: "PaidDomains");

            migrationBuilder.DropTable(
                name: "LinkSubscriptionPlans");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "DomainSubscriptions");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "LinkExpirations");

            migrationBuilder.DropTable(
                name: "LinkPasswords");

            migrationBuilder.DropTable(
                name: "LinkVisitLimits");

            migrationBuilder.DropTable(
                name: "DomainSubscriptionPlans");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
