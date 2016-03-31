# Directed Laplacian
A library for computing (an approximation to) the second eigenvector of the directed Laplacian of a directed network.

## Usage
    mono DirectedLaplacian.exe -i <input> -dt <dt> -T <T> -normalized <normalized> -seed <seed>

See the source code to figure out how to use the library from your program.
In the graph file (&lt;input&gt;), each line should contain two vertices (see Data/sample.edges).
Vertices should be numbered from zero.

The program computes (an approximation to) the second eigenvector by applying Euler's method to the heat equation (see the reference for details).
The parameter &lt;dt&gt; and &lt;T&gt; determine the time resolution and the number of iterations in the simulation, respectively.
The default values are 0.1 and 1000, respectively.

We use the normalized directed Laplacian if &lt;normalized&gt; is "true" and the (unnormalized) directed Laplacian if &lt;normalized&gt; is false.
The default value is false.

The parameter &lt;seed&gt;, which should be specified by an integer, is a seed parameter for a random number generator.
The default value is zero.

## Reference
Yuichi Yoshida. 2016. Nonlinear Laplacian for Digraphs and its Applications to Network Analysis. In Proceedings of the Ninth ACM International Conference on Web Search and Data Mining (WSDM '16). ACM, New York, NY, USA, 483-492. DOI=http://dx.doi.org/10.1145/2835776.2835785
