using System.Collections.Generic;
using System.Linq;
using System.Text;
using Partiality.Modloader;
using UnityEngine;

namespace Rainbow
{
    public class RainbowMod : PartialityMod
    {
        public RainbowMod()
        {
            this.ModID = "Rainbow Overhaul";
            this.Version = "0100";
            this.author = "topicular";
            this.coAuther = "Empathy Module & Fyre & Garrakx";
        }
        public string coAuther;

        public static RainbowScript script;

        public override void OnLoad()
        {
            base.OnLoad();
            RainbowScript.mod = this;
            GameObject go = new GameObject();
            script = go.AddComponent<RainbowScript>();
            script.Initialize();

        }

    }
}
