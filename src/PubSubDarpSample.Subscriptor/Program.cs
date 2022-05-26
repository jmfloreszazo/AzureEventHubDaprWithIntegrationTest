var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddDapr();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{

    endpoints.MapSubscribeHandler();
    endpoints.MapControllers();
});

app.Run();

public partial class Program { }