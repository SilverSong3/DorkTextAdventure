using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DorkTextAdventure.Models
{
    public class Room
    {
        //north = 0, east, south, west, up, down, No exit = -1
        public int ID { get; set; }
        public string Name { get; set; }    
        public int Level { get; set; }
        public string Description { get; set; }


        public int[] Exits { get; set; }
        public int[] Items { get; set; }


        public Room()
        {
            Exits = new int[0];
            Items = new int[0];
        }

        //public string CreateExitString()
        //{
        //    string exits = "";

        //    if (Exits.Count() == 0)
        //    {
        //        exits = "Nowhere, you are trapped...?";
        //    }

        //    foreach (int i in Exits)
        //    {
        //        //TODO: look up exits from id...thing
        //    }
        //    return exits;
        //}

        //public string CreateItemsString()
        //{
        //    string items = "";

        //    if (Items.Count() == 0)
        //    {
        //        items = "There is nothing to see here.";
        //    }


        //    foreach (int i in Items)
        //    {
        //        //TODO: look up item from ID.
        //    }
        //    return items;
        //}


        public override string ToString()
        {
            //string exits = CreateExitString();
            //string items = CreateItemsString();

            //return $"{Name}: {ID}\n\n{Description}\n\n" +
            //$"You can go: {exits}\n\n" +
            //$"Laying on the floor: {items}\n\n\n" +
            //$"What do you wish to do? ";

            string value = "";

            value = $"{Name}: {ID}\n" +
                $"You are {Description}. \n\n" +
                $"You can go: {GetExits()}.\n\n" +
                $"You see: {GetItems()}.\n\n";

            return value;
        }


        public string GetExits()
        {
            string exits = "";
            if (Exits[0] > 0) { exits += "North, "; }
            if (Exits[1] > 0) { exits += "South, "; }
            if (Exits[2] > 0) { exits += "East, "; }
            if (Exits[3] > 0) { exits += "West"; }

            if(exits.Substring(exits.Length - 2) == ", ")
            {
                exits = exits.Substring(0, exits.Length - 2); //It may remove the commas...
            }

            return exits.Length > 0 ? exits : "Nowhere...";//check if there are places to go.
        }

        public string GetItems()
        {
            string items = "";
            return items;
        }








    }
}
