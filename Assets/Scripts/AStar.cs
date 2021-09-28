using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AStar : MonoBehaviour
{
    bool Moving = true, started = false;
    Image img, left, right, top, bottum;
    Color black, red;
    Rem r = new Rem();
    int Minx = 0, Maxx = 0, Miny = 0, Maxy = 0, M = 100;
    void Start()
    {
        black = new Color32(0, 0, 0, 255);
        red = new Color32(255, 0, 0, 255);
    }
    void Update()
    {
        if (!Barriers.done)
        {
            r.Clear();
        }
    }
    void FixedUpdate()
    {
        if (M > 0)
        {
            Moving = true;
            M = (int)Mathf.Lerp(M, 0, 6f * Time.deltaTime);
        }
        else if (M <= 0)
        {
            Moving = false;
        }
        if (!Moving && started && Barriers.done && Barriers.Alg == 1)
        {
            int ty = 0, tx = 0;
            if (r.Count() > 0)
            {
                ty = r.Get(0).a;
                tx = r.Get(0).b;
                r.Clear();
            }
            if (Minx <= Maxx)
            {
                if (Minx > 0)
                {
                    Minx--;
                }
                if (Maxx < 76)
                {
                    Maxx++;
                }
            }
            else if (Minx > Maxx)
            {
                if (Minx < 76)
                {
                    Minx++;
                }
                if (Maxx > 0)
                {
                    Maxx--;
                }
            }
            if (Miny >= Maxy)
            {
                if (Miny < 38)
                {
                    Miny++;
                }
                if (Maxy > 0)
                {
                    Maxy--;
                }
            }
            else if (Miny < Maxy)
            {
                if (Miny > 0)
                {
                    Miny--;
                }
                if (Maxy < 38)
                {
                    Maxy++;
                }
            }
            M = 100;
            if (Barriers.t.Left(ty, tx) != null && Barriers.t.Left(ty, tx).color != black && !Barriers.t.Left(ty, tx).GetComponent<Advance>().visited && !Barriers.t.Left(ty, tx).GetComponent<Advance>().visited2 && Barriers.t.Left(ty, tx).color != red)
            {
                if (img.GetComponent<Advance>().Dest)
                {
                    Barriers.t.Left(ty, tx).GetComponent<Advance>().Dest = true;
                }
                Barriers.t.Left(ty, tx).GetComponent<Advance>().start = true;
            }
            if (Barriers.t.Right(ty, tx) != null && Barriers.t.Right(Miny, Minx).color != black && !Barriers.t.Right(ty, tx).GetComponent<Advance>().visited && !Barriers.t.Right(ty, tx).GetComponent<Advance>().visited2 && Barriers.t.Right(ty, tx).color != red)
            {
                if (img.GetComponent<Advance>().Dest)
                {
                    Barriers.t.Right(ty, tx).GetComponent<Advance>().Dest = true;
                }
                Barriers.t.Right(Miny, Minx).GetComponent<Advance>().start = true;
            }
            if (Barriers.t.Top(ty, tx) != null && Barriers.t.Top(ty, tx).color != black && !Barriers.t.Top(ty, tx).GetComponent<Advance>().visited && !Barriers.t.Top(ty, tx).GetComponent<Advance>().visited2 && Barriers.t.Top(ty, tx).color != red)
            {
                if (img.GetComponent<Advance>().Dest)
                {
                    Barriers.t.Top(ty, tx).GetComponent<Advance>().Dest = true;
                }
                Barriers.t.Top(ty, tx).GetComponent<Advance>().start = true;
            }
            if (Barriers.t.Bottum(ty, tx) != null && Barriers.t.Bottum(ty, tx).color != black && !Barriers.t.Bottum(ty, tx).GetComponent<Advance>().visited && !Barriers.t.Bottum(ty, tx).GetComponent<Advance>().visited2 && Barriers.t.Bottum(ty, tx).color != red)
            {
                if (img.GetComponent<Advance>().Dest)
                {
                    Barriers.t.Bottum(ty, tx).GetComponent<Advance>().Dest = true;
                }
                Barriers.t.Bottum(ty, tx).GetComponent<Advance>().start = true;
            }
        }
    }
    public void MinMax(int y1, int x1, int y2, int x2)
    {
        Miny = y1;
        Minx = x1;
        Maxy = y2;
        Maxx = x2;
    }
    public void Adv2(int y, int x)
    {
        started = true;
        left = right = top = bottum = img = Barriers.t.GetImage(y, x);
        if(Barriers.t.Left(y, x) != null)
        {
            left = Barriers.t.Left(y, x);
        }
        if (Barriers.t.Right(y, x) != null)
        {
            right = Barriers.t.Right(y, x);
        }
        if(Barriers.t.Top(y, x) != null)
        {
            top = Barriers.t.Top(y, x);
        }
        if(Barriers.t.Bottum(y, x) != null)
        {
            bottum = Barriers.t.Bottum(y, x);
        }

        int c = 0;
        if(left.color != black && !left.GetComponent<Advance>().visited && !left.GetComponent<Advance>().visited2 && left.color != red && ((Barriers.t.GetX(left) >= Minx && Barriers.t.GetX(left) <= Maxx && Minx < Maxx) || 
                                                                              (Barriers.t.GetX(left) <= Minx && Barriers.t.GetX(left) >= Maxx && Minx > Maxx)))
        {
            c++;
            if (img.GetComponent<Advance>().Dest)
            {
                left.GetComponent<Advance>().Dest = true;
            }
            left.GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            left.GetComponent<Advance>().yp = y;
            left.GetComponent<Advance>().xp = x;
            left.GetComponent<Advance>().start = true;
        }
        else if ((left.GetComponent<Advance>().visited2 && img.GetComponent<Advance>().Dest) || (left.GetComponent<Advance>().visited && !img.GetComponent<Advance>().Dest))
        {
            left.GetComponent<Advance>().Checked = true;
        }

        if (right.color != black && !right.GetComponent<Advance>().visited && !right.GetComponent<Advance>().visited2 && right.color != red && ((Barriers.t.GetX(right) >= Minx && Barriers.t.GetX(right) <= Maxx && Minx < Maxx) ||
                                                                                  (Barriers.t.GetX(right) <= Minx && Barriers.t.GetX(right) >= Maxx && Minx > Maxx)))
        {
            c++;
            if (img.GetComponent<Advance>().Dest)
            {
                right.GetComponent<Advance>().Dest = true;
            }
            right.GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            right.GetComponent<Advance>().yp = y;
            right.GetComponent<Advance>().xp = x;
            right.GetComponent<Advance>().start = true;
        }
        else if ((right.GetComponent<Advance>().visited2 && img.GetComponent<Advance>().Dest) || (right.GetComponent<Advance>().visited && !img.GetComponent<Advance>().Dest))
        {
            right.GetComponent<Advance>().Checked = true;
        }

        if (top.color != black && !top.GetComponent<Advance>().visited && !top.GetComponent<Advance>().visited2 && top.color != red && ((Barriers.t.GetY(top) >= Miny && Barriers.t.GetY(top) <= Maxy && Miny < Maxy) ||
                                                                            (Barriers.t.GetY(top) <= Miny && Barriers.t.GetY(top) >= Maxy && Miny > Maxy)))
        {
            c++;
            if (img.GetComponent<Advance>().Dest)
            {
                top.GetComponent<Advance>().Dest = true;
            }
            top.GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            top.GetComponent<Advance>().yp = y;
            top.GetComponent<Advance>().xp = x;
            top.GetComponent<Advance>().start = true;
        }
        else if ((top.GetComponent<Advance>().visited2 && img.GetComponent<Advance>().Dest) || (top.GetComponent<Advance>().visited && !img.GetComponent<Advance>().Dest))
        {
            top.GetComponent<Advance>().Checked = true;
        }

        if (bottum.color != black && !bottum.GetComponent<Advance>().visited && !bottum.GetComponent<Advance>().visited2 && bottum.color != red && ((Barriers.t.GetY(bottum) >= Miny && Barriers.t.GetY(bottum) <= Maxy && Miny < Maxy) ||
                                                                                     (Barriers.t.GetY(bottum) <= Miny && Barriers.t.GetY(bottum) >= Maxy && Miny > Maxy)))
        {
            c++;
            if (img.GetComponent<Advance>().Dest)
            {
                bottum.GetComponent<Advance>().Dest = true;
            }
            bottum.GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            bottum.GetComponent<Advance>().yp = y;
            bottum.GetComponent<Advance>().xp = x;
            bottum.GetComponent<Advance>().start = true;
        }
        else if ((bottum.GetComponent<Advance>().visited2 && img.GetComponent<Advance>().Dest) || (bottum.GetComponent<Advance>().visited && !img.GetComponent<Advance>().Dest))
        {
            bottum.GetComponent<Advance>().Checked = true;
        }
        if (c > 0)
        {
            M = 100;
        }
        if (((left.color == black || left.GetComponent<Advance>().visited || left.GetComponent<Advance>().visited2 || left.color == red || left == img) && (right.color == black || right.GetComponent<Advance>().visited || right.GetComponent<Advance>().visited2 || right.color == red || right == img) &&
            (top.color == black || top.GetComponent<Advance>().visited || top.GetComponent<Advance>().visited2 || top.color == red || top == img) && (bottum.color == black || bottum.GetComponent<Advance>().visited || bottum.GetComponent<Advance>().visited2 || bottum.color == red || bottum== img)))
        {
            img.GetComponent<Advance>().start = false;
        }
        else
        {
            r.AddRem(y, x);
        }
    }
}

class Rem
{
    public int a, b;
    List<Rem> re = new List<Rem>();
    public Rem()
    {
    }
    public Rem(int A, int B)
    {
        a = A;
        b = B;
    }
    public void AddRem(int a, int b)
    {
        re.Add(new Rem(a, b));
    }
    public void Remove(int i)
    {
        re.Remove(re[i]);
    }
    public void Clear()
    {
        re.Clear();
    }
    public Rem Get(int i)
    {
        return re[i];
    }
    public bool Contains(int y, int x)
    {
        for(int i = 0; i < re.Count; i++)
        {
            if(re[i].a == y && re[i].b == x)
            {
                return true;
            }
        }
        return false;
    }
    public int Count()
    {
        return re.Count;
    }
}
