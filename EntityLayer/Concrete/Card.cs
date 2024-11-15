using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Card
    {
        [Key]
        public string CardNumber { get; set; }
        public string CardholderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVVCVC { get; set; }

        public CardType Type { get; set; }

        public enum CardType
        {
            Visa,
            MasterCard,
            Amex
        }

    }
}
