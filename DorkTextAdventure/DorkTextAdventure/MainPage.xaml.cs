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
        
        enum Exits { North, South, East, West }
        List<Room> rooms = new List<Room>();
        List<Item> items = new List<Item>();


        string[] verbs = new string[]
        {
            "inv", "sco", "loc", "exa", "tak", "get",
            "dro", "rea", "ope", "dig", "lig", "qui"
        };

        string v3 = "";
        string n3 = "";
        int num = 1;
        int score = 0;

        //Public elements:
        Editor editorDisplay;


        public MainPage()
        {
            LoadRoomData();
            LoadItemData();

            string game = "Dork Adventure 0.01\n\nBy: El Slash";
            string desc = rooms[num].ToString();

            editorDisplay = new Editor()
            {
                Text = game + desc, //"Dork Adventure 0.01\n\nBy: El Slash" + rooms[roomID].ToString(),
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
                    editorDisplay,
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

        public void DisplayRoom()
        {
            editorDisplay.Text += rooms[num].ToString();
        }


        public void LoadRoomData()
        {
            //Use to create a room

            Room r = new Room();

            r.ID = 0;
            r.Name = "Limbo";
            r.Description = "out in Limbo.";
            r.Exits = new int[] { };
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 1;
            r.Name = "Village Green";
            r.Description = "on the village green.";
            r.Exits = new int[] { 3, 0, 4, 2 };
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 2;
            r.Name = "School";
            r.Description = "in a one-room school.";
            r.Exits = new int[] { 0, 0, 1, 6 };
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 3;
            r.Name = "Church";
            r.Description = "in a rustic church.";
            r.Exits = new int[] { 0, 1, 0, 0};
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 4;
            r.Name = "Bank";
            r.Description = "in the bank of David.";
            r.Exits = new int[] { 0, 0, 0, 1};
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 5;
            r.Name = "Safe";
            r.Description = "in the safe of the bank.";
            r.Exits = new int[] { 4, 0, 0, 0 };
            r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 6;
            r.Name = "Storeroom";
            r.Description = "in a dusty storeroom.";
            r.Exits = new int[] { 0, 0, 2, 0};
            r.Items = new int[] { };
            rooms.Add(r);

        }
        public void LoadItemData()
        {
            //Use to create items

            Item I = new Item();

            I.Id = 0;
            I.Noun = "a piece of chalk";
            //I.Description = "";
            I.Command = "cha";
            I.Room = 2;
            I.Attribute = 2;

            I = new Item();
            I.Id = 1;
            I.Noun = "a huge tree";
            I.Command = "tre";
            I.Room = 1;
            I.Attribute = 0;

            I = new Item();
            I.Id = 2;
            I.Noun = "a sign";
            I.Command = "sig";
            I.Room = 6;
            I.Attribute = 0;

            I = new Item();
            I.Id = 3;
            I.Noun = "a brass key";
            I.Command = "key";
            I.Room = 0;
            I.Attribute = 2;

            I = new Item();
            I.Id = 4;
            I.Noun = "a bag of coins";
            I.Command = "coi";
            I.Room = 5;
            I.Attribute = 3;

            I = new Item();
            I.Id = 5;
            I.Noun = "wooden pews";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I = new Item();
            I.Id = 6;
            I.Noun = "an altar";
            I.Command = "";
            I.Room = 3;
            I.Attribute = 0;

            I = new Item();
            I.Id = 7;
            I.Noun = "a long white candle";
            I.Command = "can";
            I.Room = 0;
            I.Attribute = 0;

            I = new Item();
            I.Id = 8;
            I.Noun = "a box of matches";
            I.Command = "mat";
            I.Room = 0;
            I.Attribute = 0;

            I = new Item();
            I.Id = 9;
            I.Noun = "gold nuggets";
            I.Command = "gol";
            I.Room = 0;
            I.Attribute = 3;

            I = new Item();
            I.Id = 10;
            I.Noun = "a large wooden desk";
            I.Command = "des";
            I.Room = 0;
            I.Attribute = 0;

            I = new Item();
            I.Id = 11;
            I.Noun = "a big ...";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I = new Item();
            I.Id = 12;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I.Id = 0;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I.Id = 0;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I.Id = 0;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

            I.Id = 0;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;

        }
    }
}
