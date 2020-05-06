using System;

namespace Brain.Tools
{
    public static class WorkWithArrays
    {
        //Делегат, который хранит способ вывода информации
        public delegate void DShow(string message);

        //Создает массив с указанными параметрами
        public static void CreateArray(ref int[,] array, double density, int maxLength)
        {
            //Объявляем экземпляр класса генерации рандомных чисел
            Random rand = new Random();

            //Получим плотность целым числом
            density *= 100;

            //Заполняем массив, опираясь на требования
            for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
            {
                for (int b = i; b < array.Length / (array.GetUpperBound(0) + 1); b++)
                {
                    //Проверяем, будет ли между этими вершинами путь
                    if(i != b && rand.Next(100) < density)
                    {
                        array[i, b] = rand.Next(maxLength - 1) + 1;
                        array[b, i] = array[i, b];
                    }
                    else
                    {
                        array[i, b] = 0;
                        array[b, i] = 0;
                    }
                }
            }
        }

        //Выводит массив
        public static void ShowArray(int[,] array, DShow outPut)
        {
            outPut("\n");
            for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
            {
                for (int b = 0; b < array.Length / (array.GetUpperBound(0) + 1); b++)
                {
                    outPut($"{array[i, b]}");
                }
                outPut("\n");
            }
        }
        public static void ShowArray(int[] array, DShow outPut)
        {
            outPut("\n");
            for (int i = 0; i < array.Length; i++)
            {
                outPut($"{array[i]}");
            }
        }

        //Записываем число в массив по индексу
        public static void SetNumberInArray(int[] array, int vertex, int index)
        {
            array[index] = vertex;
        }

        //Сравнивает 2 величины и, исходя из результата, перезаписывает массив
        public static void ChooseBetterArray(ref int[] arrayOld, ref int[] arrayNew, ref int oldNumber, int newNumber, int NumberOfSet)
        {
            if (oldNumber > newNumber || NumberOfSet == 0)
            {
                for (int i = 0; i < arrayOld.Length; i++)
                {
                    arrayOld[i] = arrayNew[i];
                }
                oldNumber = newNumber;
            }
        }
    }
}