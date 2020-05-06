using Brain.Tools;

namespace Brain
{
    public class FindPath : IInteractionWithArray
    {
        //Матрица смежности
        private int[,] arrayMatrix;

        public FindPath(int NumberOfVertex, double density, int maxLength)
        {
            //Создаем массив нужного размера
            arrayMatrix = new int[NumberOfVertex, NumberOfVertex];

            //Заполняем массив
            WorkWithArrays.CreateArray(ref arrayMatrix, density, maxLength);
        }

        public void AllIterate(ref int[] arrayBestWay, ref int lengthBestWay)
        {
            Decision.AllIterate.GetBestCycle(ref arrayMatrix, ref arrayBestWay, ref lengthBestWay);
        }

        //Выводит массив
        public void ShowMatrix(WorkWithArrays.DShow message)
        {
            WorkWithArrays.ShowArray(arrayMatrix, message);
        }

        public int GetNumberOfWay()
        {
            return Decision.AllIterate.GetNumberOfWay();
        }

        public ref int[,] GetMatrix()
        {
            return ref arrayMatrix;
        }
    }
}
