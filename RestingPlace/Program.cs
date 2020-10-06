using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
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
		const int numberMostExperts = 5;
		const int indexColumsFlightPrice = 1;
		const int indexColumsAccommodationPrice = 2;
		const int indexColumsServiceLevel = 3;
		const int indexColumsFoodQuality = 4;
		const int sizeChoosingVacationSpot = 4;



		static void Main(string[] args)
		{
			//1 задание

			double[,] matrix1 =
			{
				{9, 4, 8, 2, 10, 5, 1, 3, 7, 6},
				{5, 1, 9, 10, 7, 6, 3, 8, 4, 2},
				{8, 3, 9, 10, 2, 1, 4, 5, 6, 7},
				{7, 5, 10, 6, 8, 4, 3, 9, 1, 2},
				{10, 9, 8, 4, 7, 1, 6, 5, 2, 3}
			};

			//2 задание
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

			//3 задание
			double[,] matrix3 =
			{
				{21, 24, 2, 7},
				{28, 30, 3, 4},
				{25, 12, 3, 10},
				{34, 32, 4, 8},
			};
			double[] weight =
			{
				0.3, 0.3, 0.2, 0.2
			};

			Console.WriteLine("Ранживароние: ");
			DisplayMatrix(matrix1);
			Ranging(matrix1);
			Console.WriteLine();
			Console.WriteLine("Парное сравнение: ");
			PairComparison(matrix1);
			Console.WriteLine("===============================");
			Console.WriteLine("Матрица ко второму заданию: ");
			DisplayMatrix(matrix2);
			Console.WriteLine("Компетентность экспертов: ");
			DisplayArray(expertСompetence, expertСompetence.Length);
			DirectAssessment(matrix2, expertСompetence);
			Console.WriteLine("===============================");
			Console.WriteLine("Матрица к третьему заданию: ");
			DisplayMatrix(matrix3, sizeChoosingVacationSpot, sizeChoosingVacationSpot);
			Console.WriteLine("Веса критериев: ");
			DisplayArray(weight, weight.Length);
			Console.WriteLine();
			AssessingAlternatives(matrix3, weight);
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

		static void DisplayArray<T>(T[] array, int size)
		{
			for (int i = 0; i < size; i++)
			{
				Console.Write($"A{i + 1} = {array[i]}\t");
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

		static void Ranging(double[,] matrix)
		{
			//Сумма элемнтов столбца
			double[] sumArray = new double[colums];
			double[] sumAuxiliaryArray = new double[colums];
			for (int i = 0; i < colums; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					sumArray[i] = sumArray[i] + matrix[j, i];
					sumAuxiliaryArray[i] = sumAuxiliaryArray[i] + matrix[j, i];
				}
			}

			Console.WriteLine("Сумма рангов");
			DisplayArray(sumArray);

			//Запись рангов в массив
			double minValue;
			int indexMinValueArray;
			int indexMinValueList;
			List<double> tempList = new List<double>(sumArray);
			double[] supportSumArray = sumArray.Clone() as double[];
			double[] generalizedRank = new double[colums];
			for (int j = 0; j < colums; j++)
			{
				minValue = tempList.Min();
				indexMinValueArray = Array.IndexOf(supportSumArray, minValue);
				indexMinValueList = tempList.IndexOf(minValue);
				generalizedRank[indexMinValueArray] = j + 1;
				tempList.RemoveAt(indexMinValueList);
				supportSumArray[indexMinValueArray] = double.MaxValue;
			}

			//Посчет обощенных рангов
			Array.Sort(sumAuxiliaryArray);
			for (int j = 0; j < colums; j++)
			{
				List<int> tempIndex = new List<int>();
				for (int k = 0; k < colums; k++)
				{
					if (sumAuxiliaryArray[k] == sumAuxiliaryArray[j])
					{
						tempIndex.Add(k);
					}
				}

				int sumIndex = 0;
				foreach (var element in tempIndex)
				{ 
					sumIndex += element + 1;
				}
				double average = (double) sumIndex / tempIndex.Count;
				generalizedRank[j] = average;
			}

			//Присвоение общенных рангов
			double[] resultArray = new double[colums];
			for (int j = 0; j < colums; j++)
			{
				for (int k = 0; k < colums; k++)
				{
					if (sumArray[k] == sumAuxiliaryArray[j])
					{
						resultArray[k] = generalizedRank[j];
					}
				}
			}

			Console.WriteLine("Обобщенный ранг");
			DisplayArray(resultArray);
		}

		static void PairComparison(double[,] matrix)
		{
			//Список матриц
			List<int[,]> matrixList = new List<int[,]>();

			//Создание матрицы парных сравнений с булевыми значениями
			for (int i = 0; i < rows; i++)
			{
				int[,] resultMatrix = new int[colums, colums];
				double[] arrayDouble = GetRow(matrix, i);
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

		static void AssessingAlternatives(double[,] matrix, double[] weight)
		{
			double[] flightPrice = new double[sizeChoosingVacationSpot];
			for (int j = 0; j < indexColumsFlightPrice; j++)
			{
				int min = 15;
				int max = 50;
				for (int i = 0; i < sizeChoosingVacationSpot; i++)
				{
					flightPrice[i] = Math.Round((max - matrix[i, j]) / (max - min), 2);
				}
			}
			Console.WriteLine("Нормированные значения критерия 'Цена перелета'");
			DisplayArray(flightPrice, sizeChoosingVacationSpot);

			double[] accommodationPrice = new double[sizeChoosingVacationSpot];
			for (int j = 0; j < indexColumsAccommodationPrice; j++)
			{
				int min = 10;
				int max = 40;
				for (int i = 0; i < sizeChoosingVacationSpot; i++)
				{
					accommodationPrice[i] = Math.Round((max - matrix[i, j]) / (max - min), 2);
				}
			}
			Console.WriteLine("Нормированные значения критерия 'Цена проживания'");
			DisplayArray(accommodationPrice, sizeChoosingVacationSpot);

			double[] serviceLevel = new double[sizeChoosingVacationSpot];
			for (int j = 0; j < indexColumsServiceLevel; j++)
			{
				int max = 5;
				for (int i = 0; i < sizeChoosingVacationSpot; i++)
				{
					serviceLevel[i] = matrix[i, j] / max;
				}
			}
			Console.WriteLine("Нормированные значения критерия 'Уровень сервиса'");
			DisplayArray(serviceLevel, sizeChoosingVacationSpot);

			double[] foodQuality = new double[sizeChoosingVacationSpot];
			for (int j = 0; j < indexColumsFoodQuality; j++)
			{
				int max = 10;
				for (int i = 0; i < sizeChoosingVacationSpot; i++)
				{
					foodQuality[i] = matrix[i, j] / max;
				}
			}
			Console.WriteLine("Нормированные значения критерия 'Качество питания'");
			DisplayArray(foodQuality, sizeChoosingVacationSpot);

			Console.WriteLine();

			Console.WriteLine("===============================");
			AdditiveConvolution(flightPrice, accommodationPrice, 
				serviceLevel, foodQuality, weight);
			Console.WriteLine("===============================");

			MultiplicativeConvolution(flightPrice, accommodationPrice,
				serviceLevel, foodQuality, weight);
			Console.WriteLine("===============================");

			IdealPoint(flightPrice, accommodationPrice,
				serviceLevel, foodQuality, weight);
			Console.WriteLine("===============================");
		}

		static void AdditiveConvolution(double[] array1, double[] array2, 
			double[] array3, double[] array4, double[] weight)
		{
			double[] resultArray = new double[sizeChoosingVacationSpot];
			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArray[i] = Math.Round((array1[i] + array2[i] + array3[i] + array4[i]) /
					sizeChoosingVacationSpot, 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу аддитивной свертки");
			DisplayArray(resultArray, sizeChoosingVacationSpot);

			double[] resultArrayWeight = new double[sizeChoosingVacationSpot];
			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArrayWeight[i] = Math.Round((array1[i] * weight[i]) + 
					(array2[i] * weight[i]) + 
					(array3[i] * weight[i]) + 
					(array4[i] * weight[i]), 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу аддитивной свертки(вес)");
			DisplayArray(resultArrayWeight, sizeChoosingVacationSpot);

			//Вывод наилучшего значения
			Console.WriteLine();
			double maxValue = resultArray.Max();
			double indexMaxValue = Array.IndexOf(resultArray, maxValue);
			Console.WriteLine($"Наилучшее значение по методу " +
				$"аддитивной свертки оказалось место отдыха A{indexMaxValue + 1} = {maxValue}");

			maxValue = resultArrayWeight.Max();
			indexMaxValue = Array.IndexOf(resultArrayWeight, maxValue);
            Console.WriteLine($"Наилучшее значение по методу " +
                $"аддитивной свертки (с учетом веса) оказалось " +
                $"место отдыха A{indexMaxValue + 1} = {maxValue}");
			Console.WriteLine();
		}

		static void MultiplicativeConvolution(double[] array1, double[] array2,
			double[] array3, double[] array4, double[] weight)
		{
			double[] resultArray = new double[sizeChoosingVacationSpot];
			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArray[i] = Math.Round(Math.Pow((array1[i] * array2[i] * 
					array3[i] * array4[i]), 1.0 / sizeChoosingVacationSpot), 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу мультипликативной свертки");
			DisplayArray(resultArray, sizeChoosingVacationSpot);

			double[] resultArrayWeight = new double[sizeChoosingVacationSpot];
			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArrayWeight[i] = Math.Round(Math.Pow(array1[i], weight[i]) * 
					Math.Pow(array2[i], weight[i]) *
					Math.Pow(array3[i], weight[i]) * 
					Math.Pow(array4[i], weight[i]), 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу мультипликативной свертки(вес)");
			DisplayArray(resultArrayWeight, sizeChoosingVacationSpot);

			//Вывод наилучшего значения
			Console.WriteLine();
			double maxValue = resultArray.Max();
			double indexMaxValue = Array.IndexOf(resultArray, maxValue);
			Console.WriteLine($"Наилучшее значение по методу " +
				$"мультипликативной свертки оказалось место отдыха A{indexMaxValue + 1} = {maxValue}");

			maxValue = resultArrayWeight.Max();
			indexMaxValue = Array.IndexOf(resultArrayWeight, maxValue);
			Console.WriteLine($"Наилучшее значение по методу " +
				$"мультипликативной свертки (с учетом веса) оказалось " +
				$"место отдыха A{indexMaxValue + 1} = {maxValue}");
			Console.WriteLine();
		}

		static void IdealPoint(double[] array1, double[] array2,
			double[] array3, double[] array4, double[] weight)
        {
			double[] resultArray = new double[sizeChoosingVacationSpot];

			double max = 1.0;

			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArray[i] = Math.Round((Math.Pow(((Math.Pow((max - array1[i]), 2)) +
					(Math.Pow((max - array2[i]), 2)) +
					(Math.Pow((max - array3[i]), 2)) +
					(Math.Pow((max - array4[i]), 2))) 
					/ sizeChoosingVacationSpot, 1.0 / 2)), 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу идеальной точки");
			DisplayArray(resultArray, sizeChoosingVacationSpot);

			double[] resultArrayWeight = new double[sizeChoosingVacationSpot];
			for (int i = 0; i < sizeChoosingVacationSpot; i++)
			{
				resultArrayWeight[i] = Math.Round(Math.Pow(((weight[i] * (Math.Pow((max - array1[i]), 2))) +
					(weight[i] * (Math.Pow((max - array2[i]), 2))) +
					(weight[i] * (Math.Pow((max - array3[i]), 2))) +
					(weight[i] * (Math.Pow((max - array4[i]), 2)))), 1.0 / 2), 3);
			}
			Console.WriteLine("Значения интегрального критерия " +
				"по методу идеальной точки (вес)");
			DisplayArray(resultArrayWeight, sizeChoosingVacationSpot);

			//Вывод наилучшего значения
			Console.WriteLine();
			double maxValue = resultArray.Max();
			double indexMaxValue = Array.IndexOf(resultArray, maxValue);
			Console.WriteLine($"Наилучшее значение по методу " +
				$"идеальной точки оказалось место отдыха A{indexMaxValue + 1} = {maxValue}");

			maxValue = resultArrayWeight.Max();
			indexMaxValue = Array.IndexOf(resultArrayWeight, maxValue);
			Console.WriteLine($"Наилучшее значение по методу " +
				$"идеальной точки (с учетом веса) оказалось " +
				$"место отдыха A{indexMaxValue + 1} = {maxValue}");
			Console.WriteLine();
		}
	}
}
