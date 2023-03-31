using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PopUp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Button alertQbutton = new Button
            {
                Text = "Загадки",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertQbutton.Clicked += AlertQbutton_Clicked;

            Entry nameEntry = new Entry
            {
                Placeholder = "Введите ваше имя",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Content = new StackLayout
            {
                Children = {
            nameEntry,
            alertQbutton
        }
            };
            alertQbutton.Clicked += AlertQbutton_Clicked;
            Button alertButton = new Button
            {
                Text = "Teade",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertButton.Clicked += AlertButton_Clicked;
            Button alertYesNoButton = new Button
            {
                Text = "Jah või ei",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertYesNoButton.Clicked += AlertYesNoButton_Clicked;
            Button alertListButtom = new Button
            {
                Text = "Valik",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            Button alertQuestButton = new Button
            {
                Text = "Kusimus",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertQuestButton.Clicked += AlertQuestButton_Clicked;
            alertListButtom.Clicked += AlertListButtom_Clicked;
            Content = new StackLayout { Children = { alertButton, alertYesNoButton, alertListButtom, alertQuestButton, alertQbutton } };
        }

        private async void AlertQbutton_Clicked(object sender, EventArgs e)
        {
            string name = string.Empty;

            if (Content is StackLayout stackLayout)
            {
                var nameEntry = stackLayout.Children.FirstOrDefault(c => c is Entry) as Entry;
                if (nameEntry != null && !string.IsNullOrWhiteSpace(nameEntry.Text))
                {
                    name = nameEntry.Text;
                }
            }

            var action = await DisplayActionSheet("Ответь: Зимой и летом одним цветом?", "OK", "Закрыть", "Крокодил", "Ёлка","Снег");

            if (action == "Ёлка")
            {
                await DisplayAlert("Правильно!", "👍", "OK");
            }
            else
            {
                await DisplayAlert("Неправильно!", "👎", "OK");
            }


        }

        private async void AlertQuestButton_Clicked(object sender, EventArgs e)
        {
            string result1 = await DisplayPromptAsync("Kusimus", "Kuidas laheb?", placeholder: "Tore!");
            string result2 = await DisplayPromptAsync("Vasta", "Millega vordub 5 + 5?", maxLength: 2, keyboard: Keyboard.Numeric);

        }

        private async void AlertListButtom_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Mida teha", "Loobu", "Kustutada", "Tantsida", "Laulda", "Joonistada");
        }

        private async void AlertYesNoButton_Clicked(object sender, EventArgs e)
        {                                                 
            bool result = await DisplayAlert("Kinnitus", "Kas oled kindel?", "Olen kindel", "Ei ole kindel");
            await DisplayAlert("Teade", "Teie valik on: " + (result ? "Jah" : "Ei"), "OK");
        }

        private void AlertButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Teade", "Teil on uus teade", "OK");
        }
    }
}
