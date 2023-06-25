using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Enums
{
    public class ATMFunctionality
    {
        public enum Functionality
        {
            None, // Brak aktywnej funkcji (np. w razie awarii bankomatu, przed wyborem języka lub po wyborze języka ale przed wybraniem funkcji - jest to domyślny stan pracy bankomatu)
            Deposit, // Aktywna funkcja wpłaty
            Withdrawal, // Aktywna funkcja wypłaty
            BalanceCheck, // Aktywna funkcja sprawdzenia stanu konta
            LanguageChoosing, // Aktywna funkcja wyboru języka
            PinChanging, // Aktywna funkcja zmiany PINu
            TransactionHistory // Aktywna funkcja wyświetlania historii transakcji
        }
    }
}
