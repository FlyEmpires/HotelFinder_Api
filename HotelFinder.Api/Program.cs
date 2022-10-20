using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHotelService, HotelManager>();
builder.Services.AddSingleton<IHotelRepository, HotelRepository>();
builder.Services.AddSwaggerDocument(config =>
{
config.PostProcess = (doc => { doc.Info.Title = "Api For Hotels"; doc.Info.Version = "1.0.22";doc.Info.Contact = new NSwag.OpenApiContact() { Name = "Talha Acar", Url = "https://yemeksen.com" }; });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//}
app.UseHttpsRedirection();
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
//app.UseAuthorization();

app.MapRazorPages();

app.Run();
