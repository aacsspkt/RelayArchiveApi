using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayArchive.Api.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "relay_infos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chain = table.Column<string>(type: "text", nullable: false),
                    emitter_address_hex = table.Column<string>(type: "text", nullable: false),
                    sequence = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    payload_hex = table.Column<string>(type: "text", nullable: false),
                    stream_escrow = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relay_infos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "signatures",
                columns: table => new
                {
                    value = table.Column<string>(type: "text", nullable: false),
                    relay_info_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signatures", x => x.value);
                    table.ForeignKey(
                        name: "FK_signatures_relay_infos_relay_info_id",
                        column: x => x.relay_info_id,
                        principalTable: "relay_infos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_relay_infos_chain_emitter_address_hex_sequence",
                table: "relay_infos",
                columns: new[] { "chain", "emitter_address_hex", "sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_signatures_relay_info_id",
                table: "signatures",
                column: "relay_info_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "signatures");

            migrationBuilder.DropTable(
                name: "relay_infos");
        }
    }
}
