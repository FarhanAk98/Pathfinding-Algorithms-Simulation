using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadthFirstSearch : MonoBehaviour
{
    Rem r2, r3;
    Color black, red, green;
    void Start()
    {
        black = new Color32(0, 0, 0, 255);
        red = new Color32(255, 0, 0, 255);
        green = new Color32(0, 255, 0, 255);
        r2 = new Rem();
        r3 = new Rem();
    }
    void FixedUpdate()
    {
        if (!Barriers.done)
        {
            r2.Clear();
            r3.Clear();
        }
    }
    public void Adv2(int y, int x)
    {
        Image img = Barriers.t.GetImage(y, x);
        if (img.GetComponent<Advance>().Dest)
        {
            if (img.color == green)
            {
                r3.AddRem(y, x);
            }
            if (y == r3.Get(0).a && x == r3.Get(0).b)
            {
                if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).color != black && !Barriers.t.Left(y, x).GetComponent<Advance>().visited2 && Barriers.t.Left(y, x).color != green)
                {
                    Barriers.t.Left(y, x).GetComponent<Advance>().Dest = true;
                    Barriers.t.Left(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Left(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Left(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Left(y, x).GetComponent<Advance>().start = true;
                    if (!r3.Contains(Barriers.t.GetY(Barriers.t.Left(y, x)), Barriers.t.GetX(Barriers.t.Left(y, x))))
                    {
                        r3.AddRem(Barriers.t.GetY(Barriers.t.Left(y, x)), Barriers.t.GetX(Barriers.t.Left(y, x)));
                    }
                }
                if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).color != black && !Barriers.t.Right(y, x).GetComponent<Advance>().visited2 && Barriers.t.Right(y, x).color != green)
                {
                    Barriers.t.Right(y, x).GetComponent<Advance>().Dest = true;
                    Barriers.t.Right(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Right(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Right(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Right(y, x).GetComponent<Advance>().start = true;
                    if (!r3.Contains(Barriers.t.GetY(Barriers.t.Right(y, x)), Barriers.t.GetX(Barriers.t.Right(y, x))))
                    {
                        r3.AddRem(Barriers.t.GetY(Barriers.t.Right(y, x)), Barriers.t.GetX(Barriers.t.Right(y, x)));
                    }
                }
                if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).color != black && !Barriers.t.Top(y, x).GetComponent<Advance>().visited2 && Barriers.t.Top(y, x).color != green)
                {
                    Barriers.t.Top(y, x).GetComponent<Advance>().Dest = true;
                    Barriers.t.Top(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Top(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Top(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Top(y, x).GetComponent<Advance>().start = true;
                    if (!r3.Contains(Barriers.t.GetY(Barriers.t.Top(y, x)), Barriers.t.GetX(Barriers.t.Top(y, x))))
                    {
                        r3.AddRem(Barriers.t.GetY(Barriers.t.Top(y, x)), Barriers.t.GetX(Barriers.t.Top(y, x)));
                    }
                }
                if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).color != black && !Barriers.t.Bottum(y, x).GetComponent<Advance>().visited2 && Barriers.t.Bottum(y, x).color != green)
                {
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().Dest = true;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().start = true;
                    if (!r3.Contains(Barriers.t.GetY(Barriers.t.Bottum(y, x)), Barriers.t.GetX(Barriers.t.Bottum(y, x))))
                    {
                        r3.AddRem(Barriers.t.GetY(Barriers.t.Bottum(y, x)), Barriers.t.GetX(Barriers.t.Bottum(y, x)));
                    }
                }
                r3.Remove(0);
                img.GetComponent<Advance>().start = false;
            }
        }
        else
        {
            if (img.color == red)
            {
                r2.AddRem(y, x);
            }
            if (y == r2.Get(0).a && x == r2.Get(0).b)
            {
                if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).color != black && !Barriers.t.Left(y, x).GetComponent<Advance>().visited && Barriers.t.Left(y, x).color != red)
                {
                    Barriers.t.Left(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Left(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Left(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Left(y, x).GetComponent<Advance>().start = true;
                    if (!r2.Contains(Barriers.t.GetY(Barriers.t.Left(y, x)), Barriers.t.GetX(Barriers.t.Left(y, x))))
                    {
                        r2.AddRem(Barriers.t.GetY(Barriers.t.Left(y, x)), Barriers.t.GetX(Barriers.t.Left(y, x)));
                    }
                }
                if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).color != black && !Barriers.t.Right(y, x).GetComponent<Advance>().visited && Barriers.t.Right(y, x).color != red)
                {
                    Barriers.t.Right(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Right(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Right(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Right(y, x).GetComponent<Advance>().start = true;
                    if (!r2.Contains(Barriers.t.GetY(Barriers.t.Right(y, x)), Barriers.t.GetX(Barriers.t.Right(y, x))))
                    {
                        r2.AddRem(Barriers.t.GetY(Barriers.t.Right(y, x)), Barriers.t.GetX(Barriers.t.Right(y, x)));
                    }
                }
                if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).color != black && !Barriers.t.Top(y, x).GetComponent<Advance>().visited && Barriers.t.Top(y, x).color != red)
                {
                    Barriers.t.Top(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Top(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Top(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Top(y, x).GetComponent<Advance>().start = true;
                    if (!r2.Contains(Barriers.t.GetY(Barriers.t.Top(y, x)), Barriers.t.GetX(Barriers.t.Top(y, x))))
                    {
                        r2.AddRem(Barriers.t.GetY(Barriers.t.Top(y, x)), Barriers.t.GetX(Barriers.t.Top(y, x)));
                    }
                }
                if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).color != black && !Barriers.t.Bottum(y, x).GetComponent<Advance>().visited && Barriers.t.Bottum(y, x).color != red)
                {
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().yp = y;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().xp = x;
                    Barriers.t.Bottum(y, x).GetComponent<Advance>().start = true;
                    if (!r2.Contains(Barriers.t.GetY(Barriers.t.Bottum(y, x)), Barriers.t.GetX(Barriers.t.Bottum(y, x))))
                    {
                        r2.AddRem(Barriers.t.GetY(Barriers.t.Bottum(y, x)), Barriers.t.GetX(Barriers.t.Bottum(y, x)));
                    }
                }
                r2.Remove(0);
                img.GetComponent<Advance>().start = false;
            }
        }
    }
}
