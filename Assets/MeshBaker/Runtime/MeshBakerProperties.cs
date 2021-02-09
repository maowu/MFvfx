using UnityEngine;

namespace MRvfx
{
    public sealed partial class MeshBaker : MonoBehaviour
    {
        #region Editor-only property

        [SerializeField] MeshFilter[] _sources = null;
        //[SerializeField] SkinnedMeshRenderer[] _sources = null;
        [SerializeField] int _pointCount = 65536;

        void OnValidate()
        {
            _pointCount = Mathf.Max(64, _pointCount);

            // We assume that someone changed the values/references in the
            // serialized fields, so let us dispose the internal objects to
            // re-initialize them with the new values/references. #BADCODE
            DisposeInternals();


            using(var dataArray = Mesh.AcquireReadOnlyMeshData(_sources[0].sharedMesh))
            {
                for (var i = 0; i < dataArray.Length; i++)
                {
                    var data = dataArray[i];
                    Debug.Log(string.Format("{0}: {1}", i, dataArray[i].vertexCount));
                }
                    
            }
        }

        #endregion

        #region Hidden asset reference

        [SerializeField] ComputeShader _compute = null;

        #endregion

        #region Runtime-only properties

        public Texture PositionMap => _positionMap;
        public Texture VelocityMap => _velocityMap;
        public Texture NormalMap => _normalMap;
        public int VertexCount => _pointCount;

        #endregion

        #region Runtime utility

        bool IsValid
          => _sources != null && _sources.Length > 0;

        #endregion
    }

}

