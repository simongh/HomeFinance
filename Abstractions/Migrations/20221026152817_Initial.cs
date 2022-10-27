using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HomeFinance.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "finance");

			migrationBuilder.CreateTable(
				name: "Accounts",
				schema: "finance",
				columns: table => new
				{
					AccountId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
					OpeningBalance = table.Column<decimal>(type: "TEXT", nullable: false),
					Balance = table.Column<decimal>(type: "TEXT", nullable: false),
					RowVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Accounts", x => x.AccountId);
				});

			migrationBuilder.CreateTable(
				name: "Categories",
				schema: "finance",
				columns: table => new
				{
					CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
					Description = table.Column<string>(type: "TEXT", nullable: true),
					ParentId = table.Column<int>(type: "INTEGER", nullable: true),
					RowVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.CategoryId);
					table.ForeignKey(
						name: "FK_Categories_Categories_ParentId",
						column: x => x.ParentId,
						principalSchema: "finance",
						principalTable: "Categories",
						principalColumn: "CategoryId");
				});

			migrationBuilder.CreateTable(
				name: "Payees",
				schema: "finance",
				columns: table => new
				{
					PayeeId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
					Description = table.Column<string>(type: "TEXT", nullable: true),
					RowVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Payees", x => x.PayeeId);
				});

			migrationBuilder.CreateTable(
				name: "Transactions",
				schema: "finance",
				columns: table => new
				{
					TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Created = table.Column<DateOnly>(type: "TEXT", nullable: false),
					Value = table.Column<decimal>(type: "TEXT", nullable: false),
					Memo = table.Column<string>(type: "TEXT", nullable: true),
					Status = table.Column<byte>(type: "INTEGER", nullable: false),
					Type = table.Column<byte>(type: "INTEGER", nullable: false),
					RowVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0),
					CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
					PayeeId = table.Column<int>(type: "INTEGER", nullable: true),
					AccountId = table.Column<int>(type: "INTEGER", nullable: false),
					LinkedTransactionId = table.Column<int>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Transactions", x => x.TransactionId);
					table.ForeignKey(
						name: "FK_Transactions_Accounts_AccountId",
						column: x => x.AccountId,
						principalSchema: "finance",
						principalTable: "Accounts",
						principalColumn: "AccountId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Transactions_Categories_CategoryId",
						column: x => x.CategoryId,
						principalSchema: "finance",
						principalTable: "Categories",
						principalColumn: "CategoryId");
					table.ForeignKey(
						name: "FK_Transactions_Payees_PayeeId",
						column: x => x.PayeeId,
						principalSchema: "finance",
						principalTable: "Payees",
						principalColumn: "PayeeId");
					table.ForeignKey(
						name: "FK_Transactions_Transactions_LinkedTransactionId",
						column: x => x.LinkedTransactionId,
						principalSchema: "finance",
						principalTable: "Transactions",
						principalColumn: "TransactionId");
				});

			migrationBuilder.CreateIndex(
				name: "IX_Categories_ParentId",
				schema: "finance",
				table: "Categories",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_Transactions_AccountId",
				schema: "finance",
				table: "Transactions",
				column: "AccountId");

			migrationBuilder.CreateIndex(
				name: "IX_Transactions_CategoryId",
				schema: "finance",
				table: "Transactions",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Transactions_LinkedTransactionId",
				schema: "finance",
				table: "Transactions",
				column: "LinkedTransactionId");

			migrationBuilder.CreateIndex(
				name: "IX_Transactions_PayeeId",
				schema: "finance",
				table: "Transactions",
				column: "PayeeId");

			migrationBuilder.Sql(
@"CREATE TRIGGER UpdateAccountVersion
AFTER UPDATE ON Accounts
BEGIN
	UPDATE Accounts SET
		RowVersion = RowVersion + 1
	WHERE rowid = NEW.rowid;
END;");
			migrationBuilder.Sql(
@"CREATE TRIGGER UpdateCategoryVersion
AFTER UPDATE ON Categories
BEGIN
	UPDATE Categories SET
		RowVersion = RowVersion + 1
	WHERE rowid = NEW.rowid;
END;");
			migrationBuilder.Sql(
@"CREATE TRIGGER UpdatePayeeVersion
AFTER UPDATE ON Payees
BEGIN
	UPDATE Payees SET
		RowVersion = RowVersion + 1
	WHERE rowid = NEW.rowid;
END;");
			migrationBuilder.Sql(
@"CREATE TRIGGER UpdateTransactionVersion
AFTER UPDATE ON Transactions
BEGIN
	UPDATE Transactions SET
		RowVersion = RowVersion + 1
	WHERE rowid = NEW.rowid;
END;");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Transactions",
				schema: "finance");

			migrationBuilder.DropTable(
				name: "Accounts",
				schema: "finance");

			migrationBuilder.DropTable(
				name: "Categories",
				schema: "finance");

			migrationBuilder.DropTable(
				name: "Payees",
				schema: "finance");
		}
	}
}