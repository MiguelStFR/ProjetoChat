using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using ProjetoChat.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Set environment variable for Google credentials (use private key json file’s path)
var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "Configurations", "serviceAccountKey.json");
System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

//// Initialize Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault()
});

//// Add Firestore DB Service (use Project ID)
builder.Services.AddSingleton(FirestoreDb.Create("chatapi-bb9e2"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();