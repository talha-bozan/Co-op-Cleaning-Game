
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        
        #region Level Status

        //DEFINE
        public event System.Action<bool> ONLevelEnd;
        public event System.Action ONLevelStart;
        
        //FUNCS
        public void OnONLevelStart(){
            ONLevelStart?.Invoke();
        }
        public void OnONLevelEnd(bool isSuccess)
        {
            ONLevelEnd?.Invoke(isSuccess);
        }

        #endregion

        #region VFX

        //DEFINE
        public event System.Action<Vector3> ONPlayParticleHere;
        
        //FUNCS
        public void OnONPlayParticleHere(Vector3 position)
        {
            ONPlayParticleHere?.Invoke(position);
        }




        #endregion






    #region Gameplay

        #region Trash Collection
        public event System.Action<int> ONTrashCollected;
        public void OnONTrashCollected(int roomId)
        {
            ONTrashCollected?.Invoke(roomId);

        }

        public event System.Action<Vector3, Quaternion> ONTrashDropped;
        public void OnONTrashDropped(Vector3 position, Quaternion rotation)
        {
            ONTrashDropped?.Invoke(position, rotation);

        }
        #endregion

        #region City Trash
        public event System.Action<bool> ONCityTrashIsFull;
        public void OnONCityTrashIsFull(bool isFull)
        {
            ONCityTrashIsFull?.Invoke(isFull);

        }
        #endregion

        #region UI
        public event System.Action<int> ONGarbageDumped;
        public void OnONGarbageDumped(int userId){
            ONGarbageDumped?.Invoke(userId);
        }

        public event System.Action<int> ONAllTrashCleaned;
        public void OnONAllTrashCleaned(int userId){
           ONAllTrashCleaned?.Invoke(userId);
        }
        #endregion

    #endregion




        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
            
            //VFX
            ONPlayParticleHere = null;

            //Trash Movement
            ONTrashDropped = null;
            ONTrashCollected = null;
            ONCityTrashIsFull = null;
            ONTrashDropped = null;
            ONTrashCollected = null;
            ONAllTrashCleaned = null;

            //UI
            ONGarbageDumped = null;
            
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
