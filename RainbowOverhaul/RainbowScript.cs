using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RWCustom;
using System.IO;
using System.Reflection;

namespace Rainbow
{
    public class RainbowScript : MonoBehaviour
    {
        public static RainbowMod mod;
        public static int slot
        {
            get { return pm.rainWorld.options.saveSlot; }
        }

        /// <summary>
        /// RainWorld Instance.
        /// </summary>
        public static RainWorld rw;
        /// <summary>
        /// ProcessManager Instance.
        /// </summary>
        public static ProcessManager pm;

        public void Initialize()
        {
            
        }


        public void RunModification()
        {
            //StaticWorldPatch.AddCreatureTemplate();

            //StaticWorldPatch.ModifyRelationship();
        }


        public void AddAtlas()
        {
            /*
            var assembly = Assembly.GetExecutingAssembly();

            string[] names = assembly.GetManifestResourceNames();
            foreach (string name in names)
            {
                Debug.Log(name);
            }*/
            /*
            Texture2D texHornestFruit = DataManager.ReadAtlasPNG("HornestFruitAtlas");
            FAtlas atlasHornestFruit = Futile.atlasManager.LoadAtlasFromTexture("HornestFruit", texHornestFruit);
            string dataHornestFruit = DataManager.ReadAtlasTXT("HornestFruit");
            (atlasHornestFruit as patch_FAtlas).LoadAtlasDataFromString(dataHornestFruit);

            Texture2D texHornestCap = DataManager.ReadAtlasPNG("HornestCapAtlas");
            FAtlas atlasHornestCap = Futile.atlasManager.LoadAtlasFromTexture("HornestCap", texHornestCap);
            string dataHornestCap = DataManager.ReadAtlasTXT("HornestCap");
            (atlasHornestCap as patch_FAtlas).LoadAtlasDataFromString(dataHornestCap);
            */
        }



        public void Update()
        {
            if (rw == null)
            {
                rw = UnityEngine.Object.FindObjectOfType<RainWorld>();
                pm = rw.processManager;

                if (rw != null)
                {
                    RunModification();
                    AddAtlas();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //DataManager.EncodeExteriorPNG();
            }

        }

    }
}


/*
    CHECKSUM OF CC
    219cbda8355be230d87aea01b4c1b53a
    CHECKSUM OF DS
    60108f14b09d49b2e88b6f2d73c7515a
    CHECKSUM OF HI
    91193f8910bb244e91651be41fda65c6
    CHECKSUM OF GW
    cbd4c708161bb1d67e88f9ec01a805a6
    CHECKSUM OF SI
    14887f039f7f1f1d1fd7c68878a07f87
    CHECKSUM OF SU
    3210aa68e1c702296f59bb890a0e80dd
    CHECKSUM OF SH
    5904872a5821436002ab5bdc27e85f99
    CHECKSUM OF SL
    340f979afb547c26aca1c93eb30f3fc2
    CHECKSUM OF LF
    e5c450bdca530abddd599491dd56fde2
    CHECKSUM OF UW
    940339522b60a15f5f7b6e78ed03497f
    CHECKSUM OF SB
    72564ab4eafe3f9dcba248f5c7e105f8
    CHECKSUM OF SS
    10ef2ea6c399fa07c009ba05ab8f71ad


    string text = string.Empty;
    string[] array = File.ReadAllLines(string.Concat(new object[]
    {
    Custom.RootFolderDirectory(),
    "World",
    Path.DirectorySeparatorChar,
    "Regions",
    Path.DirectorySeparatorChar,
    "regions.txt"
    }));
    for (int i = 0; i < array.Length; i++)
    {
        text = ReadText(string.Concat(new object[]
        {
        "Regions",
        Path.DirectorySeparatorChar,
        array[i],
        Path.DirectorySeparatorChar,
        "world_",
        array[i],
        ".txt"
        }));
        Debug.Log(string.Concat("CHECKSUM OF ", array[i]));
        Debug.Log(Custom.Md5Sum(text));
    }
 */
