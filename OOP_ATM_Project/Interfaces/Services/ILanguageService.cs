using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface ILanguageService
    {
        void SetLanguage(string language);
        string GetMessage(string messageKey);
        string GetLanguage();
    }
}
