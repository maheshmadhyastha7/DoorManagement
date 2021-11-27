using DoorManagementSystem.Model;
using DoorManagementSystem.RelayCommands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace DoorManagementSystem.ViewModel
{
    public class DoorViewModel : DoorViewCommandBase
    {
        private readonly DoorModel _model;

        #region ctor

        public DoorViewModel()
        {
            _model = new DoorModel();
            _door = new DoorModel();
            AddDoorCommand = new RelayCommand(AddDoor, CanAddDoor);
            ReviewDoorCommand = new RelayCommand(ReviewDoor, CanReviewDoor);
            RemoveDoorCommand = new RelayCommand(RemoveDoor, CanRemoveDoor);
            ControlDoorCommand = new RelayCommand(ContrlDoor, CanControlDoor);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            ConfigureCommand = new RelayCommand(Configure, CanConfigure);
            UpdateDoorCommand = new RelayCommand(UpdateDoor, CanUpdateDoor);
        }

        #endregion

        #region Relay commmand methods

        #region Add
        private bool CanAddDoor(object arg)
        {
            return true;
        }

        private void AddDoor(object obj)
        {
            if (obj is DoorModel)
            {
                var door = obj as DoorModel;
                door.Id = Guid.NewGuid().ToString();
                _door.Add(door).GetAwaiter().GetResult();
                ClearDoor();
                this.IsDeleteVisible = Visibility.Visible;
            }
        }
        #endregion

        #region Review
        private bool CanReviewDoor(object arg)
        {
            if (arg != null)
            {
                return true;
            }
            return false;
        }

        private void ReviewDoor(object obj)
        {
            if (obj != null)
            {
                GetDoor(obj);
                this.IsControlVisible = Visibility.Collapsed;
                this.IsCancelVisible = Visibility.Visible;
                this.IsConfigureVisible = Visibility.Collapsed;
                this.IsDeleteVisible = Visibility.Collapsed;
                this.IsReviewVisible = Visibility.Collapsed;
            }
        }
        #endregion

        #region Remove
        private bool CanRemoveDoor(object arg)
        {
            if (DoorIdList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RemoveDoor(object obj)
        {
            var door = (string)obj;
            _door.Remove(door).GetAwaiter().GetResult();
        }
        #endregion

        #region Control

        private void ContrlDoor(object obj)
        {
            if (obj != null)
            {
                GetDoor(obj);
                this.IsReviewVisible = Visibility.Collapsed;
                this.IsCancelVisible = Visibility.Visible;
                this.IsDeleteVisible = Visibility.Collapsed;
                this.IsConfigureVisible = Visibility.Collapsed;
                this.IsUpdateVisible = Visibility.Visible;
                this.IsEnabled = true;
                this.IsLabelEnabled = false;
            }
        }

        private bool CanControlDoor(object arg)
        {
            if (arg != null)
            {
                return true;
            }
            return false;

        }
        #endregion

        #region Cancel

        private bool CanCancel(object arg) => true;

        private void Cancel(object obj)
        {
            ClearDoor();
            this.IsReviewVisible = Visibility.Visible;
            this.IsConfigureVisible = Visibility.Visible;
            this.IsControlVisible = Visibility.Visible;
            this.IsCancelVisible = Visibility.Collapsed;
            this.IsAddVisible = Visibility.Collapsed;
            this.IsDeleteVisible = Visibility.Collapsed;
            this.IsUpdateVisible = Visibility.Collapsed;
            this.IsEnabled = false;
            this.IsLabelEnabled = false;
        }
        #endregion

        #region Configure
        private bool CanConfigure(object arg) => true;

        private void Configure(object arg)
        {
            this.IsReviewVisible = Visibility.Collapsed;
            this.IsConfigureVisible = Visibility.Collapsed;
            this.IsControlVisible = Visibility.Collapsed;
            this.IsCancelVisible = Visibility.Visible;
            this.IsAddVisible = Visibility.Visible;
            this.IsDeleteVisible = Visibility.Visible;
            this.IsEnabled = true;
            this.IsLabelEnabled = true;
        }
        #endregion

        #region Update

        private bool CanUpdateDoor(object arg)
        {
            if (arg != null)
            {
                return true;
            }
            return false;
        }

        private void UpdateDoor(object arg)
        {
            if(arg is DoorModel)
            {
                var door = arg as DoorModel;
                _door.Update(door).GetAwaiter().GetResult();
                MessageBox.Show($"Successfully updated the door {door.Label}");
            }
           
        }

        #endregion


        #endregion


        #region Helper Methods

        private void GetDoor(object obj)
        {
            var doorId = (string)obj;
            var doorRes = _door.GetDoor(doorId).GetAwaiter().GetResult();
            _door.Id = doorRes.Id;
            _door.IsLocked = doorRes.IsLocked;
            _door.IsOpen = doorRes.IsOpen;
            _door.Label = doorRes.Label;
        }

        private void ClearDoor()
        {
            _door.Id = string.Empty;
            _door.IsLocked = null;
            _door.IsOpen = null;
            _door.Label = string.Empty;
        }
        #endregion

        #region Properties

        private bool _isLabelEnabled;
        public bool IsLabelEnabled
        {
            get => _isLabelEnabled;
            set
            {
                _isLabelEnabled = value;
                NotifyPropertyChanged(nameof(IsLabelEnabled));
            }
        }


        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled; 
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged(nameof(IsEnabled));
            }
        }

        private Visibility _isUpdateVisible;
        public Visibility IsUpdateVisible
        {
            get => _isUpdateVisible;
            set
            {
                _isUpdateVisible = value;
                NotifyPropertyChanged(nameof(IsUpdateVisible));
            }
        }

        private Visibility _isDeleteVisible;
        public Visibility IsDeleteVisible
        {
            get => _isDeleteVisible;
            set
            {
                _isDeleteVisible = value;
                NotifyPropertyChanged(nameof(IsDeleteVisible));
            }
        }

        private Visibility _isAddVisible;
        public Visibility IsAddVisible
        {
            get => _isAddVisible;
            set
            {
                _isAddVisible = value;
                NotifyPropertyChanged(nameof(IsAddVisible));
            }
        }

        private Visibility _isReviewVisible;
        public Visibility IsReviewVisible
        {
            get => _isReviewVisible;
            set
            {
                _isReviewVisible = value;
                NotifyPropertyChanged(nameof(IsReviewVisible));
            }
        }


        private Visibility _isConfigureVisible;
        public Visibility IsConfigureVisible
        {
            get => _isConfigureVisible;
            set
            {
                _isConfigureVisible = value;
                NotifyPropertyChanged(nameof(IsConfigureVisible));
            }
        }

        private Visibility _isControlVisible;
        public Visibility IsControlVisible
        {
            get => _isControlVisible;
            set
            {
                _isControlVisible = value;
                NotifyPropertyChanged(nameof(IsControlVisible));
            }
        }

        private Visibility _isCancelVisible;
        public Visibility IsCancelVisible
        {
            get => _isCancelVisible;
            set
            {
                _isCancelVisible = value;
                NotifyPropertyChanged(nameof(IsCancelVisible));
            }
        }

        #region Door

        private DoorModel _door;

        public DoorModel Door
        {
            get => _door;
            set
            {
                _door = value;
                NotifyPropertyChanged(nameof(Door));
            }
        }

        #endregion

        #region Doors

        public ObservableCollection<string> DoorIdList
        {
            get => _model.Ids;
            set => _model.Ids = value;
        }


        #endregion

        #region Coomands

        public RelayCommand AddDoorCommand { get; set; }

        public RelayCommand ReviewDoorCommand { get; set; }

        public RelayCommand RemoveDoorCommand { get; set; }

        public RelayCommand ControlDoorCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public RelayCommand ConfigureCommand { get; set; }

        public RelayCommand UpdateDoorCommand { get; set; }

        #endregion

        #endregion

    }
}
