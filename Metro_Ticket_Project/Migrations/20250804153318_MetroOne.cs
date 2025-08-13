using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metro_Ticket_Project.Migrations
{
    /// <inheritdoc />
    public partial class MetroOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    route_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    total_distance = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    train_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    train_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false, defaultValue: 300),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from_station_id = table.Column<int>(type: "int", nullable: false),
                    to_station_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    distance = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fares_stations_from_station_id",
                        column: x => x.from_station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fares_stations_to_station_id",
                        column: x => x.to_station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "route_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    station_id = table.Column<int>(type: "int", nullable: false),
                    station_order = table.Column<int>(type: "int", nullable: false),
                    distance_from_previous = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_route_details_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_route_details_stations_station_id",
                        column: x => x.station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trip_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    train_id = table.Column<int>(type: "int", nullable: false),
                    departure_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arrival_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Scheduled"),
                    start = table.Column<string>(type: "varchar(50)", nullable: false),
                    end = table.Column<string>(type: "varchar(50)", nullable: false),
                    TrainId1 = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trips_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trips_trains_TrainId1",
                        column: x => x.TrainId1,
                        principalTable: "trains",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_trips_trains_train_id",
                        column: x => x.train_id,
                        principalTable: "trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "booking_histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticket_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    from_station_id = table.Column<int>(type: "int", nullable: false),
                    to_station_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    payment_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    ticket_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Booked"),
                    booking_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    booking_id = table.Column<long>(type: "bigint", nullable: false),
                    Source = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Destination = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Fare = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_histories_stations_from_station_id",
                        column: x => x.from_station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_booking_histories_stations_to_station_id",
                        column: x => x.to_station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_booking_histories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    priority = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Medium"),
                    u_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    u_address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    phone_num = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    msg = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Response = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_complaints_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metro_cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    card_no = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    card_status = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    pin = table.Column<int>(type: "int", nullable: false),
                    icard = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    icard_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metro_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_metro_cards_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trip_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    trip_id = table.Column<int>(type: "int", nullable: false),
                    station_id = table.Column<int>(type: "int", nullable: false),
                    station_order = table.Column<int>(type: "int", nullable: false),
                    arrival_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    departure_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trip_details_stations_station_id",
                        column: x => x.station_id,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trip_details_trips_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    complaint_id = table.Column<int>(type: "int", nullable: false),
                    reply_text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    replied_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    msg = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_replies_admins_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replies_complaints_complaint_id",
                        column: x => x.complaint_id,
                        principalTable: "complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    metro_card_id = table.Column<int>(type: "int", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Source = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Destination = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_histories_metro_cards_metro_card_id",
                        column: x => x.metro_card_id,
                        principalTable: "metro_cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_histories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_email",
                table: "admins",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_histories_from_station_id",
                table: "booking_histories",
                column: "from_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_histories_to_station_id",
                table: "booking_histories",
                column: "to_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_histories_user_id",
                table: "booking_histories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHistory_BookingDate",
                table: "booking_histories",
                column: "booking_date");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_user_id",
                table: "complaints",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_from_station_id_to_station_id",
                table: "fares",
                columns: new[] { "from_station_id", "to_station_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fares_to_station_id",
                table: "fares",
                column: "to_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_histories_metro_card_id",
                table: "histories",
                column: "metro_card_id");

            migrationBuilder.CreateIndex(
                name: "IX_histories_user_id",
                table: "histories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_History_TransactionDate",
                table: "histories",
                column: "transaction_date");

            migrationBuilder.CreateIndex(
                name: "IX_metro_cards_card_no",
                table: "metro_cards",
                column: "card_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metro_cards_user_id",
                table: "metro_cards",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_replies_admin_id",
                table: "replies",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_replies_complaint_id",
                table: "replies",
                column: "complaint_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_details_route_id_station_order",
                table: "route_details",
                columns: new[] { "route_id", "station_order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_details_station_id",
                table: "route_details",
                column: "station_id");

            migrationBuilder.CreateIndex(
                name: "IX_routes_route_code",
                table: "routes",
                column: "route_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stations_code",
                table: "stations",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trains_train_number",
                table: "trains",
                column: "train_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trip_details_station_id",
                table: "trip_details",
                column: "station_id");

            migrationBuilder.CreateIndex(
                name: "IX_TripDetails_Trip_StationOrder",
                table: "trip_details",
                columns: new[] { "trip_id", "station_order" });

            migrationBuilder.CreateIndex(
                name: "IX_trips_route_id",
                table: "trips",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_train_id",
                table: "trips",
                column: "train_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_TrainId1",
                table: "trips",
                column: "TrainId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_histories");

            migrationBuilder.DropTable(
                name: "fares");

            migrationBuilder.DropTable(
                name: "histories");

            migrationBuilder.DropTable(
                name: "replies");

            migrationBuilder.DropTable(
                name: "route_details");

            migrationBuilder.DropTable(
                name: "trip_details");

            migrationBuilder.DropTable(
                name: "metro_cards");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "complaints");

            migrationBuilder.DropTable(
                name: "stations");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "trains");
        }
    }
}
