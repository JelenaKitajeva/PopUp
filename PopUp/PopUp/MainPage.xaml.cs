using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PopUp
{
    public partial class MainPage : ContentPage
    {
        string name = string.Empty;
        string[] questions;
        string[][] choices;
        string[] answers;
        int correctAnswersCount;
        int totalQuestionsCount;
        int currentQuestionIndex = -1;
        Entry nameEntry;
        private List<string> zagadki = new List<string>();
        private Dictionary<string, string> otgadki = new Dictionary<string, string>();
        public MainPage()
        {
            InitializeComponent();
             
        Button alertQbutton = new Button
            {
                Text = "Загадки",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Aquamarine,
                TextColor = Color.Black,
                FontSize = 24
            };
            alertQbutton.Clicked += AlertQbutton_Clicked;

            Button AddQbutton = new Button
            {
                Text = "Добавь загадку",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Aquamarine,
                TextColor = Color.Black,
                FontSize = 24
            };
            AddQbutton.Clicked += AddQbutton_Clicked;

            nameEntry = new Entry
            {
                Placeholder = "Введите ваше имя",
                BackgroundColor = Color.Aquamarine,
                TextColor = Color.Black,
                FontSize = 24
            };
            nameEntry.TextChanged += NameEntry_TextChanged;

            Content = new StackLayout
            {
                Children = {
                new Image { Source = "zagadka.jpg" },
                nameEntry,
                alertQbutton,
                AddQbutton
                }
            };
        }
        async Task SaveDataToFileAsync(string fileName, string data)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullPath = Path.Combine(path, fileName);

            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                await sw.WriteAsync(data).ConfigureAwait(false);
            }
        }
        private async  void AddQbutton_Clicked(object sender, EventArgs e)
        {
            {
                string zagadka = await DisplayPromptAsync("Введите новую загадку", "");
                if (!string.IsNullOrEmpty(zagadka))
                {
                    zagadki.Add(zagadka);
                    await SaveDataToFileAsync("Zagadki.txt", $"{zagadka}\n");
                }
            }
        }

        async void OnAddSolution(object sender, EventArgs e)
        {
            string zagadka = await DisplayActionSheet("Выберите загадку", "Отмена", null, zagadki.ToArray());
            if (zagadka != "Отмена")
            {
                zagadka = await DisplayPromptAsync("Введите новую загадку", "");
                if (!string.IsNullOrEmpty(zagadka))
                {
                    zagadki.Add(zagadka);
                    await SaveDataToFileAsync("Zagadki.txt", $"{zagadka}\n");
                }
            }
        }

       

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = nameEntry.Text;
        }

        private async void AlertQbutton_Clicked(object sender, EventArgs e)
        {
            if (questions == null || answers == null || choices == null)
            {
                questions = new[]
                {
                $"Привет, " + name + "! Ответь: Белым снегом всё одето - Значит, наступает...?",
                $"Привет, " + name + "! Ответь: Ночью каждое оконце Слабо освещает...?",
                $"Привет, " + name + "! Ответь: Друг зверей и друг детей Добрый доктор...?",
                $"Привет, " + name + "! Под деревом четыре льва, Один ушёл, осталось...?",
                $"Привет, " + name + "! Ответь: Кукарекает спросонок Милый, добрый...?"
                };
                choices = new[]
                {
                new[] { "Лето", "Зима", "Осень" },
                new[] { "Солнце", "Луна", "Звезды" },
                new[] { "Бармалей", "Айболит", "Стрелок" },
                new[] { "Два", "Три", "Один" },
                new[] { "Поросёнок", "Телёнок", "Петух" }
                };
                answers = new[] { "Зима", "Луна", "Айболит", "Три", "Петух" };
            }


                        currentQuestionIndex++;
                        if (currentQuestionIndex >= questions.Length)
                        {
                            // Show result
                            await DisplayAlert("Результат", $"Правильных ответов: {correctAnswersCount}/{totalQuestionsCount}", "OK");
                            // Reset
                            correctAnswersCount = 0;
                            totalQuestionsCount = 0;
                            currentQuestionIndex = -1;
                        }
                        else
                        {
                            totalQuestionsCount++;

                            string question = questions[currentQuestionIndex].Replace("{name}", name);
                            string answer = answers[currentQuestionIndex];
                            string[] choice = choices[currentQuestionIndex];

                            var action = await DisplayActionSheet(question, "OK", "Закрыть", choice[0], choice[1], choice[2]);

                            if (action == answer)
                            {
                                correctAnswersCount++;
                                await DisplayAlert("Правильно!", "👍", "OK");
                            }
                            else
                            {
                                await DisplayAlert("Неправильно!", "👎", "OK");
                            }

                            AlertQbutton_Clicked(sender, e); // Go to next question
                        }
                    }
                }
            
    
}



            





        












        


