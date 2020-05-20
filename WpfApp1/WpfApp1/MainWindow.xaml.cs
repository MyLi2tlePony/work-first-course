using Brain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brain.Tools;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private int numberOfGraph;
        private int numberOfVertex;
        private int maxLength;
        private double density;
        private static bool isItFirst;
        
        static TextBox getAnsver;
        List<FindPath> all = new List<FindPath> { };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBegin_Click(object sender, RoutedEventArgs e)
        {
            if (isItFirst)
            {
                isItFirst = false;
                ShowAnswer();
            }
        }

        //Выводит ответ
        public void ShowAnswer()
        {
            for (int i = 0; i < numberOfGraph; i++)
            {
                int lengthBestWay = 0;
                int[] arrayBestWay = new int[numberOfVertex];
                all[i].AllIterate(ref arrayBestWay, ref lengthBestWay);
                TextBox text = new TextBox();
                text.Text += $"Лучшая длина цикла: {lengthBestWay}\tВсего найдено циклов: {all[i].GetNumberOfWay()}\nЛучший цикл: ";
                foreach(int arr in arrayBestWay)
                {
                    text.Text += arr + " - "; 
                }
                text.Text += arrayBestWay[0];
                text.Background = Brushes.Transparent;
                text.BorderBrush = Brushes.Transparent;
                text.FontSize = 24;
                MainListBox.Items.Insert(i*3, text);
            } 
        }

        //Выводит значения матрицы
        void ShowArrayBeautiful(string text)
        {
            //Выраынивает и выводит значения ячеек
            try
            {
                if (Convert.ToInt32(text) < 10)
                {
                    getAnsver.Text += Convert.ToString("    ");
                    getAnsver.Text += Convert.ToString(text);
                }
                else if (Convert.ToInt32(text) >= 10 && Convert.ToInt32(text) <= 99)
                {
                    getAnsver.Text += Convert.ToString("   ");
                    getAnsver.Text += Convert.ToString(text);
                }
                else
                {
                    getAnsver.Text += Convert.ToString("  ");
                    getAnsver.Text += Convert.ToString(text);
                }
            }
            catch
            {
                getAnsver.Text += Convert.ToString(text);
            }
        }

        //Закрывает окно
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Разворачивает окно
        private void StateChange_Click(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
            }
        }

        //Сворачивает окно
        private void WindowMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Передвигает окно
        private void TopWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            this.DragMove();
        }

        //Получает значения из настроек
        private void SelectionChanged()
        {
            numberOfGraph = Convert.ToInt32(NumberOfGraph.Text);
            numberOfVertex = Convert.ToInt32(NumberOfVertex.Text);
            maxLength = Convert.ToInt32(MaxLength.Text);
            density = Convert.ToDouble(Density.Text);
        }

        //Создает массивы и выводит их
        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            isItFirst = true;

            //Получает значения из настроек
            SelectionChanged();

            //Проверяем, были ли элементы до этого, если были, то удаляем
            if (all != null)
            {
                all.Clear();
            }

            //Отчищаем главное окно
            MainListBox.Items.Clear();

            //Создаем объекты графов
            for (int i = 0; i < numberOfGraph; i++)
            {
                all.Add(new FindPath(numberOfVertex, density, maxLength));
            }
            
            //Решаем каждый граф и выовдим его
            foreach(FindPath i in all)
            {
                TextBox text = new TextBox();
                text.FontSize = 24;
                text.Background = Brushes.Transparent;
                text.BorderBrush = Brushes.Transparent;
                Grid grid = new Grid();

                MainListBox.Items.Add(text);

                DrowGraph(i, grid);
                MainListBox.Items.Add(grid);
                SetAnsver(ref text);
                i.ShowMatrix(ShowArrayBeautiful);
            }
        }

        //Создаем графически граф
        private void DrowGraph(FindPath obj, Grid can)
        {
            //Создаем список вершина графа
            List<Ellipse> el = new List<Ellipse> { };

            //Размер вершин и расстояние между ними
            int lengthOfPath = numberOfVertex * 90,
                width = 30,
                height = 30;

            if (lengthOfPath > 2000)
            {
                lengthOfPath = 2000;
            }
            
            //Выводим вершины
            for (int i = 0; i < numberOfVertex; i++)
            {
                el.Add(new Ellipse());
                el[i].Width = width;
                el[i].Height = height;
                el[i].Fill = Brushes.Black;
                el[i].VerticalAlignment = VerticalAlignment;
                
                Random rd = new Random();
                double left = rd.Next(lengthOfPath) - (width / 2);
                double top = rd.Next(lengthOfPath) - (height / 2);
               
                Canvas.SetLeft(el[i], left);
                Canvas.SetTop(el[i], top);
                Canvas c = new Canvas();

                c.Children.Add(el[i]);

                can.MinHeight = lengthOfPath;
                
                can.Children.Add(c);
            }

            int[,] array = new int[numberOfVertex, numberOfVertex];
            array = obj.GetMatrix();

            //Проводим линии между вершинами
            for (int i = 0; i < numberOfVertex; i++)
            {
                for (int j = i + 1; j < numberOfVertex; j++)
                {
                    if (array[i, j] != 0)
                    {
                        Line ln = new Line();
                        ln.StrokeThickness = 1;
                        ln.Stroke = Brushes.Black;
                        ln.X1 = Canvas.GetLeft(el[i]) + (width / 2);
                        ln.Y1 = Canvas.GetTop(el[i]) + (height / 2);
                        ln.X2 = Canvas.GetLeft(el[j]) + (width / 2);
                        ln.Y2 = Canvas.GetTop(el[j]) + (height / 2);
                        TextBox text = new TextBox();
                        text.Text = $"{array[i, j]}";
                        text.FontSize = 15;
                        text.Foreground = Brushes.Black;
                        text.Background = Brushes.Transparent;
                        text.BorderBrush = Brushes.Transparent;

                        if(Canvas.GetLeft(el[i]) > Canvas.GetLeft(el[j]))
                        {
                            Canvas.SetLeft(text, Canvas.GetLeft(el[j]) + (Canvas.GetLeft(el[i]) - Canvas.GetLeft(el[j]))/ 2 );
                        }
                        else
                        {
                            Canvas.SetLeft(text, Canvas.GetLeft(el[i]) + (Canvas.GetLeft(el[j]) - Canvas.GetLeft(el[i])) / 2);
                        }

                        if (Canvas.GetTop(el[i]) > Canvas.GetTop(el[j]))
                        {
                            Canvas.SetTop(text, Canvas.GetTop(el[j]) + (Canvas.GetTop(el[i]) - Canvas.GetTop(el[j])) / 2);
                        }
                        else
                        {
                            Canvas.SetTop(text, Canvas.GetTop(el[i]) + (Canvas.GetTop(el[j]) - Canvas.GetTop(el[i])) / 2);
                        }

                        Canvas c = new Canvas();
                        c.Children.Add(text);
                        can.Children.Add(c);
                        can.Children.Add(ln);
                    }
                }
            }

            //Подписываем вершины
            for (int i = 0; i < numberOfVertex; i++)
            {
                TextBox text = new TextBox();
                text.Text = $"{i}";
                text.FontSize = 15;
                text.Foreground = Brushes.White;
                text.Background = Brushes.Transparent;
                text.BorderBrush = Brushes.Transparent;

                Canvas.SetLeft(text, Canvas.GetLeft(el[i]) + (width / 4));
                Canvas.SetTop(text, Canvas.GetTop(el[i]) + (height / 4));

                Canvas c = new Canvas();
                c.Children.Add(text);
                can.Children.Add(c);
            }
        }

        private void SetAnsver(ref TextBox text)
        {
            getAnsver = text;
        }
    }
}
