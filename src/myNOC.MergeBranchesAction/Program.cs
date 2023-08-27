using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using myNOC.MergeBranchesAction;
using myNOC.MergeBranchesAction.Extensions;

[assembly: InternalsVisibleTo("myNOC.Tests.MergeBranchesAction")]

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMergeBranchesActionServices();

using var host = builder.Build();

var parser = ActionInput.ParseArguments(args, host.Services);
