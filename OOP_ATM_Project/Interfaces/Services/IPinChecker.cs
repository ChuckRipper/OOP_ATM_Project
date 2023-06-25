using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface IPinChecker
    {
        bool CheckPin(string pin);
        bool ChangePin(string newPin);
    }
}
