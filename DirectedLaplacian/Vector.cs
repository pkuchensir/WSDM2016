using System;
using System.Diagnostics;
using System.Linq;


namespace DirectedLaplacian
{

	static public class Vector
	{		
		static public double[] Random(int n) {
			double[] res = new double[n];			
			for (int i = 0; i < n; i++) {
				res [i] = (Util.Xor128 () % 1000000) / 1000000.0;
				if (Util.Xor128 () % 2 == 0) {
					res [i] = -res [i];
				}
			}
			return res;
		}

		static public double Dot(double[] a, double[] b) {
			Debug.Assert (a.Length == b.Length);
			return Enumerable.Range (0, a.Length).Sum (i => a [i] * b [i]);
		}

		static public double[] Add(double[] a, double[] b) {
			Debug.Assert (a.Length == b.Length);
			return Enumerable.Range (0, a.Length).Select (i => a [i] + b [i]).ToArray ();
		}

		static public double[] Subtract(double[] a, double[] b) {
			Debug.Assert (a.Length == b.Length);
			return Enumerable.Range (0, a.Length).Select (i => a [i] - b [i]).ToArray ();
		}

		static public double[] Multiply(double k, double[] a) {
			return Enumerable.Range (0, a.Length).Select (i => a [i] * k).ToArray ();
		}
			
		static public double L2Norm(double[] xs) {
			double norm = Math.Sqrt (xs.Sum (v => v * v));
			return norm;
		}

		static public double[] L2Normalize(double[] xs) {
			var norm = L2Norm (xs);
			return Enumerable.Range (0, xs.Length).Select (i => xs [i] / norm).ToArray ();
		}

	}
}

