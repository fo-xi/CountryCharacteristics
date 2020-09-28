using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryCharacteristics
{
	class Program
	{
		const int count = 10;
		static void Main(string[] args)
		{
			string[] сountrys = {"Дания", "Египет", "Венесуэла", "Германия",
			"Турция", "Бразилия", "Россия", "ЮАР", "США", "Австалия"};

			string[] continents = { "Евразия", "Африка", "Южная Америка",
			"Евразия", "Евразия", "Южная Америка", "Евразия", "Африка", "Северная Америка", "Австалия" };

			double[] grossNationalProducts = { 0.305, 0.217, 0.285, 3.306, 0.729,
			2.024, 1.477, 0.354, 14.624, 1.220 };

			double[] debts = { 0.627, 0.028, 0.043, 5.624, 0.310,
			4.07, 0.396, 0.074, 16.014, 1.376 };

			double[] populationDensity = { 128, 80, 29.8, 230, 100,
			23.6, 8.4, 40, 32, 2.8 };

			DisplayTable(сountrys, continents,
				grossNationalProducts, debts, populationDensity);

			double[,] сountrysDist = CalculatingDistance(continents);
			double[,] grossNationalProductsDist = CalculatingDistance(grossNationalProducts);
			double[,] debtsDist = CalculatingDistance(debts);
			double[,] populationDensityDist = CalculatingDistance(populationDensity);

			double[,] result = CalculatingDistance(сountrysDist, grossNationalProductsDist,
				debtsDist, populationDensityDist);


			Sorting(grossNationalProducts, сountrys, result);
			
			Console.ReadKey();
		}

		static void DisplayTable(string[] сountrys, string[] continents,
			double[] grossNationalProducts, double[] debts, double[] populationDensity)
		{
			for (int i = 0; i < count; i++)
			{
				Console.WriteLine($"{сountrys[i]}\t{continents[i]}\t" +
					$"{grossNationalProducts[i]}\t{debts[i]}\t{populationDensity[i]}");
			}
		}

		static void DisplayMatrix<T>(T[,] matrix)
		{
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					Console.Write($"{matrix[i, j]}\t");
				}
				Console.Write("\n");
			}
			Console.Write("\n");
		}

		//Подсчет расстояния по номинальной шкале 
		static double[,] CalculatingDistance(string[] array)
		{
			string[,] auxiliaryMatrix = new string[count, count];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					if (array[i] == array[j])
					{
						auxiliaryMatrix[i, j] = "=";
					}
					else
					{
						auxiliaryMatrix[i, j] = "!=";
					}
				}
			}
			DisplayMatrix(auxiliaryMatrix);

			double[,] results = new double[count, count];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					double result = 0.0;
					for (int k = 0; k < count; k++)
					{
						if (auxiliaryMatrix[i, k] != auxiliaryMatrix[j, k])
						{
							result += 1.0;
						}
					}
					results[i, j] = Math.Round((result / count), 3);
				}
			}
			DisplayMatrix<double>(results);
			return results;
		}

		//Подсчет расстояния по шкале отношений
		static double[,] CalculatingDistance(double[] array)
		{
			double[,] results = new double[count, count];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					results[i, j] = Math.Round((Math.Abs(array[i] - array[j])) / array.Max(), 2);
				}
			}
			DisplayMatrix(results);
			return results;
		}

		//Результирующая матрица
		static double[,] CalculatingDistance(double[,] continents, double[,] grossNationalProducts,
		double[,] debts, double[,] populationDensity)
		{
			double[,] results = new double[count, count];
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					results[i, j] = Math.Round(Math.Sqrt(Math.Pow(continents[i, j], 2) +
						Math.Pow(grossNationalProducts[i, j], 2) + Math.Pow(debts[i, j], 2) +
						Math.Pow(populationDensity[i, j], 2)), 2);
				}
			}
			DisplayMatrix(results);
			return results;
		}

		static void Sorting(double[] grossNationalProducts, string[] сountrys, double[,] result)
		{
			int index = 0;
			for (int i = 0; i < count; i++)
			{
				if (grossNationalProducts[i] < grossNationalProducts[index])
				{
					index = i;
				}
			}

			double[] auxiliaryArray = new double[count];
			for (int i = 0; i < count; i++)
			{
				auxiliaryArray[i] = result[index, i];
			}

			for (int i = 0; i < auxiliaryArray.Length - 1; i++)
			{
				for (int j = i + 1; j < auxiliaryArray.Length; j++)
				{
					if (auxiliaryArray[i] > auxiliaryArray[j])
					{
						string tempString = сountrys[i];
						double temp = auxiliaryArray[i];
						auxiliaryArray[i] = auxiliaryArray[j];
						сountrys[i] = сountrys[j];
						auxiliaryArray[j] = temp;
						сountrys[j] = tempString;
					}
				}
			}

			Console.WriteLine("Вывод отсортированного массива");
			for (int i = 0; i < сountrys.Length; i++)
			{
				Console.WriteLine(сountrys[i]);
			}
		}
	}
}
