using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace GameOfLife.Data
{
    public class DataContainer
    {
        #region Fields
        
        private static Dictionary<Type, Object> dataPool = new Dictionary<Type, Object>();

        #endregion

        
        
        #region Public methods

        public static GameObjects GameObjects => GetData<GameObjects>("Data/GameObjects");
        
        #endregion



        #region Private methods

        private static TDataType GetData<TDataType>(string path) where TDataType : ScriptableObject
        {
            Type type = typeof(TDataType);

            if (!dataPool.ContainsKey(type))
            {
                TDataType data = Resources.Load<TDataType>(path);
                dataPool.Add(type, data);
                return data;
            }

            return dataPool[type] as TDataType;
        }

        #endregion
    }
}
