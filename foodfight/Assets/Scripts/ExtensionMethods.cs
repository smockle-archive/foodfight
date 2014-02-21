using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

    /// <summary>
    /// <para>Maps a position on the grid to a position in the "world".</para>
    /// <para>For example, (0,0) on the grid exists at something like (-10,-4) in Unity's idea of the "world".</para>
    /// <para>This works for any grid.</para>
    /// </summary>
    /// <param name="c">The camera we're using to translate from the grid to the world. Use Camera.main most of the time.</param>
    /// <param name="x">The x position on the grid we're coming from. In our example above, x is 0.</param>
    /// <param name="y">The y position on the grid we're coming from. In our example above, y is 0.</param>
    /// <param name="g">The grid we're coming from.</param>
    /// <returns></returns>
    public static Vector3 GridToWorldPoint(this Camera c, int x, int y, Grid g)
    {
        return c.ScreenToWorldPoint(new Vector3(x * g.tileWidth, y * g.tileHeight, 0)) + new Vector3(1, 1);
    }

    public static Vector3 GridToWorldPoint(this Camera c, Vector2 v, Grid g)
    {
        return c.ScreenToWorldPoint(new Vector3(v.x * g.tileWidth, v.y * g.tileHeight, 0)) + new Vector3(1, 1);
    }

    public static Vector3 GridToWorldPoint(this Camera c, Vector3 v, Grid g)
    {
        return c.ScreenToWorldPoint(new Vector3(v.x * g.tileWidth, v.y * g.tileHeight, v.z)) + new Vector3(1, 1);
    }

    /// <summary>
    /// <para>Maps a position in the "world" to a position on the grid.</para>
    /// <para>For example, (0,0) on the grid exists at something like (-10,-4) in Unity's idea of the "world".</para>
    /// <para>This works for any grid.</para>
    /// </summary>
    /// <param name="c">The camera we're using to translate from the grid to the world. Use Camera.main most of the time.</param>
    /// <param name="x">The x position in the world. In our example above, x is -10.</param>
    /// <param name="y">The y position in the world. In our example above, y is -4.</param>
    /// <param name="g">The grid we're mapping to.</param>
    /// <returns>A Vector3 object representing the point on the grid the input world position maps to.</returns>
    public static Vector3 WorldToGridPoint(this Camera c, int x, int y, Grid g)
    {
        return c.WorldToScreenPoint(new Vector3(x / g.tileWidth, y / g.tileHeight, 0) - new Vector3(1, 1));
    }

    public static Vector3 WorldToGridPoint(this Camera c, Vector2 v, Grid g)
    {
        return c.WorldToScreenPoint(new Vector3(v.x / g.tileWidth, v.y / g.tileHeight, 0) - new Vector3(1, 1));
    }

    public static Vector3 WorldToGridPoint(this Camera c, Vector3 v, Grid g)
    {
        return c.WorldToScreenPoint(new Vector3(v.x / g.tileWidth, v.y / g.tileHeight, v.z) - new Vector3(1, 1));
    }


    /// <summary>
    /// <para>Maps a position on the screen to a position on the grid.</para>
    /// <para>Any element on the grid exists at a certain pixel on the screen, depending on screen resolution.</para>
    /// <para>This method is attached to the Camera class for consistency with the other grid conversion methods, but doesn't actually require the camera's input.</para>
    /// <para>This works for any grid.</para>
    /// </summary>
    /// <param name="c"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="g"></param>
    /// <returns></returns>
    public static Vector3 ScreenToGridPoint(this Camera c, int x, int y, Grid g)
    {
        return new Vector3(x / g.tileWidth, y / g.tileHeight, 0);
    }

    public static Vector3 ScreenToGridPoint(this Camera c, Vector2 v, Grid g)
    {
        return new Vector3(v.x / g.tileWidth, v.y / g.tileHeight, 0);
    }

    public static Vector3 ScreenToGridPoint(this Camera c, Vector3 v, Grid g)
    {
        return new Vector3(v.x / g.tileWidth, v.y / g.tileHeight, v.z);
    }
}
