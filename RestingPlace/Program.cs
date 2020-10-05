using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RestingPlace
{
	class Program
	{
		const int rows = 5;
		const int colums = 10;
		private const int numberMostExperts = 3;

		static void Main(string[] args)
		{
			string[] comparativeArray =
			{
				"A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10"
			};
			string[,] matrix1 =
			{
				{"A7", "A4", "A8", "A2", "A6", "A10", "A9", "A3", "A1", "A5"},
				{"A2", "A10", "A7", "A9", "A1", "A6", "A5", "A8", "A3", "A4"},
				{"A6", "A5", "A2", "A7", "A8", "A9", "A10", "A1", "A3", "A4"},
				{"A9", "A10", "A7", "A6", "A2", "A4", "A1", "A5", "A8", "A3"},
				{"A6", "A9", "A10", "A4", "A8", "A7", "A5", "A3", "A2", "A1"}
			};
			//double[,] matrix =
			//{
			//	{9, 3, 9, 3, 10, 4, 2, 3, 7, 5},
			//	{5, 1, 10, 10, 5, 5, 4, 10, 4, 2},
			//	{5, 3, 7, 8, 3, 2, 3, 4, 5, 6},
			//	{7, 6, 10, 7, 9, 5, 5, 9, 1, 2},
			//	{10, 9, 9, 3, 6, 1, 5, 4, 2, 2}
			//};
			//DisplayMatrix(matrix);
			Ranging(matrix1, comparativeArray);
			PairComparison(matrix1);
			//Ranging(matrix);
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

		static T[] GetRow<T>(T[,] matrix, int rowNumber)
		{
			T[] arrayString = new T[colums];
			for (int j = 0; j < rows; j++)
			{
				arrayString[j] = matrix[rowNumber, j];
			}
			return arrayString;
		}

		static void Ranging(string[,] matrix, string[] comparativeArray)
		{
			double[,] resultMatrix = new double[rows, colums];
			for (int i = 0; i < rows; i++)
			{
				for (int index = 0; index < comparativeArray.Length; index++)
				{
					for (int j = 0; j < colums; j++)
					{
						if (matrix[i, j] == comparativeArray[index])
						{
							resultMatrix[i, index] = j + 1;
						}
					}
				}
			}

			DisplayMatrix(resultMatrix);

			//Сумма элемнтов столбца
			double[] sum = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					sum[i] = sum[i] + resultMatrix[j, i];
				}
			}
			Console.WriteLine("Сумма рангов");
			DisplayArray(sum);

			double[] generalizedRank = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					int count = 0;
					for (int k = 0; k < rows; k++)
					{
						if (resultMatrix[k, i] == resultMatrix[j, i])
						{
							count++;
							if (count >= numberMostExperts)
							{
								generalizedRank[i] = resultMatrix[k, i];

							}
						}
					}
				}
			}

			//Среднее значение суммы элементов столбцы
			for (int i = 0; i < colums; i++)
			{
				if (generalizedRank[i] == 0)
				{ 
					generalizedRank[i] = sum[i] / rows;
				}
			}

			Console.WriteLine("Обобщенный ранг");
			DisplayArray(generalizedRank);
		}

		static void PairComparison(string[,] matrix)
		{
			int[,] resultMatrix = new int[rows, colums];
			string[] rowString = GetRow(matrix, rows);

			double[] resultRowString = new double[colums];
			for (int i = 0; i < rows; i++)
			{
				for (int index = 0; index < rowString.Length; index++)
				{
					for (int j = 0; j < colums; j++)
					{
						if (matrix[i, j] == rowString[index])
						{
							resultMatrix[i, index] = j + 1;
						}
					}
				}
			}

			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (resultMatrix[i] >= resultMatrix[j])
					{
						resultMatrix[i, j] = 1;
					}
					else
					{
						resultMatrix[i, j] = 0;
					}
				}
			}
			DisplayMatrix(resultMatrix);
		}
	}
}
