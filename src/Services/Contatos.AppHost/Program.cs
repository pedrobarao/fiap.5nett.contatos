using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres");

var contatosDb = postgres.AddDatabase("contatos");

builder.AddProject<Contatos_Cadastro_Api>("contatos-cadastro-api")
    .WithReference(rabbitMq)
    .WithReference(contatosDb);

builder.Build().Run();