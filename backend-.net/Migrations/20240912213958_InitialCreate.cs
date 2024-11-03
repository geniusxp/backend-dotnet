using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geniusxp_backend_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GXP_EVENT",
                columns: table => new
                {
                    id_event = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_event = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_event_type = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    url_image = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GXP_EVENT", x => x.id_event);
                });

            migrationBuilder.CreateTable(
                name: "TB_GXP_USER",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ds_password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nr_cpf = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    dt_birth = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    url_avatar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_interests = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GXP_USER", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "TB_GXP_EVENT_DAY",
                columns: table => new
                {
                    id_event_day = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_start = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    dt_end = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    url_transmission = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_event = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GXP_EVENT_DAY", x => x.id_event_day);
                    table.ForeignKey(
                        name: "FK_TB_GXP_EVENT_DAY_TB_GXP_EVENT_id_event",
                        column: x => x.id_event,
                        principalTable: "TB_GXP_EVENT",
                        principalColumn: "id_event",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_GXP_TICKET_TYPE",
                columns: table => new
                {
                    id_ticket_type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    vl_price = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    ds_category = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nr_quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    nr_sold = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dt_finished_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    id_event = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GXP_TICKET_TYPE", x => x.id_ticket_type);
                    table.ForeignKey(
                        name: "FK_TB_GXP_TICKET_TYPE_TB_GXP_EVENT_id_event",
                        column: x => x.id_event,
                        principalTable: "TB_GXP_EVENT",
                        principalColumn: "id_event",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_GXP_TICKET",
                columns: table => new
                {
                    id_ticket = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_use = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    dt_issued = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    nr_ticket = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    id_ticket_type = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_user = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GXP_TICKET", x => x.id_ticket);
                    table.ForeignKey(
                        name: "FK_TB_GXP_TICKET_TB_GXP_TICKET_TYPE_id_ticket_type",
                        column: x => x.id_ticket_type,
                        principalTable: "TB_GXP_TICKET_TYPE",
                        principalColumn: "id_ticket_type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_GXP_TICKET_TB_GXP_USER_id_user",
                        column: x => x.id_user,
                        principalTable: "TB_GXP_USER",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_EVENT_DAY_id_event",
                table: "TB_GXP_EVENT_DAY",
                column: "id_event");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_TICKET_id_ticket_type",
                table: "TB_GXP_TICKET",
                column: "id_ticket_type");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_TICKET_id_user",
                table: "TB_GXP_TICKET",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_TICKET_nr_ticket",
                table: "TB_GXP_TICKET",
                column: "nr_ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_TICKET_TYPE_id_event",
                table: "TB_GXP_TICKET_TYPE",
                column: "id_event");

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_USER_ds_email",
                table: "TB_GXP_USER",
                column: "ds_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_GXP_USER_nr_cpf",
                table: "TB_GXP_USER",
                column: "nr_cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_GXP_EVENT_DAY");

            migrationBuilder.DropTable(
                name: "TB_GXP_TICKET");

            migrationBuilder.DropTable(
                name: "TB_GXP_TICKET_TYPE");

            migrationBuilder.DropTable(
                name: "TB_GXP_USER");

            migrationBuilder.DropTable(
                name: "TB_GXP_EVENT");
        }
    }
}
