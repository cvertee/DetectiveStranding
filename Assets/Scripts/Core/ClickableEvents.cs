using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Core
{
    class ClickableEvents : MonoBehaviour
    {
        public UnityEvent events;

        public void Invoke()
        {
            events.Invoke();
        }
    }
}
