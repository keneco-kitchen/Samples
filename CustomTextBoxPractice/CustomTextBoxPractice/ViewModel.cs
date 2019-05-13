using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomTextBoxPractice
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string _Name = "Hejlsberg";
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value == _Name)
                    return;
                _Name = value;
                RaisePropertyChanged();
            }
        }

        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set
            {
                if (value == _index)
                    return;
                _index = value;
                RaisePropertyChanged();
            }
        }
    }
}
