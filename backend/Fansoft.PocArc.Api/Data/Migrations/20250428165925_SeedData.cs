using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fansoft.PocArc.Api.Data.Migrations;

    /// <inheritdoc />
public partial class SeedData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            INSERT INTO Customers (Id, Name, Email, Phone) VALUES
            (NEWID(), 'John Doe', 'john.doe@example.com', '123-456-7890'),
            (NEWID(), 'Jane Smith', 'jane.smith@example.com', '987-654-3210'),
            (NEWID(), 'Alice Johnson', 'alice.johnson@example.com', '555-123-4567'),
            (NEWID(), 'Bob Brown', 'bob.brown@example.com', '444-555-6666'),
            (NEWID(), 'Charlie Davis', 'charlie.davis@example.com', '333-222-1111'),
            (NEWID(), 'Emily White', 'emily.white@example.com', '222-333-4444'),
            (NEWID(), 'Daniel Lewis', 'daniel.lewis@example.com', '777-888-9999'),
            (NEWID(), 'Sophia Hall', 'sophia.hall@example.com', '666-777-8888'),
            (NEWID(), 'James Allen', 'james.allen@example.com', '111-222-3333'),
            (NEWID(), 'Olivia Young', 'olivia.young@example.com', '000-111-2222'),
            (NEWID(), 'Liam Hernandez', 'liam.hernandez@example.com', '999-000-1111'),
            (NEWID(), 'Isabella King', 'isabella.king@example.com', '888-999-0000'),
            (NEWID(), 'Noah Wright', 'noah.wright@example.com', '777-666-5555'),
            (NEWID(), 'Mia Lopez', 'mia.lopez@example.com', '666-555-4444'),
            (NEWID(), 'Lucas Scott', 'lucas.scott@example.com', '555-444-3333'),
            (NEWID(), 'Amelia Green', 'amelia.green@example.com', '444-333-2222'),
            (NEWID(), 'Benjamin Adams', 'benjamin.adams@example.com', '333-222-1110'),
            (NEWID(), 'Charlotte Baker', 'charlotte.baker@example.com', '222-111-0000'),
            (NEWID(), 'Elijah Nelson', 'elijah.nelson@example.com', '111-000-9999'),
            (NEWID(), 'Harper Carter', 'harper.carter@example.com', '000-999-8888'),
            (NEWID(), 'William Mitchell', 'william.mitchell@example.com', '999-888-7777'),
            (NEWID(), 'Evelyn Perez', 'evelyn.perez@example.com', '888-777-6666'),
            (NEWID(), 'James Roberts', 'james.roberts@example.com', '777-666-5550'),
            (NEWID(), 'Avery Turner', 'avery.turner@example.com', '666-555-4440'),
            (NEWID(), 'Alexander Phillips', 'alexander.phillips@example.com', '555-444-3330'),
            (NEWID(), 'Ella Campbell', 'ella.campbell@example.com', '444-333-2220'),
            (NEWID(), 'Henry Parker', 'henry.parker@example.com', '333-222-1111'),
            (NEWID(), 'Grace Evans', 'grace.evans@example.com', '222-111-0001'),
            (NEWID(), 'Sebastian Edwards', 'sebastian.edwards@example.com', '111-000-9991'),
            (NEWID(), 'Chloe Collins', 'chloe.collins@example.com', '000-999-8881'),
            (NEWID(), 'Jack Stewart', 'jack.stewart@example.com', '999-888-7771'),
            (NEWID(), 'Lily Sanchez', 'lily.sanchez@example.com', '888-777-6661'),
            (NEWID(), 'Samuel Morris', 'samuel.morris@example.com', '777-666-5551'),
            (NEWID(), 'Aria Rogers', 'aria.rogers@example.com', '666-555-4441'),
            (NEWID(), 'Matthew Reed', 'matthew.reed@example.com', '555-444-3331'),
            (NEWID(), 'Victoria Cook', 'victoria.cook@example.com', '444-333-2221'),
            (NEWID(), 'Joseph Morgan', 'joseph.morgan@example.com', '333-222-1112'),
            (NEWID(), 'Sofia Bell', 'sofia.bell@example.com', '222-111-0002'),
            (NEWID(), 'David Murphy', 'david.murphy@example.com', '111-000-9992'),
            (NEWID(), 'Scarlett Bailey', 'scarlett.bailey@example.com', '000-999-8882'),
            (NEWID(), 'Andrew Rivera', 'andrew.rivera@example.com', '999-888-7772'),
            (NEWID(), 'Madison Cooper', 'madison.cooper@example.com', '888-777-6662'),
            (NEWID(), 'Gabriel Richardson', 'gabriel.richardson@example.com', '777-666-5552'),
            (NEWID(), 'Zoey Cox', 'zoey.cox@example.com', '666-555-4442'),
            (NEWID(), 'Logan Howard', 'logan.howard@example.com', '555-444-3332'),
            (NEWID(), 'Penelope Ward', 'penelope.ward@example.com', '444-333-2222'),
            (NEWID(), 'Dylan Peterson', 'dylan.peterson@example.com', '333-222-1113'),
            (NEWID(), 'Riley Gray', 'riley.gray@example.com', '222-111-0003'),
            (NEWID(), 'Nathan Ramirez', 'nathan.ramirez@example.com', '111-000-9993'),
            (NEWID(), 'Layla James', 'layla.james@example.com', '000-999-8883');
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"DELETE FROM Customers;");
    }
}
