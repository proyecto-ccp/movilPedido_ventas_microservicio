using Microsoft.EntityFrameworkCore;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Comandos.DetallePedidos;
using Pedidos.Aplicacion.Comandos.Pedidos;
using Pedidos.Aplicacion.Consultas.DetallePedidos;
using Pedidos.Aplicacion.Consultas.Pedidos;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Dominio.Servicios.DetallePedidos;
using Pedidos.Dominio.Servicios.Pedidos;
using Pedidos.Infraestructura.Repositorios.DetallePedidos;
using Pedidos.Infraestructura.Repositorios.Pedidos;
using Pedidos.Infraestructura.RepositoriosGenericos.DetallePedidos;
using Pedidos.Infraestructura.RepositoriosGenericos.Pedidos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Pedidos
builder.Services.AddDbContext<PedidosDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDbContext")), ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(IRepositorioBasePedido<>), typeof(RepositorioBasePedido<>));
builder.Services.AddTransient<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IComandosPedido, ComandosPedido>();
builder.Services.AddScoped<IConsultasPedidos, ConsultasPedidos>();
builder.Services.AddScoped<CrearPedido>();
builder.Services.AddScoped<ActualizarPedido>();
builder.Services.AddScoped<ObtenerPedido>();
builder.Services.AddScoped<ListadoPedidosPorCliente>();
builder.Services.AddScoped<ListadoPedidosPorVendedor>();
builder.Services.AddScoped<ListadoPedidosPorEntregar>();

//DetallePedidos
builder.Services.AddDbContext<DetallesPedidoDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDbContext")), ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(IRepositorioBaseDetallePedido<>), typeof(RepositorioBaseDetallePedido<>));
builder.Services.AddTransient<IDetallePedidoRepositorio, DetallePedidoRepositorio>();
builder.Services.AddScoped<IComandosDetallePedido, ComandosDetallePedido>();
builder.Services.AddScoped<IConsultasDetallePedido, ConsultasDetallePedido>();
builder.Services.AddScoped<CrearDetallePedido>();
builder.Services.AddScoped<EliminarDetallePedido>();
builder.Services.AddScoped<ActualizarIdPedido>();
builder.Services.AddScoped<ObtenerDetallePedido>();
builder.Services.AddScoped<ObtenerDetallePedidoUsuario>();

builder.Services.AddHttpClient<IInventariosApiClient, InventariosApiClient>(client =>
{
    client.BaseAddress = new Uri("https://inventarios-596275467600.us-central1.run.app/");
});

builder.Services.AddHttpClient<IProductosApiClient, ProductosApiClient>(client =>
{
    client.BaseAddress = new Uri("https://productos-596275467600.us-central1.run.app/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }