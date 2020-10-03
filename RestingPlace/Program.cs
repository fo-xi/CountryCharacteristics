using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestingPlace
{
	class Program
	{
		const int rows = 5;
		const int colums = 10;

		static void Main(string[] args)
		{
			double[,] matrix =
			{
				{9, 3, 9, 3, 10, 4, 2, 3, 7, 5},
				{5, 1, 10, 10, 5, 5, 4, 10, 4, 2},
				{6, 3, 7, 8, 3, 2, 3, 4, 5, 6},
				{7, 6, 10, 7, 9, 5, 5, 9, 1, 2},
				{10, 9, 9, 3, 6, 1, 5, 4, 2, 2}
			};
			DisplayMatrix(matrix);
			Console.ReadKey();
		}

		static void DisplayMatrix<T>(T[,] matrix)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < colums; j++)
				{
					Console.Write($"{matrix[i, j]}\t");
				}

				Console.WriteLine();
			}

			Console.WriteLine();
		}

		static void DisplayArray<T>(T[] array)
		{
			for (int i = 0; i < colums; i++)
			{
				Console.Write($"{array[i]}\t");
			}

			Console.WriteLine();
		}

		static void Ranging(double[,] matrix)
		{
			double[] sum = new double[colums];
			double[] generalizedRank = new double[colums];

			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					sum[i] = sum[i] + matrix[j, i];
					generalizedRank[i] = sum[i] / rows;
				}
			}
			DisplayArray(sum);
			DisplayArray(generalizedRank);
		}
	}
}
