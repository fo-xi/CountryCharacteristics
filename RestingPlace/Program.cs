﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestingPlace
{
	class Program
	{
		const int rows = 5;
		const int colums = 10;
		const int numberMostExperts = 3;
		const int numberMatrices = 5;

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
			double[,] matrix2 =
			{
				{9, 3, 9, 3, 10, 4, 2, 3, 7, 5},
				{5, 1, 10, 10, 5, 5, 4, 10, 4, 2},
				{5, 3, 7, 8, 3, 2, 3, 4, 5, 6},
				{7, 6, 10, 7, 9, 5, 5, 9, 1, 2},
				{10, 9, 9, 3, 6, 1, 5, 4, 2, 2}
			};
			double[] expertСompetence =
			{
				0.26189, 0.97753, 0.021887, 0.13493, 0.48354
			};
			Console.WriteLine("Ранживароние");
			Ranging(matrix1, comparativeArray);
			Console.WriteLine();
			Console.WriteLine("Парное сравнение");
			PairComparison(matrix1, comparativeArray);
			DisplayMatrix(matrix2);
			DirectAssessment(matrix2, expertСompetence);
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

		static void DisplayMatrix<T>(T[,] matrix, int rows, int colums)
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
			for (int j = 0; j < colums; j++)
			{
				arrayString[j] = matrix[rowNumber, j];
			}
			return arrayString;
		}

		static void Ranging(string[,] matrix, string[] comparativeArray)
		{
			//Перевод из матрицы типа string в матрицу типа double
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

			//Поиск совпадений элементов в столбце
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

		static void PairComparison(string[,] matrix, string[] comparativeArray)
		{

			//Перевод из матрицы типа string в матрицу типа double
			double[,] doubleMatrix = new double[rows, colums];
			for (int i = 0; i < rows; i++)
			{
				for (int index = 0; index < comparativeArray.Length; index++)
				{
					for (int j = 0; j < colums; j++)
					{
						if (matrix[i, j] == comparativeArray[index])
						{
							doubleMatrix[i, index] = j + 1;
						}
					}
				}
			}

			//Список матриц
			List<int[,]> matrixList = new List<int[,]>();

			//Создание матрицы парных сравнений с булевыми значениями
			for (int i = 0; i < rows; i++)
			{
				int[,] resultMatrix = new int[colums, colums];
				double[] arrayDouble = GetRow(doubleMatrix, i);
				for (int j = 0; j < colums; j++)
				{
					for (int k = 0; k < colums; k++)
					{
						if (arrayDouble[k] >= arrayDouble[j])
						{
							resultMatrix[j, k] = 1;
						}
						else
						{
							resultMatrix[j, k] = 0;
						}
					}
				}
				DisplayMatrix(resultMatrix, colums, colums);
				matrixList.Add(resultMatrix);
			}

			//Подсчет результирующей матрицы
			int[,] resulBoolMatrix = new int[colums, colums];
			for (int i = 0; i < colums; i++)
			{
				int numberUnits = 0;
				int numberZeros = 0;
				for (int j = 0; j < colums; j++)
				{
					foreach (var auxiliaryMatrix in matrixList)
					{
						if (auxiliaryMatrix[i, j] == 1)
						{
							numberUnits++;
						}
						else
						{
							numberZeros++;
						}
					}

					if (numberUnits > numberZeros)
					{
						resulBoolMatrix[i, j] = 1;
					}
					else
					{
						resulBoolMatrix[i, j] = 0;
					}
				}
			}
			DisplayMatrix(resulBoolMatrix, colums, colums);
		}

		static void DirectAssessment(double[,] matrix, double[] expertСompetence)
		{
			//Сумма элемнтов столбца
			double[] sum = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					sum[i] = sum[i] + matrix[j, i];
				}
			}

			//Вычисление обобщенной оценки
			double[] resultArray = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				resultArray[i] = sum[i] / rows;
			}
			Console.WriteLine("Вычисление обобщенной оценки");
			DisplayArray(resultArray);

			//Вычисление обобщенной оценки с учетом компетентности экспертов
			double[] ElementExpertСompetence = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					ElementExpertСompetence[i] += Math.Round(matrix[j, i] * expertСompetence[j], 1);
				}
			}
			Console.WriteLine("Вычисление обобщенной оценки с учетом компетентности экспертов");
			DisplayArray(ElementExpertСompetence);
		}
	}
}
