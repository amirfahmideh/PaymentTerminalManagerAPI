using PaymentTerminalManager;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
app.MapOpenApi();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
// }
app.UseHttpsRedirection();

app.MapPost("/sendPrice", (Send parameter) =>
{
    TransactionOperation to = new TransactionOperation();
    var result = to.SendToTerminal(parameter.TerminalType, parameter.Packet);
    return result;
}).WithName("SendPrice");

app.MapPost("/refund", (Refund parameter) =>
{
    TransactionOperation to = new TransactionOperation();
    var result = to.RefundRequest(parameter.TerminalType, parameter.Packet);
    return result;
}).WithName("refund");

app.Run();
record Send(SupportedTerminal TerminalType, PaymentTerminalManager.dto.SendToTerminal Packet);
record Refund(SupportedTerminal TerminalType, PaymentTerminalManager.dto.RefundFromTerminal Packet);
