using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;
using System.Text;
#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class PopulateBD : Migration
    {
        /// <inheritdoc />


        protected override void Up(MigrationBuilder mb)
        {
            using var hmac = new HMACSHA512();
            
            mb.Sql("INSERT INTO day(Name) VALUES ('Domingo')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Segunda')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Terça')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Quarta')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Quinta')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Sexta')");
            mb.Sql("INSERT INTO day(Name) VALUES ('Sabado')");

            //Profile

            mb.Sql("INSERT INTO profile(Name) VALUES ('Administrator')");
            mb.Sql("INSERT INTO profile(Name) VALUES ('Usuário')");

            //Users

            //mb.Sql("INSERT INTO [User](Login, ProfileId) VALUES ('admin', 1)");

            //Title
            mb.Sql("INSERT INTO Title(Name) VALUES ('Assistente')");
            mb.Sql("INSERT INTO Title(Name) VALUES ('Analista')");
            mb.Sql("INSERT INTO Title(Name) VALUES ('Coordenador')");
            mb.Sql("INSERT INTO Title(Name) VALUES ('Gerente')");

            //Schedule

            mb.Sql("INSERT INTO schedule(Name,Start,[End]) VALUES ('ADM','08:00','18:00')");

            //ScheduleDAY

            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (1,1)");
            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (2,1)");
            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (3,1)");
            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (4,1)");
            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (5,1)");
            mb.Sql("INSERT INTO scheduleday(DayId, ScheduleId ) VALUES (6,1)");

            //Marking

            mb.Sql("INSERT INTO Marking(Name) VALUES ('Entrada')");
            mb.Sql("INSERT INTO Marking(Name) VALUES ('Saida')");

            //Employee

            //mb.Sql("INSERT INTO employee(Name, Registration, TitleId, UserId, ScheduleId) VALUES ('Pedro Ramon', '25126', 1,1,1)");

            //MarkingEmployee

           // mb.Sql("INSERT INTO employeemarkings(EmployeeId, MarkingId, DateTime) VALUES (1,1,SYSDATETIME())");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
     }
}
