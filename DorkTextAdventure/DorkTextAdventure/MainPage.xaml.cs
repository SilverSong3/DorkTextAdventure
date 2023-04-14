using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using DorkTextAdventure.Models;

namespace DorkTextAdventure
{
    public partial class MainPage : ContentPage
    {

        List<Room> rooms = new List<Room>();
        

        public MainPage()
        {
            int roomID = 0;
            Room r = new Room();
            r.ID = 0;
            r.Name = "First Floor";
            r.Description = "You are in a damp and cold hotel. The lights flicker ominously, and you see no one around you.";
            //r.Items[0] = 0;
            
            rooms.Add(r);



            //InitializeComponent();

            Editor editor = new Editor()
            {
                Text = "Dork Adventure 0.01\n\nBy: El Slash" + rooms[roomID].ToString(),
                FlowDirection = FlowDirection.LeftToRight,
                HeightRequest = 500,
                WidthRequest = 100,
                IsReadOnly = true,
                MinimumWidthRequest = 100,
                FontSize = 20,
                TextColor = Color.White,
                Background = new SolidColorBrush(Color.Black),
                BackgroundColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                IsTabStop = false,
            };

            Entry entry = new Entry
            {
                HeightRequest = 70,
                WidthRequest = 100,
                //MinimumWidthRequest = 100,
                FontSize = 40,
                TextColor = Color.White,
                BackgroundColor = Color.DarkSlateBlue,
            };


            Button button = new Button
            {
                Text = "Submit",
                HeightRequest = 70,
                WidthRequest = 50,
                FontSize = 40,
                TextColor = Color.White,
                //Background = new SolidColorBrush(Color.CornflowerBlue),
                BackgroundColor = Color.CornflowerBlue,
                IsTabStop = false,
            };

            //main page view
            StackLayout stackLayout = new StackLayout
            {
                Children = {
                    editor,
                    entry,
                    button,
                },
                WidthRequest = 700,
                HeightRequest = 1500,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.Center,
            };

            

            entry.Completed += async (sender, args) =>
            {
                await DisplayAlert("command: success", "great job", "Okay!");
            };

            button.Clicked += async (sender, args) => 
            {
                entry.Text = "";
                await Action();
                //eh, there's something that can make the thing focused.
            };


            Content = stackLayout;


        }


        public async Task Action()
        {
            await DisplayAlert("You pressed a button. Good job.", "...great job", "Okay...?");
        }



    }
}
