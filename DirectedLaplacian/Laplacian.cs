using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace DirectedLaplacian
{
	static public class Laplacian
	{
		static public double[] FirstEigenvector(Digraph G) {
			var res = Enumerable.Repeat (1.0, G.n).ToArray ();
			return Vector.L2Normalize (res);
		}

		static public double[] SecondEigenvector(Digraph G, double dt, int T) {
			var Pi = new List<double[]> () {FirstEigenvector (G)};

			var xs = Vector.Random (G.n);
			for (int i = 0; i < T; ++i) {
				xs = Iterate (G, xs, dt, Pi);
			}
			return xs;
		}

		static public double RayleighQuotient(Digraph G, double[] xs) {
			Debug.Assert (xs.Length == G.n);

			double denominator = Vector.L2Norm (xs);
			double numerator = 0;

			for (int u = 0; u < G.n; u++) {
				foreach (var v in G.OutNeighbors[u]) {
					if (xs [u] > xs [v]) {
						numerator += (xs [u] - xs [v]) * (xs [u] - xs [v]);
					}
				}
			}
			return numerator / denominator;
		}

		static public double[] Gradient(Digraph G, double[] xs) {
			Debug.Assert (xs.Length == G.n);

			var res = new double[G.n];
			for (int u = 0; u < G.n; u++) {
				res [u] = xs [u] * (G.OutDegree (u) + G.InDegree (u));
				foreach (var v in G.OutNeighbors[u]) {
					if (xs [u] > xs [v]) {
						res [u] -= xs [v];
					} else {
						res [u] -= xs [u];
					}
				}
				foreach (var v in G.InNeighbors[u]) {
					if (xs [u] < xs [v]) {
						res [u] -= xs [v];
					} else {
						res [u] -= xs [u];
					}
				}
			}
			for (int u = 0; u < G.n; u++) {
				res [u] = -res [u];
			}
			return res;
		}

		static public double[] Iterate(Digraph G, double[] xs, double dt, List<double[]> Pi = null) {				
			var dx = Gradient (G, xs);
			for (int u = 0; u < G.n; u++) {
				xs [u] = xs [u] + dx [u] * dt;
			}

			if (Pi != null) {
				foreach (var p in Pi) {
					var dot = Vector.Dot (xs, p);
					xs = Vector.Subtract (xs, Vector.Multiply (dot, p));
				}
			}
			xs = Vector.L2Normalize (xs);
			return xs;
		}
			
	}
}

