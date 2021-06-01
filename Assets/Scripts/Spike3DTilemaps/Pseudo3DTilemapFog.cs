using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Globals;
using static PlayerStaticFunctions;

namespace Assets.Scripts.Spike3DTilemaps
{
    public class Pseudo3DTilemapFog : MonoBehaviour
    {
        private GameObject[] tilemapGameObjects;
        private GameObject _player;
        public double previousZ;
        public double currentZ;
        void Start()
        {
            var list = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == TilemapTag)
                    list.Add(child.gameObject);
            }
            tilemapGameObjects = list.ToArray();
            
            _player = GetOrInstantiatePlayer(new Vector3(0, 0, 0), new Quaternion());

            previousZ = (int)_player.GetComponent<Pseudo3DPosition>().pseudo3DPosition.z;
        }

        void Update()
        {
            currentZ = _player.GetComponent<Pseudo3DPosition>().pseudo3DPosition.z;

            //if int change in z
            if (currentZ != previousZ) 
            {
                previousZ = currentZ;
                foreach (var tm in tilemapGameObjects)
                {
                    //ex: 1        = 2                                               - 1
                    var tzDif = tm.GetComponent<TilemapRenderer>().sortingOrder - currentZ;

                    var rgb = 1.0;
                    var a = 1.0;

                    if (tzDif < 0)
                        rgb = 1 + (tzDif / 10);
                    if (tzDif > 0)
                        a = 1 - (tzDif / 10);

                    var color = new Color((float)rgb,(float)rgb,(float)rgb,(float)a);

                    tm.GetComponent<Tilemap>().color = color;
                } 
            }
        }
    }
}
