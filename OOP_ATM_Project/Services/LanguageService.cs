using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Services
{
    public class LanguageService : ILanguageService
    {
        #region Classes

        #endregion

        #region Fields 
        private string _selectedLanguage;
        private List<string> _languages = new List<string>();
        private List<string> _messageKeys = new List<string>();
        private Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>();
        private IAuditLogger _auditLogger; // Do logowania zdarzeń

        private static LanguageService _instance; // Wzorzec Singleton
        #endregion

        #region Properties
        public string SelectedLanguage => _selectedLanguage;
        public List<string> Languages => _languages;
        public List<string> MessageKeys => _messageKeys;
        public Dictionary<string, Dictionary<string, string>> Messages => _messages;
        public IAuditLogger AuditLogger => _auditLogger;

        public static LanguageService Instance // Wzorzec Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LanguageService();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public LanguageService()
        {
            _languages = new List<string>
            {
                "Polski",
                "English",
                "Deutsch",
                "Français",
                "Русский",
                "Українська",
                "Čeština",
                "Slovenčina"
            };

            _messageKeys = new List<string>
            {
                "LanguageChoice", // Tu będzie nazwa języka
                "Welcome", // Powitanie
                "InsertCard", // Prośba o włożenie karty
                "CardBlocked", // Komunikat o zablokowanej karcie
                "EnterPIN", // Prośba o wprowadzenie PINu
                "WrongPIN", // Komunikat o błędnym PINie
                "CardLockWarning", // Komunikat po drugim błędnie podanym PINie, iż po trzecim karta zostanie zablokowana
                "CardLockError", // Komunikat o zablokowanej karcie po wprowadzeniu trzech razy błędnego PINu
                "CashWithdrawal", // Wybór wypłaty gotówki
                "CashDeposit", // Wybór wpłaty gotówki
                "ShowLast5Transactions", // Wybór wyświetlenia ostatnich 5 transakcji
                "ShowBalance", // Wybór wyświetlenia salda
                "ChangePIN", // Wybór zmiany PINu
                "RepeatPIN", // Prośba o powtórzenie PINu
                "PinMismatchWarning", // Komunikat o niezgodności PINów przy próbie zmiany PINu
                "WithdrawalNotPossibleFunds", // Komunikat o braku środków na koncie
                "WithdrawalNotPossibleATM", // Komunikat o braku wystarczającej ilości banknotów w bankomacie
                "WithdrawalProposal", // Propozycja wypłaty: {0} // Kwota do dodania dynamicznie w miejscu użycia
                "DepositNotPossible", // Komunikat o niemożliwości wpłaty z powodu pełnego bankomatu
                "TakeCard", // Prośba o zabranie karty
                "ATMNotAvailable" // Komunikat o niedostępności bankomatu
            };

            var polishMessages = new List<string>
            {
                "Polski",
                "Witamy w bankomacie!",
                "Proszę włożyć kartę",
                "Karta zablokowana",
                "Proszę wprowadzić PIN",
                "Błędny PIN",
                "Uwaga! Po trzecim błędnym PINie karta zostanie zablokowana",
                "Podano 3 razy błędnie kod PIN. Karta została zablokowana",
                "Wypłata gotówki",
                "Wpłata gotówki",
                "Wyświetl ostatnie 5 transakcji",
                "Wyświetl saldo",
                "Zmień PIN",
                "Proszę powtórzyć PIN",
                "PINy nie są zgodne",
                "Brak środków na koncie",
                "Brak nominałów do wypłaty",
                "Brak możliwości wypłaty gotówki. Proponowana kwota wypłaty: ",
                "Brak możliwości wpłaty gotówki. Bankomat jest pełny",
                "Proszę zabrać kartę",
                "Bankomat niedostępny"
            };

            var englishMessages = new List<string>
            {
                "English",
                "Welcome to the ATM!",
                "Please insert your card",
                "Card blocked",
                "Please enter your PIN",
                "Wrong PIN",
                "Warning! After the third wrong PIN, the card will be blocked",
                "3 times wrong PIN entered. The card has been blocked",
                "Cash withdrawal",
                "Cash deposit",
                "Show last 5 transactions",
                "Show balance",
                "Change PIN",
                "Please repeat your PIN",
                "PINs do not match",
                "Insufficient funds",
                "Insufficient banknotes",
                "Cash withdrawal not possible. Proposed withdrawal amount: ",
                "Cash deposit not possible. ATM is full",
                "Please take your card",
                "ATM not available"
            };

            var germanMessages = new List<string>
            {
                "Deutsch",
                "Willkommen am Geldautomaten!",
                "Bitte legen Sie Ihre Karte ein",
                "Karte gesperrt",
                "Bitte geben Sie Ihre PIN ein",
                "Falsche PIN",
                "Warnung! Nach der dritten falschen PIN wird die Karte gesperrt",
                "3 mal falsche PIN eingegeben. Die Karte wurde gesperrt",
                "Bargeldabhebung",
                "Bargeldeinzahlung",
                "Zeige die letzten 5 Transaktionen",
                "Zeige Saldo",
                "PIN ändern",
                "Bitte wiederholen Sie Ihre PIN",
                "PINs stimmen nicht überein",
                "Unzureichende Mittel",
                "Unzureichende Banknoten",
                "Bargeldabhebung nicht möglich. Vorgeschlagener Abhebungsbetrag: ",
                "Bargeldeinzahlung nicht möglich. Der Geldautomat ist voll",
                "Bitte nehmen Sie Ihre Karte",
                "Geldautomat nicht verfügbar"
            };

            var frenchMessages = new List<string>
            {
                "Français",
                "Bienvenue à l'ATM!",
                "Veuillez insérer votre carte",
                "Carte bloquée",
                "Veuillez entrer votre code PIN",
                "PIN incorrect",
                "Attention! Après le troisième code PIN erroné, la carte sera bloquée",
                "3 fois PIN erroné entré. La carte a été bloquée",
                "Retrait d'espèces",
                "Dépôt d'espèces",
                "Afficher les 5 dernières transactions",
                "Afficher le solde",
                "Changer le code PIN",
                "Veuillez répéter votre code PIN",
                "Les PIN ne correspondent pas",
                "Fonds insuffisants",
                "Billets de banque insuffisants",
                "Retrait d'espèces impossible. Montant de retrait proposé: ",
                "Dépôt d'espèces impossible. Le guichet automatique est plein",
                "Veuillez prendre votre carte",
                "DAB indisponible"
            };

            var russianMessages = new List<string>
            {
                "Русский",
                "Добро пожаловать в банкомат!",
                "Пожалуйста, вставьте карту",
                "Карта заблокирована",
                "Пожалуйста, введите свой PIN-код",
                "Неправильный PIN-код",
                "Внимание! После третьего неправильного PIN-кода карта будет заблокирована",
                "3 раза неправильный PIN-код введен. Карта была заблокирована",
                "Снятие наличных",
                "Внесение наличных",
                "Показать последние 5 транзакций",
                "Показать баланс",
                "Изменить PIN-код",
                "Пожалуйста, повторите свой PIN-код",
                "PIN-коды не совпадают",
                "Недостаточно средств",
                "Недостаточно банкнот",
                "Невозможно снять наличные. Предлагаемая сумма снятия: ",
                "Невозможно внести наличные. Банкомат заполнен",
                "Пожалуйста, возьмите свою карту",
                "Банкомат недоступен"
            };
            
            var ukrainianMessages = new List<string>
            {
                "Українська",
                "Ласкаво просимо до банкомату!",
                "Будь ласка, вставте картку",
                "Картка заблокована",
                "Будь ласка, введіть свій PIN-код",
                "Неправильний PIN-код",
                "Увага! Після третього неправильного PIN-коду карта буде заблокована",
                "3 рази неправильний PIN-код введений. Карта була заблокована",
                "Зняття готівки",
                "Внесення готівки",
                "Показати останні 5 транзакцій",
                "Показати баланс",
                "Змінити PIN-код",
                "Будь ласка, повторіть свій PIN-код",
                "PIN-коди не збігаються",
                "Недостатньо коштів",
                "Недостатньо банкнот",
                "Неможливо зняти готівку. Запропонована сума зняття: ",
                "Неможливо внести готівку. Банкомат заповнений",
                "Будь ласка, візьміть свою карту",
                "Банкомат недоступний"
            };

            var czechMessages = new List<string>
            {
                "Čeština",
                "Vítejte v bankomatu!",
                "Vložte prosím vaši kartu",
                "Karta je blokována",
                "Zadejte prosím váš PIN",
                "Špatný PIN",
                "Varování! Po třetím špatném PINu bude karta zablokována",
                "3x zadaný špatný PIN. Karta byla zablokována",
                "Výběr hotovosti",
                "Vklad hotovosti",
                "Zobrazit posledních 5 transakcí",
                "Zobrazit zůstatek",
                "Změnit PIN",
                "Zopakujte prosím váš PIN",
                "PINy se neshodují",
                "Nedostatečné prostředky",
                "Nedostatek bankovek",
                "Výběr hotovosti není možný. Navrhovaná výše výběru: ",
                "Vklad hotovosti není možný. Bankomat je plný",
                "Vyjměte prosím vaši kartu",
                "Bankomat není k dispozici"
            };

            var slovakMessages = new List<string>
            {
                "Slovenčina",
                "Vitajte v bankomate!",
                "Vložte prosím vašu kartu",
                "Karta je blokovaná",
                "Zadajte prosím váš PIN",
                "Zlý PIN",
                "Varovanie! Po tretom zlom PINe bude karta zablokovaná",
                "3x zadaný zlý PIN. Karta bola zablokovaná",
                "Výber hotovosti",
                "Vklad hotovosti",
                "Zobraziť posledných 5 transakcií",
                "Zobraziť zostatok",
                "Zmeniť PIN",
                "Zopakujte prosím váš PIN",
                "PINy sa nezhodujú",
                "Nedostatočné prostriedky",
                "Nedostatok bankoviek",
                "Výber hotovosti nie je možný. Navrhovaná výška výberu: ",
                "Vklad hotovosti nie je možný. Bankomat je plný",
                "Vyberte prosím vašu kartu",
                "Bankomat nie je k dispozícii"
            };

            var allMessages = new List<List<string>> // Lista list wiadomości
            {
                polishMessages,
                englishMessages,
                germanMessages,
                frenchMessages,
                russianMessages,
                ukrainianMessages,
                czechMessages,
                slovakMessages
            };

            // Przejście przez wszystkie języki i dodanie do _messages słowników z komunikatami.
            for (int j = 0; j < _languages.Count; j++)
            {
                var language = _languages[j];
                var languageMessagesList = allMessages[j];
                var languageMessages = new Dictionary<string, string>();

                // Przejście przez wszystkie klucze i dodanie odpowiednich komunikatów do słownika dla danego języka.
                for (int i = 0; i < _messageKeys.Count; i++)
                {
                    var key = _messageKeys[i];
                    string value;

                    // Sprawdzenie, czy klucz to "LanguageChoice", wtedy wartością jest nazwa języka.
                    if (key == "LanguageChoice")
                    {
                        value = language;
                    }
                    else
                    {
                        // W przeciwnym razie używamy wartości z listy komunikatów dla danego języka. 
                        value = languageMessagesList[i];
                    }
                    languageMessages.Add(key, value);
                }
                _messages.Add(language, languageMessages);
            }
        }

        public LanguageService(string language)
        {
            _selectedLanguage = language;
        }

        #endregion

        #region Methods
        public string GetLanguage()
        {
            AuditLogger.Log(level: LogLevel.Level.Info, message: "Wybrano język: " + SelectedLanguage);
            return SelectedLanguage;
        }

        public void SetLanguage(string language)
        {
            if (_messages.ContainsKey(language))
            {
                _selectedLanguage = language;
                AuditLogger.Log(LogLevel.Level.Info, $"Wybrano język: {SelectedLanguage}");
            }
            else
            {
                AuditLogger.Log(LogLevel.Level.Fatal, $"Wybrano nieobsługiwany język: {language}");
                throw new ArgumentException($"Language {language} not supported.", nameof(language));
            }
        }

        public string GetMessage(string messageKey)
        {
            if (SelectedLanguage == null)
            {
                AuditLogger.Log(LogLevel.Level.Error, "Nie wybrano języka");
                throw new InvalidOperationException("Language has not been set.");
            }

            if (_messages[SelectedLanguage].ContainsKey(messageKey))
            {
                AuditLogger.Log(LogLevel.Level.Info, $"Wybrany język: {SelectedLanguage}");
                return _messages[SelectedLanguage][messageKey];
            }
            else
            {
                AuditLogger.Log(LogLevel.Level.Error, $"Nie znaleziono klucza wiadomości {messageKey}");
                return messageKey;

                // TO DO: Zwiększyć ilość kluczy wiadomości
                //AuditLogger.Log(LogLevel.Level.Info, $"Nie znaleziono klucza wiadomości {messageKey}");
                //throw new ArgumentException($"Message key {messageKey} not found.", nameof(messageKey));
            }
        }
        #endregion
    }
}
