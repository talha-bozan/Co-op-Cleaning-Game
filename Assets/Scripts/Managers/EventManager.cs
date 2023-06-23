
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

    

//Events related to gameplay

#endregion




        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
            
            //VFX
            ONPlayParticleHere = null;
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
