using System;
using System.Linq;

namespace DirectedLaplacian
{

	class Options
	{
		[CommandLine.Option('i', "input", DefaultValue="../../../Data/sample.edges")]
		public string input {
			get;
			set;
		}

		[CommandLine.Option("dt", DefaultValue=1)]
		public double dt {
			get;
			set;
		}

		[CommandLine.Option('T', DefaultValue=1000)]
		public int T {
			get;
			set;
		}

		[CommandLine.Option("normalized", DefaultValue=false)]
		public bool normalied {
			get;
			set;
		}

		[CommandLine.Option("seed", DefaultValue=0)]
		public int seed {
			get;
			set;
		}
	}

	class MainClass
	{
		static readonly Options opts = new Options ();

		public static void Main (string[] args)
		{
			if (!CommandLine.Parser.Default.ParseArguments (args, opts)) {
				Console.WriteLine ("Failed to parse arguments");
			}
			for (int i = 0; i < opts.seed; i++) {
				Util.Xor128 ();
			}

			var G = new Digraph (opts.input);
			var xs = opts.normalied ? 
				NormalizedLaplacian.SecondEigenvector (G, opts.dt, opts.T) :
				Laplacian.SecondEigenvector (G, opts.dt, opts.T);

			for (int i = 0; i < G.n; i++) {
				Console.WriteLine($"{xs[i]}");
			}
		}
	}
}
