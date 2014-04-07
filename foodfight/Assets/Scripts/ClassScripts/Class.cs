using UnityEngine;
using System.Collections;

public class Class : MonoBehaviour {

    public string name;
    public string adj;
    public int a; //attack damage bonus
    public int h; //max health bonus
    public int m; //movement range bonus
    public int r; //attack range bonus
    public int c; //crit strike chance (only one class should have this, even if we decide to mix and match others)

    public Class(string name, string adj, int a, int h, int m, int r, int c)
    {
        this.name = name;
        this.adj = adj;
        this.a = a;
        this.h = h;
        this.m = m;
        this.r = r;
        this.c = c;
    }
}


