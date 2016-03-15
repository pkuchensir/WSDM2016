using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace DirectedLaplacian
{
	static public class NormalizedLaplacian
	{
		static public double[] FirstEigenvector(Digraph G) {
			var res = Enumerable.Range (0, G.n).Select (u => Math.Sqrt (G.Degree (u))).ToArray ();
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

			var ys = Enumerable.Range (0, G.n).Select (
				         u => xs [u] / Math.Sqrt (G.Degree (u))
			         ).ToArray ();

			for (int u = 0; u < G.n; u++) {
				foreach (var v in G.OutNeighbors[u]) {
					if (ys [u] > ys [v]) {
						numerator += (ys [u] - ys [v]) * (ys [u] - ys [v]);
					}
				}
			}
			return numerator / denominator;
		}

		static public double[] Gradient(Digraph G, double[] xs) {
			Debug.Assert (xs.Length == G.n);

			var ys = Enumerable.Range (0, G.n).Select (
				         u => xs [u] / Math.Sqrt (G.Degree (u))
			         ).ToArray ();

			var res = new double[G.n];
			for (int u = 0; u < G.n; u++) {
				res [u] = xs [u];
				foreach (var v in G.OutNeighbors[u]) {
					if (ys [u] > ys [v]) {
						res [u] -= xs [v] / Math.Sqrt (G.Degree (u) * G.Degree (v));
					} else {
						res [u] -= xs [u] / G.Degree (u);
					}
				}
				foreach (var v in G.InNeighbors[u]) {
					if (ys [u] < ys [v]) {
						res [u] -= xs [v] / Math.Sqrt (G.Degree (u) * G.Degree (v));
					} else {
						res [u] -= xs [u] / G.Degree (u);
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

