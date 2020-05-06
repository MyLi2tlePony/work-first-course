namespace Brain.Tools
{
    public static class CheckConditions
    {
        //Проверяет, есть ли путь между вершинами
        public static bool WayBetweenVertex(ref int[,] array, int startVertex, int endVertex)
        {
            if (endVertex < array.Length / (array.GetUpperBound(0) + 1) && array[startVertex,endVertex] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Проверяет, использовали ли мы данную вершину
        public static bool UseVertexBefore(ref int[] array, int vertex)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == vertex)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
