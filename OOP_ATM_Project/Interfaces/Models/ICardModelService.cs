using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Interfaces.Models
{
    public interface ICardModelService
    {
        Card GetCard();
        void UpdateCard(Card card);
        void UpdatePinHash(string newPinHash);
        void BlockCard();
        string GetPinHash();
        void BlockCard(Card card);
        bool IsCardBlocked();
        bool IsCardInserted();
        bool IsPinHashMatched(string TypedPin, string GeneratedPin);
    }
}
