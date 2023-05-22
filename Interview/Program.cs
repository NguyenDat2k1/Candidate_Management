
using Interview.Data;
using Interview.Models;
using Interview.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using CurrieTechnologies.Razor.SweetAlert2;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<userDetailSerivce>();
//builder.Services.AddSingleton<ContactController>();
//builder.Services.AddSingleton<SendMailController>();
//builder.Services.AddSingleton<AddPVController>();
//builder.Services.AddSingleton<JudgingController>();
//builder.Services.AddSingleton<SendResultController>();
//builder.Services.AddSingleton<HomeController>();
builder.Services.AddTransient<userDetailSerivce>();
builder.Services.AddTransient<ContactController>();
builder.Services.AddTransient<SendMailController>();
builder.Services.AddTransient<AddPVController>();
builder.Services.AddTransient<JudgingController>();
builder.Services.AddTransient<SendResultController>();
builder.Services.AddTransient<HomeController>();

builder.Services.AddHttpClient();


var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.Configure<MailSettings>(config.GetSection("MailSettings"));
//builder.Services.AddSingleton<IMailService, MailService>();
 builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddSweetAlert2();




var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();

}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
//Configuration.GetConnectionString("server=localhost;user id=root;password=20150601;port=3306;database=interview;")))
//CREATE DEFINER =`datnt`@`%` TRIGGER `update_user_id` AFTER INSERT ON `user` FOR EACH ROW BEGIN
//insert into interview.result  (user_id) select(userID) from user where userID = New.userID;
//insert into interview.in4meeting  (uv_ID) select(userID) from user where userID = New.userID;
//END