using UnityEngine;

namespace GGJ21
{
    public class Entity : MonoBehaviour, IClickable
    {
        [SerializeField, TextArea(5, 50)] private string description = "";
        public string Description => description;

        public virtual void Clicked()
        {
            Debug.Log($"Clicked on {name}");
            Debug.Log($"Description: {Description}");
        }
    }
}