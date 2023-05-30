using _5vRad.Model;
using _5vRad.Resources;
using _5vRad.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _5vRad.ViewModels
{
    internal class GameViewModel : ViewModel
    {

        private ObservableCollection<ObservableCollection<FieldModel>> f = new();
        public ObservableCollection<ObservableCollection<FieldModel>> F { get => f; set => Set(ref f, value); }

        private bool stepWhite = false;
        public bool StepWhite { get => stepWhite; set => Set(ref stepWhite, value); }

        public GameViewModel()
        {
            StartGame();
            ReadRules();
        }

        public void ReadRules()
        {
            string path = "../../../Resources/Rules.txt";
            using StreamReader reader = new(path);
            string text = reader.ReadToEnd();
            MessageBox.Show(text + "\n\nДля запуска игры нажмите OK", "Правила игры");
        }

        public void StartGame()
        {
            GenerateField();
        }

        public void GenerateField()
        {
            f.Clear();
            for (int i = 0; i < 15; i++)
            {
                ObservableCollection<FieldModel> row = new();
                for (int j = 0; j < 15; j++)
                {
                    row.Add(new FieldModel() { I = i, J = j });
                }
                f.Add(row);
            }
        }

        public void Click(FieldModel fm)
        {
            if (fm.Color == Colors.None)
            {
                fm.Color = StepWhite ? Colors.White : Colors.Black;
                StepWhite = !StepWhite;

                CheckWin();
                CheckDraw();
            }
        }
        public static bool OnField(int i, int j) => i < 15 && i >= 0 && j < 15 && j >= 0;

        public static void Quit() { Application.Current.Shutdown(); }

        public void RestartGame(string text)
        {
            MessageBoxResult mbr = MessageBox.Show($"{text} Начать заново?", "Конец игры", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes) StartGame();
            else Quit();
        }

        public void Win(string color)
        {
            string colorName = (color == Colors.White) ? "белые" : "черные";
            RestartGame($"Выйграли {colorName}.");
        }

        public void CheckDraw()
        {
            bool emptyFields = false;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (F[i][j].Color == Colors.None)
                    {
                        emptyFields = true;
                    }

                    if (emptyFields) break;
                }
                if (emptyFields) break;
            }

            if (!emptyFields)
            {
                Draw();
            }
        }
        public void Draw()
        {
            RestartGame("Ничья");
        }
        public void CheckWin()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                    FieldModel fm = F[i][j];
                    if (fm.Color == Colors.None) continue;

                    int count;

                    // проверяем вертикально вниз
                    count = 0;
                    for (int a = 0; a < 5; a++)
                    {
                        if (OnField(i + a, j) && F[i + a][j].Color == fm.Color)
                        {
                            count++;
                        }
                    }
                    if (count == 5)
                    {
                        Win(fm.Color);
                    }

                    // проверяем по диагонали влево вниз
                    count = 0;
                    for (int a = 0; a < 5; a++)
                    {
                        if (OnField(i + a, j - a) && f[i + a][j - a].Color == fm.Color)
                        {
                            count++;
                        }
                    }
                    if (count == 5)
                    {
                        Win(fm.Color);
                    }

                    // проверяем по диагонали вправо вниз
                    count = 0;
                    for (int a = 0; a < 5; a++)
                    {
                        if (OnField(i + a, j + a) && f[i + a][j + a].Color == fm.Color)
                        {
                            count++;
                        }
                    }
                    if (count == 5)
                    {
                        Win(fm.Color);
                    }

                    // проверяем по горизонтали вправо
                    count = 0;
                    for (int a = 0; a < 5; a++)
                    {
                        if (OnField(i, j + a) && f[i][j + a].Color == fm.Color)
                        {
                            count++;
                        }
                    }
                    if (count == 5)
                    {
                        Win(fm.Color);
                    }

                }
            }
        }
    }
}
