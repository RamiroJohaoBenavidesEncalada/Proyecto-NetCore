using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    public partial class ProyectoFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombres = table.Column<string>(type: "TEXT", nullable: false),
                    Cedula = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Ciudad = table.Column<string>(type: "TEXT", nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(type: "TEXT", nullable: false),
                    Usuario = table.Column<string>(type: "TEXT", nullable: false),
                    Contraseña = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetodoEntregaCompras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    MetodoEntrega = table.Column<string>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    CostoEntrega = table.Column<decimal>(type: "TEXT", nullable: false),
                    DireccionEntrega = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoEntregaCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoProductos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProductos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carritos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    MetodoEntregaCompraId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_MetodoEntregaCompras_MetodoEntregaCompraId",
                        column: x => x.MetodoEntregaCompraId,
                        principalTable: "MetodoEntregaCompras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Caducidad = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MarcaId = table.Column<string>(type: "TEXT", nullable: true),
                    TipoProductoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_TipoProductos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "TipoProductos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarritoItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarritoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<long>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritoItem_Carritos_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoItem_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrdenId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<long>(type: "INTEGER", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenItem_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenItem_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItem_CarritoId",
                table: "CarritoItem",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItem_ProductoId",
                table: "CarritoItem",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_ClienteId",
                table: "Carritos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_MetodoEntregaCompraId",
                table: "Ordenes",
                column: "MetodoEntregaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItem_OrdenId",
                table: "OrdenItem",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItem_ProductoId",
                table: "OrdenItem",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TipoProductoId",
                table: "Productos",
                column: "TipoProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoItem");

            migrationBuilder.DropTable(
                name: "OrdenItem");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "MetodoEntregaCompras");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "TipoProductos");
        }
    }
}
