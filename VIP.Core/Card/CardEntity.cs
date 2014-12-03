using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;

namespace VIP.Core.Card
{
    public class CardEntity : EditorEntity
    {
        public string CardID { get; set; }
        public string CardNumber { get; set; }
        public double Rebate { get; set; }
        public int CardCategory { get; set; }
    }
}
