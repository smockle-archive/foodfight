using UnityEngine;
using System.Collections;

public static class Classes {

    public static Class America = new Class("America",     //Default name: Hero. Increased attack and health.
                            "American",
                            1,
                            1,
                            0,
                            0,
                            0);       
    //public Class Paladin;                         //Increased attack and movement.
    //public Class Sniper;                          //Increased attack and range.
    public static Class Japan   = new Class("Japan",       //Default name: Assassin. Increased attack and crit strike.
                            "Japanese",
                            1,
                            0,
                            0,
                            0,
                            15);         
    //public Class GreatKnight;                     //Increased health and movement.
    //public Class MageKnight;                      //Inaccurate but nothing in FE really has those two traits. Increased health and range.
    public static Class France  = new Class("France",      //Default name: Ranger. Increased movement and range.
                            "French",
                            0,
                            0,
                            1,
                            1,
                            0);                            
}
