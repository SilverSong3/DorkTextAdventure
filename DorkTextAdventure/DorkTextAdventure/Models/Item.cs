using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DorkTextAdventure.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Noun { get; set; }
        public string Command { get; set; }
        public int Room { get; set; }
        public int Attribute { get; set; }

        public Item() { }





        public override string ToString()
        {
            return $"{Noun}";
        }



    }
}
