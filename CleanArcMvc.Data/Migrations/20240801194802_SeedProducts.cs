using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArcMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert Into Products(Name,Description,Price,Stock,Image,CategoryId)" + 
                "Values('Caderno Espiral','Caderno Espiral de 100 folhas',7.45,50,'caderno.jpg',1)");
            mb.Sql("Insert Into Products(Name,Description,Price,Stock,Image,CategoryId)" +
                "Values('Estojo','Estojo escolar cinza',5.45,10,'estojo.jpg',1)");
            mb.Sql("Insert Into Products(Name,Description,Price,Stock,Image,CategoryId)" +
                "Values('Calculadora','Calculadora ciêntífica',17.45,5,'calculadora.jpg',2)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
