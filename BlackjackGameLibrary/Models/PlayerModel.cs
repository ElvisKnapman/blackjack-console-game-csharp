using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGameLibrary.Models
{
    public class PlayerModel
    {
        public List<DeckCardModel> playerHand = new List<DeckCardModel>();
        public string PlayerName { get; set; }
        public bool IsDealer { get; set; }

        public PlayerModel(string name, bool isDealer = false)
        {
            PlayerName = name;
            IsDealer = isDealer;
        }
    }
}
