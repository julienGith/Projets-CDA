using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPS.DAL.Migrations
{
    public partial class AddColumnCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>("CodeHtml", "Questionnaire", "varchar(MAX)",
            unicode: false, maxLength: int.MaxValue, nullable: true);
        }

    }
}
