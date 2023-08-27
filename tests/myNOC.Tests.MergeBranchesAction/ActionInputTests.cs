using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myNOC.MergeBranchesAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myNOC.Tests.MergeBranchesAction
{
	[TestClass]
	public class ActionInputTests
	{
		private IServiceCollection _services = default!;
		private IServiceProvider _serviceProvider = default!;

		[TestInitialize]
		public void TestInit()
		{
			_services = new ServiceCollection();
			_services.AddScoped<ILoggerFactory, LoggerFactory>();
			_serviceProvider = _services.BuildServiceProvider();
		}

		[TestMethod]
		public void ParseArguments_AllArgs_ReturnsValid()
		{
			//	Arrange
			var repository = "mergeBranches";
			var branch = "main";
			var pattern = "features/*";

			var args = new[] { "--repository", repository, "--branch", branch, "--pattern", pattern };

			//	Act
			var result = ActionInput.ParseArguments(args, _serviceProvider);
			var inputs = result.Value;

			//	Assert
			Assert.AreEqual(0, result.Errors.Count());
			Assert.AreEqual(repository, inputs.Repository);
			Assert.AreEqual(branch, inputs.Branch);
			Assert.AreEqual(pattern, inputs.Pattern);
		}

		[TestMethod]
		public void ParseArguments_MissingArgs_HasErrors()
		{
			//	Arrange
			var repository = "mergeBranches";
			var branch = "main";

			var args = new[] { "--repository", repository, "--branch", branch };

			//	Act
			var result = ActionInput.ParseArguments(args, _serviceProvider);

			//	Assert
			Assert.AreEqual(1, result.Errors.Count());

			var error = result.Errors.First();
			Assert.IsInstanceOfType(error, typeof(MissingRequiredOptionError));
			Assert.AreEqual("pattern", error.Cast<MissingRequiredOptionError>().NameInfo.LongName);
		}
	}
}
