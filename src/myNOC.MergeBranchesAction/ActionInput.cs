using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace myNOC.MergeBranchesAction
{
	public class ActionInput
	{
		[Option('r', "repository", Required = true, HelpText = "The repository name, for example: \"weatherLink\".  Assign from `github.repository`.")]
		public string Repository { get; set; } = null!;

		[Option('b', "branch", Required = true, HelpText = "The branch you want to merge from, for example: \"main\".")]
		public string Branch { get; set; } = null!;

		[Option('p', "pattern", Required = true, HelpText = "The a pattern for branches you want to merge to, for example: \"feature/*\".")]
		public string Pattern { get; set; } = null!;

		internal static ParserResult<ActionInput> ParseArguments(string[] args, IServiceProvider services)
		{
			var parser = Parser.Default.ParseArguments<ActionInput>(args);
			parser.WithNotParsed(errors =>
			{
				services
				.GetRequiredService<ILoggerFactory>()
				.CreateLogger("myNOC.MergeBranchesAction.Program")
				.LogError("{Errors}", string.Join(Environment.NewLine, errors.Select(error => error.ToString())));
			});

			return parser;
		}
	}
}
