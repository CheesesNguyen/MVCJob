
using MVCJob.Job;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddQuartz(q =>
{
    var autoDeliveryOrderJobKey = new JobKey("HelloWorld");
    q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(autoDeliveryOrderJobKey));
    q.AddTrigger(opts => opts
      .ForJob(autoDeliveryOrderJobKey)
      .WithIdentity("HelloWorld-Trigger")
      .WithCronSchedule("0 0 0 ? * *", x => x //job fire at 0h:0p every day / month
        .InTimeZone(TimeZoneInfo.Utc)) //because timezone is UTC => want fire at 7h am, cron expression must be 0h pm
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
