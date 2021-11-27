using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DoorManagementSystem.Model
{
    public class DoorModel : DoorViewCommandBase
    {
        private readonly DoorManagementService.DoorManagementService _doorManagementService;

        #region ctor

        public DoorModel()
        {
            _doorManagementService = new DoorManagementService.DoorManagementService();
            var doorManagementHub = new DoorManagementHub(this);

            Ids = GetDoorIds().GetAwaiter().GetResult();
        }

        #endregion

        #region Properties

        #region Id

        private string _id;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        #endregion

        #region Label

        private string _label;

        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                NotifyPropertyChanged(nameof(Label));
            }
        }

        #endregion

        #region IsOpen

        private bool? _isOpen;

        public bool? IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isLocked == true && value == true)
                {
                    return;
                }

                _isOpen = value;
                NotifyPropertyChanged(nameof(IsOpen));
            }
        }

        #endregion

        #region IsLocked

        private bool? _isLocked;

        public bool? IsLocked
        {
            get => _isLocked;
            set
            {
                if (_isOpen == true && value == true)
                {
                    return;
                }

                _isLocked = value;
                NotifyPropertyChanged(nameof(IsLocked));
            }
        }

        #endregion

        #region Ids

        private ObservableCollection<string> _ids;

        public ObservableCollection<string> Ids
        {
            get => _ids;
            set
            {
                _ids = value;
                NotifyPropertyChanged(nameof(Ids));
            }
        }


        #endregion

        #endregion




        #region Remote Service Implementation

        public async Task<ObservableCollection<string>> GetDoorIds()
        {
            try
            {
                var response = await _doorManagementService.GetAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var value = JsonConvert.DeserializeObject<ObservableCollection<string>>(content);
                    return value;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public async Task<DoorModel> GetDoor(string doorId)
        {
            try
            {
                var response = await _doorManagementService.GetAsync(doorId).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var value = JsonConvert.DeserializeObject<DoorModel>(content);
                    return value;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


            return null;
        }

        public async Task<DoorModel> Add(DoorModel doorModel)
        {
            try
            {
                var body = JsonConvert.SerializeObject(doorModel);
                var response = await _doorManagementService.PostAsync(body).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var value = JsonConvert.DeserializeObject<DoorModel>(content);
                    return value;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public async Task<DoorModel> Update(DoorModel doorModel)
        {
            try
            {
                var body = JsonConvert.SerializeObject(doorModel);
                var response = await _doorManagementService.PutAsync(body).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var value = JsonConvert.DeserializeObject<DoorModel>(content);
                    return value;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public async Task<DoorModel> Remove(string doorId)
        {
            try
            {
                var response = await _doorManagementService.DeleteAsync(doorId).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var value = JsonConvert.DeserializeObject<DoorModel>(content);
                    return value;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        #endregion
    }
}
