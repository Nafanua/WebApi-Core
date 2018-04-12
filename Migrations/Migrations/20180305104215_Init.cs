using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfData = table.Column<int>(maxLength: 10, nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivationCode = table.Column<Guid>(nullable: false),
                    DateOfRegistration = table.Column<DateTime>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    EmailIsValidate = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: false),
                    SecondName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComplitionStatus = table.Column<string>(maxLength: 10, nullable: false),
                    DatasourceId = table.Column<int>(nullable: true),
                    Durotation = table.Column<TimeSpan>(maxLength: 30, nullable: false),
                    Error = table.Column<string>(maxLength: 300, nullable: true),
                    StartDate = table.Column<DateTime>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Datasources_DatasourceId",
                        column: x => x.DatasourceId,
                        principalTable: "Datasources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 200, nullable: true),
                    CategoryUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Comments = table.Column<string>(maxLength: 500, nullable: true),
                    DatasourceId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EnclosureUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Fulltext = table.Column<string>(nullable: true),
                    Guid = table.Column<string>(maxLength: 500, nullable: true),
                    Image = table.Column<string>(maxLength: 500, nullable: true),
                    ItemExternalId = table.Column<byte[]>(maxLength: 80, nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    PubDate = table.Column<DateTime>(maxLength: 30, nullable: false),
                    SourceUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Tags = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Datasources_DatasourceId",
                        column: x => x.DatasourceId,
                        principalTable: "Datasources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_DatasourceId",
                table: "Logs",
                column: "DatasourceId");

            migrationBuilder.CreateIndex(
                name: "IX_News_DatasourceId",
                table: "News",
                column: "DatasourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Datasources");
        }
    }
}
