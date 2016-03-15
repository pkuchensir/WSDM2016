using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace DirectedLaplacian
{
	public class Digraph
	{
		public List<List<int>> OutNeighbors = new List<List<int>>();
		public List<List<int>> InNeighbors = new List<List<int>>();	

		public Digraph ()
		{
			
		}

		public Digraph(string fn) {
			OpenEdgeList (fn);	
		}

		public int n {
			get {
				return OutNeighbors.Count;
			}
		}

		public int m {
			get {
				int res = 0;
				for (int u = 0; u < this.n; u++) {
					res += OutNeighbors [u].Count;
				}
				return res;
			}
		}

		public int OutDegree(int u) {
			return OutNeighbors[u].Count;
		}

		public int InDegree(int u) {
			return InNeighbors[u].Count;
		}

		public int Degree(int u) {
			return OutDegree (u) + InDegree (u);
		}

		public void OpenEdgeList(string fn) {
			var fs = new FileStream (fn, FileMode.Open);
			var sr = new StreamReader (fs);

			for (string line; (line = sr.ReadLine ()) != null;) {
				line = line.Trim ();
				if (line.Length > 0 && (line [0] == '#' || line [0] == '%'))
					continue;
				var words = line.Split ();

				if (words.Length >= 2) {
					var u = int.Parse (words [0]);
					var v = int.Parse (words [1]);
					if (u == v)
						continue;

					while (u >= this.n || v >= this.n) {
						OutNeighbors.Add (new List<int> ());
						InNeighbors.Add (new List<int> ());
					}
					OutNeighbors [u].Add (v);
					InNeighbors [v].Add (u);
				}
			}
		}
	}
}

