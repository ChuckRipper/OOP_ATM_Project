using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Extensions
{
    public class MenuControl
    {
        #region Classes

        #endregion

        #region Fields 
        private List<string> _InterfaceList;
        private int _Position;
        #endregion

        #region Properties
        public List<string> InterfaceList
        {
            get { return _InterfaceList; }
            set { _InterfaceList = value; }
        }

        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        #endregion

        #region Constructors
        public MenuControl(List<string> interfaceList, int position)
        {
            _InterfaceList = interfaceList;
            _Position = position;
        }
        #endregion

        #region Methods
        public void Menu(List<string> options, int position = 0)
        {
            _InterfaceList = options;
            _Position = position;
        }

        // Krok w menu w górę
        public void MoveUp()
        {
            if (_Position > 0)
            {
                _Position--;
            }
            else
            {
                _Position = _InterfaceList.Count - 1;
            }
        }

        // Krok w menu w dół
        public void MoveDown()
        {
            if (_Position < _InterfaceList.Count - 1)
            {
                _Position++;
            }
            else
            {
                _Position = 0;
            }
        }

        // Pobranie aktualnej pozycji Menu
        public string getCurrentPosition()
        {
            return _InterfaceList[_Position];
        }
        #endregion
    }
}
