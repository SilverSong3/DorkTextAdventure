using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using DorkTextAdventure.Models;
using System.Runtime.InteropServices;

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

            string game = "Dork Adventure 0.01\n\nBy: El Slash.\n\n";
            string desc = rooms[num].ToString();
            string floor = GetItems();

            editorDisplay = new Editor()
            {
                Text = game + desc + floor, //"Dork Adventure 0.01\n\nBy: El Slash" + rooms[roomID].ToString(),
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
                ParseCommand(editorDisplay.Text);
                entry.Text = "";


                //await DisplayAlert("command: success", "great job", "Okay!");
            };

            button.Clicked += async (sender, args) => 
            {
                ParseCommand(editorDisplay.Text);
                entry.Text = "";
                //HandleCommand(editorDisplay.Text, );
                //await Action();
                //eh, there's something that can make the thing focused.
            };

            Content = stackLayout;

        }

        public void ParseCommand(string cmd)
        {

            if (cmd.Length == 0) { return; }

            string noun = "";
            string verb = "";

            if (cmd.Contains(" "))
            {
                verb = cmd.Substring(0, cmd.IndexOf(" "));
                noun = cmd.Substring(cmd.IndexOf(" ") + 1);

                if(verb == "go")
                {
                    verb = noun;
                    MoveToRoom(noun);//essentially passes the verb to the room to move to the room.
                    return;
                }

                HandleCommand(verb, noun);
            }
            else
            {
                //does not account for the go -- TODO: check if verb is go, if so, show no direction provided (Go Where?)
                verb = (cmd.Length) > 1 ? cmd.Substring(0, 3) : verb = cmd;
                noun = "";

                string[] dirs = new string[] { "n", "nor", "s", "sou", "e", "eas", "w", "wes", "go"};

                foreach(string d in dirs)
                {
                    if(d == verb) { MoveToRoom(verb); DisplayRoom(); return; }
                }

                string[] verbs = new string[] { "inv", "sco", "loo", "dig", "lig", "qui" };
                foreach(string v in verbs)
                {
                    if(v == verb) { HandleCommand(v, ""); return; }
                }

                verbs = new string[] { "exa", "tak", "dro", "get", "lig", "rea" };
                foreach (string v in verbs)
                {
                    if (v == verb) { HandleCommand(v, noun); return; }
                }
            }
        }

        //public async Task Action()
        //{
            //await DisplayAlert("You pressed a button. Good job.", "...great job", "Okay...?");
        //}

        public void HandleCommand(string verb, string noun)
        {
            switch(verb)
            {
                case "loo"://look
                    {
                        editorDisplay.Text += "\n\n";
                        break;
                    }
                case "inv"://inventory
                    {
                        editorDisplay.Text += "\n\n";
                        break;
                    }
                case "sco"://score
                    {
                        editorDisplay.Text += $"You have scored {score} points out of 20.\n\n";
                        break;
                    }
                case "":
                    {
                        editorDisplay.Text += "\n\n";
                        break;
                    }
                case "exa":
                    {
                        editorDisplay.Text += $"Yup, It's a {noun}...\n\n";
                        break;
                    }
                case "get":
                case "tak"://take/get
                    {
                        editorDisplay.Text += $"You pick up a {noun}.\n\n";
                        break;
                    }
                case "dro"://drop
                    {
                        editorDisplay.Text += "Read what?\n\n";
                        break;
                    }
                case "rea":
                    {
                        editorDisplay.Text += "\n\n";
                        break;
                    }
                case "ope"://open
                    {
                        editorDisplay.Text += "Open what?\n\n";
                        break;
                    }
                case "dig"://dig
                    {
                        editorDisplay.Text += "Going to China?\n\n";
                        break;
                    }
                case "lig"://light
                    {
                        editorDisplay.Text += "Light what?\n\n";
                        break;
                    }
                case "qui":
                    {
                        editorDisplay.Text += "Goodbye!";
                        //Environment.Exit(0);


                        break;
                    }

            }

            //Display room after processing the commands.
            DisplayRoom();
        }


        public void DisplayRoom()
        {
            editorDisplay.Text += rooms[num].ToString();
            editorDisplay.Text += GetItems();
        }

        public void MoveToRoom(string verb)
        {
            string where = "Go Where?\n\n";
            switch(verb)
            {
                case "n":
                case "nor":
                    {
                        if (rooms[num].Exits[0] != 0) { num = rooms[num].Exits[0]; }
                        else { editorDisplay.Text += where; }
                        break;
                    }
                case "s":
                case "sou":
                    {
                        if (rooms[num].Exits[1] != 0) { num = rooms[num].Exits[1]; }
                        else { editorDisplay.Text += where; }
                        break;
                    }
                case "e":
                case "eas":
                    {
                        if (rooms[num].Exits[2] != 0) { num = rooms[num].Exits[2]; }
                        else { editorDisplay.Text += where; }
                        break;
                    }
                case "w":
                case "wes":
                    {
                        if (rooms[num].Exits[3] != 0) { num = rooms[num].Exits[3]; }
                        else { editorDisplay.Text += where; }
                        break;
                    }
            }

            //DisplayRoom();

        }

        public string GetItems()
        {
            string list = "";

            foreach(Item i in items)
            {
                if (i.Room == num)
                {
                    list += $"{i.ToString()}, ";
                }
            }

            if (list.Length != 0)
            {
                list = list.Substring(0, list.Length - 2) + ".";
            }
            else
            {
                list = "Nothing...";
            }

            //list = list.Substring(list.Length, list.Length - 2);

            return "You see: " + list + "\n\n";
        }


        public void LoadRoomData()
        {
            //Use to create a room

            Room r = new Room();

            r.ID = 0;
            r.Name = "Limbo";
            r.Description = "out in Limbo.";
            r.Exits = new int[] { };
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 1;
            r.Name = "Village Green";
            r.Description = "on the village green.";
            r.Exits = new int[] { 3, 0, 4, 2 };
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 2;
            r.Name = "School";
            r.Description = "in a one-room school.";
            r.Exits = new int[] { 0, 0, 1, 6 };
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 3;
            r.Name = "Church";
            r.Description = "in a rustic church.";
            r.Exits = new int[] { 0, 1, 0, 0};
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 4;
            r.Name = "Bank";
            r.Description = "in the bank of David.";
            r.Exits = new int[] { 0, 0, 0, 1};
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 5;
            r.Name = "Safe";
            r.Description = "in the safe of the bank.";
            r.Exits = new int[] { 4, 0, 0, 0 };
            //r.Items = new int[] { };
            rooms.Add(r);

            r = new Room();
            r.ID = 6;
            r.Name = "Storeroom";
            r.Description = "in a dusty storeroom.";
            r.Exits = new int[] { 0, 0, 2, 0};
            //r.Items = new int[] { };
            rooms.Add(r);

        }
        public void LoadItemData()
        {
            //Attributes: 
                //3 - Treasures,
                //2 - Other moveable objects,
                //1 - Imovable objects,
                //0 - Scenery

            //Rooms:
                //


            //Use to create items

            Item I = new Item();

            I.Id = 0;
            I.Noun = "a piece of chalk";
            //I.Description = "";
            I.Command = "cha";
            I.Room = 2;
            I.Attribute = 2;
            items.Add(I);

            I = new Item();
            I.Id = 1;
            I.Noun = "a huge tree";
            I.Command = "tre";
            I.Room = 1;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 2;
            I.Noun = "a sign";
            I.Command = "sig";
            I.Room = 6;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 3;
            I.Noun = "a brass key";
            I.Command = "key";
            I.Room = 0;
            I.Attribute = 2;
            items.Add(I);

            I = new Item();
            I.Id = 4;
            I.Noun = "a bag of coins";
            I.Command = "coi";
            I.Room = 5;
            I.Attribute = 3;
            items.Add(I);

            I = new Item();
            I.Id = 5;
            I.Noun = "wooden pews";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 6;
            I.Noun = "an altar";
            I.Command = "";
            I.Room = 3;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 7;
            I.Noun = "a long white candle";
            I.Command = "can";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 8;
            I.Noun = "a box of matches";
            I.Command = "mat";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 9;
            I.Noun = "gold nuggets";
            I.Command = "gol";
            I.Room = 0;
            I.Attribute = 3;
            items.Add(I);

            I = new Item();
            I.Id = 10;
            I.Noun = "a large wooden desk";
            I.Command = "des";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 11;
            I.Noun = "a big ...";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

            I = new Item();
            I.Id = 12;
            I.Noun = "";
            I.Command = "";
            I.Room = 0;
            I.Attribute = 0;
            items.Add(I);

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
