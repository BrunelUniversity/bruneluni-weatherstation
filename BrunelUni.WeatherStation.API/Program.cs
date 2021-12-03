using System;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/temperature", () => new { temprature=new Random().Next() });
app.MapGet("/humidity", () => new { temp=new Random().Next() });

app.Run();