using System.ComponentModel;

namespace DoorManagementSystem
{
    public class DoorViewCommandBase: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
