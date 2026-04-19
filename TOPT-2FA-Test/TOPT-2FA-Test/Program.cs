using OtpNet;
using QRCoder;

var builder = WebApplication.CreateBuilder(args);

byte[] secretKey = KeyGeneration.GenerateRandomKey(); // 20 bytes by default (SHA-1)
string base32Secret = Base32Encoding.ToString(secretKey);

const string issuer = "OtpTest";
const string accountName = "gorges@test.com";

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("opt/qrcode", () =>
{
    string escapedIssuer = Uri.EscapeDataString(issuer);
    string escapedUser = Uri.EscapeDataString(accountName);

    var qrCodeData = new QRCodeGenerator()
    .CreateQrCode($"otpauth://totp/{escapedIssuer}:{escapedUser}?secret={base32Secret}&issuer={escapedIssuer}&digits=6&period=30",
    QRCodeGenerator.ECCLevel.Q);
    var qrCode = new PngByteQRCode(qrCodeData);
    var qrCodeBytes = qrCode.GetGraphic(10);

    string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
    Directory.CreateDirectory(imagesPath);

    string filePath = Path.Combine(imagesPath, "qrcode.png");
    File.WriteAllBytes(filePath, qrCodeBytes);

    return Results.File(qrCodeBytes, "image/png");
});

app.MapPost("otp/validate", (ValidateRequest request) =>
{
    var totp = new Totp(secretKey);
    bool isValid = totp.VerifyTotp(request.Code, out var timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

    return Results.Ok(new { Valid = isValid });
});

app.Run();

internal sealed record ValidateRequest(string Code);