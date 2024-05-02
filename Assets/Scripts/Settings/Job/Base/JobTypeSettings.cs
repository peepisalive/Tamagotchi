using UnityEngine;
using Core.Job;

namespace Settings.Job
{
    public abstract class JobTypeSettings : ScriptableObject
    {
        public abstract JobType Type { get; }

        [field: Header("Base")]
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Salary { get; private set; }
    }
}