using DoorManagementSystem.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace DoorManagementSystem
{
    public class DoorManagementHub
    {
        #region ctor

        public DoorManagementHub(DoorModel doorModel)
        {
            var hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:12443/DoorManagementHub").WithAutomaticReconnect().Build();

            hubConnection.On<string>("Add", (doorId) =>
            {
                doorModel.Ids.Add(doorId);
            });

            hubConnection.On<DoorModel>("Update", (record) =>
            {
                doorModel.Id = record.Id;
                doorModel.Label = record.Label;
                doorModel.IsOpen = record.IsOpen;
                doorModel.IsLocked = record.IsLocked;

            });

            hubConnection.On<string>("Remove", (doorId) =>
            {
                doorModel.Ids.Remove(doorId);
            });

            hubConnection.StartAsync();
        }

        #endregion
    }
}
