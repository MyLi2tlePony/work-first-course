using Brain.Tools;

namespace Brain.Decision
{
	public static class AllIterate
	{
		//Делегат, который хранит способ вывода информации
		public delegate void DShow(string message);

		//Массив последовательности вершин
		private static int[] arraySequence;

		//Лучший путь
		private static int[] arrayBestWay;

		//Колличество Гамильтоновых циклов
		private static int numberOfWay = 0;

		//Лучшая длина пути
		private static int lengthBestWay = 0;

		//Текущая вершина
		private static int currentVertex = 0;

		//Шаг на cледующую вершину
		private static int nextVertex = 0;

		//Уровень, на котором мы выбираем вершину
		private static int indexNextVertex = 1;

		//Весь путь
		private static int lengthAllWay = 0;

		//Количество вершин в массиве смежности
		private static int numberOfVertex;

		//Начальная вершина
		private static int beginVertex = 0;

		public static int GetNumberOfWay()
		{
			return numberOfWay;
		}

		public static void GetBestCycle(ref int[,] arrayMatrix, ref int[] ArrayBestWay, ref int LengthBestWay)
		{
			numberOfVertex = arrayMatrix.Length / (arrayMatrix.GetUpperBound(0) + 1);
			
			//Обнуляем все статические переменные
			AllVarNull();

			Decision(ref arrayMatrix);

			ArrayBestWay = arrayBestWay;
			LengthBestWay = lengthBestWay;
		}

		//Обнуляем все статические переменные
		private static void AllVarNull()
		{
			arrayBestWay = new int[numberOfVertex];
			arraySequence = new int[numberOfVertex];
			numberOfWay = 0;
			lengthBestWay = 0;
			currentVertex = 0;
			nextVertex = 0;
			indexNextVertex = 1;
			lengthAllWay = 0;
			beginVertex = 0;
		}

		//Находим циклы полным перебором
		private static void Decision(ref int[,] arrayMatrix)
		{
			//Проверяем, использовали ли мы вершину до этого и есть ли путь между вершинами
			if (!CheckConditions.UseVertexBefore(ref arraySequence, nextVertex) && CheckConditions.WayBetweenVertex(ref arrayMatrix, currentVertex, nextVertex))
			{
				//Записываем вершинуы
				StepForward(ref arrayMatrix);
			}
			else
			{
				//Проверяем, есть ли еще свободные вершины и есть ли путь к начальной вершине
				if ((numberOfVertex == indexNextVertex) && (arrayMatrix[currentVertex, beginVertex] != 0))
				{
					//Записываем решение
					SetDecision(ref arrayMatrix);
				}
				else
				{
					//Возращаемся назад или ищим другую вершину
					FindNewVertex(ref arrayMatrix);
				}
			}
		}

		//Переходим на следующую вершину
		private static void StepForward(ref int[,] arrayMatrix)
		{
			//Прибавляем к нашему пути расстояние между вершинами
			lengthAllWay += arrayMatrix[currentVertex, nextVertex];

			WorkWithArrays.SetNumberInArray(arraySequence, nextVertex, indexNextVertex);

			currentVertex = nextVertex;

			nextVertex = 0;

			indexNextVertex++;

			//Продолжаем рекурсию
			Decision(ref arrayMatrix);
		}

		//Заполняем решение
		private static void SetDecision(ref int[,] arrayMatrix)
		{
			//Прибавляем расстояние до начальной вершины
			lengthAllWay += arrayMatrix[currentVertex, beginVertex];

			//Записываем результат, если он лучше предыдущего
			WorkWithArrays.ChooseBetterArray(ref arrayBestWay, ref arraySequence, ref lengthBestWay, lengthAllWay, numberOfWay);

			//Вычитаем расстояние до начальной вершины
			lengthAllWay -= arrayMatrix[currentVertex, beginVertex];

			//Увеличиваем количество путей
			numberOfWay++;

			//Возвращаемся назад, чтобы найти новые вершины
			StepBack(ref arrayMatrix);

			//Продолжаем рекурсию
			Decision(ref arrayMatrix);
		}

		//Возращаемся назад или ищим другую вершину
		private static void FindNewVertex(ref int[,] arrayMatrix)
		{
			//Если использовали, то идем к следующей
			if (nextVertex < numberOfVertex - 1)
			{
				nextVertex++;

				//Продолжаем рекурсию
				Decision(ref arrayMatrix);
			}
			//Если все вершины на этом уровне просмортели, то спускаемся на один уровень
			else
			{
				//Самая последняя проверка. Нужна для выхда из реккурсии
				if (arraySequence[indexNextVertex - 1] != 0)
				{
					if (indexNextVertex < numberOfVertex)
					{
						arraySequence[indexNextVertex] = -1;
					}

					//Возвращаемся назад, чтобы найти новые вершины
					StepBack(ref arrayMatrix);

					//Продолжаем рекурсию
					Decision(ref arrayMatrix);
				}
			}
		}

		//Возвращаемся назад, чтобы найти новые вершины
		private static void StepBack(ref int[,] arrayMatrix)
		{
			lengthAllWay -= arrayMatrix[arraySequence[indexNextVertex - 2], arraySequence[indexNextVertex - 1]];

			currentVertex = arraySequence[indexNextVertex - 2];

			nextVertex = arraySequence[indexNextVertex - 1] + 1;

			indexNextVertex--;
		}
	}
}