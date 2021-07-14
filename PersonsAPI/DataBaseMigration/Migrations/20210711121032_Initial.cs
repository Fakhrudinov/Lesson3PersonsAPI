using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseMigration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Company = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Age", "Company", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 42, "Quisque Ac Libero LLP", "ligula@necluctus.edu", "Veda", "Richmond" },
                    { 28, 35, "Iaculis Quis Consulting", "felis@conguea.org", "Vernon", "Cardenas" },
                    { 29, 63, "Integer Urna Institute", "Proin@feugiatnecdiam.org", "Murphy", "Weaver" },
                    { 30, 39, "Orci Industries", "facilisis.facilisis.magna@loremutaliquam.net", "Xena", "Hart" },
                    { 31, 30, "Ante Foundation", "Proin.ultrices.Duis@lacuspede.com", "Ivor", "Lara" },
                    { 32, 53, "Eget Industries", "et.magnis@Sed.edu", "Dana", "Merritt" },
                    { 33, 45, "Lorem Vehicula Et Foundation", "elit.Nulla@magna.edu", "Brielle", "Woodward" },
                    { 34, 49, "Magna Suspendisse Consulting", "et@nislsem.co.uk", "Hasad", "Duran" },
                    { 35, 32, "Eros Institute", "Proin.sed.turpis@imperdiet.co.uk", "Quamar", "Moses" },
                    { 36, 58, "Aenean Massa Consulting", "nisl.sem.consequat@idnunc.co.uk", "Scarlet", "Barlow" },
                    { 37, 47, "Mauris Associates", "urna@mauris.org", "Courtney", "Foley" },
                    { 38, 40, "Id Nunc Interdum LLC", "Cras@Nullam.org", "Kennedy", "Shields" },
                    { 39, 30, "Pellentesque Ultricies Associates", "metus.sit@lorem.ca", "Eve", "Maynard" },
                    { 40, 24, "Nulla Tincidunt Industries", "Nullam@pretium.ca", "Debra", "Ellis" },
                    { 41, 48, "Id Consulting", "ornare@at.net", "Vivian", "Mcguire" },
                    { 42, 48, "Netus Et Inc.", "tempor.augue@purusNullam.com", "Jason", "Mckinney" },
                    { 43, 61, "Hendrerit Institute", "fringilla@Proinsed.co.uk", "Patrick", "Small" },
                    { 44, 55, "Penatibus Corp.", "scelerisque.scelerisque@velit.org", "Drew", "Travis" },
                    { 45, 41, "Quis Diam Pellentesque PC", "Suspendisse@aliquet.net", "Burke", "Miller" },
                    { 46, 55, "Lorem Ipsum Dolor Corp.", "Class.aptent.taciti@mauris.edu", "Ralph", "Medina" },
                    { 47, 33, "Euismod Est Arcu Institute", "at.velit.Cras@aptenttacitisociosqu.net", "Alana", "Madden" },
                    { 48, 37, "Purus PC", "magna.Duis@Phasellus.org", "Salvador", "Cohen" },
                    { 27, 34, "Facilisis Eget Ipsum Inc.", "elit.pretium.et@malesuadafamesac.com", "Mohammad", "Thompson" },
                    { 26, 48, "Interdum Inc.", "Suspendisse.eleifend@Crasdolor.com", "Kerry", "Oneil" },
                    { 25, 26, "Enim Incorporated", "magna@felisegetvarius.net", "Hamilton", "Kidd" },
                    { 24, 29, "Dui Augue Eu Limited", "tincidunt@eget.edu", "Clio", "Shaffer" },
                    { 2, 31, "Nulla Facilisi Foundation", "feugiat.metus@penatibuset.org", "Demetria", "Andrews" },
                    { 3, 63, "Et Associates", "neque.Sed.eget@non.co.uk", "Byron", "Holmes" },
                    { 4, 23, "Vel Institute", "egestas.ligula@ultricesDuisvolutpat.ca", "Alexander", "Cummings" },
                    { 5, 64, "Eu Nibh Vulputate Company", "justo.nec.ante@nonummyFusce.ca", "Melinda", "Miles" },
                    { 6, 35, "Nec Diam Incorporated", "iaculis@afeugiat.edu", "Dustin", "Beck" },
                    { 7, 22, "Enim Corp.", "ipsum@vulputatelacus.co.uk", "Ralph", "Maddox" },
                    { 8, 57, "Sodales At Velit Corp.", "lectus.a.sollicitudin@nuncQuisque.com", "Levi", "Zamora" },
                    { 9, 37, "Id Mollis Nec LLC", "Phasellus@Craspellentesque.org", "Driscoll", "Estrada" },
                    { 10, 59, "Donec Tincidunt Donec Industries", "lacus.Mauris@semper.ca", "Hiram", "Mejia" },
                    { 11, 65, "Lectus Justo Ltd", "Integer.vitae.nibh@nibh.co.uk", "Mason", "Jefferson" },
                    { 49, 56, "Lectus Justo Incorporated", "adipiscing.Mauris.molestie@liberoduinec.ca", "Jenette", "Dejesus" },
                    { 12, 52, "Tristique Ac Ltd", "id@faucibusleoin.net", "Nigel", "Rich" },
                    { 14, 29, "Rhoncus Id Corporation", "Curabitur.ut.odio@anteMaecenasmi.co.uk", "Wallace", "Gross" },
                    { 15, 59, "Vivamus Corporation", "magna.nec.quam@lobortis.net", "Arden", "Rivers" },
                    { 16, 54, "Imperdiet Dictum Magna Foundation", "faucibus.Morbi.vehicula@ipsumdolor.edu", "Vincent", "Fox" },
                    { 17, 27, "Mattis Foundation", "ac@scelerisquesedsapien.org", "Aphrodite", "Randolph" },
                    { 18, 25, "Rutrum Non Hendrerit Consulting", "montes@scelerisque.edu", "Alisa", "Riggs" },
                    { 19, 61, "Ut LLC", "velit.Quisque.varius@aliquetmolestie.net", "Jaime", "Lott" },
                    { 20, 61, "Curabitur Sed Tortor Ltd", "arcu.eu.odio@congue.ca", "Jamalia", "Buchanan" },
                    { 21, 43, "In Inc.", "Integer.sem.elit@bibendumsedest.net", "Raya", "Mckenzie" },
                    { 22, 48, "Nec Foundation", "Cras.eget.nisi@Vestibulum.edu", "Dante", "Blackwell" },
                    { 23, 60, "Augue Scelerisque Institute", "facilisis@doloregestas.co.uk", "Kato", "Dickson" },
                    { 13, 58, "Lacus Varius Et Associates", "enim@ultricesDuisvolutpat.edu", "Tarik", "Hughes" },
                    { 50, 24, "Imperdiet Dictum LLP", "massa.Vestibulum@lectuspede.ca", "Ramona", "Gilliam" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
