using BlackjackGameLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGameLibrary.Models
{
    public class DeckCardModel
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
    }
}
