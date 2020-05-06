using Brain.Tools;

namespace Brain
{
    public interface IInteractionWithArray
    {
        void ShowMatrix(WorkWithArrays.DShow message);
        void AllIterate(ref int[] arrayBestWay, ref int lengthBestWay);
    }
}
