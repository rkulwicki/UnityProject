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
        public int previousZ;
        public int currentZ;
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
            currentZ = (int)_player.GetComponent<Pseudo3DPosition>().pseudo3DPosition.z;

            //if int change in z
            if (currentZ != previousZ) 
            {
                previousZ = currentZ;
                foreach (var tm in tilemapGameObjects)
                {
                    //ex: 1        = 2                                               - 1
                    var tzDif = tm.GetComponent<TilemapRenderer>().sortingOrder - currentZ;

                    var rgb = 1f;
                    var a = 1f;

                    //algorithm to add fog:
                    if (tzDif < -9)
                        rgb = 0;
                    else if (tzDif == -9)
                        rgb = 0.1f;
                    else if (tzDif == -8)
                        rgb = 0.2f;
                    else if (tzDif == -7)
                        rgb = 0.3f;
                    else if (tzDif == -6)
                        rgb = 0.4f;
                    else if (tzDif == -5)
                        rgb = 0.5f;
                    else if (tzDif == -4)
                        rgb = 0.6f;
                    else if (tzDif == -3)
                        rgb = 0.7f;
                    else if (tzDif == -2)
                        rgb = 0.8f;
                    else if (tzDif == -1)
                        rgb = 0.9f;
                    //tzDiff == 0 stays the same
                    else if (tzDif == 1)
                        a = 0.9f;
                    else if (tzDif == 2)
                        a = 0.8f;
                    else if (tzDif == 3)
                        a = 0.7f;
                    else if (tzDif == 4)
                        a = 0.6f;
                    else if (tzDif == 5)
                        a = 0.5f;
                    else if (tzDif == 6)
                        a = 0.4f;
                    else if (tzDif == 7)
                        a = 0.3f;
                    else if (tzDif == 8)
                        a = 0.2f;
                    else if (tzDif == 9)
                        a = 0.1f;
                    else if (tzDif > 1)
                        a = 0;

                    var color = new Color(rgb,rgb,rgb,a);

                    tm.GetComponent<Tilemap>().color = color;
                } 
            }
        }
    }
}
