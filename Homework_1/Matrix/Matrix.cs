using System;

namespace Homework_1
{
    public class Matrix
    {
        public double[,] Elements { get; }
        public int? Permutations { get; set; }

        private double this[int i, int j]
        {
            get => Elements[i, j];
            set => Elements[i, j] = value;
        }
        
        /// <summary>
        /// Creates a Matrix from an existing <see cref="array"/> of elements.
        /// </summary>
        /// <param name="array"></param>
        public Matrix(double[,] array)
        {
            Elements = array;

            for (var i = 0; i < Elements.GetLength(0); i++)
            for (var j = 0; j < Elements.GetLength(1); j++)
                Elements[i, j] = array[i, j];
        }

        /// <summary>
        /// Creates a square Matrix with all elements initialised to 0.
        /// </summary>
        /// <param name="n"></param>
        public Matrix(int n) => Elements = new double[n, n];

        /// <summary>
        /// Creates a Matrix with varying rows and columns where all elements are initialised to <see cref="fill"/>.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="fill"></param>
        public Matrix(double[,] array, double fill = 0)
        {
            Elements = array;

            for (var i = 0; i < Elements.GetLength(0); i++)
            for (var j = 0; j < Elements.GetLength(1); j++)
                Elements[i, j] = fill;
        }

        /// <summary>
        /// Decomposes the A matrix into PA = LU.
        /// </summary>
        /// <param name="A"></param>
        /// <returns>Permutation Matrix containing the number of permutations as well.</returns>
        public static Matrix LUPDecomposition(Matrix A)
        {
            var rows = A.Elements.GetLength(0);
            var cols = A.Elements.GetLength(1);
            if (rows != cols) return null;

            var P = IdentityMatrix(rows);
            var permutations = 0;
            for (var i = 0; i < rows - 1; i++)
            {
                var pivot = i;
                for (var j = i + 1; j < cols; j++)
                    if (Math.Abs(A[j, i]) > Math.Abs(A[pivot, i]))
                        pivot = j;

                A = Swap(A, i, pivot);
                P = Swap(P, i, pivot);
                permutations++;

                // if (Math.Abs(A[pivot, i]) < (double)0.00000000001)
                // {
                //     Console.WriteLine("Pivot is nearing 0!");
                //     return null;
                // }
                for (var j = i + 1; j < rows; j++)
                {
                    A[j, i] /= A[i, i];
                    for (var k = i + 1; k < cols; k++)
                        A[j, k] -= A[j, i] * A[i, k];
                }
            }

            P.Permutations = permutations;
            return P;
        }

        /// <summary>
        /// Calculates the determinant of the given matrix A.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static double Determinant(Matrix A)
        {
            var n = A.Elements.GetLength(0);
            var P = LUPDecomposition(A);

            var result = 1.0;
            
            if (P.Permutations != null) 
                result *= Math.Pow(-1, P.Permutations.Value);

            var U = UpperTriangularMatrix(A);
            
            for (var i = 0; i < n; i++) 
                result *= U[i, i];

            return result;
        }

        /// <summary>
        /// Creates an I (identity) matrix with the dimensions of n x n.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static Matrix IdentityMatrix(int n)
        {
            var identity = new Matrix(new double[n, n]);
            for (var i = 0; i < n; i++)
                identity[i, i] = 1;

            return identity;
        }


        /// <summary>
        /// Swaps the i-th and j-th row of the matrix a.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static Matrix Swap(Matrix a, int i, int j)
        {
            for (var k = 0; k < a.Elements.GetLength(0); k++)
            {
                var temp = a[i, k];
                a[i, k] = a[j, k];
                a[j, k] = temp;
            }
            return a;
        }

        /// <summary>
        /// Decomposes the given matrix A into A = LU;
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix LUDecomposition(Matrix A)
        {
            var rows = A.Elements.GetLength(0);

            for (var i = 0; i < rows - 1; i++)
            {
                if (Math.Abs(A[i, i]) < 0.00000000001)
                {
                    Console.WriteLine("Pivot is nearing 0!");
                    A[i, i] = Math.Sign(A[i, i]) * 0.0001;
                }
                for (var j = i + 1; j < rows; j++)
                {
                    A[j, i] /= A[i, i];
                    for (var k = i + 1; k < rows; k++)
                        A[j, k] -= A[j, i] * A[i, k];
                }
            }
            return A;
        }

        /// <summary>
        /// Calculates the inverse of a given matrix A.
        /// A * A^(-1) = I
        /// The inverse can only be calculated when the determinant is not 0.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix Inverse(Matrix A)
        {
            var n = A.Elements.GetLength(0);

            var detA = Determinant(A);

            if (detA < 0.00001) return null;
            
            var vector = new Matrix(new double[n, 1]);

            var permutationMatrix = LUPDecomposition(A);
            
            var result = new Matrix(n);
            for (var i = 0; i < n; i++) {
                for (var j = 0; j < n; j++) 
                    vector[j, 0] = permutationMatrix[j, i];
                
                var fwd =  ForwardSubstitution(A, vector);
                var bck = BackwardSubstitution(A, fwd);
                for (var j = 0; j < n; j++) 
                    result[j, i] = bck[j, 0];
            }
            return result;
        }
        
        /// <summary>
        /// Completes the forward substitution of the given matrix A and vector.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Matrix ForwardSubstitution(Matrix A, Matrix vector)
        {
            // "vector" and "B" and both vectors, with only a singular row.
            // var B = vector;
            var aRows = A.Elements.GetLength(0);
            var aCols = A.Elements.GetLength(1);

            var vectorRows = vector.Elements.GetLength(0);

            if (aRows != vectorRows) return null;

            for (var i = 0; i < aRows - 1; i++)
            for (var j = i + 1; j < aCols; j++)
                vector[j, 0] -= A[j, i] * vector[i, 0];

            return vector;
        }
        
        /// <summary>
        /// Completes the backward substitution of the given matrix A and vector.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Matrix BackwardSubstitution(Matrix A, Matrix vector)
        {
            // "vector" and "B" and both vectors, with only a singular row.
            var rows = A.Elements.GetLength(0);
            var cols = A.Elements.GetLength(1);
            
            var vectorRows = vector.Elements.GetLength(0);

            if (rows != vectorRows) return null;
            
            for (var i = rows - 1; i >= 0; i--)
            {
                vector[i, 0] /= A[i, i];
                for (var j = 0; j < i; j++)
                    vector[j, 0] -= A[j, i] * vector[i, 0];
            }
            return vector;
        }

        /// <summary>
        /// Adds the elements of the 2 given matrices together.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            var aRows = a.Elements.GetLength(0);
            var bRows = b.Elements.GetLength(0);

            var aCols = a.Elements.GetLength(1);
            var bCols = b.Elements.GetLength(1);

            if (aRows != bRows || aCols != bCols) return null;
            
            var result = new Matrix(new double[aRows, aCols]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
                result[i, j] = a[i, j] + b[i, j];

            return result;
        }
        
        /// <summary>
        /// Subtracts the elements of the matrix a with the elements of matrix b and returns a new matrix containing
        /// the result of the operation.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            var aRows = a.Elements.GetLength(0);
            var bRows = b.Elements.GetLength(0);

            var aCols = a.Elements.GetLength(1);
            var bCols = b.Elements.GetLength(1);

            if (aRows != bRows || aCols != bCols) return null;
            
            var result = new Matrix(new double[aRows, aCols]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
                result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        /// <summary>
        /// Multiplies the matrix with the scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix operator *(double scalar, Matrix a) => a * scalar;
        
        /// <summary>
        /// Multiplies the matrix with the scalar value.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, double scalar)
        {
            var aRows = a.Elements.GetLength(0);
            var aCols = a.Elements.GetLength(1);
            
            var result = new Matrix(new double[aRows, aCols]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
                result[i, j] = a[i, j] * scalar;

            return result;
        }
        
        /// <summary>
        /// Multiplies the 2 given matrices together.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            var aRows = a.Elements.GetLength(0);
            var bRows = b.Elements.GetLength(0);

            var aCols = a.Elements.GetLength(1);
            var bCols = b.Elements.GetLength(1);

            if (aCols != bRows) return null;

            var result = new Matrix(new double[aRows, bCols]);

            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < bCols; j++)
            {
                double tmp = 0;
                for (var k = 0; k < aCols; k++)
                {
                    tmp += a[i, k] * b[k, j];
                }
                result[i, j] = tmp;
            }

            return result;
        }
        
        /// <summary>
        /// Transposes the given matrix A.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix Transpose(Matrix A)
        {
            var aRows = A.Elements.GetLength(0);
            var aCols = A.Elements.GetLength(1);
            var result = new Matrix(new double[aCols, aRows]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
                result[j, i] = A[i, j];
            
            return result;
        }

        /// <summary>
        /// Returns the lower triangular matrix of the given matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix LowerTriangularMatrix(Matrix a)
        {
            var aRows = a.Elements.GetLength(0);
            var aCols = a.Elements.GetLength(1);
            var result = new Matrix(new double[aRows, aCols]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
            {
                if (i < j) result[i, j] = 0.0;
                else if (i == j) result[i, j] = 1.0;
                else result[i, j] = a[i, j];
            }

            return result;
        }
        
        /// <summary>
        /// Returns the upper triangular matrix of the given matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix UpperTriangularMatrix(Matrix a)
        {
            var aRows = a.Elements.GetLength(0);
            var aCols = a.Elements.GetLength(1);
            var result = new Matrix(new double[aRows, aCols]);
            
            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
            {
                if (i > j) result[i, j] = 0.0;
                else result[i, j] = a[i, j];
            }

            return result;
        }
        
        /// <summary>
        /// Returns true if the 2 given matrices are equal in size and elements.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix a, Matrix b)
        {
            var aRows = a.Elements.GetLength(0);
            var bRows = b.Elements.GetLength(0);

            var aCols = a.Elements.GetLength(1);
            var bCols = b.Elements.GetLength(1);

            if (aRows != bRows || aCols != bCols) return false;

            for (var i = 0; i < aRows; i++)
            for (var j = 0; j < aCols; j++)
                if (Math.Abs(a[i, j] - b[i, j]) > 0.00000000001)
                    return false;
            return true;
        }

        /// <summary>
        /// Returns true if the 2 given matrices are not equal either in size or elements.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix a, Matrix b) => !(a == b);

        /// <summary>
        /// Overrides the default ToString in order to better visualise the matrix.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = "___________________________________\n";
            for (var i = 0; i < Elements.GetLength(0); i++)
            {
                result += "|";
                for (var j = 0; j < Elements.GetLength(1); j++)
                    result += $"      {Elements[i, j]:0.00000}";

                result += "      |\n";
            }
            result += "___________________________________";
            return result;
        }
        
        
    }
}