using MiceGym_APIs.DAO;

var builder = WebApplication.CreateBuilder(args);

// Adicione os servi�os necess�rios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro do CaixaDAO
builder.Services.AddTransient<CaixaDAO>();
builder.Services.AddTransient<ClienteDAO>();
builder.Services.AddScoped<EquipamentoDAO>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
