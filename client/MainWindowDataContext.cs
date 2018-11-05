using manager;
using models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgu_key_guard
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        public MainWindowDataContext()
        {
            Console.WriteLine("Strating app");
            UI_KeyID = 1;
            
            UI_LoadedKey = "No key loaded.";
        }

        private string _UI_LoadedKey;
        public string UI_LoadedKey
        {
            get
            {
                return _UI_LoadedKey;
            }
            set
            {
                _UI_LoadedKey = value;
                OnPropertyChanged("UI_LoadedKey");
            }
        }

        private int _UI_KeyID;

        public int UI_KeyID
        {
            get { return _UI_KeyID; }
            set
            {
                _UI_KeyID = value;
                OnPropertyChanged("UI_KeyID");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnGetKeyButtonPressed()
        {
            Manager manager = new Manager();
            Key_Type key_Type = new Key_Type();
            key_Type.keyType = "Une";
            manager.insert(key_Type);
            manager.selectAll(key_Type);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
